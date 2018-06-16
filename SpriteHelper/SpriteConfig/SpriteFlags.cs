using System;

namespace SpriteHelper
{
    [Flags]
    public enum SpriteFlags
    {
        None = 0,
        Palettes = 1,
        Boxes = 2,
        VFlip = 4,
        HFlip = 8
    }
}
