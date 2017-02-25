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
            this.nonBlockingTextBox.Text = Defaults.Instance.NonBlockingBackground;
            this.blockingTextBox.Text = Defaults.Instance.BlockingBackground;            
            this.threatTextBox.Text = Defaults.Instance.ThreatBackground;
            this.bgColorComboBox.SelectedIndex = Defaults.Instance.BgColor;
            this.outputImageTextBox.Text = Defaults.Instance.BackgroundChr;
            this.outputSpecTextBox.Text = Defaults.Instance.BackgroundSpec;
            this.Process();
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            this.Process();
        }

        private void Process()
        {
            // Create config with file names.
            var config = new BackgroundConfig
            {
                NonBlockingFile = this.nonBlockingTextBox.Text,
                BlockingFile = this.blockingTextBox.Text,
                ThreatFile = this.threatTextBox.Text
            };

            // Get all files
            var nonBlocking = MyBitmap.FromFile(config.NonBlockingFile);
            var blocking = MyBitmap.FromFile(config.BlockingFile);
            var threat = MyBitmap.FromFile(config.ThreatFile);
            
            // Get all tiles and sprites
            var allTiles = new List<MyBitmap>();
            var tiles = new List<Tile>();
            var sprites = new List<MyBitmap>();

            // Processing function
            string emptyTileId = null;
            Action<MyBitmap, TileType> processList = (bitmap, tileType) =>
            {                
                bitmap.MakeNesGreyscale();

                for (var x = 0; x < bitmap.Width; x += Constants.BackgroundTileWidth)
                {
                    for (var y = 0; y < bitmap.Height; y += Constants.BackgroundTileHeight)
                    {
                        var newTile = bitmap.GetPart(x, y, Constants.BackgroundTileWidth, Constants.BackgroundTileHeight);
                        var isEmptyTile = newTile.IsNesColor(this.bgColorComboBox.SelectedIndex);
                        if (allTiles.Any(tile => tile.Equals(newTile)))
                        {
                            if (!isEmptyTile)
                            {
                                throw new Exception(string.Format("Tile {0}/{1} in file {2} is repeated somewhere", x, y, bitmap.FileName));
                            }

                            continue;
                        }

                        if (isEmptyTile && tileType != TileType.NonBlocking)
                        {
                            throw new Exception("Empty tile found first in file other than non-blocking");
                        }

                        var tileConfig = new Tile
                        {
                            HeightSprites = Constants.BackgroundTileHeight / Constants.SpriteHeight,
                            WidthInSprites = Constants.BackgroundTileWidth / Constants.SpriteWidth,
                            Type = tileType,
                            X = x,
                            Y = y
                        };

                        if (isEmptyTile)
                        {
                            emptyTileId = tileConfig.Id;
                        }

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
            };

            // Process all tiles (non blocking first)
            processList(nonBlocking, TileType.NonBlocking);
            processList(blocking, TileType.Blocking);            
            processList(threat, TileType.Threat);

            // Move empty tile to the 1st place
            var emptyTile = tiles.First(t => t.Id == emptyTileId);
            tiles.Remove(emptyTile);
            tiles.Insert(0, emptyTile);

            // Set tiles in the config
            config.Tiles = tiles.ToArray();

            // Generate chrFile
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
            
            // Save chrFile and spec
            chrFile.ToBitmap().Save(outputImageTextBox.Text);            
            config.Write(outputSpecTextBox.Text);
        }
    }
}
