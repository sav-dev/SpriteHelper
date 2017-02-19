using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper
{
    public partial class BackgroundTilesetCreator : Form
    {
        public BackgroundTilesetCreator()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            this.PreLoad();
        }

        private void PreLoad()
        {
            this.blockingTextBox.Lines = Defaults.Instance.BlockingBackgrounds;
            this.nonBlockingTextBox.Lines = Defaults.Instance.NonBlockingBackgrounds;
            this.threatsTextBox.Lines = Defaults.Instance.ThreatBackgrounds;
            this.bgColorTextBox.Text = Defaults.Instance.DefaultBgColor;
            this.outputImageTextBox.Text = Defaults.Instance.BackgroundChr;
            this.outputSpecTextBox.Text = Defaults.Instance.BackgroundSpec;
            this.Process();
        }

        private void BgColorTextBoxTextChanged(object sender, EventArgs e)
        {
            this.bgColorPanel.BackColor = this.GetBgColor();
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            this.Process();
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

        private void Process()
        {
            var config = new BackgroundConfig();

            //// PROCESSING 16x16 tilesets

            var bitmaps16 = this.blockingTextBox.Lines.Select(l => MyBitmap.FromFile(l)).ToArray();

            if (bitmaps16.Length > 4)
            {
                throw new Exception("Too many bitmaps");
            }

            var tiles16PerBitmap = new List<dynamic>();
            var allTiles = new List<MyBitmap>();        
            foreach (var bitmap in bitmaps16)
            {
                var tiles16 = new List<MyBitmap>();
                var uniqueColors = bitmap.UniqueColors();

                if (uniqueColors.Length > 4)
                {
                    throw new Exception("too many colors");
                }

                for (var x = 0; x < bitmap.Width; x += Constants.BackgroundTileWidth)
                { 
                    for (var y = 0; y < bitmap.Height; y += Constants.BackgroundTileHeight)
                    { 
                        var newTile = bitmap.GetPart(x, y, Constants.BackgroundTileWidth, Constants.BackgroundTileHeight);
                        if (!allTiles.Any(tile => tile.Equals(newTile)))
                        {
                            allTiles.Add(newTile);
                            tiles16.Add(newTile);
                        }
                    }
                }

                dynamic item = new ExpandoObject();
                item.Tiles = tiles16;
                item.Palette = uniqueColors;
                tiles16PerBitmap.Add(item);
            }

            var sprites = new List<MyBitmap>();
            var targetColors = new[] { NesPalette.Colors[15], NesPalette.Colors[0], NesPalette.Colors[16], NesPalette.Colors[32] }; // greyscale
            var tiles = new List<Tile>();
            config.PaletteMappings = new PaletteMapping[tiles16PerBitmap.Count];            
            for (var tilesetIndex = 0; tilesetIndex < tiles16PerBitmap.Count; tilesetIndex++)
            {
                var item = tiles16PerBitmap[tilesetIndex]; 
                var bgColor = this.GetBgColor();
                Color[] palette = item.Palette;
                if (palette.Length < 4)
                {
                    var newPalette = new Color[4];
                    for (var i = 0; i < palette.Length; i++)
                    {
                        newPalette[i] = palette[i];
                    }

                    for (var i = palette.Length; i < newPalette.Length; i++)
                    {
                        newPalette[i] = bgColor;
                    }

                    palette = newPalette;
                }

                if (!palette.Contains(bgColor))
                {
                    throw new Exception("Palette doesn't contain the bg color");
                }

                if (palette[0] != bgColor)
                {
                    var newPalette = new Color[4] { bgColor, bgColor, bgColor, bgColor };
                    var i = 1;
                    foreach (var color in palette)
                    {
                        if (color != bgColor)
                        {
                            newPalette[i++] = color;
                        }
                    }

                    palette = newPalette;
                }

                config.PaletteMappings[tilesetIndex] = new PaletteMapping
                {
                    Id = tilesetIndex,
                    ToPalette = tilesetIndex,
                    ColorMappings = palette.Select((c, i) => new ColorMapping { R = c.R, B = c.B, G = c.G, To = i }).ToArray()
                };
                
                foreach (var tile in item.Tiles)
                {
                    var tileConfig = new Tile { WidthInSprites = 2, HeightSprites = 2, PaletteMapping = tilesetIndex, Sprites = new int[4] };
                    var spritesInTile = new MyBitmap[] { tile.GetPart(0, 0, 8, 8), tile.GetPart(8, 0, 8, 8), tile.GetPart(0, 8, 8, 8), tile.GetPart(8, 8, 8, 8) };
                    for (var i = 0; i < spritesInTile.Length; i++)
                    {
                        var sprite = spritesInTile[i];
                        sprite.UpdateColors(palette.ToList(), targetColors.ToList());

                        var indexOfSprite = -1;
                        var spriteFromList = sprites.FirstOrDefault(s => s.Equals(sprite));

                        if (spriteFromList != null)
                        {
                            indexOfSprite = sprites.IndexOf(spriteFromList);
                        }
                        else
                        {
                            sprites.Add(sprite); // sprite added
                            indexOfSprite = sprites.Count - 1;
                        }

                        tileConfig.Sprites[i] = indexOfSprite;
                    }

                    tiles.Add(tileConfig);
                }
            }

            config.Tiles = tiles.ToArray();

            var maxTileIndex = config.Tiles.Max(t => t.Sprites.Max());

            //// PROCESSING 16x16 tilesets DONE

            maxTileIndex = config.Tiles.Max(t => t.Sprites.Max());

            var chrFile = new MyBitmap(Constants.SpriteWidth * Constants.ChrFileSpritesPerRow, Constants.SpriteHeight * Constants.ChrFileRows, Color.Black);
            
            {                
                var x = 0;
                var y = 0;
            
                foreach (var sprite in sprites)
                {
                    chrFile.DrawImage(sprite, x, y);
                    x += 8;
                    if (x >= Constants.SpriteWidth * Constants.ChrFileSpritesPerRow)
                    {
                        x = 0;
                        y += 8;
                    }
                }
            }
            
            chrFile.ToBitmap().Save(outputImageTextBox.Text);

            config.Write(outputSpecTextBox.Text);
        }
    }
}
