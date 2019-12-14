using SpriteHelper.Dialogs;
using SpriteHelper.NesGraphics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Frame
    {
        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Sprite[] Sprites { get; set; }

        [DataMember]
        public string Name { get; set; }

        private IDictionary<ImageFlags, Bitmap>[] cachedBitmaps;

        public Frame()
        {
            this.cachedBitmaps = new IDictionary<ImageFlags, Bitmap>[Constants.MaxZoom - 1];
            for (var i = 0; i < Constants.MaxZoom - 1; i++)
            {
                cachedBitmaps[i] = new Dictionary<ImageFlags, Bitmap>();
            }
        }

        public Frame Clone()
        {
            var clone = new Frame
            {
                Name = this.Name,
                Width = this.Width,
                Height = this.Height,
                Sprites = new Sprite[this.Sprites.Length],
            };

            for (var i = 0; i < this.Sprites.Length; i++)
            {
                clone.Sprites[i] = this.Sprites[i].Clone();
            }

            return clone;
        }

        private ImageFlags GetFlags(
            bool applyPalettes,
            bool showBoxes,
            bool vFlip,
            bool hFlip,
            bool transparent = false,
            bool transparentBg = false)
        {
            var flags = ImageFlags.None;

            if (applyPalettes)
            {
                flags |= ImageFlags.Palettes;
            }

            if (showBoxes)
            {
                flags |= ImageFlags.Boxes;
            }

            if (vFlip)
            {
                flags |= ImageFlags.VFlip;
            }

            if (hFlip)
            {
                flags |= ImageFlags.HFlip;
            }

            if (transparent)
            {
                flags |= ImageFlags.Transparent;
            }

            if (transparentBg)
            {
                flags |= ImageFlags.TransparentBg;
            }

            return flags;
        }

        /// <summary>
        /// Special method for rendering the player.
        /// </summary>
        public Bitmap GetPlayerBitmap(
            SpriteConfig config,
            Color backColor,
            bool applyPalettes,
            bool showBoxes,
            bool hFlip,
            int zoom,
            bool transparentBg = false)
        {
            // Get flags for the request.
            var flags = GetFlags(applyPalettes, showBoxes, false, hFlip, false, transparentBg);

            // Checked if a cached bitmap is available, return it if it is.
            var dictionary = this.cachedBitmaps[zoom - 1];
            if (dictionary.ContainsKey(flags))
            {
                return dictionary[flags];
            }

            // Assume game position equals the offsets.
            var gameX = Constants.PlayerXOffset;
            var gameY = Constants.PlayerYOffset;

            // Calculate image size. Get a max of all frames.
            var maxX = config.Frames.Max(f => f.Sprites.Max(s => gameX + Player.GetXOffset(s, hFlip)));
            var maxY = config.Frames.Max(f => f.Sprites.Max(s => gameY + Player.GetYOffset(s)));
            var width = maxX + Constants.SpriteWidth;
            var height = maxY + Constants.SpriteHeight;
            var image = new MyBitmap(width, height, backColor);

            // Draw each sprite.
            foreach (var sprite in this.Sprites)
            {
                // Get the correct sprite image.
                var spriteImage = sprite.GetSprite(applyPalettes, sprite.VFlip, sprite.HFlip ^ hFlip);
            
                // Draw the image.
                image.DrawImage(spriteImage, gameX + Player.GetXOffset(sprite, hFlip), gameY + Player.GetYOffset(sprite));
            }

            if (showBoxes)
            {
                // Platform box.
                var platformBoxX1 = gameX;
                var platformBoxY1 = gameY;
                var platformBoxX2 = platformBoxX1 + Constants.PlayerPlatformBoxWidth;
                var platformBoxY2 = platformBoxY1 + Constants.PlayerPlatformBoxHeight;
                image.DrawRectangle(MyBitmap.PlatformBoxColor, platformBoxX1, platformBoxY1, platformBoxX2, platformBoxY2);

                // Threat box.
                var threatBoxX1 = gameX + Constants.PlayerThreatBoxXOff;
                var threatBoxY1 = gameY + Constants.PlayerThreatBoxYOff;
                var threatBoxX2 = threatBoxX1 + Constants.PlayerThreatBoxWidth;
                var threatBoxY2 = threatBoxY1 + (this.Name == "Crouch" ? Constants.PlayerThreatBoxHeightCrouch : Constants.PlayerThreatBoxHeight);
                image.DrawRectangle(MyBitmap.ThreatBoxColor, threatBoxX1, threatBoxY1, threatBoxX2, threatBoxY2);

                // Gun point.
                var gunYOff = this.Name == "Crouch" ? Constants.PlayerGunYOffCrouch : Constants.PlayerGunYOff;
                var gunXOff = hFlip ? Constants.PlayerGunOXffL : Constants.PlayerGunOXffR;
                image.SetPixel(MyBitmap.GunColor, gameX + gunXOff, gameY + gunYOff);                

                // Game position.
                image.SetPixel(MyBitmap.XYColor, gameX, gameY);
            }

            // Scale image.
            var result = image.Scale(zoom).ToBitmap(backgroundColor: transparentBg ? backColor : (Color?)null);

            // Save result for later and return.
            dictionary.Add(flags, result);
            return result;
        }

        /// <summary>
        /// Special method for rendering the explosion.
        /// </summary>
        public Bitmap GetExplosionBitmap(
            Color backColor,
            bool applyPalettes,
            int zoom)
        {
            // Get flags for the request.
            var flags = GetFlags(applyPalettes, false, false, false);

            // Checked if a cached bitmap is available, return it if it is.
            var dictionary = this.cachedBitmaps[zoom - 1];
            if (dictionary.ContainsKey(flags))
            {
                return dictionary[flags];
            }

            // Create bitmap.
            var width = Constants.ExplosionWidth;
            var height = Constants.ExplosionHeight;
            var image = new MyBitmap(width, height, backColor);

            // Draw each sprite.
            foreach (var sprite in this.Sprites)
            {
                // Get the correct sprite image.
                var spriteImage = sprite.GetSprite(applyPalettes, sprite.VFlip, sprite.HFlip);

                // Draw the image.
                image.DrawImage(spriteImage, sprite.X, sprite.Y);
            }

            // Scale image.
            var result = image.Scale(zoom).ToBitmap();

            // Save result for later and return.
            dictionary.Add(flags, result);
            return result;
        }

        /// <summary>
        /// Method for rendering a grid bitmap.
        /// </summary>
        public Bitmap GetGridBitmap(
            Color backColor,
            bool applyPalettes,
            bool showBoxes,
            bool vFlip,
            bool hFlip,
            int zoom,
            Offsets offsets,
            bool transparent = false,
            bool transparentBg = false)
        {
            // Get flags for the request.
            var flags = GetFlags(applyPalettes, showBoxes, vFlip, hFlip, transparent, transparentBg);

            // Checked if a cached bitmap is available, return it if it is.
            var dictionary = this.cachedBitmaps[zoom - 1];
            if (dictionary.ContainsKey(flags))
            {
                return dictionary[flags];
            }

            // Create bitmap.
            var width = this.Width * Constants.SpriteWidth;
            var height = this.Height * Constants.SpriteHeight;
            var image = new MyBitmap(width, height, backColor);

            // Figure out flip.
            var isFlip = (hFlip || vFlip);

            // Draw each sprite.
            for (var spriteX = 0; spriteX < this.Width; spriteX++)
            {
                for (var spriteY = 0; spriteY < this.Height; spriteY++)
                {
                    // Sprites are aligned like:
                    //   1 2 3
                    //   4 5 6
                    var spriteId = spriteX + (spriteY * this.Width);
                    var sprite = this.Sprites[spriteId];

                    // Figure out flips.
                    var shouldBeVFlip = sprite.VFlip ^ vFlip;
                    var shouldBeHFlip = sprite.HFlip ^ hFlip;

                    // Get the correct sprite image.
                    var spriteImage = sprite.GetSprite(applyPalettes, shouldBeVFlip, shouldBeHFlip, backColor);

                    // Calculate sprite's position.
                    var x = spriteX * Constants.SpriteWidth;
                    var y = spriteY * Constants.SpriteHeight;

                    if (vFlip)
                    {
                        y = height - y - Constants.SpriteHeight;
                    }

                    if (hFlip)
                    {
                        x = width - x - Constants.SpriteWidth;
                    }

                    // Draw the image.
                    image.DrawImage(spriteImage, x, y);
                }
            }

            if (showBoxes)
            {
                image.DrawRectangle(MyBitmap.ThreatBoxColor, offsets.XOff, offsets.YOff, offsets.XOff + offsets.Width, offsets.YOff + offsets.Height);

                if (offsets.GunXOff >= 0)
                {
                    image.SetPixel(MyBitmap.GunColor, isFlip ? offsets.GunXOffFlip : offsets.GunXOff, isFlip ? offsets.GunYOffFlip : offsets.GunYOff);
                }
            }

            // Scale image.
            var result = image.Scale(zoom).ToBitmap(transparent ? Constants.TransparentAlpha : 255, transparentBg ?  backColor : (Color?)null);

            // Save result for later and return.
            dictionary.Add(flags, result);
            return result;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
