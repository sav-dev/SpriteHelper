using SpriteHelper.Contract;
using SpriteHelper.Files;
using SpriteHelper.NesGraphics;
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
        private SpriteConfig bulletConfig;
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
            this.bulletConfig = SpriteConfig.Read(bulletsTextBox.Text, this.palettes);

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

            this.timer.Interval = selectedEnemy.AnimationSpeed > 0 ? (1000 / (Constants.Framerate / selectedEnemy.AnimationSpeed)) : int.MaxValue;

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

            var animation = (Animation)this.enemiesListBox.SelectedItem;
            var flip = this.flipCheckbox.Checked;
            var zoom = (int)this.zoomPicker.Value;

            var image = frame.GetGridBitmap(
                this.pictureBox.BackColor,
                this.applyPaletteCheckbox.Checked,
                this.showBoxesCheckbox.Checked,
                animation.Flip == Flip.Vertical && flip,
                animation.Flip == Flip.Horizontal && flip,
                zoom,
                animation.Offsets);

            if (this.showBulletCheckBox.Checked)
            {
                image = DrawBullet(image, animation, flip, zoom);
            }

            this.pictureBox.Image = image;
        }

        private Bitmap DrawBullet(Bitmap image, Animation animation, bool flip, int zoom)
        {
            var width = image.Width;
            var height = image.Height;

            var xOff = Constants.SpriteWidth * zoom;
            var yOff = Constants.SpriteHeight * zoom;

            var newImage = new MyBitmap(width + xOff * 2, height + yOff * 2, this.pictureBox.BackColor);
            
            newImage.DrawImage(MyBitmap.FromBitmap(image), xOff, yOff);

            if (animation.Offsets.GunXOff >= 0)
            {
                var bullet = this.bulletConfig.Bullets.First(b => b.BulletId == animation.BulletId);
                var flipFlags = bullet.GetFlipFlags(flip);
                var bulletBitmap = bullet.Sprite.GetSprite(
                    this.applyPaletteCheckbox.Checked,
                    flipFlags.HasFlag(ImageFlags.VFlip),
                    flipFlags.HasFlag(ImageFlags.HFlip),
                    this.pictureBox.BackColor).Clone();

                var scaled = bulletBitmap.Scale(zoom);
                if (flip)
                {
                    newImage.DrawImage(
                        scaled, 
                        xOff + animation.Offsets.GunXOffFlip * zoom, 
                        yOff + animation.Offsets.GunYOffFlip * zoom);
                }
                else
                {
                    newImage.DrawImage(
                        scaled, 
                        xOff + animation.Offsets.GunXOff * zoom, 
                        yOff + animation.Offsets.GunYOff * zoom);
                }
            }


            return newImage.ToBitmap();
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

            builder.AppendLineFormat(GetConstantPropertiesComment());
            builder.AppendLineFormat(GetConstantProperties());

            builder.AppendLineFormat(GetPositionOffsetsComment());
            builder.AppendLineFormat(GetPositionOffsets());

            builder.AppendLineFormat(GetTilesAndAttsComment());
            builder.AppendLineFormat(GetTilesAndAtts());

            builder.AppendLineFormat(GetFooter());

            return builder.ToString();
        }

        private string GetHeader()
        {
            return @"EnemiesStart:

;****************************************************************
; Enemies                                                       ;
; Holds information about all enemies (auto-generated)          ;
; POITAG - possible optimization - bytes savings - lots of      ;
; duplication here, especially with cloned palette-swap enemies ;
;****************************************************************
";
        }

        private string GetFooter()
        {
            return @"EnemiesEnd:";
        }

        private string GetConstantPropertiesComment()
        {
            return @";
;  constant properties for enemies
;    width           : 1 byte
;    hitbox x off    : 1 byte
;    hitbox width    : 1 byte (inclusive)
;    hitbox y off    : 1 byte
;    hitbox height   : 1 byte (inclusive)
;    orientation     : 1 byte (see ORIENTATION_* consts)
;    bullet pointer  : 1 byte
;    gun x off       : 1 byte (signed, 0 for non shooting)
;    gun y off       : 1 byte (signed, 0 for non shooting)
;    gun x off flip  : 1 byte (signed, 0 for non shooting)
;    gun y off flip  : 1 byte (signed, 0 for non shooting)
;    animation speed : 1 byte (0 for non animated)
;    # of frames     : 1 bytes
;    rendering info  : 2 bytes
;    expl. offsets   : 2 bytes (x/y)
;    expl. id        : 1 byte
;
;  ordered by animation id
;
;  tag: depends_on_enemy_consts_format
";
        }

        private string GetConstantProperties()
        {
            var builder = new StringBuilder();

            builder.AppendLine("EnemyConsts:");
            builder.AppendLine();
            foreach (var animation in this.config.Animations.OrderBy(a => a.Id))
            {
                builder.AppendLineFormat("{0}Consts:", animation.Name.Replace(" ", ""));

                var firstFrame = animation.Frames.First();
                var widthInPixes = firstFrame.Width * Constants.SpriteWidth;
                var heightInPixels = firstFrame.Height * Constants.SpriteHeight;

                builder.AppendLine(".width:");
                builder.AppendLineFormat("  .byte ${0:X2}", widthInPixes);

                var hitbox = new[] { animation.Offsets.XOff, animation.Offsets.Width, animation.Offsets.YOff, animation.Offsets.Height };
                builder.AppendLine(".hitboxInfo:");
                builder.AppendLineFormat("  .byte {0}", string.Join(",", hitbox.Select(v => "$" + v.ToString("X2"))));

                int[] gun;

                if (animation.Offsets.GunXOff >= 0)
                {
                    switch (animation.Flip)
                    {
                        case Flip.Horizontal:
                            gun = new[]
                                {
                                    256 + animation.Offsets.GunXOff,
                                    256 + animation.Offsets.GunYOff,
                                    256 + animation.Offsets.GunXOffFlip,
                                    256 + animation.Offsets.GunYOffFlip
                                };
                            break;
                        case Flip.Vertical:
                            gun = new[]
                                {
                                    256 + animation.Offsets.GunXOff,
                                    256 + animation.Offsets.GunYOff,
                                    256 + animation.Offsets.GunXOffFlip,
                                    256 + animation.Offsets.GunYOffFlip
                                };
                            break;
                        default:
                            throw new Exception("Shooting enemy must have a flip set");
                    }
                }
                else
                {
                    gun = new[] { 0, 0, 0, 0 };
                }

                builder.AppendLine(".orientation:");
                builder.AppendLineFormat("  .byte ${0:X2}", (byte)animation.Orientation);

                builder.AppendLine(".bulletPointer:");
                builder.AppendLineFormat("  .byte ${0:X2}", (byte)(animation.BulletId * Constants.BulletDefinitionSize));

                builder.AppendLine(".gunInfo:");
                builder.AppendLineFormat("  .byte {0}", string.Join(",", gun.Select(v => "$" + (v % 256).ToString("X2"))));

                builder.AppendLine(".animationSpeed:");
                builder.AppendLineFormat("  .byte ${0:X2}", animation.AnimationSpeed);

                builder.AppendLine(".numberOfFrames:");
                builder.AppendLineFormat("  .byte ${0:X2}", animation.Frames.Count());

                builder.AppendLine(".renderingInfo:");
                builder.AppendLineFormat("  .byte LOW({0}Render), HIGH({0}Render)", animation.Name.Replace(" ", ""));

                builder.AppendLine(".explosionOffset:");
                builder.AppendLineFormat("  .byte ${0:X2}, ${1:X2}", (widthInPixes / 2) - 8, (heightInPixels / 2) - 8);

                builder.AppendLine(".explosionId:");
                builder.AppendLineFormat("  .byte $00"); // todo 0004

                builder.AppendLine();
            }

            return builder.ToString();
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

                builder.AppendLineFormat("{0}Render:", animation.Name.Replace(" ", ""));
                
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
                // todo - to save sprites at cost of processing, allow for flip in frames?
                var attsList = new List<int>();                
                for (var x = 0; x < firstFrame.Width; x++)
                {
                    for (var y = 0; y < firstFrame.Height; y++)
                    {
                        var sprite = firstFrame.Sprites[y * firstFrame.Width + x];

                        var mapping = sprite.Id == -1 ? 0 : sprite.ActualSprite.Mapping;
                        var atts = this.config.PaletteMappings[mapping].ToPalette + animation.AttsUpdate; // handle clones

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
                    builder.AppendLineFormat(".Frame{0}_{1}:", frameIndex, frame.Name.Replace(" ", ""));

                    var tileList = new List<int>();
                    for (var x = 0; x < frame.Width; x++)
                    {
                        for (var y = 0; y < frame.Height; y++)
                        {
                            var sprite = frame.Sprites[y * frame.Width + x];
                            tileList.Add(sprite.Id == -1 ? -1 : sprite.ActualSprite.Id);
                        }
                    }

                    builder.AppendLineFormat("  .byte {0}", string.Join(",", tileList.Select(t => t == -1 ? "CLEAR_SPRITE" : "$" + t.ToString("X2"))));
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
