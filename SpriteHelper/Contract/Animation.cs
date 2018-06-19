using System.Runtime.Serialization;

namespace SpriteHelper.Contract
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
        public string Flips { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
