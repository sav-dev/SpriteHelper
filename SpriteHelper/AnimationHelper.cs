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
            this.codeTextBox.Text = Defaults.Instance.PlayerGeneratedOutput;
            this.LoadFiles();
            //this.CodeButtonClick(null, null);
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
            File.WriteAllText(this.codeTextBox.Text, code);
            new CodeWindow(code).ShowDialog();
        }

        //////////////////////////

        private string GetCode()
        {
            var code = File.ReadAllText("RenderPlayerCode.txt");
            return string.Format(
                code,
                GetStateCode("Crouch"),
                GetStateCode("Crouch", true),
                GetStateCode("Stand"),
                GetStateCode("Stand", true),
                GetStateCode("Jump"),
                GetStateCode("Jump", true),
                GetStateCode("Run"),
                GetStateCode("Run", true));
        }

        private string GetStateCode(string name, bool left = false)
        {
            var state = this.config.Animations.First(a => a.Name == name);
            
            if (state.Frames.Length == 1)
            {
                return GetFrameCode(state.Frames.First(), left);
            }
            else
            {
                var builder = new StringBuilder();
                builder.AppendLineFormat("  LDA playerAnimationFrame");
                for (var i = state.Frames.Length; i >= 2; i--)
                {                    
                    builder.AppendLineFormat("  CMP #${0}", i.ToString("X2"));
                    builder.AppendLineFormat("  BEQ .jump{0}{1}{2}", name, left ? "Left" : "Right", i);
                }

                for (var i = 1; i <= state.Frames.Length; i++)
                {
                    builder.AppendLineFormat(".jump{0}{1}{2}:", name, left ? "Left" : "Right", i);
                    builder.AppendLineFormat("  JMP .{0}{1}{2}", name, left ? "Left" : "Right", i);                   
                }

                for (var i = 1; i <= state.Frames.Length; i++)
                {
                    builder.AppendLineFormat(".{0}{1}{2}:", name, left ? "Left" : "Right", i);
                    builder.AppendLine(GetFrameCode(state.Frames[state.Frames.Length - i], left));
                    builder.AppendLineFormat("  RTS", name, i);
                }

                return builder.ToString();
            }
        }

        private string GetFrameCode(Frame frame, bool left)
        {
            const int MaxSprites = 9;

            var builder = new StringBuilder();
            builder.AppendLineFormat(";{0}", frame.Name);
            for (var i = 0; i < frame.Sprites.Length; i++)
            {
                builder.AppendLine(GetSpriteCode(frame, i, left));
            }

            for (var i = frame.Sprites.Length; i < MaxSprites; i++)
            {
                builder.AppendLineFormat("  LDA #CLEAR_SPRITE");
                builder.AppendLineFormat("  STA SPRITES_PLAYER + Y_OFF + SPRITE_SIZE * ${0}", i.ToString("X2"));
            }

            return builder.ToString();
        }

        private string GetSpriteCode(Frame frame, int i, bool left)
        {
            var builder = new StringBuilder();
            var sprite = frame.Sprites[i];

            builder.AppendLineFormat("  LDA #${0}", sprite.Id.ToString("X2"));
            builder.AppendLineFormat("  STA SPRITES_PLAYER + TILE_OFF + SPRITE_SIZE * ${0}", i.ToString("X2"));

            var attributes = sprite.ActualSprite.Mapping;
            if (left)
            {
                attributes += 64; // 64 = %01000000 = flip horizontally flag
            }

            builder.AppendLineFormat("  LDA #${0}", attributes.ToString("X2"));
            builder.AppendLineFormat("  STA SPRITES_PLAYER + ATTS_OFF + SPRITE_SIZE * ${0}", i.ToString("X2"));

            builder.AppendLineFormat("  LDA playerX");

            var x = left ? (2 * config.X - sprite.X + Constants.SpriteWidth) : sprite.X;
            var xOffset = x - this.config.X;
            
            if (xOffset > 0)
            {
                builder.AppendLineFormat("  CLC");
                builder.AppendLineFormat("  ADC #${0}", xOffset.ToString("X2"));
            }
            else if (xOffset < 0)
            {
                builder.AppendLineFormat("  SEC");
                builder.AppendLineFormat("  SBC #${0}", Math.Abs(xOffset).ToString("X2"));
            }

            builder.AppendLineFormat("  STA SPRITES_PLAYER + X_OFF + SPRITE_SIZE * ${0}", i.ToString("X2"));

            var y = 0;
            builder.AppendLineFormat("  LDA #${0}", (sprite.Y + 50).ToString("X2"));
            builder.AppendLineFormat("  STA SPRITES_PLAYER + Y_OFF + SPRITE_SIZE * ${0}", i.ToString("X2"));

            return builder.ToString();
        }
    }
}
