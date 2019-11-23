using System;
using System.Windows.Forms;

namespace SpriteHelper.Controls
{
    public partial class PositionPanel : UserControl
    {
        public PositionPanel()
        {
            InitializeComponent();
        }

        public bool enableFinePosition;
        public bool EnableFinePosition
        {
            get
            {
                return this.enableFinePosition;
            }
            set
            {
                this.enableFinePosition = value;
                this.xOffsetTextBox.Enabled = value;
                this.yOffsetTextBox.Enabled = value;
                if (!value)
                {
                    this.xOffsetTextBox.Text = "0";
                    this.yOffsetTextBox.Text = "0";
                }
            }
        }

        private void InputTextChanged(object sender, EventArgs e)
        {
            int xTiles, xOffset, yTiles, yOffset;

            if (!int.TryParse(xTilesTextBox.Text, out xTiles) || !int.TryParse(xOffsetTextBox.Text, out xOffset))
            {
                xTextBox.Text = "Invalid";
            }
            else
            {
                xTextBox.Text = $"{xTiles * Constants.BackgroundTileWidth + xOffset}";
            }

            if (!int.TryParse(yTilesTextBox.Text, out yTiles) || !int.TryParse(yOffsetTextBox.Text, out yOffset))
            {
                yTextBox.Text = "Invalid";
            }
            else
            {
                yTextBox.Text = $"{yTiles * Constants.BackgroundTileHeight + yOffset}";
            }
        }

        public bool TryGetX(out int x)
        {
            return int.TryParse(xTextBox.Text, out x);
        }

        public bool TryGetY (out int y)
        {
            return int.TryParse(yTextBox.Text, out y);
        }

        public void SetX(int x)
        {
            this.xTilesTextBox.Text = $"{x / Constants.BackgroundTileWidth}";
            this.xOffsetTextBox.Text = $"{x % Constants.BackgroundTileWidth}";
        }

        public void SetY(int y)
        {
            this.yTilesTextBox.Text = $"{y / Constants.BackgroundTileHeight}";
            this.yOffsetTextBox.Text = $"{y % Constants.BackgroundTileHeight}";
        }

        public void SetDefaultValues()
        {
            this.SetX(0);
            this.SetY(0);
        }
    }
}
