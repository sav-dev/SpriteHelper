﻿using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class Defaults
    {
        [DataMember]
        public string AppToRun { get; set; }

        [DataMember]
        public string AnimationImage { get; set; }

        [DataMember]
        public string AnimationSpec { get; set; }

        [DataMember]
        public string Palette { get; set; }

        [DataMember]
        public string PalettesSpec { get; set; }

        [DataMember]
        public string NonBlockingBackground { get; set; }

        [DataMember]
        public string BlockingBackground { get; set; }

        [DataMember]
        public string ThreatBackground { get; set; }

        [DataMember]
        public int BgColor { get; set; }

        [DataMember]
        public string BackgroundChr { get; set; }
        
        [DataMember]
        public string BackgroundSpec { get; set; }
        
        [DataMember]
        public string DefaultDir { get; set; }

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