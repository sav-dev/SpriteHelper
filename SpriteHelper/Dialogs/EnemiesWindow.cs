using SpriteHelper.Contract;
using SpriteHelper.Files;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class EnemiesWindow : Form
    {
        private SpriteConfig config;
        private Palettes palettes;

        public EnemiesWindow()
        {
            InitializeComponent();
        }

        private void EnemiesWindowLoad(object sender, System.EventArgs e)
        {
            if (Defaults.Instance.ApplyDefaults)
            {
                this.LoadFiles();
            }
        }

        private void LoadButtonClick(object sender, System.EventArgs e)
        {
            this.LoadFiles();
        }

        private void LoadFiles()
        {
            this.palettes = Palettes.Read(palettesTextBox.Text);
            this.config = SpriteConfig.Read(specTextBox.Text, this.palettes);

            this.framesListBox.Items.Clear();
            this.enemiesListBox.Items.Clear();
            foreach (var animation in config.Animations)
            {
                this.enemiesListBox.Items.Add(animation);
            }

            this.enemiesListBox.SelectedIndex = 0;
        }

        private void ZoomPickerValueChanged(object sender, EventArgs e)
        {
            this.LoadFrame();
        }

        private void InputCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.LoadFrame();
        }

        private void FramesListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadFrame();
        }

        private void EnemiesListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadAnimation();
        }    

        private void LoadAnimation()
        {
            ////this.Stop();

            var selectedEnemy = (Animation)this.enemiesListBox.SelectedItem;

            ////this.timer.Interval = selectedAnimation.FPS > 0 ? (1000 / selectedAnimation.FPS) : int.MaxValue;

            this.framesListBox.Items.Clear();
            foreach (var frame in selectedEnemy.Frames)
            {
                this.framesListBox.Items.Add(frame);
            }

            this.framesListBox.SelectedIndex = 0;

            ////this.Start();
        }

        private void LoadFrame()
        {
            var frame = (Frame)this.framesListBox.SelectedItem;
            this.pictureBox.BackColor = this.applyPaletteCheckbox.Checked ? this.palettes.SpritesPalette[0].ActualColors[0] : Color.Black;

            this.pictureBox.Image = frame.GetGridBitmap(
                this.pictureBox.BackColor,
                this.applyPaletteCheckbox.Checked,
                this.showBoxesCheckbox.Checked,
                this.verticalFlipCheckbox.Checked,
                this.horizontalFlipCheckbox.Checked,
                (int)this.zoomPicker.Value);

        }
    }
}
