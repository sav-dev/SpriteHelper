using System;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class ProgramPicker : Form
    {
        Action<object, EventArgs> singleProgram;

        bool startLevelEditor = false;

        public ProgramPicker(string[] args)
        {
            startLevelEditor = (args != null && args.Length > 0 && args[0] == "LevelEditor");
            InitializeComponent();
        }

        private void ProgramPickerLoad(object sender, EventArgs e)
        {
            if (this.singleProgram != null)
            {
                this.singleProgram(null, null);
            }
            else if (startLevelEditor)
            {
                this.LevelEditorButtonClick(null, null);
            }
        }

        private void ShowDialog(Form form)
        {
            if (this.singleProgram != null)
            {
                form.FormClosed += ChildFormClosed;
            }

            form.ShowDialog();
        }

        private void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void PlayerButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new Player());
        }

        private void ExplosionButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new Explosion());
        }

        private void PalettesButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new PaletteProcessor());
        }

        private void BackgroundButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new BackgroundTilesetCreator());
        }

        private void LevelEditorButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new LevelEditor());
        }

        private void AnimationsButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new AnimationsDialog());
        }

        private void ChrButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new ChrCombine());
        }

        private void ChrProcessButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new ChrProcess());
        }

        private void EnemiesButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new EnemiesWindow());
        }

        private void BulletsButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new BulletsWindow());
        }

        private void TilesetViewerButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new TilesetViewer());
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TitleButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new TitleDialog());
        }

        private void StoryButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new StoryDialog());
        }

        private void StageSelectButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new StageSelectDialog());
        }

        private void StringConfigGenButtonClick(object sender, EventArgs e)
        {
            this.ShowDialog(new startingIdLabel());
        }
    }
}
