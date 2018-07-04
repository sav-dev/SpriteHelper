using SpriteHelper.Contract;
using SpriteHelper.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper.Dialogs
{
    public partial class AddEditEnemyDialog : Form
    {
        // todo: tab order
        // todo: close on esc

        //
        // Class.
        //

        // Private fields        
        Dictionary<string, Bitmap> bitmaps;
        Dictionary<string, MovementType[]> movements;
        Func<AddEditEnemyDialog, bool> validationFunction;

        // Constructor.
        public AddEditEnemyDialog(
            Enemy existingEnemy,
            Dictionary<string, Bitmap> bitmaps,
            Dictionary<string, MovementType[]> movements,
            Color backColor,
            Func<AddEditEnemyDialog, bool> validationFunction)
        {            
            this.InitializeComponent();

            // Set header.
            var add = existingEnemy == null;
            this.Text = add ? "Add enemy" : "Edit enemy";

            // Set right back color, store the dictionary of enemies.
            this.enemyPictureBox.BackColor = backColor;
            this.bitmaps = bitmaps;
            this.movements = movements;

            // Populate the combo box, select the right element (or first one if adding).
            this.enemyComboBox.Items.AddRange(bitmaps.Keys.ToArray());
            this.enemyComboBox.SelectedIndex = add ? 0 : this.enemyComboBox.Items.IndexOf(existingEnemy.Name);

            // Store the validation function.
            this.validationFunction = validationFunction;

            // If editing, populate values, otherwise set defaults.
            if (!add)
            {
                this.positionPanel.SetX(existingEnemy.X);
                this.positionPanel.SetY(existingEnemy.Y);

                this.movementPanel.MovementType = existingEnemy.MovementType;
                this.movementPanel.InitialFlip = existingEnemy.InitialFlip;
                this.movementPanel.SetMin(existingEnemy.MinPosition);
                this.movementPanel.SetMax(existingEnemy.MaxPosition);
            }

            this.ExistingEnemy = existingEnemy;
        }

        // Set to true if OK is clicked.
        public bool Succeeded { get; private set; }

        // Set to true if enemy is being added.
        public Enemy ExistingEnemy { get; private set; }

        //
        // Value getters.
        //
            
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

        public MovementType MovementType => this.movementPanel.MovementType;

        public bool InitialFlip => this.movementPanel.InitialFlip;

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
        // Enemy
        //

        public bool TryGetEnemy(SpriteConfig enConfig, out Enemy enemy)
        {
            // Set out value to null.
            enemy = null;

            // Create initialized instance.
            var name = (string)this.enemyComboBox.SelectedItem;
            var animation = enConfig.Animations.First(a => a.Name == name);
            var newEnemy = Enemy.CreateInitialized(animation);

            // Set values that cannot fail.
            newEnemy.MovementType = this.MovementType;
            newEnemy.InitialFlip = this.InitialFlip;            

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
            newEnemy.X = x;
            newEnemy.Y = y;
            newEnemy.Speed = speed;
            newEnemy.MinPosition = min;
            newEnemy.MaxPosition = max;
           
            // Set output value.
            enemy = newEnemy;
            return true;
        }

        //
        // Handlers.
        //

        private void EnemyComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.enemyPictureBox.Image = bitmaps[(string)this.enemyComboBox.SelectedItem];
            this.movementPanel.SetTypes(this.movements[(string)this.enemyComboBox.SelectedItem]);
            this.SetDefaultValues();
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            if (this.validationFunction(this))
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
        // Other.
        //

        private void SetDefaultValues()
        {
            this.positionPanel.SetDefaultValues();
            this.movementPanel.SetDefaultValues();
        }
    }
}
