﻿namespace SpriteHelper.Dialogs
{
    partial class EnemiesWindow
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
            this.palettesLabel = new System.Windows.Forms.Label();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.specLabel = new System.Windows.Forms.Label();
            this.specTextBox = new System.Windows.Forms.TextBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.bulletsTextBox = new System.Windows.Forms.TextBox();
            this.bulletsLabel = new System.Windows.Forms.Label();
            this.showBulletCheckBox = new System.Windows.Forms.CheckBox();
            this.codeButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.flipCheckbox = new System.Windows.Forms.CheckBox();
            this.showBoxesCheckbox = new System.Windows.Forms.CheckBox();
            this.applyPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.zoomPicker = new System.Windows.Forms.NumericUpDown();
            this.zoomLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.leftMostPanel = new System.Windows.Forms.Panel();
            this.enemiesListBox = new System.Windows.Forms.ListBox();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightMostPanel = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.framesListBox = new System.Windows.Forms.ListBox();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxPanel = new System.Windows.Forms.Panel();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.pictureBoxPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(7, 62);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(45, 13);
            this.palettesLabel.TabIndex = 29;
            this.palettesLabel.Text = "Palettes";
            // 
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(75, 59);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.ReadOnly = true;
            this.palettesTextBox.Size = new System.Drawing.Size(478, 20);
            this.palettesTextBox.TabIndex = 28;
            // 
            // specLabel
            // 
            this.specLabel.AutoSize = true;
            this.specLabel.Location = new System.Drawing.Point(7, 14);
            this.specLabel.Name = "specLabel";
            this.specLabel.Size = new System.Drawing.Size(32, 13);
            this.specLabel.TabIndex = 27;
            this.specLabel.Text = "Spec";
            // 
            // specTextBox
            // 
            this.specTextBox.Location = new System.Drawing.Point(75, 11);
            this.specTextBox.Name = "specTextBox";
            this.specTextBox.ReadOnly = true;
            this.specTextBox.Size = new System.Drawing.Size(478, 20);
            this.specTextBox.TabIndex = 26;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.bulletsTextBox);
            this.topPanel.Controls.Add(this.bulletsLabel);
            this.topPanel.Controls.Add(this.showBulletCheckBox);
            this.topPanel.Controls.Add(this.codeButton);
            this.topPanel.Controls.Add(this.startButton);
            this.topPanel.Controls.Add(this.stopButton);
            this.topPanel.Controls.Add(this.flipCheckbox);
            this.topPanel.Controls.Add(this.showBoxesCheckbox);
            this.topPanel.Controls.Add(this.applyPaletteCheckbox);
            this.topPanel.Controls.Add(this.zoomPicker);
            this.topPanel.Controls.Add(this.zoomLabel);
            this.topPanel.Controls.Add(this.loadButton);
            this.topPanel.Controls.Add(this.specLabel);
            this.topPanel.Controls.Add(this.specTextBox);
            this.topPanel.Controls.Add(this.palettesTextBox);
            this.topPanel.Controls.Add(this.palettesLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(562, 164);
            this.topPanel.TabIndex = 34;
            // 
            // bulletsTextBox
            // 
            this.bulletsTextBox.Location = new System.Drawing.Point(75, 35);
            this.bulletsTextBox.Name = "bulletsTextBox";
            this.bulletsTextBox.ReadOnly = true;
            this.bulletsTextBox.Size = new System.Drawing.Size(478, 20);
            this.bulletsTextBox.TabIndex = 48;
            // 
            // bulletsLabel
            // 
            this.bulletsLabel.AutoSize = true;
            this.bulletsLabel.Location = new System.Drawing.Point(7, 38);
            this.bulletsLabel.Name = "bulletsLabel";
            this.bulletsLabel.Size = new System.Drawing.Size(38, 13);
            this.bulletsLabel.TabIndex = 47;
            this.bulletsLabel.Text = "Bullets";
            // 
            // showBulletCheckBox
            // 
            this.showBulletCheckBox.AutoSize = true;
            this.showBulletCheckBox.Location = new System.Drawing.Point(306, 123);
            this.showBulletCheckBox.Name = "showBulletCheckBox";
            this.showBulletCheckBox.Size = new System.Drawing.Size(82, 17);
            this.showBulletCheckBox.TabIndex = 46;
            this.showBulletCheckBox.Text = "Show Bullet";
            this.showBulletCheckBox.UseVisualStyleBackColor = true;
            this.showBulletCheckBox.CheckedChanged += new System.EventHandler(this.InputCheckboxCheckedChanged);
            // 
            // codeButton
            // 
            this.codeButton.Location = new System.Drawing.Point(253, 89);
            this.codeButton.Name = "codeButton";
            this.codeButton.Size = new System.Drawing.Size(75, 23);
            this.codeButton.TabIndex = 45;
            this.codeButton.Text = "Code";
            this.codeButton.UseVisualStyleBackColor = true;
            this.codeButton.Click += new System.EventHandler(this.CodeButtonClick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(91, 88);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 42;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(172, 88);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 41;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // flipCheckbox
            // 
            this.flipCheckbox.AutoSize = true;
            this.flipCheckbox.Location = new System.Drawing.Point(397, 124);
            this.flipCheckbox.Name = "flipCheckbox";
            this.flipCheckbox.Size = new System.Drawing.Size(42, 17);
            this.flipCheckbox.TabIndex = 39;
            this.flipCheckbox.Text = "Flip";
            this.flipCheckbox.UseVisualStyleBackColor = true;
            this.flipCheckbox.CheckedChanged += new System.EventHandler(this.InputCheckboxCheckedChanged);
            // 
            // showBoxesCheckbox
            // 
            this.showBoxesCheckbox.AutoSize = true;
            this.showBoxesCheckbox.Location = new System.Drawing.Point(215, 123);
            this.showBoxesCheckbox.Name = "showBoxesCheckbox";
            this.showBoxesCheckbox.Size = new System.Drawing.Size(85, 17);
            this.showBoxesCheckbox.TabIndex = 38;
            this.showBoxesCheckbox.Text = "Show Boxes";
            this.showBoxesCheckbox.UseVisualStyleBackColor = true;
            this.showBoxesCheckbox.CheckedChanged += new System.EventHandler(this.InputCheckboxCheckedChanged);
            // 
            // applyPaletteCheckbox
            // 
            this.applyPaletteCheckbox.AutoSize = true;
            this.applyPaletteCheckbox.Checked = true;
            this.applyPaletteCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPaletteCheckbox.Location = new System.Drawing.Point(122, 123);
            this.applyPaletteCheckbox.Name = "applyPaletteCheckbox";
            this.applyPaletteCheckbox.Size = new System.Drawing.Size(88, 17);
            this.applyPaletteCheckbox.TabIndex = 37;
            this.applyPaletteCheckbox.Text = "Apply Palette";
            this.applyPaletteCheckbox.UseVisualStyleBackColor = true;
            this.applyPaletteCheckbox.CheckedChanged += new System.EventHandler(this.InputCheckboxCheckedChanged);
            // 
            // zoomPicker
            // 
            this.zoomPicker.Location = new System.Drawing.Point(60, 122);
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
            this.zoomPicker.TabIndex = 36;
            this.zoomPicker.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.zoomPicker.ValueChanged += new System.EventHandler(this.ZoomPickerValueChanged);
            // 
            // zoomLabel
            // 
            this.zoomLabel.AutoSize = true;
            this.zoomLabel.Location = new System.Drawing.Point(7, 124);
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(34, 13);
            this.zoomLabel.TabIndex = 35;
            this.zoomLabel.Text = "Zoom";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(10, 88);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 34;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 454);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(562, 10);
            this.bottomPanel.TabIndex = 35;
            // 
            // leftMostPanel
            // 
            this.leftMostPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftMostPanel.Location = new System.Drawing.Point(0, 164);
            this.leftMostPanel.Name = "leftMostPanel";
            this.leftMostPanel.Size = new System.Drawing.Size(10, 290);
            this.leftMostPanel.TabIndex = 36;
            // 
            // enemiesListBox
            // 
            this.enemiesListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.enemiesListBox.FormattingEnabled = true;
            this.enemiesListBox.Location = new System.Drawing.Point(10, 164);
            this.enemiesListBox.Name = "enemiesListBox";
            this.enemiesListBox.Size = new System.Drawing.Size(112, 290);
            this.enemiesListBox.TabIndex = 11;
            this.enemiesListBox.SelectedIndexChanged += new System.EventHandler(this.EnemiesListBoxSelectedIndexChanged);
            // 
            // leftPanel
            // 
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(122, 164);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(10, 290);
            this.leftPanel.TabIndex = 37;
            // 
            // rightMostPanel
            // 
            this.rightMostPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightMostPanel.Location = new System.Drawing.Point(552, 164);
            this.rightMostPanel.Name = "rightMostPanel";
            this.rightMostPanel.Size = new System.Drawing.Size(10, 290);
            this.rightMostPanel.TabIndex = 38;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(298, 290);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 39;
            this.pictureBox.TabStop = false;
            // 
            // framesListBox
            // 
            this.framesListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.framesListBox.FormattingEnabled = true;
            this.framesListBox.Location = new System.Drawing.Point(132, 164);
            this.framesListBox.Name = "framesListBox";
            this.framesListBox.Size = new System.Drawing.Size(112, 290);
            this.framesListBox.TabIndex = 41;
            this.framesListBox.SelectedIndexChanged += new System.EventHandler(this.FramesListBoxSelectedIndexChanged);
            // 
            // centerPanel
            // 
            this.centerPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.centerPanel.Location = new System.Drawing.Point(244, 164);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(10, 290);
            this.centerPanel.TabIndex = 42;
            // 
            // timer
            // 
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // pictureBoxPanel
            // 
            this.pictureBoxPanel.Controls.Add(this.pictureBox);
            this.pictureBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPanel.Location = new System.Drawing.Point(254, 164);
            this.pictureBoxPanel.Name = "pictureBoxPanel";
            this.pictureBoxPanel.Size = new System.Drawing.Size(298, 290);
            this.pictureBoxPanel.TabIndex = 43;
            // 
            // EnemiesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 464);
            this.Controls.Add(this.pictureBoxPanel);
            this.Controls.Add(this.centerPanel);
            this.Controls.Add(this.framesListBox);
            this.Controls.Add(this.rightMostPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.enemiesListBox);
            this.Controls.Add(this.leftMostPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "EnemiesWindow";
            this.Text = "Enemies";
            this.Load += new System.EventHandler(this.EnemiesWindowLoad);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.pictureBoxPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.TextBox specTextBox;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel leftMostPanel;
        private System.Windows.Forms.ListBox enemiesListBox;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightMostPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ListBox framesListBox;
        private System.Windows.Forms.Panel centerPanel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.NumericUpDown zoomPicker;
        private System.Windows.Forms.Label zoomLabel;
        private System.Windows.Forms.CheckBox applyPaletteCheckbox;
        private System.Windows.Forms.CheckBox flipCheckbox;
        private System.Windows.Forms.CheckBox showBoxesCheckbox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button codeButton;
        private System.Windows.Forms.Panel pictureBoxPanel;
        private System.Windows.Forms.CheckBox showBulletCheckBox;
        private System.Windows.Forms.TextBox bulletsTextBox;
        private System.Windows.Forms.Label bulletsLabel;
    }
}