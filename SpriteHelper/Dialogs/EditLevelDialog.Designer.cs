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
            this.bgPaletteLabel = new System.Windows.Forms.Label();
            this.bgPaletteComboBox = new System.Windows.Forms.ComboBox();
            this.commonGroupBox = new System.Windows.Forms.GroupBox();
            this.levelTypeComboBox = new System.Windows.Forms.ComboBox();
            this.lvlTypeLabel = new System.Windows.Forms.Label();
            this.exitGroupBox = new System.Windows.Forms.GroupBox();
            this.jetpackGroupBox = new System.Windows.Forms.GroupBox();
            this.scrollSpeedComboBox = new System.Windows.Forms.ComboBox();
            this.scrollSpeedLabel = new System.Windows.Forms.Label();
            this.commonGroupBox.SuspendLayout();
            this.exitGroupBox.SuspendLayout();
            this.jetpackGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(8, 22);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(67, 13);
            this.widthLabel.TabIndex = 39;
            this.widthLabel.Text = "Width in tiles";
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(84, 19);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(48, 20);
            this.widthTextBox.TabIndex = 38;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(157, 220);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(55, 23);
            this.okButton.TabIndex = 40;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(223, 220);
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
            this.playerXLabel.Location = new System.Drawing.Point(8, 46);
            this.playerXLabel.Name = "playerXLabel";
            this.playerXLabel.Size = new System.Drawing.Size(46, 13);
            this.playerXLabel.TabIndex = 43;
            this.playerXLabel.Text = "Player X";
            // 
            // playerXTextBox
            // 
            this.playerXTextBox.Location = new System.Drawing.Point(84, 43);
            this.playerXTextBox.Name = "playerXTextBox";
            this.playerXTextBox.Size = new System.Drawing.Size(48, 20);
            this.playerXTextBox.TabIndex = 42;
            // 
            // playerYTextBox
            // 
            this.playerYTextBox.Location = new System.Drawing.Point(222, 43);
            this.playerYTextBox.Name = "playerYTextBox";
            this.playerYTextBox.Size = new System.Drawing.Size(48, 20);
            this.playerYTextBox.TabIndex = 44;
            // 
            // playerYLabel
            // 
            this.playerYLabel.AutoSize = true;
            this.playerYLabel.Location = new System.Drawing.Point(146, 46);
            this.playerYLabel.Name = "playerYLabel";
            this.playerYLabel.Size = new System.Drawing.Size(46, 13);
            this.playerYLabel.TabIndex = 45;
            this.playerYLabel.Text = "Player Y";
            // 
            // exitYLabel
            // 
            this.exitYLabel.AutoSize = true;
            this.exitYLabel.Location = new System.Drawing.Point(146, 16);
            this.exitYLabel.Name = "exitYLabel";
            this.exitYLabel.Size = new System.Drawing.Size(50, 13);
            this.exitYLabel.TabIndex = 49;
            this.exitYLabel.Text = "Exit tile Y";
            // 
            // exitYTextBox
            // 
            this.exitYTextBox.Location = new System.Drawing.Point(222, 13);
            this.exitYTextBox.Name = "exitYTextBox";
            this.exitYTextBox.Size = new System.Drawing.Size(48, 20);
            this.exitYTextBox.TabIndex = 48;
            // 
            // exitXLabel
            // 
            this.exitXLabel.AutoSize = true;
            this.exitXLabel.Location = new System.Drawing.Point(8, 16);
            this.exitXLabel.Name = "exitXLabel";
            this.exitXLabel.Size = new System.Drawing.Size(50, 13);
            this.exitXLabel.TabIndex = 47;
            this.exitXLabel.Text = "Exit tile X";
            // 
            // exitXTextBox
            // 
            this.exitXTextBox.Location = new System.Drawing.Point(84, 13);
            this.exitXTextBox.Name = "exitXTextBox";
            this.exitXTextBox.Size = new System.Drawing.Size(48, 20);
            this.exitXTextBox.TabIndex = 46;
            // 
            // bgPaletteLabel
            // 
            this.bgPaletteLabel.AutoSize = true;
            this.bgPaletteLabel.Location = new System.Drawing.Point(146, 22);
            this.bgPaletteLabel.Name = "bgPaletteLabel";
            this.bgPaletteLabel.Size = new System.Drawing.Size(57, 13);
            this.bgPaletteLabel.TabIndex = 50;
            this.bgPaletteLabel.Text = "BG palette";
            // 
            // bgPaletteComboBox
            // 
            this.bgPaletteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bgPaletteComboBox.FormattingEnabled = true;
            this.bgPaletteComboBox.Location = new System.Drawing.Point(222, 19);
            this.bgPaletteComboBox.Name = "bgPaletteComboBox";
            this.bgPaletteComboBox.Size = new System.Drawing.Size(48, 21);
            this.bgPaletteComboBox.TabIndex = 51;
            // 
            // commonGroupBox
            // 
            this.commonGroupBox.Controls.Add(this.levelTypeComboBox);
            this.commonGroupBox.Controls.Add(this.lvlTypeLabel);
            this.commonGroupBox.Controls.Add(this.bgPaletteComboBox);
            this.commonGroupBox.Controls.Add(this.widthTextBox);
            this.commonGroupBox.Controls.Add(this.bgPaletteLabel);
            this.commonGroupBox.Controls.Add(this.widthLabel);
            this.commonGroupBox.Controls.Add(this.playerXTextBox);
            this.commonGroupBox.Controls.Add(this.playerXLabel);
            this.commonGroupBox.Controls.Add(this.playerYTextBox);
            this.commonGroupBox.Controls.Add(this.playerYLabel);
            this.commonGroupBox.Location = new System.Drawing.Point(8, 12);
            this.commonGroupBox.Name = "commonGroupBox";
            this.commonGroupBox.Size = new System.Drawing.Size(281, 100);
            this.commonGroupBox.TabIndex = 52;
            this.commonGroupBox.TabStop = false;
            this.commonGroupBox.Text = "Common";
            // 
            // levelTypeComboBox
            // 
            this.levelTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levelTypeComboBox.FormattingEnabled = true;
            this.levelTypeComboBox.Location = new System.Drawing.Point(84, 67);
            this.levelTypeComboBox.Name = "levelTypeComboBox";
            this.levelTypeComboBox.Size = new System.Drawing.Size(96, 21);
            this.levelTypeComboBox.TabIndex = 53;
            this.levelTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.LevelTypeComboBoxSelectedIndexChanged);
            // 
            // lvlTypeLabel
            // 
            this.lvlTypeLabel.AutoSize = true;
            this.lvlTypeLabel.Location = new System.Drawing.Point(8, 70);
            this.lvlTypeLabel.Name = "lvlTypeLabel";
            this.lvlTypeLabel.Size = new System.Drawing.Size(60, 13);
            this.lvlTypeLabel.TabIndex = 52;
            this.lvlTypeLabel.Text = "Level Type";
            // 
            // exitGroupBox
            // 
            this.exitGroupBox.Controls.Add(this.exitXLabel);
            this.exitGroupBox.Controls.Add(this.exitXTextBox);
            this.exitGroupBox.Controls.Add(this.exitYLabel);
            this.exitGroupBox.Controls.Add(this.exitYTextBox);
            this.exitGroupBox.Location = new System.Drawing.Point(8, 118);
            this.exitGroupBox.Name = "exitGroupBox";
            this.exitGroupBox.Size = new System.Drawing.Size(281, 45);
            this.exitGroupBox.TabIndex = 53;
            this.exitGroupBox.TabStop = false;
            this.exitGroupBox.Text = "Exit";
            // 
            // jetpackGroupBox
            // 
            this.jetpackGroupBox.Controls.Add(this.scrollSpeedComboBox);
            this.jetpackGroupBox.Controls.Add(this.scrollSpeedLabel);
            this.jetpackGroupBox.Location = new System.Drawing.Point(8, 169);
            this.jetpackGroupBox.Name = "jetpackGroupBox";
            this.jetpackGroupBox.Size = new System.Drawing.Size(281, 45);
            this.jetpackGroupBox.TabIndex = 54;
            this.jetpackGroupBox.TabStop = false;
            this.jetpackGroupBox.Text = "Jetpack";
            // 
            // scrollSpeedComboBox
            // 
            this.scrollSpeedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scrollSpeedComboBox.FormattingEnabled = true;
            this.scrollSpeedComboBox.Items.AddRange(new object[] {
            "0.25",
            "0.5",
            "1",
            "2",
            "3",
            "4"});
            this.scrollSpeedComboBox.Location = new System.Drawing.Point(84, 13);
            this.scrollSpeedComboBox.Name = "scrollSpeedComboBox";
            this.scrollSpeedComboBox.Size = new System.Drawing.Size(48, 21);
            this.scrollSpeedComboBox.TabIndex = 54;
            // 
            // scrollSpeedLabel
            // 
            this.scrollSpeedLabel.AutoSize = true;
            this.scrollSpeedLabel.Location = new System.Drawing.Point(8, 16);
            this.scrollSpeedLabel.Name = "scrollSpeedLabel";
            this.scrollSpeedLabel.Size = new System.Drawing.Size(67, 13);
            this.scrollSpeedLabel.TabIndex = 47;
            this.scrollSpeedLabel.Text = "Scroll Speed";
            // 
            // EditLevelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 257);
            this.Controls.Add(this.jetpackGroupBox);
            this.Controls.Add(this.exitGroupBox);
            this.Controls.Add(this.commonGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditLevelDialog";
            this.Text = "Edit Level";
            this.commonGroupBox.ResumeLayout(false);
            this.commonGroupBox.PerformLayout();
            this.exitGroupBox.ResumeLayout(false);
            this.exitGroupBox.PerformLayout();
            this.jetpackGroupBox.ResumeLayout(false);
            this.jetpackGroupBox.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label bgPaletteLabel;
        private System.Windows.Forms.ComboBox bgPaletteComboBox;
        private System.Windows.Forms.GroupBox commonGroupBox;
        private System.Windows.Forms.ComboBox levelTypeComboBox;
        private System.Windows.Forms.Label lvlTypeLabel;
        private System.Windows.Forms.GroupBox exitGroupBox;
        private System.Windows.Forms.GroupBox jetpackGroupBox;
        private System.Windows.Forms.ComboBox scrollSpeedComboBox;
        private System.Windows.Forms.Label scrollSpeedLabel;
    }
}