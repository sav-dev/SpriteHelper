using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            this.nonBlockingTextBox.Lines = Defaults.Instance.NonBlockingBackgrounds;
            this.blockingTextBox.Lines = Defaults.Instance.BlockingBackgrounds;
            this.threatTextBox.Lines = Defaults.Instance.ThreatBackgrounds;
            this.bgColorComboBox.SelectedIndex = Defaults.Instance.BgColor;
            this.outputImageTextBox.Text = Defaults.Instance.BackgroundImage;
            this.outputSpecTextBox.Text = Defaults.Instance.BackgroundSpec;
            this.outputChrTextBox.Text = Defaults.Instance.BackgroundChr;
            this.Process();
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            this.Process();
        }

        private void Process()
        {
            var allFiles = this.nonBlockingTextBox.Lines.Union(this.blockingTextBox.Lines).Union(this.threatTextBox.Lines);
            var directories = allFiles.Select(f => new FileInfo(f).Directory.ToString()).Distinct();
            if (directories.Count() > 1)
            {
                throw new Exception("All input files should be in the same directory");
            }

            var directory = directories.First();
            var nonBlockingFile = directory + "\\_nonblocking_gs.png";
            var blockingFile = directory + "\\_blocking_gs.png";
            var threatFile = directory + "\\_threat.png";

            if (allFiles.Any(f => new FileInfo(f).FullName == new FileInfo(nonBlockingFile).FullName ||
                                  new FileInfo(f).FullName == new FileInfo(blockingFile).FullName ||
                                  new FileInfo(f).FullName == new FileInfo(threatFile).FullName))
            {
                throw new Exception("One input file called like the intermediate ouputs");
            }

            var nonBlocking = this.nonBlockingTextBox.Lines.Select(l => MyBitmap.FromFileWithParams(l)).ToArray();
            var blocking = this.blockingTextBox.Lines.Select(l => MyBitmap.FromFileWithParams(l)).ToArray();
            var threat = this.threatTextBox.Lines.Select(l => MyBitmap.FromFileWithParams(l)).ToArray();

            Func<MyBitmap[], MyBitmap> process = bitmaps =>
            {
                foreach (var bitmap in bitmaps)
                {
                    bitmap.MakeNesGreyscale();
                }

                return CreateSingleBitmap(bitmaps);                
            };

            var nonBlockingBitmap = process(nonBlocking);
            var blockingBitmap = process(blocking);
            var threatBitmap = process(threat);
            
            nonBlockingBitmap.ToBitmap().Save(nonBlockingFile);
            blockingBitmap.ToBitmap().Save(blockingFile);
            threatBitmap.ToBitmap().Save(threatFile);
            
            Process(directory + "\\_nonblocking_gs.png", directory + "\\_blocking_gs.png", directory + "\\_threat.png");
        }

        private MyBitmap CreateSingleBitmap(MyBitmap[] bitmaps)
        {            
            var positions = Packer.Pack(bitmaps.Select(b => b.Size), Constants.PickerWidth);
            var bitmapsCopy = bitmaps.ToList();
            var backColor = MyBitmap.NesGreyscale[bgColorComboBox.SelectedIndex];
            var resultBitmap = new MyBitmap(1, 1, backColor);

            foreach (var tuple in positions)
            {
                var position = tuple.Item1;
                var size = tuple.Item2;

                var bitmap = bitmapsCopy.First(b => b.Size == size);
                bitmapsCopy.Remove(bitmap);

                resultBitmap.DrawImage(bitmap, position.X, position.Y, true, backColor);
            }

            return resultBitmap;
        }

        private void Process(string nonBlockingFile, string blockingFile, string threatFile)
        {
            // Create config with file names.
            var config = new BackgroundConfig
            {
                NonBlockingFile = nonBlockingFile,
                BlockingFile = blockingFile,
                ThreatFile = threatFile
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
            Tile emptyTile = null;
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
                            Type = tileType,
                            X = x / Constants.BackgroundTileWidth,
                            Y = y / Constants.BackgroundTileHeight
                        };
            
                        if (isEmptyTile)
                        {
                            emptyTile = tileConfig;
                        }
            
                        tileConfig.Sprites = new int[4];
            
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
            if (emptyTile == null)
            {
                throw new Exception("Empty tile not found");
            }

            tiles.Remove(emptyTile);
            tiles.Insert(0, emptyTile);
            
            // Set tiles in the config
            config.Tiles = tiles.ToArray();
            
            // Generate chr image
            var chrImage = new MyBitmap(Constants.SpriteWidth * Constants.ChrFileSpritesPerRow, Constants.SpriteHeight * Constants.ChrFileRows, Color.Black);
            {                
                var x = 0;
                var y = 0;
            
                foreach (var sprite in sprites)
                {
                    chrImage.DrawImage(sprite, x, y);
                    x += 8;
                    if (x >= Constants.SpriteWidth * Constants.ChrFileSpritesPerRow)
                    {
                        x = 0;
                        y += 8;
                    }
                }
            }

            // Generate actual chr file
            // CHR format:
            //  each sprite is 16 bytes:
            //  first 8 bytes are low bits per sprite row
            //  second 8 bytes are high bits per sprite row
            var bytes = new List<byte>();
            foreach (var sprite in sprites)
            {
                var lowBits = new List<byte>();
                var highBits = new List<byte>();

                for (var y = 0; y < Constants.SpriteHeight; y++)
                {
                    byte lowBit = 0;
                    byte highBit = 0;

                    for (var x = 0; x < Constants.SpriteWidth; x++)
                    {
                        lowBit = (byte)(lowBit << 1);
                        highBit = (byte)(highBit << 1);

                        var pixel = sprite.GetNesPixel(x, y);

                        if (pixel == 1 || pixel == 3)
                        {
                            // low bit set
                            lowBit |= 1;
                        }

                        if (pixel == 2 || pixel == 3)
                        {
                            // high bit set
                            highBit |= 1;
                        }
                    }

                    lowBits.Add(lowBit);
                    highBits.Add(highBit);
                }

                bytes.AddRange(lowBits);
                bytes.AddRange(highBits);
            }        

            // Save everything
            chrImage.ToBitmap().Save(outputImageTextBox.Text);

            config.Write(outputSpecTextBox.Text);

            if (File.Exists(outputChrTextBox.Text))
            {
                File.Delete(outputChrTextBox.Text);
            }

            File.WriteAllBytes(outputChrTextBox.Text, bytes.ToArray());
        }
    }
}
