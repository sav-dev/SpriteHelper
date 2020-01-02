namespace SpriteHelper.Dialogs
{
    partial class ChrCombine
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
            this.specsTextBox = new System.Windows.Forms.TextBox();
            this.specsLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.processButton = new System.Windows.Forms.Button();
            this.constChrLabel = new System.Windows.Forms.Label();
            this.constChrTextBox = new System.Windows.Forms.TextBox();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.palettesTextBox = new System.Windows.Forms.TextBox();
            this.constChrConfigLabel = new System.Windows.Forms.Label();
            this.constChrConfigTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // specsTextBox
            // 
            this.specsTextBox.Location = new System.Drawing.Point(12, 31);
            this.specsTextBox.Multiline = true;
            this.specsTextBox.Name = "specsTextBox";
            this.specsTextBox.Size = new System.Drawing.Size(562, 102);
            this.specsTextBox.TabIndex = 24;
            this.specsTextBox.WordWrap = false;
            // 
            // specsLabel
            // 
            this.specsLabel.AutoSize = true;
            this.specsLabel.Location = new System.Drawing.Point(9, 15);
            this.specsLabel.Name = "specsLabel";
            this.specsLabel.Size = new System.Drawing.Size(37, 13);
            this.specsLabel.TabIndex = 23;
            this.specsLabel.Text = "Specs";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(12, 218);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(39, 13);
            this.outputLabel.TabIndex = 26;
            this.outputLabel.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(12, 234);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(562, 20);
            this.outputTextBox.TabIndex = 25;
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(499, 319);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(75, 23);
            this.processButton.TabIndex = 27;
            this.processButton.Text = "Process";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.ProcessButtonClick);
            // 
            // constChrLabel
            // 
            this.constChrLabel.AutoSize = true;
            this.constChrLabel.Location = new System.Drawing.Point(12, 136);
            this.constChrLabel.Name = "constChrLabel";
            this.constChrLabel.Size = new System.Drawing.Size(75, 13);
            this.constChrLabel.TabIndex = 29;
            this.constChrLabel.Text = "Constant CHR";
            // 
            // constChrTextBox
            // 
            this.constChrTextBox.Location = new System.Drawing.Point(12, 152);
            this.constChrTextBox.Name = "constChrTextBox";
            this.constChrTextBox.ReadOnly = true;
            this.constChrTextBox.Size = new System.Drawing.Size(562, 20);
            this.constChrTextBox.TabIndex = 28;
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(12, 259);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(45, 13);
            this.palettesLabel.TabIndex = 31;
            this.palettesLabel.Text = "Palettes";
            // 
            // palettesTextBox
            // 
            this.palettesTextBox.Location = new System.Drawing.Point(12, 275);
            this.palettesTextBox.Name = "palettesTextBox";
            this.palettesTextBox.ReadOnly = true;
            this.palettesTextBox.Size = new System.Drawing.Size(562, 20);
            this.palettesTextBox.TabIndex = 30;
            // 
            // constChrConfigLabel
            // 
            this.constChrConfigLabel.AutoSize = true;
            this.constChrConfigLabel.Location = new System.Drawing.Point(12, 177);
            this.constChrConfigLabel.Name = "constChrConfigLabel";
            this.constChrConfigLabel.Size = new System.Drawing.Size(107, 13);
            this.constChrConfigLabel.TabIndex = 33;
            this.constChrConfigLabel.Text = "Constant CHR config";
            // 
            // constChrConfigTextBox
            // 
            this.constChrConfigTextBox.Location = new System.Drawing.Point(12, 193);
            this.constChrConfigTextBox.Name = "constChrConfigTextBox";
            this.constChrConfigTextBox.ReadOnly = true;
            this.constChrConfigTextBox.Size = new System.Drawing.Size(562, 20);
            this.constChrConfigTextBox.TabIndex = 32;
            // 
            // ChrCombine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 366);
            this.Controls.Add(this.constChrConfigLabel);
            this.Controls.Add(this.constChrConfigTextBox);
            this.Controls.Add(this.palettesLabel);
            this.Controls.Add(this.palettesTextBox);
            this.Controls.Add(this.constChrLabel);
            this.Controls.Add(this.constChrTextBox);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.specsTextBox);
            this.Controls.Add(this.specsLabel);
            this.Name = "ChrCombine";
            this.Text = "CHR Combine";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox specsTextBox;
        private System.Windows.Forms.Label specsLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.Label constChrLabel;
        private System.Windows.Forms.TextBox constChrTextBox;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox palettesTextBox;
        private System.Windows.Forms.Label constChrConfigLabel;
        private System.Windows.Forms.TextBox constChrConfigTextBox;
    }
}