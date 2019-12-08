using SpriteHelper.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class ChrCombine : Form
    {
        public ChrCombine()
        {
            InitializeComponent();
        }

        private byte[] GetSpritesFromSpec(string spec)
        {
            var bytes = new List<byte>();
            var spriteConfig = SpriteConfig.Read(spec, Palettes.Read(this.palettesTextBox.Text));
            for (var i = 0; i < 256; i++)
            {
                var sprite = spriteConfig.Sprites.FirstOrDefault(s => s.Id == i);
                if (sprite != null)
                {
                    var lowBits = new List<byte>();
                    var highBits = new List<byte>();
                    var image = sprite.GetSprite();

                    for (var y = 0; y < Constants.SpriteHeight; y++)
                    {
                        byte lowBit = 0;
                        byte highBit = 0;

                        for (var x = 0; x < Constants.SpriteWidth; x++)
                        {
                            lowBit = (byte)(lowBit << 1);
                            highBit = (byte)(highBit << 1);

                            var pixel = spriteConfig.PaletteMappings[sprite.Mapping].ColorMappings.First(c => c.Color == image.GetPixel(x, y)).To;

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
                else
                {
                    bytes.AddRange(Enumerable.Repeat((byte)0, 16));
                }
            }

            return bytes.ToArray();
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            var inputs = new List<byte[][]>();

            inputs.AddRange(this.specsTextBox.Lines.Select(s => SplitChr(GetSpritesFromSpec(s))));            

            var result = new byte[4096];
            var index = 0;
            
            for (var sprite = 0; sprite < inputs.First().Length; sprite++)
            {
                byte[] resultSprite;
                var setSprites = inputs.Select(f => f[sprite]).Where(s => s.Any(b => b != 0)).ToArray();
                if (setSprites.Length > 1)
                {
                    throw new Exception(string.Format("Sprite {0} set more than once!", sprite));
                }
                else if (setSprites.Length == 1)
                {
                    resultSprite = setSprites[0];
                }
                else
                {
                    break; // last sprite processed
                }
            
                Array.Copy(resultSprite, 0, result, index * 16, 16);
                index++;
            }

            var consts = SplitChr(File.ReadAllBytes(this.constChrTextBox.Text)).ToArray();
            var constsConfig = ConstSprites.Read(this.constChrConfigTextBox.Text);
            var stringBuilder = new StringBuilder();

            var constIndex = 0;
            while (true)
            {
                var sprite = consts[constIndex];
                if (sprite.All(b => b == 0))
                {
                    break; // last sprite processed
                }

                Array.Copy(sprite, 0, result, index * 16, 16);
                stringBuilder.AppendLineFormat("{0} = ${1:X2}", constsConfig.Names[constIndex], index);
                index++;
                constIndex++;
            }

            if (constIndex != constsConfig.Names.Length)
            {
                throw new Exception("Some const sprites were not added!");
            }

            if (index >= 255)
            {
                throw new Exception("Too many sprites!");
            }

            var emptyArray = new byte[16];
            while (index < 256)
            {
                Array.Copy(emptyArray, 0, result, index * 16, 16);
                index++;
            }
            
            File.WriteAllBytes(outputTextBox.Text, result);
            ShowCode(stringBuilder.ToString());
        }     

        private void ShowCode(string code)
        {
            var result =
@";****************************************************************
; Const sprites                                                 ;
; Holds ids of const sprites (auto-generated)                   ;
;****************************************************************

" + code;
            
            new CodeWindow(result).ShowDialog();
        }

        private byte[][] SplitChr(byte[] input)
        {
            if (input.Length != 4096)
            {
                throw new Exception("Invalid CHR");
            }

            var results = new List<byte[]>();

            for (var i = 0; i < 4096; i += 16)
            {
                var result = new byte[16];
                Array.Copy(input, i, result, 0, 16);
                results.Add(result);
            }

            return results.ToArray();
        }

        ////private void ExportButtonClick(object sender, EventArgs e)
        ////{
        ////    // Generate actual chr file
        ////    // CHR format:
        ////    //  each sprite is 16 bytes:
        ////    //  first 8 bytes are low bits per sprite row
        ////    //  second 8 bytes are high bits per sprite row
        ////    var bytes = new List<byte>();
        ////    foreach (var sprite in this.config.Sprites)
        ////    {
        ////        var lowBits = new List<byte>();
        ////        var highBits = new List<byte>();
        ////        var image = sprite.GetSprite();
        ////
        ////        for (var y = 0; y < Constants.SpriteHeight; y++)
        ////        {
        ////            byte lowBit = 0;
        ////            byte highBit = 0;
        ////
        ////            for (var x = 0; x < Constants.SpriteWidth; x++)
        ////            {
        ////                lowBit = (byte)(lowBit << 1);
        ////                highBit = (byte)(highBit << 1);
        ////
        ////                var pixel = this.config.PaletteMappings[sprite.Mapping].ColorMappings.First(c => c.Color == image.GetPixel(x, y)).To;
        ////
        ////                if (pixel == 1 || pixel == 3)
        ////                {
        ////                    // low bit set
        ////                    lowBit |= 1;
        ////                }
        ////
        ////                if (pixel == 2 || pixel == 3)
        ////                {
        ////                    // high bit set
        ////                    highBit |= 1;
        ////                }
        ////            }
        ////
        ////            lowBits.Add(lowBit);
        ////            highBits.Add(highBit);
        ////        }
        ////
        ////        bytes.AddRange(lowBits);
        ////        bytes.AddRange(highBits);
        ////    }
        ////
        ////    while (bytes.Count < 4096)
        ////    {
        ////        bytes.Add(0);
        ////    }
        ////
        ////    if (File.Exists(this.outputTextBox.Text))
        ////    {
        ////        File.Delete(this.outputTextBox.Text);
        ////    }
        ////
        ////    File.WriteAllBytes(this.outputTextBox.Text, bytes.ToArray());
        ////}
    }
}
