using System.Drawing;

namespace SpriteHelper
{
    public static class Extensions
    {
        public static double Luminance(this Color color)
        {
            return 0.299 * color.R + 0.587 * color.G + 0.114 * color.B;
        }
    }
}
