using SpriteHelper.Contract;
using SpriteHelper.Controls;
using SpriteHelper.Files;
using SpriteHelper.NesGraphics;
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
        private BackgroundConfig bgConfig;
        private SpriteConfig enConfig;

        // Palette tabs.
        private Dictionary<TileType, TabPage> tileTabs;
        private Color TransparentColor => this.palettes.SpritesPalette.First().ActualColors.First();

        // Background bitmaps, keys: type -> palette.
        private Dictionary<TileType, Dictionary<int, MyBitmap>> bgBitmaps;

        // Enemy bitmaps, keys: name -> flip
        private Dictionary<string, Dictionary<bool, Bitmap>> enBitmaps;
        private Dictionary<string, Dictionary<bool, Bitmap>> enBitmapsTransparent;

        // Player bitmap
        private Bitmap playerBitmap;

        // Add/edit enemy dictionaries.
        private Dictionary<string, Bitmap> enBitmapsSimple;
        private Dictionary<string, MovementType[]> enMovements;
        private Dictionary<string, bool> enShooting;

        // Cached tiles. Key is tile ID + palette.
        private Dictionary<string, Bitmap[]> tiles;
        private string selectedTile;

        // Default size.
        private const int DefaultWidthInTiles = 64;
        private const int HeightInTiles = 15;

        // Zoom and tile width
        public const int TileWidth = Constants.BackgroundTileWidth * Constants.LevelEditorZoom;
        public const int TileHeight = Constants.BackgroundTileHeight * Constants.LevelEditorZoom;

        // Empty tile for the level.
        private string emptyTile;

        // Current level.
        private string[][] level;

        // Properties.
        private Point playerStartingPosition;
        private Point exitPosition;

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
            this.InitializeComponent();
            this.ClearStatus();

            this.history = new Stack<string[][]>();
            this.future = new Stack<string[][]>();

            this.drawPanel = new DoubleBufferedPanel();
            this.outerDrawPanel.Controls.Add(this.drawPanel);
            this.drawPanel.Location = new Point(0, 0);
            this.drawPanel.MouseDown += this.DrawPanelMouseDown;
            this.drawPanel.MouseLeave += this.DrawPanelMouseLeave;
            this.drawPanel.MouseEnter += this.DrawPanelMouseEnter;
            this.drawPanel.MouseMove += this.DrawPanelMouseMove;
            (this.drawPanel as Control).KeyDown += this.DrawPanelKeyUpDown;
            (this.drawPanel as Control).KeyUp += this.DrawPanelKeyUpDown;

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
            this.LoadLevel(
                Defaults.Instance.DefaultLevel, 
                Defaults.Instance.BackgroundSpec,
                @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Sprites\enemies.xml",
                Defaults.Instance.PalettesSpec,
                Defaults.Instance.PlayerSpec);
        }

        private void ClearStatus()
        {
            this.toolStripStatusLabel.Text = null;
        }

        private bool UpdateStatusWithPosition(Point position)
        {
            return this.UpdateStatus("{0} / {1}", position.X, position.Y);
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

        private void LoadLevel(string level, string bgSpec, string enSpec, string palettes, string player)
        {
            this.palettes = Palettes.Read(palettes);
            this.bgConfig = BackgroundConfig.Read(bgSpec);

            this.bgBitmaps = new Dictionary<TileType, Dictionary<int, MyBitmap>>();

            foreach (var kvp in new Dictionary<TileType, MyBitmap>
                                    {
                                        { TileType.NonBlocking, MyBitmap.FromFile(bgConfig.NonBlockingFile) },
                                        { TileType.Blocking, MyBitmap.FromFile(bgConfig.BlockingFile) },
                                        { TileType.Threat, MyBitmap.FromFile(bgConfig.ThreatFile) },
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

                this.bgBitmaps.Add(type, dictionary);
            }

            this.tiles = new Dictionary<string, Bitmap[]>();
            foreach (var tile in this.bgConfig.Tiles)
            {
                for (var palette = 0; palette < 4; palette++)
                {
                    var tileBitmaps = new Bitmap[(int)TileVersion.All + 1];
                    var tileBitmap = bgBitmaps[tile.Type][palette].GetPart(
                        tile.X * Constants.BackgroundTileWidth, 
                        tile.Y * Constants.BackgroundTileHeight, 
                        Constants.BackgroundTileWidth, 
                        Constants.BackgroundTileHeight).Scale(Constants.LevelEditorZoom);

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
            this.emptyTile = TileIds.PaletteTileId(0, this.bgConfig.Tiles[0].Id);

            string[][] newLevel;
            Enemy[] enemies;

            if (File.Exists(level))
            {
                var readLevel = Level.Read(level);
                newLevel = readLevel.Tiles;
                enemies = readLevel.Enemies;
                this.playerStartingPosition = readLevel.PlayerStartingPosition;
                this.exitPosition = readLevel.ExitPosition;
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

                enemies = new Enemy[0];
                this.playerStartingPosition = default(Point);
                this.exitPosition = default(Point);
            }

            // Load enemies config
            this.enConfig = SpriteConfig.Read(enSpec, this.palettes);
            this.enBitmaps = new Dictionary<string, Dictionary<bool, Bitmap>>();
            this.enBitmapsTransparent = new Dictionary<string, Dictionary<bool, Bitmap>>();

            // Prerender each enemy
            foreach (var animation in this.enConfig.Animations)
            {
                var firstFrame = animation.Frames.First();
                
                this.enBitmaps.Add(animation.Name, new Dictionary<bool, Bitmap>());
                this.enBitmapsTransparent.Add(animation.Name, new Dictionary<bool, Bitmap>());

                var imgNoFlip =
                    firstFrame.GetGridBitmap(
                        this.TransparentColor,
                        true,
                        false,
                        false,
                        false,
                        Constants.LevelEditorZoom,
                        null,
                        false,
                        true);

                var imgFlip =
                    firstFrame.GetGridBitmap(
                        this.TransparentColor,
                        true,
                        false,
                        animation.Flip == Flip.Vertical,
                        animation.Flip == Flip.Horizontal,
                        Constants.LevelEditorZoom,
                        null,
                        false,
                        true);

                var imgNoFlipTransparent =
                    firstFrame.GetGridBitmap(
                        this.TransparentColor,
                        true,
                        false,
                        false,
                        false,
                        Constants.LevelEditorZoom,
                        null,
                        true,
                        true);

                var imgFlipTransparent =
                    firstFrame.GetGridBitmap(
                        this.TransparentColor,
                        true,
                        false,
                        animation.Flip == Flip.Vertical,
                        animation.Flip == Flip.Horizontal,
                        Constants.LevelEditorZoom,
                        null,
                        true,
                        true);

                this.enBitmaps[animation.Name].Add(false, imgNoFlip);
                this.enBitmaps[animation.Name].Add(true, imgFlip);
                this.enBitmapsTransparent[animation.Name].Add(false, imgNoFlipTransparent);
                this.enBitmapsTransparent[animation.Name].Add(true, imgFlipTransparent);
            }

            // Initialize and set enemies.
            foreach (var enemy in enemies)
            {
                enemy.Initialize(this.enConfig.Animations.First(a => a.Name == enemy.Name));
            }

            this.enemiesListBox.Items.Clear();
            this.enemiesListBox.Items.AddRange(enemies);

            // Add/edit enemy dictionaries.
            this.enBitmapsSimple = this.enBitmaps.Keys.ToDictionary(k => k, k => this.enBitmaps[k][false]);

            this.enMovements = this.enConfig.Animations.ToDictionary(
                a => a.Name,
                a =>
                {
                    var movements = new List<MovementType> { MovementType.None, MovementType.Horizontal, MovementType.Vertical };
                    if (a.Flip == Flip.Horizontal)
                    {
                        movements.Remove(MovementType.Vertical);
                    }
                    else if (a.Flip == Flip.Vertical)
                    {
                        movements.Remove(MovementType.Horizontal);
                    }

                    return movements.ToArray();
                });

            this.enShooting = this.enConfig.Animations.ToDictionary(a => a.Name, a => a.Offsets.GunXOff >= 0);

            // Player config.
            var playerConfig = SpriteConfig.Read(player, this.palettes);
            this.playerBitmap = playerConfig.Frames.First().GetPlayerBitmap(playerConfig, this.TransparentColor, true, false, false, Constants.LevelEditorZoom, true);

            // Populate tiles, set level.
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
                    var tileSelector = new TileSelector(this.bgBitmaps[type][palette], type, palette, id => this.SetSelectedTile(id));

                    var tableLayoutPanel = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = this.TransparentColor };
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

        public Point MouseTilePosition(int x, int y)
        {
            return new Point(x / TileWidth, y / TileHeight);
        }

        public Point MouseGamePosition(int x, int y)
        {
            return new Point(x / Constants.LevelEditorZoom, y / Constants.LevelEditorZoom);
        }

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

        private bool ChangeWidth(int width)
        {
            if (width == this.level.Length)
            {
                return false;
            }
            
            if (width < 16 | width > 252)
            {
                MessageBox.Show("Wrongh width: min is 16, max is 252", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!(width % 4 == 0))
            {
                MessageBox.Show("Width must be a multiple of 4", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
            return true;
        }

        private void SetLevel(string[][] newLevel)
        {
            this.level = newLevel;
            this.UpdateTileCount();
            this.UpdateBitmap();
        }

        private void UpdateBitmap()
        {
            // Create bitmap.
            this.bitmap = new Bitmap(this.level.Length * TileWidth, this.level[0].Length * TileHeight);
            this.graphics = Graphics.FromImage(this.bitmap);

            // Draw background.
            this.DrawBackground();

            // Draw screen boundaries if needed.
            this.DrawScreenBoundaries();

            // Draw enemies.
            this.DrawEnemies();

            // Draw player and exit.
            this.DrawPlayerAndExit();

            this.UpdateDrawPanel();
        }

        private void DrawBackground()
        {
            for (var x = 0; x < this.level.Length; x++)
            {
                for (var y = 0; y < this.level[x].Length; y++)
                {
                    var key = this.level[x][y];
                    var image = this.tiles[key][(int)this.Settings];
                    this.graphics.DrawImage(image, new Point(x * TileWidth, y * TileHeight));
                }
            }
        }

        private void DrawScreenBoundaries()
        {
            if (this.showScreensToolStripMenuItem.Checked)
            {
                for (var x = Constants.ScreenWidthInTiles * TileWidth; x < this.level.Length * TileWidth; x += Constants.ScreenWidthInTiles * TileWidth)
                {
                    this.graphics.DrawLine(new Pen(Color.Yellow, 3), x, 0, x, this.bitmap.Height);
                }
            }
        }

        private void DrawEnemies()
        {
            if (this.showEnemiesToolStripMenuItem.Checked)
            {
                foreach (var enemy in Enemies)
                {
                    // Get image.
                    var image = this.enBitmaps[enemy.Name][enemy.InitialFlip];

                    if (this.showEnemyMovementToolStripMenuItem.Checked && enemy.MovementType != MovementType.None)
                    {
                        // Get transparent image.
                        var imageTransparent = this.enBitmapsTransparent[enemy.Name][enemy.InitialFlip];

                        // Calculate coordinates.
                        var minX = enemy.MovementType == MovementType.Horizontal ? enemy.MinPosition : enemy.X;
                        var minY = enemy.MovementType == MovementType.Vertical ? enemy.MinPosition : enemy.Y;
                        var maxX = enemy.MovementType == MovementType.Horizontal ? enemy.MaxPosition : enemy.X;
                        var maxY = enemy.MovementType == MovementType.Vertical ? enemy.MaxPosition : enemy.Y;

                        // Draw line.
                        var width = image.Width;
                        var height = image.Height;
                        var p1 = new Point(minX * Constants.LevelEditorZoom + width / 2, minY * Constants.LevelEditorZoom + height / 2);
                        var p2 = new Point(maxX * Constants.LevelEditorZoom + width / 2, maxY * Constants.LevelEditorZoom + height / 2);
                        var orangePen = new Pen(Color.DarkOrange, 3);
                        this.graphics.DrawLine(orangePen, p1, p2);

                        // Draw arrow.
                        if (enemy.MovementType == MovementType.Horizontal)
                        {
                            if (enemy.InitialFlip)
                            {
                                // Left, p1
                                this.graphics.DrawPolygon(orangePen, new[] { p1, new Point(p1.X + 3, p1.Y + 3), new Point(p1.X + 3, p1.Y - 3) });
                            }
                            else
                            {
                                // Right, p2
                                this.graphics.DrawPolygon(orangePen, new[] { p2, new Point(p2.X - 3, p2.Y + 3), new Point(p2.X - 3, p2.Y - 3) });
                            }
                        }
                        else if (enemy.MovementType == MovementType.Vertical)
                        {
                            if (enemy.InitialFlip)
                            {
                                // Up, p1
                                this.graphics.DrawPolygon(orangePen, new[] { p1, new Point(p1.X - 3, p1.Y + 3), new Point(p1.X + 3, p1.Y + 3) });
                            }
                            else
                            {
                                // Down, p2
                                this.graphics.DrawPolygon(orangePen, new[] { p2, new Point(p2.X - 3, p2.Y - 3), new Point(p2.X + 3, p2.Y - 3) });
                            }
                        }

                        if (minX != enemy.X || minY != enemy.Y)
                        {
                            // Draw min. transparent image.
                            this.graphics.DrawImage(imageTransparent, new Point(minX * Constants.LevelEditorZoom, minY * Constants.LevelEditorZoom));
                        }

                        if (maxX != enemy.X || maxY != enemy.Y)
                        {
                            // Draw max. transparent image.
                            this.graphics.DrawImage(imageTransparent, new Point(maxX * Constants.LevelEditorZoom, maxY * Constants.LevelEditorZoom));
                        }
                    }

                    // Draw regular image.                   
                    this.graphics.DrawImage(image, new Point(enemy.X * Constants.LevelEditorZoom, enemy.Y * Constants.LevelEditorZoom));

                    if (enemy == this.SelectedEnemy)
                    {
                        // Draw red box around selected enemy.
                        this.graphics.DrawRectangle(Pens.Red, enemy.X * Constants.LevelEditorZoom, enemy.Y * Constants.LevelEditorZoom, image.Width, image.Height);
                    }
                }
            }
        }

        private void DrawPlayerAndExit()
        {
            if (this.showPlayerToolStripMenuItem.Checked)
            {
                this.graphics.DrawImage(
                    this.playerBitmap, 
                    new Point(
                        (this.playerStartingPosition.X - Constants.PlayerXOffset) * Constants.LevelEditorZoom, 
                        (this.playerStartingPosition.Y - Constants.PlayerYOffset) * Constants.LevelEditorZoom));
            }
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

        private void ShowScreensToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void ShowEnemiesToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void ShowEnemiesToolStripMenuItemCheckedChanged(object sender, EventArgs e)
        {
            this.showEnemyMovementToolStripMenuItem.Enabled = this.showEnemiesToolStripMenuItem.Checked;
        }

        private void ShowEnemyMovementToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void ShowPlayerToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void ShowMovingPlatformsToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void HideNonBgToolStringMenuItemClick(object sender, EventArgs e)
        {
            this.OnlyBackgroundShown = this.hideNonBgToolStringMenuItem.Checked;
        }

        private void HideNonBgToolStringMenuItemCheckedChanged(object sender, EventArgs e)
        {
            this.hideNonBgToolStringMenuItem.Enabled = !this.hideNonBgToolStringMenuItem.Checked;
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
                    this.LoadLevel(loadLevelDialog.Level, loadLevelDialog.BgSpec, loadLevelDialog.EnSpec, loadLevelDialog.Palettes, loadLevelDialog.Player);
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
                    this.LoadLevel(null, loadLevelDialog.BgSpec, loadLevelDialog.EnSpec, loadLevelDialog.Palettes, loadLevelDialog.Player);
                }
            };

            loadLevelDialog.ShowDialog();
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!this.ValidateAllEnemies())
            {
                return;
            }

            var saveFileDialog = new SaveFileDialog { InitialDirectory = Defaults.Instance.LevelsDefaultDir, Filter = "xml files (*.xml)|*.xml" };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                var level = new Level { Tiles = this.level, Enemies = this.Enemies, PlayerStartingPosition = this.playerStartingPosition, ExitPosition = this.exitPosition };
                level.Write(saveFileDialog.FileName);
            }
        }

        private void ExportLevelToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!this.ValidateAllEnemies())
            {
                return;
            }

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
            Func<EditLevelDialog, Tuple<int, Point, Point>> getResultFunc =
                dialog =>
                {
                    int levelWidth;
                    Point playerStartingPosition, exitPosition;

                    if (!dialog.TryGetWidth(out levelWidth))
                    {
                        MessageBox.Show("Level width is invalid");
                        return null;
                    }

                    if (!dialog.TryGetPlayerStartingPosition(out playerStartingPosition))
                    {
                        MessageBox.Show("Player starting position is invalid");
                        return null;
                    }

                    if (!dialog.TryGetExitPosition(out exitPosition))
                    {
                        MessageBox.Show("Exit position is invalid");
                        return null;
                    }

                    if (levelWidth < 16 | levelWidth > 252)
                    {
                        MessageBox.Show("Wrongh width: min is 16, max is 252");
                        return null;
                    }

                    if (!(levelWidth % 4 == 0))
                    {
                        MessageBox.Show("Width must be a multiple of 4");
                        return null;
                    }

                    if (playerStartingPosition.X < 0)
                    { 
                        MessageBox.Show("Player position X must be >= 0");
                        return null;
                    }

                    if (playerStartingPosition.X > Constants.ScreenWidth / 2 - Constants.SpriteWidth * 3)
                    { 
                        MessageBox.Show($"Player position X must be <= {Constants.ScreenWidth / 2 - Constants.SpriteWidth * 3}");
                        return null;
                    }

                    if (playerStartingPosition.Y < 0)
                    {
                        MessageBox.Show("Player position Y must be >= 0");
                        return null;
                    }

                    if (playerStartingPosition.Y > Constants.ScreenHeight)
                    {
                        MessageBox.Show($"Player position Y must be <= {Constants.ScreenHeight}");
                        return null;
                    }

                    if (exitPosition.X < 0)
                    {
                        MessageBox.Show("Exit position X must be >= 0");
                        return null;
                    }

                    if (exitPosition.X > levelWidth * Constants.BackgroundTileWidth)
                    {
                        MessageBox.Show($"Exit position X must be <= {levelWidth * Constants.BackgroundTileWidth}");
                        return null;
                    }

                    if (exitPosition.Y < 0)
                    {
                        MessageBox.Show("Exit position Y must be >= 0");
                        return null;
                    }

                    if (exitPosition.Y > Constants.ScreenHeight)
                    {
                        MessageBox.Show($"Exit position Y must be <= {Constants.ScreenHeight}");
                        return null;
                    }

                    var clippedEnemies = this.Enemies.Where(en => en.X > levelWidth * Constants.BackgroundTileWidth).Select(en => en.ToString()).ToArray();
                    if (clippedEnemies.Any())
                    {
                        MessageBox.Show($"Enemies\r\n{string.Join("\r\n", clippedEnemies)}\r\n would be removed from the screen");
                        return null;
                    }

                    return Tuple.Create(levelWidth, playerStartingPosition, exitPosition);
                };

            var editLevelDialog = new EditLevelDialog(
                this.level.Length, 
                this.playerStartingPosition, 
                this.exitPosition, 
                dialog => getResultFunc(dialog) != null);

            editLevelDialog.ShowDialog();
            if (!editLevelDialog.Succeeded)
            {
                return;
            }

            var result = getResultFunc(editLevelDialog);

            var playerStartingPositionDifferent = false;
            var exitPositionDifferent = false;
            var widthChanged = false;

            if (this.playerStartingPosition != result.Item2)
            {
                this.playerStartingPosition = result.Item2;
                playerStartingPositionDifferent = true;
            }

            if (this.exitPosition != result.Item3)
            {
                this.exitPosition = result.Item3;
                exitPositionDifferent = true;
            }
            
            widthChanged = this.ChangeWidth(result.Item1);

            // Only update bitmap if either position has changed, and width hasn't changed - ChangeWidth updates the bitmap if that's the case.
            if ((playerStartingPositionDifferent || exitPositionDifferent) && !widthChanged)
            {
                this.UpdateBitmap();
            }

            // todo: store player and exit position in history
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
            writer.WriteLineIfNotNull("Total size: {0} bytes", bytes.Length);
            MessageBox.Show(builder.ToString(), "Export data", MessageBoxButtons.OK);
        }

        private bool OnlyBackgroundShown
        {
            get
            {
                return !(this.showScreensToolStripMenuItem.Checked ||
                         this.showEnemiesToolStripMenuItem.Checked ||
                         this.showEnemyMovementToolStripMenuItem.Checked ||
                         this.showPlayerToolStripMenuItem.Checked ||
                         this.showMovingPlatformsToolStripMenuItem.Checked);
            }

            set
            { 
                if (value)
                {
                    this.showScreensToolStripMenuItem.Checked = false;
                    this.showEnemiesToolStripMenuItem.Checked = false;
                    this.showEnemyMovementToolStripMenuItem.Checked = false;
                    this.showPlayerToolStripMenuItem.Checked = false;
                    this.showMovingPlatformsToolStripMenuItem.Checked = false;
                    this.UpdateBitmap();
                }
            }
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
        //// Draw panel events
        ////

        private bool ControlPressed => (Control.ModifierKeys & Keys.Control) != 0;

        private void DrawPanelKeyUpDown(object sender, KeyEventArgs e)
        {
            // When key is pressed, update cursor.
            this.DrawPanelSetCursor();

            // Get the mouse position relative to the panel.
            var mouse = this.drawPanel.PointToClient(Control.MousePosition);

            // And based on the input draw or remove the tile cursor, and update the panel.
            if (this.ControlPressed)
            {
                var position = MouseGamePosition(mouse.X, mouse.Y);
                this.UpdateStatusWithPosition(position);
                this.DrawPanelRemoveTileCursor();
            }
            else
            {

                var position = MouseTilePosition(mouse.X, mouse.Y);
                this.UpdateStatusWithPosition(position);
                this.DrawPanelDrawTileCursor(position);
            }
        }

        private void DrawPanelMouseLeave(object sender, EventArgs e)
        {
            // On mouse leave clear the status.
            this.ClearStatus();

            // And remove the tile cursor.
            this.DrawPanelRemoveTileCursor();
        }

        private void DrawPanelMouseEnter(object sender, EventArgs e)
        {
            // When mouse enters the panel, focus it so key events will work.
            this.drawPanel.Focus();

            // And set the right cursor.
            this.DrawPanelSetCursor();            
        }

        private void DrawPanelMouseDown(object sender, MouseEventArgs e)
        {
            // When mouse is clicked, do the right action.            
            if (!this.ControlPressed)
            {
                // If control not pressed, set a tile (if needed) and draw the cursor
                var position = MouseTilePosition(e.X, e.Y);
                if (this.SetTile(position, e.Button))
                {
                    this.DrawPanelDrawTileCursor(position);
                }
            }
            else if (this.showEnemiesToolStripMenuItem.Checked)
            {
                // If control pressed, if enemies are shown, select an ememy and (on right click) open edit window.
                var position = MouseGamePosition(e.X, e.Y);
                var clickedEnemy = this.Enemies.FirstOrDefault(en => en.Select(position));
                if (clickedEnemy != null)
                {
                    var left = e.Button.HasFlag(MouseButtons.Left);
                    var right = e.Button.HasFlag(MouseButtons.Right);
                    if (left || right)
                    {
                        this.mainTabControl.SelectedTab = enTabPage;
                        this.enemiesListBox.Focus();
                        this.enemiesListBox.SelectedItem = clickedEnemy;                        

                        if (right)
                        {
                            this.EditEnemy();
                        }
                    }                    
                }
                else
                {
                    this.enemiesListBox.SelectedItem = null;
                }              
            }
        }

        private void DrawPanelMouseMove(object sender, MouseEventArgs e)
        {
            // When mouse moves, set the right cursor.
            this.DrawPanelSetCursor();

            // Then update status (do that always).                        
            if (!this.ControlPressed)
            {
                // If control not pressed update status, set a tile (if needed) and draw the cursor
                var position = MouseTilePosition(e.X, e.Y);
                if (this.UpdateStatusWithPosition(position))
                {
                    this.SetTile(position, e.Button);
                    this.DrawPanelDrawTileCursor(position);
                }
            }
            else
            {
                // Otherwise update status and clear the cursor.
                var position = MouseGamePosition(e.X, e.Y);
                this.UpdateStatusWithPosition(position);
                this.DrawPanelRemoveTileCursor();
            }
               
        }

        private void DrawPanelSetCursor()
        {
            this.drawPanel.Cursor = this.ControlPressed ? Cursors.Hand : Cursors.Cross;
        }

        private void DrawPanelDrawTileCursor(Point position)
        {
            var x = position.X;
            var y = position.Y;

            var bitmapClone = new Bitmap(this.bitmap);
            using (var graphics = Graphics.FromImage(bitmapClone))
            {
                graphics.DrawRectangle(new Pen(MyBitmap.GridColor, 2), x * TileWidth + 1, y * TileHeight + 1, TileWidth - 2, TileHeight - 2);
            }

            drawPanel.BackgroundImage = bitmapClone;
            drawPanel.Refresh();
        }

        private void DrawPanelRemoveTileCursor()
        {
            this.drawPanel.BackgroundImage = this.bitmap;
            this.drawPanel.Refresh();
        }

        public bool SetTile(Point position, MouseButtons buttons)
        {
            var x = position.X;
            var y = position.Y;

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

            if (this.OnlyBackgroundShown)
            {
                // Just draw one tile if only background is shown.
                this.graphics.DrawImage(image, new Point(x * TileWidth, y * TileHeight));                
            }
            else
            {
                // Otherwise, we have to redraw the whole bitmap.
                this.UpdateBitmap();
            }
            
            // Update tile count.
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
            result.AddRange(GetExportPlatformAndThreatData(logger));
            logger.WriteLineIfNotNull();

            // Enemies.
            result.AddRange(GetExportEnemiesData(logger));
            logger.WriteLineIfNotNull();

            // Starting position and level end.
            result.AddRange(GetStartingPositionAndEndData(logger));
            logger.WriteLineIfNotNull();

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
                var tileConfig = this.bgConfig.Tiles.First(t => t.Id == TileIds.ParsePaletteId(tileId).Item2);
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

        private byte[] GetExportPlatformAndThreatData(TextWriter logger = null)
        {
            var result = new List<byte>();
            result.AddRange(GetExportPlatformOrThreatData(TileType.Blocking, logger));
            result.AddRange(GetExportPlatformOrThreatData(TileType.Threat, logger));
            return result.ToArray();
        }

        private byte[] GetExportPlatformOrThreatData(TileType tileType, TextWriter logger = null)
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

        private byte[] GetExportEnemiesData(TextWriter logger = null)
        {
            //
            // - enemies in the following format:
            //   - pointer to next screen (from here): (n x 14) + 3 (1 byte)
            //   - number of enemies (1 byte)
            //   - n times the enemy data (14 bytes)
            //        - id (1 byte)
            //        - slot to put enemy in (1 byte)
            //        - pointer to const. data (1 byte)
            //        - screen the enemy is on (1 byte)            
            //        - movement speed (1 byte)
            //        - max movement distance (1 byte)            
            //        - initial movement left (1 byte)            
            //        - initial flip (1 byte)
            //        - movement type (1 byte)
            //        - x position (1 byte)
            //        - y position (1 byte)            
            //        - initial life (1 byte)
            //        - shooting frequency initial (1 byte)
            //        - shooting frequency (1 byte)
            //   - pointer to the previous screen (from here): (n x 14) + 2 (1 byte)
            //

            var result = new List<byte>();

            // if width % screen_width_in_tiles == 0, the last screen will contain no data
            // e.g. if width == 40, we have 3 screens, numberOfScreens == 3
            //      if width == 32, we have 2 screens, but numberOfScreens still == 3
            // same applies to all other data types (platforms etc).
            var width = this.level.Length;
            var numberOfScreens = (width / Constants.ScreenWidthInTiles) + 1;

            var dataPerScreen = new Dictionary<int, byte[]>();

            // Available slots. -1 means empty, n (>0) means taken for enemy from the nth screen.
            var slots = new int[Constants.EnemiesLimitPerTwoScreens];
            for (var i = 0; i < Constants.EnemiesLimitPerTwoScreens; i++)
            {
                slots[i] = -1;
            }

            var enemyId = 0;
            
            for (var screen = 0; screen < numberOfScreens; screen++)
            {
                // Clear the slots for two screens ago.
                for (var slot = 0; slot < Constants.EnemiesLimitPerTwoScreens; slot++)
                {
                    if (slots[slot] == screen - 2)
                    {
                        slots[slot] = -1;
                    }
                }

                var enemies = this.Enemies.Where(e => e.Screen == screen).ToArray();
                var n = (byte)enemies.Length;

                // Pointer to the next screen
                result.Add((byte)(n * Constants.EnemyInLevelDataSize + 3));

                // Number of enemies
                result.Add(n);

                // Enemies data.
                foreach (var enemy in enemies)
                {
                    int slot;
                    for (slot = 0; slot < Constants.EnemiesLimitPerTwoScreens; slot++)
                    {
                        if (slots[slot] == -1)
                        {
                            slots[slot] = screen;
                            break;
                        }
                    }

                    if (slot == Constants.EnemiesLimitPerTwoScreens)
                    {
                        // Sanity check.
                        throw new Exception();
                    }

                    var animation = enemy.Animation;

                    //
                    // Set the data.
                    //

                    // id
                    // todo: make this two bytes
                    result.Add((byte)enemyId++);

                    // slot to put enemy in (1 byte)
                    result.Add((byte)(slot * Constants.EnemyInMemorySize));

                    // pointer to const. data
                    result.Add((byte)(animation.Id * Constants.EnemyDefinitionSize));

                    // screen
                    result.Add((byte)screen);

                    // movement speed
                    result.Add((byte)enemy.Speed);

                    // max movement distance
                    result.Add((byte)(enemy.MovementRange));

                    // initial flip
                    result.Add((byte)(enemy.InitialFlip ? 1 : 0));

                    // initial movement distance
                    result.Add((byte)(enemy.InitialDistanceLeft));

                    // movement type
                    result.Add((byte)enemy.MovementOrientation);

                    // x position
                    result.Add((byte)enemy.X);

                    // y position
                    result.Add((byte)enemy.Y);

                    // initial life
                    result.Add((byte)animation.MaxHealth);

                                        // shooting frequency initial
                    result.Add((byte)enemy.ShootingInitialFrequency);

                    // shooting frequency
                    result.Add((byte)enemy.ShootingFrequency);
                }

                // Pointer to the previous screen
                result.Add((byte)(n * Constants.EnemyInLevelDataSize + 2));
            }

            logger.WriteLineIfNotNull("Total bytes for Enemies data: {0}", result.Count);
            return result.ToArray();
        }

        private byte[] GetStartingPositionAndEndData(TextWriter logger = null)
        {
            //
            // - data in the following format:
            //   - 2 bytes: player position (x/y, always on screen 0)
            //   - 3 bytes: exit position (screen/x/y)
            //

            var result = new List<byte>();

            result.Add((byte)this.playerStartingPosition.X);
            result.Add((byte)this.playerStartingPosition.Y);
            result.Add((byte)(this.exitPosition.X / 256));
            result.Add((byte)(this.exitPosition.X % 256));
            result.Add((byte)this.exitPosition.Y);

            logger.WriteLineIfNotNull("Total bytes for Player and Exit data: {0}", result.Count);
            return result.ToArray();
        }

        #endregion

        #region Enemies

        public Enemy[] Enemies => this.enemiesListBox.Items.Cast<Enemy>().ToArray();

        public Enemy SelectedEnemy => this.enemiesListBox.SelectedItem as Enemy;        

        private void EnemiesListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable/disable buttons.
            this.deleteEnemyButton.Enabled = this.SelectedEnemy != null;
            this.editEnemyButton.Enabled = this.SelectedEnemy != null;
            this.findEnemyButton.Enabled = this.SelectedEnemy != null;

            // Update bitmap (highlight enemy).
            this.UpdateBitmap();
        }

        private void EnemiesListBoxMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Right))
            {
                var index = this.enemiesListBox.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    var item = this.enemiesListBox.Items[index];
                    this.enemiesListBox.SelectedItem = item;

                    // On right click edit the enemy.
                    this.EditEnemy();
                }
            }
        }

        private void EnemiesListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var index = this.enemiesListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var item = this.enemiesListBox.Items[index];
                if (item == this.SelectedEnemy)
                {
                    this.FindEnemy();
                }
            }            
        }

        private void EnemiesListBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && this.SelectedEnemy != null)
            {
                this.DeleteEnemy();
            }
        }

        private void AddEnemyButtonClick(object sender, EventArgs e)
        {
            this.AddEnemy();
        }

        private void DeleteEnemyButtonClick(object sender, EventArgs e)
        {
            this.DeleteEnemy();
        }

        private void EditEnemyButtonClick(object sender, EventArgs e)
        {
            this.EditEnemy();
        }

        private void FindEnemyButtonClick(object sender, EventArgs e)
        {
            this.FindEnemy();
        }

        private void FindEnemy()
        {
            if (this.SelectedEnemy == null)
            {
                // No enemy selected.
                return;
            }

            if (!this.scrollBar.Enabled)
            {
                // Level too small, no scroll.
                return;
            }

            // Get enemy's center in metatiles.
            var enemyRectangle = this.SelectedEnemy.GetGameRectangleInMetaTiles();
            var x1 = enemyRectangle.X;
            var x2 = enemyRectangle.X + enemyRectangle.Width;
            var center = (x1 + x2) / 2;

            // Outer panel width in metatiles.
            var outerOuterPanelWidth = this.outerOuterDrawPanel.Width;
            var outerOuterPanelWidthInMetaTiles = outerOuterPanelWidth / TileWidth;

            // TARGET SCROLL + OUTER PANEL WIDTH / 2 = ENEMY CENTER
            // TARGET SCROLL = ENEMY CENTER - OUTER PANEL WIDTH / 2

            // Calculate target scroll.
            var targetScroll = Math.Max(this.scrollBar.Minimum, Math.Min(this.scrollBar.Maximum, center - outerOuterPanelWidthInMetaTiles / 2));
            this.scrollBar.Value = targetScroll;
            this.UpdateScroll();
        }

        private void AddEnemy()
        {
            this.AddOrEditEnemy(null);
        }

        private void EditEnemy()
        {
            this.AddOrEditEnemy(this.SelectedEnemy);
        }

        private void AddOrEditEnemy(Enemy selectedEnemy)
        {
            // todo: save history in this method

            // Show the dialog.
            var dialog = new AddEditEnemyDialog(selectedEnemy, this.enBitmapsSimple, this.enMovements, this.enShooting, this.TransparentColor, this.ValidateEnemy);
            dialog.ShowDialog();
            if (!dialog.Succeeded)
            {
                return;
            }

            // Get the new enemy.
            Enemy newEnemy;
            if (!dialog.TryGetEnemy(this.enConfig, out newEnemy))
            {
                MessageBox.Show("Something went wrong.");
                return;
            }

            // Remove old enemy from the list if given.
            if (selectedEnemy != null)
            {
                this.enemiesListBox.Items.Remove(selectedEnemy);
            }

            // Add the new enemy to the list and select it.
            this.enemiesListBox.Items.Add(newEnemy);
            this.enemiesListBox.SelectedItem = newEnemy;

            // Sort the list.
            this.SortEnemiesListBox();

            // Update bitmap.
            this.UpdateBitmap();
        }

        private void DeleteEnemy()
        {
            // todo: save history in this method

            this.enemiesListBox.Items.Remove(this.SelectedEnemy);
            this.UpdateBitmap();
        }

        private void SortEnemiesListBox()
        {
            // optimize this?
            // Sort by X. Keep the same item selected.
            var selectedItem = this.SelectedEnemy;
            var enemies = this.enemiesListBox.Items.Cast<Enemy>().OrderBy(e => e.X).ToArray();
            this.enemiesListBox.Items.Clear();
            this.enemiesListBox.Items.AddRange(enemies);
            if (selectedItem != null)
            {
                this.enemiesListBox.SelectedItem = selectedItem;
            }
        }

        private bool ValidateAllEnemies()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Enemy errors:");

            var errors = false;
            foreach (var enemy in this.Enemies)
            {                
                var validation = this.ValidateEnemy(enemy, enemy);
                if (validation != null)
                {
                    errors = true;
                    stringBuilder.AppendLineFormat("{0}: {1}", enemy, validation);
                }
            }

            if (errors)
            {
                MessageBox.Show(stringBuilder.ToString());
            }

            return !errors;
        }

        private string ValidateEnemy(AddEditEnemyDialog dialog)
        {
            // Get all values.
            int x;
            if (!dialog.TryGetX(out x))
            {
                return "X is not a valid number";
            }

            int y;
            if (!dialog.TryGetY(out y))
            {
                return "Y is not a valid number";
            }

            int speed;
            if (!dialog.TryGetSpeed(out speed))
            {
                return "Speed is not a valid number";
            }

            int min;
            if (!dialog.TryGetMin(out min))
            {
                return "Min. position is not a valid number";
            }

            int max;
            if (!dialog.TryGetMin(out max))
            {
                return "Max. position is not a valid number";
            }

            int shootingFreq;
            if (!dialog.TryGetShootingFreq(out shootingFreq))
            {
                return "Shooting freq. is not a valid number";
            }

            int shootingFreqOffset;
            if (!dialog.TryGetShootingInitialFreq(out shootingFreqOffset))
            {
                return "Shooting initial freq. is not a valid number";
            }

            // Try creating an enemy. This should never fail.
            Enemy enemy;
            if (!dialog.TryGetEnemy(this.enConfig, out enemy))
            {
                return "Something went wrong.";
            }            

            // Validate the enemy.
            return this.ValidateEnemy(enemy, dialog.ExistingEnemy);
        }

        private string ValidateEnemy(Enemy enemy, Enemy existingEnemy)
        {
            var position = this.ValidateEnemyPosition(enemy, existingEnemy);
            if (position != null)
            {
                return position;
            }

            var movement = this.ValidateEnemyMovement(enemy);
            if (movement != null)
            {
                return movement;
            }

            var shooting = this.ValidateEnemyShooting(enemy);
            if (shooting != null)
            {
                return shooting;
            }

            return null;
        }

        private string ValidateEnemyPosition(Enemy enemy, Enemy existingEnemy)
        {
            if (enemy.X > this.level.Length * Constants.BackgroundTileWidth)
            {
                return "X is too high";
            }

            if (enemy.X < 0)
            {
                return "X cannot be negative";
            }

            if (enemy.Y > Constants.ScreenHeight)
            {
                return "Y is too high";
            }

            if (enemy.Y < 0)
            {
                return "Y cannot be negative";
            }

            var previousScreenCount = this.Enemies.Count(e => e != existingEnemy && e.Screen == enemy.Screen - 1);
            var currentScreenCount = this.Enemies.Count(e => e != existingEnemy && e.Screen == enemy.Screen) + 1;
            var nextScreenCount = this.Enemies.Count(e => e != existingEnemy && e.Screen == enemy.Screen + 1);

            if (previousScreenCount + currentScreenCount > Constants.EnemiesLimitPerTwoScreens)
            {
                return "Too many enemies on this an the previous screen";
            }

            if (currentScreenCount + nextScreenCount > Constants.EnemiesLimitPerTwoScreens)
            {
                return "Too many enemies on this an the next screen";
            }

            return null;
        }

        private string ValidateEnemyMovement(Enemy enemy)
        {
            if (enemy.MovementType == MovementType.None)
            {
                if (enemy.Speed != 0)
                {
                    return "Speed must be 0 for non-moving enemies";
                }

                if (enemy.InitialDistanceLeft != 0)
                {
                    return "Distance left must be 0 for non-moving enemies";
                }

                return null;
            }

            if (enemy.Speed <= 0)
            {
                return "Speed must be greater than 0";
            }

            if (enemy.Speed > 255)
            {
                return "Speed is too high";
            }

            if (enemy.MinPosition > enemy.MaxPosition)
            {
                return "Min. position must be less than max. position";
            }

            int enemyPosition;
            string enemyPositionString;
            if (enemy.MovementType == MovementType.Horizontal)
            {
                enemyPosition = enemy.X;
                enemyPositionString = "X";

                if (enemy.MinPosition < enemy.Screen * Constants.ScreenWidth)
                {
                    return "Enemy walking off-screen to the left";
                }

                if (enemy.MaxPosition >= Math.Min((enemy.Screen + 1) * Constants.ScreenWidth, this.level.Length * Constants.BackgroundTileWidth))
                {

                    return "Enemy walking off-screen to the right";
                }
            }
            else if (enemy.MovementType == MovementType.Vertical)
            {
                enemyPosition = enemy.Y;
                enemyPositionString = "Y";

                if (enemy.MinPosition < 0)
                {
                    return "Min. position cannot be negative";
                }

                if (enemy.MaxPosition > Constants.ScreenHeight)
                {
                    return "Max. position is too high";
                }
            }
            else
            {
                return "Unknown movement type";
            }

            if (enemy.MinPosition > enemyPosition)
            {
                return $"Min. position must be <= {enemyPositionString}";
            }

            if (enemy.MaxPosition < enemyPosition)
            {
                return $"Max. position must be >= {enemyPositionString}";
            }

            if (enemy.InitialDistanceLeft == 0)
            {
                return "Enemy must have some initial movement in the direction it's facing";
            }

            if ((enemyPosition - enemy.MinPosition) % enemy.Speed != 0)
            {
                return "Min. position is not reachable with given speed";
            }

            if ((enemy.MaxPosition - enemyPosition) % enemy.Speed != 0)
            {
                return "Max. position is not reachable with given speed";
            }

            return null;
        }

        private string ValidateEnemyShooting(Enemy enemy)
        {
            if (enemy.ShootingFrequency < 0)
            {
                return "Shooting frequency must be greater or equal to 0";
            }

            if (enemy.ShootingFrequency > 255)
            {
                return "Shooting frequency is too high";
            }

            if (enemy.ShootingFrequency != 0)
            {
                if (enemy.ShootingInitialFrequency == 0)
                {
                    return "If enemy is shooting, initial frequency cannot be 0";
                }

                if (enemy.MovementOrientation == 0)
                {
                    return "This enemy cannot shoot";
                }
            }            

            if (enemy.ShootingInitialFrequency < 0)
            {
                return "Shooting initial frequency must be greater or equal to 0";
            }

            if (enemy.ShootingInitialFrequency > 255)
            {
                return "Shooting initial frequency is too high";
            }

            return null;
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
                this.bitmap = image.Scale(Constants.LevelEditorZoom).ToBitmap();
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
