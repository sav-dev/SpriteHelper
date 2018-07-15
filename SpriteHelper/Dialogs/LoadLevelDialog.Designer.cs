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
            this.bgSpecTextBox = new System.Windows.Forms.TextBox();
            this.bgSpecLabel = new System.Windows.Forms.Label();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.levelTextBox = new System.Windows.Forms.TextBox();
            this.browseLevelButton = new System.Windows.Forms.Button();
            this.browseBgSpecButton = new System.Windows.Forms.Button();
            this.browsePalettesButton = new System.Windows.Forms.Button();
            this.browseEnSpecButton = new System.Windows.Forms.Button();
            this.enSpecTextBox = new System.Windows.Forms.TextBox();
            this.enSpecLabel = new System.Windows.Forms.Label();
            this.browsePlayerButton = new System.Windows.Forms.Button();
            this.playerTextBox = new System.Windows.Forms.TextBox();
            this.playerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(365, 142);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 23);
            this.okButton.TabIndex = 40;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(426, 142);
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
            // bgSpecTextBox
            // 
            this.bgSpecTextBox.Location = new System.Drawing.Point(67, 32);
            this.bgSpecTextBox.Name = "bgSpecTextBox";
            this.bgSpecTextBox.Size = new System.Drawing.Size(414, 20);
            this.bgSpecTextBox.TabIndex = 42;
            // 
            // bgSpecLabel
            // 
            this.bgSpecLabel.AutoSize = true;
            this.bgSpecLabel.Location = new System.Drawing.Point(12, 35);
            this.bgSpecLabel.Name = "bgSpecLabel";
            this.bgSpecLabel.Size = new System.Drawing.Size(51, 13);
            this.bgSpecLabel.TabIndex = 43;
            this.bgSpecLabel.Text = "Bg. Spec";
            // 
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(67, 84);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.Size = new System.Drawing.Size(414, 20);
            this.palettesTextBox.TabIndex = 44;
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(12, 87);
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
            this.browseLevelButton.Location = new System.Drawing.Point(487, 4);
            this.browseLevelButton.Name = "browseLevelButton";
            this.browseLevelButton.Size = new System.Drawing.Size(75, 23);
            this.browseLevelButton.TabIndex = 48;
            this.browseLevelButton.Text = "Browse";
            this.browseLevelButton.UseVisualStyleBackColor = true;
            this.browseLevelButton.Click += new System.EventHandler(this.BrowseLevelButtonClick);
            // 
            // browseBgSpecButton
            // 
            this.browseBgSpecButton.Location = new System.Drawing.Point(487, 30);
            this.browseBgSpecButton.Name = "browseBgSpecButton";
            this.browseBgSpecButton.Size = new System.Drawing.Size(75, 23);
            this.browseBgSpecButton.TabIndex = 49;
            this.browseBgSpecButton.Text = "Browse";
            this.browseBgSpecButton.UseVisualStyleBackColor = true;
            this.browseBgSpecButton.Click += new System.EventHandler(this.BrowseBgSpecButtonClick);
            // 
            // browsePalettesButton
            // 
            this.browsePalettesButton.Location = new System.Drawing.Point(487, 82);
            this.browsePalettesButton.Name = "browsePalettesButton";
            this.browsePalettesButton.Size = new System.Drawing.Size(75, 23);
            this.browsePalettesButton.TabIndex = 50;
            this.browsePalettesButton.Text = "Browse";
            this.browsePalettesButton.UseVisualStyleBackColor = true;
            this.browsePalettesButton.Click += new System.EventHandler(this.BrowsePalettesButtonClick);
            // 
            // browseEnSpecButton
            // 
            this.browseEnSpecButton.Location = new System.Drawing.Point(487, 56);
            this.browseEnSpecButton.Name = "browseEnSpecButton";
            this.browseEnSpecButton.Size = new System.Drawing.Size(75, 23);
            this.browseEnSpecButton.TabIndex = 53;
            this.browseEnSpecButton.Text = "Browse";
            this.browseEnSpecButton.UseVisualStyleBackColor = true;
            this.browseEnSpecButton.Click += new System.EventHandler(this.BrowseEnSpecButtonClick);
            // 
            // enSpecTextBox
            // 
            this.enSpecTextBox.Location = new System.Drawing.Point(67, 58);
            this.enSpecTextBox.Name = "enSpecTextBox";
            this.enSpecTextBox.Size = new System.Drawing.Size(414, 20);
            this.enSpecTextBox.TabIndex = 51;
            // 
            // enSpecLabel
            // 
            this.enSpecLabel.AutoSize = true;
            this.enSpecLabel.Location = new System.Drawing.Point(12, 61);
            this.enSpecLabel.Name = "enSpecLabel";
            this.enSpecLabel.Size = new System.Drawing.Size(51, 13);
            this.enSpecLabel.TabIndex = 52;
            this.enSpecLabel.Text = "En. Spec";
            // 
            // browsePlayerButton
            // 
            this.browsePlayerButton.Location = new System.Drawing.Point(487, 108);
            this.browsePlayerButton.Name = "browsePlayerButton";
            this.browsePlayerButton.Size = new System.Drawing.Size(75, 23);
            this.browsePlayerButton.TabIndex = 56;
            this.browsePlayerButton.Text = "Browse";
            this.browsePlayerButton.UseVisualStyleBackColor = true;
            this.browsePlayerButton.Click += new System.EventHandler(this.BrowsePlayerButtonClick);
            // 
            // playerTextBox
            // 
            this.playerTextBox.Location = new System.Drawing.Point(67, 110);
            this.playerTextBox.Name = "playerTextBox";
            this.playerTextBox.Size = new System.Drawing.Size(414, 20);
            this.playerTextBox.TabIndex = 54;
            // 
            // playerLabel
            // 
            this.playerLabel.AutoSize = true;
            this.playerLabel.Location = new System.Drawing.Point(12, 113);
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(36, 13);
            this.playerLabel.TabIndex = 55;
            this.playerLabel.Text = "Player";
            // 
            // LoadLevelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 216);
            this.Controls.Add(this.browsePlayerButton);
            this.Controls.Add(this.playerTextBox);
            this.Controls.Add(this.playerLabel);
            this.Controls.Add(this.browseEnSpecButton);
            this.Controls.Add(this.enSpecTextBox);
            this.Controls.Add(this.enSpecLabel);
            this.Controls.Add(this.browsePalettesButton);
            this.Controls.Add(this.browseBgSpecButton);
            this.Controls.Add(this.browseLevelButton);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.bgSpecTextBox);
            this.Controls.Add(this.bgSpecLabel);
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
        private System.Windows.Forms.TextBox bgSpecTextBox;
        private System.Windows.Forms.Label bgSpecLabel;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox levelTextBox;
        private System.Windows.Forms.Button browseLevelButton;
        private System.Windows.Forms.Button browseBgSpecButton;
        private System.Windows.Forms.Button browsePalettesButton;
        private System.Windows.Forms.Button browseEnSpecButton;
        private System.Windows.Forms.TextBox enSpecTextBox;
        private System.Windows.Forms.Label enSpecLabel;
        private System.Windows.Forms.Button browsePlayerButton;
        private System.Windows.Forms.TextBox playerTextBox;
        private System.Windows.Forms.Label playerLabel;
    }
}