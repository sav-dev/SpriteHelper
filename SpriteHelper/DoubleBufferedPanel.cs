using System.Windows.Forms;

namespace SpriteHelper
{
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel() : base()
        {
            this.DoubleBuffered = true;
        }
    }
}