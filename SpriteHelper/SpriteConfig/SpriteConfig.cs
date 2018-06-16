using System;
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
}
