namespace SpriteHelper.Controls
{
    partial class FlipPanel
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
            this.initialFlipLabel = new System.Windows.Forms.Label();
            this.shouldFlipLabel = new System.Windows.Forms.Label();
            this.shouldFlipCheckBox = new System.Windows.Forms.CheckBox();
            this.initialFlipCheckBox = new System.Windows.Forms.CheckBox();
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
            this.propertiesLayoutPanel.Controls.Add(this.initialFlipLabel, 0, 0);
            this.propertiesLayoutPanel.Controls.Add(this.shouldFlipLabel, 2, 0);
            this.propertiesLayoutPanel.Controls.Add(this.shouldFlipCheckBox, 3, 0);
            this.propertiesLayoutPanel.Controls.Add(this.initialFlipCheckBox, 1, 0);
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
            // initialFlipLabel
            // 
            this.initialFlipLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.initialFlipLabel.AutoSize = true;
            this.initialFlipLabel.Location = new System.Drawing.Point(41, 13);
            this.initialFlipLabel.Name = "initialFlipLabel";
            this.initialFlipLabel.Size = new System.Drawing.Size(50, 13);
            this.initialFlipLabel.TabIndex = 8;
            this.initialFlipLabel.Text = "Initial Flip";
            // 
            // shouldFlipLabel
            // 
            this.shouldFlipLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.shouldFlipLabel.AutoSize = true;
            this.shouldFlipLabel.Location = new System.Drawing.Point(267, 13);
            this.shouldFlipLabel.Name = "shouldFlipLabel";
            this.shouldFlipLabel.Size = new System.Drawing.Size(59, 13);
            this.shouldFlipLabel.TabIndex = 8;
            this.shouldFlipLabel.Text = "Should Flip";
            // 
            // shouldFlipCheckBox
            // 
            this.shouldFlipCheckBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.shouldFlipCheckBox.AutoSize = true;
            this.shouldFlipCheckBox.Location = new System.Drawing.Point(392, 12);
            this.shouldFlipCheckBox.Name = "shouldFlipCheckBox";
            this.shouldFlipCheckBox.Size = new System.Drawing.Size(15, 14);
            this.shouldFlipCheckBox.TabIndex = 9;
            this.shouldFlipCheckBox.UseVisualStyleBackColor = true;
            // 
            // initialFlipCheckBox
            // 
            this.initialFlipCheckBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.initialFlipCheckBox.AutoSize = true;
            this.initialFlipCheckBox.Location = new System.Drawing.Point(157, 12);
            this.initialFlipCheckBox.Name = "initialFlipCheckBox";
            this.initialFlipCheckBox.Size = new System.Drawing.Size(15, 14);
            this.initialFlipCheckBox.TabIndex = 10;
            this.initialFlipCheckBox.UseVisualStyleBackColor = true;
            // 
            // FlipPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertiesLayoutPanel);
            this.Name = "FlipPanel";
            this.Size = new System.Drawing.Size(470, 39);
            this.propertiesLayoutPanel.ResumeLayout(false);
            this.propertiesLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel propertiesLayoutPanel;
        private System.Windows.Forms.Label initialFlipLabel;
        private System.Windows.Forms.Label shouldFlipLabel;
        private System.Windows.Forms.CheckBox shouldFlipCheckBox;
        private System.Windows.Forms.CheckBox initialFlipCheckBox;
    }
}
