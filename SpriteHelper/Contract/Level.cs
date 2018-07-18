using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Level
    {
        [DataMember]
        public string[][] Tiles { get; set; }

        [DataMember]
        public Enemy[] Enemies { get; set; }

        [DataMember]
        public Elevator[] Elevators { get; set; }

        [DataMember]
        public Point PlayerStartingPosition { get; set; }

        [DataMember]
        public Point ExitPosition { get; set; }

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
