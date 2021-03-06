﻿using SpriteHelper.Contract;
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
    public partial class TitleDialog : Form
    {
        // these must be in these order in the fonts file
        public static readonly List<char> Chars = new List<char> {
            ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
            'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z', '!', '?', '-', '\'', ':', ',', '.', };

        public TitleDialog()
        {
            InitializeComponent();
            this.logoTextBox.Text = FileConstants.Logo;
            this.fontTextBox.Text = FileConstants.Font;
            this.chrOutputTextBox.Text = FileConstants.TitleChr;
            this.stringsTextBox.Text = FileConstants.Strings;
            this.cursorTextBox.Text = FileConstants.Cursor;
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            var tiles = new List<MyBitmap>();
            var atts = new List<int>();
            var bgColor = Color.Black;

            // Font
            ProcessFont(this.fontTextBox.Text, tiles, atts);
            if (tiles.Count != Chars.Count)
            {
                throw new Exception("Invalid number of tiles in the font bmp");
            }
        
            // Logo
            var logo = MyBitmap.FromFile(this.logoTextBox.Text);
            var ids = new int[logo.Width / Constants.SpriteWidth, logo.Height / Constants.SpriteHeight];
            for (var y = 0; y < logo.Height; y += Constants.SpriteHeight)
            {
                for (var x = 0; x < logo.Width; x += Constants.SpriteWidth)                    
                {
                    var tile = logo.GetPart(x, y, Constants.SpriteWidth, Constants.SpriteHeight);
                    if (!tiles.Any(t => t.Equals(tile)))
                    {
                        tiles.Add(tile);
                        atts.Add(3);
                    }

                    ids[x / Constants.SpriteWidth, y / Constants.SpriteHeight] = tiles.IndexOf(tile);
                }
            }

            // Cursor.
            var cursor = MyBitmap.FromFile(this.cursorTextBox.Text);
            var cursorId = tiles.Count;
            tiles.Add(cursor);
            atts.Add(2);

            // Create CHR.
            // 1st empty sprite
            var bytes = new List<byte>();
            for (var i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                var attsForTile = atts[i];

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
                            if (attsForTile == 1 || attsForTile == 3)
                            {
                                lowBit |= 1;
                            }

                            if (attsForTile == 2 || attsForTile == 3)
                            {
                                highBit |= 1;
                            }
                        }                        
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
            var code = this.GetCode(logo, ids, cursorId);
            new CodeWindow(code).ShowDialog();
        }

        public static void ProcessFont(string fontBmp, IList<MyBitmap> tiles, IList<int> atts = null)
        {
            var emptyFound = false;
            var secondEmptyFound = false;
            var fontBitmap = MyBitmap.FromFile(fontBmp);
            for (var y = 0; y < fontBitmap.Height; y += Constants.SpriteHeight)
            {
                for (var x = 0; x < fontBitmap.Width; x += Constants.SpriteWidth)
                {
                    var part = fontBitmap.GetPart(x, y, Constants.SpriteWidth, Constants.SpriteHeight);
                    if (part.IsSolidColor(part.GetPixel(0, 0)))
                    {
                        if (!emptyFound)
                        {
                            emptyFound = true;
                            tiles.Add(part);
                            if (atts != null)
                            {
                                atts.Add(1);
                            }                            
                        }
                        else
                        {
                            secondEmptyFound = true;
                        }
                    }
                    else
                    {
                        if (secondEmptyFound)
                        {
                            throw new Exception("Empty font tile in the middle of other tiles");
                        }

                        tiles.Add(part);
                        if (atts != null)
                        {
                            atts.Add(1);
                        }
                    }
                }
            }
        }

        private string GetCode(MyBitmap logo, int[,] logoIds, int cursorId)
        {
            var builder = new StringBuilder();
            builder.AppendLine(
 @"LogoAndTextDataStart:

;****************************************************************
; LogoAndTextData                                               ;
; Holds info for rendering the logo and text (auto-generated)   ;
;****************************************************************
");
            GetLogoData(builder, logo, logoIds);
            GetStringsData(builder);

            builder.AppendLine();
            builder.AppendLine($"CURSOR_TILE = {ToHex(cursorId)}");
            builder.AppendLine();

            builder.AppendLine(@"
LogoAndTextDataEnd:");

            return builder.ToString();
        }

        private void GetLogoData(StringBuilder builder, MyBitmap logo, int[,] logoIds)
        {
            builder.AppendLine(
@";****************************************************************
; Logo                                                          ;
;****************************************************************
");

            var logoX = ((256 - logo.Width) / 2) / Constants.SpriteHeight;
            var logoY = 5;
            var initLogoAddrs = 8192 + logoX + logoY * 32;

            builder.AppendLine($"LOGO_X = {ToHex(logoX)}");
            builder.AppendLine($"LOGO_Y = {ToHex(logoY)}");
            builder.AppendLine($"INITIAL_LOGO_ADDR_L = {ToHex(initLogoAddrs % 256)}");
            builder.AppendLine($"INITIAL_LOGO_ADDR_H = {ToHex(initLogoAddrs / 256)}");
            builder.AppendLine($"LOGO_ROW_LENGTH = {ToHex(logoIds.GetLength(0))}");
            builder.AppendLine($"LOGO_ROWS = {ToHex(logoIds.GetLength(1))}");
            builder.AppendLine("Logo:");
            for (var y = 0; y < logoIds.GetLength(1); y++)
            {
                builder.Append("  .byte ");
                for (var x = 0; x < logoIds.GetLength(0); x++)
                {
                    if (x > 0)
                    {
                        builder.Append(", ");
                    }

                    builder.Append(ToHex(logoIds[x, y]));
                }

                builder.AppendLine();
            }

            builder.AppendLine();
        }

        private void GetStringsData(StringBuilder builder)
        {
            var stringConfig = StringsConfig.Read(this.stringsTextBox.Text);

            builder.AppendLine(
@";****************************************************************
; Strings                                                       ;
;****************************************************************
");

            builder.AppendLine("StringPointers:");

            var maxId = stringConfig.Strings.Max(s => s.Id);
            if (maxId > 127)
            {
                throw new Exception("Too many strings");
            }
                       
            for (var i = 0; i <= maxId; i++)
            {
                builder.AppendLine($"; {i}");

                var str = stringConfig.Strings.FirstOrDefault(s => s.Id == i);
                if (str != null)
                {
                    builder.AppendLine($"; \"{str.Value}\"");
                    builder.AppendLine($"STR_{i} = {i * 2}"); // x2 because it's a pointer
                    builder.AppendLine($"  .byte LOW(string{i}), HIGH(string{i})");
                }
                else
                {
                    builder.AppendLine($"; placeholder");
                    builder.AppendLine($"  .byte $00, $00");
                }

                builder.AppendLine();
            }

            builder.AppendLine();
            builder.AppendLine("Strings:");
            foreach (var str in stringConfig.Strings)
            {
                builder.AppendLine($"string{str.Id}:");
                builder.AppendLine($"  .byte {string.Join(", ", EncodeString(str.Value).Select(b => ToHex(b)))}");
            }

            builder.AppendLine();
        }

        private byte[] EncodeString(string str)
        {
            // 1st byte = length
            // Then byte per char.

            var bytes = new List<byte>();

            if (str.Length > 255) throw new Exception($"string too long: {str}");           
            bytes.Add((byte)str.Length);

            foreach (var chr in str.ToLower().ToCharArray())
            {
                if (!Chars.Contains(chr))
                {
                    throw new Exception($"Invalid character: {chr}");
                }

                bytes.Add((byte)Chars.IndexOf(chr));
            }

            return bytes.ToArray();
        }

        private string ToHex(int n) => $"${n:X2}";
    }
}
