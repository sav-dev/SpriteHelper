namespace SpriteHelper.Dialogs
{
    partial class TitleDialog
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
            this.fontTextBox = new System.Windows.Forms.TextBox();
            this.fontLabel = new System.Windows.Forms.Label();
            this.logoLabel = new System.Windows.Forms.Label();
            this.logoTextBox = new System.Windows.Forms.TextBox();
            this.chrOutputTextBox = new System.Windows.Forms.TextBox();
            this.chrOutputLabel = new System.Windows.Forms.Label();
            this.processButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fontTextBox
            // 
            this.fontTextBox.Location = new System.Drawing.Point(75, 29);
            this.fontTextBox.Name = "fontTextBox";
            this.fontTextBox.ReadOnly = true;
            this.fontTextBox.Size = new System.Drawing.Size(478, 20);
            this.fontTextBox.TabIndex = 54;
            // 
            // fontLabel
            // 
            this.fontLabel.AutoSize = true;
            this.fontLabel.Location = new System.Drawing.Point(7, 32);
            this.fontLabel.Name = "fontLabel";
            this.fontLabel.Size = new System.Drawing.Size(28, 13);
            this.fontLabel.TabIndex = 53;
            this.fontLabel.Text = "Font";
            // 
            // logoLabel
            // 
            this.logoLabel.AutoSize = true;
            this.logoLabel.Location = new System.Drawing.Point(7, 8);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Size = new System.Drawing.Size(31, 13);
            this.logoLabel.TabIndex = 50;
            this.logoLabel.Text = "Logo";
            // 
            // logoTextBox
            // 
            this.logoTextBox.Location = new System.Drawing.Point(75, 5);
            this.logoTextBox.Name = "logoTextBox";
            this.logoTextBox.ReadOnly = true;
            this.logoTextBox.Size = new System.Drawing.Size(478, 20);
            this.logoTextBox.TabIndex = 49;
            // 
            // chrOutputTextBox
            // 
            this.chrOutputTextBox.Location = new System.Drawing.Point(75, 53);
            this.chrOutputTextBox.Name = "chrOutputTextBox";
            this.chrOutputTextBox.ReadOnly = true;
            this.chrOutputTextBox.Size = new System.Drawing.Size(478, 20);
            this.chrOutputTextBox.TabIndex = 51;
            // 
            // chrOutputLabel
            // 
            this.chrOutputLabel.AutoSize = true;
            this.chrOutputLabel.Location = new System.Drawing.Point(7, 56);
            this.chrOutputLabel.Name = "chrOutputLabel";
            this.chrOutputLabel.Size = new System.Drawing.Size(63, 13);
            this.chrOutputLabel.TabIndex = 52;
            this.chrOutputLabel.Text = "CHR output";
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(478, 79);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(75, 23);
            this.processButton.TabIndex = 55;
            this.processButton.Text = "Process";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.ProcessButtonClick);
            // 
            // TitleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 111);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.fontTextBox);
            this.Controls.Add(this.fontLabel);
            this.Controls.Add(this.logoLabel);
            this.Controls.Add(this.logoTextBox);
            this.Controls.Add(this.chrOutputTextBox);
            this.Controls.Add(this.chrOutputLabel);
            this.Name = "TitleDialog";
            this.Text = "TitleDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fontTextBox;
        private System.Windows.Forms.Label fontLabel;
        private System.Windows.Forms.Label logoLabel;
        private System.Windows.Forms.TextBox logoTextBox;
        private System.Windows.Forms.TextBox chrOutputTextBox;
        private System.Windows.Forms.Label chrOutputLabel;
        private System.Windows.Forms.Button processButton;
    }
}