using SpriteHelper.Contract;
using System;
using System.Windows.Forms;

namespace SpriteHelper.Controls
{
    public partial class BlinkingPanel : UserControl
    {
        public BlinkingPanel()
        {
            InitializeComponent();
            this.blinkingTypeComboBox.Items.Add(BlinkingType.NotBlinking.ToString());
            this.blinkingTypeComboBox.Items.Add(BlinkingType.ConstInitVisible.ToString());
            this.blinkingTypeComboBox.Items.Add(BlinkingType.ConstInitInvisible.ToString());
            this.blinkingTypeComboBox.SelectedIndex = 0;
        }

        public bool TryGetBlinkingType(out BlinkingType blinkingType)
        {
            return Enum.TryParse(this.blinkingTypeComboBox.SelectedItem.ToString(), out blinkingType);
        }

        public bool TryGetFreq(out int freq)
        {
            return int.TryParse(this.blinkingFreqTextBox.Text, out freq);
        }

        public bool TryGetInitialFreq(out int initialFreq)
        {
            return int.TryParse(this.initialFreqTextBox.Text, out initialFreq);
        }

        public void SetBlinkingType(BlinkingType blinkingType)
        {
            this.blinkingTypeComboBox.SelectedItem = blinkingType.ToString();
        }

        public void SetFreq(int freq)
        {
            this.blinkingFreqTextBox.Text = freq.ToString();
        }

        public void SetInitialFreq(int initialFreq)
        {
            this.initialFreqTextBox.Text = initialFreq.ToString();
        }

        public void SetDefaultValues()
        {
            this.SetBlinkingType(BlinkingType.NotBlinking);
            this.SetFreq(0);
            this.SetInitialFreq(0);            
        }

        private void BlinkingTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            BlinkingType bt;
            TryGetBlinkingType(out bt);
            this.blinkingFreqTextBox.Enabled = bt != BlinkingType.NotBlinking;
            this.initialFreqTextBox.Enabled = bt != BlinkingType.NotBlinking;
        }
    }
}
