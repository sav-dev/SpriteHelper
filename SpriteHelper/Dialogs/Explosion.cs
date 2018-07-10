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
    public partial class Explosion : Form
    {
        private SpriteConfig config;
        private Palettes palettes;
        
        public Explosion()
        {
            InitializeComponent();
            this.zoomPicker.Maximum = Constants.MaxZoom;
        }

        private void ExplosionLoad(object sender, EventArgs e)
        {
            if (Defaults.Instance.ApplyDefaults)
            {
                this.LoadFiles();
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
       
        private void LoadFiles()
        {
            this.palettes = Palettes.Read(palettesTextBox.Text);
            this.config = SpriteConfig.Read(specTextBox.Text, this.palettes);
            this.LoadAnimation();
        }

        private void LoadFrame()
        {
            var frame = (Frame)this.framesListBox.SelectedItem;
            this.pictureBox.BackColor = this.applyPaletteCheckbox.Checked ? this.palettes.SpritesPalette[0].ActualColors[0] : Color.White;

            this.pictureBox.Image = frame.GetExplosionBitmap(
                this.pictureBox.BackColor,
                this.applyPaletteCheckbox.Checked,
                (int)this.zoomPicker.Value);            
        }

        private void LoadAnimation()
        {
            this.Stop();

            var animation = this.config.Animations.First();
            this.timer.Interval = animation.AnimationSpeed > 0 ? (1000 / (Constants.Framerate / animation.AnimationSpeed)) : int.MaxValue;
            
            this.framesListBox.Items.Clear();
            foreach (var frame in animation.Frames)
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

        //////////////////////////

        private string GetCode()
        {
            var builder = new StringBuilder();

            var firstFrame = this.config.Frames.First();
            var firstFrameSprites = firstFrame.Sprites;
            var otherFrame = this.config.Frames.Last();

            /////// X OFF 1st

            builder.AppendLineFormat("explosionXOff1st:");
            var xOff1st = firstFrameSprites.Select(s => "$" + s.X.ToString("X2")).ToList();
            while (xOff1st.Count < Constants.ExplosionSprites)
            {
                xOff1st.Add("CLEAR_SPRITE");
            }

            builder.AppendLineFormat("  .byte {0}", string.Join(", ", xOff1st));

            /////// X OFF REST

            builder.AppendLineFormat("; Commented out for optimization, use XOff2x2 instead");

            builder.AppendLineFormat(";explosionXOffRest:");

            builder.AppendLineFormat(
                ";  .byte {0}",
                string.Join(", ", otherFrame.Sprites.Select(f => "$" + f.X.ToString("X2"))));

            /////// Y OFF 1st

            builder.AppendLineFormat("explosionYOff1st:");
            var yOff1st = firstFrameSprites.Select(s => "$" + s.Y.ToString("X2")).ToList();
            while (yOff1st.Count < Constants.ExplosionSprites)
            {
                yOff1st.Add("CLEAR_SPRITE");
            }

            builder.AppendLineFormat("  .byte {0}", string.Join(", ", yOff1st));

            /////// Y OFF REST

            builder.AppendLineFormat("; Commented out for optimization, use YOff2x2 instead");

            builder.AppendLineFormat(";explosionYOffRest:");

            builder.AppendLineFormat(
                ";  .byte {0}",
                string.Join(", ", otherFrame.Sprites.Select(f => "$" + f.Y.ToString("X2"))));

            ///////// ATTS
            //
            //builder.AppendLineFormat("explosionAtts:");
            //
            //builder.AppendLineFormat(
            //    "  .byte {0}",
            //    string.Join(", ", otherFrame.Sprites.Select(s =>
            //        {
            //            var atts = s.Mapping;
            //            if (s.Reversed)
            //            {
            //                atts += 64;
            //            }
            //
            //            return "$" + atts.ToString("X2");
            //        })));
            // no need for this.
            // !!! one sprite can be saved for cost of additional processing if needed !!!

            /////// TILES

            // if there are 4 frames, they are executed in order: 4 -> 3 -> 2 -> 1
            // so start with the last one etc.

            builder.AppendLineFormat("explosionTiles:");
            for (var i = this.config.Animations.First().Frames.Length - 1; i >= 0; i--)
            {
                var frame = this.config.Animations.First().Frames[i];
                var sprites = new List<string>();
                for (var j = 0; j < Constants.ExplosionSprites; j++)
                {
                    var sprite = frame.Sprites.Length > j ? frame.Sprites[j] : null;
                    if (sprite == null)
                    {
                        sprites.Add("CLEAR_SPRITE");
                    }
                    else
                    {
                        var tileId = sprite.Id;
                        sprites.Add("$" + tileId.ToString("X2"));
                    }
                }

                builder.AppendLineFormat("  .byte {0}", string.Join(", ", sprites));
            }

            return builder.ToString();
        }
    }
}
