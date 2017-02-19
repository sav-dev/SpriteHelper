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
            this.bgColorTextBox = new System.Windows.Forms.TextBox();
            this.bgColorPanel = new System.Windows.Forms.Panel();
            this.nonBlockingTextBox = new System.Windows.Forms.TextBox();
            this.nonBlockingLabel = new System.Windows.Forms.Label();
            this.threatsTextBox = new System.Windows.Forms.TextBox();
            this.ThreatsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // outputImageTextBox
            // 
            this.outputImageTextBox.Location = new System.Drawing.Point(86, 342);
            this.outputImageTextBox.Name = "outputImageTextBox";
            this.outputImageTextBox.Size = new System.Drawing.Size(395, 20);
            this.outputImageTextBox.TabIndex = 3;
            // 
            // blockingLabel
            // 
            this.blockingLabel.AutoSize = true;
            this.blockingLabel.Location = new System.Drawing.Point(12, 11);
            this.blockingLabel.Name = "blockingLabel";
            this.blockingLabel.Size = new System.Drawing.Size(48, 13);
            this.blockingLabel.TabIndex = 4;
            this.blockingLabel.Text = "Blocking";
            // 
            // specLabel
            // 
            this.specLabel.AutoSize = true;
            this.specLabel.Location = new System.Drawing.Point(12, 345);
            this.specLabel.Name = "specLabel";
            this.specLabel.Size = new System.Drawing.Size(70, 13);
            this.specLabel.TabIndex = 5;
            this.specLabel.Text = "Output image";
            // 
            // palettesLabel
            // 
            this.palettesLabel.AutoSize = true;
            this.palettesLabel.Location = new System.Drawing.Point(12, 371);
            this.palettesLabel.Name = "palettesLabel";
            this.palettesLabel.Size = new System.Drawing.Size(65, 13);
            this.palettesLabel.TabIndex = 15;
            this.palettesLabel.Text = "Output spec";
            // 
            // outputSpecTextBox
            // 
            this.outputSpecTextBox.Location = new System.Drawing.Point(86, 368);
            this.outputSpecTextBox.Name = "outputSpecTextBox";
            this.outputSpecTextBox.Size = new System.Drawing.Size(395, 20);
            this.outputSpecTextBox.TabIndex = 14;
            // 
            // blockingTextBox
            // 
            this.blockingTextBox.Location = new System.Drawing.Point(86, 8);
            this.blockingTextBox.Multiline = true;
            this.blockingTextBox.Name = "blockingTextBox";
            this.blockingTextBox.Size = new System.Drawing.Size(395, 91);
            this.blockingTextBox.TabIndex = 16;
            this.blockingTextBox.WordWrap = false;
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(406, 403);
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
            this.bgColorLabel.Location = new System.Drawing.Point(12, 301);
            this.bgColorLabel.Name = "bgColorLabel";
            this.bgColorLabel.Size = new System.Drawing.Size(49, 13);
            this.bgColorLabel.TabIndex = 19;
            this.bgColorLabel.Text = "BG Color";
            // 
            // bgColorTextBox
            // 
            this.bgColorTextBox.Location = new System.Drawing.Point(86, 298);
            this.bgColorTextBox.Name = "bgColorTextBox";
            this.bgColorTextBox.Size = new System.Drawing.Size(105, 20);
            this.bgColorTextBox.TabIndex = 18;
            this.bgColorTextBox.TextChanged += new System.EventHandler(this.BgColorTextBoxTextChanged);
            // 
            // bgColorPanel
            // 
            this.bgColorPanel.Location = new System.Drawing.Point(197, 298);
            this.bgColorPanel.Name = "bgColorPanel";
            this.bgColorPanel.Size = new System.Drawing.Size(20, 20);
            this.bgColorPanel.TabIndex = 20;
            // 
            // nonBlockingTextBox
            // 
            this.nonBlockingTextBox.Location = new System.Drawing.Point(86, 104);
            this.nonBlockingTextBox.Multiline = true;
            this.nonBlockingTextBox.Name = "nonBlockingTextBox";
            this.nonBlockingTextBox.Size = new System.Drawing.Size(395, 91);
            this.nonBlockingTextBox.TabIndex = 22;
            this.nonBlockingTextBox.WordWrap = false;
            // 
            // nonBlockingLabel
            // 
            this.nonBlockingLabel.AutoSize = true;
            this.nonBlockingLabel.Location = new System.Drawing.Point(12, 108);
            this.nonBlockingLabel.Name = "nonBlockingLabel";
            this.nonBlockingLabel.Size = new System.Drawing.Size(70, 13);
            this.nonBlockingLabel.TabIndex = 21;
            this.nonBlockingLabel.Text = "Non-blocking";
            // 
            // threatsTextBox
            // 
            this.threatsTextBox.Location = new System.Drawing.Point(86, 201);
            this.threatsTextBox.Multiline = true;
            this.threatsTextBox.Name = "threatsTextBox";
            this.threatsTextBox.Size = new System.Drawing.Size(395, 91);
            this.threatsTextBox.TabIndex = 24;
            this.threatsTextBox.WordWrap = false;
            // 
            // ThreatsLabel
            // 
            this.ThreatsLabel.AutoSize = true;
            this.ThreatsLabel.Location = new System.Drawing.Point(12, 204);
            this.ThreatsLabel.Name = "ThreatsLabel";
            this.ThreatsLabel.Size = new System.Drawing.Size(43, 13);
            this.ThreatsLabel.TabIndex = 23;
            this.ThreatsLabel.Text = "Threats";
            // 
            // BackgroundTilesetCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 433);
            this.Controls.Add(this.threatsTextBox);
            this.Controls.Add(this.ThreatsLabel);
            this.Controls.Add(this.nonBlockingTextBox);
            this.Controls.Add(this.nonBlockingLabel);
            this.Controls.Add(this.bgColorPanel);
            this.Controls.Add(this.bgColorLabel);
            this.Controls.Add(this.bgColorTextBox);
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
        private System.Windows.Forms.TextBox bgColorTextBox;
        private System.Windows.Forms.Panel bgColorPanel;
        private System.Windows.Forms.TextBox nonBlockingTextBox;
        private System.Windows.Forms.Label nonBlockingLabel;
        private System.Windows.Forms.TextBox threatsTextBox;
        private System.Windows.Forms.Label ThreatsLabel;
    }
}

