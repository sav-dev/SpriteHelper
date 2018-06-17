using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class CodeWindow : Form
    {
        public CodeWindow(string text)
        {
            InitializeComponent();
            this.textBox.Text = text;
        }
    }
}
