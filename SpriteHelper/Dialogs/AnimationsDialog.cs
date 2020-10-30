using SpriteHelper.NesGraphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class AnimationsDialog : Form
    {
        private Dictionary<string, Dictionary<int, MyBitmap>> images;
        private Point position;
        private bool loaded;
        private string direction;

        public AnimationsDialog()
        {
            this.loaded = false;
            InitializeComponent();
            this.bgComboBox.SelectedIndex = 0;
            this.directionComboBox.SelectedIndex = 0;
            this.loaded = true;
        }

        private void LoadButtonClick(object sender, EventArgs e)
        {
            this.LoadDirectory();
        }

        private void LoadDirectory()
        {
            this.images = new Dictionary<string, Dictionary<int, MyBitmap>>();
            var directory = new DirectoryInfo(this.directoryTextBox.Text);

            int? width = null;
            int? height = null;

            foreach (var file in directory.EnumerateFiles().OrderBy(f => f.Name))
            {
                int ignore;
                if (!int.TryParse(file.Name.Substring(0, file.Name.Length - file.Extension.Length), out ignore))
                {
                    continue;
                }

                this.images.Add(file.Name, new Dictionary<int, MyBitmap>());
                var image = MyBitmap.FromFile(file.FullName);

                if (width == null)
                {
                    width = image.Width;
                }
                else if (width != image.Width)
                {
                    throw new Exception("Invalid width " + file.Name);
                }

                if (height == null)
                {
                    height = image.Height;
                }
                else if (height != image.Height)
                {
                    throw new Exception("Invalid height " + file.Name);
                }

                for (var zoom = 1; zoom <= 10; zoom++)
                {
                    this.images[file.Name].Add(zoom, image.Scale(zoom));
                }
            }

            this.framesListBox.Items.Clear();
            foreach (var item in images.Keys)
            {
                this.framesListBox.Items.Add(item);
            }

            this.StartAnimation();
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            this.StopAnimation();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            this.StartAnimation();
        }

        private void UpdateImage()
        {
            var image = this.GetImage(); 

            if (this.direction != "None")
            {
                var bitmap = new MyBitmap(this.pictureBox.Width, this.pictureBox.Height, GetBgColor());
                bitmap.DrawImage(image, position.X, position.Y);
                this.pictureBox.Image = bitmap.ToBitmap();
            }            
            else
            {
                this.pictureBox.Image = image.ToBitmap();
            }
        }

        private MyBitmap GetImage()
        {
            return this.images[this.framesListBox.SelectedItem.ToString()][(int)this.zoomPicker.Value];
        }

        private bool GoingLeft()
        {
            return false;
        }

        private void FramesListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateImage();
        }

        private void ZoomPickerValueChanged(object sender, EventArgs e)
        {
            this.StartAnimation();
        }

        private void SpeedPickerValueChanged(object sender, EventArgs e)
        {
            this.StartAnimation();
        }

        private void MovSpeedPickerValueChanged(object sender, EventArgs e)
        {
            this.StartAnimation();
        }

        private void UpdateTimer()
        {
            this.timer.Interval = (int)((1.0 / 60.0) * (int)this.speedPicker.Value * 1000);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var previousIndex = this.framesListBox.SelectedIndex;
            this.framesListBox.SelectedIndex = (this.framesListBox.SelectedIndex + 1) % this.framesListBox.Items.Count;
            var newIndex = this.framesListBox.SelectedIndex;

            if (previousIndex == newIndex)
            {
                // Only one frame, call UpdateImage manually
                this.UpdateImage();
            }

            var speed = (int)this.movSpeedPicker.Value * (int)this.zoomPicker.Value;
            var pictureBoxWidth = this.pictureBox.Width;
            var pictureBoxHeight = this.pictureBox.Height;
            var image = this.GetImage();
            var imageWidth = image.Width;
            var imageHeight = image.Height;

            if (this.direction == "Left")
            {
                this.position.X -= speed;
                if (this.position.X < 0)
                {
                    this.position.X = pictureBoxWidth - imageWidth;
                }
            }
            else if (this.direction == "Right")
            {
                this.position.X += speed;
                if (this.position.X + imageWidth > pictureBoxWidth)
                {
                    this.position.X = 0;
                }
            }
            else if (this.direction == "Up")
            {
                this.position.Y -= speed;
                if (this.position.Y < 0)
                {
                    this.position.Y = pictureBoxHeight - imageHeight;
                }
            }
            else if (this.direction == "Down")
            {
                this.position.Y += speed;
                if (this.position.Y + imageHeight > pictureBoxHeight)
                {
                    this.position.Y = 0;
                }
            }
        }

        private void StopAnimation()
        {
            this.timer.Stop();
            this.startButton.Enabled = true;
            this.stopButton.Enabled = false;
        }

        private void StartAnimation()
        {
            this.framesListBox.SelectedIndex = 0;
            this.UpdateTimer();

            var pictureBoxWidth = this.pictureBox.Width;
            var pictureBoxHeight = this.pictureBox.Height;
            var image = this.GetImage();
            var imageWidth = image.Width;
            var imageHeight = image.Height;

            if (this.direction == "Left")
            {
                this.position = new Point(pictureBoxWidth - imageWidth, (pictureBoxHeight - imageHeight) / 2);
            }
            else if (this.direction == "Right")
            {
                this.position = new Point(0, (pictureBoxHeight - imageHeight) / 2);
            }
            else if (this.direction == "Up")
            {
                this.position = new Point((pictureBoxWidth - imageWidth) / 2, pictureBoxHeight - imageHeight);
            }
            else if (this.direction == "Down")
            {
                this.position = new Point((pictureBoxWidth - imageWidth) / 2, 0);
            }

            this.timer.Start();
            this.startButton.Enabled = false;
            this.stopButton.Enabled = true;
        }

        private void BgComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.pictureBox.BackColor = GetBgColor();            
        }

        private Color GetBgColor()
        {
            if ((string)this.bgComboBox.SelectedItem == "White")
            {
                return Color.White;
            }

            if ((string)this.bgComboBox.SelectedItem == "Black")
            {
                return Color.Black;
            }

            return Color.Black;
        }

        private void DirectionComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.direction = (string)this.directionComboBox.SelectedItem;
            if (this.loaded)
            {
                this.StartAnimation();
            }
        }
    }
}
