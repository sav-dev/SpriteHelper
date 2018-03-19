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
        private int animationCounter;

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

        private void DirectionCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            this.LoadFrame();
        }

        private void ShowBoxesCheckBoxCheckedChanged(object sender, EventArgs e)
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
            this.outputTextBox.Text = Defaults.Instance.AnimationOutput;
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
            this.pictureBox.Image = frame.GetBitmap(
                this.config, 
                this.pictureBox.BackColor, 
                this.applyPaletteCheckbox.Checked, 
                this.showBoxesCheckBox.Checked,
                this.directionCheckBox.Checked,
                (int)this.zoomPicker.Value);
            
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

        private void AnimationPickerValueChanged(object sender, EventArgs e)
        {
            this.animationCounter = 0;
        }

        private void ExportButtonClick(object sender, EventArgs e)
        {
            // Generate actual chr file
            // CHR format:
            //  each sprite is 16 bytes:
            //  first 8 bytes are low bits per sprite row
            //  second 8 bytes are high bits per sprite row
            var bytes = new List<byte>();
            foreach (var sprite in this.config.Sprites)
            {
                var lowBits = new List<byte>();
                var highBits = new List<byte>();
                var image = sprite.GetSprite();

                for (var y = 0; y < Constants.SpriteHeight; y++)
                {
                    byte lowBit = 0;
                    byte highBit = 0;
            
                    for (var x = 0; x < Constants.SpriteWidth; x++)
                    {
                        lowBit = (byte)(lowBit << 1);
                        highBit = (byte)(highBit << 1);
            
                        var pixel = this.config.PaletteMappings[sprite.Mapping].ColorMappings.First(c => c.Color == image.GetPixel(x, y)).To;

                        if (pixel == 1 || pixel == 3)
                        {
                            // low bit set
                            lowBit |= 1;
                        }
            
                        if (pixel == 2 || pixel == 3)
                        {
                            // high bit set
                            highBit |= 1;
                        }
                    }
            
                    lowBits.Add(lowBit);
                    highBits.Add(highBit);
                }
            
                bytes.AddRange(lowBits);
                bytes.AddRange(highBits);
            }
            
            while (bytes.Count < 4096)
            {
                bytes.Add(0);
            }

            if (File.Exists(this.outputTextBox.Text))
            {
                File.Delete(this.outputTextBox.Text);
            }

            File.WriteAllBytes(this.outputTextBox.Text, bytes.ToArray());
        }

        private void CodeButtonClick(object sender, EventArgs e)
        {
            var code = this.GetCode();
            new CodeWindow(code).ShowDialog();
        }

        //////////////////////////

        private string GetCode()
        {
            var builder = new StringBuilder();

            /////// STAND RENDER ///////

            var sprites = 
                this.config.Frames.First(a => a.Name == "Stand").Sprites.OrderBy(s => s.GameSprite).ToList();

            builder.AppendLineFormat("initialTiles:");
            builder.AppendLineFormat(
                "  .byte {0}, CLEAR_SPRITE", 
                string.Join(", ", sprites.Select(s => "$" + s.Id.ToString("X2"))));

            sprites.Add(
                this.config.Frames.First(a => a.Name == "Run 2").Sprites.First(s => s.GameSprite == Constants.PlayerSprites - 1));

            builder.AppendLineFormat("initialAtts:");
            builder.AppendLineFormat(
                "  .byte {0}",
                string.Join(", ", sprites.Select(s => "$" + s.ActualSprite.Mapping.ToString("X2"))));

            builder.AppendLineFormat("initialXOff:");
            builder.AppendLineFormat(
                "  .byte {0}",
                string.Join(", ", sprites.Select(s =>
                {
                    var xOffset = s.X - this.config.X;
                    xOffset = 256 + xOffset;
                    xOffset = xOffset % 256;
                    return "$" + xOffset.ToString("X2");
                })));
            
            builder.AppendLineFormat("initialYOff:");
            builder.AppendLineFormat(
                "  .byte {0}",
                string.Join(", ", sprites.Select(s =>
                {
                    var yOffset = s.Y - this.config.Y - 1; // -1 for scan line
                    yOffset = 256 + yOffset;
                    yOffset = yOffset % 256;
                    return "$" + yOffset.ToString("X2");
                })));
            
            // ANIMATIONS

            GetAnimation(builder, "Stand");
            GetAnimation(builder, "Jump");
            GetAnimation(builder, "Crouch");
            GetAnimation(builder, "Run");

            return builder.ToString();
        }

        private string GetAnimation(StringBuilder builder, string name)
        {
            var animation = this.config.Animations.First(f => f.Name == name);

            builder.AppendLineFormat("tiles{0}:", name);

            // if there are 4 frames, they are executed in order: 4 -> 3 -> 2 -> 1
            // so start with the last one etc.

            for (var i = animation.Frames.Length - 1; i >= 0; i--)
            {
                var frame = animation.Frames[i];
                var sprites = new List<string>();
                for (var j = 4; j < Constants.PlayerSprites; j++)
                {
                    var sprite = frame.Sprites.FirstOrDefault(s => s.GameSprite == j);
                    if (sprite == null)
                    {
                        sprites.Add("CLEAR_SPRITE");
                    }
                    else
                    {
                        sprites.Add("$" + sprite.Id.ToString("X2"));
                    }
                }

                builder.AppendLineFormat("  .byte {0} ; {1}", string.Join(", ", sprites), frame.Name);
            }
        
            return builder.ToString();
        }
    }
}
