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

        [DataMember]
        public int CopyOf { get; set; }

        [DataMember]
        public int AttsUpdate { get; set; }

        [DataMember]
        public bool AlwaysAnimate { get; set; }

        [DataMember]
        public int BulletId { get; set; }

        [DataMember]
        public int ExplosionId { get; set; }

        public void SetFrom(Animation other)
        {
            this.AnimationSpeed = other.AnimationSpeed;
            this.Flip = other.Flip;
            this.Offsets = other.Offsets;
            this.MaxHealth = other.MaxHealth;
        }

        // Same as ORIENTATION_* consts
        public Orientation Orientation => 
            this.Flip == Flip.None ? Orientation.None : (this.Flip == Flip.Horizontal ? Orientation.Horizontal : Orientation.Vertical);

        public override string ToString()
        {
            return this.Name;
        }
    }
}
