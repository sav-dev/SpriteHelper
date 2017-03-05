namespace SpriteHelper
{
    partial class PaletteProcessor
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
            this.palettesLabel = new System.Windows.Forms.Label();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.processButton = new System.Windows.Forms.Button();
            this.palettesPictureBox = new System.Windows.Forms.PictureBox();
            this.spritesLabel = new System.Windows.Forms.Label();
            this.spritesTextBox = new System.Windows.Forms.TextBox();
            this.backgroundLabel = new System.Windows.Forms.Label();
            this.backgroundTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.palettesPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(10, 15);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(45, 13);
            this.palettesLabel.TabIndex = 17;
            this.palettesLabel.Text = "Palettes";
            // 
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(80, 12);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.Size = new System.Drawing.Size(399, 20);
            this.palettesTextBox.TabIndex = 16;
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(405, 90);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(75, 23);
            this.processButton.TabIndex = 18;
            this.processButton.Text = "Process";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.ProcessButtonClick);
            // 
            // palettesPictureBox
            // 
            this.palettesPictureBox.BackColor = System.Drawing.Color.White;
            this.palettesPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.palettesPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.palettesPictureBox.Location = new System.Drawing.Point(14, 129);
            this.palettesPictureBox.Name = "palettesPictureBox";
            this.palettesPictureBox.Size = new System.Drawing.Size(466, 146);
            this.palettesPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.palettesPictureBox.TabIndex = 19;
            this.palettesPictureBox.TabStop = false;
            // 
            // spritesLabel
            // 
            this.spritesLabel.AutoSize = true;
            this.spritesLabel.Location = new System.Drawing.Point(11, 41);
            this.spritesLabel.Name = "spritesLabel";
            this.spritesLabel.Size = new System.Drawing.Size(39, 13);
            this.spritesLabel.TabIndex = 21;
            this.spritesLabel.Text = "Sprites";
            // 
            // spritesTextBox
            // 
            this.spritesTextBox.Location = new System.Drawing.Point(81, 38);
            this.spritesTextBox.Name = "spritesTextBox";
            this.spritesTextBox.Size = new System.Drawing.Size(399, 20);
            this.spritesTextBox.TabIndex = 20;
            // 
            // backgroundLabel
            // 
            this.backgroundLabel.AutoSize = true;
            this.backgroundLabel.Location = new System.Drawing.Point(11, 67);
            this.backgroundLabel.Name = "backgroundLabel";
            this.backgroundLabel.Size = new System.Drawing.Size(65, 13);
            this.backgroundLabel.TabIndex = 23;
            this.backgroundLabel.Text = "Background";
            // 
            // backgroundTextBox
            // 
            this.backgroundTextBox.Location = new System.Drawing.Point(81, 64);
            this.backgroundTextBox.Name = "backgroundTextBox";
            this.backgroundTextBox.Size = new System.Drawing.Size(399, 20);
            this.backgroundTextBox.TabIndex = 22;
            // 
            // PaletteProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 283);
            this.Controls.Add(this.backgroundLabel);
            this.Controls.Add(this.backgroundTextBox);
            this.Controls.Add(this.spritesLabel);
            this.Controls.Add(this.spritesTextBox);
            this.Controls.Add(this.palettesPictureBox);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.palettesLabel);
            this.Controls.Add(this.palettesTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "PaletteProcessor";
            this.Text = "Palette Processor";
            this.Load += new System.EventHandler(this.PaletteProcessorLoad);
            ((System.ComponentModel.ISupportInitialize)(this.palettesPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.PictureBox palettesPictureBox;
        private System.Windows.Forms.Label spritesLabel;
        private System.Windows.Forms.TextBox spritesTextBox;
        private System.Windows.Forms.Label backgroundLabel;
        private System.Windows.Forms.TextBox backgroundTextBox;
    }
}