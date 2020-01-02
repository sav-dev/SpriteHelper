using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper.Contract
{
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

        public static TilesetSpec Read(string file)
        {
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
                    return (TilesetSpec)xmlSerializer.Deserialize(memoryStream);
                }
            }
        }
    }
}
