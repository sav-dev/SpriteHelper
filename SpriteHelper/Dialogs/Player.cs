using SpriteHelper.Contract;
using SpriteHelper.Files;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class Player : Form
    {
        private SpriteConfig config;
        private Palettes palettes;
        
        public Player()
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
            this.specTextBox.Text = Defaults.Instance.PlayerSpec;
            this.palettesTextBox.Text = Defaults.Instance.PalettesSpec;
            this.outputTextBox.Text = Defaults.Instance.AnimationOutput;
            this.LoadFiles();
        }

        private void LoadFiles()
        {
            this.palettes = Palettes.Read(palettesTextBox.Text);
            this.config = SpriteConfig.Read(specTextBox.Text, this.palettes);

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

            this.pictureBox.Image = frame.GetPlayerBitmap(
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

        private void CodeButtonClick(object sender, EventArgs e)
        {
            var code = this.GetCode();
            new CodeWindow(code).ShowDialog();
        }

        public static int GetYOffset(Sprite sprite)
        {
            return sprite.Y - Constants.PlayerYOffset;
        }

        public static int GetXOffset(Sprite sprite, bool hFlip)
        {
            return hFlip ? Constants.PlayerXOffset - sprite.X + Constants.SpriteWidth : sprite.X - Constants.PlayerXOffset;
        }

        private string GetCode()
        {
            var builder = new StringBuilder();
          
            // Sprite positions

            var spritesNonCrouch = 
                this.config.Frames.First(a => a.Name == "Stand").Sprites.OrderBy(s => s.GameSprite).ToList();

            spritesNonCrouch.Add(
                this.config.Frames.First(a => a.Name == "Run 2").Sprites.First(s => s.GameSprite == Constants.PlayerSprites - 1));

            builder.AppendLineFormat("playerXOffRight:");
            builder.AppendLineFormat(
                "  .byte {0}",
                string.Join(", ", spritesNonCrouch.Select(s =>
                {
                    var xOffset = GetXOffset(s, false);
                    xOffset = 256 + xOffset;
                    xOffset = xOffset % 256;
                    return "$" + xOffset.ToString("X2");
                })));

            builder.AppendLineFormat("playerXOffLeft:");
            builder.AppendLineFormat(
                "  .byte {0}",
                string.Join(", ", spritesNonCrouch.Select(s =>
                {
                    var xOffset = GetXOffset(s, true);
                    xOffset = 256 + xOffset;
                    xOffset = xOffset % 256;
                    return "$" + xOffset.ToString("X2");
                })));

            builder.AppendLineFormat("playerYOffNonCrouch:");
            builder.AppendLineFormat(
                "  .byte {0}",
                string.Join(", ", spritesNonCrouch.Select(s =>
                {
                    var yOffset = GetYOffset(s);
                    yOffset = 256 + yOffset;
                    yOffset = yOffset % 256;
                    return "$" + yOffset.ToString("X2");
                })));

            var spritesCrouch = this.config.Frames.First(a => a.Name == "Crouch").Sprites.ToDictionary(s => s.GameSprite, s =>
            {
                var yOffset = GetYOffset(s);
                yOffset = 256 + yOffset;
                yOffset = yOffset % 256;
                return "$" + yOffset.ToString("X2");
            });

            for (var i = 0; i < Constants.PlayerSprites; i++)
            {
                if (!spritesCrouch.ContainsKey(i))
                {
                    spritesCrouch.Add(i, "CLEAR_SPRITE");
                }                
            }

            builder.AppendLineFormat("playerYOffCrouch:");
            builder.AppendLineFormat("  .byte {0}", string.Join(", ", spritesCrouch.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value)));

            builder.AppendLineFormat("playerAttsRight:");
            builder.AppendLineFormat(
                "  .byte {0}",
                string.Join(", ", spritesNonCrouch.Select(s => "$" + s.ActualSprite.Mapping.ToString("X2"))));            

            builder.AppendLineFormat("playerAttsLeft:");
            builder.AppendLineFormat(
                "  .byte {0}",
                string.Join(", ", spritesNonCrouch.Select(s => "$" + (s.ActualSprite.Mapping + 64).ToString("X2"))));

            GetTiles(builder, "Stand");
            GetTiles(builder, "Jump");
            GetTiles(builder, "Crouch");
            GetTiles(builder, "Run");

            return builder.ToString();
        }

        private string GetTiles(StringBuilder builder, string name)
        {
            var animation = this.config.Animations.First(f => f.Name == name);

            builder.AppendLineFormat("playerTiles{0}:", name);

            // if there are 4 frames, they are executed in order: 4 -> 3 -> 2 -> 1
            // so start with the last one etc.

            for (var i = animation.Frames.Length - 1; i >= 0; i--)
            {
                var frame = animation.Frames[i];
                var sprites = new List<string>();
                for (var j = 0; j < Constants.PlayerSprites; j++)
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
