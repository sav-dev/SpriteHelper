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

            // todo 0008 completely redo this
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

        public string SpriteChr
        {
            get
            {
                return this.spriteChrTextBox.Text;
            }
        }

        public string ConstSpritesChr
        {
            get
            {
                return this.constSpritesChrTextBox.Text;
            }
        }

        public string ConstSpritesConfig
        {
            get
            {
                return this.constSpritesConfTextBox.Text;
            }
        }

        private void BrowseLevelButtonClick(object sender, EventArgs e)
        {
            this.OpenFile(this.levelTextBox);
        }

        private void BrowseBgSpecButtonClick(object sender, EventArgs e)
        {
            this.OpenFile(this.bgSpecTextBox);
        }

        private void BrowseEnSpecButtonClick(object sender, EventArgs e)
        {
            this.OpenFile(this.enSpecTextBox);
        }

        private void BrowsePalettesButtonClick(object sender, EventArgs e)
        {
            this.OpenFile(this.palettesTextBox);
        }

        private void BrowsePlayerButtonClick(object sender, EventArgs e)
        {
            this.OpenFile(this.playerTextBox);
        }

        private void BrowseSpriteChrButtonClick(object sender, EventArgs e)
        {
            this.OpenFile(this.spriteChrTextBox);
        }

        private void BrowseConstSpritesButtonClick(object sender, EventArgs e)
        {
            this.OpenFile(this.constSpritesChrTextBox);
        }

        private void BrowseConstSpritesConfButtonClick(object sender, EventArgs e)
        {
            this.OpenFile(this.constSpritesConfTextBox);
        }

        private void OpenFile(TextBox target)
        {
            var openFileDialog = new OpenFileDialog(); // todo 0008 if we keep this add variable default dir
            openFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                target.Text = openFileDialog.FileName;
            }
        }
    }    
}
