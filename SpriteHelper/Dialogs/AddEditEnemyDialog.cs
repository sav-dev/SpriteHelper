﻿using SpriteHelper.Contract;
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
        Dictionary<string, bool> shooting;
        Func<AddEditEnemyDialog, string> validationFunction;

        // Constructor.
        public AddEditEnemyDialog(
            Enemy existingEnemy,
            Dictionary<string, Bitmap> bitmaps,
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
                this.movementPanel.Direction = existingEnemy.Direction;
                this.movementPanel.SpecialMovement = existingEnemy.SpecialMovement;
                this.movementPanel.SetSpeed(existingEnemy.Speed);
                this.movementPanel.SetMin(existingEnemy.MinPosition);
                this.movementPanel.SetMax(existingEnemy.MaxPosition);

                this.shootingPanel.SetFreq(existingEnemy.ShootingFrequency);
                this.shootingPanel.SetInitialFreq(existingEnemy.ShootingInitialFrequency);

                this.flipPanel.InitialFlip = existingEnemy.InitialFlip;
                this.flipPanel.ShouldFlip = existingEnemy.ShouldFlip;

                this.blinkingPanel.SetBlinkingType(existingEnemy.BlinkingType);
                this.blinkingPanel.SetFreq(existingEnemy.BlinkingFrequency);
                this.blinkingPanel.SetInitialFreq(existingEnemy.BlinkingInitialFrequency);
            }
            else
            {
                this.SetDefaultValues();
            }

            this.ExistingEnemy = existingEnemy;
        }

        // Set to true if OK is clicked.
        public bool Succeeded { get; private set; }

        // Set to null if enemy is being added.
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

        public MovementType MovementType => this.movementPanel.MovementType ?? MovementType.None;
        public Direction Direction => this.movementPanel.Direction ?? Direction.None;
        public SpecialMovement SpecialMovement => this.movementPanel.SpecialMovement ?? SpecialMovement.None;

        public bool InitialFlip => this.flipPanel.InitialFlip;
        public bool ShouldFlip => this.flipPanel.ShouldFlip;

        public double GetSpeed()
        {
            return this.movementPanel.GetSpeed();
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
        // Blinking
        //

        public bool TryGetBlinkingType(out BlinkingType blinkingType)
        {
            return this.blinkingPanel.TryGetBlinkingType(out blinkingType);
        }

        public bool TryGetBlinkingFreq(out int freq)
        {
            return this.blinkingPanel.TryGetFreq(out freq);
        }

        public bool TryGetBlinkingInitialFreq(out int initialFreq)
        {
            return this.blinkingPanel.TryGetInitialFreq(out initialFreq);
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
            newEnemy.Direction = this.Direction;
            newEnemy.SpecialMovement = this.SpecialMovement;
            newEnemy.InitialFlip = this.InitialFlip;
            newEnemy.ShouldFlip = this.ShouldFlip;
            newEnemy.Speed = this.GetSpeed();

            // Get values that can fail.
            BlinkingType blinkingType;
            int x, y, min, max, shootingFreq, shootingInitialFreq, blinkingFreq, blinkingInitialFreq;
            if (!this.TryGetX(out x) || 
                !this.TryGetY(out y) || 
                !this.TryGetMin(out min) || 
                !this.TryGetMax(out max) ||
                !this.TryGetShootingFreq(out shootingFreq) ||
                !this.TryGetShootingInitialFreq(out shootingInitialFreq) ||
                !this.TryGetBlinkingType(out blinkingType) ||
                !this.TryGetBlinkingFreq(out blinkingFreq) ||
                !this.TryGetBlinkingInitialFreq(out blinkingInitialFreq))
            {
                return false;
            }

            // Set values that can fail.
            newEnemy.X = x;
            newEnemy.Y = y;
            newEnemy.MinPosition = min;
            newEnemy.MaxPosition = max;
            newEnemy.ShootingFrequency = shootingFreq;
            newEnemy.ShootingInitialFrequency = shootingInitialFreq;
            newEnemy.BlinkingType = blinkingType;
            newEnemy.BlinkingFrequency = blinkingFreq;
            newEnemy.BlinkingInitialFrequency = blinkingInitialFreq;

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
            this.movementPanel.SetTypes(new MovementType[] { MovementType.None, MovementType.Horizontal, MovementType.Vertical });
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
            this.flipPanel.SetDefaultValues();
            this.blinkingPanel.SetDefaultValues();
        }
    }
}
