using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteHelper
{
    public static class FileConstants
    {
        public const string ChrDirectory = @"C:\users\tomas\documents\nes\github\platformer\PlatformerGraphics\chr\";

        public const string ConstChr = ChrDirectory + "const.chr";
        public const string SprChr = ChrDirectory + "spr.chr";
        
        // todo 0008: update this
        public const string BgChr = ChrDirectory + "bg_0.chr";
        
        public const string ConstChrConfig = @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Sprites\constSprites.xml";

        public const string PalettesSpec = @"C:\users\tomas\documents\nes\github\platformer\PlatformerGraphics\palettes\palettes.xml";
        public const string SprPalette = @"C:\users\tomas\documents\nes\github\platformer\PlatformerGraphics\palettes\spr.bin";
        public const string BgPalettes = @"C:\users\tomas\documents\nes\github\platformer\PlatformerGraphics\palettes\bg.bin";
        public const string PalettePicture = @"C:\users\tomas\documents\nes\github\platformer\PlatformerGraphics\palettes\palette.png";

        public const string BulletSpec = @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Sprites\bullets.xml";        
        public const string PlayerSpec = @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Sprites\player.xml";
        public const string ExplosionsSpec = @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Sprites\explosions.xml";
        public const string EnemiesSpec = @"C:\Users\tomas\Documents\NES\GitHub\Platformer\PlatformerGraphics\Sprites\enemies.xml";

        public const string LevelsDir = @"C:\Users\tomas\Documents\NES\GitHub\Platformer\data\levels";

        public const string Desktop = @"C:\users\tomas\desktop";
    }
}
