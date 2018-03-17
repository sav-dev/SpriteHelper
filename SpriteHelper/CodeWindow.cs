using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteHelper
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
