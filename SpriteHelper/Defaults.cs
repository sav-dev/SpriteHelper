using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class Defaults
    {
        [DataMember]
        public string DefaultApp { get; set; }

        [DataMember]
        public bool ApplyDefaults { get; set; }

        [DataMember]
        public string AnimationDirectory { get; set; }

        [DataMember]
        public string AnimationImage { get; set; }

        [DataMember]
        public string AnimationSpec { get; set; }

        [DataMember]
        public string Palette { get; set; }

        [DataMember]
        public string PalettesSpec { get; set; }

        [DataMember]
        public string AnimationOutput { get; set; }

        [DataMember]
        public string ConstChrInput { get; set; }

        [DataMember]
        public string CombinedChrOutput { get; set; }

        [DataMember]
        public string[] NonBlockingBackgrounds { get; set; }

        [DataMember]
        public string[] BlockingBackgrounds { get; set; }

        [DataMember]
        public string[] ThreatBackgrounds { get; set; }

        [DataMember]
        public int BgColor { get; set; }

        [DataMember]
        public string BackgroundImage { get; set; }
        
        [DataMember]
        public string BackgroundSpec { get; set; }

        [DataMember]
        public string BackgroundChr { get; set; }

        [DataMember]
        public string GraphicsDefaultDir { get; set; }

        [DataMember]
        public string DefaultLevel { get; set; }

        [DataMember]
        public string LevelsDefaultDir { get; set; }

        [DataMember]
        public string SpritesPalette { get; set; }

        [DataMember]
        public string BackgroundPalette { get; set; }

        private static Defaults instance;
        public static Defaults Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                instance = ReadDefaults();
                return instance;
            }
        }

        private static Defaults ReadDefaults()
        {
            var xml = File.ReadAllText("Defaults.xml");
            var xmlSerializer = new XmlSerializer(typeof(Defaults));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(xml);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    return (Defaults)xmlSerializer.Deserialize(memoryStream);
                }
            }
        }
    }
}