using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class TransformDialog : Form
    {
        private TransformOperation result;
        private string selectedTile;

        public TransformOperation Result { get { return this.result; } }

        public TransformDialog(string selectedTile, Image selectedTileImage)
        {
            InitializeComponent();
            this.selectedTile = selectedTile;
            this.selectedTilePictureBox.Image = selectedTileImage;
            this.result = new TransformOperation();
            this.UpdatePanels();
        }

        private void RadioButtonCheckedChanged(object sender, EventArgs e)
        {
            this.UpdatePanels();
        }

        private void UpdatePanels()
        {
            foreach (var panel in new Panel[] { this.fillPanel, this.clonePanel })
            {
                panel.Enabled = false;
            }

            switch (this.Operation)
            {
                case TransformDialogResult.Fill:
                    this.fillPanel.Enabled = true;                    
                    this.clonePanel.Enabled = false;
                    this.moveItemsPanel.Enabled = false;
                    break;

                case TransformDialogResult.Clone:
                    this.fillPanel.Enabled = false;                    
                    this.clonePanel.Enabled = true;
                    this.moveItemsPanel.Enabled = false;
                    break;

                case TransformDialogResult.MoveItems:
                    this.fillPanel.Enabled = false;                    
                    this.clonePanel.Enabled = false;
                    this.moveItemsPanel.Enabled = true;
                    break;
            }
        }

        private TransformDialogResult Operation
        {
            get
            {
                if (this.fillRadioButton.Checked)
                {
                    return TransformDialogResult.Fill;
                }

                if (this.cloneRadioButton.Checked)
                {
                    return TransformDialogResult.Clone;
                }

                if (this.moveItemsRadioButton.Checked)
                {
                    return TransformDialogResult.MoveItems;
                }

                return TransformDialogResult.None;
            }
        }

        public class TransformOperation
        {
            public TransformDialogResult Operation { get; protected set; }
            public TransformOperation()
            {
                this.Operation = TransformDialogResult.None;
            }
        }

        public class FillOperation : TransformOperation
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public string SelectedTile { get; private set; }

            public FillOperation(int x, int y, int width, int height, string selectedTile)
            {
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
                this.SelectedTile = selectedTile;
                this.Operation = TransformDialogResult.Fill;
            }
        }

        public class CloneOperation : TransformOperation
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public int NewX { get; private set; }
            public int NewY { get; private set; }

            public CloneOperation(int x, int y, int width, int height, int newX, int newY)
            {
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
                this.NewX = newX;
                this.NewY = newY;
                this.Operation = TransformDialogResult.Clone;
            }
        }

        public class MoveItemsOperation : TransformOperation
        {
            public int DX { get; private set; }

            public MoveItemsOperation(int dx)
            {
                this.DX = dx;
                this.Operation = TransformDialogResult.MoveItems;
            }
        }

        public enum TransformDialogResult
        {
            None,
            Fill,
            Clone,
            MoveItems,
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            switch (this.Operation)
            {
                case TransformDialogResult.Fill:
                    {
                        var x = int.Parse(this.fillXTextbox.Text);
                        var y = int.Parse(this.fillYTextbox.Text);
                        var x2 = int.Parse(this.fillX2Textbox.Text);
                        var y2 = int.Parse(this.fillY2Textbox.Text);
                        this.result = new FillOperation(x, y, x2 - x + 1, y2 - y + 1, this.selectedTile);
                        break;
                    }
                case TransformDialogResult.Clone:
                    {
                        var x = int.Parse(this.cloneXTextbox.Text);
                        var y = int.Parse(this.cloneYTextbox.Text);
                        var x2 = int.Parse(this.cloneX2Textbox.Text);
                        var y2 = int.Parse(this.cloneY2Textbox.Text);
                        var newX = int.Parse(this.cloneNewXTextbox.Text);
                        var newY = int.Parse(this.cloneNewYTextbox.Text);
                        this.result = new CloneOperation(x, y, x2 - x + 1, y2 - y + 1, newX, newY);
                        break;
                    }
                case TransformDialogResult.MoveItems:
                    {
                        var dx = int.Parse(this.dxTextbox.Text);
                        this.result = new MoveItemsOperation(dx);
                        break;
                    }
            }

            this.Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
