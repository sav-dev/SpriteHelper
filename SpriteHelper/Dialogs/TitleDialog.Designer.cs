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
            this.stringsTextBox = new System.Windows.Forms.TextBox();
            this.stringsLabel = new System.Windows.Forms.Label();
            this.cursorTextBox = new System.Windows.Forms.TextBox();
            this.cursorLabel = new System.Windows.Forms.Label();
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
            this.processButton.Location = new System.Drawing.Point(431, 127);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(122, 23);
            this.processButton.TabIndex = 55;
            this.processButton.Text = "Process and get code";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.ProcessButtonClick);
            // 
            // stringsTextBox
            // 
            this.stringsTextBox.Location = new System.Drawing.Point(75, 77);
            this.stringsTextBox.Name = "stringsTextBox";
            this.stringsTextBox.ReadOnly = true;
            this.stringsTextBox.Size = new System.Drawing.Size(478, 20);
            this.stringsTextBox.TabIndex = 56;
            // 
            // stringsLabel
            // 
            this.stringsLabel.AutoSize = true;
            this.stringsLabel.Location = new System.Drawing.Point(7, 80);
            this.stringsLabel.Name = "stringsLabel";
            this.stringsLabel.Size = new System.Drawing.Size(39, 13);
            this.stringsLabel.TabIndex = 57;
            this.stringsLabel.Text = "Strings";
            // 
            // cursorTextBox
            // 
            this.cursorTextBox.Location = new System.Drawing.Point(75, 101);
            this.cursorTextBox.Name = "cursorTextBox";
            this.cursorTextBox.ReadOnly = true;
            this.cursorTextBox.Size = new System.Drawing.Size(478, 20);
            this.cursorTextBox.TabIndex = 58;
            // 
            // cursorLabel
            // 
            this.cursorLabel.AutoSize = true;
            this.cursorLabel.Location = new System.Drawing.Point(7, 104);
            this.cursorLabel.Name = "cursorLabel";
            this.cursorLabel.Size = new System.Drawing.Size(37, 13);
            this.cursorLabel.TabIndex = 59;
            this.cursorLabel.Text = "Cursor";
            // 
            // TitleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 163);
            this.Controls.Add(this.cursorTextBox);
            this.Controls.Add(this.cursorLabel);
            this.Controls.Add(this.stringsTextBox);
            this.Controls.Add(this.stringsLabel);
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
        private System.Windows.Forms.TextBox stringsTextBox;
        private System.Windows.Forms.Label stringsLabel;
        private System.Windows.Forms.TextBox cursorTextBox;
        private System.Windows.Forms.Label cursorLabel;
    }
}