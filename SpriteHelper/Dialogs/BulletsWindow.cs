using SpriteHelper.Contract;
using SpriteHelper.NesGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        private void CodeButtonClick(object sender, EventArgs e)
        {
            // todo show code
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
    }
}
