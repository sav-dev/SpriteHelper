using System;
using System.Windows.Forms;

namespace SpriteHelper
{
    public partial class ProgramPicker : Form
    {
        public ProgramPicker()
        {
            InitializeComponent();
        }

        private void AnimationButtonClick(object sender, EventArgs e)
        {
            new AnimationHelper().ShowDialog();
        }

        private void PalettesButtonClick(object sender, EventArgs e)
        {
            new PaletteProcessor().ShowDialog();
        }

        private void BackgroundButtonClick(object sender, EventArgs e)
        {
            new BackgroundTilesetCreator().ShowDialog();
        }

        private void LevelEditorButtonClick(object sender, EventArgs e)
        {
            new LevelEditor().ShowDialog();
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
