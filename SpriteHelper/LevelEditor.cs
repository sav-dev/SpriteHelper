using System;
using System.Collections.Generic;
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
            this.PreLoad();
            this.splitContainerVertical.Panel2.Focus();
        }
        
        private void LevelEditorResize(object sender, EventArgs e)
        {
            this.UpdateDrawPanel();
        }
        
        private void PreLoad()
        {
            this.LoadLevel(null, Defaults.Instance.BackgroundSpec, Defaults.Instance.PalettesSpec);
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

                    this.tiles.Add(TileIds.TileId(palette, tile.Type, tile.X, tile.Y), tileBitmaps);
                }
            }
                        
            // Empty tile should always be the 1st one
            this.emptyTile = TileIds.TileId(0, this.config.Tiles[0].Type, this.config.Tiles[0].X, this.config.Tiles[0].Y);
            
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
                    var tileSelector = new TileSelector(this.bitmaps[type][palette], type, palette, id => this.SetSelectedTile(id) );
                    
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

            if (!(width % 2 == 0))
            {
                MessageBox.Show("Width must be a multiple of 2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
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
            var saveFileDialog = new SaveFileDialog { InitialDirectory = Defaults.Instance.DefaultDir, Filter = "xml files (*.xml)|*.xml" };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                var level = new Level { Tiles = this.level };
                level.Write(saveFileDialog.FileName);
            }
        }

        private void ExportToolStripMenuItemClick(object sender, EventArgs e)
        {
            // todo: export level and attributes
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

        private void UpdateTileCount()
        {
            var tileCount = this.level.SelectMany(l => l).Distinct().Count();
            this.uniqueTilesCountLabel.Text = tileCount.ToString();
            this.uniqueTilesCountLabel.ForeColor = tileCount <= 32 ? Color.Black : Color.Red;
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
                onClick(TileIds.TileId(this.palette, this.tileType, x, y));

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
