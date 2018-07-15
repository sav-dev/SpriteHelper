using SpriteHelper.Files;
using System;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class LoadLevelDialog : Form
    {
        private bool clickedOk;

        public LoadLevelDialog(bool allowOpen)
        {
            this.InitializeComponent();
            this.clickedOk = false;
            this.levelLabel.Enabled = allowOpen;
            this.levelTextBox.Enabled = allowOpen;
            this.browseLevelButton.Enabled = allowOpen;
            if (!allowOpen)
            {
                this.Text = "New level";
            }
        }

        private void LoadLevelDialogLoad(object sender, EventArgs e)
        {
            if (Defaults.Instance.ApplyDefaults)
            {
                this.PreLoad();
            }
        }

        private void PreLoad()
        {
            this.bgSpecTextBox.Text = Defaults.Instance.BackgroundSpec;
            this.palettesTextBox.Text = Defaults.Instance.PalettesSpec;
            this.levelTextBox.Text = Defaults.Instance.DefaultLevel;
            this.enSpecTextBox.Text = "C:\\Users\\tomas\\Documents\\NES\\GitHub\\Platformer\\PlatformerGraphics\\Sprites\\enemies.xml";
            this.playerTextBox.Text = Defaults.Instance.PlayerSpec;
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            this.clickedOk = true;
            this.Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool ClickedOk
        {
            get
            {
                return this.clickedOk;
            }
        }

        public string Level
        {
            get
            {
                return this.levelTextBox.Text;
            }
        }

        public string Palettes
        {
            get
            {
                return this.palettesTextBox.Text;            
            }
        }

        public string BgSpec
        {
            get
            {
                return this.bgSpecTextBox.Text;
            }
        }

        public string EnSpec
        {
            get
            {
                return this.enSpecTextBox.Text;
            }
        }

        public string Player
        {
            get
            {
                return this.playerTextBox.Text;
            }
        }

        private void BrowseLevelButtonClick(object sender, EventArgs e)
        {
            this.OpenXmlFile(this.levelTextBox);
        }

        private void BrowseBgSpecButtonClick(object sender, EventArgs e)
        {
            this.OpenXmlFile(this.bgSpecTextBox);
        }

        private void BrowseEnSpecButtonClick(object sender, EventArgs e)
        {
            this.OpenXmlFile(this.enSpecTextBox);
        }

        private void BrowsePalettesButtonClick(object sender, EventArgs e)
        {
            this.OpenXmlFile(this.palettesTextBox);
        }

        private void BrowsePlayerButtonClick(object sender, EventArgs e)
        {
            this.OpenXmlFile(this.playerTextBox);
        }

        private void OpenXmlFile(TextBox target)
        {
            var openFileDialog = new OpenFileDialog { InitialDirectory = Defaults.Instance.GraphicsDefaultDir };
            openFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                target.Text = openFileDialog.FileName;
            }
        }
    }    
}
