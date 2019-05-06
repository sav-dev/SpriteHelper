using SpriteHelper.Contract;
using System;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class AddEditElevatorDialog : Form
    {
        // todo: tab order
        // todo: close on esc

        //
        // Class.
        //

        // Private fields        
        Func<AddEditElevatorDialog, string> validationFunction;

        // Constructor.
        public AddEditElevatorDialog(
            Elevator existingElevator,
            Func<AddEditElevatorDialog, string> validationFunction)
        {
            InitializeComponent();
            this.sizeNumericUpDown.Minimum = Constants.MinElevatorSize;
            this.sizeNumericUpDown.Maximum = Constants.MaxElevatorSize;

            // Set header.
            var add = existingElevator == null;
            this.Text = add ? "Add elevator" : "Edit elevator";

            // Store the validation function.
            this.validationFunction = validationFunction;

            // Set movement types.
            // todo - support horizontal movement
            this.movementPanel.SetTypes(new[] { MovementType.None, MovementType.Vertical });

            // If editing, populate values, otherwise set defaults.
            if (!add)
            {
                this.sizeNumericUpDown.Value = existingElevator.Size;

                this.positionPanel.SetX(existingElevator.X);
                this.positionPanel.SetY(existingElevator.Y);

                this.movementPanel.MovementType = existingElevator.MovementType;
                this.movementPanel.Direction = existingElevator.Direction;
                this.movementPanel.SetSpeed(existingElevator.Speed);
                this.movementPanel.SetMin(existingElevator.MinPosition);
                this.movementPanel.SetMax(existingElevator.MaxPosition);
            }
            else
            {
                this.SetDefaultValues();
            }
            
            this.ExistingElevator = existingElevator;
            this.movementPanel.SpecialMovement = SpecialMovement.None;
        }

        // Set to true if OK is clicked.
        public bool Succeeded { get; private set; }

        // Set to null if elevator is being added.
        public Elevator ExistingElevator { get; private set; }

        //
        // Value getters.
        //

        //
        // Size
        //

        public int ElevatorSize => (int)this.sizeNumericUpDown.Value;

        //
        // Position
        //

        public bool TryGetX(out int x)
        {
            return this.positionPanel.TryGetX(out x);
        }

        public bool TryGetY(out int y)
        {
            return this.positionPanel.TryGetY(out y);
        }

        //
        // Movement
        //

        public MovementType MovementType => this.movementPanel.MovementType ?? MovementType.None;
        public Direction Direction => this.movementPanel.Direction ?? Direction.None;

        public bool TryGetSpeed(out int speed)
        {
            return this.movementPanel.TryGetSpeed(out speed);
        }

        public bool TryGetMin(out int min)
        {
            return this.movementPanel.TryGetMin(out min);
        }

        public bool TryGetMax(out int max)
        {
            return this.movementPanel.TryGetMax(out max);
        }

        //
        // Elevator
        //

        public bool TryGetElevator(out Elevator elevator)
        {
            // Set out value to null.
            elevator = null;

            // Create instance.
            var newElevator = new Elevator();

            // Set values that cannot fail.
            newElevator.Size = this.ElevatorSize;
            newElevator.MovementType = this.MovementType;
            newElevator.Direction = this.Direction;

            // Get values that can fail.
            int x, y, speed, min, max;
            if (!this.TryGetX(out x) ||
                !this.TryGetY(out y) ||
                !this.TryGetSpeed(out speed) ||
                !this.TryGetMin(out min) ||
                !this.TryGetMax(out max))
            {
                return false;
            }

            // Set values that can fail.
            newElevator.X = x;
            newElevator.Y = y;
            newElevator.Speed = speed;
            newElevator.MinPosition = min;
            newElevator.MaxPosition = max;

            // Set output value.
            elevator = newElevator;
            return true;
        }

        //
        // Handlers.
        //

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

        //
        // Other
        //

        private void SetDefaultValues()
        {
            this.sizeNumericUpDown.Value = 2;
            this.positionPanel.SetDefaultValues();
            this.movementPanel.SetDefaultValues();
        }
    }
}
