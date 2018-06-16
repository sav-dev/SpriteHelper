namespace SpriteHelper
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
            this.playerSpecLabel = new System.Windows.Forms.Label();
            this.playerSpecTextbox = new System.Windows.Forms.TextBox();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.specLabel = new System.Windows.Forms.Label();
            this.specTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.leftMostPanel = new System.Windows.Forms.Panel();
            this.enemiesListBox = new System.Windows.Forms.ListBox();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.commentLabel = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // playerSpecLabel
            // 
            this.playerSpecLabel.AutoSize = true;
            this.playerSpecLabel.Location = new System.Drawing.Point(7, 66);
            this.playerSpecLabel.Name = "playerSpecLabel";
            this.playerSpecLabel.Size = new System.Drawing.Size(65, 13);
            this.playerSpecLabel.TabIndex = 31;
            this.playerSpecLabel.Text = "Player spec.";
            // 
            // playerSpecTextbox
            // 
            this.playerSpecTextbox.Location = new System.Drawing.Point(75, 63);
            this.playerSpecTextbox.Name = "playerSpecTextbox";
            this.playerSpecTextbox.Size = new System.Drawing.Size(585, 20);
            this.playerSpecTextbox.TabIndex = 30;
            this.playerSpecTextbox.Text = "C:\\Users\\tomas\\Documents\\NES\\GitHub\\Platformer\\PlatformerGraphics\\Sprites\\player." +
    "xml";
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
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(75, 37);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.Size = new System.Drawing.Size(585, 20);
            this.palettesTextBox.TabIndex = 28;
            this.palettesTextBox.Text = "C:\\users\\tomas\\documents\\nes\\github\\platformer\\PlatformerGraphics\\palettes\\palett" +
    "es_00.xml";
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
            this.specTextBox.Size = new System.Drawing.Size(585, 20);
            this.specTextBox.TabIndex = 26;
            this.specTextBox.Text = "C:\\Users\\tomas\\Documents\\NES\\GitHub\\Platformer\\PlatformerGraphics\\Sprites\\enemies" +
    ".xml";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Expl. spec.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 89);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(585, 20);
            this.textBox1.TabIndex = 32;
            this.textBox1.Text = "C:\\Users\\tomas\\Documents\\NES\\GitHub\\Platformer\\PlatformerGraphics\\Sprites\\explosi" +
    "on.xml";
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.specLabel);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.specTextBox);
            this.topPanel.Controls.Add(this.textBox1);
            this.topPanel.Controls.Add(this.palettesTextBox);
            this.topPanel.Controls.Add(this.playerSpecLabel);
            this.topPanel.Controls.Add(this.palettesLabel);
            this.topPanel.Controls.Add(this.playerSpecTextbox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(672, 187);
            this.topPanel.TabIndex = 34;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 445);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(672, 10);
            this.bottomPanel.TabIndex = 35;
            // 
            // leftMostPanel
            // 
            this.leftMostPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftMostPanel.Location = new System.Drawing.Point(0, 187);
            this.leftMostPanel.Name = "leftMostPanel";
            this.leftMostPanel.Size = new System.Drawing.Size(10, 258);
            this.leftMostPanel.TabIndex = 36;
            // 
            // enemiesListBox
            // 
            this.enemiesListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.enemiesListBox.FormattingEnabled = true;
            this.enemiesListBox.Location = new System.Drawing.Point(10, 187);
            this.enemiesListBox.Name = "enemiesListBox";
            this.enemiesListBox.Size = new System.Drawing.Size(112, 258);
            this.enemiesListBox.TabIndex = 11;
            // 
            // leftPanel
            // 
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(122, 187);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(10, 258);
            this.leftPanel.TabIndex = 37;
            // 
            // rightPanel
            // 
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(662, 187);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(10, 258);
            this.rightPanel.TabIndex = 38;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox.Location = new System.Drawing.Point(392, 187);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(270, 258);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 39;
            this.pictureBox.TabStop = false;
            // 
            // centerPanel
            // 
            this.centerPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.centerPanel.Location = new System.Drawing.Point(382, 187);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(10, 258);
            this.centerPanel.TabIndex = 40;
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.Location = new System.Drawing.Point(150, 205);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(123, 13);
            this.commentLabel.TabIndex = 41;
            this.commentLabel.Text = "add properties view here";
            // 
            // EnemiesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 455);
            this.Controls.Add(this.commentLabel);
            this.Controls.Add(this.centerPanel);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.enemiesListBox);
            this.Controls.Add(this.leftMostPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "EnemiesWindow";
            this.Text = "Enemies";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playerSpecLabel;
        private System.Windows.Forms.TextBox playerSpecTextbox;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.TextBox specTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel leftMostPanel;
        private System.Windows.Forms.ListBox enemiesListBox;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel centerPanel;
        private System.Windows.Forms.Label commentLabel;
    }
}