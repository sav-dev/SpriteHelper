namespace SpriteHelper.Controls
{
    partial class ShootingPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertiesLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.shootingFreqLabel = new System.Windows.Forms.Label();
            this.shootingFreqTextBox = new System.Windows.Forms.TextBox();
            this.initialFreqTextBox = new System.Windows.Forms.TextBox();
            this.inifialFreqLabel = new System.Windows.Forms.Label();
            this.propertiesLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertiesLayoutPanel
            // 
            this.propertiesLayoutPanel.ColumnCount = 4;
            this.propertiesLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.propertiesLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.propertiesLayoutPanel.Controls.Add(this.shootingFreqLabel, 0, 0);
            this.propertiesLayoutPanel.Controls.Add(this.shootingFreqTextBox, 1, 0);
            this.propertiesLayoutPanel.Controls.Add(this.initialFreqTextBox, 3, 0);
            this.propertiesLayoutPanel.Controls.Add(this.inifialFreqLabel, 2, 0);
            this.propertiesLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.propertiesLayoutPanel.Name = "propertiesLayoutPanel";
            this.propertiesLayoutPanel.RowCount = 1;
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.Size = new System.Drawing.Size(470, 39);
            this.propertiesLayoutPanel.TabIndex = 1;
            // 
            // shootingFreqLabel
            // 
            this.shootingFreqLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.shootingFreqLabel.AutoSize = true;
            this.shootingFreqLabel.Location = new System.Drawing.Point(18, 13);
            this.shootingFreqLabel.Name = "shootingFreqLabel";
            this.shootingFreqLabel.Size = new System.Drawing.Size(73, 13);
            this.shootingFreqLabel.TabIndex = 8;
            this.shootingFreqLabel.Text = "Shooting freq,";
            // 
            // shootingFreqTextBox
            // 
            this.shootingFreqTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.shootingFreqTextBox.Location = new System.Drawing.Point(114, 9);
            this.shootingFreqTextBox.Name = "shootingFreqTextBox";
            this.shootingFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.shootingFreqTextBox.TabIndex = 0;
            this.shootingFreqTextBox.Text = "0";
            // 
            // freqOffsetTextBox
            // 
            this.initialFreqTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.initialFreqTextBox.Location = new System.Drawing.Point(349, 9);
            this.initialFreqTextBox.Name = "freqOffsetTextBox";
            this.initialFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.initialFreqTextBox.TabIndex = 1;
            this.initialFreqTextBox.Text = "0";
            // 
            // freqOffsetLabel
            // 
            this.inifialFreqLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.inifialFreqLabel.AutoSize = true;
            this.inifialFreqLabel.Location = new System.Drawing.Point(266, 13);
            this.inifialFreqLabel.Name = "freqOffsetLabel";
            this.inifialFreqLabel.Size = new System.Drawing.Size(60, 13);
            this.inifialFreqLabel.TabIndex = 8;
            this.inifialFreqLabel.Text = "Freq. offset";
            // 
            // ShootingPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertiesLayoutPanel);
            this.Name = "ShootingPanel";
            this.Size = new System.Drawing.Size(470, 39);
            this.propertiesLayoutPanel.ResumeLayout(false);
            this.propertiesLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel propertiesLayoutPanel;
        private System.Windows.Forms.Label shootingFreqLabel;
        private System.Windows.Forms.TextBox shootingFreqTextBox;
        private System.Windows.Forms.TextBox initialFreqTextBox;
        private System.Windows.Forms.Label inifialFreqLabel;
    }
}
