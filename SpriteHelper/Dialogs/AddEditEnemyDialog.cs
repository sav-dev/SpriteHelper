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
        Dictionary<string, bool> shooting;
        Func<AddEditEnemyDialog, string> validationFunction;

        // Constructor.
        public AddEditEnemyDialog(
            Enemy existingEnemy,
            Dictionary<string, Bitmap> bitmaps,
            Dictionary<string, MovementType[]> movements,
            Dictionary<string, bool> shooting,
            Color backColor,
            Func<AddEditEnemyDialog, string> validationFunction)
        {            
            this.InitializeComponent();

            // Set header.
            var add = existingEnemy == null;
            this.Text = add ? "Add enemy" : "Edit enemy";

            // Set right back color, store the dictionary of enemies.
            this.enemyPictureBox.BackColor = backColor;
            this.bitmaps = bitmaps;
            this.movements = movements;
            this.shooting = shooting;

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
                this.movementPanel.SetSpeed(existingEnemy.Speed);
                this.movementPanel.InitialFlip = existingEnemy.InitialFlip;
                this.movementPanel.SetMin(existingEnemy.MinPosition);
                this.movementPanel.SetMax(existingEnemy.MaxPosition);

                this.shootingPanel.SetFreq(existingEnemy.ShootingFrequency);
                this.shootingPanel.SetInitialFreq(existingEnemy.ShootingInitialFrequency);
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
        // Shooting
        //

        public bool TryGetShootingFreq(out int freq)
        {
            return this.shootingPanel.TryGetFreq(out freq);            
        }

        public bool TryGetShootingInitialFreq(out int initialFreq)
        {
            return this.shootingPanel.TryGetInitialFreq(out initialFreq);            
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
            int x, y, speed, min, max, shootingFreq, shootingInitialFreq;
            if (!this.TryGetX(out x) || 
                !this.TryGetY(out y) || 
                !this.TryGetSpeed(out speed) || 
                !this.TryGetMin(out min) || 
                !this.TryGetMax(out max) ||
                !this.TryGetShootingFreq(out shootingFreq) ||
                !this.TryGetShootingInitialFreq(out shootingInitialFreq))
            {
                return false;
            }

            // Set values that can fail.
            newEnemy.X = x;
            newEnemy.Y = y;
            newEnemy.Speed = speed;
            newEnemy.MinPosition = min;
            newEnemy.MaxPosition = max;
            newEnemy.ShootingFrequency = shootingFreq;
            newEnemy.ShootingInitialFrequency = shootingInitialFreq;

            // Set output value.
            enemy = newEnemy;
            return true;
        }

        //
        // Handlers.
        //

        private void EnemyComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedEnemy = (string)this.enemyComboBox.SelectedItem;

            this.enemyPictureBox.Image = bitmaps[selectedEnemy];
            this.movementPanel.SetTypes(this.movements[selectedEnemy]);
            this.shootingPanel.Enabled = this.shooting[selectedEnemy];
            this.SetDefaultValues();
        }

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
        // Other.
        //

        private void SetDefaultValues()
        {
            this.positionPanel.SetDefaultValues();
            this.movementPanel.SetDefaultValues();
            this.shootingPanel.SetDefaultValues();
        }
    }
}
