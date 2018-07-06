using SpriteHelper.Contract;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper.Controls
{
    public partial class MovementPanel : UserControl
    {
        public MovementPanel()
        {
            InitializeComponent();
        }

        private void InputTextChanged(object sender, EventArgs e)
        {
            int minTiles, minOffset, maxTiles, maxOffset;

            if (!int.TryParse(minTilesTextBox.Text, out minTiles) || !int.TryParse(minOffsetTextBox.Text, out minOffset))
            {
                minTextBox.Text = "Invalid";
            }
            else
            {
                minTextBox.Text = $"{minTiles * Constants.BackgroundTileWidth + minOffset}";
            }

            if (!int.TryParse(maxTilesTextBox.Text, out maxTiles) || !int.TryParse(maxOffsetTextBox.Text, out maxOffset))
            {
                maxTextBox.Text = "Invalid";
            }
            else
            {
                maxTextBox.Text = $"{maxTiles * Constants.BackgroundTileHeight + maxOffset}";
            }
        }

        public MovementType? MovementType
        {
            get
            {
                var value = this.movementComboBox.SelectedItem as string;
                return (MovementType)Enum.Parse(typeof(MovementType), value);
            }
            set
            {
                this.movementComboBox.SelectedItem = value?.ToString();
            }
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

        public bool TryGetSpeed(out int speed)
        {
            return int.TryParse(this.speedTextBox.Text, out speed);
        }

        public bool TryGetMin(out int min)
        {
            return int.TryParse(this.minTextBox.Text, out min);
        }

        public bool TryGetMax(out int max)
        {
            return int.TryParse(this.maxTextBox.Text, out max);
        }

        public void SetSpeed(int speed)
        {
            this.speedTextBox.Text = speed.ToString();
        }

        public void SetMin(int min)
        {
            this.minTilesTextBox.Text = $"{min / Constants.BackgroundTileWidth}";
            this.minOffsetTextBox.Text = $"{min % Constants.BackgroundTileWidth}";
        }

        public void SetMax(int max)
        {
            this.maxTilesTextBox.Text = $"{max / Constants.BackgroundTileHeight}";
            this.maxOffsetTextBox.Text = $"{max % Constants.BackgroundTileHeight}";
        }

        public void SetTypes(params MovementType[] types)
        {
            this.movementComboBox.Items.Clear();
            this.movementComboBox.Items.AddRange(types.Select(t => t.ToString()).ToArray());
        }

        public void SetDefaultValues()
        {
            this.MovementType = Contract.MovementType.None;
            this.InitialFlip = false;
            this.SetSpeed(0);
            this.SetMin(0);
            this.SetMax(0);
        }

        private void MovementComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            var setToNone = this.MovementType == Contract.MovementType.None;

            this.speedTextBox.Enabled = !setToNone;
            this.minTilesTextBox.Enabled = !setToNone;
            this.minOffsetTextBox.Enabled = !setToNone;
            this.maxTilesTextBox.Enabled = !setToNone;
            this.maxOffsetTextBox.Enabled = !setToNone;
            this.minTextBox.Enabled = !setToNone;
            this.maxTextBox.Enabled = !setToNone;
        }
    }
}
