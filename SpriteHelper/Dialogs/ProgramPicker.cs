using System;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class ProgramPicker : Form
    {
        bool startLevelEditor = false;

        public ProgramPicker(string[] args)
        {
            startLevelEditor = (args != null && args.Length > 0 && args[0] == "LevelEditor");
            InitializeComponent();
        }

        private void ProgramPickerLoad(object sender, EventArgs e)
        {
            if (startLevelEditor)
            {
                this.LevelEditorButtonClick(null, null);
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

        private void ChrProcessButtonClick(object sender, EventArgs e)
        {
            new ChrProcess().ShowDialog();
        }

        private void EnemiesButtonClick(object sender, EventArgs e)
        {
            new EnemiesWindow().ShowDialog();
        }

        private void BulletsButtonClick(object sender, EventArgs e)
        {
            new BulletsWindow().ShowDialog();
        }

        private void TilesetViewerButtonClick(object sender, EventArgs e)
        {
            new TilesetViewer().ShowDialog();
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TitleButtonClick(object sender, EventArgs e)
        {
            new TitleDialog().ShowDialog();
        }

        private void StoryButtonClick(object sender, EventArgs e)
        {
            new StoryDialog().ShowDialog();
        }

        private void StageSelectButtonClick(object sender, EventArgs e)
        {
            new StageSelectDialog().ShowDialog();
        }

        private void StringConfigGenButtonClick(object sender, EventArgs e)
        {
            new startingIdLabel().ShowDialog();
        }
    }
}
