using SpriteHelper.Contract;
using SpriteHelper.NesGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class BulletsWindow : Form
    {
        private SpriteConfig config;
        private Palettes palettes;

        private Dictionary<int, Dictionary<int, Dictionary<ImageFlags, Bitmap>>> cachedBitmaps;

        public BulletsWindow()
        {
            InitializeComponent();
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            this.LoadFiles();
        }

        private void LoadFiles()
        {
            this.palettes = Palettes.Read(palettesTextBox.Text);
            this.config = SpriteConfig.Read(specTextBox.Text, this.palettes);
            CreateCachedBitmaps();

            this.bulletsListBox.Items.Clear();
            foreach (var bullet in config.Bullets)
            {
                this.bulletsListBox.Items.Add(bullet);
            }

            this.bulletsListBox.SelectedIndex = 0;
        }

        private void CreateCachedBitmaps()
        {
            this.cachedBitmaps = new Dictionary<int, Dictionary<int, Dictionary<ImageFlags, Bitmap>>>();
            foreach (var bullet in this.config.Bullets)
            {
                cachedBitmaps[bullet.BulletId] = new Dictionary<int, Dictionary<ImageFlags, Bitmap>>();
                for (var zoom = this.zoomPicker.Minimum; zoom <= this.zoomPicker.Maximum; zoom++)
                {
                    cachedBitmaps[bullet.BulletId][(int)zoom] = new Dictionary<ImageFlags, Bitmap>();
                    foreach (var applyPalettes in new bool[] { true, false })
                    {
                        foreach (var showBoxes in new bool[] { true, false })
                        {
                            foreach (var flip in new bool[] { true, false })
                            {
                                var flipFlags = bullet.GetFlipFlags(flip);

                                var bitmap = bullet.Sprite.GetSprite(
                                    applyPalettes,
                                    flipFlags.HasFlag(ImageFlags.VFlip),
                                    flipFlags.HasFlag(ImageFlags.HFlip),
                                    GetBgColor(applyPalettes)).Clone();

                                if (showBoxes)
                                {
                                    if (flip)
                                    {
                                        bitmap.DrawRectangle(MyBitmap.ThreatBoxColor, bullet.BoxDxFlip, bullet.BoxDyFlip, bullet.BoxDxFlip + bullet.BoxWidth, bullet.BoxDyFlip + bullet.BoxHeight); 
                                    }
                                    else
                                    {
                                        bitmap.DrawRectangle(MyBitmap.ThreatBoxColor, bullet.BoxDx, bullet.BoxDy, bullet.BoxDx + bullet.BoxWidth, bullet.BoxDy + bullet.BoxHeight);
                                    }
                                }

                                var flags = GetFlags(applyPalettes, showBoxes, flipFlags);
                                cachedBitmaps[bullet.BulletId][(int)zoom][flags] = bitmap.Scale((int)zoom).ToBitmap();
                            }
                        }
                    }
                }
            }
        }

        private ImageFlags GetFlags(bool applyPalettes, bool showBoxes, ImageFlags flipFlags)
        {
            var result = flipFlags;
            if (applyPalettes)
            {
                result |= ImageFlags.Palettes;
            }

            if (showBoxes)
            {
                result |= ImageFlags.Boxes;
            }

            return result;
        }

        private void ZoomPickerValueChanged(object sender, EventArgs e)
        {
            this.LoadBullet();
        }

        private void ApplyPaletteCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.LoadBullet();
        }

        private void ShowBoxesCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.LoadBullet();
        }

        private void FlipCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.LoadBullet();
        }

        private void BulletsListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadBullet();
        }

        private Color GetBgColor(bool applyPalettes)
        {
            return applyPalettes ? this.palettes.SpritesPalette[0].ActualColors[0] : Color.Black;
        }

        private void LoadBullet()
        {
            var bullet = (Bullet)this.bulletsListBox.SelectedItem;
            this.pictureBox.BackColor = GetBgColor(this.applyPaletteCheckbox.Checked);
            var zoom = (int)this.zoomPicker.Value;
            var flags = GetFlags(this.applyPaletteCheckbox.Checked, this.showBoxesCheckbox.Checked, bullet.GetFlipFlags(this.flipCheckbox.Checked));
            this.pictureBox.Image = this.cachedBitmaps[bullet.BulletId][zoom][flags];
        }

        private void CodeButtonClick(object sender, EventArgs e)
        {
            var code = this.GetCode();
            new CodeWindow(code).ShowDialog();
        }

        private string GetCode()
        {
            var builder = new StringBuilder();

            builder.AppendLineFormat(GetHeader());
            builder.AppendLineFormat(GetComment());
            builder.AppendLineFormat(GetData());
            builder.AppendLineFormat(GetFooter());

            return builder.ToString();
        }

        private string GetHeader()
        {
            return @"BulletsStart:

;****************************************************************
; Bullets                                                       ;
; Holds information about all bullets (auto-generated)          ;
;****************************************************************
";
        }

        private string GetFooter()
        {
            return @"BulletsEnd:";
        }

        private string GetComment()
        {
            return @";
;  constant properties for bullets
;    sprite id          : 1 byte
;    box width, height  : 2 bytes
;    atts               : 1 byte
;    bullet dx, dy      : 2 bytes
;    box dx, dy         : 2 bytes
;    atts flip          : 1 byte
;    bullet dx, dy flip : 2 bytes
;    box dx, dy flip    : 2 bytes
;
;  ordered by bullet id
;
;  tag: depends_on_bullets_consts_format
";
        }

        private string GetData()
        {
            var builder = new StringBuilder();

            builder.AppendLine();
            builder.AppendLine("BulletConsts:");

            foreach (var bullet in this.config.Bullets)
            {
                //// todo: don't export player bullet data? hardcode it instead? or export as consts?

                builder.AppendLine();

                // Name
                builder.AppendLineFormat($"{bullet.Name.Replace(" ", "_")}:");

                // Sprite
                builder.AppendLine(".spriteId:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.SpriteId)}");

                // Box size
                builder.AppendLine(".boxSize:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BoxWidth)}, {ToHex(bullet.BoxHeight)}");

                // Atts
                var paletteId = config.PaletteMappings.First(pm => pm.Id == bullet.Sprite.Mapping).ToPalette;
                var atts = paletteId;

                if (bullet.HFlip)
                {
                    atts += 64;
                }

                if (bullet.VFlip)
                {
                    atts += 128;
                }

                builder.AppendLine(".atts:");
                builder.AppendLineFormat($"  .byte {ToHex(atts)}");

                // DX, DY
                builder.AppendLine(".speed:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BulletDx)}, {ToHex(bullet.BulletDy)}");

                // Box
                builder.AppendLine(".boxOffset:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BoxDx)}, {ToHex(bullet.BoxDy)}");

                // Atts flip
                var attsFlip = paletteId;

                if (bullet.Orientation == Contract.Orientation.Horizontal)
                {
                    if (!bullet.HFlip)
                    {
                        attsFlip += 64;
                    }

                    if (bullet.VFlip)
                    {
                        attsFlip += 128;
                    }
                }
                else if (bullet.Orientation == Contract.Orientation.Vertical)
                {
                    if (bullet.HFlip)
                    {
                        attsFlip += 64;
                    }

                    if (!bullet.VFlip)
                    {
                        attsFlip += 128;
                    }
                }
                else
                {
                    throw new Exception("Bullet can't have 'none' orientation.");
                }

                builder.AppendLine(".attsFlip:");
                builder.AppendLineFormat($"  .byte {ToHex(attsFlip)}");

                // DX, DY flip
                builder.AppendLine(".speedFlip:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BulletDxFlip)}, {ToHex(bullet.BulletDyFlip)}");

                // Box flip
                builder.AppendLine(".boxOffsetFlip:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BoxDxFlip)}, {ToHex(bullet.BoxDyFlip)}");                
            }

            return builder.ToString();
        }

        private string ToHex(int n) => $"${n:X2}";
    }
}
