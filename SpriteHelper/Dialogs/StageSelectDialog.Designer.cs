namespace SpriteHelper.Dialogs
{
    partial class StageSelectDialog
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.codeButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.stagesTextBox = new System.Windows.Forms.TextBox();
            this.stagesLabel = new System.Windows.Forms.Label();
            this.stringsTextBox = new System.Windows.Forms.TextBox();
            this.stringsLabel = new System.Windows.Forms.Label();
            this.fontTextBox = new System.Windows.Forms.TextBox();
            this.fontLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(26, 125);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(512, 448);
            this.pictureBox.TabIndex = 77;
            this.pictureBox.TabStop = false;
            // 
            // codeButton
            // 
            this.codeButton.Location = new System.Drawing.Point(158, 96);
            this.codeButton.Name = "codeButton";
            this.codeButton.Size = new System.Drawing.Size(75, 23);
            this.codeButton.TabIndex = 76;
            this.codeButton.Text = "Code";
            this.codeButton.UseVisualStyleBackColor = true;
            this.codeButton.Click += new System.EventHandler(this.CodeButtonClick);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(77, 96);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 75;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // stagesTextBox
            // 
            this.stagesTextBox.Location = new System.Drawing.Point(77, 18);
            this.stagesTextBox.Name = "stagesTextBox";
            this.stagesTextBox.ReadOnly = true;
            this.stagesTextBox.Size = new System.Drawing.Size(478, 20);
            this.stagesTextBox.TabIndex = 73;
            // 
            // stagesLabel
            // 
            this.stagesLabel.AutoSize = true;
            this.stagesLabel.Location = new System.Drawing.Point(9, 21);
            this.stagesLabel.Name = "stagesLabel";
            this.stagesLabel.Size = new System.Drawing.Size(40, 13);
            this.stagesLabel.TabIndex = 72;
            this.stagesLabel.Text = "Stages";
            // 
            // stringsTextBox
            // 
            this.stringsTextBox.Location = new System.Drawing.Point(77, 70);
            this.stringsTextBox.Name = "stringsTextBox";
            this.stringsTextBox.ReadOnly = true;
            this.stringsTextBox.Size = new System.Drawing.Size(478, 20);
            this.stringsTextBox.TabIndex = 70;
            // 
            // stringsLabel
            // 
            this.stringsLabel.AutoSize = true;
            this.stringsLabel.Location = new System.Drawing.Point(9, 73);
            this.stringsLabel.Name = "stringsLabel";
            this.stringsLabel.Size = new System.Drawing.Size(39, 13);
            this.stringsLabel.TabIndex = 71;
            this.stringsLabel.Text = "Strings";
            // 
            // fontTextBox
            // 
            this.fontTextBox.Location = new System.Drawing.Point(77, 44);
            this.fontTextBox.Name = "fontTextBox";
            this.fontTextBox.ReadOnly = true;
            this.fontTextBox.Size = new System.Drawing.Size(478, 20);
            this.fontTextBox.TabIndex = 69;
            // 
            // fontLabel
            // 
            this.fontLabel.AutoSize = true;
            this.fontLabel.Location = new System.Drawing.Point(9, 47);
            this.fontLabel.Name = "fontLabel";
            this.fontLabel.Size = new System.Drawing.Size(28, 13);
            this.fontLabel.TabIndex = 68;
            this.fontLabel.Text = "Font";
            // 
            // StageSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 591);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.codeButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.stagesTextBox);
            this.Controls.Add(this.stagesLabel);
            this.Controls.Add(this.stringsTextBox);
            this.Controls.Add(this.stringsLabel);
            this.Controls.Add(this.fontTextBox);
            this.Controls.Add(this.fontLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "StageSelectDialog";
            this.Text = "StageSelectDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button codeButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.TextBox stagesTextBox;
        private System.Windows.Forms.Label stagesLabel;
        private System.Windows.Forms.TextBox stringsTextBox;
        private System.Windows.Forms.Label stringsLabel;
        private System.Windows.Forms.TextBox fontTextBox;
        private System.Windows.Forms.Label fontLabel;
    }
}