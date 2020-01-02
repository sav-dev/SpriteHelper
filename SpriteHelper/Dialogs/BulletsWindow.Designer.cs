namespace SpriteHelper.Dialogs
{
    partial class BulletsWindow
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.codeButton = new System.Windows.Forms.Button();
            this.flipCheckbox = new System.Windows.Forms.CheckBox();
            this.showBoxesCheckbox = new System.Windows.Forms.CheckBox();
            this.applyPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.zoomPicker = new System.Windows.Forms.NumericUpDown();
            this.zoomLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.specLabel = new System.Windows.Forms.Label();
            this.specTextBox = new System.Windows.Forms.TextBox();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.leftMostPanel = new System.Windows.Forms.Panel();
            this.bulletsListBox = new System.Windows.Forms.ListBox();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.codeButton);
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
            this.topPanel.Size = new System.Drawing.Size(566, 130);
            this.topPanel.TabIndex = 35;
            // 
            // codeButton
            // 
            this.codeButton.Location = new System.Drawing.Point(91, 65);
            this.codeButton.Name = "codeButton";
            this.codeButton.Size = new System.Drawing.Size(75, 23);
            this.codeButton.TabIndex = 45;
            this.codeButton.Text = "Code";
            this.codeButton.UseVisualStyleBackColor = true;
            this.codeButton.Click += new System.EventHandler(this.CodeButtonClick);
            // 
            // flipCheckbox
            // 
            this.flipCheckbox.AutoSize = true;
            this.flipCheckbox.Location = new System.Drawing.Point(308, 100);
            this.flipCheckbox.Name = "flipCheckbox";
            this.flipCheckbox.Size = new System.Drawing.Size(42, 17);
            this.flipCheckbox.TabIndex = 39;
            this.flipCheckbox.Text = "Flip";
            this.flipCheckbox.UseVisualStyleBackColor = true;
            this.flipCheckbox.CheckedChanged += new System.EventHandler(this.FlipCheckboxCheckedChanged);
            // 
            // showBoxesCheckbox
            // 
            this.showBoxesCheckbox.AutoSize = true;
            this.showBoxesCheckbox.Location = new System.Drawing.Point(215, 100);
            this.showBoxesCheckbox.Name = "showBoxesCheckbox";
            this.showBoxesCheckbox.Size = new System.Drawing.Size(85, 17);
            this.showBoxesCheckbox.TabIndex = 38;
            this.showBoxesCheckbox.Text = "Show Boxes";
            this.showBoxesCheckbox.UseVisualStyleBackColor = true;
            this.showBoxesCheckbox.CheckedChanged += new System.EventHandler(this.ShowBoxesCheckboxCheckedChanged);
            // 
            // applyPaletteCheckbox
            // 
            this.applyPaletteCheckbox.AutoSize = true;
            this.applyPaletteCheckbox.Checked = true;
            this.applyPaletteCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPaletteCheckbox.Location = new System.Drawing.Point(122, 100);
            this.applyPaletteCheckbox.Name = "applyPaletteCheckbox";
            this.applyPaletteCheckbox.Size = new System.Drawing.Size(88, 17);
            this.applyPaletteCheckbox.TabIndex = 37;
            this.applyPaletteCheckbox.Text = "Apply Palette";
            this.applyPaletteCheckbox.UseVisualStyleBackColor = true;
            this.applyPaletteCheckbox.CheckedChanged += new System.EventHandler(this.ApplyPaletteCheckboxCheckedChanged);
            // 
            // zoomPicker
            // 
            this.zoomPicker.Location = new System.Drawing.Point(60, 99);
            this.zoomPicker.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.zoomPicker.Minimum = new decimal(new int[] {
            3,
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
            this.zoomLabel.Location = new System.Drawing.Point(7, 101);
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(34, 13);
            this.zoomLabel.TabIndex = 35;
            this.zoomLabel.Text = "Zoom";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(10, 65);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 34;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClick);
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
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(75, 37);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.ReadOnly = true;
            this.palettesTextBox.Size = new System.Drawing.Size(478, 20);
            this.palettesTextBox.TabIndex = 28;
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(7, 40);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(45, 13);
            this.palettesLabel.TabIndex = 29;
            this.palettesLabel.Text = "Palettes";
            // 
            // leftMostPanel
            // 
            this.leftMostPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftMostPanel.Location = new System.Drawing.Point(0, 130);
            this.leftMostPanel.Name = "leftMostPanel";
            this.leftMostPanel.Size = new System.Drawing.Size(10, 243);
            this.leftMostPanel.TabIndex = 37;
            // 
            // bulletsListBox
            // 
            this.bulletsListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.bulletsListBox.FormattingEnabled = true;
            this.bulletsListBox.Location = new System.Drawing.Point(10, 130);
            this.bulletsListBox.Name = "bulletsListBox";
            this.bulletsListBox.Size = new System.Drawing.Size(112, 243);
            this.bulletsListBox.TabIndex = 38;
            this.bulletsListBox.SelectedIndexChanged += new System.EventHandler(this.BulletsListBoxSelectedIndexChanged);
            // 
            // leftPanel
            // 
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(122, 130);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(10, 243);
            this.leftPanel.TabIndex = 39;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(132, 130);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(434, 243);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 40;
            this.pictureBox.TabStop = false;
            // 
            // BulletsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 373);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.bulletsListBox);
            this.Controls.Add(this.leftMostPanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "BulletsWindow";
            this.Text = "BulletsWindow";
            this.Load += new System.EventHandler(this.BulletsWindowLoad);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.CheckBox flipCheckbox;
        private System.Windows.Forms.CheckBox showBoxesCheckbox;
        private System.Windows.Forms.CheckBox applyPaletteCheckbox;
        private System.Windows.Forms.NumericUpDown zoomPicker;
        private System.Windows.Forms.Label zoomLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.TextBox specTextBox;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.Button codeButton;
        private System.Windows.Forms.Panel leftMostPanel;
        private System.Windows.Forms.ListBox bulletsListBox;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}