using System.Runtime.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class DoorAndKeycard
    {
        [DataMember]
        public bool DoorExists { get; set; }

        [DataMember]
        public int DoorX { get; set; }

        [DataMember]
        public int DoorY{ get; set; }

        [DataMember]
        public int KeycardX { get; set; }

        [DataMember]
        public int KeycardY { get; set; }
    }
}
