using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class Frame
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Sprite[] Sprites { get; set; }

        [DataMember]
        public string Name { get; set; }

        private IDictionary<SpriteFlags, Bitmap>[] cachedBitmaps;

        public int Width { get; set; }

        public int Height { get; set; }

        public int XOffset { get; set; }

        public int YOffset { get; set; }

        public Frame()
        {
            this.cachedBitmaps = new IDictionary<SpriteFlags, Bitmap>[Constants.MaxZoom - 1];
            for (var i = 0; i < Constants.MaxZoom - 1; i++)
            {
                cachedBitmaps[i] = new Dictionary<SpriteFlags, Bitmap>();
            }
        }

        public Bitmap GetBitmap(
            SpriteConfig config,
            Color backColor,
            bool applyPalettes,
            bool showBoxes,
            bool vFlip,
            bool hFlip,
            int zoom,
            Offsets offsets,
            Offsets secondOffsets = null // for the threat box for the player
            )
        {
            var flags = SpriteFlags.None;

            if (applyPalettes)
            {
                flags |= SpriteFlags.Palettes;
            }

            if (showBoxes)
            {
                flags |= SpriteFlags.Boxes;
            }

            if (vFlip)
            {
                flags |= SpriteFlags.VFlip;
            }

            if (hFlip)
            {
                flags |= SpriteFlags.HFlip;
            }

            var dictionary = this.cachedBitmaps[zoom - 1];
            if (dictionary.ContainsKey(flags))
            {
                return dictionary[flags];
            }

            var image = new MyBitmap(Width, Height, backColor);

            foreach (var sprite in this.Sprites)
            {
                var shouldBeVFlip = sprite.VFlip ^ vFlip;
                var shouldBeHFlip = sprite.HFlip ^ hFlip;

                var spriteImage = sprite.GetSprite(applyPalettes, shouldBeVFlip, shouldBeHFlip);

                var x = sprite.X + config.XOffset - this.XOffset;
                var y = sprite.Y + config.YOffset - this.YOffset;

                if (vFlip)
                {
                    y = this.Height - y - Constants.SpriteHeight;
                }

                if (hFlip)
                {
                    x = this.Width - x - Constants.SpriteWidth;
                }


                image.DrawImage(spriteImage, x, y);
            }

            if (showBoxes)
            {
                //// I can't get this to work
                //
                //image.DrawRectangle(MyBitmap.PlatformBoxColor, config.XOffset, config.YOffset, config.XOffset + offsets.BoxWidth, config.YOffset + offsets.BoxHeight);
                //
                //////image.DrawRectangle(MyBitmap.ThreatBoxColor, config.X, config.Y, config.Y + offsets.BoxHeight, config.X + offsets.BoxWidth);
                //
                //if (hFlip)
                //{
                //    image.SetPixel(MyBitmap.GunColor, config.XOffset + offsets.GunXOffL, config.YOffset + offsets.GunYOff);
                //}
                //else
                //{
                //    image.SetPixel(MyBitmap.GunColor, config.XOffset + offsets.GunXOffR, config.YOffset + offsets.GunYOff); 
                //}                    
                //
                //image.SetPixel(MyBitmap.XYColor, config.XOffset, config.YOffset);
            }

            var largeImage = new MyBitmap(config.MaxFrameWidth, config.MaxFrameHeight, backColor);
            int largeImageX = this.XOffset - config.MinXOffset;
            if (hFlip)
            {
                largeImageX = config.MaxFrameWidth - this.Width - largeImageX;
            }

            var largeImageY = this.YOffset - config.MinYOffset;
            if (vFlip)
            {
                largeImageY = config.MaxFrameHeight - this.Height - largeImageY;
            }

            largeImage.DrawImage(image, largeImageX, largeImageY);

            var result = largeImage.Scale(zoom).ToBitmap();

            dictionary.Add(flags, result);

            return result;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
