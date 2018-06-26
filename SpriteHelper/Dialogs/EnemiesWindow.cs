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

            builder.AppendLineFormat(GetHeader());

            builder.AppendLineFormat(GetPositionOffsetsComment());
            builder.AppendLineFormat(GetPositionOffsets());

            builder.AppendLineFormat(GetTilesAndAttsComment());
            builder.AppendLineFormat(GetTilesAndAtts());


            return builder.ToString();
        }

        private string GetHeader()
        {
            return @";****************************************************************
; Enemies                                                       ;
; Holds information about all enemies (auto-generated)          ;
;****************************************************************
";
        }

        private string GetPositionOffsetsComment()
        {
            return @";
;  all offsets for possible grids
;    XOffNxM = x offsets for NxM grid
;    XOffNxMH = x offsets for NxM grid (H flip)
;    YOffNxM = y offsets for NxM grid
;    YOffNxMV = y offsets for NxM grid (V flip)
;
";
        }

        private string GetPositionOffsets()
        {
            var builder = new StringBuilder();

            var dictionary = new Dictionary<string, List<Flip>>();
            foreach (var animation in this.config.Animations)
            {
                var firstFrame = animation.Frames.First();
                var type = string.Format("{0}x{1}", firstFrame.Width, firstFrame.Height);
                if (!dictionary.ContainsKey(type))
                {
                    dictionary.Add(type, new List<Flip>());
                }

                if (animation.Flip != Flip.None && !dictionary[type].Contains(animation.Flip))
                {
                    dictionary[type].Add(animation.Flip);
                }                
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

                if (flips.Contains(Flip.Horizontal))
                {
                    builder.AppendLineFormat("{0}:", xOffHKey);
                    builder.AppendLineFormat("  .byte {0}", string.Join(", ", offsetDictionary[xOffHKey].Select(o => "$" + o.ToString("X2"))));
                }

                if (flips.Contains(Flip.Vertical))
                {
                    builder.AppendLineFormat("{0}:", yOffVKey);
                    builder.AppendLineFormat("  .byte {0}", string.Join(", ", offsetDictionary[yOffVKey].Select(o => "$" + o.ToString("X2"))));
                }
            }

            return builder.ToString();
        }

        private string GetTilesAndAttsComment()
        {
            return @";
;  all information needed to draw an enemy
;  Format:
;    1 byte = sprite count (N)
;    8 bytes = pointers to offsets in order: xOff, yOff, xOffFlip, yOffFlip
;    1 byte = value to XOR with atts if flip
;    N bytes = atts
;    N bytes = frame M - 1
;    N bytes = frame M - 2
;    ...
;    N bytes = frame 1
;    N bytes = frame 0
;
";
        }

        private string GetTilesAndAtts()
        {
            var builder = new StringBuilder();

            foreach (var animation in this.config.Animations)
            {
                var firstFrame = animation.Frames.First();

                builder.AppendLineFormat("{0}:", animation.Name);
                
                // Sprite count.
                builder.AppendLineFormat(".spriteCount:");
                builder.AppendLineFormat("  .byte ${0:X2}", firstFrame.Width * firstFrame.Height);

                // Offsets pointers.
                builder.AppendLineFormat(".offsets:");

                // Order: xOff, yOff, xOffFlip, yOffFlip.               
                var type = string.Format("{0}x{1}", firstFrame.Width, firstFrame.Height);

                var xOffKey = "XOff" + type;
                var xOffPointer = $"LOW({xOffKey}), HIGH({xOffKey})";

                var yOffKey = "YOff" + type;
                var yOffPointer = $"LOW({yOffKey}), HIGH({yOffKey})";

                var xOffFlipKey = animation.Flip == Flip.Horizontal ? xOffKey + "H" : xOffKey;
                var xOffFlipPointer = $"LOW({xOffFlipKey}), HIGH({xOffFlipKey})";

                var yOffFlipKey = animation.Flip == Flip.Vertical ? yOffKey + "V" : yOffKey;
                var yOffFlipPointer = $"LOW({yOffFlipKey}), HIGH({yOffFlipKey})";

                builder.AppendLineFormat("  .byte {0}, {1}, {2}, {3}", xOffPointer, yOffPointer, xOffFlipPointer, yOffFlipPointer);

                // Flip XOR.
                builder.AppendLineFormat(".flipXor:");
                switch (animation.Flip)
                {
                    case Flip.None:
                        builder.AppendLineFormat("  .byte %00000000");
                        break;
                    case Flip.Horizontal:
                        builder.AppendLineFormat("  .byte %01000000");
                        break;
                    case Flip.Vertical:
                        builder.AppendLineFormat("  .byte %10000000");
                        break;
                }

                // Start with atts. We're assuming all animation frames have the same atts.
                var attsList = new List<int>();                
                for (var x = 0; x < firstFrame.Width; x++)
                {
                    for (var y = 0; y < firstFrame.Height; y++)
                    {
                        var sprite = firstFrame.Sprites[y * firstFrame.Width + x];

                        var mapping = sprite.ActualSprite.Mapping;
                        var atts = this.config.PaletteMappings[mapping].ToPalette;

                        if (sprite.HFlip)
                        {
                            atts += 64;
                        }

                        if (sprite.VFlip)
                        {
                            atts += 128;
                        }

                        attsList.Add(atts);
                    }
                }

                builder.AppendLine(".attributes:");
                builder.AppendLineFormat("  .byte {0}", string.Join(",", attsList.Select(a => "$" + a.ToString("X2"))));

                // Frames in descending order
                builder.AppendLine(".tiles:");
                for (var frameIndex = animation.Frames.Length - 1; frameIndex >= 0; frameIndex--)
                {
                    var frame = animation.Frames[frameIndex];
                    builder.AppendLineFormat(".{0}:", frame.Name.Replace(" ", ""));

                    var tileList = new List<int>();
                    for (var x = 0; x < frame.Width; x++)
                    {
                        for (var y = 0; y < frame.Height; y++)
                        {
                            var sprite = frame.Sprites[y * frame.Width + x];
                            tileList.Add(sprite.ActualSprite.Id);
                        }
                    }

                    builder.AppendLineFormat("  .byte {0}", string.Join(",", tileList.Select(t => "$" + t.ToString("X2"))));
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
