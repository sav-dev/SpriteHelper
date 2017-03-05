using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SpriteHelper
{
    public partial class AnimationHelper : Form
    {
        private MyBitmap image;
        private SpriteConfig config;
        private Palettes palettes;

        public AnimationHelper()
        {
            InitializeComponent();
            this.zoomPicker.Maximum = Constants.MaxZoom;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            if (Defaults.Instance.ApplyDefaults)
            {
                this.PreLoad();
            }
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            this.LoadFiles();
        }

        private void FramesListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadFrame();
        }

        private void AnimationsListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadAnimation();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            this.Start();
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            this.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            this.NextFrame();
        }

        private void ApplyPaletteCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.LoadFrame();
        }

        private void ZoomPickerValueChanged(object sender, EventArgs e)
        {
            this.LoadFrame();
        }

        private void PreLoad()
        {
            this.imageTextBox.Text = Defaults.Instance.AnimationImage;
            this.specTextBox.Text = Defaults.Instance.AnimationSpec;
            this.palettesTextBox.Text = Defaults.Instance.PalettesSpec;
            this.LoadFiles();
        }

        private void LoadFiles()
        {
            this.image = MyBitmap.FromFile(imageTextBox.Text);            
            this.palettes = Palettes.Read(palettesTextBox.Text);
            this.config = SpriteConfig.Read(specTextBox.Text, this.image, this.palettes);

            this.framesListBox.Items.Clear();
            this.animationsListBox.Items.Clear();
            foreach (var animation in config.Animations)
            {
                this.animationsListBox.Items.Add(animation);
            }

            this.animationsListBox.SelectedIndex = 0;
        }

        private void LoadFrame()
        {
            var frame = (Frame)this.framesListBox.SelectedItem;
            this.pictureBox.BackColor = this.applyPaletteCheckbox.Checked ? this.palettes.SpritesPalette[0].ActualColors[0] : Color.White;
            this.pictureBox.Image = frame.GetBitmap(this.config, this.pictureBox.BackColor, this.applyPaletteCheckbox.Checked, (int)this.zoomPicker.Value);
            
        }

        private void LoadAnimation()
        {
            this.Stop();

            var selectedAnimation = (Animation)this.animationsListBox.SelectedItem;

            this.timer.Interval = selectedAnimation.FPS > 0 ? (1000 / selectedAnimation.FPS) : int.MaxValue;

            this.framesListBox.Items.Clear();
            foreach (var frame in selectedAnimation.Frames)
            {
                this.framesListBox.Items.Add(frame);
            }

            this.framesListBox.SelectedIndex = 0;

            this.Start();
        }

        private void Start()
        {
            this.startButton.Enabled = false;
            this.stopButton.Enabled = true;
            this.timer.Start();
        }

        private void Stop()
        {
            this.stopButton.Enabled = false;
            this.startButton.Enabled = true;
            this.timer.Stop();
        }

        private void NextFrame()
        {
            var frameCount = framesListBox.Items.Count;
            var selectedFrame = framesListBox.SelectedIndex;
            framesListBox.SelectedIndex = (selectedFrame + 1) % frameCount;
        }
    }
}
