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
            this.enSpecTextBox.Text = @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Sprites\enemies.xml";
            this.playerTextBox.Text = Defaults.Instance.PlayerSpec;
            this.spriteChrTextBox.Text = @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Chr\spr.chr";
            this.constSpritesChrTextBox.Text = Defaults.Instance.ConstChrInput;
            this.constSpritesConfTextBox.Text = Defaults.Instance.ConstSpritesConfig;
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
            var openFileDialog = new OpenFileDialog { InitialDirectory = Defaults.Instance.GraphicsDefaultDir };
            openFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                target.Text = openFileDialog.FileName;
            }
        }
    }    
}
