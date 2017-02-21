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

        public LoadLevelDialog()
        {
            this.clickedOk = false;
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
    }    
}
