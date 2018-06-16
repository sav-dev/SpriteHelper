using System.Drawing;
using System.Runtime.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class ColorMapping
    {
        [DataMember]
        public int R { get; set; }

        [DataMember]
        public int G { get; set; }

        [DataMember]
        public int B { get; set; }

        [DataMember]
        public int To { get; set; }

        public Color Color
        {
            get
            {
                return Color.FromArgb(this.R, this.G, this.B);
            }
        }
    }
}
