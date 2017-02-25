namespace SpriteHelper
{
    partial class BackgroundTilesetCreator
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
            this.outputImageTextBox = new System.Windows.Forms.TextBox();
            this.blockingLabel = new System.Windows.Forms.Label();
            this.specLabel = new System.Windows.Forms.Label();
            this.palettesLabel = new System.Windows.Forms.Label();
            this.outputSpecTextBox = new System.Windows.Forms.TextBox();
            this.blockingTextBox = new System.Windows.Forms.TextBox();
            this.processButton = new System.Windows.Forms.Button();
            this.bgColorLabel = new System.Windows.Forms.Label();
            this.nonBlockingTextBox = new System.Windows.Forms.TextBox();
            this.nonBlockingLabel = new System.Windows.Forms.Label();
            this.threatTextBox = new System.Windows.Forms.TextBox();
            this.ThreatsLabel = new System.Windows.Forms.Label();
            this.bgColorComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // outputImageTextBox
            // 
            this.outputImageTextBox.Location = new System.Drawing.Point(86, 130);
            this.outputImageTextBox.Name = "outputImageTextBox";
            this.outputImageTextBox.Size = new System.Drawing.Size(395, 20);
            this.outputImageTextBox.TabIndex = 3;
            // 
            // blockingLabel
            // 
            this.blockingLabel.AutoSize = true;
            this.blockingLabel.Location = new System.Drawing.Point(13, 37);
            this.blockingLabel.Name = "blockingLabel";
            this.blockingLabel.Size = new System.Drawing.Size(48, 13);
            this.blockingLabel.TabIndex = 4;
            this.blockingLabel.Text = "Blocking";
            // 
            // specLabel
            // 
            this.specLabel.AutoSize = true;
            this.specLabel.Location = new System.Drawing.Point(12, 133);
            this.specLabel.Name = "specLabel";
            this.specLabel.Size = new System.Drawing.Size(70, 13);
            this.specLabel.TabIndex = 5;
            this.specLabel.Text = "Output image";
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(12, 159);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(65, 13);
            this.palettesLabel.TabIndex = 15;
            this.palettesLabel.Text = "Output spec";
            // 
            // outputSpecTextBox
            // 
            this.outputSpecTextBox.Location = new System.Drawing.Point(86, 156);
            this.outputSpecTextBox.Name = "outputSpecTextBox";
            this.outputSpecTextBox.Size = new System.Drawing.Size(395, 20);
            this.outputSpecTextBox.TabIndex = 14;
            // 
            // blockingTextBox
            // 
            this.blockingTextBox.Location = new System.Drawing.Point(86, 34);
            this.blockingTextBox.Name = "blockingTextBox";
            this.blockingTextBox.Size = new System.Drawing.Size(395, 20);
            this.blockingTextBox.TabIndex = 16;
            this.blockingTextBox.WordWrap = false;
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(406, 191);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(75, 23);
            this.processButton.TabIndex = 17;
            this.processButton.Text = "Process";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.ProcessButtonClick);
            // 
            // bgColorLabel
            // 
            this.bgColorLabel.AutoSize = true;
            this.bgColorLabel.Location = new System.Drawing.Point(12, 89);
            this.bgColorLabel.Name = "bgColorLabel";
            this.bgColorLabel.Size = new System.Drawing.Size(49, 13);
            this.bgColorLabel.TabIndex = 19;
            this.bgColorLabel.Text = "BG Color";
            // 
            // nonBlockingTextBox
            // 
            this.nonBlockingTextBox.Location = new System.Drawing.Point(86, 8);
            this.nonBlockingTextBox.Name = "nonBlockingTextBox";
            this.nonBlockingTextBox.Size = new System.Drawing.Size(395, 20);
            this.nonBlockingTextBox.TabIndex = 22;
            this.nonBlockingTextBox.WordWrap = false;
            // 
            // nonBlockingLabel
            // 
            this.nonBlockingLabel.AutoSize = true;
            this.nonBlockingLabel.Location = new System.Drawing.Point(13, 11);
            this.nonBlockingLabel.Name = "nonBlockingLabel";
            this.nonBlockingLabel.Size = new System.Drawing.Size(70, 13);
            this.nonBlockingLabel.TabIndex = 21;
            this.nonBlockingLabel.Text = "Non-blocking";
            // 
            // threatsTextBox
            // 
            this.threatTextBox.Location = new System.Drawing.Point(86, 60);
            this.threatTextBox.Name = "threatsTextBox";
            this.threatTextBox.Size = new System.Drawing.Size(395, 20);
            this.threatTextBox.TabIndex = 24;
            this.threatTextBox.WordWrap = false;
            // 
            // ThreatsLabel
            // 
            this.ThreatsLabel.AutoSize = true;
            this.ThreatsLabel.Location = new System.Drawing.Point(12, 63);
            this.ThreatsLabel.Name = "ThreatsLabel";
            this.ThreatsLabel.Size = new System.Drawing.Size(43, 13);
            this.ThreatsLabel.TabIndex = 23;
            this.ThreatsLabel.Text = "Threats";
            // 
            // bgColorComboBox
            // 
            this.bgColorComboBox.FormattingEnabled = true;
            this.bgColorComboBox.Items.AddRange(new object[] {
            "Black",
            "Light Grey",
            "Medium Grey",
            "Dark Grey"});
            this.bgColorComboBox.Location = new System.Drawing.Point(86, 86);
            this.bgColorComboBox.Name = "bgColorComboBox";
            this.bgColorComboBox.Size = new System.Drawing.Size(121, 21);
            this.bgColorComboBox.TabIndex = 25;
            // 
            // BackgroundTilesetCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 219);
            this.Controls.Add(this.bgColorComboBox);
            this.Controls.Add(this.threatTextBox);
            this.Controls.Add(this.ThreatsLabel);
            this.Controls.Add(this.nonBlockingTextBox);
            this.Controls.Add(this.nonBlockingLabel);
            this.Controls.Add(this.bgColorLabel);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.blockingTextBox);
            this.Controls.Add(this.palettesLabel);
            this.Controls.Add(this.outputSpecTextBox);
            this.Controls.Add(this.specLabel);
            this.Controls.Add(this.blockingLabel);
            this.Controls.Add(this.outputImageTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "BackgroundTilesetCreator";
            this.Text = "Background tileset creator";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox outputImageTextBox;
        private System.Windows.Forms.Label blockingLabel;
        private System.Windows.Forms.Label specLabel;
        private System.Windows.Forms.Label palettesLabel;
        private System.Windows.Forms.TextBox outputSpecTextBox;
        private System.Windows.Forms.TextBox blockingTextBox;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.Label bgColorLabel;
        private System.Windows.Forms.TextBox nonBlockingTextBox;
        private System.Windows.Forms.Label nonBlockingLabel;
        private System.Windows.Forms.TextBox threatTextBox;
        private System.Windows.Forms.Label ThreatsLabel;
        private System.Windows.Forms.ComboBox bgColorComboBox;
    }
}

