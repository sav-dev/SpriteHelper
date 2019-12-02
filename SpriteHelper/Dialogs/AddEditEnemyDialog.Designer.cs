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
            this.positionGroupBox = new System.Windows.Forms.GroupBox();
            this.movementGroupBox = new System.Windows.Forms.GroupBox();
            this.shootingGroupBox = new System.Windows.Forms.GroupBox();
            this.flipGroupBox = new System.Windows.Forms.GroupBox();
            this.flipPanel = new SpriteHelper.Controls.FlipPanel();
            this.shootingPanel = new SpriteHelper.Controls.ShootingPanel();
            this.movementPanel = new SpriteHelper.Controls.MovementPanel();
            this.positionPanel = new SpriteHelper.Controls.PositionPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.blinkingPanel = new SpriteHelper.Controls.BlinkingPanel();
            ((System.ComponentModel.ISupportInitialize)(this.enemyPictureBox)).BeginInit();
            this.positionGroupBox.SuspendLayout();
            this.movementGroupBox.SuspendLayout();
            this.shootingGroupBox.SuspendLayout();
            this.flipGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(548, 161);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 23);
            this.cancelButton.TabIndex = 43;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(487, 161);
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
            this.enemyComboBox.Location = new System.Drawing.Point(459, 12);
            this.enemyComboBox.Name = "enemyComboBox";
            this.enemyComboBox.Size = new System.Drawing.Size(144, 21);
            this.enemyComboBox.TabIndex = 44;
            this.enemyComboBox.SelectedIndexChanged += new System.EventHandler(this.EnemyComboBoxSelectedIndexChanged);
            // 
            // enemyPictureBox
            // 
            this.enemyPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.enemyPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.enemyPictureBox.Location = new System.Drawing.Point(459, 39);
            this.enemyPictureBox.Name = "enemyPictureBox";
            this.enemyPictureBox.Size = new System.Drawing.Size(144, 116);
            this.enemyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.enemyPictureBox.TabIndex = 45;
            this.enemyPictureBox.TabStop = false;
            // 
            // positionGroupBox
            // 
            this.positionGroupBox.Controls.Add(this.positionPanel);
            this.positionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.positionGroupBox.Name = "positionGroupBox";
            this.positionGroupBox.Size = new System.Drawing.Size(441, 102);
            this.positionGroupBox.TabIndex = 46;
            this.positionGroupBox.TabStop = false;
            this.positionGroupBox.Text = "Position";
            // 
            // movementGroupBox
            // 
            this.movementGroupBox.Controls.Add(this.movementPanel);
            this.movementGroupBox.Location = new System.Drawing.Point(12, 117);
            this.movementGroupBox.Name = "movementGroupBox";
            this.movementGroupBox.Size = new System.Drawing.Size(441, 170);
            this.movementGroupBox.TabIndex = 47;
            this.movementGroupBox.TabStop = false;
            this.movementGroupBox.Text = "Movement";
            // 
            // shootingGroupBox
            // 
            this.shootingGroupBox.Controls.Add(this.shootingPanel);
            this.shootingGroupBox.Location = new System.Drawing.Point(15, 293);
            this.shootingGroupBox.Name = "shootingGroupBox";
            this.shootingGroupBox.Size = new System.Drawing.Size(441, 73);
            this.shootingGroupBox.TabIndex = 47;
            this.shootingGroupBox.TabStop = false;
            this.shootingGroupBox.Text = "Shooting";
            // 
            // flipGroupBox
            // 
            this.flipGroupBox.Controls.Add(this.flipPanel);
            this.flipGroupBox.Location = new System.Drawing.Point(18, 372);
            this.flipGroupBox.Name = "flipGroupBox";
            this.flipGroupBox.Size = new System.Drawing.Size(435, 54);
            this.flipGroupBox.TabIndex = 49;
            this.flipGroupBox.TabStop = false;
            this.flipGroupBox.Text = "Flip";
            // 
            // flipPanel
            // 
            this.flipPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flipPanel.InitialFlip = false;
            this.flipPanel.Location = new System.Drawing.Point(3, 16);
            this.flipPanel.Name = "flipPanel";
            this.flipPanel.ShouldFlip = false;
            this.flipPanel.Size = new System.Drawing.Size(429, 35);
            this.flipPanel.TabIndex = 48;
            // 
            // shootingPanel
            // 
            this.shootingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shootingPanel.Location = new System.Drawing.Point(3, 16);
            this.shootingPanel.Name = "shootingPanel";
            this.shootingPanel.Size = new System.Drawing.Size(435, 54);
            this.shootingPanel.TabIndex = 0;
            // 
            // movementPanel
            // 
            this.movementPanel.AllowSpecialMovement = true;
            this.movementPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movementPanel.Location = new System.Drawing.Point(3, 16);
            this.movementPanel.Name = "movementPanel";
            this.movementPanel.Size = new System.Drawing.Size(435, 151);
            this.movementPanel.TabIndex = 0;
            // 
            // positionPanel
            // 
            this.positionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionPanel.EnableFinePosition = true;
            this.positionPanel.Location = new System.Drawing.Point(3, 16);
            this.positionPanel.Name = "positionPanel";
            this.positionPanel.Size = new System.Drawing.Size(435, 83);
            this.positionPanel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.blinkingPanel);
            this.groupBox1.Location = new System.Drawing.Point(21, 432);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 77);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Blinking";
            // 
            // blinkingPanel
            // 
            this.blinkingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blinkingPanel.Location = new System.Drawing.Point(3, 16);
            this.blinkingPanel.Name = "blinkingPanel";
            this.blinkingPanel.Size = new System.Drawing.Size(423, 58);
            this.blinkingPanel.TabIndex = 0;
            // 
            // AddEditEnemyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 522);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.flipGroupBox);
            this.Controls.Add(this.shootingGroupBox);
            this.Controls.Add(this.movementGroupBox);
            this.Controls.Add(this.positionGroupBox);
            this.Controls.Add(this.enemyPictureBox);
            this.Controls.Add(this.enemyComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddEditEnemyDialog";
            this.Text = "placeholder";
            ((System.ComponentModel.ISupportInitialize)(this.enemyPictureBox)).EndInit();
            this.positionGroupBox.ResumeLayout(false);
            this.movementGroupBox.ResumeLayout(false);
            this.shootingGroupBox.ResumeLayout(false);
            this.flipGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox enemyComboBox;
        private System.Windows.Forms.PictureBox enemyPictureBox;
        private System.Windows.Forms.GroupBox positionGroupBox;
        private System.Windows.Forms.GroupBox movementGroupBox;
        private Controls.PositionPanel positionPanel;
        private Controls.MovementPanel movementPanel;
        private System.Windows.Forms.GroupBox shootingGroupBox;
        private Controls.ShootingPanel shootingPanel;
        private Controls.FlipPanel flipPanel;
        private System.Windows.Forms.GroupBox flipGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.BlinkingPanel blinkingPanel;
    }
}