using System;
using System.Collections.Generic;
using System.Drawing;
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

            // Get all files
            var blocking = this.blockingTextBox.Lines.Select(l => MyBitmap.FromFile(l)).ToArray();
            var nonBlocking = this.nonBlockingTextBox.Lines.Select(l => MyBitmap.FromFile(l)).ToArray();
            var threat = this.threatsTextBox.Lines.Select(l => MyBitmap.FromFile(l)).ToArray();

            // Figure out palettes
            var palettes = GetPalettes(blocking.Union(nonBlocking).Union(threat));
            config.PaletteMappings = palettes.Select(
                (p, i) => new PaletteMapping { Id = i, ToPalette = i, ColorMappings = p.Select((c, j) => new ColorMapping { R = c.R, G = c.G, B = c.B, To = j }).ToArray() }).ToArray();

            // Get all tiles and sprites
            var allTiles = new List<MyBitmap>();
            var tiles = new List<Tile>();
            var sprites = new List<MyBitmap>();

            // Processing function
            Action<MyBitmap[], TileType> processList = (bitmaps, tileType) =>
            {
                foreach (var bitmap in bitmaps)
                {
                    // Get target palette id.
                    var uniqueColors = bitmap.UniqueColors();
                    var palette = palettes.First(p => uniqueColors.All(c => p.Contains(c)));
                    var paletteId = palettes.IndexOf(palette);

                    for (var x = 0; x < bitmap.Width; x += Constants.BackgroundTileWidth)
                    {
                        for (var y = 0; y < bitmap.Height; y += Constants.BackgroundTileHeight)
                        {
                            var newTile = bitmap.GetPart(x, y, Constants.BackgroundTileWidth, Constants.BackgroundTileHeight);
                            if (!allTiles.Any(tile => tile.Equals(newTile)))
                            {
                                var tileConfig = new Tile
                                {
                                    FileName = bitmap.FileName,
                                    HeightSprites = Constants.BackgroundTileHeight / Constants.SpriteHeight,
                                    WidthInSprites = Constants.BackgroundTileWidth / Constants.SpriteWidth,
                                    PaletteMapping = paletteId,                                    
                                    Type = tileType,
                                    X = x,
                                    Y = y
                                };

                                tileConfig.Sprites = new int[tileConfig.WidthInSprites * tileConfig.HeightSprites];

                                var spritesInTile = new MyBitmap[] 
                                {
                                    newTile.GetPart(0, 0, Constants.SpriteWidth, Constants.SpriteHeight),
                                    newTile.GetPart(Constants.SpriteWidth, 0, Constants.SpriteWidth, Constants.SpriteHeight),
                                    newTile.GetPart(0, Constants.SpriteHeight, Constants.SpriteWidth, Constants.SpriteHeight),
                                    newTile.GetPart(Constants.SpriteWidth, Constants.SpriteHeight, Constants.SpriteWidth, Constants.SpriteHeight)
                                };

                                for (var i = 0; i < spritesInTile.Length; i++)
                                {
                                    var sprite = spritesInTile[i];
                                    sprite.UpdateToGreyscale(palette);

                                    var indexOfSprite = -1;
                                    var spriteFromList = sprites.FirstOrDefault(s => s.Equals(sprite));
                                    
                                    if (spriteFromList != null)
                                    {
                                        indexOfSprite = sprites.IndexOf(spriteFromList);
                                    }
                                    else
                                    {
                                        sprites.Add(sprite);
                                        indexOfSprite = sprites.Count - 1;
                                    }
                                    
                                    tileConfig.Sprites[i] = indexOfSprite;
                                }

                                tiles.Add(tileConfig);
                                allTiles.Add(newTile);
                            }
                        }
                    }
                }
            };

            // Process all tiles
            processList(blocking, TileType.Blocking);
            processList(nonBlocking, TileType.NonBlocking);
            processList(threat, TileType.Threat);
            config.Tiles = tiles.ToArray();
            
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

        private List<Color[]> GetPalettes(IEnumerable<MyBitmap> bitmaps)
        {
            var bgColor = this.GetBgColor();
            var palettes = new List<Color[]>();
            foreach (var bitmap in bitmaps)
            {
                var uniqueColors = bitmap.UniqueColors();
                if (uniqueColors.Length > 4)
                {
                    throw new Exception("Too many colors!");
                }

                // Looks for equals or supersets
                if (palettes.Any(p => uniqueColors.All(c => p.Contains(c))))
                {
                    // Already in the list.
                    continue;
                }

                // Look for subsets
                var subset = palettes.FirstOrDefault(p => p.Length < uniqueColors.Length && p.All(c => uniqueColors.Contains(c)));
                if (subset != null)
                {
                    palettes.Remove(subset);
                }

                // Add new palette.
                if (palettes.Count == 4)
                {
                    throw new Exception("Too many palettes!");
                }

                palettes.Add(uniqueColors);
            }

            // Normalize palettes - must start with bg color, then all colors, padded with more bg color
            var newPalettes = new List<Color[]>();
            foreach (var palette in palettes)
            {
                if (palette.Length == 4 && !palette.Contains(bgColor))
                {
                    throw new Exception("Invalid palette, doesn't contain the bg color");
                }

                var newPalette = new List<Color>();
                newPalette.Add(bgColor);
                foreach (var color in palette)
                {
                    if (color == bgColor)
                    {
                        continue;
                    }

                    newPalette.Add(color);
                }

                while (newPalette.Count < 4)
                {
                    newPalette.Add(bgColor);
                }

                newPalettes.Add(newPalette.ToArray());
            }

            while (newPalettes.Count < 4)
            {
                newPalettes.Add(new Color[] { bgColor, bgColor, bgColor, bgColor });
            }

            return newPalettes;
        }
    }
}
