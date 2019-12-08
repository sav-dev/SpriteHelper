using SpriteHelper.Contract;
using SpriteHelper.Files;
using SpriteHelper.NesGraphics;
using SpriteHelper.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class BackgroundTilesetCreator : Form
    {
        public BackgroundTilesetCreator()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            if (Defaults.Instance.ApplyDefaults)
            {
                this.PreLoad();
            }
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
            this.lvlFilesTextBox.Lines = Defaults.Instance.BgDefaultLevels;
            //this.Process();
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            this.Process();
        }

        private void Process()
        {
            // Validate all files are in the same directory.
            var allFiles = this.nonBlockingTextBox.Lines.Union(this.blockingTextBox.Lines).Union(this.threatTextBox.Lines).Where(f => f != "+" && f != "-");
            var directories = allFiles.Select(f => new FileInfo(f).Directory.ToString()).Distinct();
            if (directories.Count() > 1)
            {
                throw new Exception("All input files should be in the same directory");
            }

            // Validate no files are called the same as the intermediate files.
            var directory = directories.First();
            var nonBlockingFile = directory + "\\_nonblocking_gs.png";
            var blockingFile = directory + "\\_blocking_gs.png";
            var threatFile = directory + "\\_threat_gs.png";
            var nonBlockingFileOld = directory + "\\_nonblocking_gs_old.png";
            var blockingFileOld = directory + "\\_blocking_gs_old.png";
            var threatFileOld = directory + "\\_threat_gs_old.png";

            var allIntermediate = new[] { nonBlockingFile, blockingFile, threatFile, nonBlockingFileOld, blockingFileOld, threatFileOld };
            var intermediateInfos = allIntermediate.Select(f => new FileInfo(f).FullName);
            if (allFiles.Any(f => intermediateInfos.Contains(new FileInfo(f).FullName)))
            {
                throw new Exception("One input file called like the intermediate ouputs");
            }

            // Create all collections.
            var nonBlockingAll = new List<MyBitmap>();
            var blockingAll = new List<MyBitmap>();
            var threatAll = new List<MyBitmap>();
            var nonBlockingOld = new List<MyBitmap>();
            var blockingOld = new List<MyBitmap>();
            var threatOld = new List<MyBitmap>();

            Action<string[], List<MyBitmap>, List<MyBitmap>> createCollections = (lines, all, old) =>
            {
                var minusFound = false;
                var plusFound = false;
                foreach (var line in lines)
                {
                    if (line == "-")
                    {
                        minusFound = true;
                        continue;
                    }

                    if (minusFound)
                    {
                        if (line == "+")
                        {
                            throw new Exception("+ after -");
                        }

                        old.Add(MyBitmap.FromFileWithParams(line));
                    }
                    else
                    {
                        if (line == "+")
                        {
                            plusFound = true;
                            continue;
                        }

                        if (!plusFound)
                        {
                            old.Add(MyBitmap.FromFileWithParams(line));
                        }

                        all.Add(MyBitmap.FromFileWithParams(line));
                    }                    

                }
            };

            createCollections(this.nonBlockingTextBox.Lines, nonBlockingAll, nonBlockingOld);
            createCollections(this.blockingTextBox.Lines, blockingAll, blockingOld);
            createCollections(this.threatTextBox.Lines, threatAll, threatOld);

            // Create intermediate files.
            Action<IEnumerable<MyBitmap>, string> createIntermediate = (bitmaps, targetFile) =>
            {
                if (!bitmaps.Any())
                {
                    return;
                }

                foreach (var bitmap in bitmaps)
                {
                    bitmap.MakeNesGreyscale();
                }

                var result = CreateSingleBitmap(bitmaps);
                result.ToBitmap().Save(targetFile);
            };

            createIntermediate(nonBlockingAll, nonBlockingFile);
            createIntermediate(blockingAll, blockingFile);
            createIntermediate(threatAll, threatFile);
            createIntermediate(nonBlockingOld, nonBlockingFileOld);
            createIntermediate(blockingOld, blockingFileOld);
            createIntermediate(threatOld, threatFileOld);

            // Validate level files.
            var lvlFiles = this.lvlFilesTextBox.Lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            foreach (var lvl in lvlFiles)
            {
                if (!File.Exists(lvl))
                {
                    throw new Exception($"Lvl {lvl} doesn't exist");
                }
            }

            // Process everything.
            BackgroundConfig config;
            List<MyBitmap> sprites;
            Dictionary<string, MyBitmap> tilesWithImages;
            GetConfigAndSprites(nonBlockingFile, blockingFile, threatFile, true, out config, out sprites, out tilesWithImages);

            if (lvlFiles.Any())
            {
                Dictionary<string, MyBitmap> tilesWithImagesOld = null;
                BackgroundConfig ignore1;
                List<MyBitmap> ignore2;
                GetConfigAndSprites(nonBlockingFileOld, blockingFileOld, threatFileOld, false, out ignore1, out ignore2, out tilesWithImagesOld);                               
                ProcessLvlFiles(lvlFiles, tilesWithImages, tilesWithImagesOld);
            }

            GenerateChrImageAndSaveFiles(config, sprites);

            if (File.Exists(nonBlockingFileOld))
            {
                File.Delete(nonBlockingFileOld);
            }

            if (File.Exists(blockingFileOld))
            {
                File.Delete(blockingFileOld);
            }

            if (File.Exists(threatFileOld))
            {
                File.Delete(threatFileOld);
            }
        }

        private void GetConfigAndSprites(
            string nonBlockingFile, 
            string blockingFile, 
            string threatFile, 
            bool handleEmptyTile,
            out BackgroundConfig configRes, 
            out List<MyBitmap> spritesRes,
            out Dictionary<string, MyBitmap> tilesWithImagesRes)
        {
            // Create config with file names.
            var config = new BackgroundConfig
            {
                NonBlockingFile = nonBlockingFile,
                BlockingFile = blockingFile,
                ThreatFile = threatFile
            };

            // Get all files. Handle some not existing.
            var nonBlocking = File.Exists(nonBlockingFile) ? MyBitmap.FromFile(config.NonBlockingFile) : null;
            var blocking = File.Exists(blockingFile) ? MyBitmap.FromFile(config.BlockingFile) : null;
            var threat = File.Exists(threatFile) ? MyBitmap.FromFile(config.ThreatFile) : null;

            // Get all tiles and sprites
            var allTiles = new List<MyBitmap>();
            var tiles = new List<Tile>();
            var sprites = new List<MyBitmap>();
            var tilesWithImages = new Dictionary<string, MyBitmap>();

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
                            newTile.GetPart(0, Constants.SpriteHeight, Constants.SpriteWidth, Constants.SpriteHeight),
                            newTile.GetPart(Constants.SpriteWidth, 0, Constants.SpriteWidth, Constants.SpriteHeight),
                            newTile.GetPart(Constants.SpriteWidth, Constants.SpriteHeight, Constants.SpriteWidth, Constants.SpriteHeight),
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

                        tilesWithImages.Add(tileConfig.Id, newTile);
                    }
                }
            };

            // Process all tiles (non blocking first)
            if (nonBlocking != null)
            {
                processList(nonBlocking, TileType.NonBlocking);
            }

            if (blocking != null)
            {
                processList(blocking, TileType.Blocking);
            }

            if (threat != null)
            {
                processList(threat, TileType.Threat);
            }

            // Move empty tile to the 1st place
            if (handleEmptyTile)
            {
                if (emptyTile == null)
                {
                    throw new Exception("Empty tile not found");
                }

                tiles.Remove(emptyTile);
                tiles.Insert(0, emptyTile);
            }

            // Set tiles in the config
            config.Tiles = tiles.ToArray();

            // Set results.
            configRes = config;
            spritesRes = sprites;
            tilesWithImagesRes = tilesWithImages;
        }


        private void GenerateChrImageAndSaveFiles(BackgroundConfig config, List<MyBitmap> sprites)
        {
            // Find the empty tile, move it to the end, update the config
            int emptyTileIndex = -1;
            for (var i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].IsNesColor(this.bgColorComboBox.SelectedIndex))
                {
                    emptyTileIndex = i;
                }
            }

            if (emptyTileIndex == -1)
            {
                throw new Exception("Empty tile not found for some reason");
            }

            // Move empty tile to the end.
            var emptyTile = sprites[emptyTileIndex];
            sprites.RemoveAt(emptyTileIndex);
            var newEmptyTileIndex = sprites.Count;
            sprites.Add(emptyTile);

            // Update the config.
            foreach (var tile in config.Tiles)
            {
                var newSprites = new int[4];
                Array.Copy(tile.Sprites, newSprites, 4);
                for (var i = 0; i < 4; i++)
                {
                    if (newSprites[i] == emptyTileIndex)
                    {
                        newSprites[i] = newEmptyTileIndex;
                    }
                    else if (newSprites[i] > emptyTileIndex)
                    {
                        newSprites[i] = newSprites[i] - 1;
                    }
                }

                tile.Sprites = newSprites;
            }

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

            while (bytes.Count < 4096)
            {
                bytes.Add(0);
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

        private MyBitmap CreateSingleBitmap(IEnumerable<MyBitmap> bitmaps)
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

        private void ProcessLvlFiles(IEnumerable<string> files, Dictionary<string, MyBitmap> newMap, Dictionary<string, MyBitmap> oldMap)
        {
            // Create the mapping.
            var mapping = new Dictionary<string, string>();

            foreach (var tileOld in oldMap)
            {
                var tileNew = newMap.FirstOrDefault(t => t.Value.Equals(tileOld.Value));
                mapping.Add(tileOld.Key, tileNew.Key); // tileNew.Key will be null if not found
            }
            
            foreach (var file in files)
            {
                var level = Level.Read(file);
                for (var x = 0; x < level.Tiles.Length; x++)
                {
                    for (var y = 0; y < level.Tiles[x].Length; y++)
                    {
                        var id = TileIds.ParsePaletteId(level.Tiles[x][y]);
                        var newId = mapping[id.Item2];

                        if (newId == null)
                        {
                            throw new Exception($"Removed tile in level {file}");
                        }

                        level.Tiles[x][y] = TileIds.PaletteTileId(id.Item1, newId);
                    }
                }
            
                level.Write(file);
            }
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show(
@"When adding/removing new files, levels must be updated.
If new files are added, add a '+' line in the list and then list the new files.
If files are removed, add a '-' line in the list and the list the removed files.
A file can be updated by adding a new version after '+' and removing old one after '-'.
List all level files in the right text box.
If a tile is removed but referenced in a level file, exception will be thrown.
Please note - if anything goes wrong, the files will be left in an inconsistent state.
To fix it:
  1) Make a copy of all updated files
  2) Revert all graphics updates
  3) Revert all level updates");
        }
    }
}
