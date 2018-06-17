using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Sprite
    {
        public Sprite ActualSprite;

        private Dictionary<SpriteFlags, MyBitmap> sprites = new Dictionary<SpriteFlags, MyBitmap>();

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
            sprites.Add(SpriteFlags.None, sprite);
        }

        public void PreparePalettes(Palettes palettes, PaletteMapping paletteMapping)
        {
            var sprite = this.sprites[SpriteFlags.None];
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

            sprites.Add(SpriteFlags.Palettes, spriteWithPalettesApplied);
        }

        public void PrepareReversed()
        {
            var sprite = this.sprites[SpriteFlags.None];
            var spriteWithPalettesApplied = this.sprites[SpriteFlags.Palettes];

            var options = new[]
                {
                    SpriteFlags.VFlip,
                    SpriteFlags.HFlip,
                    SpriteFlags.VFlip | SpriteFlags.HFlip,
                    SpriteFlags.VFlip | SpriteFlags.Palettes,
                    SpriteFlags.HFlip | SpriteFlags.Palettes,
                    SpriteFlags.VFlip | SpriteFlags.HFlip | SpriteFlags.Palettes,
                };

            foreach (var option in options)
            {
                var source = option.HasFlag(SpriteFlags.Palettes) ? spriteWithPalettesApplied : sprite;

                if (option.HasFlag(SpriteFlags.VFlip))
                {
                    source = source.ReverseVertically();
                }

                if (option.HasFlag(SpriteFlags.HFlip))
                {
                    source = source.ReverseHorizontally();
                }

                this.sprites.Add(option, source);
            }
        }

        public MyBitmap GetSprite(bool applyPalettes = false, bool vFlip = false, bool hFlip = false)
        {
            if (this.ActualSprite != null)
            {
                return this.ActualSprite.GetSprite(applyPalettes, vFlip, hFlip);
            }

            var flags = SpriteFlags.None;
            if (applyPalettes)
            {
                flags = flags | SpriteFlags.Palettes;
            }

            if (vFlip)
            {
                flags = flags | SpriteFlags.VFlip;
            }

            if (hFlip)
            {
                flags = flags | SpriteFlags.HFlip;
            }

            return this.sprites[flags];
        }
    }
}
