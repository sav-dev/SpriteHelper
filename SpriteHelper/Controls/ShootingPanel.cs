using System.Windows.Forms;

namespace SpriteHelper.Controls
{
    public partial class ShootingPanel : UserControl
    {
        public ShootingPanel()
        {
            InitializeComponent();
        }

        public bool TryGetFreq(out int freq)
        {
            return int.TryParse(this.shootingFreqTextBox.Text, out freq);
        }

        public bool TryGetInitialFreq(out int initialFreq)
        {
            return int.TryParse(this.initialFreqTextBox.Text, out initialFreq);
        }

        public void SetFreq(int freq)
        {
            this.shootingFreqTextBox.Text = freq.ToString();
        }

        public void SetInitialFreq(int initialFreq)
        {
            this.initialFreqTextBox.Text = initialFreq.ToString();
        }

        public void SetDefaultValues()
        {
            this.SetFreq(0);
            this.SetInitialFreq(0);
        }
    }
}