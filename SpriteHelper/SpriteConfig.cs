using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class SpriteConfig
    {
        [DataMember]
        public PaletteMapping[] PaletteMappings { get; set; }

        [DataMember]
        public Sprite[] Sprites { get; set; }

        [DataMember]
        public Frame[] Frames { get; set; }

        [DataMember]
        public Animation[] Animations { get; set; }

        public static SpriteConfig Read(string file, MyBitmap source, Palettes palettes)
        {
            SpriteConfig config;
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(SpriteConfig));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    config = (SpriteConfig)xmlSerializer.Deserialize(memoryStream);
                }
            }

            foreach (var sprite in config.Sprites)
            {
                sprite.PrepareSprite(source);
                var uniqueColors = sprite.GetSprite().UniqueColors();
                if (uniqueColors.Length > 4)
                {
                    throw new Exception(string.Format("Too many colors in sprite {0}", sprite.Id));
                }

                var mapping = config.PaletteMappings.First(m => m.Id == sprite.Mapping);
                foreach (var color in uniqueColors)
                {
                    if (!mapping.ColorMappings.Select(c => c.Color).Contains(color))
                    {
                        throw new Exception(string.Format("Color not mapped in sprite {0}", sprite.Id));
                    }
                }

                sprite.PreparePalettes(palettes, mapping);
            }

            foreach (var frame in config.Frames)
            {
                foreach (var sprite in frame.Sprites)
                {
                    sprite.ActualSprite = config.Sprites.First(s => s.Id == sprite.Id);
                }
            }

            foreach (var animation in config.Animations)
            {
                for (var i = 0; i < animation.Frames.Length; i++)
                {
                    animation.Frames[i] = config.Frames.First(frame => frame.Id == animation.Frames[i].Id);
                }
            }

            return config;
        }

        public MyBitmap GetAllSprites(MyBitmap source, int rows)
        {
            var spritesPerRow = (int)(Math.Ceiling((double)this.Sprites.Length / rows));
            var width = spritesPerRow * Constants.SpriteWidth;
            var height = rows * Constants.SpriteHeight;
            var image = new MyBitmap(width, height);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; (j < spritesPerRow) && (i * spritesPerRow + j) < this.Sprites.Length; j++)
                {
                    image.DrawImage(this.Sprites[i * spritesPerRow + j].GetSprite(), j * Constants.SpriteWidth, i * Constants.SpriteHeight);
                }
            }

            return image;
        }
    }

    [DataContract]
    public class PaletteMapping
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ToPalette { get; set; }

        [DataMember]
        public ColorMapping[] ColorMappings { get; set; }
    }

    [DataContract]
    public class ColorMapping
    {
        [DataMember]
        public int R { get; set; }

        [DataMember]
        public int G { get; set; }

        [DataMember]
        public int B { get; set; }

        [DataMember]
        public int To { get; set; }

        public Color Color
        {
            get
            {
                return Color.FromArgb(this.R, this.G, this.B);
            }
        }
    }

    [DataContract]
    public class Sprite
    {
        public Sprite ActualSprite;

        private MyBitmap sprite;
        private MyBitmap spriteWithPalettesApplied;

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public int Mapping { get; set; }

        public void PrepareSprite(MyBitmap source)
        {
            if (this.ActualSprite != null)
            {
                throw new Exception("Not expected");
            }

            this.sprite = source.GetPart(this.X, this.Y, Constants.SpriteWidth, Constants.SpriteHeight);
           
        }

        public void PreparePalettes(Palettes palettes, PaletteMapping paletteMapping)
        {
            this.spriteWithPalettesApplied = new MyBitmap(this.sprite.Width, this.sprite.Height);
            var palette = palettes.SpritesPalette[paletteMapping.ToPalette];

            for (var x = 0; x < this.sprite.Width; x++)
            {
                for (var y = 0; y < this.sprite.Height; y++)
                {
                    var color = this.sprite.GetPixel(x, y);
                    var mappedColorId = paletteMapping.ColorMappings.First(c => c.Color == color).To;
                    var mappedColor = palette.ActualColors[mappedColorId];
                    this.spriteWithPalettesApplied.SetPixel(mappedColor, x, y);
                }
            }    
        }

        public MyBitmap GetSprite(bool applyPalettes = false)
        {
            if (this.ActualSprite != null)
            {
                return this.ActualSprite.GetSprite(applyPalettes);
            }

            return applyPalettes ? spriteWithPalettesApplied : sprite;
        }
    }

    [DataContract]
    public class Frame
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Sprite[] Sprites { get; set; }

        [DataMember]
        public string Name { get; set; }

        private Bitmap[] cachedBitmaps;
        private Bitmap[] cachedBitmapsWithPalettesApplied;

        public Frame()
        {
            this.cachedBitmaps = new Bitmap[Constants.MaxZoom - 1];
            this.cachedBitmapsWithPalettesApplied = new Bitmap[Constants.MaxZoom - 1];
        }

        public Bitmap GetBitmap(SpriteConfig config, Color backColor, bool applyPalettes, int zoom)
        {
            if (applyPalettes && this.cachedBitmapsWithPalettesApplied[zoom - 1] != null)
            {
                return this.cachedBitmapsWithPalettesApplied[zoom - 1];
            }
            else if (!applyPalettes && this.cachedBitmaps[zoom - 1] != null)
            {
                return this.cachedBitmaps[zoom - 1];
            }            

            var width = this.Sprites.Max(s => s.X) + Constants.SpriteWidth;
            var height = this.Sprites.Max(s => s.Y) + Constants.SpriteHeight;
            var image = new MyBitmap(width, height, backColor);

            foreach (var sprite in this.Sprites)
            {
                var spriteImage = sprite.GetSprite(applyPalettes);
                image.DrawImage(spriteImage, sprite.X, sprite.Y);
            }

            var result = image.Scale(zoom).ToBitmap();

            if (applyPalettes)
            {
                this.cachedBitmapsWithPalettesApplied[zoom - 1] = result;
            }
            else
            {
                this.cachedBitmaps[zoom - 1] = result;
            }

            return result;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    [DataContract]
    public class Animation
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int FPS { get; set; }

        [DataMember]
        public Frame[] Frames { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}