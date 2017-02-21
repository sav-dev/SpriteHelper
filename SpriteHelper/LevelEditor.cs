using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper
{
    public partial class LevelEditor : Form
    {
        // Configs.
        private Palettes palettes;
        private BackgroundConfig config;

        // Cached bitmaps.
        private Dictionary<string, Bitmap[]> tiles;
        
        // Default size.
        private const int DefaultWidthInTiles = 64;
        private const int HeightInTiles = 15;

        // Zoom and tile width
        private const int Zoom = 2;
        private const int TileWidth = Constants.BackgroundTileWidth * Zoom;
        private const int TileHeight = Constants.BackgroundTileHeight * Zoom;

        // Empty tile for the level.
        private string emptyTile;

        // Current level.
        private string[][] level;

        // History/future (for undo-redo).
        private Stack<string[][]> history;
        private Stack<string[][]> future;

        // Draw panel.
        private DoubleBufferedPanel drawPanel;
        private Bitmap bitmap;
        private Graphics graphics;

        #region FormRelated

        ////
        //// Form related
        ////

        public LevelEditor()
        {
            InitializeComponent();            
            UpdateStatus(null);

            this.history = new Stack<string[][]>();
            this.future = new Stack<string[][]>();

            this.drawPanel = new DoubleBufferedPanel();
            this.outerDrawPanel.Controls.Add(this.drawPanel);
            this.drawPanel.Location = new Point(0, 0);
            this.drawPanel.MouseDown += this.DrawPanelMouseDown;
            this.drawPanel.MouseLeave += this.DrawPanelMouseLeave;
            this.drawPanel.MouseMove += this.DrawPanelMouseMove;
        }

        private void LevelEditorLoad(object sender, EventArgs e)
        {
            this.PreLoad();
            this.splitContainerVertical.Panel2.Focus();
        }
        
        private void LevelEditorResize(object sender, EventArgs e)
        {
            this.UpdateDrawPanel();
        }
        
        private void PreLoad()
        {
            this.LoadLevel(Defaults.Instance.Level, Defaults.Instance.BackgroundSpec, Defaults.Instance.PalettesSpec);
        }

        private void UpdateStatus(string text)
        {
            this.toolStripStatusLabel.Text = text;
        }

        private void UpdateStatus(string format, params object[] args)
        {
            this.toolStripStatusLabel.Text = string.Format(format, args);
        }

        #endregion

        #region LevelLoadingAndSaving

        ////
        //// Level loading & saving
        ////

        private void LoadLevel(string level, string spec, string palettes)
        {
            this.palettes = Palettes.Read(palettes);
            this.config = BackgroundConfig.Read(spec);

            var bitmaps = new MyBitmap[this.config.BackgroundFiles.Max(f => f.Id) + 1];
            foreach (var file in this.config.BackgroundFiles)
            {
                bitmaps[file.Id] = MyBitmap.FromFile(file.FileName);
            }

            this.tiles = new Dictionary<string, Bitmap[]>();

            foreach (var tile in this.config.Tiles)
            {
                var tileBitmaps = new Bitmap[(int)TileVersion.All + 1];               
                var tileBitmap = bitmaps[tile.BackgroundFileId].GetPart(tile.X, tile.Y, tile.WidthInSprites * Constants.SpriteWidth, tile.HeightSprites * Constants.SpriteHeight).Scale(Zoom);

                // None
                tileBitmaps[(int)TileVersion.None] = tileBitmap.ToBitmap();

                // Grid
                var tileBitmapGrid = tileBitmap.Clone();
                tileBitmapGrid.DrawGrid();
                tileBitmaps[(int)TileVersion.Grid] = tileBitmapGrid.ToBitmap();

                // Palette
                var paletteMapping = this.config.PaletteMappings.First(pm => pm.Id == tile.PaletteMappingId);
                var palette = this.palettes.BackgroundPalette[paletteMapping.ToPalette];
                var tileBitmapPalette = tileBitmap.Clone();
                tileBitmapPalette.UpdateColors(paletteMapping.ColorMappings.OrderBy(cm => cm.To).Select(cm => Color.FromArgb(cm.R, cm.G, cm.B)).ToArray(), palette.ActualColors);
                tileBitmaps[(int)TileVersion.Palettes] = tileBitmapPalette.ToBitmap();
                
                // Palette and grid
                var tileBitmapGridWithPaletteApplied = tileBitmapPalette.Clone();
                tileBitmapGridWithPaletteApplied.DrawGrid();
                tileBitmaps[(int)(TileVersion.Palettes | TileVersion.Grid)] = tileBitmapGridWithPaletteApplied.ToBitmap();

                // Type
                foreach (var tileVersion in new[] { TileVersion.None, TileVersion.Grid, TileVersion.Palettes, TileVersion.Palettes | TileVersion.Grid })
                {
                    var bitmapForVersion = tileBitmaps[(int)tileVersion];
                    var bitmapCopy = new Bitmap(bitmapForVersion);

                    Brush brush = null;
                    switch (tile.Type)
                    {
                        case TileType.Blocking:
                            brush = new SolidBrush(Color.FromArgb(100, 0, 255, 0));
                            break;

                        case TileType.Threat:
                            brush = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
                            break;
                    }

                    if (brush != null)
                    { 
                        using (var graphics = Graphics.FromImage(bitmapCopy))
                        {
                            graphics.FillRectangle(brush, 0, 0, bitmapCopy.Width, bitmapCopy.Height);                                
                        }
                    }

                    tileBitmaps[(int)(tileVersion | TileVersion.Type)] = bitmapCopy;
                }

                this.tiles.Add(tile.Id, tileBitmaps);
            }

            this.PopulateListViews();

            // empty tile should always be the 1st one
            this.emptyTile = this.config.Tiles[0].Id;

            string[][] newLevel;
            if (File.Exists(level))
            {
                // todo: load level
                newLevel = null;
            }
            else
            {
                var widthInTiles = DefaultWidthInTiles;
                newLevel = new string[widthInTiles][];
                for (var i = 0; i < widthInTiles; i++)
                {
                    newLevel[i] = new string[HeightInTiles];
                    for (var j = 0; j < HeightInTiles; j++)
                    {
                        newLevel[i][j] = this.emptyTile;
                    }
                }
            }

            this.SetLevel(newLevel);            
        }

        #endregion

        #region ListView

        ////
        //// ListView related stuff
        ////

        private void PopulateListViews()
        {
            Action<TileType, ListView> populateListView = (tileType, listView) =>
            {
                listView.Items.Clear();
                listView.LargeImageList = new ImageList { ImageSize = new Size(TileWidth, TileHeight) };
                var index = 0;
                foreach (var kvp in this.tiles)
                {
                    if (this.config.Tiles.First(t => t.Id == kvp.Key).Type != tileType)
                    {
                        continue;
                    }

                    listView.LargeImageList.Images.Add(kvp.Value[this.ApplyPalettes ? (int)TileVersion.Palettes : (int)TileVersion.None]);
                    listView.Items.Add(new ListViewItem(kvp.Key, index++));
                }
            };

            populateListView(TileType.Blocking, this.listViewBlocking);
            populateListView(TileType.NonBlocking, this.listViewNonBlocking);
            populateListView(TileType.Threat, this.listViewThreat);
        }

        public string SelectedTile()
        {
            var selectedListView = this.tabControl.SelectedTab.Controls.Cast<object>().First(c => c is ListView) as ListView;
            if (selectedListView.SelectedItems.Count == 0)
            {
                return null;
            }

            return selectedListView.SelectedItems[0].Text;
        }

        #endregion

        #region DrawPanel

        ////
        //// DrawPanel related stuff
        ////

        public void UpdateDrawPanel()
        {
            ////
            //// Pre-checks
            ////

            var widthInTiles = this.level != null ? this.level.Length : 0;
            this.outerDrawPanel.Visible = widthInTiles > 0;
            if (!this.outerDrawPanel.Visible)
            {
                this.scrollBar.Minimum = 0;
                this.scrollBar.Maximum = 0;
                this.scrollBar.Enabled = false;
                return;
            }

            ////
            //// Position of the outer panels.
            ////

            var outerOuterPanelWidth = this.outerOuterDrawPanel.Width;
            var outerOuterPanelWidthInTiles = outerOuterPanelWidth / TileWidth;
            var outerPanelWidthInTiles = (int)Math.Min(outerOuterPanelWidthInTiles, widthInTiles);
            var outerPanelWidth = outerPanelWidthInTiles * TileWidth;
            var horizontalPadding = (outerOuterPanelWidth - outerPanelWidth) / 2;            
        
            var outerOuterPanelHeight = this.outerOuterDrawPanel.Height;
            var outerPanelHeightInTiles = HeightInTiles;
            var outerPanelHeight = outerPanelHeightInTiles * TileHeight;
            var verticalPadding = (outerOuterPanelHeight - outerPanelHeight) / 2;

            this.outerDrawPanel.Size = new Size(outerPanelWidth, outerPanelHeight);
            this.outerDrawPanel.Location = new Point(horizontalPadding, verticalPadding);

            ////
            //// Scroll bar
            ////

            var currentScroll = this.scrollBar.Value;
            this.scrollBar.Enabled = outerPanelWidthInTiles < widthInTiles;
            this.scrollBar.Minimum = 0;
            this.scrollBar.Maximum = widthInTiles - outerPanelWidthInTiles;
            this.scrollBar.Value = (int)Math.Min(currentScroll, this.scrollBar.Maximum); ;

            ////
            //// Draw panel.
            ////

            this.drawPanel.Size = this.bitmap.Size;
            this.drawPanel.BackgroundImage = this.bitmap;
            this.UpdateScroll();
        }

        private void ChangeWidth(int width)
        {
            // todo: change width
        }

        private void SetLevel(string[][] newLevel)
        {
            this.AddHistory();
            this.level = newLevel;
            this.UpdateBitmap();
        }

        private void UpdateBitmap()
        {
            this.bitmap = new Bitmap(this.level.Length * TileWidth, this.level[0].Length * TileHeight);
            this.graphics = Graphics.FromImage(this.bitmap);

            for (var x = 0; x < this.level.Length; x++)
            {
                for (var y = 0; y < this.level[x].Length; y++)
                {
                    var key = this.level[x][y];
                    var image = this.tiles[key][(int)this.Settings];
                    this.graphics.DrawImage(image, new Point(x * TileWidth, y * TileHeight));
                }
            }

            this.UpdateDrawPanel();
        }

        private void ScrollBarScroll(object sender, ScrollEventArgs e)
        {
            this.UpdateScroll();
        }

        private void UpdateScroll()
        {
            this.drawPanel.Location = new Point(-this.scrollBar.Value * TileWidth, this.drawPanel.Location.Y);
        }

        #endregion

        #region MenuItems

        ////
        //// Menu Items
        ////

        private void ShowTypeToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
        }

        private void ShowGridToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
        }

        private void ApplyPaletteToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.PopulateListViews();
            this.UpdateBitmap();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            var loadLevelDialog = new LoadLevelDialog();
            loadLevelDialog.FormClosed += (notUsed1, notUsed2) =>
            {
                if (loadLevelDialog.ClickedOk)
                {
                    this.LoadLevel(loadLevelDialog.Level, loadLevelDialog.Spec, loadLevelDialog.Palettes);
                }
            };

            loadLevelDialog.ShowDialog();
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            // todo: save
        }

        private void ExportToolStripMenuItemClick(object sender, EventArgs e)
        {
            // todo: export
        }

        private void AdvancedToolStripMenuItemClick(object sender, EventArgs e)
        {
            var editLevelDialog = new EditLevelDialog(this.level.Length);
            editLevelDialog.FormClosed += (notUsed1, notUsed2) =>
            {
                switch (editLevelDialog.Result)
                {
                    case EditLevelDialogResult.WidthChange:
                        this.ChangeWidth(editLevelDialog.Width);
                        break;
                }
            };

            editLevelDialog.ShowDialog();
        }

        private void UndoToolStripMenuItemClick(object sender, EventArgs e)
        {
            // todo: undo
        }

        private void RedoToolStripMenuItemClick(object sender, EventArgs e)
        {
            // todo: redo
        }

        #endregion

        #region MenuGetters

        ////
        //// Menu getters
        ////

        public bool ApplyPalettes
        {
            get
            {
                return this.applyPaletteToolStripMenuItem.Checked;
            }
        }

        public bool ShowGrid
        {
            get
            {
                return this.showGridToolStripMenuItem.Checked;
            }
        }

        public bool ShowType
        {
            get
            {
                return this.showTypeToolStripMenuItem.Checked;
            }
        }

        public TileVersion Settings
        {
            get
            {
                var result = TileVersion.None;
                if (this.ApplyPalettes)
                {
                    result = result | TileVersion.Palettes;
                }

                if (this.ShowGrid)
                {
                    result = result | TileVersion.Grid;
                }

                if (this.ShowType)
                {
                    result = result | TileVersion.Type;
                }

                return result;
            }
        }

        #endregion

        #region MouseEvents

        ////
        //// Draw panel mouse events
        ////


        private void DrawPanelMouseLeave(object sender, EventArgs e)
        {
            this.UpdateStatus(null);
        }

        private void DrawPanelMouseDown(object sender, MouseEventArgs e)
        {
            var x = e.X / TileWidth;
            var y = e.Y / TileWidth;
            this.SetTile(x, y, e.Button);
        }

        private void DrawPanelMouseMove(object sender, MouseEventArgs e)
        {
            var x = e.X / TileWidth;
            var y = e.Y / TileWidth;
            this.UpdateStatus("{0} / {1}", x, y);
            this.SetTile(x, y, e.Button);
        }

        public void SetTile(int x, int y, MouseButtons buttons)
        {
            if (x >= this.level.Length || y >= this.level[0].Length)
            {
                return;
            }

            string tile;
            if (buttons.HasFlag(MouseButtons.Left))
            {
                tile = this.SelectedTile();
                if (tile == null)
                {
                    return;
                }
            }
            else if (buttons.HasFlag(MouseButtons.Right))
            {
                tile = emptyTile;
            }
            else
            {
                return;
            }
            
            if (this.level[x][y] == tile)
            {
                return;
            }

            this.AddHistory();
            this.level[x][y] = tile;
            var image = this.tiles[tile][(int)this.Settings];
            this.graphics.DrawImage(image, new Point(x * TileWidth, y * TileHeight));
            this.drawPanel.Refresh();
        }

        #endregion

        #region HelperTypes

        ////
        //// Helper types
        ////

        [Flags]
        public enum TileVersion
        {
            None = 0,
            Palettes = 1,
            Grid = 2,
            Type = 4,
            All = 7
        }

        #endregion

        #region History

        ////
        //// History stuff
        ////

        public void AddHistory()
        {
            if (this.level != null)
            {
                this.history.Push(this.level);
            }
        }

        #endregion
    }
}
