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
            this.outerOuterDrawPanel = new System.Windows.Forms.Panel();
            this.outerDrawPanel = new System.Windows.Forms.Panel();
            this.drawPanel = new System.Windows.Forms.Panel();
            this.scrollBar = new System.Windows.Forms.HScrollBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageBlocking = new System.Windows.Forms.TabPage();
            this.listViewBlocking = new System.Windows.Forms.ListView();
            this.tabPageNonBlocking = new System.Windows.Forms.TabPage();
            this.listViewNonBlocking = new System.Windows.Forms.ListView();
            this.tabPageThreat = new System.Windows.Forms.TabPage();
            this.listViewThreat = new System.Windows.Forms.ListView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).BeginInit();
            this.splitContainerVertical.Panel1.SuspendLayout();
            this.splitContainerVertical.Panel2.SuspendLayout();
            this.splitContainerVertical.SuspendLayout();
            this.outerOuterDrawPanel.SuspendLayout();
            this.outerDrawPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageBlocking.SuspendLayout();
            this.tabPageNonBlocking.SuspendLayout();
            this.tabPageThreat.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerVertical
            // 
            this.splitContainerVertical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerVertical.IsSplitterFixed = true;
            this.splitContainerVertical.Location = new System.Drawing.Point(0, 24);
            this.splitContainerVertical.Name = "splitContainerVertical";
            // 
            // splitContainerVertical.Panel1
            // 
            this.splitContainerVertical.Panel1.Controls.Add(this.outerOuterDrawPanel);
            this.splitContainerVertical.Panel1.Controls.Add(this.scrollBar);
            // 
            // splitContainerVertical.Panel2
            // 
            this.splitContainerVertical.Panel2.Controls.Add(this.tabControl);
            this.splitContainerVertical.Size = new System.Drawing.Size(1040, 495);
            this.splitContainerVertical.SplitterDistance = 570;
            this.splitContainerVertical.TabIndex = 999;
            // 
            // outerOuterDrawPanel
            // 
            this.outerOuterDrawPanel.Controls.Add(this.outerDrawPanel);
            this.outerOuterDrawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outerOuterDrawPanel.Location = new System.Drawing.Point(0, 0);
            this.outerOuterDrawPanel.Name = "outerOuterDrawPanel";
            this.outerOuterDrawPanel.Size = new System.Drawing.Size(568, 476);
            this.outerOuterDrawPanel.TabIndex = 2;
            // 
            // outerDrawPanel
            // 
            this.outerDrawPanel.Controls.Add(this.drawPanel);
            this.outerDrawPanel.Cursor = System.Windows.Forms.Cursors.Cross;
            this.outerDrawPanel.Location = new System.Drawing.Point(49, 53);
            this.outerDrawPanel.Name = "outerDrawPanel";
            this.outerDrawPanel.Size = new System.Drawing.Size(200, 100);
            this.outerDrawPanel.TabIndex = 1;
            // 
            // drawPanel
            // 
            this.drawPanel.Location = new System.Drawing.Point(0, 0);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(100, 50);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawPanelMouseClick);
            this.drawPanel.MouseLeave += new System.EventHandler(this.DrawPanelMouseLeave);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPanelMouseMove);
            // 
            // scrollBar
            // 
            this.scrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scrollBar.LargeChange = 1;
            this.scrollBar.Location = new System.Drawing.Point(0, 476);
            this.scrollBar.Name = "scrollBar";
            this.scrollBar.Size = new System.Drawing.Size(568, 17);
            this.scrollBar.TabIndex = 0;
            this.scrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBarScroll);
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
            this.tabControl.Location = new System.Drawing.Point(19, 20);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(430, 458);
            this.tabControl.TabIndex = 46;
            // 
            // tabPageBlocking
            // 
            this.tabPageBlocking.Controls.Add(this.listViewBlocking);
            this.tabPageBlocking.Location = new System.Drawing.Point(4, 4);
            this.tabPageBlocking.Name = "tabPageBlocking";
            this.tabPageBlocking.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBlocking.Size = new System.Drawing.Size(422, 432);
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
            this.listViewBlocking.Size = new System.Drawing.Size(416, 426);
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
            this.tabPageNonBlocking.Size = new System.Drawing.Size(422, 432);
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
            this.listViewNonBlocking.Size = new System.Drawing.Size(416, 426);
            this.listViewNonBlocking.TabIndex = 49;
            this.listViewNonBlocking.TileSize = new System.Drawing.Size(30, 30);
            this.listViewNonBlocking.UseCompatibleStateImageBehavior = false;
            // 
            // tabPageThreat
            // 
            this.tabPageThreat.Controls.Add(this.listViewThreat);
            this.tabPageThreat.Location = new System.Drawing.Point(4, 4);
            this.tabPageThreat.Name = "tabPageThreat";
            this.tabPageThreat.Size = new System.Drawing.Size(422, 432);
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
            this.listViewThreat.Size = new System.Drawing.Size(422, 432);
            this.listViewThreat.TabIndex = 50;
            this.listViewThreat.TileSize = new System.Drawing.Size(30, 30);
            this.listViewThreat.UseCompatibleStateImageBehavior = false;
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
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1040, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItemClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.advancedToolStripMenuItem.Text = "&Advanced";
            this.advancedToolStripMenuItem.Click += new System.EventHandler(this.AdvancedToolStripMenuItemClick);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItemClick);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTypeToolStripMenuItem,
            this.showGridToolStripMenuItem,
            this.applyPaletteToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // showTypeToolStripMenuItem
            // 
            this.showTypeToolStripMenuItem.CheckOnClick = true;
            this.showTypeToolStripMenuItem.Name = "showTypeToolStripMenuItem";
            this.showTypeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.showTypeToolStripMenuItem.Text = "Show &Type";
            this.showTypeToolStripMenuItem.Click += new System.EventHandler(this.ShowTypeToolStripMenuItemClick);
            // 
            // showGridToolStripMenuItem
            // 
            this.showGridToolStripMenuItem.Checked = true;
            this.showGridToolStripMenuItem.CheckOnClick = true;
            this.showGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGridToolStripMenuItem.Name = "showGridToolStripMenuItem";
            this.showGridToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.showGridToolStripMenuItem.Text = "Show &Grid";
            this.showGridToolStripMenuItem.Click += new System.EventHandler(this.ShowGridToolStripMenuItemClick);
            // 
            // applyPaletteToolStripMenuItem
            // 
            this.applyPaletteToolStripMenuItem.Checked = true;
            this.applyPaletteToolStripMenuItem.CheckOnClick = true;
            this.applyPaletteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.applyPaletteToolStripMenuItem.Name = "applyPaletteToolStripMenuItem";
            this.applyPaletteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.applyPaletteToolStripMenuItem.Text = "Apply &Palette";
            this.applyPaletteToolStripMenuItem.Click += new System.EventHandler(this.ApplyPaletteToolStripMenuItemClick);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 541);
            this.Controls.Add(this.splitContainerVertical);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(16, 580);
            this.Name = "LevelEditor";
            this.Text = "Level editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LevelEditorLoad);
            this.Resize += new System.EventHandler(this.LevelEditorResize);
            this.splitContainerVertical.Panel1.ResumeLayout(false);
            this.splitContainerVertical.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).EndInit();
            this.splitContainerVertical.ResumeLayout(false);
            this.outerOuterDrawPanel.ResumeLayout(false);
            this.outerDrawPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageBlocking.ResumeLayout(false);
            this.tabPageNonBlocking.ResumeLayout(false);
            this.tabPageThreat.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerVertical;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBlocking;
        private System.Windows.Forms.TabPage tabPageNonBlocking;
        private System.Windows.Forms.TabPage tabPageThreat;
        private System.Windows.Forms.ListView listViewThreat;
        private System.Windows.Forms.ListView listViewBlocking;
        private System.Windows.Forms.ListView listViewNonBlocking;
        private System.Windows.Forms.Panel outerDrawPanel;
        private System.Windows.Forms.HScrollBar scrollBar;
        private System.Windows.Forms.Panel outerOuterDrawPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.Panel drawPanel;
    }
}