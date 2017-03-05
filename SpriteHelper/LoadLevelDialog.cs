using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteHelper
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
            this.specTextBox.Text = Defaults.Instance.BackgroundSpec;
            this.palettesTextBox.Text = Defaults.Instance.PalettesSpec;
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

        public string Spec
        {
            get
            {
                return this.specTextBox.Text;
            }
        }

        private void BrowseLevelButtonClick(object sender, EventArgs e)
        {
            this.OpenXmlFile(this.levelTextBox);
        }

        private void BrowseSpecButtonClick(object sender, EventArgs e)
        {
            this.OpenXmlFile(this.specTextBox);
        }

        private void BrowsePalettesButtonClick(object sender, EventArgs e)
        {
            this.OpenXmlFile(this.palettesTextBox);
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
