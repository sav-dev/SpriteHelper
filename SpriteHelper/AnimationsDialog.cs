using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteHelper
{
    public partial class AnimationsDialog : Form
    {
        private Dictionary<string, Dictionary<int, MyBitmap>> images;
        private int position = 0;

        public AnimationsDialog()
        {
            InitializeComponent();
            if (Defaults.Instance.ApplyDefaults)
            {
                this.PreLoad();
            }
        }

        private void PreLoad()
        {
            this.directoryTextBox.Text = Defaults.Instance.AnimationDirectory;
            this.LoadDirectory();
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

            if (this.moveCheckBox.Checked)
            {
                var bitmap = new MyBitmap(this.pictureBox.Width, image.Height, Color.Black);
                bitmap.DrawImage(image, position, 0);
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
            return this.directionCheckBox.Checked;
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

        private void DirectionCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            this.StartAnimation();
        }

        private void MoveCheckBoxCheckedChanged(object sender, EventArgs e)
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
            var imageWidth = this.GetImage().Width;

            if (this.GoingLeft())
            {
                this.position -= speed;
                if (this.position < 0)
                {
                    this.position = pictureBoxWidth - imageWidth;
                }
            }
            else
            {
                this.position += speed;
                if (this.position + imageWidth > pictureBoxWidth)
                {
                    this.position = 0;
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

            if (this.GoingLeft())
            {
                var pictureBoxWidth = this.pictureBox.Width;
                var imageWidth = this.GetImage().Width;
                this.position = pictureBoxWidth - imageWidth;
            }
            else
            {
                this.position = 0;
            }

            this.timer.Start();
            this.startButton.Enabled = false;
            this.stopButton.Enabled = true;
        }
    }
}
