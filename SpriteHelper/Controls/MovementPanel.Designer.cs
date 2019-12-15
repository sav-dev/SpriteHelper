namespace SpriteHelper.Controls
{
    partial class MovementPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertiesLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.movementTypeLabel = new System.Windows.Forms.Label();
            this.xLabel = new System.Windows.Forms.Label();
            this.minTextBox = new System.Windows.Forms.TextBox();
            this.yLabel = new System.Windows.Forms.Label();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.xOffsetLabel = new System.Windows.Forms.Label();
            this.minOffsetTextBox = new System.Windows.Forms.TextBox();
            this.yOffsetLabel = new System.Windows.Forms.Label();
            this.maxOffsetTextBox = new System.Windows.Forms.TextBox();
            this.xTilesLabel = new System.Windows.Forms.Label();
            this.minTilesTextBox = new System.Windows.Forms.TextBox();
            this.yTilesLabel = new System.Windows.Forms.Label();
            this.maxTilesTextBox = new System.Windows.Forms.TextBox();
            this.movementComboBox = new System.Windows.Forms.ComboBox();
            this.movementSpeedLabel = new System.Windows.Forms.Label();
            this.directionLabel = new System.Windows.Forms.Label();
            this.directionComboBox = new System.Windows.Forms.ComboBox();
            this.specialMovementLabel = new System.Windows.Forms.Label();
            this.specialMovementComboBox = new System.Windows.Forms.ComboBox();
            this.speedComboBox = new System.Windows.Forms.ComboBox();
            this.propertiesLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertiesLayoutPanel
            // 
            this.propertiesLayoutPanel.ColumnCount = 4;
            this.propertiesLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.propertiesLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.propertiesLayoutPanel.Controls.Add(this.movementTypeLabel, 0, 0);
            this.propertiesLayoutPanel.Controls.Add(this.xLabel, 0, 4);
            this.propertiesLayoutPanel.Controls.Add(this.minTextBox, 1, 4);
            this.propertiesLayoutPanel.Controls.Add(this.yLabel, 2, 4);
            this.propertiesLayoutPanel.Controls.Add(this.maxTextBox, 3, 4);
            this.propertiesLayoutPanel.Controls.Add(this.xOffsetLabel, 0, 3);
            this.propertiesLayoutPanel.Controls.Add(this.minOffsetTextBox, 1, 3);
            this.propertiesLayoutPanel.Controls.Add(this.yOffsetLabel, 2, 3);
            this.propertiesLayoutPanel.Controls.Add(this.maxOffsetTextBox, 3, 3);
            this.propertiesLayoutPanel.Controls.Add(this.xTilesLabel, 0, 2);
            this.propertiesLayoutPanel.Controls.Add(this.minTilesTextBox, 1, 2);
            this.propertiesLayoutPanel.Controls.Add(this.yTilesLabel, 2, 2);
            this.propertiesLayoutPanel.Controls.Add(this.maxTilesTextBox, 3, 2);
            this.propertiesLayoutPanel.Controls.Add(this.movementComboBox, 1, 0);
            this.propertiesLayoutPanel.Controls.Add(this.movementSpeedLabel, 2, 1);
            this.propertiesLayoutPanel.Controls.Add(this.directionLabel, 0, 1);
            this.propertiesLayoutPanel.Controls.Add(this.directionComboBox, 1, 1);
            this.propertiesLayoutPanel.Controls.Add(this.specialMovementLabel, 2, 0);
            this.propertiesLayoutPanel.Controls.Add(this.specialMovementComboBox, 3, 0);
            this.propertiesLayoutPanel.Controls.Add(this.speedComboBox, 3, 1);
            this.propertiesLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.propertiesLayoutPanel.Name = "propertiesLayoutPanel";
            this.propertiesLayoutPanel.RowCount = 5;
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.propertiesLayoutPanel.Size = new System.Drawing.Size(487, 196);
            this.propertiesLayoutPanel.TabIndex = 1;
            // 
            // movementTypeLabel
            // 
            this.movementTypeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.movementTypeLabel.AutoSize = true;
            this.movementTypeLabel.Location = new System.Drawing.Point(37, 13);
            this.movementTypeLabel.Name = "movementTypeLabel";
            this.movementTypeLabel.Size = new System.Drawing.Size(57, 13);
            this.movementTypeLabel.TabIndex = 15;
            this.movementTypeLabel.Text = "Movement";
            // 
            // xLabel
            // 
            this.xLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(70, 169);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(24, 13);
            this.xLabel.TabIndex = 14;
            this.xLabel.Text = "Min";
            // 
            // minTextBox
            // 
            this.minTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minTextBox.Location = new System.Drawing.Point(120, 166);
            this.minTextBox.Name = "minTextBox";
            this.minTextBox.ReadOnly = true;
            this.minTextBox.Size = new System.Drawing.Size(100, 20);
            this.minTextBox.TabIndex = 4;
            this.minTextBox.Text = "0";
            // 
            // yLabel
            // 
            this.yLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(310, 169);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(27, 13);
            this.yLabel.TabIndex = 13;
            this.yLabel.Text = "Max";
            // 
            // maxTextBox
            // 
            this.maxTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxTextBox.Location = new System.Drawing.Point(363, 166);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.ReadOnly = true;
            this.maxTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxTextBox.TabIndex = 5;
            this.maxTextBox.Text = "0";
            // 
            // xOffsetLabel
            // 
            this.xOffsetLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xOffsetLabel.AutoSize = true;
            this.xOffsetLabel.Location = new System.Drawing.Point(35, 130);
            this.xOffsetLabel.Name = "xOffsetLabel";
            this.xOffsetLabel.Size = new System.Drawing.Size(59, 13);
            this.xOffsetLabel.TabIndex = 11;
            this.xOffsetLabel.Text = "Min (offset)";
            // 
            // minOffsetTextBox
            // 
            this.minOffsetTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minOffsetTextBox.Location = new System.Drawing.Point(120, 126);
            this.minOffsetTextBox.Name = "minOffsetTextBox";
            this.minOffsetTextBox.Size = new System.Drawing.Size(100, 20);
            this.minOffsetTextBox.TabIndex = 2;
            this.minOffsetTextBox.Text = "0";
            this.minOffsetTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // yOffsetLabel
            // 
            this.yOffsetLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yOffsetLabel.AutoSize = true;
            this.yOffsetLabel.Location = new System.Drawing.Point(275, 130);
            this.yOffsetLabel.Name = "yOffsetLabel";
            this.yOffsetLabel.Size = new System.Drawing.Size(62, 13);
            this.yOffsetLabel.TabIndex = 12;
            this.yOffsetLabel.Text = "Max (offset)";
            // 
            // maxOffsetTextBox
            // 
            this.maxOffsetTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxOffsetTextBox.Location = new System.Drawing.Point(363, 126);
            this.maxOffsetTextBox.Name = "maxOffsetTextBox";
            this.maxOffsetTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxOffsetTextBox.TabIndex = 3;
            this.maxOffsetTextBox.Text = "0";
            this.maxOffsetTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // xTilesLabel
            // 
            this.xTilesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xTilesLabel.AutoSize = true;
            this.xTilesLabel.Location = new System.Drawing.Point(17, 91);
            this.xTilesLabel.Name = "xTilesLabel";
            this.xTilesLabel.Size = new System.Drawing.Size(77, 13);
            this.xTilesLabel.TabIndex = 8;
            this.xTilesLabel.Text = "Min (in bg tiles)";
            // 
            // minTilesTextBox
            // 
            this.minTilesTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minTilesTextBox.Location = new System.Drawing.Point(120, 87);
            this.minTilesTextBox.Name = "minTilesTextBox";
            this.minTilesTextBox.Size = new System.Drawing.Size(100, 20);
            this.minTilesTextBox.TabIndex = 0;
            this.minTilesTextBox.Text = "0";
            this.minTilesTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // yTilesLabel
            // 
            this.yTilesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yTilesLabel.AutoSize = true;
            this.yTilesLabel.Location = new System.Drawing.Point(257, 91);
            this.yTilesLabel.Name = "yTilesLabel";
            this.yTilesLabel.Size = new System.Drawing.Size(80, 13);
            this.yTilesLabel.TabIndex = 8;
            this.yTilesLabel.Text = "Max (in bg tiles)";
            // 
            // maxTilesTextBox
            // 
            this.maxTilesTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxTilesTextBox.Location = new System.Drawing.Point(363, 87);
            this.maxTilesTextBox.Name = "maxTilesTextBox";
            this.maxTilesTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxTilesTextBox.TabIndex = 1;
            this.maxTilesTextBox.Text = "0";
            this.maxTilesTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // movementComboBox
            // 
            this.movementComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.movementComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.movementComboBox.FormattingEnabled = true;
            this.movementComboBox.Location = new System.Drawing.Point(109, 9);
            this.movementComboBox.Name = "movementComboBox";
            this.movementComboBox.Size = new System.Drawing.Size(121, 21);
            this.movementComboBox.TabIndex = 20;
            this.movementComboBox.SelectedIndexChanged += new System.EventHandler(this.MovementComboBoxSelectedIndexChanged);
            // 
            // movementSpeedLabel
            // 
            this.movementSpeedLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.movementSpeedLabel.AutoSize = true;
            this.movementSpeedLabel.Location = new System.Drawing.Point(299, 52);
            this.movementSpeedLabel.Name = "movementSpeedLabel";
            this.movementSpeedLabel.Size = new System.Drawing.Size(38, 13);
            this.movementSpeedLabel.TabIndex = 16;
            this.movementSpeedLabel.Text = "Speed";
            // 
            // directionLabel
            // 
            this.directionLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.directionLabel.AutoSize = true;
            this.directionLabel.Location = new System.Drawing.Point(18, 52);
            this.directionLabel.Name = "directionLabel";
            this.directionLabel.Size = new System.Drawing.Size(76, 13);
            this.directionLabel.TabIndex = 21;
            this.directionLabel.Text = "Initial Direciton";
            // 
            // directionComboBox
            // 
            this.directionComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.directionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.directionComboBox.FormattingEnabled = true;
            this.directionComboBox.Location = new System.Drawing.Point(109, 48);
            this.directionComboBox.Name = "directionComboBox";
            this.directionComboBox.Size = new System.Drawing.Size(121, 21);
            this.directionComboBox.TabIndex = 22;
            // 
            // specialMovementLabel
            // 
            this.specialMovementLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.specialMovementLabel.AutoSize = true;
            this.specialMovementLabel.Location = new System.Drawing.Point(280, 6);
            this.specialMovementLabel.Name = "specialMovementLabel";
            this.specialMovementLabel.Size = new System.Drawing.Size(57, 26);
            this.specialMovementLabel.TabIndex = 23;
            this.specialMovementLabel.Text = "Special Movement";
            // 
            // specialMovementComboBox
            // 
            this.specialMovementComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.specialMovementComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.specialMovementComboBox.FormattingEnabled = true;
            this.specialMovementComboBox.Location = new System.Drawing.Point(353, 9);
            this.specialMovementComboBox.Name = "specialMovementComboBox";
            this.specialMovementComboBox.Size = new System.Drawing.Size(121, 21);
            this.specialMovementComboBox.TabIndex = 24;
            // 
            // speedComboBox
            // 
            this.speedComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.speedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speedComboBox.FormattingEnabled = true;
            this.speedComboBox.Items.AddRange(new object[] {
            "0.25",
            "0.5",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.speedComboBox.Location = new System.Drawing.Point(353, 48);
            this.speedComboBox.Name = "speedComboBox";
            this.speedComboBox.Size = new System.Drawing.Size(121, 21);
            this.speedComboBox.TabIndex = 25;
            // 
            // MovementPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertiesLayoutPanel);
            this.Name = "MovementPanel";
            this.Size = new System.Drawing.Size(487, 196);
            this.propertiesLayoutPanel.ResumeLayout(false);
            this.propertiesLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel propertiesLayoutPanel;
        private System.Windows.Forms.TextBox minTextBox;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label xTilesLabel;
        private System.Windows.Forms.Label xOffsetLabel;
        private System.Windows.Forms.TextBox minTilesTextBox;
        private System.Windows.Forms.TextBox minOffsetTextBox;
        private System.Windows.Forms.TextBox maxTilesTextBox;
        private System.Windows.Forms.TextBox maxOffsetTextBox;
        private System.Windows.Forms.Label yTilesLabel;
        private System.Windows.Forms.Label yOffsetLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.Label movementTypeLabel;
        private System.Windows.Forms.Label movementSpeedLabel;
        private System.Windows.Forms.ComboBox movementComboBox;
        private System.Windows.Forms.Label directionLabel;
        private System.Windows.Forms.ComboBox directionComboBox;
        private System.Windows.Forms.Label specialMovementLabel;
        private System.Windows.Forms.ComboBox specialMovementComboBox;
        private System.Windows.Forms.ComboBox speedComboBox;
    }
}
