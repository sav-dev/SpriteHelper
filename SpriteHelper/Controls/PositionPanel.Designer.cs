namespace SpriteHelper.Controls
{
    partial class PositionPanel
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
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.xLabel = new System.Windows.Forms.Label();
            this.xTilesLabel = new System.Windows.Forms.Label();
            this.xOffsetLabel = new System.Windows.Forms.Label();
            this.xTilesTextBox = new System.Windows.Forms.TextBox();
            this.xOffsetTextBox = new System.Windows.Forms.TextBox();
            this.yTilesTextBox = new System.Windows.Forms.TextBox();
            this.yOffsetTextBox = new System.Windows.Forms.TextBox();
            this.yTilesLabel = new System.Windows.Forms.Label();
            this.yOffsetLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.yTextBox = new System.Windows.Forms.TextBox();
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
            this.propertiesLayoutPanel.Controls.Add(this.xTextBox, 0, 2);
            this.propertiesLayoutPanel.Controls.Add(this.xLabel, 0, 2);
            this.propertiesLayoutPanel.Controls.Add(this.xTilesLabel, 0, 0);
            this.propertiesLayoutPanel.Controls.Add(this.xOffsetLabel, 0, 1);
            this.propertiesLayoutPanel.Controls.Add(this.xTilesTextBox, 1, 0);
            this.propertiesLayoutPanel.Controls.Add(this.xOffsetTextBox, 1, 1);
            this.propertiesLayoutPanel.Controls.Add(this.yTilesTextBox, 3, 0);
            this.propertiesLayoutPanel.Controls.Add(this.yOffsetTextBox, 3, 1);
            this.propertiesLayoutPanel.Controls.Add(this.yTilesLabel, 2, 0);
            this.propertiesLayoutPanel.Controls.Add(this.yOffsetLabel, 2, 1);
            this.propertiesLayoutPanel.Controls.Add(this.yLabel, 2, 2);
            this.propertiesLayoutPanel.Controls.Add(this.yTextBox, 3, 2);
            this.propertiesLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.propertiesLayoutPanel.Name = "propertiesLayoutPanel";
            this.propertiesLayoutPanel.RowCount = 3;
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.propertiesLayoutPanel.Size = new System.Drawing.Size(375, 99);
            this.propertiesLayoutPanel.TabIndex = 0;
            // 
            // xTextBox
            // 
            this.xTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.xTextBox.Location = new System.Drawing.Point(81, 72);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.ReadOnly = true;
            this.xTextBox.Size = new System.Drawing.Size(100, 20);
            this.xTextBox.TabIndex = 4;
            this.xTextBox.Text = "0";
            // 
            // xLabel
            // 
            this.xLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(58, 76);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(14, 13);
            this.xLabel.TabIndex = 14;
            this.xLabel.Text = "X";
            // 
            // xTilesLabel
            // 
            this.xTilesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xTilesLabel.AutoSize = true;
            this.xTilesLabel.Location = new System.Drawing.Point(5, 10);
            this.xTilesLabel.Name = "xTilesLabel";
            this.xTilesLabel.Size = new System.Drawing.Size(67, 13);
            this.xTilesLabel.TabIndex = 8;
            this.xTilesLabel.Text = "X (in bg tiles)";
            // 
            // xOffsetLabel
            // 
            this.xOffsetLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xOffsetLabel.AutoSize = true;
            this.xOffsetLabel.Location = new System.Drawing.Point(23, 43);
            this.xOffsetLabel.Name = "xOffsetLabel";
            this.xOffsetLabel.Size = new System.Drawing.Size(49, 13);
            this.xOffsetLabel.TabIndex = 11;
            this.xOffsetLabel.Text = "X (offset)";
            // 
            // xTilesTextBox
            // 
            this.xTilesTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.xTilesTextBox.Location = new System.Drawing.Point(81, 6);
            this.xTilesTextBox.Name = "xTilesTextBox";
            this.xTilesTextBox.Size = new System.Drawing.Size(100, 20);
            this.xTilesTextBox.TabIndex = 0;
            this.xTilesTextBox.Text = "0";
            this.xTilesTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // xOffsetTextBox
            // 
            this.xOffsetTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.xOffsetTextBox.Location = new System.Drawing.Point(81, 39);
            this.xOffsetTextBox.Name = "xOffsetTextBox";
            this.xOffsetTextBox.Size = new System.Drawing.Size(100, 20);
            this.xOffsetTextBox.TabIndex = 2;
            this.xOffsetTextBox.Text = "0";
            this.xOffsetTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // yTilesTextBox
            // 
            this.yTilesTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.yTilesTextBox.Location = new System.Drawing.Point(268, 6);
            this.yTilesTextBox.Name = "yTilesTextBox";
            this.yTilesTextBox.Size = new System.Drawing.Size(100, 20);
            this.yTilesTextBox.TabIndex = 1;
            this.yTilesTextBox.Text = "0";
            this.yTilesTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // yOffsetTextBox
            // 
            this.yOffsetTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.yOffsetTextBox.Location = new System.Drawing.Point(268, 39);
            this.yOffsetTextBox.Name = "yOffsetTextBox";
            this.yOffsetTextBox.Size = new System.Drawing.Size(100, 20);
            this.yOffsetTextBox.TabIndex = 3;
            this.yOffsetTextBox.Text = "0";
            this.yOffsetTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // yTilesLabel
            // 
            this.yTilesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yTilesLabel.AutoSize = true;
            this.yTilesLabel.Location = new System.Drawing.Point(192, 10);
            this.yTilesLabel.Name = "yTilesLabel";
            this.yTilesLabel.Size = new System.Drawing.Size(67, 13);
            this.yTilesLabel.TabIndex = 8;
            this.yTilesLabel.Text = "Y (in bg tiles)";
            // 
            // yOffsetLabel
            // 
            this.yOffsetLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yOffsetLabel.AutoSize = true;
            this.yOffsetLabel.Location = new System.Drawing.Point(210, 43);
            this.yOffsetLabel.Name = "yOffsetLabel";
            this.yOffsetLabel.Size = new System.Drawing.Size(49, 13);
            this.yOffsetLabel.TabIndex = 12;
            this.yOffsetLabel.Text = "Y (offset)";
            // 
            // yLabel
            // 
            this.yLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(245, 76);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(14, 13);
            this.yLabel.TabIndex = 13;
            this.yLabel.Text = "Y";
            // 
            // yTextBox
            // 
            this.yTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.yTextBox.Location = new System.Drawing.Point(268, 72);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.ReadOnly = true;
            this.yTextBox.Size = new System.Drawing.Size(100, 20);
            this.yTextBox.TabIndex = 5;
            this.yTextBox.Text = "0";
            // 
            // PositionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertiesLayoutPanel);
            this.Name = "PositionPanel";
            this.Size = new System.Drawing.Size(375, 99);
            this.propertiesLayoutPanel.ResumeLayout(false);
            this.propertiesLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel propertiesLayoutPanel;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label xTilesLabel;
        private System.Windows.Forms.Label xOffsetLabel;
        private System.Windows.Forms.TextBox xTilesTextBox;
        private System.Windows.Forms.TextBox xOffsetTextBox;
        private System.Windows.Forms.TextBox yTilesTextBox;
        private System.Windows.Forms.TextBox yOffsetTextBox;
        private System.Windows.Forms.Label yTilesLabel;
        private System.Windows.Forms.Label yOffsetLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.TextBox yTextBox;
    }
}
