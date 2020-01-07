using SpriteHelper.NesGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class TitleDialog : Form
    {
        public TitleDialog()
        {
            InitializeComponent();
            this.logoTextBox.Text = FileConstants.Logo;
            this.fontTextBox.Text = FileConstants.Font;
            this.chrOutputTextBox.Text = FileConstants.TitleChr;
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            var tiles = new List<MyBitmap>();
            var bgColor = Color.Black;

            // Font
            var fontBitmap = MyBitmap.FromFile(fontTextBox.Text);
            for (var y = 0; y < fontBitmap.Height; y += Constants.SpriteHeight)                
            {
                for (var x = 0; x < fontBitmap.Width; x += Constants.SpriteWidth)
                {
                    var part = fontBitmap.GetPart(x, y, Constants.SpriteWidth, Constants.SpriteHeight);
                    if (!part.IsSolidColor(part.GetPixel(0, 0)))
                    {
                        tiles.Add(part);
                    }
                }
            }
        
            // Logo
            var logo = MyBitmap.FromFile(FileConstants.Logo);
            for (var y = 0; y < logo.Height; y += Constants.SpriteHeight)
            {
                for (var x = 0; x < logo.Width; x += Constants.SpriteWidth)                    
                {
                    var tile = logo.GetPart(x, y, Constants.SpriteWidth, Constants.SpriteHeight);
                    if (!tiles.Any(t => t.Equals(tile)))
                    {
                        tiles.Add(tile);
                    }
                }
            }

            // Create CHR.
            // 1st empty sprite
            var bytes = new List<byte>();
            bytes.AddRange(Enumerable.Repeat((byte)0, 16));
            foreach (var tile in tiles)
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

                        var pixel = tile.GetPixel(x, y).ToArgb();
                        if (pixel != bgColor.ToArgb())
                        {
                            lowBit |= 1;
                        }

                        ////var pixel = 0 .. 3
                        ////
                        ////if (pixel == 1 || pixel == 3)
                        ////{
                        ////    // low bit set
                        ////    lowBit |= 1;
                        ////}
                        ////
                        ////if (pixel == 2 || pixel == 3)
                        ////{
                        ////    // high bit set
                        ////    highBit |= 1;
                        ////}
                    }

                    lowBits.Add(lowBit);
                    highBits.Add(highBit);
                }

                bytes.AddRange(lowBits);
                bytes.AddRange(highBits);
            }

            bytes.AddRange(Enumerable.Repeat((byte)0, 4096 - bytes.Count));
            File.WriteAllBytes(chrOutputTextBox.Text, bytes.ToArray());

            // Get code.
            var code = this.GetCode();
            new CodeWindow(code).ShowDialog();
        }

        private string GetCode()
        {
            // todo
            return string.Empty;            
        }
    }
}
