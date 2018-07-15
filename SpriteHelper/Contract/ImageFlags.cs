using System;

namespace SpriteHelper.Contract
{
    [Flags]
    public enum ImageFlags
    {
        None = 0,
        Palettes = 1,
        Boxes = 2,
        VFlip = 4,
        HFlip = 8,
        Transparent = 16,
        TransparentBg = 32
    }
}
