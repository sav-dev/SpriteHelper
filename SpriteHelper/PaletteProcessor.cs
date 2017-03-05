using System.Windows.Forms;

namespace SpriteHelper
{
    public partial class PaletteProcessor : Form
    {
        public PaletteProcessor()
        {
            InitializeComponent();
            this.PreLoad();
        }

        private void PreLoad()
        {
            this.palettesTextBox.Text = Defaults.Instance.PalettesSpec;
            this.Process();
        }

        private void ProcessButtonClick(object sender, System.EventArgs e)
        {
            this.Process();
        }

        private void Process()
        {
            var palettesConfig = Palettes.Read(this.palettesTextBox.Text);            
        }
    }
}
