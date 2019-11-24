using SpriteHelper.Contract;
using System;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class EditDoorAndKeycardDialog : Form
    {
        private Func<EditDoorAndKeycardDialog, string> validationFunction;

        public EditDoorAndKeycardDialog(DoorAndKeycard existing, Func<EditDoorAndKeycardDialog, string> validationFunction)
        {
            InitializeComponent();
            this.Succeeded = false;
            this.validationFunction = validationFunction;

            if (existing != null)
            {
                this.doorExistsCheckBox.Checked = existing.DoorExists;
                this.doorPositionPanel.SetX(existing.DoorX);
                this.doorPositionPanel.SetY(existing.DoorY);
                this.keycardPositionPanel.SetX(existing.KeycardX);
                this.keycardPositionPanel.SetY(existing.KeycardY);
            }
            else
            {
                this.doorExistsCheckBox.Checked = false;
                this.doorPositionPanel.SetDefaultValues();
                this.keycardPositionPanel.SetDefaultValues();
            }
        }

        public bool Succeeded { get; set; }

        public bool DoorExists => this.doorExistsCheckBox.Checked;
        public bool TryGetDoorX(out int doorX) => this.doorPositionPanel.TryGetX(out doorX);
        public bool TryGetDoorY(out int doorY) => this.doorPositionPanel.TryGetY(out doorY);
        public bool TryGetKeycardX(out int keycardX) => this.keycardPositionPanel.TryGetX(out keycardX);
        public bool TryGetKeycardY(out int keycardY) => this.keycardPositionPanel.TryGetY(out keycardY);

        private void OkButtonClick(object sender, EventArgs e)
        {
            var validation = this.validationFunction(this);
            if (validation != null)
            {
                MessageBox.Show(validation);
            }
            else
            {
                this.Succeeded = true;
                this.Close();
            }
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
