namespace SpriteHelper
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
            this.editWidthRadio = new System.Windows.Forms.RadioButton();
            this.widthPanel = new System.Windows.Forms.Panel();
            this.widthPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(17, 18);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(67, 13);
            this.widthLabel.TabIndex = 39;
            this.widthLabel.Text = "Width in tiles";
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(93, 15);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(48, 20);
            this.widthTextBox.TabIndex = 38;
            this.widthTextBox.Text = "64";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(75, 174);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 23);
            this.okButton.TabIndex = 40;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(141, 174);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 23);
            this.cancelButton.TabIndex = 41;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // editWidthRadio
            // 
            this.editWidthRadio.AutoSize = true;
            this.editWidthRadio.Checked = true;
            this.editWidthRadio.Location = new System.Drawing.Point(13, 13);
            this.editWidthRadio.Name = "editWidthRadio";
            this.editWidthRadio.Size = new System.Drawing.Size(74, 17);
            this.editWidthRadio.TabIndex = 42;
            this.editWidthRadio.TabStop = true;
            this.editWidthRadio.Text = "Edit Width";
            this.editWidthRadio.UseVisualStyleBackColor = true;
            this.editWidthRadio.CheckedChanged += new System.EventHandler(this.RadioCheckedChanged);
            // 
            // widthPanel
            // 
            this.widthPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthPanel.Controls.Add(this.widthLabel);
            this.widthPanel.Controls.Add(this.widthTextBox);
            this.widthPanel.Location = new System.Drawing.Point(13, 36);
            this.widthPanel.Name = "widthPanel";
            this.widthPanel.Size = new System.Drawing.Size(254, 50);
            this.widthPanel.TabIndex = 43;
            // 
            // EditLevelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 226);
            this.Controls.Add(this.widthPanel);
            this.Controls.Add(this.editWidthRadio);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditLevelDialog";
            this.Text = "Edit Level";
            this.widthPanel.ResumeLayout(false);
            this.widthPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.RadioButton editWidthRadio;
        private System.Windows.Forms.Panel widthPanel;
    }
}