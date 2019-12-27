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
;    bullet dx, dy      : 2 bytes
;    box dx, dy         : 2 bytes
;    atts               : 1 byte
;    bullet dx, dy flip : 2 bytes
;    box dx, dy flip    : 2 bytes
;    atts flip          : 1 byte
;    sprite id          : 1 byte
;    box width, height  : 2 bytes
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

            Func<Bullet, int> getAtts = b =>
            {
                var paletteId = config.PaletteMappings.First(pm => pm.Id == b.Sprite.Mapping).ToPalette;
                var atts = paletteId;

                if (b.HFlip)
                {
                    atts += 64;
                }

                if (b.VFlip)
                {
                    atts += 128;
                }

                return atts;
            };

            Func<Bullet, int> getAttsFlip = b =>
            {
                var paletteId = config.PaletteMappings.First(pm => pm.Id == b.Sprite.Mapping).ToPalette;
                var attsFlip = paletteId;

                if (b.Orientation == Contract.Orientation.Horizontal)
                {
                    if (!b.HFlip)
                    {
                        attsFlip += 64;
                    }

                    if (b.VFlip)
                    {
                        attsFlip += 128;
                    }
                }
                else if (b.Orientation == Contract.Orientation.Vertical)
                {
                    if (b.HFlip)
                    {
                        attsFlip += 64;
                    }

                    if (!b.VFlip)
                    {
                        attsFlip += 128;
                    }
                }
                else
                {
                    throw new Exception("Bullet can't have 'none' orientation.");
                }

                return attsFlip;
            };

            foreach (var bullet in this.config.Bullets.Where(b => b.Name != "Player"))
            {
                builder.AppendLine();

                // Name
                builder.AppendLineFormat($"{bullet.Name.Replace(" ", "_")}:");

                // DX, DY
                builder.AppendLine(".speed:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BulletDx)}, {ToHex(bullet.BulletDy)}");

                // Box
                builder.AppendLine(".boxOffset:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BoxDx)}, {ToHex(bullet.BoxDy)}");

                // Atts
                builder.AppendLine(".atts:");
                builder.AppendLineFormat($"  .byte {ToHex(getAtts(bullet))}");

                // DX, DY flip
                builder.AppendLine(".speedFlip:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BulletDxFlip)}, {ToHex(bullet.BulletDyFlip)}");

                // Box flip
                builder.AppendLine(".boxOffsetFlip:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BoxDxFlip)}, {ToHex(bullet.BoxDyFlip)}");

                // Atts flip
                builder.AppendLine(".attsFlip:");
                builder.AppendLineFormat($"  .byte {ToHex(getAttsFlip(bullet))}");

                // Sprite
                builder.AppendLine(".spriteId:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.SpriteId)}");

                // Box size
                builder.AppendLine(".boxSize:");
                builder.AppendLineFormat($"  .byte {ToHex(bullet.BoxWidth)}, {ToHex(bullet.BoxHeight)}");
            }

            var playerBullet = this.config.Bullets.First(b => b.Name == "Player");
            var prefix = "PLAYER_BULLET_";
            builder.AppendLine();
            builder.AppendLine("; Player consts");
            builder.AppendLine("; note: box DX/DY = 0 for both flip and non-flip");
            builder.AppendLine(";       speed DY = 0 for both flip and non-flip");
            builder.AppendLine($"{prefix}SPRITE = {ToHex(playerBullet.SpriteId)}");
            builder.AppendLine($"{prefix}BOX_WIDTH = {ToHex(playerBullet.BoxWidth)}");
            builder.AppendLine($"{prefix}BOX_HEIGHT = {ToHex(playerBullet.BoxHeight)}");
            builder.AppendLine($"{prefix}SPEED_X = {ToHex(playerBullet.BulletDx)}");
            builder.AppendLine($"{prefix}SPEED_X_FLIP = {ToHex(playerBullet.BulletDxFlip)}");
            builder.AppendLine($"{prefix}ATTS = {ToHex(getAtts(playerBullet))}");
            builder.AppendLine($"{prefix}ATTS_FLIP = {ToHex(getAttsFlip(playerBullet))}");

            if (playerBullet.BoxDx != 0 || playerBullet.BoxDy != 0 || playerBullet.BoxDxFlip != 0 || playerBullet.BoxDyFlip != 0 || playerBullet.BulletDy != 0 || playerBullet.BulletDyFlip != 0)
            {
                throw new Exception("We expect player bullet box DX/DY to be 0!");
            }

            return builder.ToString();
        }

        private string ToHex(int n) => $"${n:X2}";
    }
}
