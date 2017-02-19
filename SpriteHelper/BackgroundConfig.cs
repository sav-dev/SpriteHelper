using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class BackgroundConfig
    {
        [DataMember]
        public PaletteMapping[] PaletteMappings { get; set; }

        [DataMember]
        public Tile[] Tiles { get; set; }

        public void Write(string file)
        {            
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            var xmlSerializer = new XmlSerializer(typeof(BackgroundConfig));
            using (var stream = new FileStream(file, FileMode.CreateNew))
            {
                xmlSerializer.Serialize(stream, this);
            }
        }

        public static BackgroundConfig Read(string file)
        {
            BackgroundConfig config;
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(BackgroundConfig));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    config = (BackgroundConfig)xmlSerializer.Deserialize(memoryStream);
                }
            }

            return config;
        }
    }

    [DataContract]
    public class Tile
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public TileType Type { get; set; }

        [DataMember]
        public int WidthInSprites { get; set; }

        [DataMember]
        public int HeightSprites { get; set; }

        [DataMember]
        public int PaletteMapping { get; set; }

        // 0 1 2
        // 3 4 5
        // 6 7 8
        // ...
        [DataMember]        
        public int[] Sprites { get; set; }
    }

    [DataContract]
    public enum TileType
    {
        Blocking,
        NonBlocking,
        Threat
    }
}
