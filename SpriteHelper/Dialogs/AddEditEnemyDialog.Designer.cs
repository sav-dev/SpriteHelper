namespace SpriteHelper.Dialogs
{
    partial class AddEditEnemyDialog
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.enemyComboBox = new System.Windows.Forms.ComboBox();
            this.enemyPictureBox = new System.Windows.Forms.PictureBox();
            this.initialFlipCheckBox = new System.Windows.Forms.CheckBox();
            this.initialFlipLabel = new System.Windows.Forms.Label();
            this.positionGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.enemyPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(510, 209);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 23);
            this.cancelButton.TabIndex = 43;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(449, 209);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 23);
            this.okButton.TabIndex = 42;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // enemyComboBox
            // 
            this.enemyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enemyComboBox.FormattingEnabled = true;
            this.enemyComboBox.Location = new System.Drawing.Point(421, 12);
            this.enemyComboBox.Name = "enemyComboBox";
            this.enemyComboBox.Size = new System.Drawing.Size(144, 21);
            this.enemyComboBox.TabIndex = 44;
            this.enemyComboBox.SelectedIndexChanged += new System.EventHandler(this.EnemyComboBoxSelectedIndexChanged);
            // 
            // enemyPictureBox
            // 
            this.enemyPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.enemyPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.enemyPictureBox.Location = new System.Drawing.Point(421, 39);
            this.enemyPictureBox.Name = "enemyPictureBox";
            this.enemyPictureBox.Size = new System.Drawing.Size(144, 116);
            this.enemyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.enemyPictureBox.TabIndex = 45;
            this.enemyPictureBox.TabStop = false;
            // 
            // initialFlipCheckBox
            // 
            this.initialFlipCheckBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.initialFlipCheckBox.AutoSize = true;
            this.initialFlipCheckBox.Location = new System.Drawing.Point(136, 212);
            this.initialFlipCheckBox.Name = "initialFlipCheckBox";
            this.initialFlipCheckBox.Size = new System.Drawing.Size(15, 14);
            this.initialFlipCheckBox.TabIndex = 5;
            this.initialFlipCheckBox.UseVisualStyleBackColor = true;
            // 
            // initialFlipLabel
            // 
            this.initialFlipLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.initialFlipLabel.AutoSize = true;
            this.initialFlipLabel.Location = new System.Drawing.Point(59, 213);
            this.initialFlipLabel.Name = "initialFlipLabel";
            this.initialFlipLabel.Size = new System.Drawing.Size(47, 13);
            this.initialFlipLabel.TabIndex = 6;
            this.initialFlipLabel.Text = "Initial flip";
            // 
            // positionGroupBox
            // 
            this.positionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.positionGroupBox.Name = "positionGroupBox";
            this.positionGroupBox.Size = new System.Drawing.Size(403, 100);
            this.positionGroupBox.TabIndex = 46;
            this.positionGroupBox.TabStop = false;
            this.positionGroupBox.Text = "Position";
            // 
            // AddEditEnemyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 241);
            this.Controls.Add(this.positionGroupBox);
            this.Controls.Add(this.initialFlipLabel);
            this.Controls.Add(this.initialFlipCheckBox);
            this.Controls.Add(this.enemyPictureBox);
            this.Controls.Add(this.enemyComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddEditEnemyDialog";
            this.Text = "placeholder";
            ((System.ComponentModel.ISupportInitialize)(this.enemyPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox enemyComboBox;
        private System.Windows.Forms.PictureBox enemyPictureBox;
        private System.Windows.Forms.CheckBox initialFlipCheckBox;
        private System.Windows.Forms.Label initialFlipLabel;
        private System.Windows.Forms.GroupBox positionGroupBox;
    }
}