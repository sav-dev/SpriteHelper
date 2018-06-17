using System.Windows.Forms;

namespace SpriteHelper.Controls
{
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel() : base()
        {
            this.DoubleBuffered = true;
        }
    }
}