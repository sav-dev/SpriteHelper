﻿using SpriteHelper.NesGraphics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Palettes
    {        
        [DataMember]
        public PaletteSet[] SpritesPalettes { get; set; }
       
        [DataMember]
        public PaletteSet[] BackgroundPalettes { get; set; }

        public Palette[] SpritesPalette { get; set; }

        public static Palettes Read(string file)
        {
            Palettes palettes;
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(Palettes));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    palettes = (Palettes)xmlSerializer.Deserialize(memoryStream);
                }
            }

            if (palettes.SpritesPalettes.Count() > 1)
            {
                throw new System.Exception("There can only be one sprites palette");
            }

            palettes.SpritesPalette = palettes.SpritesPalettes[0].Palettes;

            foreach (var palette in (palettes.SpritesPalettes.SelectMany(p => p.Palettes).Union(palettes.BackgroundPalettes.SelectMany(p => p.Palettes))))
            {
                palette.ActualColors = palette.Colors.Select(c => NesPalette.Colors[c]).ToArray();
            }

            var id = 0;
            foreach (var palette in palettes.BackgroundPalettes)
            {
                if (palette.Id != id)
                {
                    throw new System.Exception("Invalid ids for bg palettes");
                }

                id++;
            }

            if (id > 8)
            {
                throw new System.Exception("Too many bg palettes");
            }

            return palettes;
        }
    }

    [DataContract]
    public class PaletteSet
    {
        [DataMember]
        public Palette[] Palettes { get; set; }

        [DataMember]
        public int Id { get; set; }
    }

    [DataContract]
    public class Palette
    {
        [DataMember]
        public int[] Colors { get; set; }
        
        public Color[] ActualColors { get; set; }
    }
}
