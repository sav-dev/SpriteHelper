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
        private Color transparentColor => this.palettes.SpritesPalette.First().ActualColors.First();

        // Background bitmaps, keys: type -> palette.
        private Dictionary<TileType, Dictionary<int, MyBitmap>> bgBitmaps;

        // Enemy bitmaps, keys: name -> flip.
        private Dictionary<string, Dictionary<bool, Bitmap>> enBitmaps;
        private Dictionary<string, Dictionary<bool, Bitmap>> enBitmapsTransparent;

        // Player bitmap.
        private Bitmap playerBitmap;
        private Bitmap playerJetpackBitmap;

        // Elevators.
        private Dictionary<int, Bitmap> elevatorBitmaps;
        private Dictionary<int, Bitmap> elevatorBitmapsTransparent;

        // Doors.
        private DoorAndKeycard doorAndKeycard;
        private Bitmap keycardBitmap;
        private Bitmap doorBitmap;

        // Add/edit enemy dictionaries.
        private Dictionary<string, Bitmap> enBitmapsSimple;
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
        private LevelType levelType;
        private Point playerStartingPosition;
        private Point exitPosition;
        private double scrollSpeed;

        // POI history/future (for undo-redo).
        private Stack<string[][]> history;
        private Stack<string[][]> future;

        // Draw panel.
        private DoubleBufferedPanel drawPanel;
        private Bitmap bitmap;
        private Graphics graphics;

        // Pens and brushes.
        private Pen EnemyMovementPen = new Pen(Color.DarkOrange, 3);
        private Pen ElevatorMovementPen = new Pen(Color.Crimson, 3);

        // Palette related.
        private int bgPalette;

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
                Defaults.Instance.PlayerSpec,
                @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Chr\spr.chr");
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

        private void LoadLevel(string level, string bgSpec, string enSpec, string palettes, string player, string spriteChr)
        {
            this.palettes = Palettes.Read(palettes);
            this.bgConfig = BackgroundConfig.Read(bgSpec);

            string[][] newLevel;
            Enemy[] enemies;
            Elevator[] elevators;

            if (File.Exists(level))
            {
                var readLevel = Level.Read(level);
                newLevel = readLevel.Tiles;
                enemies = readLevel.Enemies;
                elevators = readLevel.Elevators;
                this.levelType = readLevel.LevelType;
                this.playerStartingPosition = readLevel.PlayerStartingPosition;
                this.exitPosition = readLevel.ExitPosition;
                this.scrollSpeed = readLevel.ScrollSpeed;
                this.bgPalette = readLevel.BgPalette;
                this.doorAndKeycard = readLevel.DoorAndKeycard;
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
                elevators = new Elevator[0];
                this.playerStartingPosition = default(Point);
                this.exitPosition = default(Point);
                this.bgPalette = 0;
                this.levelType = LevelType.Normal;
                this.scrollSpeed = 1;
            }

            this.CreateTileDictionaries();

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
                        this.transparentColor,
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
                        this.transparentColor,
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
                        this.transparentColor,
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
                        this.transparentColor,
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

            // Set elevators.
            this.elevatorsListBox.Items.Clear();
            this.elevatorsListBox.Items.AddRange(elevators);

            // Add/edit enemy dictionaries.
            this.enBitmapsSimple = this.enBitmaps.Keys.ToDictionary(k => k, k => this.enBitmaps[k][false]);
            this.enShooting = this.enConfig.Animations.ToDictionary(a => a.Name, a => a.Offsets.GunXOff >= 0);

            // Sprites.
            var sprites = new ChrLoader(spriteChr, this.palettes.SpritesPalette);

            // Player config.
            var playerConfig = SpriteConfig.Read(player, this.palettes);
            this.playerBitmap = playerConfig.Frames.First().GetPlayerBitmap(playerConfig, this.transparentColor, true, false, false, Constants.LevelEditorZoom, true);
            var playerBitmapJump = playerConfig.Frames.First(f => f.Name == "Jump").GetPlayerBitmap(playerConfig, this.transparentColor, true, false, false, 1, true);
            var playerMyBitmapJump = MyBitmap.FromBitmap(playerBitmapJump);
            var jetpack = sprites.GetSprite(Constants.JetpackSprite, Constants.JetpackPalette);
            var flames = sprites.GetSprite(Constants.FlamesSprite, Constants.FlamesPalette);
            playerMyBitmapJump.DrawImage(jetpack, Constants.JetpackXOff, Constants.JetpackYOff);
            playerMyBitmapJump.DrawImage(flames, Constants.FlamesXOff, Constants.FlamesYOff);
            this.playerJetpackBitmap = playerMyBitmapJump.Scale(Constants.LevelEditorZoom).ToBitmap(backgroundColor: this.transparentColor);

            // Elevators
            var elevator = sprites.GetSprite(Constants.ElevatorSprite, Constants.ElevatorPalette);
            var elevatorEndR = sprites.GetSprite(Constants.ElevatorEndRSprite, Constants.ElevatorPalette);
            var elevatorEndL = elevatorEndR.ReverseHorizontally();

            this.elevatorBitmaps = new Dictionary<int, Bitmap>();
            this.elevatorBitmapsTransparent = new Dictionary<int, Bitmap>();
            for (var i = Constants.MinElevatorSize; i <= Constants.MaxElevatorSize; i++)
            {
                var bmp = new MyBitmap(i * Constants.SpriteWidth, Constants.SpriteHeight);
                bmp.DrawImage(elevatorEndL, 0, 0);
                bmp.DrawImage(elevatorEndR, i * Constants.SpriteWidth - Constants.SpriteWidth, 0);

                for (var x = 1; x <= i - 2; x++)
                {
                    bmp.DrawImage(elevator, x * Constants.SpriteWidth, 0);
                }

                // crop height and scale
                bmp = bmp.Crop(bmp.Width, Constants.ElevatorHeight).Scale(Constants.LevelEditorZoom);

                this.elevatorBitmaps.Add(i, bmp.ToBitmap(backgroundColor: this.transparentColor));
                this.elevatorBitmapsTransparent.Add(i, bmp.ToBitmap(alpha: 50, backgroundColor: this.transparentColor));
            }

            // Door, keycard
            var keycard1 = sprites.GetSprite(Constants.KeycardSprite1, Constants.KeycardPalette);
            var keycard2 = sprites.GetSprite(Constants.KeycardSprite2, Constants.KeycardPalette);
            var door = sprites.GetSprite(Constants.DoorSprite, Constants.DoorPalette);
            var doorRev = door.ReverseHorizontally();

            var keycardBmp = new MyBitmap(Constants.KeycardWidth, Constants.KeycardHeight);
            keycardBmp.DrawImage(keycard1, 0, 0);
            keycardBmp.DrawImage(keycard2, Constants.SpriteWidth, 0);
            this.keycardBitmap = keycardBmp.Scale(Constants.LevelEditorZoom).ToBitmap(backgroundColor: this.transparentColor);

            var doorBmp = new MyBitmap(Constants.DoorWidth, Constants.DoorHeight);
            for (var y = 0; y < Constants.DoorHeight; y += Constants.SpriteHeight)
            {
                doorBmp.DrawImage(door, 0, y);
            }

            for (var y = 0; y < Constants.DoorHeight; y += Constants.SpriteHeight)
            {
                doorBmp.DrawImage(doorRev, Constants.SpriteWidth, y);
            }

            this.doorBitmap = doorBmp.Scale(Constants.LevelEditorZoom).ToBitmap(backgroundColor: this.transparentColor);

            this.SetLevel(newLevel);
            this.ClearHistory();
        }

        private void CreateTileDictionaries()
        {
            // Empty tile should always be the 1st one
            this.emptyTile = TileIds.PaletteTileId(0, this.bgConfig.Tiles[0].Id);

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
                    clone.UpdateColors(clone.UniqueColors(), this.palettes.BackgroundPalettes[this.bgPalette].Palettes[i].ActualColors);
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

            this.PopulateTiles();
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

                    var tableLayoutPanel = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = this.transparentColor };
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

            // Draw elevators.
            this.DrawElevators();

            // Draw door and keycard.
            this.DrawDoorAndKeycard();

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
                    //// todo: draw shooting?

                    var image = this.enBitmaps[enemy.Name][enemy.InitialFlip];
                    var imageTransparent = this.enBitmapsTransparent[enemy.Name][enemy.InitialFlip];

                    var rectangle = enemy.GetMovementRectangle();
                    var enemyMinX = rectangle.X;
                    var enemyMaxX = rectangle.X + rectangle.Width;
                    var enemyMinY = rectangle.Y;
                    var enemyMaxY = rectangle.Y + rectangle.Height;

                    this.DrawObject(
                        image,
                        imageTransparent,
                        this.EnemyMovementPen,
                        this.showEnemyMovementToolStripMenuItem.Checked,
                        enemy.MovementType,
                        enemy.Direction,
                        enemy.SpecialMovement,
                        enemy.X,
                        enemy.Y,
                        enemyMinX,
                        enemyMaxX,
                        enemyMinY,
                        enemyMaxY,
                        enemy == this.SelectedEnemy);
                }
            }
        }

        private void DrawElevators()
        {
            // this logic is a copy of DrawEnemies except for line color.
            // when one is update, update the other.

            if (this.showElevatorsToolStripMenuItem.Checked)
            {
                foreach (var elevator in this.Elevators)
                {
                    if (this.showElevatorCollisionsStripMenuItem.Checked)
                    {
                        var rectangle = elevator.PlayerRectangle;
                        var x1 = Math.Max(rectangle.X * Constants.LevelEditorZoom, 0);
                        var x2 = Math.Min(
                            (rectangle.X + rectangle.Width) * Constants.LevelEditorZoom,
                            this.level.Length * Constants.BackgroundTileWidth * Constants.LevelEditorZoom);
                        var y1 = rectangle.Y * Constants.LevelEditorZoom;
                        var y2 = (rectangle.Y + rectangle.Height) * Constants.LevelEditorZoom;

                        this.graphics.FillRectangle(
                            new SolidBrush(Color.FromArgb(100, Color.LightCoral)),
                            new Rectangle(
                                x1,
                                y1,
                                x2 - x1,
                                y2 - y1));
                    }

                    var image = this.elevatorBitmaps[elevator.Size];
                    var imageTransparent = this.elevatorBitmapsTransparent[elevator.Size];

                    this.DrawObject(
                        image,
                        imageTransparent,
                        this.ElevatorMovementPen,
                        this.showElevatorMovementToolStripMenuItem.Checked,
                        elevator.MovementType,
                        elevator.Direction,
                        SpecialMovement.None,
                        elevator.X,
                        elevator.Y,
                        elevator.MovementType == MovementType.Horizontal ? elevator.MinPosition : elevator.X,
                        elevator.MovementType == MovementType.Horizontal ? elevator.MaxPosition : elevator.X,
                        elevator.MovementType == MovementType.Vertical ? elevator.MinPosition : elevator.Y,
                        elevator.MovementType == MovementType.Vertical ? elevator.MaxPosition : elevator.Y,
                        elevator == this.SelectedElevator);
                }
            }
        }

        // todo: draw stop movement?
        private void DrawObject(
            Bitmap image,
            Bitmap imageTransparent,
            Pen pen,
            bool drawMovement,
            MovementType movementType,
            Direction direction,
            SpecialMovement specialMovement,
            int x,
            int y,
            int minX,
            int maxX,
            int minY,
            int maxY,
            bool selected)
        {
            if (drawMovement && movementType != MovementType.None)
            {
                if (specialMovement == SpecialMovement.Sinus8 || specialMovement == SpecialMovement.Sinus16)
                {
                    this.DrawSinusMovement(
                        imageTransparent,
                        pen,
                        movementType,
                        direction,
                        specialMovement,
                        x,
                        y,
                        movementType == MovementType.Horizontal ? minX : x, // for sinus movement, there's the 8/16 (x2) movement in the different 
                        movementType == MovementType.Horizontal ? maxX : x, // plane. But we don't want to draw the transparent images on the 
                        movementType == MovementType.Vertical ? minY : y,   // edges of the rectangle. so we have this logic of calculating
                        movementType == MovementType.Vertical ? maxY : y);  // min/max x/y here.
                }
                else if (specialMovement == SpecialMovement.Clockwise || specialMovement == SpecialMovement.CounterClockwise)
                {
                    this.DrawRectangleMovement(
                        imageTransparent,
                        pen,
                        movementType,
                        direction,
                        specialMovement,
                        x,
                        y,
                        minX,
                        maxX,
                        minY,
                        maxY);
                }
                else
                {
                    this.DrawNormalMovement(
                        imageTransparent,
                        pen,
                        movementType,
                        direction,
                        x,
                        y,
                        minX,
                        maxX,
                        minY,
                        maxY);
                }
            }

            // Draw regular image at the very end.
            this.graphics.DrawImage(image, new Point(x * Constants.LevelEditorZoom, y * Constants.LevelEditorZoom));

            if (selected)
            {
                // Draw red box around selected enemy.
                this.graphics.DrawRectangle(Pens.Red, x * Constants.LevelEditorZoom, y * Constants.LevelEditorZoom, image.Width, image.Height);
            }
        }

        private void DrawNormalMovement(
            Image imageTransparent,
            Pen pen,
            MovementType movementType,
            Direction direction,
            int x,
            int y,
            int minX,
            int maxX,
            int minY,
            int maxY)
        {
            var width = imageTransparent.Width;
            var height = imageTransparent.Height;

            var p1 = new Point(minX * Constants.LevelEditorZoom + width / 2, minY * Constants.LevelEditorZoom + height / 2);
            var p2 = new Point(maxX * Constants.LevelEditorZoom + width / 2, maxY * Constants.LevelEditorZoom + height / 2);

            this.graphics.DrawLine(pen, p1, p2);

            // Draw arrow.
            this.DrawArrow(
                pen,
                direction,
                (direction == Direction.Left || direction == Direction.Up) ? p1 : p2);

            if (minX != x || minY != y)
            {
                // Draw min. transparent image.
                this.graphics.DrawImage(imageTransparent, new Point(minX * Constants.LevelEditorZoom, minY * Constants.LevelEditorZoom));
            }

            if (maxX != x || maxY != y)
            {
                // Draw max. transparent image.
                this.graphics.DrawImage(imageTransparent, new Point(maxX * Constants.LevelEditorZoom, maxY * Constants.LevelEditorZoom));
            }
        }


        private void DrawSinusMovement(
            Image imageTransparent,
            Pen pen,
            MovementType movementType,
            Direction direction,
            SpecialMovement specialMovement,
            int x,
            int y,
            int minX,
            int maxX,
            int minY,
            int maxY)
        {
            var width = imageTransparent.Width;
            var height = imageTransparent.Height;

            var p1 = new Point(minX * Constants.LevelEditorZoom + width / 2, minY * Constants.LevelEditorZoom + height / 2);
            var p2 = new Point(maxX * Constants.LevelEditorZoom + width / 2, maxY * Constants.LevelEditorZoom + height / 2);

            var sinusLength = (specialMovement == SpecialMovement.Sinus8 ? 32 : 64) * Constants.LevelEditorZoom;
            var radius = sinusLength / 4; // so 8 * zoom or 16 * zoom
            var horizontal = movementType == MovementType.Horizontal;
            if (horizontal)
            {
                var distance = Math.Abs(p2.X - p1.X);
                var startPoint = new Point(p1.X, p1.Y);
                for (var i = 0; i < distance / sinusLength; i++)
                {
                    var points = new Point[]
                    {
                                startPoint,
                                new Point(startPoint.X + radius, startPoint.Y + (direction == Direction.Left ? -radius : radius)),
                                new Point(startPoint.X + radius * 2, startPoint.Y),
                                new Point(startPoint.X + 3 * radius, startPoint.Y + (direction == Direction.Left ? radius : -radius)),
                                new Point(startPoint.X + 4 * radius, startPoint.Y),
                    };

                    this.graphics.DrawCurve(pen, points);

                    startPoint = new Point(startPoint.X + 4 * radius, startPoint.Y);
                }
            }
            else // vertical
            {
                var distance = Math.Abs(p2.Y - p1.Y);
                var startPoint = new Point(p1.X, p1.Y);
                for (var i = 0; i < distance / sinusLength; i++)
                {
                    var points = new Point[]
                    {
                                startPoint,
                                new Point(startPoint.X + (direction == Direction.Up ? -radius : radius), startPoint.Y + radius),
                                new Point(startPoint.X, startPoint.Y + radius * 2),
                                new Point(startPoint.X + (direction == Direction.Up ? radius : -radius), startPoint.Y + radius * 3),
                                new Point(startPoint.X, startPoint.Y + radius * 4),
                    };

                    this.graphics.DrawCurve(pen, points);

                    startPoint = new Point(startPoint.X, startPoint.Y + radius * 4);
                }
            }

            // Draw arrow.
            this.DrawArrow(
                pen,
                direction,
                (direction == Direction.Left || direction == Direction.Up) ? p1 : p2);

            if (minX != x || minY != y)
            {
                // Draw min. transparent image.
                this.graphics.DrawImage(imageTransparent, new Point(minX * Constants.LevelEditorZoom, minY * Constants.LevelEditorZoom));
            }

            if (maxX != x || maxY != y)
            {
                // Draw max. transparent image.
                this.graphics.DrawImage(imageTransparent, new Point(maxX * Constants.LevelEditorZoom, maxY * Constants.LevelEditorZoom));
            }
        }

        private void DrawRectangleMovement(
            Image imageTransparent,
            Pen pen,
            MovementType movementType,
            Direction direction,
            SpecialMovement specialMovement,
            int x,
            int y,
            int minX,
            int maxX,
            int minY,
            int maxY)
        {
            var width = imageTransparent.Width;
            var height = imageTransparent.Height;

            // points in clockwise order
            var p1 = new Point(minX, minY);
            var p2 = new Point(maxX, minY);
            var p3 = new Point(maxX, maxY);
            var p4 = new Point(minX, maxY);
            var points = new[] { p1, p2, p3, p4 };
            var scaledPoints = points.Select(
                p => new Point(p.X * Constants.LevelEditorZoom + width / 2, p.Y * Constants.LevelEditorZoom + height / 2)).ToArray();

            var directions = specialMovement == SpecialMovement.Clockwise ?
                new[] { Direction.Up, Direction.Right, Direction.Down, Direction.Left } :
                new[] { Direction.Left, Direction.Up, Direction.Right, Direction.Down };

            // draw all lines
            for (var i = 0; i < scaledPoints.Length; i++)
            {
                this.graphics.DrawLine(pen, scaledPoints[i], scaledPoints[(i + 1) % scaledPoints.Length]);
                this.DrawArrow(pen, directions[i], scaledPoints[i]);
            }

            // draw transparent images
            foreach (var point in points)
            {
                if (point.X != x || point.Y != y)
                {
                    // Draw min. transparent image.
                    this.graphics.DrawImage(imageTransparent, new Point(point.X * Constants.LevelEditorZoom, point.Y * Constants.LevelEditorZoom));
                }
            }
        }

        private void DrawArrow(Pen pen, Direction dir, Point p)
        {
            switch (dir)
            {
                case Direction.Left:
                    this.graphics.DrawPolygon(pen, new[] { p, new Point(p.X + 3, p.Y + 3), new Point(p.X + 3, p.Y - 3) });
                    break;
                case Direction.Right:
                    this.graphics.DrawPolygon(pen, new[] { p, new Point(p.X - 3, p.Y + 3), new Point(p.X - 3, p.Y - 3) });
                    break;
                case Direction.Up:
                    this.graphics.DrawPolygon(pen, new[] { p, new Point(p.X - 3, p.Y + 3), new Point(p.X + 3, p.Y + 3) });
                    break;
                case Direction.Down:
                    this.graphics.DrawPolygon(pen, new[] { p, new Point(p.X - 3, p.Y - 3), new Point(p.X + 3, p.Y - 3) });
                    break;
            }
        }

        private void DrawDoorAndKeycard()
        {
            if (this.showDoorAndKeycardToolStripMenuItem.Checked && this.doorAndKeycard != null && this.doorAndKeycard.DoorExists)
            {
                this.graphics.DrawImage(this.doorBitmap, new Point(this.doorAndKeycard.DoorX * Constants.LevelEditorZoom, this.doorAndKeycard.DoorY * Constants.LevelEditorZoom));
                this.graphics.DrawImage(this.keycardBitmap, new Point(this.doorAndKeycard.KeycardX * Constants.LevelEditorZoom, this.doorAndKeycard.KeycardY * Constants.LevelEditorZoom));
            }
        }

        private void DrawPlayerAndExit()
        {
            if (this.showPlayerToolStripMenuItem.Checked)
            {
                if (this.levelType == LevelType.Normal || this.levelType == LevelType.Boss)
                {
                    this.graphics.DrawImage(
                        this.playerBitmap,
                        new Point(
                            (this.playerStartingPosition.X - Constants.PlayerXOffset) * Constants.LevelEditorZoom,
                            (this.playerStartingPosition.Y - Constants.PlayerYOffset) * Constants.LevelEditorZoom));
                }
                else if (this.levelType == LevelType.Jetpack)
                {
                    this.graphics.DrawImage(
                        this.playerJetpackBitmap,
                        new Point(
                            (this.playerStartingPosition.X - Constants.PlayerXOffset) * Constants.LevelEditorZoom,
                            (this.playerStartingPosition.Y - Constants.PlayerYOffset) * Constants.LevelEditorZoom));
                }


                if (this.levelType == LevelType.Normal)
                {
                    this.graphics.FillRectangle(
                        new SolidBrush(Color.FromArgb(150, Color.LightCoral)),
                        this.exitPosition.X * Constants.LevelEditorZoom,
                        this.exitPosition.Y * Constants.LevelEditorZoom,
                        Constants.ExitWidth * Constants.LevelEditorZoom,
                        Constants.ExitHeight * Constants.LevelEditorZoom);

                    this.graphics.DrawRectangle(
                        Pens.Red,
                        this.exitPosition.X * Constants.LevelEditorZoom,
                        this.exitPosition.Y * Constants.LevelEditorZoom,
                        Constants.ExitWidth * Constants.LevelEditorZoom,
                        Constants.ExitHeight * Constants.LevelEditorZoom);

                    this.graphics.DrawRectangle(
                        Pens.Red,
                        this.exitPosition.X * Constants.LevelEditorZoom + 1,
                        this.exitPosition.Y * Constants.LevelEditorZoom + 1,
                        Constants.ExitWidth * Constants.LevelEditorZoom - 2,
                        Constants.ExitHeight * Constants.LevelEditorZoom - 2);
                }
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

        private void ShowElevatorsToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void ShowElevatorsToolStripMenuItemCheckedChanged(object sender, EventArgs e)
        {
            this.showElevatorMovementToolStripMenuItem.Enabled = this.showElevatorsToolStripMenuItem.Checked;
            this.showElevatorCollisionsStripMenuItem.Enabled = this.showElevatorsToolStripMenuItem.Checked;
        }

        private void ShowElevatorMovementToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void ShowElevatorCollisionsStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void ShowDoorAndKeycardToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.UpdateBitmap();
            this.hideNonBgToolStringMenuItem.Checked = this.OnlyBackgroundShown;
        }

        private void ShowPlayerToolStripMenuItemClick(object sender, EventArgs e)
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
                    this.LoadLevel(loadLevelDialog.Level, loadLevelDialog.BgSpec, loadLevelDialog.EnSpec, loadLevelDialog.Palettes, loadLevelDialog.Player, loadLevelDialog.SpriteChr);
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
                    this.LoadLevel(null, loadLevelDialog.BgSpec, loadLevelDialog.EnSpec, loadLevelDialog.Palettes, loadLevelDialog.Player, loadLevelDialog.SpriteChr);
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

            if (!this.ValidateAllElevators())
            {
                return;
            }

            var saveFileDialog = new SaveFileDialog { InitialDirectory = Defaults.Instance.LevelsDefaultDir, Filter = "xml files (*.xml)|*.xml" };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                var level = new Level
                {
                    Tiles = this.level,
                    BgPalette = this.bgPalette,
                    Enemies = this.Enemies,
                    Elevators = this.Elevators,
                    PlayerStartingPosition = this.playerStartingPosition,
                    ExitPosition = this.exitPosition,
                    DoorAndKeycard = this.doorAndKeycard,
                    LevelType = this.levelType,
                    ScrollSpeed = this.scrollSpeed,
                };

                level.Write(saveFileDialog.FileName);
            }
        }

        private void ExportLevelToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!this.ValidateAllEnemies())
            {
                return;
            }

            if (!this.ValidateAllElevators())
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
            Func<EditLevelDialog, Tuple<int, int, LevelType, Point, Point, double>> getResultFunc =
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

                    if (levelWidth < 32 | levelWidth > 252)
                    {
                        MessageBox.Show("Wrongh width: min is 32, max is 252");
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

                    var clippedElevators = this.Elevators.Where(el => el.X > levelWidth * Constants.BackgroundTileWidth).Select(el => el.ToString()).ToArray();
                    if (clippedElevators.Any())
                    {
                        MessageBox.Show($"Elevators\r\n{string.Join("\r\n", clippedElevators)}\r\n would be removed from the screen");
                        return null;
                    }

                    var levelType = dialog.LevelType;
                    var scrollSpeed = dialog.ScrollSpeed;

                    return Tuple.Create(levelWidth, dialog.BgPalette, levelType, playerStartingPosition, exitPosition, scrollSpeed);
                };

            var editLevelDialog = new EditLevelDialog(
                this.level.Length,
                this.palettes.BackgroundPalettes.Length,
                this.bgPalette,
                this.playerStartingPosition,
                this.levelType,
                this.exitPosition,
                this.scrollSpeed,
                dialog => getResultFunc(dialog) != null);

            editLevelDialog.ShowDialog();
            if (!editLevelDialog.Succeeded)
            {
                return;
            }

            var result = getResultFunc(editLevelDialog);


            var widthChanged = false;
            widthChanged = this.ChangeWidth(result.Item1);

            var paletteChanged = false;
            if (this.bgPalette != result.Item2)
            {
                paletteChanged = true;
                this.bgPalette = result.Item2;
                this.CreateTileDictionaries();
            }

            var levelTypeChanged = false;
            if (this.levelType != result.Item3)
            {
                this.levelType = result.Item3;
                levelTypeChanged = true;
            }

            var playerStartingPositionChanged = false;
            if (this.playerStartingPosition != result.Item4)
            {
                this.playerStartingPosition = result.Item4;
                playerStartingPositionChanged = true;
            }

            var exitPositionChanged = false;
            if (this.exitPosition != result.Item5)
            {
                this.exitPosition = result.Item5;
                exitPositionChanged = true;
            }

            this.scrollSpeed = result.Item6; // doesn't affect rendering

            // Only update bitmap if 
            //  1) something affecting rednering has changed, and 
            //  2) width hasn't changed - ChangeWidth updates the bitmap if that's the case
            if (!widthChanged && (paletteChanged || playerStartingPositionChanged || levelTypeChanged || exitPositionChanged))
            {
                this.UpdateBitmap();
            }

            // POI history: store stuff in history
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
            if (!ValidateExport())
            {
                return;
            }

            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            var bytes = GetExportPayload(writer);            
            writer.WriteLineIfNotNull("Total size: {0} bytes", bytes.Length);
            MessageBox.Show(builder.ToString(), "Export data", MessageBoxButtons.OK);
        }

        private void DiagnosticsToolStripMenuItemClick(object sender, EventArgs eventArgs)
        {
            // POI diagnostics - this is an untested beta kind of

            // Same logic as export enemies/elevators.
            var width = this.level.Length;
            var numberOfScreens = (width / Constants.ScreenWidthInTiles) + 1;

            var stringBuilder = new StringBuilder();

            for (var screen = 0; screen < numberOfScreens; screen++)
            {
                var enemies = this.Enemies.Where(e => e.Screen == screen || e.Screen == screen + 1);
                var elevators = this.Elevators.Where(e => e.Screen == screen || e.Screen == screen + 1);

                var enemySprites = 0;
                var elevatorSprites = 0;

                for (var i = 0; i < Constants.ScreenWidth - 1; i++)
                {
                    var enemiesOnScreen = enemies.Where(e =>
                    {
                        int x1;
                        switch (e.MovementType)
                        {
                            case MovementType.Horizontal:
                                x1 = e.MinPosition;
                                break;
                            default:
                                x1 = e.X;
                                break;
                        }

                        var x2 = x1 + e.Width;

                        return x1 <= i + Constants.ScreenWidth - 1 && x2 >= i;
                    });

                    var elevatorsOnScreen = elevators.Where(e => e.X <= i + Constants.ScreenWidth && e.X + e.Size + Constants.SpriteWidth >= i);

                    enemySprites = Math.Max(enemySprites, enemiesOnScreen.Sum(e => e.Sprites));
                    elevatorSprites = Math.Max(elevatorSprites, elevatorsOnScreen.Sum(e => e.Size));
                }                              

                var totalSprites = Constants.PlayerSprites + Constants.MaxBullets + enemySprites + elevatorSprites;

                stringBuilder.AppendLineFormat("Screens {0}/{1}: max sprite count: {2}", screen, screen + 1, totalSprites);
                if (totalSprites > Constants.SpritesPerFrame)
                {
                    stringBuilder.AppendLine("  WARNING - possibly too many sprites");
                }
            }

            MessageBox.Show(stringBuilder.ToString());
        }

        private bool OnlyBackgroundShown
        {
            get
            {
                return !(this.showScreensToolStripMenuItem.Checked ||
                         this.showEnemiesToolStripMenuItem.Checked ||
                         this.showEnemyMovementToolStripMenuItem.Checked ||
                         this.showElevatorsToolStripMenuItem.Checked ||
                         this.showElevatorMovementToolStripMenuItem.Checked ||
                         this.showElevatorCollisionsStripMenuItem.Checked ||
                         this.showDoorAndKeycardToolStripMenuItem.Checked ||
                         this.showPlayerToolStripMenuItem.Checked);
            }

            set
            { 
                if (value)
                {
                    this.showScreensToolStripMenuItem.Checked = false;
                    this.showEnemiesToolStripMenuItem.Checked = false;
                    this.showEnemyMovementToolStripMenuItem.Checked = false;
                    this.showElevatorsToolStripMenuItem.Checked = false;
                    this.showElevatorMovementToolStripMenuItem.Checked = false;
                    this.showElevatorCollisionsStripMenuItem.Checked = false;
                    this.showDoorAndKeycardToolStripMenuItem.Checked = false;
                    this.showPlayerToolStripMenuItem.Checked = false;                    
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
            else if (this.showEnemiesToolStripMenuItem.Checked || this.showElevatorsToolStripMenuItem.Checked)
            {
                // If control pressed, if enemies or elevators are shown, select an ememy or elevator and (on right click) open edit window.
                var position = MouseGamePosition(e.X, e.Y);

                if (this.showEnemiesToolStripMenuItem.Checked)
                {
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

                if (this.showElevatorsToolStripMenuItem.Checked)
                {
                    var clickedElevator = this.Elevators.FirstOrDefault(el => el.Select(position));
                    if (clickedElevator != null)
                    {
                        var left = e.Button.HasFlag(MouseButtons.Left);
                        var right = e.Button.HasFlag(MouseButtons.Right);
                        if (left || right)
                        {
                            this.mainTabControl.SelectedTab = elTabPage;
                            this.elevatorsListBox.Focus();
                            this.elevatorsListBox.SelectedItem = clickedElevator;

                            if (right)
                            {
                                this.EditElevator();
                            }
                        }
                    }
                    else
                    {
                        this.elevatorsListBox.SelectedItem = null;
                    }
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
        //// POI history stuff
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

        private bool ValidateExport()
        {
            string error = null;

            if (this.levelType == LevelType.Boss && this.Enemies.Length > 8)
            {
                error = "There can be up to 8 enemies on a boss level";
            }

            if (error != null)
            {
                MessageBox.Show(error, "Export error", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void Export(string fileName)
        {
            if (!ValidateExport())
            {
                return;
            }

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

            // Elevators.
            result.AddRange(GetExportElevatorsData(logger));
            logger.WriteLineIfNotNull();

            // Door and keycard.
            result.AddRange(GetExportDoorAndKeycardData(logger));
            logger.WriteLineIfNotNull();

            // Bg Palette.
            result.AddRange(GetBgPalette(logger));
            logger.WriteLineIfNotNull();

            // Starting position
            result.AddRange(GetStartingPosition(logger));
            logger.WriteLineIfNotNull();

            // Level type data.
            result.AddRange(GetExportLevelTypeData(logger));
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
            // - enemies in level data the following format:
            //   - pointer to next screen (from here): (n x 22) + 3 (1 byte)
            //   - number of enemies (1 byte)
            //   - n times the enemy data (22 bytes)
            //        - 1st byte of id - pointer to the right variable (1 byte)
            //        - 2nd byte of id - a mask in the right variable (1 byte) 
            //        - slot to put enemy in (1 byte)
            //        - pointer to const. data (low) (1 byte)
            //        - pointer to const. data (high) (1 byte)
            //        - screen the enemy is on (1 byte)
            //        - should flip (1 byte)
            //        - movement speed (1 byte)
            //        - special movement type (1 byte)
            //        - max movement distance (1 byte)
            //        - initial flip (1 byte)
            //        - initial movement direction (1 byte)
            //        - initial movement left (1 byte)            
            //        - initial special movement var (1 byte)
            //        - x position (1 byte)
            //        - y position (1 byte)            
            //        - blinking type (1 byte)
            //        - blinking frequency initial (1 byte)
            //        - blinking frequency  (1 byte)
            //        - initial life (1 byte)
            //        - shooting frequency initial (1 byte)
            //        - shooting frequency (1 byte)
            //   - pointer to the previous screen (from here): (n x 22) + 2 (1 byte)
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

                    // id (two bytes)
                    var idFirstByte = (byte)(enemyId / 8);
                    var idSecondByte = (byte)(1 << (enemyId % 8));

                    result.Add(idFirstByte);      
                    result.Add(idSecondByte);
                    enemyId++;

                    // slot to put enemy in (1 byte)
                    result.Add((byte)(slot * Constants.EnemyInMemorySize));

                    // pointer to const. data (low byte and high byte)
                    var constsPointer = ((animation.Id * Constants.EnemyDefinitionSize) + Constants.EnemyConstsLow) + (Constants.EnemyConstsHigh * 256);
                    result.Add((byte)(constsPointer % 256));
                    result.Add((byte)(constsPointer / 256));

                    // screen
                    result.Add((byte)screen);

                    // should flip
                    result.Add((byte)(enemy.ShouldFlip ? 1 : 0));

                    // movement speed
                    result.Add((byte)enemy.Speed);

                    // special movement type
                    result.Add((byte)enemy.SpecialMovement);

                    // max movement distance
                    result.Add((byte)(enemy.MovementRange));

                    // initial flip
                    result.Add((byte)(enemy.InitialFlip ? 1 : 0));

                    // initial movement direction
                    result.Add((byte)enemy.Direction);

                    // initial movement distance
                    result.Add((byte)(enemy.InitialDistanceLeft));

                    // initial special movement var
                    result.Add((byte)(enemy.InitialSpecialMovementVar));

                    // x position
                    result.Add((byte)enemy.X);

                    // y position
                    result.Add((byte)enemy.Y);

                    // blinking type
                    result.Add((byte)enemy.BlinkingType);

                    // blinking frequency initial
                    result.Add((byte)enemy.BlinkingInitialFrequency);

                    // blinking frequency
                    result.Add((byte)enemy.BlinkingFrequency);

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

        private byte[] GetExportElevatorsData(TextWriter logger = null)
        {
            //
            // - elevators in the following format:
            //   - pointer to next screen (from here): (n x 10) + 3 (1 byte)
            //   - number of elevators (1 byte)
            //   - n times the elevator data (10 bytes)
            //        - slot to put elevator in (1 byte)
            //        - elevator size (1 byte)
            //        - screen the elevator is on (1 byte)            
            //        - movement speed (1 byte)
            //        - max movement distance (1 byte)
            //        - movement type (1 byte)            
            //        - (initial) movement left (1 byte)
            //        - (initial) movement direction (1 byte)            
            //        - y position (1 byte)
            //        - x position (1 byte) (y comes before x!)
            //   - pointer to the previous screen (from here): (n x 10) + 2 (1 byte)
            //

            var result = new List<byte>();

            // same logic as export enemies
            var width = this.level.Length;
            var numberOfScreens = (width / Constants.ScreenWidthInTiles) + 1;

            var dataPerScreen = new Dictionary<int, byte[]>();

            // Available slots. -1 means empty, n (>0) means taken for elevator from the nth screen.
            var slots = new int[Constants.ElevatorsLimitPerTwoScreens];
            for (var i = 0; i < Constants.ElevatorsLimitPerTwoScreens; i++)
            {
                slots[i] = -1;
            }

            for (var screen = 0; screen < numberOfScreens; screen++)
            {
                // Clear the slots for two screens ago.
                for (var slot = 0; slot < Constants.ElevatorsLimitPerTwoScreens; slot++)
                {
                    if (slots[slot] == screen - 2)
                    {
                        slots[slot] = -1;
                    }
                }

                var elevators = this.Elevators.Where(e => e.Screen == screen).ToArray();
                var n = (byte)elevators.Length;

                // Pointer to the next screen
                result.Add((byte)(n * Constants.ElevatorInLevelDataSize + 3));

                // Number of elevators.
                result.Add(n);

                // Elevator data.
                foreach (var elevator in elevators)
                {
                    int slot;
                    for (slot = 0; slot < Constants.ElevatorsLimitPerTwoScreens; slot++)
                    {
                        if (slots[slot] == -1)
                        {
                            slots[slot] = screen;
                            break;
                        }
                    }

                    if (slot == Constants.ElevatorsLimitPerTwoScreens)
                    {
                        // Sanity check.
                        throw new Exception();
                    }

                    //
                    // Set the data.
                    //                   

                    // slot to put elevator in (1 byte)
                    result.Add((byte)(slot * Constants.ElevatorInMemorySize));

                    // size
                    result.Add((byte)elevator.Size);

                    // screen
                    result.Add((byte)screen);

                    // movement speed
                    result.Add((byte)elevator.Speed);                    

                    // max movement distance
                    result.Add((byte)(elevator.MovementRange));

                    // initial movement distance
                    result.Add((byte)(elevator.InitialDistanceLeft));

                    // initial movement direction
                    result.Add((byte)(elevator.Direction));

                    // y position
                    result.Add((byte)elevator.Y);

                    // x position
                    result.Add((byte)elevator.X);

                }

                // Pointer to the previous screen
                result.Add((byte)(n * Constants.ElevatorInLevelDataSize + 2));
            }

            logger.WriteLineIfNotNull("Total bytes for Elevator data: {0}", result.Count);
            return result.ToArray();
        }

        private byte[] GetExportDoorAndKeycardData(TextWriter logger = null)
        {
            //
            // - data in the following format:
            //   - 1 byte: door exists
            //   - 1 byte: door Screen
            //   - 1 byte: door X
            //   - 1 byte: door Y
            //   - 1 byte: keycard Screen
            //   - 1 byte: keycard X
            //   - 1 byte: keycard Y
            //

            var result = new List<byte>();

            if (this.doorAndKeycard != null)
            {
                result.Add(this.doorAndKeycard.DoorExists ? (byte)1 : (byte)0);
                result.Add((byte)(this.doorAndKeycard.DoorX / Constants.ScreenWidth));
                result.Add((byte)(this.doorAndKeycard.DoorX % Constants.ScreenWidth));
                result.Add((byte)this.doorAndKeycard.DoorY);
                result.Add((byte)(this.doorAndKeycard.KeycardX / Constants.ScreenWidth));
                result.Add((byte)(this.doorAndKeycard.KeycardX % Constants.ScreenWidth));
                result.Add((byte)this.doorAndKeycard.KeycardY);
            }
            else
            {
                for (var i = 0; i < 7; i++)
                {
                    result.Add(0);
                }
            }

            logger.WriteLineIfNotNull("Total bytes for Door and Keycard data: {0}", result.Count);
            
            return result.ToArray();
        }

        private byte[] GetBgPalette(TextWriter logger = null)
        {
            // Bg palette offset, 1 byte.
            var paletteOffset = this.bgPalette * Constants.PaletteSize;
            var result = new byte[] { (byte)paletteOffset };
            logger.WriteLineIfNotNull("Total bytes for Bg Palette: {0}", result.Length);
            return result;

        }

        private byte[] GetStartingPosition(TextWriter logger = null)
        {
            //
            // - data in the following format:
            //   - 2 bytes: player position (x/y, always on screen 0)
            //

            var result = new List<byte>();

            result.Add((byte)this.playerStartingPosition.X);
            result.Add((byte)this.playerStartingPosition.Y);
            
            logger.WriteLineIfNotNull("Total bytes for Player data: {0}", result.Count);
            return result.ToArray();
        }

        private byte[] GetExportLevelTypeData(TextWriter logger = null)
        {
            //
            // - data in the following format:
            //   - 1st byte: level type
            //   - next 3 bytes: depends on level type:
            //     - normal: exit position (screen/x/y)
            //     - jetpack: scroll speed (1 byte, 2 bytes are not important)
            //     - boss: 1st byte must be 0, 2nd byte is victory condition, 3rd byte is not important

            var result = new List<byte>();
            result.Add((byte)this.levelType);

            if (this.levelType == LevelType.Normal)
            {
                result.Add((byte)(this.exitPosition.X / 256));
                result.Add((byte)(this.exitPosition.X % 256));
                result.Add((byte)this.exitPosition.Y);
            }
            else if (this.levelType == LevelType.Jetpack)
            {
                byte speed;
                if (this.scrollSpeed == 0.25)
                {
                    speed = 254;
                }
                else if (this.scrollSpeed == 0.5)
                {
                    speed = 255;
                }
                else
                {
                    speed = (byte)this.scrollSpeed;
                }

                result.Add(speed);
                result.Add(0);
                result.Add(0);
            }
            else if (this.levelType == LevelType.Boss)
            {
                result.Add(0);

                // todo 0001 - for now assume all enemies must be destroyed. Later add some tagging for bosses.
                // This logic is the same as enemies export.
                var resultVar = 0;
                var enemyId = 0;
                var width = this.level.Length;
                var numberOfScreens = (width / Constants.ScreenWidthInTiles) + 1;
                for (var screen = 0; screen < numberOfScreens; screen++)
                {
                    var enemies = this.Enemies.Where(e => e.Screen == screen).ToArray();
                    foreach (var enemy in enemies)
                    {
                        var idSecondByte = (byte)(1 << (enemyId % 8));
                        enemyId++;

                        if (enemy.Animation.MaxHealth > 0)
                        {
                            // Skip indestructible enemies.
                            resultVar |= idSecondByte;
                        }
                    }
                }

                result.Add((byte)resultVar);
                result.Add(0);
            }

            logger.WriteLineIfNotNull("Total bytes for Exit data: {0}", result.Count);
            return result.ToArray();
        }

        #endregion

        #region Door

        private void EditDoorAndKeycardToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.EditDoor();
        }

        private void EditDoor()
        {
            // POI history: save history in this method
            
            // Show the dialog.
            var dialog = new EditDoorAndKeycardDialog(this.doorAndKeycard, ValidateDoorAndKeycard);
            dialog.ShowDialog();
            if (!dialog.Succeeded)
            {
                return;
            }

            this.doorAndKeycard = new DoorAndKeycard();
            this.doorAndKeycard.DoorExists = dialog.DoorExists;
            int doorX, doorY, keycardX, keycardY;
            dialog.TryGetDoorX(out doorX);
            dialog.TryGetDoorY(out doorY);
            dialog.TryGetKeycardX(out keycardX);
            dialog.TryGetKeycardY(out keycardY);
            this.doorAndKeycard.DoorX = doorX;
            this.doorAndKeycard.DoorY = doorY;
            this.doorAndKeycard.KeycardX = keycardX;
            this.doorAndKeycard.KeycardY = keycardY;
            this.UpdateBitmap();
        }

        private string ValidateDoorAndKeycard(EditDoorAndKeycardDialog dialog)
        {
            int doorX, doorY, keycardX, keycardY;

            if (!dialog.TryGetDoorX(out doorX))
            {
                return "DoorX invalid";
            }

            if (!dialog.TryGetDoorY(out doorY))
            {
                return "DoorY invalid";
            }

            if (!dialog.TryGetKeycardX(out keycardX))
            {
                return "KeycardX invalid";
            }

            if (!dialog.TryGetKeycardY(out keycardY))
            {
                return "KeycardY invalid";
            }

            if (doorX % Constants.BackgroundTileWidth != 0)
            {
                return "DoorX not multiple of tile width";
            }

            if (doorY % Constants.BackgroundTileHeight != 0)
            {
                return "DoorY not multiple of tile height";
            }

            if (doorX > this.level.Length * Constants.BackgroundTileWidth)
            {
                return "DoorX is too high";
            }

            if (doorX < 0)
            {
                return "DoorX cannot be negative";
            }

            if (keycardX > this.level.Length * Constants.BackgroundTileWidth)
            {
                return "KeycardX is too high";
            }

            if (keycardX < 0)
            {
                return "KeycardX cannot be negative";
            }

            if (doorY < 0)
            {
                return "DoorY cannot be negative";
            }

            if (doorY > Constants.ScreenHeight)
            {
                return "DoorY is too high";
            }

            if (keycardY < 0)
            {
                return "KeycardY cannot be negative";
            }

            if (keycardY > Constants.ScreenHeight)
            {
                return "KeycardY is too high";
            }

            return null;
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
            // POI history: save history in this method

            // Show the dialog.
            var dialog = new AddEditEnemyDialog(selectedEnemy, this.enBitmapsSimple, this.enShooting, this.transparentColor, this.ValidateEnemy);
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

            // Enable/disable the add button as needed.
            this.addEnemyButton.Enabled = this.Enemies.Length < Constants.EnemiesLimitPerLevel;

            // Update bitmap.
            this.UpdateBitmap();
        }

        private void DeleteEnemy()
        {
            // POI history: save history in this method

            // Remove the enemy.
            this.enemiesListBox.Items.Remove(this.SelectedEnemy);

            // Enable/disable the add button as needed.
            this.addEnemyButton.Enabled = this.Enemies.Length < Constants.EnemiesLimitPerLevel;

            // Update bitmap.
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

        #endregion

        #region Elevators

        // Basically copy of enemies, don't change anything here unless it's also changed in enemies.

        public Elevator[] Elevators => this.elevatorsListBox.Items.Cast<Elevator>().ToArray();

        public Elevator SelectedElevator => this.elevatorsListBox.SelectedItem as Elevator;

        private void ElevatorsListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable/disable buttons.
            this.deleteElevatorButton.Enabled = this.SelectedElevator != null;
            this.editElevatorButton.Enabled = this.SelectedElevator != null;
            this.findElevatorButton.Enabled = this.SelectedElevator != null;

            // Update bitmap (highlight elevator).
            this.UpdateBitmap();
        }

        private void ElevatorsListBoxMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Right))
            {
                var index = this.elevatorsListBox.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    var item = this.elevatorsListBox.Items[index];
                    this.elevatorsListBox.SelectedItem = item;

                    // On right click edit the enemy.
                    this.EditElevator();
                }
            }
        }

        private void ElevatorsListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var index = this.elevatorsListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var item = this.elevatorsListBox.Items[index];
                if (item == this.SelectedElevator)
                {
                    this.FindElevator();
                }
            }
        }

        private void ElevatorsListBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && this.SelectedElevator != null)
            {
                this.DeleteElevator();
            }
        }

        private void AddElevatorButtonClick(object sender, EventArgs e)
        {
            this.AddElevator();
        }

        private void DeleteElevatorButtonClick(object sender, EventArgs e)
        {
            this.DeleteElevator();
        }

        private void EditElevatorButtonClick(object sender, EventArgs e)
        {
            this.EditElevator();
        }

        private void FindElevatorButtonClick(object sender, EventArgs e)
        {
            this.FindElevator();
        }

        private void FindElevator()
        {
            if (this.SelectedElevator == null)
            {
                // No elevator selected.
                return;
            }

            if (!this.scrollBar.Enabled)
            {
                // Level too small, no scroll.
                return;
            }

            // Get elevator's center in metatiles.
            var elevatorRectangle = this.SelectedElevator.GetGameRectangleInMetaTiles();
            var x1 = elevatorRectangle.X;
            var x2 = elevatorRectangle.X + elevatorRectangle.Width;
            var center = (x1 + x2) / 2;

            // Outer panel width in metatiles.
            var outerOuterPanelWidth = this.outerOuterDrawPanel.Width;
            var outerOuterPanelWidthInMetaTiles = outerOuterPanelWidth / TileWidth;

            // TARGET SCROLL + OUTER PANEL WIDTH / 2 = ELEVATOR CENTER
            // TARGET SCROLL = ELEVATOR CENTER - OUTER PANEL WIDTH / 2

            // Calculate target scroll.
            var targetScroll = Math.Max(this.scrollBar.Minimum, Math.Min(this.scrollBar.Maximum, center - outerOuterPanelWidthInMetaTiles / 2));
            this.scrollBar.Value = targetScroll;
            this.UpdateScroll();
        }

        private void AddElevator()
        {            
            this.AddOrEditElevator(null);
        }

        private void EditElevator()
        {
            this.AddOrEditElevator(this.SelectedElevator);
        }

        private void AddOrEditElevator(Elevator selectedElevator)
        {
            // POI history: save history in this method

            // Show the dialog.
            var dialog = new AddEditElevatorDialog(selectedElevator, this.ValidateElevator);
            dialog.ShowDialog();
            if (!dialog.Succeeded)
            {
                return;
            }
            
            // Get the new elevator.
            Elevator newElevator;
            if (!dialog.TryGetElevator(out newElevator))
            {
                MessageBox.Show("Something went wrong.");
                return;
            }
            
            // Remove old elevator from the list if given.
            if (selectedElevator != null)
            {
                this.elevatorsListBox.Items.Remove(selectedElevator);
            }
            
            // Add the new elevator to the list and select it.
            this.elevatorsListBox.Items.Add(newElevator);
            this.elevatorsListBox.SelectedItem = newElevator;
            
            // Sort the list.
            this.SortElevatorsListBox();
            
            // Update bitmap.
            this.UpdateBitmap();
        }

        private void DeleteElevator()
        {
            // POI history: save history in this method

            this.elevatorsListBox.Items.Remove(this.SelectedElevator);
            this.UpdateBitmap();
        }

        private void SortElevatorsListBox()
        {
            // optimize this?
            // Sort by X. Keep the same item selected.
            var selectedItem = this.SelectedElevator;
            var elevators = this.elevatorsListBox.Items.Cast<Elevator>().OrderBy(e => e.X).ToArray();
            this.elevatorsListBox.Items.Clear();
            this.elevatorsListBox.Items.AddRange(elevators);
            if (selectedItem != null)
            {
                this.elevatorsListBox.SelectedItem = selectedItem;
            }
        }

        #endregion

        #region ValidationFunctions

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
            if (!dialog.TryGetMax(out max))
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
            if (this.Enemies.Count(e => e != existingEnemy) + 1 > Constants.EnemiesLimitPerLevel)
            {
                return "Too many enemies in the level";
            }

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

            var blinking = this.ValidateEnemyBlinking(enemy);
            if (blinking != null)
            {
                return blinking;
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

            if (enemy.X + enemy.Width > (enemy.Screen + 1) * Constants.ScreenWidth)
            {
                return "Enemy is spanning across screens";
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

                if (enemy.Direction != Direction.None)
                {
                    return "Direction must be none for non-moving enemies";
                }

                if (enemy.SpecialMovement != SpecialMovement.None)
                {
                    return "Non-moving enemies cannot have special movement";
                }

                return null;
            }

            if (enemy.Direction == Direction.None)
            {
                return "Direction cannot be none";
            }

            if (enemy.MovementType == MovementType.Vertical && (enemy.Direction == Direction.Left || enemy.Direction == Direction.Right) ||
                enemy.MovementType == MovementType.Horizontal && (enemy.Direction == Direction.Up || enemy.Direction == Direction.Down))
            {
                return "Invalid direction for given movement type";
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

            var rectangle = enemy.GetMovementRectangle();
            var enemyMinX = rectangle.X;
            var enemyMaxX = rectangle.X + rectangle.Width;
            var enemyMinY = rectangle.Y;
            var enemyMaxY = rectangle.Y + rectangle.Height;

            if (enemyMinX < enemy.Screen * Constants.ScreenWidth)
            {
                return "Enemy walking off-screen to the left";
            }
            
            if (enemyMaxX + enemy.Width > Math.Min((enemy.Screen + 1) * Constants.ScreenWidth, this.level.Length * Constants.BackgroundTileWidth))
            {
                return "Enemy walking off-screen to the right";
            }
            
            if (enemyMinY < 0)
            {
                return "Enemy walking off-screen on the top";
            }
            
            if (enemyMaxY > Constants.ScreenHeight)
            {
                return "Enemy walking off-screen on the bottom";
            }

            int enemyPosition;
            string enemyPositionString;
            if (enemy.MovementType == MovementType.Horizontal)
            {
                enemyPosition = enemy.X;
                enemyPositionString = "X";
            }
            else if (enemy.MovementType == MovementType.Vertical)
            {
                enemyPosition = enemy.Y;
                enemyPositionString = "Y";
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

            if (enemy.Speed != 254 && enemy.Speed != 255) // special values for 1/4 and 1/2 - everything is reachable with those
            {
                if ((enemyPosition - enemy.MinPosition) % enemy.Speed != 0)
                {
                    return "Min. position is not reachable with given speed";
                }

                if ((enemy.MaxPosition - enemyPosition) % enemy.Speed != 0)
                {
                    return "Max. position is not reachable with given speed";
                }
            }

            if (enemy.SpecialMovement == SpecialMovement.Sinus8 ||
                enemy.SpecialMovement == SpecialMovement.Sinus16)
            {
                int frames1, frames2;

                if (enemy.Speed == 255) // 1/2
                {
                    frames1 = (enemyPosition - enemy.MinPosition) * 2;
                    frames2 = (enemy.MaxPosition - enemyPosition) * 2;
                }
                else if (enemy.Speed == 254) // 1/4
                {
                    frames1 = (enemyPosition - enemy.MinPosition) * 4;
                    frames2 = (enemy.MaxPosition - enemyPosition) * 4;
                }
                else
                {
                    frames1 = (enemyPosition - enemy.MinPosition) / enemy.Speed;
                    frames2 = (enemy.MaxPosition - enemyPosition) / enemy.Speed;
                }

                if (enemy.SpecialMovement == SpecialMovement.Sinus8 && frames1 % 32 != 0)
                {
                    return "Min. posisition is not reachable with given speed and special movement";
                }

                if (enemy.SpecialMovement == SpecialMovement.Sinus16 && frames1 % 64 != 0)
                {
                    return "Min. posisition is not reachable with given speed and special movement";
                }

                if (enemy.SpecialMovement == SpecialMovement.Sinus8 && frames2 % 32 != 0)
                {
                    return "Max. posisition is not reachable with given speed and special movement";
                }

                if (enemy.SpecialMovement == SpecialMovement.Sinus16 && frames2 % 64 != 0)
                {
                    return "Max. posisition is not reachable with given speed and special movement";
                }
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
               
                if (!this.enShooting[enemy.Animation.Name])
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

        private string ValidateEnemyBlinking(Enemy enemy)
        {
            if (enemy.BlinkingType == BlinkingType.NotBlinking)
            {
                return null;
            }

            if (enemy.BlinkingFrequency <= 0)
            {
                return "Blinkingfrequency must be greater than 0";
            }

            if (enemy.BlinkingFrequency > 255)
            {
                return "Blinking frequency is too high";
            }

            if (enemy.BlinkingInitialFrequency <= 0)
            {
                return "Blinking initial frequency must be greater than 0";
            }

            if (enemy.BlinkingInitialFrequency > 255)
            {
                return "Blinking initial frequency is too high";
            }

            return null;
        }

        private bool ValidateAllElevators()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Elevator errors:");

            var errors = false;
            foreach (var elevator in this.Elevators)
            {
                var validation = this.ValidateElevator(elevator, elevator);
                if (validation != null)
                {
                    errors = true;
                    stringBuilder.AppendLineFormat("{0}: {1}", elevator, validation);
                }
            }

            if (errors)
            {
                MessageBox.Show(stringBuilder.ToString());
            }

            return !errors;
        }

        private string ValidateElevator(AddEditElevatorDialog dialog)
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
            if (!dialog.TryGetMax(out max))
            {
                return "Max. position is not a valid number";
            }
            
            // Try creating an elevator. This should never fail.
            Elevator elevator;
            if (!dialog.TryGetElevator(out elevator))
            {
                return "Something went wrong.";
            }

            // Validate the elevator.
            return this.ValidateElevator(elevator, dialog.ExistingElevator);
        }

        private string ValidateElevator(Elevator elevator, Elevator existingElevator)
        {
            if (elevator.Size < Constants.MinElevatorSize || elevator.Size > Constants.MaxElevatorSize)
            {
                return "Elevator size is invalid";
            }

            var position = this.ValidateElevatorPosition(elevator, existingElevator);
            if (position != null)
            {
                return position;
            }

            var movement = this.ValidateElevatorMovement(elevator);
            if (movement != null)
            {
                return movement;
            }

            return null;
        }

        private string ValidateElevatorPosition(Elevator elevator, Elevator existingElevator)
        {
            if (elevator.X > this.level.Length * Constants.BackgroundTileWidth)
            {
                return "X is too high";
            }

            if (elevator.X < 0)
            {
                return "X cannot be negative";
            }

            if (elevator.X + elevator.Width > (elevator.Screen + 1) * Constants.ScreenWidth)
            {
                return "Elevator is spanning across screens";
            }

            if (elevator.Y > Constants.ScreenHeight)
            {
                return "Y is too high";
            }

            if (elevator.Y < 0)
            {
                return "Y cannot be negative";
            }

            var previousScreenCount = this.Elevators.Count(e => e != existingElevator && e.Screen == elevator.Screen - 1);
            var currentScreenCount = this.Elevators.Count(e => e != existingElevator && e.Screen == elevator.Screen) + 1;
            var nextScreenCount = this.Elevators.Count(e => e != existingElevator && e.Screen == elevator.Screen + 1);

            if (previousScreenCount + currentScreenCount > Constants.ElevatorsLimitPerTwoScreens)
            {
                return "Too many elevators on this an the previous screen";
            }

            if (currentScreenCount + nextScreenCount > Constants.ElevatorsLimitPerTwoScreens)
            {
                return "Too many elevators on this an the next screen";
            }
            
            return null;
        }

        private string ValidateElevatorMovement(Elevator elevator)
        {
            if (elevator.MovementType == MovementType.None)
            {
                if (elevator.Speed != 0)
                {
                    return "Speed must be 0 for non-moving elevators";
                }

                if (elevator.InitialDistanceLeft != 0)
                {
                    return "Distance left must be 0 for non-moving elevators";
                }

                if (elevator.Direction != Direction.None)
                {
                    return "Direction must be none for non-moving elevators";
                }

                return null;
            }          

            if (elevator.Direction == Direction.None)
            {
                return "Direction cannot be none";
            }

            if (elevator.Speed <= 0)
            {
                return "Speed must be greater than 0";
            }

            if (elevator.Speed > 255)
            {
                return "Speed is too high";
            }
            
            if (elevator.MinPosition > elevator.MaxPosition)
            {
                return "Min. position must be less than max. position";
            }

            if (elevator.MinPosition < 0)
            {
                return "Min. position cannot be negative";
            }

            if (elevator.MovementType == MovementType.Vertical && (elevator.Direction == Direction.Left || elevator.Direction == Direction.Right) ||
                elevator.MovementType == MovementType.Horizontal && (elevator.Direction == Direction.Up || elevator.Direction == Direction.Down))
            {
                return "Invalid direction for given movement type";
            }           

            var rectangle = elevator.MovementRectangle;
            var elevatorMinX = rectangle.X;
            var elevatorMaxX = rectangle.X + rectangle.Width;
            var elevatorMinY = rectangle.Y;
            var elevatorMaxY = rectangle.Y + rectangle.Height;

            if (elevatorMinX < elevator.Screen * Constants.ScreenWidth)
            {
                return "Elevator moving off-screen to the left";
            }

            if (elevatorMaxX + elevator.Width > Math.Min((elevator.Screen + 1) * Constants.ScreenWidth, this.level.Length * Constants.BackgroundTileWidth))
            {
                return "Elevator moving off-screen to the right";
            }

            if (elevatorMinY < 0)
            {
                return "Elevator walking off-screen on the top";
            }

            if (elevatorMaxY > Constants.ScreenHeight)
            {
                return "Elevator walking off-screen on the bottom";
            }

            int elevatorPosition;
            string elevatorPositionString;
            if (elevator.MovementType == MovementType.Horizontal)
            {
                elevatorPosition = elevator.X;
                elevatorPositionString = "X";
            }
            else if (elevator.MovementType == MovementType.Vertical)
            {
                elevatorPosition = elevator.Y;
                elevatorPositionString = "Y";
            }
            else
            {
                return "Unknown movement type";
            }

            if (elevator.MinPosition > elevatorPosition)
            {
                return $"Min. position must be <= {elevatorPositionString}";
            }

            if (elevator.MaxPosition < elevatorPosition)
            {
                return $"Max. position must be >= {elevatorPositionString}";
            }

            if (elevator.InitialDistanceLeft == 0)
            {
                return "Elevator must have some initial movement in the direction it's facing";
            }

            if (elevator.Speed != 254 && elevator.Speed != 255) // special values for 1/4 and 1/2 - everything is reachable with those
            {
                if ((elevatorPosition - elevator.MinPosition) % elevator.Speed != 0)
                {
                    return "Min. position is not reachable with given speed";
                }

                if ((elevator.MaxPosition - elevatorPosition) % elevator.Speed != 0)
                {
                    return "Max. position is not reachable with given speed";
                }
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