using SpriteHelper.Contract;
using SpriteHelper.NesGraphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class StoryDialog : Form
    {
        private Story story;

        private const int PressStartX = 10;
        private const int PressStartY = 24;
        private const string PressStart = "press start";

        private const int BitmapZoom = 2;

        public StoryDialog()
        {
            InitializeComponent();
            this.fontTextBox.Text = FileConstants.Font;
            this.stringsTextBox.Text = FileConstants.Strings;            
        }

        private void BrowseButtonClick(object sender, System.EventArgs e)
        {
            var openFileDialog = new OpenFileDialog { InitialDirectory = FileConstants.StoriesDir, Filter = "(*.xml)|*.xml" };
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName))
            {
                return;
            }

            this.storyTextBox.Text = openFileDialog.FileName;
        }

        private void LoadButtonClick(object sender, System.EventArgs e)
        {
            this.story = Story.Read(this.storyTextBox.Text);
            var stringsConfig = StringsConfig.Read(this.stringsTextBox.Text);
            var tiles = new List<MyBitmap>();
            TitleDialog.ProcessFont(this.fontTextBox.Text, tiles);

            var strings = new List<StringToRedner>();

            foreach (var strData in this.story.Strings)
            {
                if (strData.X < 1 || strData.Y < 1)
                {
                    throw new System.Exception("Invalid string position");
                }

                var str = stringsConfig.Strings.First(s => s.Id == strData.StringId).Value;
                if (str.Length > 30)
                {
                    throw new System.Exception("Invalid string length");
                }

                strings.Add(new StringToRedner(strData.X, strData.Y, str));
            }

            strings.Add(new StringToRedner(PressStartX, PressStartY, PressStart));

            var bmp = new MyBitmap(512, 448, System.Drawing.Color.Black);
            foreach (var str in strings)
            {
                var x = str.X;
                foreach (var chr in str.Str.ToCharArray())
                {
                    var tile = tiles[TitleDialog.Chars.IndexOf(chr)].Scale(BitmapZoom);
                    bmp.DrawImage(tile, x * Constants.SpriteWidth * BitmapZoom, str.Y * Constants.SpriteHeight * BitmapZoom);
                    x++;
                }
            }

            this.pictureBox.Image = bmp.ToBitmap();
        }

        private void ExportButtonClick(object sender, System.EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { InitialDirectory = FileConstants.StoriesDir, Filter = "binary files (*.bin)|*.bin" };
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                this.Export(saveFileDialog.FileName);
            }
        }

        private void Export(string fileName)
        {
            // Export format:
            // 1 byte: number of strings
            // then for each string:
            //   1 byte: x
            //   1 byte: y
            //   1 byte: stirng id

            var payload = new List<byte>();
            payload.Add((byte)this.story.Strings.Length);
            foreach (var str in this.story.Strings)
            {
                payload.Add((byte)str.X);
                payload.Add((byte)str.Y);
                payload.Add((byte)str.StringId);
            }

            File.WriteAllBytes(fileName, payload.ToArray());
        }

        private class StringToRedner
        {
            public int X { get; }
            public int Y { get; }
            public string Str { get; }

            public StringToRedner(int x, int y, string str)
            {
                this.X = x;
                this.Y = y;
                this.Str = str;
            }
        }
    }
}
