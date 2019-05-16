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
        public int AnimationSpeed { get; set; }

        [DataMember]
        public Frame[] Frames { get; set; }

        [DataMember]
        public Flip Flip { get; set; }

        [DataMember]
        public Offsets Offsets { get; set; }

        [DataMember]
        public int MaxHealth { get; set; }

        // Same as ORIENTATION_* consts
        public Orientation Orientation => 
            this.Flip == Flip.None ? Orientation.None : (this.Flip == Flip.Horizontal ? Orientation.Horizontal : Orientation.Vertical);

        public override string ToString()
        {
            return this.Name;
        }
    }
}
