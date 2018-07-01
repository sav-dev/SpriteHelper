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
        //
        // Class.
        //

        // Controls.
        PositionPanel positionPanel;

        // Private fields
        Dictionary<string, Bitmap> enemies;
        Func<AddEditEnemyDialog, bool> validationFunction;

        // Constructor.
        public AddEditEnemyDialog(
            Enemy existingEnemy,
            Dictionary<string, Bitmap> enemies,
            Color backColor,
            Func<AddEditEnemyDialog, bool> validationFunction)
        {            
            this.InitializeComponent();

            // Create controls.
            this.positionPanel = new PositionPanel();
            this.positionPanel.Dock = DockStyle.Fill;
            this.positionGroupBox.Controls.Add(this.positionPanel);

            // Set header.
            var add = existingEnemy == null;
            this.Text = add ? "Add enemy" : "Edit enemy";

            // Set right back color, store the dictionary of enemies.
            this.enemyPictureBox.BackColor = backColor;
            this.enemies = enemies;

            // Populate the combo box, select the right element.
            this.enemyComboBox.Items.AddRange(enemies.Keys.ToArray());
            this.enemyComboBox.SelectedIndex = add ? 0 : this.enemyComboBox.Items.IndexOf(existingEnemy.Name);

            // Store the validation function.
            this.validationFunction = validationFunction;

            // If editing, populate values.
            if (!add)
            {
                this.positionPanel.SetX(existingEnemy.X);
                this.positionPanel.SetY(existingEnemy.Y);
                this.initialFlipCheckBox.Checked = existingEnemy.InitialFlip;
            }
        }

        // Set to true if OK is clicked.
        public bool Succeeded { get; private set; }

        //
        // Value getters.
        //
            
        public bool TryGetX(out int x)
        {
            return this.positionPanel.TryGetX(out x);
        }
        
        public bool TryGetY(out int y)
        {
            return this.positionPanel.TryGetY(out y);
        }

        public bool InitialFlip => this.initialFlipCheckBox.Checked;

        public bool TryGetEnemy(SpriteConfig enConfig, out Enemy enemy)
        {
            // Set out value to null.
            enemy = null;

            // Create initialized instance.
            var name = (string)this.enemyComboBox.SelectedItem;
            var animation = enConfig.Animations.First(a => a.Name == name);
            var newEnemy = Enemy.CreateInitialized(animation);

            // Set values that cannot fail.
            newEnemy.InitialFlip = this.InitialFlip;

            // Get values that can fail.
            int x, y;
            if (!this.TryGetX(out x) || !this.TryGetY(out y))
            {
                return false;
            }

            // Set values that can fail.
            newEnemy.X = x;
            newEnemy.Y = y;
           
            // Set output value.
            enemy = newEnemy;
            return true;
        }

        //
        // Handlers.
        //

        private void EnemyComboBoxSelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.enemyPictureBox.Image = enemies[(string)this.enemyComboBox.SelectedItem];
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
    }
}
