namespace SpriteHelper.Dialogs
{
    partial class LoadLevelDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.levelLabel = new System.Windows.Forms.Label();
            this.specTextBox = new System.Windows.Forms.TextBox();
            this.specLabel = new System.Windows.Forms.Label();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.levelTextBox = new System.Windows.Forms.TextBox();
            this.browseLevelButton = new System.Windows.Forms.Button();
            this.browseSpecButton = new System.Windows.Forms.Button();
            this.browsePalettesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(365, 84);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 23);
            this.okButton.TabIndex = 40;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(426, 84);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 23);
            this.cancelButton.TabIndex = 41;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(12, 9);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(33, 13);
            this.levelLabel.TabIndex = 47;
            this.levelLabel.Text = "Level";
            // 
            // specTextBox
            // 
            this.specTextBox.Location = new System.Drawing.Point(67, 32);
            this.specTextBox.Name = "specTextBox";
            this.specTextBox.Size = new System.Drawing.Size(414, 20);
            this.specTextBox.TabIndex = 42;
            // 
            // specLabel
            // 
            this.specLabel.AutoSize = true;
            this.specLabel.Location = new System.Drawing.Point(12, 35);
            this.specLabel.Name = "specLabel";
            this.specLabel.Size = new System.Drawing.Size(32, 13);
            this.specLabel.TabIndex = 43;
            this.specLabel.Text = "Spec";
            // 
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(67, 58);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.Size = new System.Drawing.Size(414, 20);
            this.palettesTextBox.TabIndex = 44;
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(12, 61);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(45, 13);
            this.palettesLabel.TabIndex = 45;
            this.palettesLabel.Text = "Palettes";
            // 
            // levelTextBox
            // 
            this.levelTextBox.Location = new System.Drawing.Point(67, 6);
            this.levelTextBox.Name = "levelTextBox";
            this.levelTextBox.Size = new System.Drawing.Size(414, 20);
            this.levelTextBox.TabIndex = 46;
            // 
            // browseLevelButton
            // 
            this.browseLevelButton.Location = new System.Drawing.Point(487, 6);
            this.browseLevelButton.Name = "browseLevelButton";
            this.browseLevelButton.Size = new System.Drawing.Size(75, 23);
            this.browseLevelButton.TabIndex = 48;
            this.browseLevelButton.Text = "Browse";
            this.browseLevelButton.UseVisualStyleBackColor = true;
            this.browseLevelButton.Click += new System.EventHandler(this.BrowseLevelButtonClick);
            // 
            // browseSpecButton
            // 
            this.browseSpecButton.Location = new System.Drawing.Point(487, 31);
            this.browseSpecButton.Name = "browseSpecButton";
            this.browseSpecButton.Size = new System.Drawing.Size(75, 23);
            this.browseSpecButton.TabIndex = 49;
            this.browseSpecButton.Text = "Browse";
            this.browseSpecButton.UseVisualStyleBackColor = true;
            this.browseSpecButton.Click += new System.EventHandler(this.BrowseSpecButtonClick);
            // 
            // browsePalettesButton
            // 
            this.browsePalettesButton.Location = new System.Drawing.Point(487, 56);
            this.browsePalettesButton.Name = "browsePalettesButton";
            this.browsePalettesButton.Size = new System.Drawing.Size(75, 23);
            this.browsePalettesButton.TabIndex = 50;
            this.browsePalettesButton.Text = "Browse";
            this.browsePalettesButton.UseVisualStyleBackColor = true;
            this.browsePalettesButton.Click += new System.EventHandler(this.BrowsePalettesButtonClick);
            // 
            // LoadLevelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 115);
            this.Controls.Add(this.browsePalettesButton);
            this.Controls.Add(this.browseSpecButton);
            this.Controls.Add(this.browseLevelButton);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.specTextBox);
            this.Controls.Add(this.specLabel);
            this.Controls.Add(this.palettesTextBox);
            this.Controls.Add(this.palettesLabel);
            this.Controls.Add(this.levelTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoadLevelDialog";
            this.Text = "Open Level";
            this.Load += new System.EventHandler(this.LoadLevelDialogLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.TextBox specTextBox;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox levelTextBox;
        private System.Windows.Forms.Button browseLevelButton;
        private System.Windows.Forms.Button browseSpecButton;
        private System.Windows.Forms.Button browsePalettesButton;
    }
}