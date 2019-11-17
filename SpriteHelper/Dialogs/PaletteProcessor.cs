using SpriteHelper.Contract;
using SpriteHelper.Files;
using SpriteHelper.NesGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class PaletteProcessor : Form
    {
        public PaletteProcessor()
        {
            InitializeComponent();
        }

        private void PaletteProcessorLoad(object sender, EventArgs e)
        {
            if (Defaults.Instance.ApplyDefaults)
            {
                this.PreLoad();
            }
        }

        private void PreLoad()
        {
            this.palettesTextBox.Text = Defaults.Instance.PalettesSpec;
            this.spritesTextBox.Text = Defaults.Instance.SpritesPalette;
            this.backgroundTextBox.Text = Defaults.Instance.BackgroundPalette;
            this.Process();
        }

        private void ProcessButtonClick(object sender, System.EventArgs e)
        {
            this.Process();
        }

        private void Process()
        {
            var palettesConfig = Palettes.Read(this.palettesTextBox.Text);
            var spritesPalette = new List<byte>();
            var backgroundPalettes = new List<byte>();

            spritesPalette.AddRange(palettesConfig.SpritesPalette.SelectMany(p => p.Colors).Select(c => (byte)c));
            foreach (var bgPalette in palettesConfig.BackgroundPalettes)
            {
                backgroundPalettes.AddRange(bgPalette.Palettes.SelectMany(p => p.Colors).Select(c => (byte)c));
            }

            if (File.Exists(spritesTextBox.Text))
            {
                File.Delete(spritesTextBox.Text);
            }

            File.WriteAllBytes(spritesTextBox.Text, spritesPalette.ToArray());

            if (File.Exists(backgroundTextBox.Text))
            {
                File.Delete(backgroundTextBox.Text);
            }

            File.WriteAllBytes(backgroundTextBox.Text, backgroundPalettes.ToArray());

            const int HorizontalPadding = 6;
            const int VerticalPadding = 12;
            const int RectangleSize = 32;

            Func<Palette, Bitmap> bitmapCreator = palette =>
            {
                var bitmap = new Bitmap(RectangleSize * 4, RectangleSize);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    for (var i = 0; i < 4; i++)
                    {                        
                        graphics.FillRectangle(new SolidBrush(palette.ActualColors[i]), i * RectangleSize, 0, RectangleSize, RectangleSize);
                        graphics.DrawRectangle(new Pen(MyBitmap.GridColor, 1), i * RectangleSize, 0, RectangleSize - 1, RectangleSize - 1);
                    }
                }

                var bitmapWithPadding = new Bitmap(bitmap.Width + HorizontalPadding, bitmap.Height + HorizontalPadding);
                using (var graphics = Graphics.FromImage(bitmapWithPadding))
                {
                    graphics.DrawImage(bitmap, HorizontalPadding / 2, HorizontalPadding / 2);
                }

                return bitmapWithPadding;
            };

            Func<Bitmap[], Bitmap> bigBitmapCreator = bitmaps =>
            {
                var bitmap = new Bitmap(bitmaps.Sum(b => b.Width), bitmaps[0].Height);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    var x = 0;
                    foreach (var b in bitmaps)
                    {
                        graphics.DrawImage(b, x, 0);
                        x += b.Width;
                    }
                }

                return bitmap;
            };

            var spriteBitmap = bigBitmapCreator(palettesConfig.SpritesPalette.Select(bitmapCreator).ToArray());
            var backgroundBitmaps = palettesConfig.BackgroundPalettes.Select(p => bigBitmapCreator(p.Palettes.Select(bitmapCreator).ToArray())).ToArray();

            // Default height is just for one bg pallete, resize
            var paletteHeight = spriteBitmap.Height + VerticalPadding;
            var resize = (backgroundBitmaps.Length - 1) * paletteHeight;
            this.Height = this.Height + resize;
            this.palettesPictureBox.Height = this.palettesPictureBox.Height + resize;

            var resultBitmap = new Bitmap(spriteBitmap.Width, paletteHeight * (backgroundBitmaps.Length + 1));
            using (var graphics = Graphics.FromImage(resultBitmap))
            {
                graphics.DrawImage(spriteBitmap, 0, 0);
                var y = paletteHeight;
                foreach (var bgBitmap in backgroundBitmaps)
                {
                    graphics.DrawImage(bgBitmap, 0, y);
                    y += paletteHeight;
                }
            }

            this.palettesPictureBox.Image = resultBitmap;
        }       
    }
}
