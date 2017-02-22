using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class Level
    {
        [DateMember]
        public string[][] Tiles { get; set; }

        public void Write(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            var xmlSerializer = new XmlSerializer(typeof(Level));
            using (var stream = new FileStream(file, FileMode.CreateNew))
            {
                xmlSerializer.Serialize(stream, this);
            }
        }

        public static Level Read(string file)
        {
            Level level;
            var xml = File.ReadAllText(file);
            var xmlSerializer = new XmlSerializer(typeof(Level));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    return (Level)xmlSerializer.Deserialize(memoryStream);
                }
            }
        }
    }
}
