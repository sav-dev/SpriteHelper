using System;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
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
                case "Player":
                    new Player().ShowDialog();
                    break;
                case "Explosion":
                    new Explosion().ShowDialog();
                    break;
                ////case "Sprites":
                ////    new AnimationHelperV2().ShowDialog();
                ////    break;
                case "Palettes":
                    new PaletteProcessor().ShowDialog();
                    break;
                case "Background":
                    new BackgroundTilesetCreator().ShowDialog();
                    break;
                case "Level":
                    new LevelEditor().ShowDialog();
                    break;
                case "Animations":
                    new AnimationsDialog().ShowDialog();
                    break;
                case "CHR Combine":
                    new ChrCombine().ShowDialog();
                    break;
            }
        }

        private void PlayerButtonClick(object sender, EventArgs e)
        {
            new Player().ShowDialog();
        }

        private void ExplosionButtonClick(object sender, EventArgs e)
        {
            new Explosion().ShowDialog();
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

        private void AnimationsButtonClick(object sender, EventArgs e)
        {
            new AnimationsDialog().ShowDialog();
        }

        private void ChrButtonClick(object sender, EventArgs e)
        {
            new ChrCombine().ShowDialog();
        }

        private void EnemiesButtonClick(object sender, EventArgs e)
        {
            new EnemiesWindow().ShowDialog();
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
