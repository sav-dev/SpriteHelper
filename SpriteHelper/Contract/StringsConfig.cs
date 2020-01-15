using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class StringsConfig
    {
        [DataMember]
        public StringConfig[] Strings { get; set; }

        public static StringsConfig Read(string file)
        {
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(StringsConfig));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    return (StringsConfig)xmlSerializer.Deserialize(memoryStream);
                }
            }
        }
    }

    [DataContract]
    public class StringConfig
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
