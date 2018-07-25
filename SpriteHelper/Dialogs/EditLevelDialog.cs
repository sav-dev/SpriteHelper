using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class EditLevelDialog : Form
    {
        private Func<EditLevelDialog, bool> validationFunc;

        public EditLevelDialog(int width, Point playerStartingPosition, Point exitPosition, Func<EditLevelDialog, bool> validationFunc)
        {
            InitializeComponent();
            this.widthTextBox.Text = width.ToString();
            this.playerXTextBox.Text = playerStartingPosition.X.ToString();
            this.playerYTextBox.Text = playerStartingPosition.Y.ToString();
            this.exitXTextBox.Text = (exitPosition.X / Constants.BackgroundTileWidth).ToString();
            this.exitYTextBox.Text = (exitPosition.Y / Constants.BackgroundTileHeight).ToString();
            this.validationFunc = validationFunc;
        }

        public bool Succeeded { get; set; }

        private void OkButtonClick(object sender, EventArgs e)
        {
            if (this.validationFunc(this))
            {
                this.Succeeded = true;
                this.Close();
            }
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool TryGetWidth(out int width)
        {
            return int.TryParse(this.widthTextBox.Text, out width);
        }

        public bool TryGetPlayerStartingPosition(out Point position)
        {
            int x, y;
            if (int.TryParse(this.playerXTextBox.Text, out x) && int.TryParse(this.playerYTextBox.Text, out y))
            {
                position = new Point(x, y);
                return true;
            }
            else
            {
                position = default(Point);
                return false;
            }
        }

        public bool TryGetExitPosition(out Point position)
        {
            int x, y;
            if (int.TryParse(this.exitXTextBox.Text, out x) && int.TryParse(this.exitYTextBox.Text, out y))
            {
                position = new Point(
                    x * Constants.BackgroundTileWidth + Constants.ExitXOff, 
                    y * Constants.BackgroundTileHeight + Constants.ExitYOff);

                return true;
            }
            else
            {
                position = default(Point);
                return false;
            }
        }
    }
}
