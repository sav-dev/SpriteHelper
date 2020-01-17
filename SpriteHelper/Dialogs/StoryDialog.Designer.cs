namespace SpriteHelper.Dialogs
{
    partial class StoryDialog
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
            this.stringsTextBox = new System.Windows.Forms.TextBox();
            this.stringsLabel = new System.Windows.Forms.Label();
            this.fontTextBox = new System.Windows.Forms.TextBox();
            this.fontLabel = new System.Windows.Forms.Label();
            this.storyTextBox = new System.Windows.Forms.TextBox();
            this.storyLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // stringsTextBox
            // 
            this.stringsTextBox.Location = new System.Drawing.Point(75, 64);
            this.stringsTextBox.Name = "stringsTextBox";
            this.stringsTextBox.ReadOnly = true;
            this.stringsTextBox.Size = new System.Drawing.Size(478, 20);
            this.stringsTextBox.TabIndex = 60;
            // 
            // stringsLabel
            // 
            this.stringsLabel.AutoSize = true;
            this.stringsLabel.Location = new System.Drawing.Point(7, 67);
            this.stringsLabel.Name = "stringsLabel";
            this.stringsLabel.Size = new System.Drawing.Size(39, 13);
            this.stringsLabel.TabIndex = 61;
            this.stringsLabel.Text = "Strings";
            // 
            // fontTextBox
            // 
            this.fontTextBox.Location = new System.Drawing.Point(75, 38);
            this.fontTextBox.Name = "fontTextBox";
            this.fontTextBox.ReadOnly = true;
            this.fontTextBox.Size = new System.Drawing.Size(478, 20);
            this.fontTextBox.TabIndex = 59;
            // 
            // fontLabel
            // 
            this.fontLabel.AutoSize = true;
            this.fontLabel.Location = new System.Drawing.Point(7, 41);
            this.fontLabel.Name = "fontLabel";
            this.fontLabel.Size = new System.Drawing.Size(28, 13);
            this.fontLabel.TabIndex = 58;
            this.fontLabel.Text = "Font";
            // 
            // storyTextBox
            // 
            this.storyTextBox.Location = new System.Drawing.Point(75, 12);
            this.storyTextBox.Name = "storyTextBox";
            this.storyTextBox.Size = new System.Drawing.Size(478, 20);
            this.storyTextBox.TabIndex = 63;
            // 
            // storyLabel
            // 
            this.storyLabel.AutoSize = true;
            this.storyLabel.Location = new System.Drawing.Point(7, 15);
            this.storyLabel.Name = "storyLabel";
            this.storyLabel.Size = new System.Drawing.Size(31, 13);
            this.storyLabel.TabIndex = 62;
            this.storyLabel.Text = "Story";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(75, 90);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 64;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButtonClick);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(156, 90);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 65;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(237, 90);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 66;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButtonClick);
            // 
            // pictureBox1
            // 
            this.pictureBox.Location = new System.Drawing.Point(24, 119);
            this.pictureBox.Name = "pictureBox1";
            this.pictureBox.Size = new System.Drawing.Size(512, 448);
            this.pictureBox.TabIndex = 67;
            this.pictureBox.TabStop = false;
            // 
            // StoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 587);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.storyTextBox);
            this.Controls.Add(this.storyLabel);
            this.Controls.Add(this.stringsTextBox);
            this.Controls.Add(this.stringsLabel);
            this.Controls.Add(this.fontTextBox);
            this.Controls.Add(this.fontLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "StoryDialog";
            this.Text = "StoryDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox stringsTextBox;
        private System.Windows.Forms.Label stringsLabel;
        private System.Windows.Forms.TextBox fontTextBox;
        private System.Windows.Forms.Label fontLabel;
        private System.Windows.Forms.TextBox storyTextBox;
        private System.Windows.Forms.Label storyLabel;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}