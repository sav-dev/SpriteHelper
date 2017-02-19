using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpriteHelper
{
    [DataContract]
    public class BackgroundConfig
    {
        [DataMember]
        public PaletteMapping[] PaletteMappings { get; set; }

        [DataMember]
        public Tile[] Tiles { get; set; }

        public void Write(string file)
        {            
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            var xmlSerializer = new XmlSerializer(typeof(BackgroundConfig));
            using (var stream = new FileStream(file, FileMode.CreateNew))
            {
                xmlSerializer.Serialize(stream, this);
            }
        }
    }

    [DataContract]
    public class Tile
    {
        [DataMember]
        public int WidthInSprites { get; set; }

        [DataMember]
        public int HeightSprites { get; set; }

        [DataMember]
        public int PaletteMapping { get; set; }

        // 0 1 2
        // 3 4 5
        // 6 7 8
        // ...
        [DataMember]        
        public int[] Sprites { get; set; }
    }
}
