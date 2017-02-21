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
        private Palettes palettes;
        private BackgroundConfig config;

        private Dictionary<string, MyBitmap> tiles;
        private Dictionary<string, MyBitmap> tilesPaletteApplied;
        private Dictionary<string, MyBitmap> tilesGrid;
        private Dictionary<string, MyBitmap> tilesGridPaletteApplied;

        private int width;
        private const int DefaultWidth = 64;
        private const int Height = 15; // in tiles
        private string emptyTile;
        private string[][] level;    
        private int scroll;

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

        private void LoadButtonClick(object sender, EventArgs e)
        {
            this.LoadLevel();
        }

        private void BgColorTextBoxTextChanged(object sender, EventArgs e)
        {
            this.bgColorPanel.BackColor = this.GetBgColor();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            // todo
        }

        private void ExportButtonClick(object sender, EventArgs e)
        {
            // todo
        }

        private void LevelEditorResize(object sender, EventArgs e)
        {
            this.UpdateDrawPanel();
        }

        private void ZoomPickerValueChanged(object sender, EventArgs e)
        {
            this.UpdateDrawPanel();
        }

        private void ShowGridCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.UpdateDrawPanel();
        }

        private void ApplyPaletteCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.UpdateDrawPanel();
            this.PopulateListViews();
        }

        private void EditButtonClick(object sender, EventArgs e)
        {
            var editLevelDialog = new EditLevelDialog(this.width);
            editLevelDialog.FormClosed += (notUsed1, notUsed2) =>
            {
                switch (editLevelDialog.Result)
                {
                    case EditLevelDialogResult.WidthChange:
                        this.width = editLevelDialog.LevelWidth;
                        this.UpdateDrawPanel();
                        break;
                }
            };

            editLevelDialog.ShowDialog();
        }

        private void DrawPanelMouseMove(object sender, MouseEventArgs e)
        {
            var zoom = (int)this.zoomPicker.Value;
            var tileWidth = Constants.BackgroundTileWidth * zoom;
            var tileHeight = Constants.BackgroundTileHeight * zoom;
            this.UpdateToolBar(string.Format("{0} / {1}", e.X / tileWidth, e.Y / tileHeight));
        }

        private void DrawPanelMouseLeave(object sender, EventArgs e)
        {
            this.UpdateToolBar(null);
        }

        private void UpdateToolBar(string text)
        {
            this.toolStripStatusLabel.Text = text;
        }

        private void PreLoad()
        {
            this.levelTextBox.Text = Defaults.Instance.Level;
            this.specTextBox.Text = Defaults.Instance.BackgroundSpec;
            this.palettesTextBox.Text = Defaults.Instance.PalettesSpec;
            this.bgColorTextBox.Text = Defaults.Instance.DefaultBgColor;
            this.LoadLevel();
        }

        private void LoadLevel()
        {
            this.palettes = Palettes.Read(this.palettesTextBox.Text);
            this.config = BackgroundConfig.Read(this.specTextBox.Text);

            var bitmaps = new MyBitmap[this.config.BackgroundFiles.Max(f => f.Id) + 1];
            foreach (var file in this.config.BackgroundFiles)
            {
                bitmaps[file.Id] = MyBitmap.FromFile(file.FileName);
            }

            this.tiles = new Dictionary<string, MyBitmap>();
            this.tilesPaletteApplied = new Dictionary<string, MyBitmap>();
            this.tilesGrid = new Dictionary<string, MyBitmap>();
            this.tilesGridPaletteApplied = new Dictionary<string, MyBitmap>();

            foreach (var tile in this.config.Tiles)
            {
                var bitmap = bitmaps[tile.BackgroundFileId];
                var tileBitmap = bitmap.GetPart(tile.X, tile.Y, tile.WidthInSprites * Constants.SpriteWidth, tile.HeightSprites * Constants.SpriteHeight);
                this.tiles.Add(tile.Id, tileBitmap);

                var tileBitmapGrid = tileBitmap.Clone();
                tileBitmapGrid.DrawGrid();
                this.tilesGrid.Add(tile.Id, tileBitmapGrid);

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
                
                this.tilesPaletteApplied.Add(tile.Id, tileBitmapWithPaletteApplied);

                var tileBitmapGridWithPaletteApplied = tileBitmapWithPaletteApplied.Clone();
                tileBitmapGridWithPaletteApplied.DrawGrid();
                this.tilesGridPaletteApplied.Add(tile.Id, tileBitmapGridWithPaletteApplied);
            }

            this.PopulateListViews();

            // empty tile should always be 1st
            this.emptyTile = this.config.Tiles[0].Id;

            if (File.Exists(this.levelTextBox.Text))
            {
                // todo load level
            }
            else
            {
                this.width = DefaultWidth;
                this.scroll = 0;
                this.level = new string[this.width][];
                for (var i = 0; i < this.width; i++)
                {
                    this.level[i] = new string[Height];
                    for (var j = 0; j < Height; j++)
                    {
                        this.level[i][j] = this.emptyTile;
                    }
                }
            }

            this.scroll = 0;

            this.UpdateDrawPanel();
        }

        private Color GetBgColor()
        {
            try
            {
                var rgb = this.bgColorTextBox.Text.Split(',').Select(int.Parse).ToArray();
                return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
            }
            catch (Exception)
            {
                return Color.Black;
            }
        }

        private void PopulateListViews()
        {
            const int ListViewZoom = 2;
            var applyPalettes = this.applyPaletteCheckbox.Checked;

            Action<TileType, ListView> populateListView = (tileType, listView) =>
            {
                listView.Items.Clear();
                listView.LargeImageList = new ImageList { ImageSize = new Size(Constants.BackgroundTileWidth * ListViewZoom, Constants.BackgroundTileHeight * ListViewZoom) };
                var index = 0;
                foreach (var kvp in applyPalettes ? this.tilesPaletteApplied : tiles)
                {
                    if (this.config.Tiles.First(t => t.Id == kvp.Key).Type != tileType)
                    {
                        continue;
                    }

                    var scaledImage = kvp.Value.Scale(ListViewZoom);
                    var bitmap = scaledImage.ToBitmap();
                    listView.LargeImageList.Images.Add(bitmap);
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

        public void UpdateDrawPanel()
        {
            ////
            //// Pre-checks
            ////

            this.drawPanel.Visible = this.width > 0;
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

            var zoom = (int)this.zoomPicker.Value;
            var tileWidth = Constants.BackgroundTileWidth * zoom;
            var tileHeight = Constants.BackgroundTileHeight * zoom;

            var outerPanelWidth = this.outerDrawPanel.Width;
            var outerPanelWidthInTiles = outerPanelWidth / tileWidth;
            var panelWidthInTiles = (int)Math.Min(outerPanelWidthInTiles, this.width);
            var panelWidth = panelWidthInTiles * tileWidth;
            var horizontalPadding = (outerPanelWidth - panelWidth) / 2;            
        
            var outerPanelHeight = this.outerDrawPanel.Height;
            var panelHeightInTiles = Height;
            var panelHeight = panelHeightInTiles * tileHeight;
            var verticalPadding = (outerPanelHeight - panelHeight) / 2;

            this.drawPanel.Size = new Size(panelWidth, panelHeight);
            this.drawPanel.Location = new Point(horizontalPadding, verticalPadding);

            ////
            //// Scroll bar
            ////

            this.scrollBar.Enabled = panelWidthInTiles < this.width;
            this.scrollBar.Minimum = 0;
            this.scrollBar.Maximum = this.width - panelWidthInTiles;
            this.scroll = (int)Math.Min(this.scroll, this.scrollBar.Maximum);
            this.scrollBar.Value = this.scroll;

            ////
            //// Tiles
            ////

            var bitmap = new MyBitmap(panelWidth, panelHeight);
            for (var i = this.scroll; i < panelWidthInTiles + this.scroll; i++)
            {
                for (var j  = 0; j < Height; j++)
                {
                    var tile = this.level[i][j];
                    var dictionary = this.showGridCheckbox.Checked ?
                                         (this.applyPaletteCheckbox.Checked ? this.tilesGridPaletteApplied : this.tilesGrid) :
                                         (this.applyPaletteCheckbox.Checked ? this.tilesPaletteApplied : this.tiles);

                    var image = dictionary[tile];
                    bitmap.DrawImage(image.Scale(zoom), i * tileWidth, j * tileHeight);
                }
            }
            
            this.drawPanel.BackgroundImage = bitmap.ToBitmap();
        }

        private void DrawPanelMouseClick(object sender, MouseEventArgs e)
        {
            // todo: actual implementation
            this.level[3][3] = this.config.Tiles[50].Id;
            this.UpdateDrawPanel();
        }
    }
}
