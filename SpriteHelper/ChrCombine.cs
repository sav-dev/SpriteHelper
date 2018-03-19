using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteHelper
{
    public partial class ChrCombine : Form
    {
        public ChrCombine()
        {
            InitializeComponent();

            if (Defaults.Instance.ApplyDefaults)
            {
                this.PreLoad();
            }
        }

        private void PreLoad()
        {
            this.outputTextBox.Text = Defaults.Instance.CombinedChrOutput;
            this.inputTextBox.Lines = new[] { Defaults.Instance.AnimationOutput, Defaults.Instance.ConstChrInput };
        }

        private void ProcessButtonClick(object sender, EventArgs e)
        {
            var inputFiles = this.inputTextBox.Lines.Select(l => this.SplitChr(File.ReadAllBytes(l))).ToArray();

            var result = new byte[4096];
            var index = 0;

            for (var sprite = 0; sprite < inputFiles.First().Length; sprite++)
            {
                byte[] resultSprite;
                var setSprites = inputFiles.Select(f => f[sprite]).Where(s => s.Any(b => b != 0)).ToArray();
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
                    resultSprite = new byte[16];
                }

                Array.Copy(resultSprite, 0, result, index * 16, 16);
                index++;
            }

            File.WriteAllBytes(outputTextBox.Text, result);
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
    }
}
