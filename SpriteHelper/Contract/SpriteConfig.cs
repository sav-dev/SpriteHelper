using SpriteHelper.NesGraphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper.Contract
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

        [DataMember]
        public Bullet[] Bullets { get; set; }

        public int MaxFrameWidth { get; set; }
        public int MaxFrameHeight { get; set; }
        public int MinXOffset { get; set; }
        public int MinYOffset { get; set; }

        public static SpriteConfig Read(string file, Palettes palettes)
        {
            // Read the XML.
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

            // Get the image (same name).
            var sourceImagePath = file.Substring(0, file.Length - 3) + "png";
            var source = MyBitmap.FromFile(sourceImagePath);

            // Prepare all sprites.
            foreach (var sprite in config.Sprites)
            {
                // Prepare sprite - get the image.
                sprite.PrepareSprite(source);

                // Validate number of unique colors.
                var uniqueColors = sprite.GetSprite().UniqueColors();
                if (uniqueColors.Length > 4)
                {
                    sprite.GetSprite().ToBitmap().Save("debug.bmp");
                    throw new Exception(string.Format("Too many colors in sprite {0}", sprite.Id));
                }

                // Get and validate the palette mapping.
                var mapping = config.PaletteMappings.First(m => m.Id == sprite.Mapping);
                foreach (var color in uniqueColors)
                {
                    if (!mapping.ColorMappings.Select(c => c.Color).Contains(color))
                    {
                        sprite.GetSprite().ToBitmap().Save("debug.bmp");
                        throw new Exception(string.Format("Color not mapped in sprite {0}", sprite.Id));
                    }
                }

                // Prepare sprite with pallete applied and all reverse combinations.                
                sprite.PreparePalettes(palettes, mapping);
                sprite.PrepareReversed();
            }

            // Prepare frames.
            foreach (var frame in config?.Frames ?? new Frame[0])
            {
                foreach (var sprite in frame.Sprites)
                {
                    if (sprite.Id != -1)
                    {
                        sprite.ActualSprite = config.Sprites.First(s => s.Id == sprite.Id);
                    }
                    else
                    {
                        sprite.IsEmpty = true;
                    }
                }
            }

            // Prepare animations.
            foreach (var animation in config.Animations ?? new Animation[0])
            {
                // No copy of animation 0 possible since it defaults to 0
                if (animation.CopyOf > 0)
                {
                    var copyFrom = config.Animations.First(a => a.Id == animation.CopyOf);
                    
                    //// Essentially what's happening here is we clone the animation, frames and sprites and change the palette

                    // Copy everything except for Frames.
                    animation.SetFrom(copyFrom);

                    // Now we must copy Frames.
                    var newFrames = new List<Frame>();
                    foreach (var frame in copyFrom.Frames)
                    {
                        var frameClone = frame.Clone();
                        foreach (var sprite in frameClone.Sprites)
                        {
                            var mapping = config.PaletteMappings.First(m => m.Id == sprite.ActualSprite.Mapping);
                            sprite.ActualSprite.PreparePalettes(palettes, mapping, animation.AttsUpdate);
                            sprite.ActualSprite.PrepareReversed();
                        }

                        newFrames.Add(frameClone);
                    }

                    animation.Frames = newFrames.ToArray();
                    continue;
                }

                for (var i = 0; i < animation.Frames.Length; i++)
                {
                    animation.Frames[i] = config.Frames.First(frame => frame.Id == animation.Frames[i].Id);
                }
            }

            // Prepare bullets
            foreach (var bullet in config?.Bullets ?? new Bullet[0])
            {
                bullet.Sprite = config.Sprites.First(s => s.Id == bullet.SpriteId);
            }

            return config;
        }
    }
}
