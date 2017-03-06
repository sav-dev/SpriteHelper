using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class BackgroundConfig
    {
        [DataMember]
        public string NonBlockingFile { get; set; }

        [DataMember]
        public string BlockingFile { get; set; }

        [DataMember]
        public string ThreatFile { get; set; }

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
    public class BackgroundFile
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FileName { get; set; }
    }

    [DataContract]
    public class Tile
    {
        public string Id { get { return TileIds.ConfigTileId(this.Type, this.X, this.Y); } }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public TileType Type { get; set; }
        
        // 0 2
        // 1 3
        [DataMember]        
        public int[] Sprites { get; set; }
    }

    [DataContract]
    public enum TileType
    {
        Blocking = 0,
        NonBlocking = 1,
        Threat = 2
    }

    public static class TileIds
    {
        public static string ConfigTileId(TileType tileType, int x, int y)
        {
            return string.Format("{0}-{1}-{2}", (int)tileType, x, y);
        }

        public static string PaletteTileId(int palette, TileType tileType, int x, int y)
        {
            return PaletteTileId(palette, ConfigTileId(tileType, x, y));
        }

        public static string PaletteTileId(int palette, string configId)
        {
            return string.Format("{0}-{1}", palette, configId);
        }

        public static Tuple<int, string> ParsePaletteId(string paletteId)
        {
            var split = paletteId.Split(new[] { '-' }, 2);
            return Tuple.Create(int.Parse(split[0]), split[1]);
        }
    }
}
