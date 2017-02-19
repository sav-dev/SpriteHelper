﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteHelper
{
    public partial class LevelEditor : Form
    {
        private Palettes palettes;
        private BackgroundConfig config;
        private Dictionary<string, MyBitmap> tiles;
        private Dictionary<string, MyBitmap> tilesPaletteApplied;

        public LevelEditor()
        {
            InitializeComponent();
            UpdateToolBar(string.Empty);
        }

        private void LevelEditorLoad(object sender, EventArgs e)
        {
            this.PreLoad();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            this.SaveLevel();
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            this.LoadLevel();
        }

        private void BgColorTextBoxTextChanged(object sender, EventArgs e)
        {
            this.bgColorPanel.BackColor = this.GetBgColor();
        }

        private void ApplyPaletteCheckboxCheckedChanged(object sender, EventArgs e)
        {
            // todo
        }

        private void ZoomPickerValueChanged(object sender, EventArgs e)
        {
            // todo
        }

        private void SaveLevel()
        {
            // todo
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
            if (File.Exists(this.levelTextBox.Text))
            {
                // todo load level
            }

            this.palettes = Palettes.Read(this.palettesTextBox.Text);
            this.config = BackgroundConfig.Read(this.specTextBox.Text);

            var bitmaps = new MyBitmap[this.config.BackgroundFiles.Max(f => f.Id) + 1];
            foreach (var file in this.config.BackgroundFiles)
            {
                bitmaps[file.Id] = MyBitmap.FromFile(file.FileName);
            }

            this.tiles = new Dictionary<string, MyBitmap>();
            this.tilesPaletteApplied = new Dictionary<string, MyBitmap>();

            foreach (var tile in this.config.Tiles)
            {
                var bitmap = bitmaps[tile.BackgroundFileId];
                var tileBitmap = bitmap.GetPart(tile.X, tile.Y, tile.WidthInSprites * Constants.SpriteWidth, tile.HeightSprites * Constants.SpriteHeight);
                this.tiles.Add(tile.Id, tileBitmap);


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
            }            
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
    }
}
