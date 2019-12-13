using SpriteHelper.NesGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Sprite
    {
        public bool IsEmpty { get; set; } = false;

        public Sprite ActualSprite;

        private Dictionary<ImageFlags, MyBitmap> sprites = new Dictionary<ImageFlags, MyBitmap>();

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public int GameSprite { get; set; }

        [DataMember]
        public bool VFlip { get; set; }

        [DataMember]
        public bool HFlip { get; set; }

        [DataMember]
        public int Mapping { get; set; }

        public void PrepareSprite(MyBitmap source)
        {
            if (this.ActualSprite != null)
            {
                throw new Exception("Not expected");
            }

            var sprite = source.GetPart(this.X, this.Y, Constants.SpriteWidth, Constants.SpriteHeight);
            sprites.Add(ImageFlags.None, sprite);
        }

        public void PreparePalettes(Palettes palettes, PaletteMapping paletteMapping)
        {
            var sprite = this.sprites[ImageFlags.None];
            var spriteWithPalettesApplied = new MyBitmap(sprite.Width, sprite.Height);
            var palette = palettes.SpritesPalette[paletteMapping.ToPalette];

            for (var x = 0; x < sprite.Width; x++)
            {
                for (var y = 0; y < sprite.Height; y++)
                {
                    var color = sprite.GetPixel(x, y);
                    var mappedColorId = paletteMapping.ColorMappings.First(c => c.Color == color).To;
                    var mappedColor = palette.ActualColors[mappedColorId];
                    spriteWithPalettesApplied.SetPixel(mappedColor, x, y);
                }
            }

            sprites.Add(ImageFlags.Palettes, spriteWithPalettesApplied);
        }

        public void PrepareReversed()
        {
            var sprite = this.sprites[ImageFlags.None];
            var spriteWithPalettesApplied = this.sprites[ImageFlags.Palettes];

            var options = new[]
                {
                    ImageFlags.VFlip,
                    ImageFlags.HFlip,
                    ImageFlags.VFlip | ImageFlags.HFlip,
                    ImageFlags.VFlip | ImageFlags.Palettes,
                    ImageFlags.HFlip | ImageFlags.Palettes,
                    ImageFlags.VFlip | ImageFlags.HFlip | ImageFlags.Palettes,
                };

            foreach (var option in options)
            {
                var source = option.HasFlag(ImageFlags.Palettes) ? spriteWithPalettesApplied : sprite;

                if (option.HasFlag(ImageFlags.VFlip))
                {
                    source = source.ReverseVertically();
                }

                if (option.HasFlag(ImageFlags.HFlip))
                {
                    source = source.ReverseHorizontally();
                }

                this.sprites.Add(option, source);
            }
        }

        public MyBitmap GetSprite(bool applyPalettes = false, bool vFlip = false, bool hFlip = false, Color backColor = default(Color))
        {
            if (this.IsEmpty)
            {
                return new MyBitmap(8, 8, backColor);
            }

            if (this.ActualSprite != null)
            {
                return this.ActualSprite.GetSprite(applyPalettes, vFlip, hFlip);
            }

            var flags = ImageFlags.None;
            if (applyPalettes)
            {
                flags = flags | ImageFlags.Palettes;
            }

            if (vFlip)
            {
                flags = flags | ImageFlags.VFlip;
            }

            if (hFlip)
            {
                flags = flags | ImageFlags.HFlip;
            }

            return this.sprites[flags];
        }
    }
}
