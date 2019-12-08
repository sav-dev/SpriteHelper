using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class ConstSprites
    {
        [DataMember]
        public string[] Names { get; set; }

        public static ConstSprites Read(string file)
        {
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(ConstSprites));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    return (ConstSprites)xmlSerializer.Deserialize(memoryStream);
                }
            }
        }
    }
}
