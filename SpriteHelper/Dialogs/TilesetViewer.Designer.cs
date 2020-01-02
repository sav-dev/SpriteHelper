namespace SpriteHelper.Dialogs
{
    partial class TilesetViewer
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
            this.tilesetComboBox = new System.Windows.Forms.ComboBox();
            this.tilesetLabel = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.paletteComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.paletteLabel = new System.Windows.Forms.Panel();
            this.reloadButton = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tilesetComboBox
            // 
            this.tilesetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tilesetComboBox.FormattingEnabled = true;
            this.tilesetComboBox.Location = new System.Drawing.Point(57, 9);
            this.tilesetComboBox.Name = "tilesetComboBox";
            this.tilesetComboBox.Size = new System.Drawing.Size(121, 21);
            this.tilesetComboBox.TabIndex = 40;
            this.tilesetComboBox.SelectedIndexChanged += new System.EventHandler(this.TilesetComboBoxSelectedIndexChanged);
            // 
            // tilesetLabel
            // 
            this.tilesetLabel.AutoSize = true;
            this.tilesetLabel.Location = new System.Drawing.Point(13, 12);
            this.tilesetLabel.Name = "tilesetLabel";
            this.tilesetLabel.Size = new System.Drawing.Size(38, 13);
            this.tilesetLabel.TabIndex = 39;
            this.tilesetLabel.Text = "Tileset";
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.reloadButton);
            this.topPanel.Controls.Add(this.cancelButton);
            this.topPanel.Controls.Add(this.okButton);
            this.topPanel.Controls.Add(this.paletteComboBox);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.tilesetComboBox);
            this.topPanel.Controls.Add(this.tilesetLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(524, 42);
            this.topPanel.TabIndex = 41;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(440, 7);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 44;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(359, 7);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 43;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // paletteComboBox
            // 
            this.paletteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paletteComboBox.FormattingEnabled = true;
            this.paletteComboBox.Location = new System.Drawing.Point(232, 9);
            this.paletteComboBox.Name = "paletteComboBox";
            this.paletteComboBox.Size = new System.Drawing.Size(121, 21);
            this.paletteComboBox.TabIndex = 42;
            this.paletteComboBox.SelectedIndexChanged += new System.EventHandler(this.PaletteComboBoxSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Palette";
            // 
            // paletteLabel
            // 
            this.paletteLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.paletteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paletteLabel.Location = new System.Drawing.Point(0, 42);
            this.paletteLabel.Name = "paletteLabel";
            this.paletteLabel.Size = new System.Drawing.Size(524, 561);
            this.paletteLabel.TabIndex = 42;
            // 
            // reloadButton
            // 
            this.reloadButton.Location = new System.Drawing.Point(359, 7);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(63, 23);
            this.reloadButton.TabIndex = 45;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.ReloadButtonClick);
            // 
            // TilesetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 603);
            this.Controls.Add(this.paletteLabel);
            this.Controls.Add(this.topPanel);
            this.Name = "TilesetViewer";
            this.Text = "TilesetViewer";
            this.Load += new System.EventHandler(this.TilesetViewerLoad);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox tilesetComboBox;
        private System.Windows.Forms.Label tilesetLabel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel paletteLabel;
        private System.Windows.Forms.ComboBox paletteComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button reloadButton;
    }
}