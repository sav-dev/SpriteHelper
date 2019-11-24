namespace SpriteHelper.Dialogs
{
    partial class EditDoorAndKeycardDialog
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
            this.bgPanel = new System.Windows.Forms.Panel();
            this.positionPanel = new System.Windows.Forms.Panel();
            this.innerPanel = new System.Windows.Forms.Panel();
            this.keycardPositionGroupBox = new System.Windows.Forms.GroupBox();
            this.doorPositionGroupBox = new System.Windows.Forms.GroupBox();
            this.generalGroupBox = new System.Windows.Forms.GroupBox();
            this.doorExistsCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.keycardPositionPanel = new SpriteHelper.Controls.PositionPanel();
            this.doorPositionPanel = new SpriteHelper.Controls.PositionPanel();
            this.bgPanel.SuspendLayout();
            this.positionPanel.SuspendLayout();
            this.innerPanel.SuspendLayout();
            this.keycardPositionGroupBox.SuspendLayout();
            this.doorPositionGroupBox.SuspendLayout();
            this.generalGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgPanel
            // 
            this.bgPanel.BackColor = System.Drawing.SystemColors.Control;
            this.bgPanel.Controls.Add(this.positionPanel);
            this.bgPanel.Controls.Add(this.generalGroupBox);
            this.bgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bgPanel.Location = new System.Drawing.Point(0, 0);
            this.bgPanel.Name = "bgPanel";
            this.bgPanel.Size = new System.Drawing.Size(415, 289);
            this.bgPanel.TabIndex = 2;
            // 
            // positionPanel
            // 
            this.positionPanel.Controls.Add(this.innerPanel);
            this.positionPanel.Controls.Add(this.doorPositionGroupBox);
            this.positionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionPanel.Location = new System.Drawing.Point(0, 47);
            this.positionPanel.Name = "positionPanel";
            this.positionPanel.Size = new System.Drawing.Size(415, 242);
            this.positionPanel.TabIndex = 4;
            // 
            // innerPanel
            // 
            this.innerPanel.Controls.Add(this.cancelButton);
            this.innerPanel.Controls.Add(this.okButton);
            this.innerPanel.Controls.Add(this.keycardPositionGroupBox);
            this.innerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerPanel.Location = new System.Drawing.Point(0, 100);
            this.innerPanel.Name = "innerPanel";
            this.innerPanel.Size = new System.Drawing.Size(415, 142);
            this.innerPanel.TabIndex = 3;
            // 
            // keycardPositionGroupBox
            // 
            this.keycardPositionGroupBox.Controls.Add(this.keycardPositionPanel);
            this.keycardPositionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.keycardPositionGroupBox.Location = new System.Drawing.Point(0, 0);
            this.keycardPositionGroupBox.Name = "keycardPositionGroupBox";
            this.keycardPositionGroupBox.Size = new System.Drawing.Size(415, 100);
            this.keycardPositionGroupBox.TabIndex = 2;
            this.keycardPositionGroupBox.TabStop = false;
            this.keycardPositionGroupBox.Text = "Keycard position";
            // 
            // doorPositionGroupBox
            // 
            this.doorPositionGroupBox.Controls.Add(this.doorPositionPanel);
            this.doorPositionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.doorPositionGroupBox.Location = new System.Drawing.Point(0, 0);
            this.doorPositionGroupBox.Name = "doorPositionGroupBox";
            this.doorPositionGroupBox.Size = new System.Drawing.Size(415, 100);
            this.doorPositionGroupBox.TabIndex = 1;
            this.doorPositionGroupBox.TabStop = false;
            this.doorPositionGroupBox.Text = "Door position";
            // 
            // generalGroupBox
            // 
            this.generalGroupBox.Controls.Add(this.doorExistsCheckBox);
            this.generalGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.generalGroupBox.Location = new System.Drawing.Point(0, 0);
            this.generalGroupBox.Name = "generalGroupBox";
            this.generalGroupBox.Size = new System.Drawing.Size(415, 47);
            this.generalGroupBox.TabIndex = 3;
            this.generalGroupBox.TabStop = false;
            this.generalGroupBox.Text = "General";
            // 
            // doorExistsCheckBox
            // 
            this.doorExistsCheckBox.AutoSize = true;
            this.doorExistsCheckBox.Location = new System.Drawing.Point(12, 19);
            this.doorExistsCheckBox.Name = "doorExistsCheckBox";
            this.doorExistsCheckBox.Size = new System.Drawing.Size(81, 17);
            this.doorExistsCheckBox.TabIndex = 0;
            this.doorExistsCheckBox.Text = "Create door";
            this.doorExistsCheckBox.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(348, 106);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 23);
            this.cancelButton.TabIndex = 45;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(287, 106);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 23);
            this.okButton.TabIndex = 44;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // keycardPositionPanel
            // 
            this.keycardPositionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keycardPositionPanel.EnableFinePosition = true;
            this.keycardPositionPanel.Location = new System.Drawing.Point(3, 16);
            this.keycardPositionPanel.Name = "keycardPositionPanel";
            this.keycardPositionPanel.Size = new System.Drawing.Size(409, 81);
            this.keycardPositionPanel.TabIndex = 0;
            // 
            // doorPositionPanel
            // 
            this.doorPositionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doorPositionPanel.EnableFinePosition = false;
            this.doorPositionPanel.Location = new System.Drawing.Point(3, 16);
            this.doorPositionPanel.Name = "doorPositionPanel";
            this.doorPositionPanel.Size = new System.Drawing.Size(409, 81);
            this.doorPositionPanel.TabIndex = 0;
            // 
            // EditDoorAndKeycardDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 289);
            this.Controls.Add(this.bgPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditDoorAndKeycardDialog";
            this.Text = "Edit door and keycard";
            this.bgPanel.ResumeLayout(false);
            this.positionPanel.ResumeLayout(false);
            this.innerPanel.ResumeLayout(false);
            this.keycardPositionGroupBox.ResumeLayout(false);
            this.doorPositionGroupBox.ResumeLayout(false);
            this.generalGroupBox.ResumeLayout(false);
            this.generalGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel bgPanel;
        private System.Windows.Forms.Panel positionPanel;
        private System.Windows.Forms.Panel innerPanel;
        private System.Windows.Forms.GroupBox keycardPositionGroupBox;
        private Controls.PositionPanel keycardPositionPanel;
        private System.Windows.Forms.GroupBox doorPositionGroupBox;
        private Controls.PositionPanel doorPositionPanel;
        private System.Windows.Forms.GroupBox generalGroupBox;
        private System.Windows.Forms.CheckBox doorExistsCheckBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
    }
}