﻿namespace SpriteHelper.Dialogs
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
            this.scrollBar = new System.Windows.Forms.HScrollBar();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.bgTabPage = new System.Windows.Forms.TabPage();
            this.bgTabControl = new System.Windows.Forms.TabControl();
            this.blockingTilesTabPage = new System.Windows.Forms.TabPage();
            this.nonBlockingTilesTabPage = new System.Windows.Forms.TabPage();
            this.threatTilesTabPage = new System.Windows.Forms.TabPage();
            this.enTabPage = new System.Windows.Forms.TabPage();
            this.enemiesListBox = new System.Windows.Forms.ListBox();
            this.enTabBottomPanel = new System.Windows.Forms.Panel();
            this.editEnemyButton = new System.Windows.Forms.Button();
            this.deleteEnemyButton = new System.Windows.Forms.Button();
            this.addEnemyButton = new System.Windows.Forms.Button();
            this.uniqueTilesCountLabel = new System.Windows.Forms.Label();
            this.uniqueTilesLabel = new System.Windows.Forms.Label();
            this.selectedTilePictureBox = new System.Windows.Forms.PictureBox();
            this.selectedTileLabel = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.showEnemiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.viewPlatformsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewThreatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findEnemyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).BeginInit();
            this.splitContainerVertical.Panel1.SuspendLayout();
            this.splitContainerVertical.Panel2.SuspendLayout();
            this.splitContainerVertical.SuspendLayout();
            this.outerOuterDrawPanel.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.bgTabPage.SuspendLayout();
            this.bgTabControl.SuspendLayout();
            this.enTabPage.SuspendLayout();
            this.enTabBottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTilePictureBox)).BeginInit();
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
            this.splitContainerVertical.Panel2.Controls.Add(this.mainTabControl);
            this.splitContainerVertical.Panel2.Controls.Add(this.uniqueTilesCountLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.uniqueTilesLabel);
            this.splitContainerVertical.Panel2.Controls.Add(this.selectedTilePictureBox);
            this.splitContainerVertical.Panel2.Controls.Add(this.selectedTileLabel);
            this.splitContainerVertical.Size = new System.Drawing.Size(943, 535);
            this.splitContainerVertical.SplitterDistance = 603;
            this.splitContainerVertical.TabIndex = 999;
            // 
            // outerOuterDrawPanel
            // 
            this.outerOuterDrawPanel.Controls.Add(this.outerDrawPanel);
            this.outerOuterDrawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outerOuterDrawPanel.Location = new System.Drawing.Point(0, 0);
            this.outerOuterDrawPanel.Name = "outerOuterDrawPanel";
            this.outerOuterDrawPanel.Size = new System.Drawing.Size(601, 516);
            this.outerOuterDrawPanel.TabIndex = 2;
            // 
            // outerDrawPanel
            // 
            this.outerDrawPanel.Cursor = System.Windows.Forms.Cursors.Cross;
            this.outerDrawPanel.Location = new System.Drawing.Point(49, 53);
            this.outerDrawPanel.Name = "outerDrawPanel";
            this.outerDrawPanel.Size = new System.Drawing.Size(200, 100);
            this.outerDrawPanel.TabIndex = 1;
            // 
            // scrollBar
            // 
            this.scrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scrollBar.LargeChange = 1;
            this.scrollBar.Location = new System.Drawing.Point(0, 516);
            this.scrollBar.Name = "scrollBar";
            this.scrollBar.Size = new System.Drawing.Size(601, 17);
            this.scrollBar.TabIndex = 0;
            this.scrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBarScroll);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.bgTabPage);
            this.mainTabControl.Controls.Add(this.enTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(13, 53);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(309, 464);
            this.mainTabControl.TabIndex = 0;
            // 
            // bgTabPage
            // 
            this.bgTabPage.Controls.Add(this.bgTabControl);
            this.bgTabPage.Location = new System.Drawing.Point(4, 4);
            this.bgTabPage.Name = "bgTabPage";
            this.bgTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.bgTabPage.Size = new System.Drawing.Size(301, 438);
            this.bgTabPage.TabIndex = 0;
            this.bgTabPage.Text = "Background";
            this.bgTabPage.UseVisualStyleBackColor = true;
            // 
            // bgTabControl
            // 
            this.bgTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.bgTabControl.Controls.Add(this.blockingTilesTabPage);
            this.bgTabControl.Controls.Add(this.nonBlockingTilesTabPage);
            this.bgTabControl.Controls.Add(this.threatTilesTabPage);
            this.bgTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bgTabControl.Location = new System.Drawing.Point(3, 3);
            this.bgTabControl.Multiline = true;
            this.bgTabControl.Name = "bgTabControl";
            this.bgTabControl.SelectedIndex = 0;
            this.bgTabControl.Size = new System.Drawing.Size(295, 432);
            this.bgTabControl.TabIndex = 47;
            // 
            // blockingTilesTabPage
            // 
            this.blockingTilesTabPage.Location = new System.Drawing.Point(4, 4);
            this.blockingTilesTabPage.Name = "blockingTilesTabPage";
            this.blockingTilesTabPage.Size = new System.Drawing.Size(287, 406);
            this.blockingTilesTabPage.TabIndex = 2;
            this.blockingTilesTabPage.Text = "Blocking";
            this.blockingTilesTabPage.UseVisualStyleBackColor = true;
            // 
            // nonBlockingTilesTabPage
            // 
            this.nonBlockingTilesTabPage.Location = new System.Drawing.Point(4, 4);
            this.nonBlockingTilesTabPage.Name = "nonBlockingTilesTabPage";
            this.nonBlockingTilesTabPage.Size = new System.Drawing.Size(287, 406);
            this.nonBlockingTilesTabPage.TabIndex = 3;
            this.nonBlockingTilesTabPage.Text = "Non-blocking";
            this.nonBlockingTilesTabPage.UseVisualStyleBackColor = true;
            // 
            // threatTilesTabPage
            // 
            this.threatTilesTabPage.Location = new System.Drawing.Point(4, 4);
            this.threatTilesTabPage.Name = "threatTilesTabPage";
            this.threatTilesTabPage.Size = new System.Drawing.Size(287, 406);
            this.threatTilesTabPage.TabIndex = 4;
            this.threatTilesTabPage.Text = "Threat";
            this.threatTilesTabPage.UseVisualStyleBackColor = true;
            // 
            // enTabPage
            // 
            this.enTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.enTabPage.Controls.Add(this.enemiesListBox);
            this.enTabPage.Controls.Add(this.enTabBottomPanel);
            this.enTabPage.Location = new System.Drawing.Point(4, 4);
            this.enTabPage.Name = "enTabPage";
            this.enTabPage.Size = new System.Drawing.Size(301, 438);
            this.enTabPage.TabIndex = 1;
            this.enTabPage.Text = "Enemies";
            // 
            // enemiesListBox
            // 
            this.enemiesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enemiesListBox.FormattingEnabled = true;
            this.enemiesListBox.Location = new System.Drawing.Point(0, 0);
            this.enemiesListBox.Name = "enemiesListBox";
            this.enemiesListBox.Size = new System.Drawing.Size(301, 400);
            this.enemiesListBox.TabIndex = 1;
            this.enemiesListBox.SelectedIndexChanged += new System.EventHandler(this.EnemiesListBoxSelectedIndexChanged);
            this.enemiesListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnemiesListBoxKeyDown);
            this.enemiesListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.EnemiesListBoxMouseDoubleClick);
            this.enemiesListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnemiesListBoxMouseDown);
            // 
            // enTabBottomPanel
            // 
            this.enTabBottomPanel.Controls.Add(this.editEnemyButton);
            this.enTabBottomPanel.Controls.Add(this.findEnemyButton);
            this.enTabBottomPanel.Controls.Add(this.deleteEnemyButton);
            this.enTabBottomPanel.Controls.Add(this.addEnemyButton);
            this.enTabBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.enTabBottomPanel.Location = new System.Drawing.Point(0, 400);
            this.enTabBottomPanel.Name = "enTabBottomPanel";
            this.enTabBottomPanel.Size = new System.Drawing.Size(301, 38);
            this.enTabBottomPanel.TabIndex = 0;
            // 
            // editEnemyButton
            // 
            this.editEnemyButton.Enabled = false;
            this.editEnemyButton.Location = new System.Drawing.Point(152, 7);
            this.editEnemyButton.Name = "editEnemyButton";
            this.editEnemyButton.Size = new System.Drawing.Size(66, 23);
            this.editEnemyButton.TabIndex = 2;
            this.editEnemyButton.Text = "Edit";
            this.editEnemyButton.UseVisualStyleBackColor = true;
            this.editEnemyButton.Click += new System.EventHandler(this.EditEnemyButtonClick);
            // 
            // deleteEnemyButton
            // 
            this.deleteEnemyButton.Enabled = false;
            this.deleteEnemyButton.Location = new System.Drawing.Point(80, 7);
            this.deleteEnemyButton.Name = "deleteEnemyButton";
            this.deleteEnemyButton.Size = new System.Drawing.Size(66, 23);
            this.deleteEnemyButton.TabIndex = 1;
            this.deleteEnemyButton.Text = "Delete";
            this.deleteEnemyButton.UseVisualStyleBackColor = true;
            this.deleteEnemyButton.Click += new System.EventHandler(this.DeleteEnemyButtonClick);
            // 
            // addEnemyButton
            // 
            this.addEnemyButton.Location = new System.Drawing.Point(8, 7);
            this.addEnemyButton.Name = "addEnemyButton";
            this.addEnemyButton.Size = new System.Drawing.Size(66, 23);
            this.addEnemyButton.TabIndex = 0;
            this.addEnemyButton.Text = "Add";
            this.addEnemyButton.UseVisualStyleBackColor = true;
            this.addEnemyButton.Click += new System.EventHandler(this.AddEnemyButtonClick);
            // 
            // uniqueTilesCountLabel
            // 
            this.uniqueTilesCountLabel.AutoSize = true;
            this.uniqueTilesCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uniqueTilesCountLabel.ForeColor = System.Drawing.Color.Red;
            this.uniqueTilesCountLabel.Location = new System.Drawing.Point(235, 24);
            this.uniqueTilesCountLabel.Name = "uniqueTilesCountLabel";
            this.uniqueTilesCountLabel.Size = new System.Drawing.Size(14, 13);
            this.uniqueTilesCountLabel.TabIndex = 51;
            this.uniqueTilesCountLabel.Text = "0";
            // 
            // uniqueTilesLabel
            // 
            this.uniqueTilesLabel.AutoSize = true;
            this.uniqueTilesLabel.Location = new System.Drawing.Point(171, 24);
            this.uniqueTilesLabel.Name = "uniqueTilesLabel";
            this.uniqueTilesLabel.Size = new System.Drawing.Size(68, 13);
            this.uniqueTilesLabel.TabIndex = 50;
            this.uniqueTilesLabel.Text = "Unique tiles: ";
            // 
            // selectedTilePictureBox
            // 
            this.selectedTilePictureBox.BackColor = System.Drawing.Color.White;
            this.selectedTilePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedTilePictureBox.Location = new System.Drawing.Point(100, 14);
            this.selectedTilePictureBox.Name = "selectedTilePictureBox";
            this.selectedTilePictureBox.Size = new System.Drawing.Size(32, 32);
            this.selectedTilePictureBox.TabIndex = 49;
            this.selectedTilePictureBox.TabStop = false;
            // 
            // selectedTileLabel
            // 
            this.selectedTileLabel.AutoSize = true;
            this.selectedTileLabel.Location = new System.Drawing.Point(22, 24);
            this.selectedTileLabel.Name = "selectedTileLabel";
            this.selectedTileLabel.Size = new System.Drawing.Size(72, 13);
            this.selectedTileLabel.TabIndex = 48;
            this.selectedTileLabel.Text = "Selected Tile:";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 559);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(943, 22);
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
            this.menuStrip.Size = new System.Drawing.Size(943, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportLevelToolStripMenuItem,
            this.exportImageToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // exportLevelToolStripMenuItem
            // 
            this.exportLevelToolStripMenuItem.Name = "exportLevelToolStripMenuItem";
            this.exportLevelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exportLevelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportLevelToolStripMenuItem.Text = "&Export";
            this.exportLevelToolStripMenuItem.Click += new System.EventHandler(this.ExportLevelToolStripMenuItemClick);
            // 
            // exportImageToolStripMenuItem
            // 
            this.exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
            this.exportImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.exportImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportImageToolStripMenuItem.Text = "Export Image";
            this.exportImageToolStripMenuItem.Click += new System.EventHandler(this.ExportImageToolStripMenuItemClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem,
            this.toolStripSeparator1,
            this.transformToolStripMenuItem,
            this.toolStripSeparator4,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.propertiesToolStripMenuItem.Text = "&Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.PropertiesToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.transformToolStripMenuItem.Text = "Transform";
            this.transformToolStripMenuItem.Click += new System.EventHandler(this.TransformToolStripMenuItemClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItemClick);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTypeToolStripMenuItem,
            this.showGridToolStripMenuItem,
            this.toolStripSeparator5,
            this.showEnemiesToolStripMenuItem,
            this.toolStripSeparator2,
            this.viewPlatformsToolStripMenuItem,
            this.viewThreatsToolStripMenuItem,
            this.toolStripSeparator3,
            this.exportCheckToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // showTypeToolStripMenuItem
            // 
            this.showTypeToolStripMenuItem.CheckOnClick = true;
            this.showTypeToolStripMenuItem.Name = "showTypeToolStripMenuItem";
            this.showTypeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.showTypeToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.showTypeToolStripMenuItem.Text = "Show &Type";
            this.showTypeToolStripMenuItem.Click += new System.EventHandler(this.ShowTypeToolStripMenuItemClick);
            // 
            // showGridToolStripMenuItem
            // 
            this.showGridToolStripMenuItem.CheckOnClick = true;
            this.showGridToolStripMenuItem.Name = "showGridToolStripMenuItem";
            this.showGridToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.showGridToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.showGridToolStripMenuItem.Text = "Show &Grid";
            this.showGridToolStripMenuItem.Click += new System.EventHandler(this.ShowGridToolStripMenuItemClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(223, 6);
            // 
            // showEnemiesToolStripMenuItem
            // 
            this.showEnemiesToolStripMenuItem.Checked = true;
            this.showEnemiesToolStripMenuItem.CheckOnClick = true;
            this.showEnemiesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showEnemiesToolStripMenuItem.Name = "showEnemiesToolStripMenuItem";
            this.showEnemiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.showEnemiesToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.showEnemiesToolStripMenuItem.Text = "Show &Enemies";
            this.showEnemiesToolStripMenuItem.Click += new System.EventHandler(this.ShowEnemiesToolStripMenuItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(223, 6);
            // 
            // viewPlatformsToolStripMenuItem
            // 
            this.viewPlatformsToolStripMenuItem.Name = "viewPlatformsToolStripMenuItem";
            this.viewPlatformsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
            this.viewPlatformsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.viewPlatformsToolStripMenuItem.Text = "View Platforms";
            this.viewPlatformsToolStripMenuItem.Click += new System.EventHandler(this.ViewPlatformsToolStripMenuItemClick);
            // 
            // viewThreatsToolStripMenuItem
            // 
            this.viewThreatsToolStripMenuItem.Name = "viewThreatsToolStripMenuItem";
            this.viewThreatsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.viewThreatsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.viewThreatsToolStripMenuItem.Text = "View Threats";
            this.viewThreatsToolStripMenuItem.Click += new System.EventHandler(this.ViewThreatsToolStripMenuItemClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(223, 6);
            // 
            // exportCheckToolStripMenuItem
            // 
            this.exportCheckToolStripMenuItem.Name = "exportCheckToolStripMenuItem";
            this.exportCheckToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.exportCheckToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.exportCheckToolStripMenuItem.Text = "Export Check";
            this.exportCheckToolStripMenuItem.Click += new System.EventHandler(this.ExportCheckToolStripMenuItemClick);
            // 
            // findEnemyButton
            // 
            this.findEnemyButton.Enabled = false;
            this.findEnemyButton.Location = new System.Drawing.Point(224, 7);
            this.findEnemyButton.Name = "findEnemyButton";
            this.findEnemyButton.Size = new System.Drawing.Size(66, 23);
            this.findEnemyButton.TabIndex = 2;
            this.findEnemyButton.Text = "Find";
            this.findEnemyButton.UseVisualStyleBackColor = true;
            this.findEnemyButton.Click += new System.EventHandler(this.FindEnemyButtonClick);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 581);
            this.Controls.Add(this.splitContainerVertical);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(600, 620);
            this.Name = "LevelEditor";
            this.Text = "Level editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LevelEditorLoad);
            this.Resize += new System.EventHandler(this.LevelEditorResize);
            this.splitContainerVertical.Panel1.ResumeLayout(false);
            this.splitContainerVertical.Panel2.ResumeLayout(false);
            this.splitContainerVertical.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVertical)).EndInit();
            this.splitContainerVertical.ResumeLayout(false);
            this.outerOuterDrawPanel.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.bgTabPage.ResumeLayout(false);
            this.bgTabControl.ResumeLayout(false);
            this.enTabPage.ResumeLayout(false);
            this.enTabBottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.selectedTilePictureBox)).EndInit();
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
        private System.Windows.Forms.Panel outerDrawPanel;
        private System.Windows.Forms.HScrollBar scrollBar;
        private System.Windows.Forms.Panel outerOuterDrawPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.PictureBox selectedTilePictureBox;
        private System.Windows.Forms.Label selectedTileLabel;
        private System.Windows.Forms.Label uniqueTilesLabel;
        private System.Windows.Forms.Label uniqueTilesCountLabel;
        private System.Windows.Forms.ToolStripMenuItem exportImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem viewPlatformsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewThreatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exportCheckToolStripMenuItem;
        private System.Windows.Forms.TabControl bgTabControl;
        private System.Windows.Forms.TabPage blockingTilesTabPage;
        private System.Windows.Forms.TabPage nonBlockingTilesTabPage;
        private System.Windows.Forms.TabPage threatTilesTabPage;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage bgTabPage;
        private System.Windows.Forms.TabPage enTabPage;
        private System.Windows.Forms.Panel enTabBottomPanel;
        private System.Windows.Forms.Button editEnemyButton;
        private System.Windows.Forms.Button deleteEnemyButton;
        private System.Windows.Forms.Button addEnemyButton;
        private System.Windows.Forms.ListBox enemiesListBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem showEnemiesToolStripMenuItem;
        private System.Windows.Forms.Button findEnemyButton;
    }
}