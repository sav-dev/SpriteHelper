using System;
using System.Collections.Generic;
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
        public int XOffset { get; set; }

        [DataMember]
        public int YOffset { get; set; }

        [DataMember]
        public PaletteMapping[] PaletteMappings { get; set; }

        [DataMember]
        public Sprite[] Sprites { get; set; }

        [DataMember]
        public Frame[] Frames { get; set; }

        [DataMember]
        public Animation[] Animations { get; set; }

        public int MaxFrameWidth { get; set; }
        public int MaxFrameHeight { get; set; }
        public int MinXOffset { get; set; }
        public int MinYOffset { get; set; }

        public static SpriteConfig Read(string file, Palettes palettes)
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

            var sourceImagePath = file.Substring(0, file.Length - 3) + "png";
            var source = MyBitmap.FromFile(sourceImagePath);

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
                sprite.PrepareReversed();
            }

            foreach (var frame in config.Frames)
            {
                var maxX = frame.Sprites.Max(s => s.X + config.XOffset);
                var minX = frame.Sprites.Min(s => s.X + config.XOffset);

                var maxY = frame.Sprites.Max(s => s.Y + config.YOffset);
                var minY = frame.Sprites.Min(s => s.Y + config.YOffset);

                frame.Width = maxX - minX + Constants.SpriteWidth;
                frame.Height = maxY - minY + Constants.SpriteHeight;
                frame.XOffset = minX;
                frame.YOffset = minY;

                foreach (var sprite in frame.Sprites)
                {                                        
                    sprite.ActualSprite = config.Sprites.First(s => s.Id == sprite.Id);
                }
            }

            config.MaxFrameWidth = config.Frames.Max(f => f.Width);
            config.MaxFrameHeight = config.Frames.Max(f => f.Height);
            config.MinXOffset = config.Frames.Min(f => f.XOffset);
            config.MinYOffset = config.Frames.Min(f => f.YOffset);

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

    public class Offsets
    {
        public int BoxXOff { get; set; }
        public int BoxYOff { get; set; }
        public int BoxWidth { get; set; }
        public int BoxHeight { get; set; }
        public int GunXOffL { get; set; }
        public int GunXOffR { get; set; }
        public int GunYOff { get; set; }
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

    [Flags]
    public enum SpriteFlags
    {
        None = 0,
        Palettes = 1,
        Boxes = 2,
        VFlip = 4,
        HFlip = 8
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
                ////image.DrawRectangle(MyBitmap.PlatformBoxColor, config.XOffset, config.YOffset, config.XOffset + offsets.BoxWidth, config.YOffset + offsets.BoxHeight);
                ////
                ////////image.DrawRectangle(MyBitmap.ThreatBoxColor, config.X, config.Y, config.Y + offsets.BoxHeight, config.X + offsets.BoxWidth);
                ////
                ////if (hFlip)
                ////{
                ////    image.SetPixel(MyBitmap.GunColor, config.XOffset + offsets.GunXOffL, config.YOffset + offsets.GunYOff);
                ////}
                ////else
                ////{
                ////    image.SetPixel(MyBitmap.GunColor, config.XOffset + offsets.GunXOffR, config.YOffset + offsets.GunYOff); 
                ////}                    
                ////
                ////image.SetPixel(MyBitmap.XYColor, config.XOffset, config.YOffset);
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

        [DataMember]
        public int PlatformBoxWidth { get; set; }

        [DataMember]
        public int PlatformBoxHeight { get; set; }

        [DataMember]
        public int ThreatBoxWidth { get; set; }

        [DataMember]
        public int ThreatBoxHeight { get; set; }

        [DataMember]
        public int GunXOffL { get; set; }

        [DataMember]
        public int GunXOffR { get; set; }

        [DataMember]
        public int GunYOff { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}