using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpriteHelper
{
    public partial class LevelSplitView : Form
    {
        private Bitmap bitmap1;
        private Bitmap bitmap2;
        private bool bitmap1Set;
        private int scale;

        private readonly Color[] Colors = new[]
                          {
                                      Color.White,
                                      Color.Black,
                                      Color.Red,
                                      Color.Blue,
                                      Color.Yellow,
                                      Color.Pink,
                                      Color.Violet,
                                      Color.Brown,
                                      Color.Green,
                                      Color.Gray,
                                      Color.Crimson,
                                      Color.Aquamarine,
                                      Color.Gold,
                                      Color.LightPink,
                                      Color.Purple,
                                      Color.SandyBrown,
                                      Color.LightGreen,
                                      Color.LightGray
                                  };

        public LevelSplitView(bool[][] level)
        {
            InitializeComponent();

            var width = level.Length;
            var height = level[0].Length;
            this.scale = this.pictureBox.Width / width;

            // Bitmap 1 - black and white
            var bitmap1Array = new int[width][];
            for (var x = 0; x < width; x++)
            {
                bitmap1Array[x] = new int[height];
                for (var y = 0; y < height; y++)
                {
                    bitmap1Array[x][y] = level[x][y] ? 1 : 0;
                }
            }

            this.bitmap1 = this.CreateBitmap(bitmap1Array, width, height);

            // Bitmap 2 - with colors
            var result = LevelEditor.SplitIntoRectangles(level);
            var bitmap2Array = new int[width][];
            for (var x = 0; x < width; x++)
            {
                bitmap2Array[x] = new int[height];
                for (var y = 0; y < height; y++)
                {
                    bitmap2Array[x][y] = 0;
                }
            }

            var index = 2;
            foreach (var kvp in result)
            {
                var screen = kvp.Key;
                var value = kvp.Value;

                foreach (var rectangle in value)
                {
                    for (var x = rectangle.Item1.X; x <= rectangle.Item2.X; x++)
                    {
                        for (var y = rectangle.Item1.Y; y <= rectangle.Item2.Y; y++)
                        {
                            bitmap2Array[x + screen * Constants.ScreenWidthInTiles][y] = index;
                        }
                    }

                    index++;
                }
            }

            bitmap2 = this.CreateBitmap(bitmap2Array, width, height, true);

            // Set bitmap 1 for now
            this.pictureBox.Image = bitmap1;
            this.bitmap1Set = true;
        }

        private void PictureBox1Click(object sender, EventArgs e)
        {
            this.bitmap1Set = !this.bitmap1Set;
            this.pictureBox.Image = this.bitmap1Set ? this.bitmap1 : this.bitmap2;
        }

        private Bitmap CreateBitmap(int[][] input, int width, int height, bool addLines = false)
        {
            var bitmap = new MyBitmap(width, height);
            
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var index = input[x][y];
                    if (index > 1)
                    {
                        index = index % (Colors.Length - 2) + 2;
                    }

                    var color = Colors[index];
                    bitmap.SetPixel(color, x, y);
                }
            }

            var scaledBitmap = bitmap.Scale(this.scale);

            if (addLines)
            {
                for (var screen = 0; screen < width * this.scale; screen += Constants.ScreenWidthInTiles * this.scale)
                {
                    for (var y = 0; y < height * this.scale; y++)
                    {
                        scaledBitmap.SetPixel(Color.Black, screen, y);
                    }      
                }

                for (var y = 0; y < height * this.scale; y++)
                {
                    scaledBitmap.SetPixel(Color.Black, width * this.scale - 1, y);
                }
            }

            return scaledBitmap.ToBitmap();
        }
    }
}
