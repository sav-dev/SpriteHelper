using SpriteHelper.Contract;
using SpriteHelper.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class LevelEditor : Form
    {
        // Configs.
        private Palettes palettes;
        private BackgroundConfig config;

        // Palette tabs.
        private Dictionary<TileType, TabPage> tileTabs;

        // Bitmaps.
        private Dictionary<TileType, Dictionary<int, MyBitmap>> bitmaps;

        // Cached tiles. Key is tile ID + palette.
        private Dictionary<string, Bitmap[]> tiles;
        private string selectedTile;

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

            this.tileTabs = new Dictionary<TileType, TabPage>();
            this.tileTabs.Add(TileType.Blocking, blockingTilesTabPage);
            this.tileTabs.Add(TileType.NonBlocking, nonBlockingTilesTabPage);
            this.tileTabs.Add(TileType.Threat, threatTilesTabPage);

        }

        private void LevelEditorLoad(object sender, EventArgs e)
        {
            if (Defaults.Instance.ApplyDefaults)
            {
                this.PreLoad();
            }

            this.splitContainerVertical.Panel2.Focus();
        }

        private void LevelEditorResize(object sender, EventArgs e)
        {
            this.UpdateDrawPanel();
        }

        private void PreLoad()
        {
            this.LoadLevel(Defaults.Instance.DefaultLevel, Defaults.Instance.BackgroundSpec, Defaults.Instance.PalettesSpec);
        }

        private void UpdateStatus(string text)
        {
            this.toolStripStatusLabel.Text = text;
        }

        private bool UpdateStatus(string format, params object[] args)
        {
            var newStatus = string.Format(format, args);
            if (this.toolStripStatusLabel.Text == newStatus)
            {
                return false;
            }

            this.toolStripStatusLabel.Text = newStatus;
            return true;
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

            this.bitmaps = new Dictionary<TileType, Dictionary<int, MyBitmap>>();

            foreach (var kvp in new Dictionary<TileType, MyBitmap>
                                    {
                                        { TileType.NonBlocking, MyBitmap.FromFile(config.NonBlockingFile) },
                                        { TileType.Blocking, MyBitmap.FromFile(config.BlockingFile) },
                                        { TileType.Threat, MyBitmap.FromFile(config.ThreatFile) },
                                    })
            {
                var type = kvp.Key;
                var bitmap = kvp.Value;
                var dictionary = new Dictionary<int, MyBitmap>();

                for (var i = 0; i < 4; i++)
                {
                    var clone = bitmap.Clone();
                    clone.UpdateColors(clone.UniqueColors(), this.palettes.BackgroundPalette[i].ActualColors);
                    dictionary.Add(i, clone);
                }

                this.bitmaps.Add(type, dictionary);
            }

            this.tiles = new Dictionary<string, Bitmap[]>();
            foreach (var tile in this.config.Tiles)
            {
                for (var palette = 0; palette < 4; palette++)
                {
                    var tileBitmaps = new Bitmap[(int)TileVersion.All + 1];
                    var tileBitmap = bitmaps[tile.Type][palette].GetPart(tile.X * Constants.BackgroundTileWidth, tile.Y * Constants.BackgroundTileHeight, Constants.BackgroundTileWidth, Constants.BackgroundTileHeight).Scale(Zoom);

                    // None
                    tileBitmaps[(int)TileVersion.None] = tileBitmap.ToBitmap();

                    // Grid
                    var tileBitmapGrid = tileBitmap.Clone();
                    tileBitmapGrid.DrawGrid();
                    tileBitmaps[(int)TileVersion.Grid] = tileBitmapGrid.ToBitmap();

                    // Type
                    foreach (var tileVersion in new[] { TileVersion.None, TileVersion.Grid })
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

                    this.tiles.Add(TileIds.PaletteTileId(palette, tile.Id), tileBitmaps);
                }
            }

            // Empty tile should always be the 1st one
            this.emptyTile = TileIds.PaletteTileId(0, this.config.Tiles[0].Id);

            string[][] newLevel;
            if (File.Exists(level))
            {
                newLevel = Level.Read(level).Tiles;
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

            this.PopulateTiles();
            this.SetLevel(newLevel);
            this.ClearHistory();
        }

        #endregion

        #region ListView

        ////
        //// ListView related stuff
        ////

        private void PopulateTiles()
        {
            this.SetSelectedTile(this.emptyTile);

            foreach (var type in new[] { TileType.Blocking, TileType.NonBlocking, TileType.Threat })
            {
                var tab = this.tileTabs[type];
                tab.Controls.Clear();

                var paletteTabControl = new TabControl { Alignment = TabAlignment.Bottom, Dock = DockStyle.Fill };

                for (var palette = 0; palette < 4; palette++)
                {
                    var tileSelector = new TileSelector(this.bitmaps[type][palette], type, palette, id => this.SetSelectedTile(id));

                    var tableLayoutPanel = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = this.palettes.BackgroundPalette[0].ActualColors[0] };
                    tableLayoutPanel.ColumnCount = 1;
                    tableLayoutPanel.RowCount = 1;
                    tableLayoutPanel.Controls.Add(tileSelector, 0, 0);

                    var tabPage = new TabPage { Text = string.Format("Palette {0}", palette) };
                    tabPage.Controls.Add(tableLayoutPanel);
                    paletteTabControl.Controls.Add(tabPage);
                }

                tab.Controls.Add(paletteTabControl);
            }
        }

        private void SetSelectedTile(string id)
        {
            if (tiles.Keys.Any(k => k == id))
            {
                this.selectedTile = id;
            }
            else
            {
                this.selectedTile = emptyTile;
            }

            this.selectedTilePictureBox.Image = this.tiles[this.selectedTile][(int)TileVersion.None];
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
            if (width == this.level.Length)
            {
                return;
            }
            
            if (width < 16 | width > 252)
            {
                MessageBox.Show("Wrongh width: min is 16, max is 252", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!(width % 4 == 0))
            {
                MessageBox.Show("Width must be a multiple of 4", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.AddHistory();

            var newLevel = new string[width][];
            for (var x = 0; x < newLevel.Length; x++)
            {
                newLevel[x] = new string[HeightInTiles];
                for (var y = 0; y < HeightInTiles; y++)
                {
                    newLevel[x][y] = x < this.level.Length ? this.level[x][y] : emptyTile;
                }
            }

            this.SetLevel(newLevel);
        }

        private void SetLevel(string[][] newLevel)
        {
            this.level = newLevel;
            this.UpdateTileCount();
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

        private void ExportImageToolStripMenuItemClick(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { InitialDirectory = Defaults.Instance.GraphicsDefaultDir, Filter = "png files (*.png)|*.png" };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                this.bitmap.Save(saveFileDialog.FileName);
            }
        }

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
            this.PopulateTiles();
            this.UpdateBitmap();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            var loadLevelDialog = new LoadLevelDialog(true);
            loadLevelDialog.FormClosed += (notUsed1, notUsed2) =>
            {
                if (loadLevelDialog.ClickedOk)
                {
                    this.LoadLevel(loadLevelDialog.Level, loadLevelDialog.Spec, loadLevelDialog.Palettes);
                }
            };

            loadLevelDialog.ShowDialog();
        }

        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            var loadLevelDialog = new LoadLevelDialog(false);
            loadLevelDialog.FormClosed += (notUsed1, notUsed2) =>
            {
                if (loadLevelDialog.ClickedOk)
                {
                    this.LoadLevel(null, loadLevelDialog.Spec, loadLevelDialog.Palettes);
                }
            };

            loadLevelDialog.ShowDialog();
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { InitialDirectory = Defaults.Instance.GraphicsDefaultDir, Filter = "xml files (*.xml)|*.xml" };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                var level = new Level { Tiles = this.level };
                level.Write(saveFileDialog.FileName);
            }
        }

        private void ExportLevelToolStripMenuItemClick(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { InitialDirectory = Defaults.Instance.LevelsDefaultDir, Filter = "binary files (*.bin)|*.bin" };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                this.Export(saveFileDialog.FileName);
            }
        }        

        private void TransformToolStripMenuItemClick(object sender, EventArgs e)
        {
            var transformDialog = new TransformDialog(this.selectedTile, this.selectedTilePictureBox.Image);
            transformDialog.FormClosed += (notUsed1, notUsed2) =>
            {
                switch (transformDialog.Result.Operation)
                {
                    case TransformDialog.TransformDialogResult.Fill:
                        this.Fill((TransformDialog.FillOperation)transformDialog.Result);
                        break;
                    case TransformDialog.TransformDialogResult.Clone:
                        this.Clone((TransformDialog.CloneOperation)transformDialog.Result);
                        break;
                }
            };

            transformDialog.ShowDialog();
        }

        private void Fill(TransformDialog.FillOperation result)
        {
            var changed = false;
            var newLevel = this.CloneLevel();
            for (var x = result.X; x < (this.level.Length) && x < result.X + result.Width; x++)
            {
                for (var y = result.Y; y < (this.level[0].Length) && y < result.Y + result.Height; y++)
                {
                    if (!changed && newLevel[x][y] != selectedTile)
                    {
                        changed = true;
                    }

                    newLevel[x][y] = result.SelectedTile;
                }
            }

            if (changed)
            {
                this.AddHistory();
                this.SetLevel(newLevel);
            }
        }

        private void Clone(TransformDialog.CloneOperation result)
        {
            var changed = false;
            var newLevel = this.CloneLevel();

            var dx = 0;
            for (var x = result.X; x < (this.level.Length) && x < result.X + result.Width; x++, dx++)
            {
                var newX = result.NewX + dx;
                if (newX >= this.level.Length)
                {
                    break;
                }

                var dy = 0;
                for (var y = result.Y; y < (this.level[0].Length) && y < result.Y + result.Height; y++, dy++)
                {
                    var newY = result.NewY + dy;
                    if (newY >= this.level[0].Length)
                    {
                        break;
                    }

                    var newTile = newLevel[x][y];

                    if (!changed && newLevel[newX][newY] != newTile)
                    {
                        changed = true;
                    }

                    newLevel[newX][newY] = newTile;
                }
            }

            if (changed)
            {
                this.AddHistory();
                this.SetLevel(newLevel);
            }
        }

        private void PropertiesToolStripMenuItemClick(object sender, EventArgs e)
        {
            var editLevelDialog = new EditLevelDialog(this.level.Length);
            editLevelDialog.FormClosed += (notUsed1, notUsed2) =>
            {
                switch (editLevelDialog.Result)
                {
                    case EditLevelDialogResult.WidthChange:
                        this.ChangeWidth(editLevelDialog.LevelWidth);
                        break;
                }
            };

            editLevelDialog.ShowDialog();
        }

        private void UndoToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Undo();
        }

        private void RedoToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Redo();
        }

        private void ViewPlatformsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var dialog = new LevelSplitView(this.GetSplittingInput(TileType.Blocking));
            dialog.ShowDialog();
        }

        private void ViewThreatsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var dialog = new LevelSplitView(this.GetSplittingInput(TileType.Threat));
            dialog.ShowDialog();
        }

        private void ExportCheckToolStripMenuItemClick(object sender, EventArgs e)
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            var bytes = GetExportPayload(writer);            
            ///writer.WriteLineIfNotNull();
            writer.WriteLineIfNotNull("Total size: {0} bytes", bytes.Length);
            MessageBox.Show(builder.ToString(), "Export data", MessageBoxButtons.OK);
        }

        #endregion

        #region MenuGetters

        ////
        //// Menu getters
        ////

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
            this.drawPanel.BackgroundImage = this.bitmap;
            this.drawPanel.Refresh();
        }

        private void DrawPanelMouseDown(object sender, MouseEventArgs e)
        {
            var x = e.X / TileWidth;
            var y = e.Y / TileWidth;
            if (this.SetTile(x, y, e.Button))
            {
                this.DrawPanelDrawCursor(x, y);
            }
        }

        private void DrawPanelMouseMove(object sender, MouseEventArgs e)
        {
            var x = e.X / TileWidth;
            var y = e.Y / TileWidth;
            if (this.UpdateStatus("{0} / {1}", x, y))
            {
                this.SetTile(x, y, e.Button);
                this.DrawPanelDrawCursor(x, y);
            }
        }

        private void DrawPanelDrawCursor(int x, int y)
        {
            var bitmapClone = new Bitmap(this.bitmap);
            using (var graphics = Graphics.FromImage(bitmapClone))
            {
                graphics.DrawRectangle(new Pen(MyBitmap.GridColor, 2), x * TileWidth + 1, y * TileHeight + 1, TileWidth - 2, TileHeight - 2);
            }

            drawPanel.BackgroundImage = bitmapClone;
            drawPanel.Refresh();
        }

        public bool SetTile(int x, int y, MouseButtons buttons)
        {
            if (x >= this.level.Length || y >= this.level[0].Length || x < 0 || y < 0)
            {
                return false;
            }

            string tile;
            if (buttons.HasFlag(MouseButtons.Left))
            {
                tile = this.selectedTile;
                if (tile == null)
                {
                    return false;
                }
            }
            else if (buttons.HasFlag(MouseButtons.Right))
            {
                tile = emptyTile;
            }
            else
            {
                return false;
            }

            if (this.level[x][y] == tile)
            {
                return false;
            }

            this.AddHistory();
            this.level[x][y] = tile;
            var image = this.tiles[tile][(int)this.Settings];
            this.graphics.DrawImage(image, new Point(x * TileWidth, y * TileHeight));
            this.UpdateTileCount();
            return true;
        }

        private List<string> UniqueTiles()
        {
            return this.level.SelectMany(l => l).Distinct().ToList();
        }

        private int UniqueTilesCount()
        {
            return this.UniqueTiles().Count();
        }

        private void UpdateTileCount()
        {
            var tileCount = this.UniqueTilesCount();
            this.uniqueTilesCountLabel.Text = tileCount.ToString();
            this.uniqueTilesCountLabel.ForeColor = tileCount <= Constants.MaxUniqueTiles ? Color.Black : Color.Red;
        }

        #endregion

        #region History

        ////
        //// History stuff
        ////

        public void ClearHistory()
        {
            this.history.Clear();
            this.future.Clear();
            this.undoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Enabled = false;
        }

        public string[][] CloneLevel()
        {
            var newLevel = new string[this.level.Length][];
            for (var x = 0; x < this.level.Length; x++)
            {
                newLevel[x] = new string[this.level[x].Length];
                for (var y = 0; y < this.level[x].Length; y++)
                {
                    newLevel[x][y] = this.level[x][y];
                }
            }

            return newLevel;
        }        

        public void AddHistory()
        {
            this.history.Push(this.CloneLevel());
            this.undoToolStripMenuItem.Enabled = true;
            this.future.Clear();
            this.redoToolStripMenuItem.Enabled = false;
        }

        public void Undo()
        {
            if (this.history.Count == 0)
            {
                return;
            }

            this.future.Push(this.CloneLevel());
            this.redoToolStripMenuItem.Enabled = true;
            this.SetLevel(this.history.Pop());

            if (this.history.Count == 0)
            {
                this.undoToolStripMenuItem.Enabled = false;
            }
        }

        public void Redo()
        {
            if (this.future.Count == 0)
            {
                return;
            }

            this.history.Push(this.CloneLevel());
            this.undoToolStripMenuItem.Enabled = true;
            this.SetLevel(this.future.Pop());

            if (this.future.Count == 0)
            {
                this.redoToolStripMenuItem.Enabled = false;
            }
        }

        #endregion

        #region Exporting

        private void Export(string fileName)
        {
            var bytes = GetExportPayload();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllBytes(fileName, bytes);
        }

        private byte[] GetExportPayload(TextWriter logger = null)
        {
            // Result byte list.
            var result = new List<byte>();

            // Tiles data.
            result.AddRange(GetExportTilesData(logger));
            logger.WriteLineIfNotNull();

            // Platform & threat data.
            result.AddRange(GetPlatformAndThreatData(logger));
            logger.WriteLineIfNotNull();

            // todo: add information about starting position, enemies etc

            return result.ToArray();
        }

        private byte[] GetExportTilesData(TextWriter logger = null)
        {
            //
            // - number of unique tiles (1 byte)
            // - sprites for each tile in the left column (2 bytes each)
            // - sprites for each tile in the right column (2 bytes each)
            // - number of columns (1 byte)
            // - column of 0s
            // - tiles in each column (15 bytes each)
            // - column of 0s
            // - atts column of 0s
            // - attributes (# of columns x 4 bytes)
            // - atts column of 0s
            //

            // Result byte list.
            var result = new List<byte>();

            // Local ids dictionary.            
            var localIds = new Dictionary<string, byte>();
            byte id = 0;

            // Number of unique tiles.
            var uniqueTilesCount = (byte)this.UniqueTilesCount();
            result.Add(uniqueTilesCount);
            logger.WriteLineIfNotNull("Unique tiles ({0}): 1 byte", uniqueTilesCount);
            logger.WriteLineIfNotNull("Tiles spec: {0} bytes", uniqueTilesCount * 4);

            // Sprites in the right column.
            var spritesInRightColumn = new List<byte>();

            // Sprites for each tile in the left column.
            foreach (var tileId in this.UniqueTiles())
            {
                // Assign a one byte id to the tile.
                localIds.Add(tileId, id++);

                // Find the tile config, get sprites, append to the result (sprites 0 and 1) and to the second list (2 and 3)
                var tileConfig = this.config.Tiles.First(t => t.Id == TileIds.ParsePaletteId(tileId).Item2);
                result.Add((byte)tileConfig.Sprites[0]);
                result.Add((byte)tileConfig.Sprites[1]);
                spritesInRightColumn.Add((byte)tileConfig.Sprites[2]);
                spritesInRightColumn.Add((byte)tileConfig.Sprites[3]);
            }

            // Add sprites in the right column to the result.
            result.AddRange(spritesInRightColumn);

            // Number of columns.
            var columnsCount = (byte)this.level.Length;
            result.Add(columnsCount);
            logger.WriteLineIfNotNull("Number of columns ({0}) - 1 byte", columnsCount);
            var countBefore = result.Count;

            // Column of 0s
            for (var i = 0; i < Constants.ScreenHeightInTiles; i++)
            {
                result.Add(0);
            }

            // Tiles in each column.
            for (var x = 0; x < this.level.Length; x++)
            {
                for (var y = 0; y < this.level[x].Length; y++)
                {
                    result.Add(localIds[this.level[x][y]]);
                }
            }

            // Column of 0s
            for (var i = 0; i < Constants.ScreenHeightInTiles; i++)
            {
                result.Add(0);
            }

            logger.WriteLineIfNotNull("Columns data - {0} bytes", result.Count - countBefore);
            countBefore = result.Count;

            // Atts column of 0s
            for (var i = 0; i < Constants.ScreenHeightInAtts; i++)
            {
                result.Add(0);
            }

            // Attributes
            for (var x = 0; x < this.level.Length; x += 2)
            {
                for (var y = 0; y < this.level[x].Length; y += 2)
                {
                    // Atts:
                    //  0 1
                    //  2 3
                    //
                    // So if atts are assigned as the numbers, the value will be:
                    //  11 10 01 00

                    byte palette0, palette1, palette2, palette3;

                    palette0 = (byte)TileIds.ParsePaletteId(this.level[x][y]).Item1;
                    palette1 = (byte)TileIds.ParsePaletteId(this.level[x + 1][y]).Item1;

                    if (y + 1 < this.level[x].Length)
                    {
                        palette2 = (byte)TileIds.ParsePaletteId(this.level[x][y + 1]).Item1;
                        palette3 = (byte)TileIds.ParsePaletteId(this.level[x + 1][y + 1]).Item1;
                    }
                    else
                    {
                        palette2 = 0;
                        palette3 = 0;
                    }

                    var atts = (byte)(palette3 << 6 | palette2 << 4 | palette1 << 2 | palette0);
                    result.Add(atts);
                }
            }

            // Atts column of 0s
            for (var i = 0; i < Constants.ScreenHeightInAtts; i++)
            {
                result.Add(0);
            }

            logger.WriteLineIfNotNull("Attributes data - {0} bytes", result.Count - countBefore);            
            return result.ToArray();
        }

        private byte[] GetPlatformAndThreatData(TextWriter logger = null)
        {
            var result = new List<byte>();
            result.AddRange(GetPlatformOrThreatData(TileType.Blocking, logger));
            result.AddRange(GetPlatformOrThreatData(TileType.Threat, logger));
            return result.ToArray();
        }

        private byte[] GetPlatformOrThreatData(TileType tileType, TextWriter logger = null)
        {
            //
            // - platforms in the following format:
            //   - pointer to next screen (from here): (n x 4) + 3 (1 byte)
            //   - number of platforms (1 byte)
            //   - n times platform data (x1, y1, x2, y2) (n x 4 bytes)
            //     both checks should be greater/less or equal - e.g. values will be x1 = 0, x2 = 15
            //   - pointer to the previous screen (from here): (n x 4) + 2 (1 byte)
            // - threats in the same format
            //

            var result = new List<byte>();
            var splitted = SplitIntoRectangles(GetSplittingInput(tileType));

            foreach (var kvp in splitted)
            {
                var screen = kvp.Key;
                var rectangles = kvp.Value;
                var n = (byte)rectangles.Length;
                
                // Pointer to the next screen
                result.Add((byte)(n * 4 + 3));

                // Number of objects
                result.Add(n);

                // Object data - order by x1
                foreach (var rectangle in rectangles.OrderBy(r => r.Item1.X))
                {
                    var x1 = rectangle.Item1.X * Constants.BackgroundTileWidth;
                    var y1 = rectangle.Item1.Y * Constants.BackgroundTileHeight;
                    var x2 = rectangle.Item2.X * Constants.BackgroundTileWidth + (Constants.BackgroundTileWidth - 1);
                    var y2 = rectangle.Item2.Y * Constants.BackgroundTileHeight + (Constants.BackgroundTileHeight - 1);

                    // Add offsets to threats so they are less deadly
                    if (tileType == TileType.Threat)
                    {
                        x1 += Constants.ThreatXOff;
                        x2 -= Constants.ThreatXOff;
                        y1 += Constants.ThreatYOff;
                        y2 -= Constants.ThreatYOff;
                    }

                    result.Add((byte)x1);
                    result.Add((byte)y1);
                    result.Add((byte)x2);
                    result.Add((byte)y2);
                }

                // Pointer to the previous screen
                result.Add((byte)(n * 4 + 2));
            }

            logger.WriteLineIfNotNull("Total bytes for {0} data: {1}", tileType, result.Count);
            return result.ToArray();
        }

        public bool[][] GetSplittingInput(TileType tileType)
        {
            var width = this.level.Length;
            var height = this.level[0].Length;
            var parsed = new bool[width][];
            for (var x = 0; x < width; x++)
            {
                parsed[x] = new bool[height];
                for (var y = 0; y < height; y++)
                {
                    parsed[x][y] = int.Parse(level[x][y].Split('-')[1]) == (int)tileType;
                }
            }

            return parsed;
        }

        public static Dictionary<int, Tuple<Point, Point>[]> SplitIntoRectangles(bool[][] input)
        {
            var result = new Dictionary<int, Tuple<Point, Point>[]>();
            var width = input.Length;

            var numberOfScreens = (width / Constants.ScreenWidthInTiles) + 1;
            var lastScreenWidth = width - ((numberOfScreens - 1) * Constants.ScreenWidthInTiles);

            for (var screen = 0; screen < numberOfScreens; screen++)
            {
                var isLastScreen = screen == numberOfScreens - 1;
                var segmentWidth = isLastScreen ? lastScreenWidth : Constants.ScreenWidthInTiles;
                var newInput = new bool[segmentWidth][];

                for (var x = 0; x < segmentWidth; x++)
                {
                    var sourceX = screen * Constants.ScreenWidthInTiles + x;
                    if (sourceX >= input.Length)
                    {
                        break;
                    }

                    newInput[x] = new bool[input[sourceX].Length];
                    for (var y = 0; y < input[sourceX].Length; y++)
                    {
                        newInput[x][y] = input[sourceX][y];
                    }
                }

                result.Add(screen, SplitSectionRectangles(newInput));                
            }

            return result;
        }

        private static Tuple<Point, Point>[] SplitSectionRectangles(bool[][] input)
        {
            var result = new List<Tuple<Point, Point>>();
            var width = input.Length;

            if (width == 0)
            {
                return result.ToArray();
            }

            var height = input[0].Length;

            while (true)
            {
                // Find a set cell.
                int x = 0, y = 0, x2 = 0, y2 = 0;
                var found = false;
                for (x = 0; x < width; x++)
                {
                    for (y = 0; y < height; y++)
                    {
                        if (input[x][y])
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }

                if (!found)
                {
                    // No set cells left.
                    break;
                }

                // Go down as long as possilble
                y2 = y;
                while (true)
                {
                    input[x][y2] = false;

                    if (y2 < height - 1)
                    {
                        y2++;
                        if (!input[x][y2])
                        {
                            y2--;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Go right as long as possible
                x2 = x;
                while (x2 < width - 1)
                {
                    x2++;

                    var canErase = true;
                    for (var yPos = y; yPos <= y2 && canErase; yPos++)
                    {
                        canErase = input[x2][yPos];
                    }

                    if (!canErase)
                    {
                        x2--;
                        break;
                    }

                    for (var yPos = y; yPos <= y2 && canErase; yPos++)
                    {
                        input[x2][yPos] = false;
                    }
                }

                result.Add(Tuple.Create(new Point(x, y), new Point(x2, y2)));
            }

            return result.ToArray();
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
            Grid = 1,
            Type = 2,
            All = 3
        }

        public class TileSelector : PictureBox
        {
            private Bitmap bitmap;
            private Action<string> onClick;
            private TileType tileType;
            private int palette;

            public TileSelector(MyBitmap image, TileType tileType, int palette, Action<string> onClick)
            {
                this.Anchor = AnchorStyles.None;
                this.bitmap = image.Scale(Zoom).ToBitmap();
                this.Image = this.bitmap;
                this.Size = this.bitmap.Size;
                this.Cursor = Cursors.Cross;
                this.onClick = onClick;
                this.tileType = tileType;
                this.palette = palette;

                this.MouseMove += TileSelectorMouseMove;
                this.MouseLeave += TileSelectorMouseLeave;
                this.MouseDown += TileSelectorMouseDown;
            }

            private void TileSelectorMouseDown(object sender, MouseEventArgs e)
            {
                var x = e.X / TileWidth;
                var y = e.Y / TileHeight;
                onClick(TileIds.PaletteTileId(this.palette, this.tileType, x, y));            
            }

            private void TileSelectorMouseLeave(object sender, EventArgs e)
            {
                this.Image = this.bitmap;
                this.Refresh();
            }

            private void TileSelectorMouseMove(object sender, MouseEventArgs e)
            {
                var x = e.X / TileWidth;
                var y = e.Y / TileHeight;
                var bitmapClone = new Bitmap(this.bitmap);
                using (var graphics = Graphics.FromImage(bitmapClone))
                {
                    graphics.DrawRectangle(new Pen(MyBitmap.GridColor, 2), x * TileWidth + 1, y * TileHeight + 1, TileWidth - 2, TileHeight - 2);
                }

                this.Image = bitmapClone;
                this.Refresh();
            }
        }

        #endregion        
    }
}
