using System.Runtime.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class PaletteMapping
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ToPalette { get; set; }

        [DataMember]
        public ColorMapping[] ColorMappings { get; set; }
    }
}
