using SpriteHelper.Contract;
using SpriteHelper.NesGraphics;
using SpriteHelper.Utility;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class TilesetViewer : Form
    {
        private const int minWidth = 525;
        private const int maxWidth = 1250;
        private const int heightOffset = 83;

        private Tilesets config;
        private Palettes palettes;

        private bool disableRedrawing;
        private int? selectTileset;
        private int? selectPalette;
        private bool isLevelEditor;

        public TilesetViewer(int? selectTileset = null, int? selectPalette = null, bool allowTilesetChange = true, bool isLevelEditor = false)
        {
            InitializeComponent();
            this.selectTileset = selectTileset;
            this.selectPalette = selectPalette;
            this.tilesetComboBox.Enabled = allowTilesetChange;
            if (!allowTilesetChange && !selectTileset.HasValue) throw new Exception("Tileset must be give");
            this.button1.Text = isLevelEditor ? "OK" : "Reload";
            this.button2.Text = isLevelEditor ? "Cancel" : "Code";
            this.isLevelEditor = isLevelEditor;
        }

        private void TilesetViewerLoad(object sender, EventArgs e)
        {
            this.LoadTilesetList(this.selectTileset, this.selectPalette);
        }

        private void LoadTilesetList(int? selectTileset = null, int? selectPalette = null)
        {
            this.disableRedrawing = true;
            this.config = Tilesets.Read(FileConstants.Tilesets);
            this.palettes = Palettes.Read(FileConstants.PalettesSpec);
            this.tilesetComboBox.Items.Clear();
            foreach (var id in this.config.LoadedSets.Select(ls => ls.Id).ToArray()) this.tilesetComboBox.Items.Add(id);
            this.tilesetComboBox.SelectedIndex = selectTileset ?? 0;
            this.paletteComboBox.Items.Clear();
            foreach (var id in this.palettes.BackgroundPalettes.Select(ls => ls.Id).ToArray()) this.paletteComboBox.Items.Add(id);
            this.paletteComboBox.SelectedIndex = selectPalette ?? 0;
            this.disableRedrawing = false;
            this.Redraw();
        }

        public bool Succeeded { get; private set; }

        public int SelectedTileset => (int)this.tilesetComboBox.SelectedItem;

        public int SelectedPalette => (int)this.paletteComboBox.SelectedItem;

        private void PaletteComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.Redraw();
        }

        private void TilesetComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.Redraw();
        }

        private void Redraw()
        {
            if (this.disableRedrawing)
            {
                return;
            }

            var selectedSpec = this.config.GetSpecById((int)this.tilesetComboBox.SelectedItem);
            var selectedPalette = this.palettes.BackgroundPalettes.First(p => p.Id == (int)this.paletteComboBox.SelectedItem);

            var bgSpec = selectedSpec.GetBgSpec();
            var blocking = MyBitmap.FromFile(bgSpec.BlockingFile).Scale(2);
            var nonBlocking = MyBitmap.FromFile(bgSpec.NonBlockingFile).Scale(2);
            var threats = MyBitmap.FromFile(bgSpec.ThreatFile).Scale(2);

            var packed = Packer.Pack(new Size[] { blocking.Size, nonBlocking.Size, threats.Size }, maxWidth);

            var width = packed.Max(tpl => tpl.Item1.X + tpl.Item2.Width);
            var height = packed.Max(tpl => tpl.Item1.Y + tpl.Item2.Height);

            var blockingPosition = packed.First(tpl => tpl.Item2 == blocking.Size);
            packed.Remove(blockingPosition);
            var nonBlockingPosition = packed.First(tpl => tpl.Item2 == nonBlocking.Size);
            packed.Remove(nonBlockingPosition);
            var threatsPosition = packed.First(); // only one left

            var palette = this.palettes.BackgroundPalettes[this.SelectedPalette];
            var bgColor = palette.Palettes.First().ActualColors.First();

            var bigBitmap = new MyBitmap(2 * width + 30, 2 * height + 30, bgColor);

            for (var i = 0; i <= 3; i++)
            {
                var bitmap = new MyBitmap(width, height, bgColor);

                var blockingClone = blocking.Clone();
                blockingClone.UpdateColors(blockingClone.UniqueColors(), palette.Palettes[i].ActualColors);
                var nonBlockingClone = nonBlocking.Clone();
                nonBlockingClone.UpdateColors(nonBlockingClone.UniqueColors(), palette.Palettes[i].ActualColors);
                var threatsClone = threats.Clone();
                threatsClone.UpdateColors(threatsClone.UniqueColors(), palette.Palettes[i].ActualColors);

                bitmap.DrawImage(blockingClone, blockingPosition.Item1.X, blockingPosition.Item1.Y);
                bitmap.DrawImage(nonBlockingClone, nonBlockingPosition.Item1.X, nonBlockingPosition.Item1.Y);
                bitmap.DrawImage(threatsClone, threatsPosition.Item1.X, threatsPosition.Item1.Y);

                bigBitmap.DrawImage(bitmap, 10 + (i % 2) * (bitmap.Width + 10), 10 + (i / 2) * (bitmap.Height + 10));
            }

            this.paletteLabel.BackgroundImage = bigBitmap.ToBitmap();

            var newWidth = Math.Max(bigBitmap.Width, minWidth);
            var newHeight = bigBitmap.Height;
            this.Width = newWidth;
            this.Height = newHeight + heightOffset;
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (this.isLevelEditor)
            {
                this.Succeeded = true;
                this.Close();
            }
            else
            {
                this.Redraw();
            }
        }

        private void Button2Click(object sender, EventArgs e)
        {
            if (this.isLevelEditor)
            {
                this.Close();
            }
            else
            {
                var code = this.GetCode();
                new CodeWindow(code).ShowDialog();

                // todo 0008: also lay out the chrs in the 1st bank and export that here
            }
        }

        private string GetCode()
        {
            var builder = new StringBuilder();

            builder.AppendLine(
@"TilesetsStart:

;****************************************************************
; Tilesets                                                      ;
; Holds tileset pointers (auto-generated)                       ;
;****************************************************************

Tilesets:");

            foreach (var item in this.config.LoadedSets)
            {
                builder.AppendLine($"Tileset{item.Id}:");
                builder.AppendLine($"  .byte LOW(bg{item.Id}chr), HIGH(bg{item.Id}chr)");
            }

            builder.AppendLine();
            builder.AppendLine("TilesetsEnd:");
            return builder.ToString();
        }

        private void ReloadButtonClick(object sender, EventArgs e)
        {
            var previousTileset = SelectedTileset;
            var previousPalette = SelectedPalette;
            this.LoadTilesetList(previousTileset, previousPalette);            
        }
    }
}
