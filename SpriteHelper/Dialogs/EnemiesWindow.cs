using SpriteHelper.Contract;
using SpriteHelper.Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void TimerTick(object sender, EventArgs e)
        {
            this.NextFrame();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            this.Start();
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            this.Stop();
        }

        private void CodeButtonClick(object sender, EventArgs e)
        {
            var code = this.GetCode();
            new CodeWindow(code).ShowDialog();
        }

        private void LoadAnimation()
        {
            this.Stop();

            var selectedEnemy = (Animation)this.enemiesListBox.SelectedItem;

            this.timer.Interval = selectedEnemy.FPS > 0 ? (1000 / selectedEnemy.FPS) : int.MaxValue;

            this.framesListBox.Items.Clear();
            foreach (var frame in selectedEnemy.Frames)
            {
                this.framesListBox.Items.Add(frame);
            }

            this.framesListBox.SelectedIndex = 0;

            this.Start();
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

        private string GetCode()
        {
            var builder = new StringBuilder();

            builder.AppendLineFormat(GetPositionOffsets());

            return builder.ToString();
        }

        private string GetPositionOffsets()
        {
            var builder = new StringBuilder();

            var dictionary = new Dictionary<string, List<string>>();
            foreach (var animation in this.config.Animations)
            {
                var firstFrame = animation.Frames.First();
                var type = string.Format("{0}x{1}", firstFrame.Width, firstFrame.Height);
                if (!dictionary.ContainsKey(type))
                {
                    dictionary.Add(type, new List<string>());
                }

                var flips = animation.Flips.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                dictionary[type] = dictionary[type].Union(flips).ToList();
            }

            foreach (var kvp in dictionary)
            {
                var type = kvp.Key;
                var flips = kvp.Value;

                var split = type.Split('x').Select(int.Parse).ToArray();

                var offsetDictionary = new Dictionary<string, List<int>>();

                var xOffKey = "XOff" + type;
                var xOffHKey = xOffKey + "H";
                var yOffKey = "YOff" + type;
                var yOffVKey = yOffKey + "V";

                offsetDictionary.Add(xOffKey, new List<int>());
                offsetDictionary.Add(xOffHKey, new List<int>());
                offsetDictionary.Add(yOffKey, new List<int>());
                offsetDictionary.Add(yOffVKey, new List<int>());

                // This order is important as we will render like this:
                //   0 2 4
                //   1 3 5
                for (var x = 0; x < split[0]; x++) 
                {
                    for (var y = 0; y < split[1]; y++)
                    {
                        var xOff = x * Constants.SpriteWidth;
                        var yOff = y * Constants.SpriteHeight;
                        var xOffH = (split[0] - 1) * Constants.SpriteWidth - x * Constants.SpriteWidth;
                        var yOffV = (split[1] - 1) * Constants.SpriteHeight - y * Constants.SpriteHeight;

                        offsetDictionary[xOffKey].Add(xOff);
                        offsetDictionary[xOffHKey].Add(xOffH);
                        offsetDictionary[yOffKey].Add(yOff);
                        offsetDictionary[yOffVKey].Add(yOffV);
                    }
                }

                builder.AppendLineFormat("{0}:", xOffKey);
                builder.AppendLineFormat("  .byte {0}", string.Join(", ", offsetDictionary[xOffKey].Select(o => "$" + o.ToString("X2"))));

                builder.AppendLineFormat("{0}:", yOffKey);
                builder.AppendLineFormat("  .byte {0}", string.Join(", ", offsetDictionary[yOffKey].Select(o => "$" + o.ToString("X2"))));

                if (flips.Contains("H"))
                {
                    builder.AppendLineFormat("{0}:", xOffHKey);
                    builder.AppendLineFormat("  .byte {0}", string.Join(", ", offsetDictionary[xOffHKey].Select(o => "$" + o.ToString("X2"))));
                }

                if (flips.Contains("V"))
                {
                    builder.AppendLineFormat("{0}:", yOffVKey);
                    builder.AppendLineFormat("  .byte {0}", string.Join(", ", offsetDictionary[yOffVKey].Select(o => "$" + o.ToString("X2"))));
                }
            }

            return builder.ToString();
        }
    }
}
