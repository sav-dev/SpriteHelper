namespace SpriteHelper.Controls
{
    partial class BlinkingPanel
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
            this.blinkingTypeComboBox = new System.Windows.Forms.ComboBox();
            this.blinkingFreqLabel = new System.Windows.Forms.Label();
            this.blinkingFreqTextBox = new System.Windows.Forms.TextBox();
            this.initialFreqTextBox = new System.Windows.Forms.TextBox();
            this.inifialFreqLabel = new System.Windows.Forms.Label();
            this.blinkingTypeLabel = new System.Windows.Forms.Label();
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
            this.propertiesLayoutPanel.Controls.Add(this.blinkingTypeComboBox, 1, 0);
            this.propertiesLayoutPanel.Controls.Add(this.blinkingFreqLabel, 0, 1);
            this.propertiesLayoutPanel.Controls.Add(this.blinkingFreqTextBox, 1, 1);
            this.propertiesLayoutPanel.Controls.Add(this.initialFreqTextBox, 3, 1);
            this.propertiesLayoutPanel.Controls.Add(this.inifialFreqLabel, 2, 1);
            this.propertiesLayoutPanel.Controls.Add(this.blinkingTypeLabel, 0, 0);
            this.propertiesLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.propertiesLayoutPanel.Name = "propertiesLayoutPanel";
            this.propertiesLayoutPanel.RowCount = 2;
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.Size = new System.Drawing.Size(437, 150);
            this.propertiesLayoutPanel.TabIndex = 2;
            // 
            // blinkingTypeComboBox
            // 
            this.blinkingTypeComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.blinkingTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.blinkingTypeComboBox.FormattingEnabled = true;
            this.blinkingTypeComboBox.Location = new System.Drawing.Point(90, 27);
            this.blinkingTypeComboBox.Name = "blinkingTypeComboBox";
            this.blinkingTypeComboBox.Size = new System.Drawing.Size(125, 21);
            this.blinkingTypeComboBox.TabIndex = 3;
            this.blinkingTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.BlinkingTypeComboBoxSelectedIndexChanged);
            // 
            // blinkingFreqLabel
            // 
            this.blinkingFreqLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.blinkingFreqLabel.AutoSize = true;
            this.blinkingFreqLabel.Location = new System.Drawing.Point(16, 106);
            this.blinkingFreqLabel.Name = "blinkingFreqLabel";
            this.blinkingFreqLabel.Size = new System.Drawing.Size(68, 13);
            this.blinkingFreqLabel.TabIndex = 9;
            this.blinkingFreqLabel.Text = "Blinking freq,";
            // 
            // blinkingFreqTextBox
            // 
            this.blinkingFreqTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.blinkingFreqTextBox.Location = new System.Drawing.Point(102, 102);
            this.blinkingFreqTextBox.Name = "blinkingFreqTextBox";
            this.blinkingFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.blinkingFreqTextBox.TabIndex = 0;
            this.blinkingFreqTextBox.Text = "0";
            // 
            // initialFreqTextBox
            // 
            this.initialFreqTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.initialFreqTextBox.Location = new System.Drawing.Point(321, 102);
            this.initialFreqTextBox.Name = "initialFreqTextBox";
            this.initialFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.initialFreqTextBox.TabIndex = 1;
            this.initialFreqTextBox.Text = "0";
            // 
            // inifialFreqLabel
            // 
            this.inifialFreqLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.inifialFreqLabel.AutoSize = true;
            this.inifialFreqLabel.Location = new System.Drawing.Point(247, 106);
            this.inifialFreqLabel.Name = "inifialFreqLabel";
            this.inifialFreqLabel.Size = new System.Drawing.Size(55, 13);
            this.inifialFreqLabel.TabIndex = 8;
            this.inifialFreqLabel.Text = "Initial freq.";
            // 
            // blinkingTypeLabel
            // 
            this.blinkingTypeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.blinkingTypeLabel.AutoSize = true;
            this.blinkingTypeLabel.Location = new System.Drawing.Point(17, 31);
            this.blinkingTypeLabel.Name = "blinkingTypeLabel";
            this.blinkingTypeLabel.Size = new System.Drawing.Size(67, 13);
            this.blinkingTypeLabel.TabIndex = 8;
            this.blinkingTypeLabel.Text = "Blinking type";
            // 
            // BlinkingPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertiesLayoutPanel);
            this.Name = "BlinkingPanel";
            this.Size = new System.Drawing.Size(437, 150);
            this.propertiesLayoutPanel.ResumeLayout(false);
            this.propertiesLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel propertiesLayoutPanel;
        private System.Windows.Forms.Label blinkingTypeLabel;
        private System.Windows.Forms.TextBox blinkingFreqTextBox;
        private System.Windows.Forms.TextBox initialFreqTextBox;
        private System.Windows.Forms.Label inifialFreqLabel;
        private System.Windows.Forms.Label blinkingFreqLabel;
        private System.Windows.Forms.ComboBox blinkingTypeComboBox;
    }
}
