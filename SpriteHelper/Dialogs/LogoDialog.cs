using SpriteHelper.NesGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class LogoDialog : Form
    {
        public LogoDialog()
        {
            InitializeComponent();
        }

        private void LogoDialogLoad(object sender, EventArgs e)
        {
            var logo = MyBitmap.FromFile(FileConstants.Logo)
            var tiles = new List<MyBitmap>();

            for (var x = 0; x < logo.Width; x += Constants.SpriteWidth)
            {
                for (var y = 0; y < logo.Height; y += Constants.SpriteHeight)
                {
                    var tile = logo.GetPart(x, y, Constants.SpriteWidth, Constants.SpriteHeight);
                    if (!tiles.Any(t => t.Equals(tile)))
                    {
                        tiles.Add(tile);
                    }
                }
            }
        }
    }
}
