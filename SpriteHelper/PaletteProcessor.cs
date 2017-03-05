using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper
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
            var backgroundPalette = new List<byte>();

            spritesPalette.AddRange(palettesConfig.SpritesPalette.SelectMany(p => p.Colors).Select(c => (byte)c));
            backgroundPalette.AddRange(palettesConfig.BackgroundPalette.SelectMany(p => p.Colors).Select(c => (byte)c));
        }       
    }
}
