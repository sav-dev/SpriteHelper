namespace SpriteHelper.Dialogs
{
    partial class Player
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.framesListBox = new System.Windows.Forms.ListBox();
            this.specTextBox = new System.Windows.Forms.TextBox();
            this.specLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.zoomLabel = new System.Windows.Forms.Label();
            this.zoomPicker = new System.Windows.Forms.NumericUpDown();
            this.animationsListBox = new System.Windows.Forms.ListBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.applyPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.directionCheckBox = new System.Windows.Forms.CheckBox();
            this.showBoxesCheckBox = new System.Windows.Forms.CheckBox();
            this.codeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(15, 125);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(270, 303);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // framesListBox
            // 
            this.framesListBox.FormattingEnabled = true;
            this.framesListBox.Location = new System.Drawing.Point(389, 125);
            this.framesListBox.Name = "framesListBox";
            this.framesListBox.Size = new System.Drawing.Size(92, 303);
            this.framesListBox.TabIndex = 1;
            this.framesListBox.SelectedIndexChanged += new System.EventHandler(this.FramesListBoxSelectedIndexChanged);
            // 
            // specTextBox
            // 
            this.specTextBox.Location = new System.Drawing.Point(67, 10);
            this.specTextBox.Name = "specTextBox";
            this.specTextBox.Size = new System.Drawing.Size(414, 20);
            this.specTextBox.TabIndex = 3;
            // 
            // specLabel
            // 
            this.specLabel.AutoSize = true;
            this.specLabel.Location = new System.Drawing.Point(12, 13);
            this.specLabel.Name = "specLabel";
            this.specLabel.Size = new System.Drawing.Size(32, 13);
            this.specLabel.TabIndex = 5;
            this.specLabel.Text = "Spec";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(308, 63);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 6;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // zoomLabel
            // 
            this.zoomLabel.AutoSize = true;
            this.zoomLabel.Location = new System.Drawing.Point(14, 68);
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(34, 13);
            this.zoomLabel.TabIndex = 7;
            this.zoomLabel.Text = "Zoom";
            // 
            // zoomPicker
            // 
            this.zoomPicker.Location = new System.Drawing.Point(67, 66);
            this.zoomPicker.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.zoomPicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zoomPicker.Name = "zoomPicker";
            this.zoomPicker.Size = new System.Drawing.Size(46, 20);
            this.zoomPicker.TabIndex = 9;
            this.zoomPicker.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.zoomPicker.ValueChanged += new System.EventHandler(this.ZoomPickerValueChanged);
            // 
            // animationsListBox
            // 
            this.animationsListBox.FormattingEnabled = true;
            this.animationsListBox.Location = new System.Drawing.Point(291, 124);
            this.animationsListBox.Name = "animationsListBox";
            this.animationsListBox.Size = new System.Drawing.Size(92, 303);
            this.animationsListBox.TabIndex = 10;
            this.animationsListBox.SelectedIndexChanged += new System.EventHandler(this.AnimationsListBoxSelectedIndexChanged);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(389, 89);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 11;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(308, 89);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // timer
            // 
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // applyPaletteCheckbox
            // 
            this.applyPaletteCheckbox.AutoSize = true;
            this.applyPaletteCheckbox.Checked = true;
            this.applyPaletteCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPaletteCheckbox.Location = new System.Drawing.Point(15, 95);
            this.applyPaletteCheckbox.Name = "applyPaletteCheckbox";
            this.applyPaletteCheckbox.Size = new System.Drawing.Size(88, 17);
            this.applyPaletteCheckbox.TabIndex = 13;
            this.applyPaletteCheckbox.Text = "Apply Palette";
            this.applyPaletteCheckbox.UseVisualStyleBackColor = true;
            this.applyPaletteCheckbox.CheckedChanged += new System.EventHandler(this.ApplyPaletteCheckboxCheckedChanged);
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(12, 39);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(45, 13);
            this.palettesLabel.TabIndex = 15;
            this.palettesLabel.Text = "Palettes";
            // 
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(67, 36);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.Size = new System.Drawing.Size(414, 20);
            this.palettesTextBox.TabIndex = 14;
            // 
            // directionCheckBox
            // 
            this.directionCheckBox.AutoSize = true;
            this.directionCheckBox.Location = new System.Drawing.Point(114, 95);
            this.directionCheckBox.Name = "directionCheckBox";
            this.directionCheckBox.Size = new System.Drawing.Size(75, 17);
            this.directionCheckBox.TabIndex = 18;
            this.directionCheckBox.Text = "Going Left";
            this.directionCheckBox.UseVisualStyleBackColor = true;
            this.directionCheckBox.CheckedChanged += new System.EventHandler(this.DirectionCheckBoxCheckedChanged);
            // 
            // showBoxesCheckBox
            // 
            this.showBoxesCheckBox.AutoSize = true;
            this.showBoxesCheckBox.Location = new System.Drawing.Point(200, 95);
            this.showBoxesCheckBox.Name = "showBoxesCheckBox";
            this.showBoxesCheckBox.Size = new System.Drawing.Size(85, 17);
            this.showBoxesCheckBox.TabIndex = 19;
            this.showBoxesCheckBox.Text = "Show Boxes";
            this.showBoxesCheckBox.UseVisualStyleBackColor = true;
            this.showBoxesCheckBox.CheckedChanged += new System.EventHandler(this.ShowBoxesCheckBoxCheckedChanged);
            // 
            // codeButton
            // 
            this.codeButton.Location = new System.Drawing.Point(389, 63);
            this.codeButton.Name = "codeButton";
            this.codeButton.Size = new System.Drawing.Size(75, 23);
            this.codeButton.TabIndex = 23;
            this.codeButton.Text = "Code";
            this.codeButton.UseVisualStyleBackColor = true;
            this.codeButton.Click += new System.EventHandler(this.CodeButtonClick);
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 438);
            this.Controls.Add(this.codeButton);
            this.Controls.Add(this.showBoxesCheckBox);
            this.Controls.Add(this.directionCheckBox);
            this.Controls.Add(this.palettesLabel);
            this.Controls.Add(this.palettesTextBox);
            this.Controls.Add(this.applyPaletteCheckbox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.animationsListBox);
            this.Controls.Add(this.zoomPicker);
            this.Controls.Add(this.zoomLabel);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.specLabel);
            this.Controls.Add(this.specTextBox);
            this.Controls.Add(this.framesListBox);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Player";
            this.Text = "Animation Helper";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ListBox framesListBox;
        private System.Windows.Forms.TextBox specTextBox;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Label zoomLabel;
        private System.Windows.Forms.NumericUpDown zoomPicker;
        private System.Windows.Forms.ListBox animationsListBox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox applyPaletteCheckbox;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.CheckBox directionCheckBox;
        private System.Windows.Forms.CheckBox showBoxesCheckBox;
        private System.Windows.Forms.Button codeButton;
    }
}

