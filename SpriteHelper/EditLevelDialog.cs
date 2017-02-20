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
    public partial class EditLevelDialog : Form
    {
        private EditLevelDialogResult result;

        public EditLevelDialog(int width)
        {
            InitializeComponent();
            this.widthTextBox.Text = width.ToString();
            this.RadioCheckedChanged();
            this.result = EditLevelDialogResult.None;
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            this.result = this.Operation;
            this.Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void RadioCheckedChanged(object sender, EventArgs e)
        {
            this.RadioCheckedChanged();
        }

        private void RadioCheckedChanged()
        {
            foreach (var panel in new Panel[] { this.widthPanel })
            {
                panel.Enabled = false;
            }

            switch (this.Operation)
            {
                case EditLevelDialogResult.WidthChange:
                    this.widthPanel.Enabled = true;
                    break;
            }
        }

        private EditLevelDialogResult Operation
        {
            get
            {
                if (this.editWidthRadio.Checked)
                {
                    return EditLevelDialogResult.WidthChange;
                }

                return EditLevelDialogResult.None;
            }
        }

        public int LevelWidth
        {
            get
            {
                return int.Parse(this.widthTextBox.Text);
            }
        }

        public EditLevelDialogResult Result
        {
            get
            {
                return this.result;
            }
        }
    }

    public enum EditLevelDialogResult
    {
        None,
        WidthChange
    }
}
