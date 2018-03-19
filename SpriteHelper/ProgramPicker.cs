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

        private void ProgramPickerLoad(object sender, EventArgs e)
        {
            if (Defaults.Instance.ApplyDefaults)
            {
                this.PreLoad();
            }
        }

        private void PreLoad()
        {
            switch (Defaults.Instance.DefaultApp)
            {
                case "Animation":
                    new AnimationHelper().ShowDialog();
                    break;
                case "Palettes":
                    new PaletteProcessor().ShowDialog();
                    break;
                case "Background":
                    new BackgroundTilesetCreator().ShowDialog();
                    break;
                case "Level":
                    new LevelEditor().ShowDialog();
                    break;
                case "CHR Combine":
                    new ChrCombine().ShowDialog();
                    break;
            }
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

        private void EnemiesButtonClick(object sender, EventArgs e)
        {

        }

        private void ChrButtonClick(object sender, EventArgs e)
        {

        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
