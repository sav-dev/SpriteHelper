﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
            // todo: uncomment
            //this.UpdateDrawPanel();
        }
        
        private void PreLoad()
        {
            this.LoadLevel(null, Defaults.Instance.BackgroundSpec, Defaults.Instance.PalettesSpec);
        }

        private void UpdateStatus(string text)
        {
            this.toolStripStatusLabel.Text = text;
        }

        private void UpdateStatus(string format, params object[] args)
        {
            this.toolStripStatusLabel.Text = string.Format(format, args);
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

            // todo: implement
            //this.tiles = new Dictionary<string, Bitmap[]>();            
            //foreach (var tile in this.config.Tiles)
            //{
            //    var tileBitmaps = new Bitmap[(int)TileVersion.All + 1];               
            //    var tileBitmap = bitmaps[tile.Type].GetPart(tile.X, tile.Y, tile.WidthInSprites * Constants.SpriteWidth, tile.HeightSprites * Constants.SpriteHeight).Scale(Zoom);
            //
            //    // None
            //    tileBitmaps[(int)TileVersion.None] = tileBitmap.ToBitmap();
            //
            //    // Grid
            //    var tileBitmapGrid = tileBitmap.Clone();
            //    tileBitmapGrid.DrawGrid();
            //    tileBitmaps[(int)TileVersion.Grid] = tileBitmapGrid.ToBitmap();
            //
            //    // Type
            //    foreach (var tileVersion in new[] { TileVersion.None, TileVersion.Grid })
            //    {
            //        var bitmapForVersion = tileBitmaps[(int)tileVersion];
            //        var bitmapCopy = new Bitmap(bitmapForVersion);
            //
            //        Brush brush = null;
            //        switch (tile.Type)
            //        {
            //            case TileType.Blocking:
            //                brush = new SolidBrush(Color.FromArgb(100, 0, 255, 0));
            //                break;
            //
            //            case TileType.Threat:
            //                brush = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
            //                break;
            //        }
            //
            //        if (brush != null)
            //        { 
            //            using (var graphics = Graphics.FromImage(bitmapCopy))
            //            {
            //                graphics.FillRectangle(brush, 0, 0, bitmapCopy.Width, bitmapCopy.Height);                                
            //            }
            //        }
            //
            //        tileBitmaps[(int)(tileVersion | TileVersion.Type)] = bitmapCopy;
            //    }
            //
            //    this.tiles.Add(tile.Id, tileBitmaps);
            //}
            
            this.PopulateTiles();
            
            // Empty tile should always be the 1st one
            this.emptyTile = this.config.Tiles[0].Id;
            
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
            foreach (var type in new[] { TileType.Blocking, TileType.NonBlocking, TileType.Threat })
            {
                var tab = this.tileTabs[type];
                tab.Controls.Clear();

                var paletteTabControl = new TabControl { Alignment = TabAlignment.Bottom, Dock = DockStyle.Fill };

                for (var palette = 0; palette < 4; palette++)
                {
                    var tileSelector = new TileSelector(this.bitmaps[type][palette]);
                    
                    var tableLayoutPanel = new TableLayoutPanel { Dock = DockStyle.Fill };
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

        public string SelectedTile()
        {
            var tabControl = this.mainTabControl.SelectedTab.Controls.Cast<object>().FirstOrDefault(c => c is TabControl) as TabControl;
            if (tabControl == null)
            {
                return null;
            }

            var panel = tabControl.SelectedTab.Controls.Cast<object>().FirstOrDefault(c => c is TableLayoutPanel) as TableLayoutPanel;
            if (panel == null)
            {
                return null;
            }

            var selector = panel.Controls.Cast<object>().FirstOrDefault(c => c is TileSelector) as TileSelector;
            if (selector == null)
            {
                return null;
            }

            return selector.SelectedTile;
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
            this.UpdateBitmap();
        }

        private void UpdateBitmap()
        {
            // todo fix
            //this.bitmap = new Bitmap(this.level.Length * TileWidth, this.level[0].Length * TileHeight);
            //this.graphics = Graphics.FromImage(this.bitmap);
            //
            //for (var x = 0; x < this.level.Length; x++)
            //{
            //    for (var y = 0; y < this.level[x].Length; y++)
            //    {
            //        var key = this.level[x][y];
            //        var image = this.tiles[key][(int)this.Settings];
            //        this.graphics.DrawImage(image, new Point(x * TileWidth, y * TileHeight));
            //    }
            //}
            //
            //this.UpdateDrawPanel();
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
            // todo: open the transform dialog
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
        }

        private void DrawPanelMouseDown(object sender, MouseEventArgs e)
        {
            var x = e.X / TileWidth;
            var y = e.Y / TileWidth;
            this.SetTile(x, y, e.Button);
        }

        private void DrawPanelMouseMove(object sender, MouseEventArgs e)
        {
            var x = e.X / TileWidth;
            var y = e.Y / TileWidth;
            this.UpdateStatus("{0} / {1}", x, y);
            this.SetTile(x, y, e.Button);
        }

        public void SetTile(int x, int y, MouseButtons buttons)
        {
            if (x >= this.level.Length || y >= this.level[0].Length || x < 0 || y < 0)
            {
                return;
            }

            string tile;
            if (buttons.HasFlag(MouseButtons.Left))
            {
                tile = this.SelectedTile();
                if (tile == null)
                {
                    return;
                }
            }
            else if (buttons.HasFlag(MouseButtons.Right))
            {
                tile = emptyTile;
            }
            else
            {
                return;
            }
            
            if (this.level[x][y] == tile)
            {
                return;
            }

            this.AddHistory();
            this.level[x][y] = tile;
            var image = this.tiles[tile][(int)this.Settings];
            this.graphics.DrawImage(image, new Point(x * TileWidth, y * TileHeight));
            this.drawPanel.Refresh();
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
            public TileSelector(MyBitmap image)
            {
                this.Anchor = AnchorStyles.None;
                this.Image = image.Scale(Zoom).ToBitmap();
                this.Size = this.Image.Size;
            }

            public string SelectedTile { get; private set; }

            // todo: implement this control
        }

        #endregion
    }
}
