using SpriteHelper.Contract;
using System;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class StageSelectDialog : Form
    {
        private Stages stages;
        private StringsConfig stringsConfig;

        public StageSelectDialog()
        {
            InitializeComponent();
            this.stagesTextBox.Text = FileConstants.Stages;
            this.fontTextBox.Text = FileConstants.Font;
            this.stringsTextBox.Text = FileConstants.Strings;
        }     

        private void LoadButtonClick(object sender, EventArgs e)
        {
            this.stages = Stages.Read(this.stagesTextBox.Text);
            this.stringsConfig = StringsConfig.Read(this.stringsTextBox.Text);

            // todo 0005: implement (or remove if not needed; also remove stage.cs and FileConstants.Stages in that case)
        }

        private void CodeButtonClick(object sender, EventArgs e)
        {
            // todo 0005: implement (or remove if not needed; also remove stage.cs and FileConstants.Stages in that case)
        }
    }
}
