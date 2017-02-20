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
            this.editButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageBlocking = new System.Windows.Forms.TabPage();
            this.listViewBlocking = new System.Windows.Forms.ListView();
            this.tabPageNonBlocking = new System.Windows.Forms.TabPage();
            this.listViewNonBlocking = new System.Windows.Forms.ListView();
            this.tabPageThreat = new System.Windows.Forms.TabPage();
            this.listViewThreat = new System.Windows.Forms.ListView();
            this.showTypeCheckbox = new System.Windows.Forms.CheckBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.levelLabel = new System.Windows.Forms.Label();
            this.specTextBox = new System.Windows.Forms.TextBox();
            this.applyPaletteCheckbox = new System.Windows.Forms.CheckBox();
            this.specLabel = new System.Windows.Forms.Label();
            this.zoomPicker = new System.Windows.Forms.NumericUpDown();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.zoomLabel = new System.Windows.Forms.Label();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.bgColorPanel = new System.Windows.Forms.Panel();
            this.levelTextBox = new System.Windows.Forms.TextBox();
            this.bgColorLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.bgColorTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.showGridCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).BeginInit();
            this.splitContainerVertical.Panel2.SuspendLayout();
            this.splitContainerVertical.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageBlocking.SuspendLayout();
            this.tabPageNonBlocking.SuspendLayout();
            this.tabPageThreat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerVertical
            // 
            this.splitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerVertical.IsSplitterFixed = true;
            this.splitContainerVertical.Location = new System.Drawing.Point(0, 0);
            this.splitContainerVertical.Name = "splitContainerVertical";
            // 
            // splitContainerVertical.Panel2
            // 
            this.splitContainerVertical.Panel2.Controls.Add(this.showGridCheckbox);
            this.splitContainerVertical.Panel2.Controls.Add(this.editButton);
            this.splitContainerVertical.Panel2.Controls.Add(this.tabControl);
            this.splitContainerVertical.Panel2.Controls.Add(this.showTypeCheckbox);
            this.splitContainerVertical.Panel2.Controls.Add(this.exportButton);
            this.splitContainerVertical.Panel2.Controls.Add(this.levelLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.specTextBox);
            this.splitContainerVertical.Panel2.Controls.Add(this.applyPaletteCheckbox);
            this.splitContainerVertical.Panel2.Controls.Add(this.specLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.zoomPicker);
            this.splitContainerVertical.Panel2.Controls.Add(this.palettesTextBox);
            this.splitContainerVertical.Panel2.Controls.Add(this.zoomLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.palettesLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.bgColorPanel);
            this.splitContainerVertical.Panel2.Controls.Add(this.levelTextBox);
            this.splitContainerVertical.Panel2.Controls.Add(this.bgColorLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.loadButton);
            this.splitContainerVertical.Panel2.Controls.Add(this.bgColorTextBox);
            this.splitContainerVertical.Panel2.Controls.Add(this.saveButton);
            this.splitContainerVertical.Size = new System.Drawing.Size(1040, 519);
            this.splitContainerVertical.SplitterDistance = 536;
            this.splitContainerVertical.TabIndex = 999;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(405, 109);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(80, 23);
            this.editButton.TabIndex = 47;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.EditButtonClick);
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageBlocking);
            this.tabControl.Controls.Add(this.tabPageNonBlocking);
            this.tabControl.Controls.Add(this.tabPageThreat);
            this.tabControl.Location = new System.Drawing.Point(19, 144);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(466, 360);
            this.tabControl.TabIndex = 46;
            // 
            // tabPageBlocking
            // 
            this.tabPageBlocking.Controls.Add(this.listViewBlocking);
            this.tabPageBlocking.Location = new System.Drawing.Point(4, 4);
            this.tabPageBlocking.Name = "tabPageBlocking";
            this.tabPageBlocking.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBlocking.Size = new System.Drawing.Size(458, 334);
            this.tabPageBlocking.TabIndex = 0;
            this.tabPageBlocking.Text = "Blocking";
            this.tabPageBlocking.UseVisualStyleBackColor = true;
            // 
            // listViewBlocking
            // 
            this.listViewBlocking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewBlocking.Location = new System.Drawing.Point(3, 3);
            this.listViewBlocking.MultiSelect = false;
            this.listViewBlocking.Name = "listViewBlocking";
            this.listViewBlocking.Size = new System.Drawing.Size(452, 328);
            this.listViewBlocking.TabIndex = 48;
            this.listViewBlocking.TileSize = new System.Drawing.Size(30, 30);
            this.listViewBlocking.UseCompatibleStateImageBehavior = false;
            // 
            // tabPageNonBlocking
            // 
            this.tabPageNonBlocking.Controls.Add(this.listViewNonBlocking);
            this.tabPageNonBlocking.Location = new System.Drawing.Point(4, 4);
            this.tabPageNonBlocking.Name = "tabPageNonBlocking";
            this.tabPageNonBlocking.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNonBlocking.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabPageNonBlocking.Size = new System.Drawing.Size(458, 334);
            this.tabPageNonBlocking.TabIndex = 1;
            this.tabPageNonBlocking.Text = "Non-blocking";
            this.tabPageNonBlocking.UseVisualStyleBackColor = true;
            // 
            // listViewNonBlocking
            // 
            this.listViewNonBlocking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewNonBlocking.Location = new System.Drawing.Point(3, 3);
            this.listViewNonBlocking.MultiSelect = false;
            this.listViewNonBlocking.Name = "listViewNonBlocking";
            this.listViewNonBlocking.Size = new System.Drawing.Size(452, 328);
            this.listViewNonBlocking.TabIndex = 49;
            this.listViewNonBlocking.TileSize = new System.Drawing.Size(30, 30);
            this.listViewNonBlocking.UseCompatibleStateImageBehavior = false;
            // 
            // tabPageThreat
            // 
            this.tabPageThreat.Controls.Add(this.listViewThreat);
            this.tabPageThreat.Location = new System.Drawing.Point(4, 4);
            this.tabPageThreat.Name = "tabPageThreat";
            this.tabPageThreat.Size = new System.Drawing.Size(458, 334);
            this.tabPageThreat.TabIndex = 2;
            this.tabPageThreat.Text = "Threat";
            this.tabPageThreat.UseVisualStyleBackColor = true;
            // 
            // listViewThreat
            // 
            this.listViewThreat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewThreat.Location = new System.Drawing.Point(0, 0);
            this.listViewThreat.MultiSelect = false;
            this.listViewThreat.Name = "listViewThreat";
            this.listViewThreat.Size = new System.Drawing.Size(458, 334);
            this.listViewThreat.TabIndex = 50;
            this.listViewThreat.TileSize = new System.Drawing.Size(30, 30);
            this.listViewThreat.UseCompatibleStateImageBehavior = false;
            // 
            // showTypeCheckbox
            // 
            this.showTypeCheckbox.AutoSize = true;
            this.showTypeCheckbox.Checked = true;
            this.showTypeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showTypeCheckbox.Location = new System.Drawing.Point(125, 113);
            this.showTypeCheckbox.Name = "showTypeCheckbox";
            this.showTypeCheckbox.Size = new System.Drawing.Size(80, 17);
            this.showTypeCheckbox.TabIndex = 45;
            this.showTypeCheckbox.Text = "Show Type";
            this.showTypeCheckbox.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(405, 84);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(80, 23);
            this.exportButton.TabIndex = 44;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButtonClick);
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(16, 11);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(33, 13);
            this.levelLabel.TabIndex = 35;
            this.levelLabel.Text = "Level";
            // 
            // specTextBox
            // 
            this.specTextBox.Location = new System.Drawing.Point(71, 34);
            this.specTextBox.Name = "specTextBox";
            this.specTextBox.Size = new System.Drawing.Size(414, 20);
            this.specTextBox.TabIndex = 30;
            // 
            // applyPaletteCheckbox
            // 
            this.applyPaletteCheckbox.AutoSize = true;
            this.applyPaletteCheckbox.Checked = true;
            this.applyPaletteCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPaletteCheckbox.Location = new System.Drawing.Point(211, 113);
            this.applyPaletteCheckbox.Name = "applyPaletteCheckbox";
            this.applyPaletteCheckbox.Size = new System.Drawing.Size(88, 17);
            this.applyPaletteCheckbox.TabIndex = 43;
            this.applyPaletteCheckbox.Text = "Apply Palette";
            this.applyPaletteCheckbox.UseVisualStyleBackColor = true;
            this.applyPaletteCheckbox.CheckedChanged += new System.EventHandler(this.ApplyPaletteCheckboxCheckedChanged);
            // 
            // specLabel
            // 
            this.specLabel.AutoSize = true;
            this.specLabel.Location = new System.Drawing.Point(16, 37);
            this.specLabel.Name = "specLabel";
            this.specLabel.Size = new System.Drawing.Size(32, 13);
            this.specLabel.TabIndex = 31;
            this.specLabel.Text = "Spec";
            // 
            // zoomPicker
            // 
            this.zoomPicker.Location = new System.Drawing.Point(71, 112);
            this.zoomPicker.Maximum = new decimal(new int[] {
            2,
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
            this.zoomPicker.TabIndex = 42;
            this.zoomPicker.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.zoomPicker.ValueChanged += new System.EventHandler(this.ZoomPickerValueChanged);
            // 
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(71, 60);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.Size = new System.Drawing.Size(414, 20);
            this.palettesTextBox.TabIndex = 32;
            // 
            // zoomLabel
            // 
            this.zoomLabel.AutoSize = true;
            this.zoomLabel.Location = new System.Drawing.Point(16, 116);
            this.zoomLabel.Name = "zoomLabel";
            this.zoomLabel.Size = new System.Drawing.Size(34, 13);
            this.zoomLabel.TabIndex = 41;
            this.zoomLabel.Text = "Zoom";
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(16, 63);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(45, 13);
            this.palettesLabel.TabIndex = 33;
            this.palettesLabel.Text = "Palettes";
            // 
            // bgColorPanel
            // 
            this.bgColorPanel.Location = new System.Drawing.Point(155, 86);
            this.bgColorPanel.Name = "bgColorPanel";
            this.bgColorPanel.Size = new System.Drawing.Size(20, 20);
            this.bgColorPanel.TabIndex = 40;
            // 
            // levelTextBox
            // 
            this.levelTextBox.Location = new System.Drawing.Point(71, 8);
            this.levelTextBox.Name = "levelTextBox";
            this.levelTextBox.Size = new System.Drawing.Size(414, 20);
            this.levelTextBox.TabIndex = 34;
            // 
            // bgColorLabel
            // 
            this.bgColorLabel.AutoSize = true;
            this.bgColorLabel.Location = new System.Drawing.Point(16, 89);
            this.bgColorLabel.Name = "bgColorLabel";
            this.bgColorLabel.Size = new System.Drawing.Size(49, 13);
            this.bgColorLabel.TabIndex = 39;
            this.bgColorLabel.Text = "BG Color";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(319, 84);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(80, 23);
            this.loadButton.TabIndex = 36;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButtonClick);
            // 
            // bgColorTextBox
            // 
            this.bgColorTextBox.Location = new System.Drawing.Point(71, 86);
            this.bgColorTextBox.Name = "bgColorTextBox";
            this.bgColorTextBox.Size = new System.Drawing.Size(78, 20);
            this.bgColorTextBox.TabIndex = 38;
            this.bgColorTextBox.TextChanged += new System.EventHandler(this.BgColorTextBoxTextChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(233, 84);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(80, 23);
            this.saveButton.TabIndex = 37;
            this.saveButton.Text = "Save Level";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 519);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1040, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // showGridCheckbox
            // 
            this.showGridCheckbox.AutoSize = true;
            this.showGridCheckbox.Checked = true;
            this.showGridCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGridCheckbox.Location = new System.Drawing.Point(305, 113);
            this.showGridCheckbox.Name = "showGridCheckbox";
            this.showGridCheckbox.Size = new System.Drawing.Size(75, 17);
            this.showGridCheckbox.TabIndex = 48;
            this.showGridCheckbox.Text = "Show Grid";
            this.showGridCheckbox.UseVisualStyleBackColor = true;
            this.showGridCheckbox.CheckedChanged += new System.EventHandler(this.ShowGridCheckboxCheckedChanged);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 541);
            this.Controls.Add(this.splitContainerVertical);
            this.Controls.Add(this.statusStrip);
            this.Name = "LevelEditor";
            this.Text = "Level editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LevelEditorLoad);
            this.splitContainerVertical.Panel2.ResumeLayout(false);
            this.splitContainerVertical.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).EndInit();
            this.splitContainerVertical.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageBlocking.ResumeLayout(false);
            this.tabPageNonBlocking.ResumeLayout(false);
            this.tabPageThreat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicker)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerVertical;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.TextBox specTextBox;
        private System.Windows.Forms.CheckBox applyPaletteCheckbox;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.NumericUpDown zoomPicker;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Label zoomLabel;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.Panel bgColorPanel;
        private System.Windows.Forms.TextBox levelTextBox;
        private System.Windows.Forms.Label bgColorLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.TextBox bgColorTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.CheckBox showTypeCheckbox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBlocking;
        private System.Windows.Forms.TabPage tabPageNonBlocking;
        private System.Windows.Forms.TabPage tabPageThreat;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.ListView listViewThreat;
        private System.Windows.Forms.ListView listViewBlocking;
        private System.Windows.Forms.ListView listViewNonBlocking;
        private System.Windows.Forms.CheckBox showGridCheckbox;
    }
}