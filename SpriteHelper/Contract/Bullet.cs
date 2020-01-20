using System.Runtime.Serialization;

namespace SpriteHelper.Contract
{
    [DataContract]
    public class Bullet
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int BulletId { get; set; }

        [DataMember]
        public int SpriteId { get; set; }

        [DataMember]
        public bool HFlip { get; set; }

        [DataMember]
        public bool VFlip { get; set; }

        [DataMember]
        public Orientation Orientation { get; set; }

        [DataMember]
        public int BulletDx { get; set; }

        [DataMember]
        public int BulletDy { get; set; }

        [DataMember]
        public int BulletDxFlip { get; set; }

        [DataMember]
        public int BulletDyFlip { get; set; }

        [DataMember]
        public int BoxWidth { get; set; }

        [DataMember]
        public int BoxHeight { get; set; }

        [DataMember]
        public int BoxDx { get; set; }

        [DataMember]
        public int BoxDy { get; set; }

        [DataMember]
        public int BoxDxFlip { get; set; }

        [DataMember]
        public int BoxDyFlip { get; set; }

        [DataMember]
        public string Sound { get; set; }

        public Sprite Sprite { get; set; }

        public ImageFlags GetFlipFlags(bool flip)
        {
            return flip ? GetFlipFlagsFlip() : GetFlipFlagsNoFlip();
        }

        public ImageFlags GetFlipFlagsNoFlip()
        {
            var flags = ImageFlags.None;
            if (this.HFlip)
            {
                flags |= ImageFlags.HFlip;
            }

            if (this.VFlip)
            {
                flags |= ImageFlags.VFlip;
            }

            return flags;
        }

        public ImageFlags GetFlipFlagsFlip()
        {
            var flags = ImageFlags.None;

            if (this.Orientation == Orientation.Horizontal)
            {
                if (!this.HFlip)
                {
                    flags |= ImageFlags.HFlip;
                }

                if (this.VFlip)
                {
                    flags |= ImageFlags.VFlip;
                }
            }
            else if (this.Orientation == Orientation.Vertical)
            {
                if (this.HFlip)
                {
                    flags |= ImageFlags.HFlip;
                }

                if (!this.VFlip)
                {
                    flags |= ImageFlags.VFlip;
                }
            }
            else
            {
                throw new System.Exception("Bullet orientation must be vertical or horizontal");
            }

            return flags;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
