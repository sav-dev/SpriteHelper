namespace SpriteHelper
{
    partial class LevelEditor
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
            this.splitContainerVertical = new System.Windows.Forms.SplitContainer();
            this.applyPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.zoomPicker = new System.Windows.Forms.NumericUpDown();
            this.zoomLabel = new System.Windows.Forms.Label();
            this.bgColorPanel = new System.Windows.Forms.Panel();
            this.bgColorLabel = new System.Windows.Forms.Label();
            this.bgColorTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.levelLabel = new System.Windows.Forms.Label();
            this.levelTextBox = new System.Windows.Forms.TextBox();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.specLabel = new System.Windows.Forms.Label();
            this.specTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).BeginInit();
            this.splitContainerVertical.Panel2.SuspendLayout();
            this.splitContainerVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerVertical
            // 
            this.splitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerVertical.IsSplitterFixed = true;
            this.splitContainerVertical.Location = new System.Drawing.Point(0, 0);
            this.splitContainerVertical.Name = "splitContainerVertical";
            // 
            // splitContainerVertical.Panel2
            // 
            this.splitContainerVertical.Panel2.Controls.Add(this.applyPaletteCheckbox);
            this.splitContainerVertical.Panel2.Controls.Add(this.zoomPicker);
            this.splitContainerVertical.Panel2.Controls.Add(this.zoomLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.bgColorPanel);
            this.splitContainerVertical.Panel2.Controls.Add(this.bgColorLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.bgColorTextBox);
            this.splitContainerVertical.Panel2.Controls.Add(this.saveButton);
            this.splitContainerVertical.Panel2.Controls.Add(this.loadButton);
            this.splitContainerVertical.Panel2.Controls.Add(this.levelLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.levelTextBox);
            this.splitContainerVertical.Panel2.Controls.Add(this.palettesLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.palettesTextBox);
            this.splitContainerVertical.Panel2.Controls.Add(this.specLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.specTextBox);
            this.splitContainerVertical.Size = new System.Drawing.Size(1040, 519);
            this.splitContainerVertical.SplitterDistance = 536;
            this.splitContainerVertical.TabIndex = 0;
            // 
            // applyPaletteCheckbox
            // 
            this.applyPaletteCheckbox.AutoSize = true;
            this.applyPaletteCheckbox.Checked = true;
            this.applyPaletteCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPaletteCheckbox.Location = new System.Drawing.Point(309, 92);
            this.applyPaletteCheckbox.Name = "applyPaletteCheckbox";
            this.applyPaletteCheckbox.Size = new System.Drawing.Size(88, 17);
            this.applyPaletteCheckbox.TabIndex = 29;
            this.applyPaletteCheckbox.Text = "Apply Palette";
            this.applyPaletteCheckbox.UseVisualStyleBackColor = true;
            this.applyPaletteCheckbox.CheckedChanged += new System.EventHandler(this.ApplyPaletteCheckboxCheckedChanged);
            // 
            // zoomPicker
            // 
            this.zoomPicker.Location = new System.Drawing.Point(248, 90);
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
            this.zoomPicker.TabIndex = 28;
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
            this.zoomLabel.Location = new System.Drawing.Point(208, 93);
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(34, 13);
            this.zoomLabel.TabIndex = 27;
            this.zoomLabel.Text = "Zoom";
            // 
            // bgColorPanel
            // 
            this.bgColorPanel.Location = new System.Drawing.Point(182, 90);
            this.bgColorPanel.Name = "bgColorPanel";
            this.bgColorPanel.Size = new System.Drawing.Size(20, 20);
            this.bgColorPanel.TabIndex = 26;
            // 
            // bgColorLabel
            // 
            this.bgColorLabel.AutoSize = true;
            this.bgColorLabel.Location = new System.Drawing.Point(13, 93);
            this.bgColorLabel.Name = "bgColorLabel";
            this.bgColorLabel.Size = new System.Drawing.Size(49, 13);
            this.bgColorLabel.TabIndex = 25;
            this.bgColorLabel.Text = "BG Color";
            // 
            // bgColorTextBox
            // 
            this.bgColorTextBox.Location = new System.Drawing.Point(68, 90);
            this.bgColorTextBox.Name = "bgColorTextBox";
            this.bgColorTextBox.Size = new System.Drawing.Size(105, 20);
            this.bgColorTextBox.TabIndex = 24;
            this.bgColorTextBox.TextChanged += new System.EventHandler(this.BgColorTextBoxTextChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(405, 90);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(34, 20);
            this.saveButton.TabIndex = 23;
            this.saveButton.Text = "S";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(448, 90);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(34, 20);
            this.loadButton.TabIndex = 22;
            this.loadButton.Text = "L";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(13, 15);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(33, 13);
            this.levelLabel.TabIndex = 21;
            this.levelLabel.Text = "Level";
            // 
            // levelTextBox
            // 
            this.levelTextBox.Location = new System.Drawing.Point(68, 12);
            this.levelTextBox.Name = "levelTextBox";
            this.levelTextBox.Size = new System.Drawing.Size(414, 20);
            this.levelTextBox.TabIndex = 20;
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(13, 67);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(45, 13);
            this.palettesLabel.TabIndex = 19;
            this.palettesLabel.Text = "Palettes";
            // 
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(68, 64);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.Size = new System.Drawing.Size(414, 20);
            this.palettesTextBox.TabIndex = 18;
            // 
            // specLabel
            // 
            this.specLabel.AutoSize = true;
            this.specLabel.Location = new System.Drawing.Point(13, 41);
            this.specLabel.Name = "specLabel";
            this.specLabel.Size = new System.Drawing.Size(32, 13);
            this.specLabel.TabIndex = 17;
            this.specLabel.Text = "Spec";
            // 
            // specTextBox
            // 
            this.specTextBox.Location = new System.Drawing.Point(68, 38);
            this.specTextBox.Name = "specTextBox";
            this.specTextBox.Size = new System.Drawing.Size(414, 20);
            this.specTextBox.TabIndex = 16;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 519);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1040, 22);
            this.statusStrip.TabIndex = 20;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 541);
            this.Controls.Add(this.splitContainerVertical);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "LevelEditor";
            this.Text = "Level editor";
            this.Load += new System.EventHandler(this.LevelEditorLoad);
            this.splitContainerVertical.Panel2.ResumeLayout(false);
            this.splitContainerVertical.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).EndInit();
            this.splitContainerVertical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerVertical;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.TextBox specTextBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.TextBox levelTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Panel bgColorPanel;
        private System.Windows.Forms.Label bgColorLabel;
        private System.Windows.Forms.TextBox bgColorTextBox;
        private System.Windows.Forms.CheckBox applyPaletteCheckbox;
        private System.Windows.Forms.NumericUpDown zoomPicker;
        private System.Windows.Forms.Label zoomLabel;
    }
}