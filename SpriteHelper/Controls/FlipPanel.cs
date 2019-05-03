using System.Windows.Forms;

namespace SpriteHelper.Controls
{
    public partial class FlipPanel : UserControl
    {
        public FlipPanel()
        {
            InitializeComponent();
        }

        public bool InitialFlip
        {
            get
            {
                return this.initialFlipCheckBox.Checked;
            }

            set
            {
                this.initialFlipCheckBox.Checked = value;
            }
        }

        public bool ShouldFlip
        {
            get
            {
                return this.shouldFlipCheckBox.Checked;
            }

            set
            {
                this.shouldFlipCheckBox.Checked = value;
            }
        }

        public void SetDefaultValues()
        {
            this.InitialFlip = false;
            this.ShouldFlip = true;
        }
    }
}