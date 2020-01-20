using SpriteHelper.Contract;
using SpriteHelper.NesSound;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class EditLevelDialog : Form
    {
        private Func<EditLevelDialog, bool> validationFunc;

        public EditLevelDialog(
            int width,
            Point playerStartingPosition,
            LevelType levelType,
            Point exitPosition,
            double scrollSpeed,
            string song,
            Func<EditLevelDialog, bool> validationFunc)
        {
            InitializeComponent();

            // Width
            this.widthTextBox.Text = width.ToString();

            // Starting position
            this.playerXTextBox.Text = playerStartingPosition.X.ToString();
            this.playerYTextBox.Text = playerStartingPosition.Y.ToString();
                        
            // Level type
            this.levelTypeComboBox.Items.Clear();
            this.levelTypeComboBox.Items.Add(LevelType.Normal.ToString());
            this.levelTypeComboBox.Items.Add(LevelType.Jetpack.ToString());
            this.levelTypeComboBox.Items.Add(LevelType.Boss.ToString());
            this.levelTypeComboBox.SelectedItem = levelType.ToString();
            this.LevelTypeComboBoxSelectedIndexChanged(null, null);

            // Exit position
            this.exitXTextBox.Text = (exitPosition.X / Constants.BackgroundTileWidth).ToString();
            this.exitYTextBox.Text = (exitPosition.Y / Constants.BackgroundTileHeight).ToString();

            // Scroll speed
            this.scrollSpeedComboBox.SelectedItem = scrollSpeed.ToString();

            // Song
            this.songComboBox.Items.Clear();
            foreach (var item in SoundDataReader.GetSongs().Keys)
            {
                this.songComboBox.Items.Add(item);
            }

            this.songComboBox.SelectedItem = song;

            // Store the validation function
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

        public LevelType LevelType => (LevelType)Enum.Parse(typeof(LevelType), this.levelTypeComboBox.SelectedItem.ToString());

        public double ScrollSpeed => double.Parse(this.scrollSpeedComboBox.SelectedItem.ToString());

        public string Song => this.songComboBox.SelectedItem as string;

        private void LevelTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.exitGroupBox.Enabled = this.LevelType == LevelType.Normal;
            this.jetpackGroupBox.Enabled = this.LevelType == LevelType.Jetpack;
        }
    }
}
