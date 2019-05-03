namespace SpriteHelper.Dialogs
{
    partial class AddEditElevatorDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.movementGroupBox = new System.Windows.Forms.GroupBox();
            this.movementPanel = new SpriteHelper.Controls.MovementPanel();
            this.positionGroupBox = new System.Windows.Forms.GroupBox();
            this.positionPanel = new SpriteHelper.Controls.PositionPanel();
            this.sizeGroupBox = new System.Windows.Forms.GroupBox();
            this.sizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.movementGroupBox.SuspendLayout();
            this.positionGroupBox.SuspendLayout();
            this.sizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // movementGroupBox
            // 
            this.movementGroupBox.Controls.Add(this.movementPanel);
            this.movementGroupBox.Location = new System.Drawing.Point(12, 197);
            this.movementGroupBox.Name = "movementGroupBox";
            this.movementGroupBox.Size = new System.Drawing.Size(441, 170);
            this.movementGroupBox.TabIndex = 49;
            this.movementGroupBox.TabStop = false;
            this.movementGroupBox.Text = "Movement";
            // 
            // movementPanel
            // 
            this.movementPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movementPanel.Location = new System.Drawing.Point(3, 16);
            this.movementPanel.Name = "movementPanel";
            this.movementPanel.Size = new System.Drawing.Size(435, 151);
            this.movementPanel.TabIndex = 0;
            // 
            // positionGroupBox
            // 
            this.positionGroupBox.Controls.Add(this.positionPanel);
            this.positionGroupBox.Location = new System.Drawing.Point(12, 91);
            this.positionGroupBox.Name = "positionGroupBox";
            this.positionGroupBox.Size = new System.Drawing.Size(441, 102);
            this.positionGroupBox.TabIndex = 48;
            this.positionGroupBox.TabStop = false;
            this.positionGroupBox.Text = "Position";
            // 
            // positionPanel
            // 
            this.positionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionPanel.Location = new System.Drawing.Point(3, 16);
            this.positionPanel.Name = "positionPanel";
            this.positionPanel.Size = new System.Drawing.Size(435, 83);
            this.positionPanel.TabIndex = 0;
            // 
            // sizeGroupBox
            // 
            this.sizeGroupBox.Controls.Add(this.sizeNumericUpDown);
            this.sizeGroupBox.Controls.Add(this.sizeLabel);
            this.sizeGroupBox.Location = new System.Drawing.Point(12, 12);
            this.sizeGroupBox.Name = "sizeGroupBox";
            this.sizeGroupBox.Size = new System.Drawing.Size(248, 73);
            this.sizeGroupBox.TabIndex = 50;
            this.sizeGroupBox.TabStop = false;
            this.sizeGroupBox.Text = "Size";
            // 
            // sizeNumericUpDown
            // 
            this.sizeNumericUpDown.Location = new System.Drawing.Point(105, 33);
            this.sizeNumericUpDown.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.sizeNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.sizeNumericUpDown.Name = "sizeNumericUpDown";
            this.sizeNumericUpDown.Size = new System.Drawing.Size(102, 20);
            this.sizeNumericUpDown.TabIndex = 1;
            this.sizeNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(58, 35);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(27, 13);
            this.sizeLabel.TabIndex = 0;
            this.sizeLabel.Text = "Size";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(364, 42);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 23);
            this.cancelButton.TabIndex = 52;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(303, 42);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 23);
            this.okButton.TabIndex = 51;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // AddEditElevatorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 385);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.sizeGroupBox);
            this.Controls.Add(this.movementGroupBox);
            this.Controls.Add(this.positionGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddEditElevatorDialog";
            this.Text = "placeholder";
            this.movementGroupBox.ResumeLayout(false);
            this.positionGroupBox.ResumeLayout(false);
            this.sizeGroupBox.ResumeLayout(false);
            this.sizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox movementGroupBox;
        private Controls.MovementPanel movementPanel;
        private System.Windows.Forms.GroupBox positionGroupBox;
        private Controls.PositionPanel positionPanel;
        private System.Windows.Forms.GroupBox sizeGroupBox;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.NumericUpDown sizeNumericUpDown;
    }
}