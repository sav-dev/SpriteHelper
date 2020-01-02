using SpriteHelper.NesGraphics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Tilesets
    {
        [DataMember]
        public string[] Sets { get; set; }

        public TilesetSpec[] LoadedSets;

        public TilesetSpec GetSpecById(int i) => this.LoadedSets.First(s => s.Id == i);

        public static Tilesets Read(string file)
        {
            Tilesets config;

            // Read the XML.
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(Tilesets));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    config = (Tilesets)xmlSerializer.Deserialize(memoryStream);
                }
            }

            config.LoadedSets = config.Sets.Select(s => TilesetSpec.Read(s + "\\_spec.xml")).ToArray();

            return config;
        }
    }

    [DataContract]
    public class TilesetSpec
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Directory { get; set; }

        [DataMember]
        public string[] NonBlocking { get; set; }

        [DataMember]
        public string[] Blocking { get; set; }

        [DataMember]
        public string[] Threats { get; set; }

        public string ChrPath() => string.Format(FileConstants.BgChr, this.Id);
        public string ChrImagePath() => this.Directory + "_chr.bmp";
        public string SpecPath() => this.Directory + "_back.xml";
        public MyBitmap GetChrImage() => MyBitmap.FromFile(this.ChrImagePath());
        public BackgroundConfig GetBgSpec() => BackgroundConfig.Read(this.SpecPath());

        public static TilesetSpec Read(string file)
        {
            TilesetSpec config;

            // Read the XML.
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(TilesetSpec));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    config = (TilesetSpec)xmlSerializer.Deserialize(memoryStream);
                }
            }

            return config;
        }
    }
}
