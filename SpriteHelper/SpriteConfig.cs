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
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public PaletteMapping[] PaletteMappings { get; set; }

        [DataMember]
        public Sprite[] Sprites { get; set; }

        [DataMember]
        public Frame[] Frames { get; set; }

        [DataMember]
        public Animation[] Animations { get; set; }

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
                foreach (var sprite in frame.Sprites)
                {
                    sprite.ReversedX = 2 * config.X - sprite.X + Constants.SpriteWidth;
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
    public class Offsets
    {
        [DataMember]
        public int BoxXOff { get; set; }

        [DataMember]
        public int BoxYOff { get; set; }

        [DataMember]
        public int BoxWidth { get; set; }

        [DataMember]
        public int BoxHeight { get; set; }

        [DataMember]
        public int GunXOffL { get; set; }

        [DataMember]
        public int GunXOffR { get; set; }

        [DataMember]
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

        private MyBitmap sprite;
        private MyBitmap spriteWithPalettesApplied;
        private MyBitmap spriteReversed;
        private MyBitmap spriteWithPalettesAppliedReversed;

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int X { get; set; }        

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public int GameSprite { get; set; }

        [DataMember]
        public int Mapping { get; set; }

        public int ReversedX { get; set; }

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

        public void PrepareReversed()
        {
            this.spriteWithPalettesAppliedReversed = this.spriteWithPalettesApplied.Reverse();
            this.spriteReversed = this.sprite.Reverse();
        }

        public MyBitmap GetSprite(bool applyPalettes = false, bool reversed = false)
        {
            if (this.ActualSprite != null)
            {
                return this.ActualSprite.GetSprite(applyPalettes, reversed);
            }

            return applyPalettes ?
                (reversed ? spriteWithPalettesAppliedReversed : spriteWithPalettesApplied) :
                (reversed ? spriteReversed : sprite);
        }
    }

    [Flags]
    public enum FrameFlags
    {
        None = 0,
        Palettes = 1,
        Boxes = 2,
        Reversed = 4
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

        private IDictionary<FrameFlags, Bitmap>[] cachedBitmaps;
        
        public Frame()
        {
            this.cachedBitmaps = new IDictionary<FrameFlags, Bitmap>[Constants.MaxZoom - 1];
            for (var i = 0; i < Constants.MaxZoom - 1; i++)
            {
                cachedBitmaps[i] = new Dictionary<FrameFlags, Bitmap>();
            }            
        }

        public Bitmap GetBitmap(
            SpriteConfig config, 
            Color backColor, 
            bool applyPalettes, 
            bool showBoxes, 
            bool reversed, 
            int zoom, 
            Offsets offsets,
            Offsets secondOffsets = null // for the threat box for the player
            )
        {
            var flags = FrameFlags.None;

            if (applyPalettes)
            {
                flags |= FrameFlags.Palettes;
            }

            if (showBoxes)
            {
                flags |= FrameFlags.Boxes;
            }

            if (reversed)
            {
                flags |= FrameFlags.Reversed;
            }

            var dictionary = this.cachedBitmaps[zoom - 1];
            if (dictionary.ContainsKey(flags))
            {
                return dictionary[flags];
            }

            var width = config.Frames.Max(f => f.Sprites.Max(s => Math.Max(s.ReversedX, s.X))) + Constants.SpriteWidth;
            var height = this.Sprites.Max(s => s.Y) + Constants.SpriteHeight;
            var image = new MyBitmap(width, height, backColor);

            foreach (var sprite in this.Sprites)
            {
                var spriteImage = sprite.GetSprite(applyPalettes, reversed);

                var x = reversed ? sprite.ReversedX : sprite.X;
                var y = sprite.Y;
                
                image.DrawImage(spriteImage, x, y);
            }

            if (showBoxes)
            {            
                image.DrawRectangle(MyBitmap.PlatformBoxColor, config.X, config.Y, config.Y + offsets.BoxHeight, config.X + offsets.BoxWidth);

                ////image.DrawRectangle(MyBitmap.ThreatBoxColor, config.X, config.Y, config.Y + offsets.BoxHeight, config.X + offsets.BoxWidth);

                if (reversed)
                {
                    image.SetPixel(MyBitmap.GunColor, config.X + offsets.GunXOffL, config.Y + offsets.GunYOff);
                }
                else
                {
                    image.SetPixel(MyBitmap.GunColor, config.X + offsets.GunXOffR, config.Y + offsets.GunYOff); 
                }                    

                image.SetPixel(MyBitmap.XYColor, config.X, config.Y);
            }

            var result = image.Scale(zoom).ToBitmap();

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