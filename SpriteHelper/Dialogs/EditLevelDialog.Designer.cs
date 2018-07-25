namespace SpriteHelper.Dialogs
{
    partial class EditLevelDialog
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
            this.widthLabel = new System.Windows.Forms.Label();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.playerXLabel = new System.Windows.Forms.Label();
            this.playerXTextBox = new System.Windows.Forms.TextBox();
            this.playerYTextBox = new System.Windows.Forms.TextBox();
            this.playerYLabel = new System.Windows.Forms.Label();
            this.exitYLabel = new System.Windows.Forms.Label();
            this.exitYTextBox = new System.Windows.Forms.TextBox();
            this.exitXLabel = new System.Windows.Forms.Label();
            this.exitXTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(12, 9);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(67, 13);
            this.widthLabel.TabIndex = 39;
            this.widthLabel.Text = "Width in tiles";
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(88, 6);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(48, 20);
            this.widthTextBox.TabIndex = 38;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(153, 102);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 23);
            this.okButton.TabIndex = 40;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(219, 102);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 23);
            this.cancelButton.TabIndex = 41;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // playerXLabel
            // 
            this.playerXLabel.AutoSize = true;
            this.playerXLabel.Location = new System.Drawing.Point(12, 35);
            this.playerXLabel.Name = "playerXLabel";
            this.playerXLabel.Size = new System.Drawing.Size(46, 13);
            this.playerXLabel.TabIndex = 43;
            this.playerXLabel.Text = "Player X";
            // 
            // playerXTextBox
            // 
            this.playerXTextBox.Location = new System.Drawing.Point(88, 32);
            this.playerXTextBox.Name = "playerXTextBox";
            this.playerXTextBox.Size = new System.Drawing.Size(48, 20);
            this.playerXTextBox.TabIndex = 42;
            // 
            // playerYTextBox
            // 
            this.playerYTextBox.Location = new System.Drawing.Point(226, 32);
            this.playerYTextBox.Name = "playerYTextBox";
            this.playerYTextBox.Size = new System.Drawing.Size(48, 20);
            this.playerYTextBox.TabIndex = 44;
            // 
            // playerYLabel
            // 
            this.playerYLabel.AutoSize = true;
            this.playerYLabel.Location = new System.Drawing.Point(150, 35);
            this.playerYLabel.Name = "playerYLabel";
            this.playerYLabel.Size = new System.Drawing.Size(46, 13);
            this.playerYLabel.TabIndex = 45;
            this.playerYLabel.Text = "Player Y";
            // 
            // exitYLabel
            // 
            this.exitYLabel.AutoSize = true;
            this.exitYLabel.Location = new System.Drawing.Point(150, 61);
            this.exitYLabel.Name = "exitYLabel";
            this.exitYLabel.Size = new System.Drawing.Size(50, 13);
            this.exitYLabel.TabIndex = 49;
            this.exitYLabel.Text = "Exit tile Y";
            // 
            // exitYTextBox
            // 
            this.exitYTextBox.Location = new System.Drawing.Point(226, 58);
            this.exitYTextBox.Name = "exitYTextBox";
            this.exitYTextBox.Size = new System.Drawing.Size(48, 20);
            this.exitYTextBox.TabIndex = 48;
            // 
            // exitXLabel
            // 
            this.exitXLabel.AutoSize = true;
            this.exitXLabel.Location = new System.Drawing.Point(12, 61);
            this.exitXLabel.Name = "exitXLabel";
            this.exitXLabel.Size = new System.Drawing.Size(50, 13);
            this.exitXLabel.TabIndex = 47;
            this.exitXLabel.Text = "Exit tile X";
            // 
            // exitXTextBox
            // 
            this.exitXTextBox.Location = new System.Drawing.Point(88, 58);
            this.exitXTextBox.Name = "exitXTextBox";
            this.exitXTextBox.Size = new System.Drawing.Size(48, 20);
            this.exitXTextBox.TabIndex = 46;
            // 
            // EditLevelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 135);
            this.Controls.Add(this.exitYLabel);
            this.Controls.Add(this.exitYTextBox);
            this.Controls.Add(this.exitXLabel);
            this.Controls.Add(this.exitXTextBox);
            this.Controls.Add(this.playerYLabel);
            this.Controls.Add(this.playerYTextBox);
            this.Controls.Add(this.playerXLabel);
            this.Controls.Add(this.playerXTextBox);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.widthTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditLevelDialog";
            this.Text = "Edit Level";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label playerXLabel;
        private System.Windows.Forms.TextBox playerXTextBox;
        private System.Windows.Forms.TextBox playerYTextBox;
        private System.Windows.Forms.Label playerYLabel;
        private System.Windows.Forms.Label exitYLabel;
        private System.Windows.Forms.TextBox exitYTextBox;
        private System.Windows.Forms.Label exitXLabel;
        private System.Windows.Forms.TextBox exitXTextBox;
    }
}