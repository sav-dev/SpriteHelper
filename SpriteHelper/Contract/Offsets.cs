using System.Runtime.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Offsets
    {
        [DataMember]
        public int XOff { get; set; }

        [DataMember]
        public int YOff { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public int GunXOff { get; set; }

        [DataMember]
        public int GunYOff { get; set; }

        [DataMember]
        public int GunXOffFlip { get; set; }

        [DataMember]
        public int GunYOffFlip { get; set; }
    }
}
