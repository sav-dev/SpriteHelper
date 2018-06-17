using System.Runtime.Serialization;

namespace SpriteHelper.Config
{
    [DataContract]
    public class Animation
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int FPS { get; set; }

        [DataMember]
        public Frame[] Frames { get; set; }

        [DataMember]
        public int PlatformBoxWidth { get; set; }

        [DataMember]
        public int PlatformBoxHeight { get; set; }

        [DataMember]
        public int ThreatBoxWidth { get; set; }

        [DataMember]
        public int ThreatBoxHeight { get; set; }

        [DataMember]
        public int GunXOffL { get; set; }

        [DataMember]
        public int GunXOffR { get; set; }

        [DataMember]
        public int GunYOff { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
