namespace SpriteHelper.Dialogs
{
    partial class AnimationsDialog
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
            this.directoryLabel = new System.Windows.Forms.Label();
            this.directoryTextBox = new System.Windows.Forms.TextBox();
            this.speedPicker = new System.Windows.Forms.NumericUpDown();
            this.speedLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.framesListBox = new System.Windows.Forms.ListBox();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.zoomPicker = new System.Windows.Forms.NumericUpDown();
            this.zoomLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.movSpeedPicker = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.bgComboBox = new System.Windows.Forms.ComboBox();
            this.directionComboBox = new System.Windows.Forms.ComboBox();
            this.moveLabel = new System.Windows.Forms.Label();
            this.bgLabel = new System.Windows.Forms.Label();
            this.picturePanel = new SpriteHelper.Controls.DoubleBufferedPanel();
            this.browseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.speedPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movSpeedPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // directoryLabel
            // 
            this.directoryLabel.AutoSize = true;
            this.directoryLabel.Location = new System.Drawing.Point(6, 9);
            this.directoryLabel.Name = "directoryLabel";
            this.directoryLabel.Size = new System.Drawing.Size(49, 13);
            this.directoryLabel.TabIndex = 6;
            this.directoryLabel.Text = "Directory";
            // 
            // directoryTextBox
            // 
            this.directoryTextBox.Location = new System.Drawing.Point(61, 6);
            this.directoryTextBox.Name = "directoryTextBox";
            this.directoryTextBox.Size = new System.Drawing.Size(443, 20);
            this.directoryTextBox.TabIndex = 5;
            // 
            // speedPicker
            // 
            this.speedPicker.Location = new System.Drawing.Point(447, 70);
            this.speedPicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speedPicker.Name = "speedPicker";
            this.speedPicker.Size = new System.Drawing.Size(46, 20);
            this.speedPicker.TabIndex = 12;
            this.speedPicker.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.speedPicker.ValueChanged += new System.EventHandler(this.SpeedPickerValueChanged);
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(376, 72);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(65, 13);
            this.speedLabel.TabIndex = 11;
            this.speedLabel.Text = "Anim. speed";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(348, 32);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 10;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // framesListBox
            // 
            this.framesListBox.FormattingEnabled = true;
            this.framesListBox.Location = new System.Drawing.Point(515, 96);
            this.framesListBox.Name = "framesListBox";
            this.framesListBox.Size = new System.Drawing.Size(75, 394);
            this.framesListBox.TabIndex = 14;
            this.framesListBox.SelectedIndexChanged += new System.EventHandler(this.FramesListBoxSelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(429, 32);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 16;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(510, 32);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 15;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // zoomPicker
            // 
            this.zoomPicker.Location = new System.Drawing.Point(539, 70);
            this.zoomPicker.Maximum = new decimal(new int[] {
            10,
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
            this.zoomPicker.TabIndex = 18;
            this.zoomPicker.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zoomPicker.ValueChanged += new System.EventHandler(this.ZoomPickerValueChanged);
            // 
            // zoomLabel
            // 
            this.zoomLabel.AutoSize = true;
            this.zoomLabel.Location = new System.Drawing.Point(499, 72);
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(34, 13);
            this.zoomLabel.TabIndex = 17;
            this.zoomLabel.Text = "Zoom";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // movSpeedPicker
            // 
            this.movSpeedPicker.Location = new System.Drawing.Point(324, 70);
            this.movSpeedPicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.movSpeedPicker.Name = "movSpeedPicker";
            this.movSpeedPicker.Size = new System.Drawing.Size(46, 20);
            this.movSpeedPicker.TabIndex = 20;
            this.movSpeedPicker.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.movSpeedPicker.ValueChanged += new System.EventHandler(this.MovSpeedPickerValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Mov. speed";
            // 
            // bgComboBox
            // 
            this.bgComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bgComboBox.FormattingEnabled = true;
            this.bgComboBox.Items.AddRange(new object[] {
            "Black",
            "White"});
            this.bgComboBox.Location = new System.Drawing.Point(46, 69);
            this.bgComboBox.Name = "bgComboBox";
            this.bgComboBox.Size = new System.Drawing.Size(83, 21);
            this.bgComboBox.TabIndex = 21;
            this.bgComboBox.SelectedIndexChanged += new System.EventHandler(this.BgComboBoxSelectedIndexChanged);
            // 
            // directionComboBox
            // 
            this.directionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.directionComboBox.FormattingEnabled = true;
            this.directionComboBox.Items.AddRange(new object[] {
            "None",
            "Up",
            "Down",
            "Left",
            "Right"});
            this.directionComboBox.Location = new System.Drawing.Point(175, 69);
            this.directionComboBox.Name = "directionComboBox";
            this.directionComboBox.Size = new System.Drawing.Size(74, 21);
            this.directionComboBox.TabIndex = 22;
            this.directionComboBox.SelectedIndexChanged += new System.EventHandler(this.DirectionComboBoxSelectedIndexChanged);
            // 
            // moveLabel
            // 
            this.moveLabel.AutoSize = true;
            this.moveLabel.Location = new System.Drawing.Point(135, 72);
            this.moveLabel.Name = "moveLabel";
            this.moveLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.moveLabel.Size = new System.Drawing.Size(34, 13);
            this.moveLabel.TabIndex = 23;
            this.moveLabel.Text = "Move";
            // 
            // bgLabel
            // 
            this.bgLabel.AutoSize = true;
            this.bgLabel.Location = new System.Drawing.Point(18, 72);
            this.bgLabel.Name = "bgLabel";
            this.bgLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bgLabel.Size = new System.Drawing.Size(22, 13);
            this.bgLabel.TabIndex = 24;
            this.bgLabel.Text = "BG";
            // 
            // picturePanel
            // 
            this.picturePanel.Location = new System.Drawing.Point(17, 96);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(492, 394);
            this.picturePanel.TabIndex = 25;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(510, 4);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 26;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButtonClick);
            // 
            // AnimationsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 502);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.picturePanel);
            this.Controls.Add(this.bgLabel);
            this.Controls.Add(this.moveLabel);
            this.Controls.Add(this.directionComboBox);
            this.Controls.Add(this.bgComboBox);
            this.Controls.Add(this.movSpeedPicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zoomPicker);
            this.Controls.Add(this.zoomLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.framesListBox);
            this.Controls.Add(this.speedPicker);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.directoryLabel);
            this.Controls.Add(this.directoryTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AnimationsDialog";
            this.Text = "AnimationsDialog";
            ((System.ComponentModel.ISupportInitialize)(this.speedPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movSpeedPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label directoryLabel;
        private System.Windows.Forms.TextBox directoryTextBox;
        private System.Windows.Forms.NumericUpDown speedPicker;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.ListBox framesListBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.NumericUpDown zoomPicker;
        private System.Windows.Forms.Label zoomLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.NumericUpDown movSpeedPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox bgComboBox;
        private System.Windows.Forms.ComboBox directionComboBox;
        private System.Windows.Forms.Label moveLabel;
        private System.Windows.Forms.Label bgLabel;
        private Controls.DoubleBufferedPanel picturePanel;
        private System.Windows.Forms.Button browseButton;
    }
}