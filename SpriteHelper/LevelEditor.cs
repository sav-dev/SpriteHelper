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

        // Current scroll value.
        private int scroll;

        // Current level.
        private string[][] level;

        // History/future (for undo-redo).
        private Stack<string[][]> history;
        private Stack<string[][]> future;

        #region FormRelated

        ////
        //// Form related
        ////

        public LevelEditor()
        {
            InitializeComponent();            
            UpdateToolBar(null);
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

        private void UpdateToolBar(string text)
        {
            this.toolStripStatusLabel.Text = text;
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

            if (File.Exists(level))
            {
                // todo load level
            }
            else
            {
                var widthInTiles = DefaultWidthInTiles;
                this.scroll = 0;
                this.level = new string[widthInTiles][];
                for (var i = 0; i < widthInTiles; i++)
                {
                    this.level[i] = new string[HeightInTiles];
                    for (var j = 0; j < HeightInTiles; j++)
                    {
                        this.level[i][j] = this.emptyTile;
                    }
                }
            }

            this.scroll = 0;

            this.UpdateDrawPanel();
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
            this.drawPanel.Visible = widthInTiles > 0;
            if (!this.drawPanel.Visible)
            {
                this.scrollBar.Minimum = 0;
                this.scrollBar.Maximum = 0;
                this.scrollBar.Enabled = false;
                return;
            }

            ////
            //// Position, size
            ////

            var outerPanelWidth = this.outerDrawPanel.Width;
            var outerPanelWidthInTiles = outerPanelWidth / TileWidth;
            var panelWidthInTiles = (int)Math.Min(outerPanelWidthInTiles, widthInTiles);
            var panelWidth = panelWidthInTiles * TileWidth;
            var horizontalPadding = (outerPanelWidth - panelWidth) / 2;            
        
            var outerPanelHeight = this.outerDrawPanel.Height;
            var panelHeightInTiles = HeightInTiles;
            var panelHeight = panelHeightInTiles * TileHeight;
            var verticalPadding = (outerPanelHeight - panelHeight) / 2;

            this.drawPanel.Size = new Size(panelWidth, panelHeight);
            this.drawPanel.Location = new Point(horizontalPadding, verticalPadding);

            ////
            //// Scroll bar
            ////

            this.scrollBar.Enabled = panelWidthInTiles < widthInTiles;
            this.scrollBar.Minimum = 0;
            this.scrollBar.Maximum = widthInTiles - panelWidthInTiles;
            this.scroll = (int)Math.Min(this.scroll, this.scrollBar.Maximum);
            this.scrollBar.Value = this.scroll;
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
            // todo: show grid
        }

        private void ApplyPaletteToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.PopulateListViews();
            // todo: apply palette to draw panel
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
    }
}
