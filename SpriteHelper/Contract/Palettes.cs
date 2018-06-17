using SpriteHelper.NesGraphics;
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
        public Palette[] SpritesPalette { get; set; }
       
        [DataMember]
        public Palette[] BackgroundPalette { get; set; }
   
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

            foreach (var palette in (palettes.SpritesPalette.Union(palettes.BackgroundPalette)))
            {
                palette.ActualColors = palette.Colors.Select(c => NesPalette.Colors[c]).ToArray();
            }

            return palettes;
        }
    }

    [DataContract]
    public class Palette
    {
        [DataMember]
        public int[] Colors { get; set; }

        public Color[] ActualColors { get; set; }
    }
}
