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
        private Dictionary<string, Bitmap> tiles;
        private Dictionary<string, Bitmap> tilesPaletteApplied;
        private Dictionary<string, Bitmap> tilesGrid;
        private Dictionary<string, Bitmap> tilesGridPaletteApplied;

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

        // The bitmap.
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
        }

        private void LevelEditorLoad(object sender, EventArgs e)
        {
            this.PreLoad();
            this.splitContainerVertical.Panel2.Focus();
        }
        
        private void LevelEditorResize(object sender, EventArgs e)
        {
            // todo: resize
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

            this.tiles = new Dictionary<string, Bitmap>();
            this.tilesPaletteApplied = new Dictionary<string, Bitmap>();
            this.tilesGrid = new Dictionary<string, Bitmap>();
            this.tilesGridPaletteApplied = new Dictionary<string, Bitmap>();

            foreach (var tile in this.config.Tiles)
            {
                var bitmap = bitmaps[tile.BackgroundFileId];
                var tileBitmap = bitmap.GetPart(tile.X, tile.Y, tile.WidthInSprites * Constants.SpriteWidth, tile.HeightSprites * Constants.SpriteHeight).Scale(Zoom);
                this.tiles.Add(tile.Id, tileBitmap.ToBitmap());

                var tileBitmapGrid = tileBitmap.Clone();
                tileBitmapGrid.DrawGrid();
                this.tilesGrid.Add(tile.Id, tileBitmapGrid.ToBitmap());

                var paletteMapping = this.config.PaletteMappings.First(pm => pm.Id == tile.PaletteMappingId);
                var palette = this.palettes.BackgroundPalette[paletteMapping.ToPalette];
                var tileBitmapWithPaletteApplied = new MyBitmap(tileBitmap.Width, tileBitmap.Height);

                for (var x = 0; x < tileBitmap.Width; x++)
                {
                    for (var y = 0; y < tileBitmap.Height; y++)
                    {
                        var color = tileBitmap.GetPixel(x, y);
                        var mappedColorId = paletteMapping.ColorMappings.First(c => c.Color == color).To;
                        var mappedColor = palette.ActualColors[mappedColorId];
                        tileBitmapWithPaletteApplied.SetPixel(mappedColor, x, y);
                    }
                }
                
                this.tilesPaletteApplied.Add(tile.Id, tileBitmapWithPaletteApplied.ToBitmap());

                var tileBitmapGridWithPaletteApplied = tileBitmapWithPaletteApplied.Clone();
                tileBitmapGridWithPaletteApplied.DrawGrid();
                this.tilesGridPaletteApplied.Add(tile.Id, tileBitmapGridWithPaletteApplied.ToBitmap());
            }

            this.PopulateListViews();

            // empty tile should always be 1st
            this.emptyTile = this.config.Tiles[0].Id;

            string[][] newLevel;
            if (File.Exists(level))
            {
                // todo load level
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
                foreach (var kvp in this.ApplyPalettes ? this.tilesPaletteApplied : tiles)
                {
                    if (this.config.Tiles.First(t => t.Id == kvp.Key).Type != tileType)
                    {
                        continue;
                    }

                    listView.LargeImageList.Images.Add(kvp.Value);
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

        private void SetLevel(string[][] newLevel)
        {
            if (this.level != null)
            {
                this.history.Push(this.level);
            }

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
                    var image = this.ApplyPalettes ? 
                        (this.ShowGrid ? this.tilesGridPaletteApplied[key] : this.tilesPaletteApplied[key]) : 
                        (this.ShowGrid ? this.tilesGrid[key] : this.tiles[key]);

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
            // todo: show type
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

        private void ZoomMenuItemClick(object sender, EventArgs e)
        {
            // todo: zoom
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
                        // todo: change width
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

        #endregion

        #region MouseEvents

        ////
        //// Draw panel mouse events
        ////


        private void DrawPanelMouseLeave(object sender, EventArgs e)
        {
            this.UpdateStatus(null);
        }

        private void DrawPanelMouseClick(object sender, MouseEventArgs e)
        {
            // todo
        }

        private void DrawPanelMouseMove(object sender, MouseEventArgs e)
        {
            this.UpdateStatus("{0} / {1}", e.X / TileWidth, e.Y / TileHeight);
        }

        #endregion
    }
}
