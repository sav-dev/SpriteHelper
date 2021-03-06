﻿using SpriteHelper.Contract;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SpriteHelper.Controls
{
    public partial class MovementPanel : UserControl
    {
        public MovementPanel()
        {
            InitializeComponent();
            
            for (var i = 0; i < 255; i++)
            {
                if (Enum.IsDefined(typeof(Contract.SpecialMovement), i))
                {
                    this.specialMovementComboBox.Items.Add(((SpecialMovement)i).ToString());
                }
            }
        }

        private void InputTextChanged(object sender, EventArgs e)
        {
            int minTiles, minOffset, maxTiles, maxOffset;

            if (!int.TryParse(minTilesTextBox.Text, out minTiles) || !int.TryParse(minOffsetTextBox.Text, out minOffset))
            {
                minTextBox.Text = "Invalid";
            }
            else
            {
                minTextBox.Text = $"{minTiles * Constants.BackgroundTileWidth + minOffset}";
            }

            if (!int.TryParse(maxTilesTextBox.Text, out maxTiles) || !int.TryParse(maxOffsetTextBox.Text, out maxOffset))
            {
                maxTextBox.Text = "Invalid";
            }
            else
            {
                maxTextBox.Text = $"{maxTiles * Constants.BackgroundTileHeight + maxOffset}";
            }
        }

        public MovementType? MovementType
        {
            get
            {
                var value = this.movementComboBox.SelectedItem as string;
                return (MovementType)Enum.Parse(typeof(MovementType), value);
            }
            set
            {
                this.movementComboBox.SelectedItem = value?.ToString();
            }
        }

        public SpecialMovement? SpecialMovement
        {
            get
            {
                var value = this.specialMovementComboBox.SelectedItem as string;
                return (SpecialMovement)Enum.Parse(typeof(SpecialMovement), value);
            }
            set
            {
                this.specialMovementComboBox.SelectedItem = value?.ToString();
            }
        }

        public Direction? Direction
        {
            get
            {
                var value = this.directionComboBox.SelectedItem as string;
                return (Direction)Enum.Parse(typeof(Direction), value);
            }
            set
            {
                this.directionComboBox.SelectedItem = value?.ToString();
            }
        }

        public double GetSpeed()
        {
            return double.Parse(this.speedComboBox.SelectedItem.ToString());
        }

        public bool TryGetMin(out int min)
        {
            return int.TryParse(this.minTextBox.Text, out min);
        }

        public bool TryGetMax(out int max)
        {
            return int.TryParse(this.maxTextBox.Text, out max);
        }

        public void SetSpeed(double speed)
        {
            this.speedComboBox.SelectedItem = speed.ToString();
        }

        public void SetMin(int min)
        {
            this.minTilesTextBox.Text = $"{min / Constants.BackgroundTileWidth}";
            this.minOffsetTextBox.Text = $"{min % Constants.BackgroundTileWidth}";
        }

        public void SetMax(int max)
        {
            this.maxTilesTextBox.Text = $"{max / Constants.BackgroundTileHeight}";
            this.maxOffsetTextBox.Text = $"{max % Constants.BackgroundTileHeight}";
        }        

        public void SetTypes(params MovementType[] types)
        {
            this.movementComboBox.Items.Clear();
            this.movementComboBox.Items.AddRange(types.Select(t => t.ToString()).ToArray());
        }

        public void SetDefaultValues()
        {
            this.MovementType = Contract.MovementType.None;
            this.SpecialMovement = Contract.SpecialMovement.None;
            this.SetSpeed(0);
            this.SetMin(0);
            this.SetMax(0);
        }

        public bool AllowSpecialMovement { get; set; }

        private void MovementComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            var setToNone = this.MovementType == Contract.MovementType.None;

            this.speedComboBox.Enabled = !setToNone;
            this.minTilesTextBox.Enabled = !setToNone;
            this.minOffsetTextBox.Enabled = !setToNone;
            this.maxTilesTextBox.Enabled = !setToNone;
            this.maxOffsetTextBox.Enabled = !setToNone;
            this.minTextBox.Enabled = !setToNone;
            this.maxTextBox.Enabled = !setToNone;
            this.specialMovementComboBox.Enabled = !setToNone && AllowSpecialMovement;

            if (setToNone)
            {
                this.SetDefaultValues();
            }

            this.directionComboBox.Items.Clear();
            switch (this.MovementType)
            {
                case Contract.MovementType.None:
                    this.directionComboBox.Items.Add(Contract.Direction.None.ToString());
                    break;
                case Contract.MovementType.Horizontal:
                    this.directionComboBox.Items.Add(Contract.Direction.Left.ToString());
                    this.directionComboBox.Items.Add(Contract.Direction.Right.ToString());
                    break;
                case Contract.MovementType.Vertical:
                    this.directionComboBox.Items.Add(Contract.Direction.Up.ToString());
                    this.directionComboBox.Items.Add(Contract.Direction.Down.ToString());
                    break;
            }

            this.directionComboBox.SelectedIndex = 0;
        }
    }
}
