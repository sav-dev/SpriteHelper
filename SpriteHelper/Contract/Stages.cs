using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Stages
    {
        [DataMember]
        public StageData[] StageList { get; set; }

        public static Stages Read(string file)
        {
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(Stages));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    return (Stages)xmlSerializer.Deserialize(memoryStream);
                }
            }
        }
    }

    [DataContract]
    public class StageData
    {
        [DataMember]
        public int LevelCount { get; set; }
    }
}
