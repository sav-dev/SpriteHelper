namespace SpriteHelper.Controls
{
    partial class MovementPanel
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
            this.components = new System.ComponentModel.Container();
            this.propertiesLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.movementTypeLabel = new System.Windows.Forms.Label();
            this.xLabel = new System.Windows.Forms.Label();
            this.minTextBox = new System.Windows.Forms.TextBox();
            this.yLabel = new System.Windows.Forms.Label();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.xOffsetLabel = new System.Windows.Forms.Label();
            this.minOffsetTextBox = new System.Windows.Forms.TextBox();
            this.yOffsetLabel = new System.Windows.Forms.Label();
            this.maxOffsetTextBox = new System.Windows.Forms.TextBox();
            this.xTilesLabel = new System.Windows.Forms.Label();
            this.minTilesTextBox = new System.Windows.Forms.TextBox();
            this.yTilesLabel = new System.Windows.Forms.Label();
            this.maxTilesTextBox = new System.Windows.Forms.TextBox();
            this.movementSpeedLabel = new System.Windows.Forms.Label();
            this.initialFlipLabel = new System.Windows.Forms.Label();
            this.initialFlipCheckBox = new System.Windows.Forms.CheckBox();
            this.speedTextBox = new System.Windows.Forms.TextBox();
            this.movementComboBox = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.shouldFlipLabel = new System.Windows.Forms.Label();
            this.shouldFlipCheckBox = new System.Windows.Forms.CheckBox();
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
            this.propertiesLayoutPanel.Controls.Add(this.movementTypeLabel, 0, 0);
            this.propertiesLayoutPanel.Controls.Add(this.xLabel, 0, 4);
            this.propertiesLayoutPanel.Controls.Add(this.minTextBox, 1, 4);
            this.propertiesLayoutPanel.Controls.Add(this.yLabel, 2, 4);
            this.propertiesLayoutPanel.Controls.Add(this.maxTextBox, 3, 4);
            this.propertiesLayoutPanel.Controls.Add(this.xOffsetLabel, 0, 3);
            this.propertiesLayoutPanel.Controls.Add(this.minOffsetTextBox, 1, 3);
            this.propertiesLayoutPanel.Controls.Add(this.yOffsetLabel, 2, 3);
            this.propertiesLayoutPanel.Controls.Add(this.maxOffsetTextBox, 3, 3);
            this.propertiesLayoutPanel.Controls.Add(this.xTilesLabel, 0, 2);
            this.propertiesLayoutPanel.Controls.Add(this.minTilesTextBox, 1, 2);
            this.propertiesLayoutPanel.Controls.Add(this.yTilesLabel, 2, 2);
            this.propertiesLayoutPanel.Controls.Add(this.maxTilesTextBox, 3, 2);
            this.propertiesLayoutPanel.Controls.Add(this.movementSpeedLabel, 2, 1);
            this.propertiesLayoutPanel.Controls.Add(this.initialFlipLabel, 0, 1);
            this.propertiesLayoutPanel.Controls.Add(this.initialFlipCheckBox, 1, 1);
            this.propertiesLayoutPanel.Controls.Add(this.speedTextBox, 3, 1);
            this.propertiesLayoutPanel.Controls.Add(this.movementComboBox, 1, 0);
            this.propertiesLayoutPanel.Controls.Add(this.shouldFlipLabel, 2, 0);
            this.propertiesLayoutPanel.Controls.Add(this.shouldFlipCheckBox, 3, 0);
            this.propertiesLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.propertiesLayoutPanel.Name = "propertiesLayoutPanel";
            this.propertiesLayoutPanel.RowCount = 5;
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.propertiesLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.propertiesLayoutPanel.Size = new System.Drawing.Size(487, 196);
            this.propertiesLayoutPanel.TabIndex = 1;
            // 
            // movementTypeLabel
            // 
            this.movementTypeLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.movementTypeLabel.AutoSize = true;
            this.movementTypeLabel.Location = new System.Drawing.Point(37, 13);
            this.movementTypeLabel.Name = "movementTypeLabel";
            this.movementTypeLabel.Size = new System.Drawing.Size(57, 13);
            this.movementTypeLabel.TabIndex = 15;
            this.movementTypeLabel.Text = "Movement";
            // 
            // xLabel
            // 
            this.xLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(70, 169);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(24, 13);
            this.xLabel.TabIndex = 14;
            this.xLabel.Text = "Min";
            // 
            // minTextBox
            // 
            this.minTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minTextBox.Location = new System.Drawing.Point(120, 166);
            this.minTextBox.Name = "minTextBox";
            this.minTextBox.ReadOnly = true;
            this.minTextBox.Size = new System.Drawing.Size(100, 20);
            this.minTextBox.TabIndex = 4;
            this.minTextBox.Text = "0";
            // 
            // yLabel
            // 
            this.yLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(310, 169);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(27, 13);
            this.yLabel.TabIndex = 13;
            this.yLabel.Text = "Max";
            // 
            // maxTextBox
            // 
            this.maxTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxTextBox.Location = new System.Drawing.Point(363, 166);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.ReadOnly = true;
            this.maxTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxTextBox.TabIndex = 5;
            this.maxTextBox.Text = "0";
            // 
            // xOffsetLabel
            // 
            this.xOffsetLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xOffsetLabel.AutoSize = true;
            this.xOffsetLabel.Location = new System.Drawing.Point(35, 130);
            this.xOffsetLabel.Name = "xOffsetLabel";
            this.xOffsetLabel.Size = new System.Drawing.Size(59, 13);
            this.xOffsetLabel.TabIndex = 11;
            this.xOffsetLabel.Text = "Min (offset)";
            // 
            // minOffsetTextBox
            // 
            this.minOffsetTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minOffsetTextBox.Location = new System.Drawing.Point(120, 126);
            this.minOffsetTextBox.Name = "minOffsetTextBox";
            this.minOffsetTextBox.Size = new System.Drawing.Size(100, 20);
            this.minOffsetTextBox.TabIndex = 2;
            this.minOffsetTextBox.Text = "0";
            this.minOffsetTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // yOffsetLabel
            // 
            this.yOffsetLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yOffsetLabel.AutoSize = true;
            this.yOffsetLabel.Location = new System.Drawing.Point(275, 130);
            this.yOffsetLabel.Name = "yOffsetLabel";
            this.yOffsetLabel.Size = new System.Drawing.Size(62, 13);
            this.yOffsetLabel.TabIndex = 12;
            this.yOffsetLabel.Text = "Max (offset)";
            // 
            // maxOffsetTextBox
            // 
            this.maxOffsetTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxOffsetTextBox.Location = new System.Drawing.Point(363, 126);
            this.maxOffsetTextBox.Name = "maxOffsetTextBox";
            this.maxOffsetTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxOffsetTextBox.TabIndex = 3;
            this.maxOffsetTextBox.Text = "0";
            this.maxOffsetTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // xTilesLabel
            // 
            this.xTilesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.xTilesLabel.AutoSize = true;
            this.xTilesLabel.Location = new System.Drawing.Point(17, 91);
            this.xTilesLabel.Name = "xTilesLabel";
            this.xTilesLabel.Size = new System.Drawing.Size(77, 13);
            this.xTilesLabel.TabIndex = 8;
            this.xTilesLabel.Text = "Min (in bg tiles)";
            // 
            // minTilesTextBox
            // 
            this.minTilesTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minTilesTextBox.Location = new System.Drawing.Point(120, 87);
            this.minTilesTextBox.Name = "minTilesTextBox";
            this.minTilesTextBox.Size = new System.Drawing.Size(100, 20);
            this.minTilesTextBox.TabIndex = 0;
            this.minTilesTextBox.Text = "0";
            this.minTilesTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // yTilesLabel
            // 
            this.yTilesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.yTilesLabel.AutoSize = true;
            this.yTilesLabel.Location = new System.Drawing.Point(257, 91);
            this.yTilesLabel.Name = "yTilesLabel";
            this.yTilesLabel.Size = new System.Drawing.Size(80, 13);
            this.yTilesLabel.TabIndex = 8;
            this.yTilesLabel.Text = "Max (in bg tiles)";
            // 
            // maxTilesTextBox
            // 
            this.maxTilesTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxTilesTextBox.Location = new System.Drawing.Point(363, 87);
            this.maxTilesTextBox.Name = "maxTilesTextBox";
            this.maxTilesTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxTilesTextBox.TabIndex = 1;
            this.maxTilesTextBox.Text = "0";
            this.maxTilesTextBox.TextChanged += new System.EventHandler(this.InputTextChanged);
            // 
            // movementSpeedLabel
            // 
            this.movementSpeedLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.movementSpeedLabel.AutoSize = true;
            this.movementSpeedLabel.Location = new System.Drawing.Point(286, 52);
            this.movementSpeedLabel.Name = "movementSpeedLabel";
            this.movementSpeedLabel.Size = new System.Drawing.Size(51, 13);
            this.movementSpeedLabel.TabIndex = 16;
            this.movementSpeedLabel.Text = "Speed (*)";
            // 
            // initialFlipLabel
            // 
            this.initialFlipLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.initialFlipLabel.AutoSize = true;
            this.initialFlipLabel.Location = new System.Drawing.Point(47, 52);
            this.initialFlipLabel.Name = "initialFlipLabel";
            this.initialFlipLabel.Size = new System.Drawing.Size(47, 13);
            this.initialFlipLabel.TabIndex = 17;
            this.initialFlipLabel.Text = "Initial flip";
            // 
            // initialFlipCheckBox
            // 
            this.initialFlipCheckBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.initialFlipCheckBox.AutoSize = true;
            this.initialFlipCheckBox.Location = new System.Drawing.Point(162, 51);
            this.initialFlipCheckBox.Name = "initialFlipCheckBox";
            this.initialFlipCheckBox.Size = new System.Drawing.Size(15, 14);
            this.initialFlipCheckBox.TabIndex = 18;
            this.initialFlipCheckBox.UseVisualStyleBackColor = true;
            // 
            // speedTextBox
            // 
            this.speedTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.speedTextBox.Location = new System.Drawing.Point(363, 48);
            this.speedTextBox.Name = "speedTextBox";
            this.speedTextBox.Size = new System.Drawing.Size(100, 20);
            this.speedTextBox.TabIndex = 19;
            this.speedTextBox.Text = "0";
            // 
            // movementComboBox
            // 
            this.movementComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.movementComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.movementComboBox.FormattingEnabled = true;
            this.movementComboBox.Location = new System.Drawing.Point(109, 9);
            this.movementComboBox.Name = "movementComboBox";
            this.movementComboBox.Size = new System.Drawing.Size(121, 21);
            this.movementComboBox.TabIndex = 20;
            this.movementComboBox.SelectedIndexChanged += new System.EventHandler(this.MovementComboBoxSelectedIndexChanged);
            // 
            // shouldFlipLabel
            // 
            this.shouldFlipLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.shouldFlipLabel.AutoSize = true;
            this.shouldFlipLabel.Location = new System.Drawing.Point(278, 13);
            this.shouldFlipLabel.Name = "shouldFlipLabel";
            this.shouldFlipLabel.Size = new System.Drawing.Size(59, 13);
            this.shouldFlipLabel.TabIndex = 21;
            this.shouldFlipLabel.Text = "Should Flip";
            // 
            // shouldFlipCheckBox
            // 
            this.shouldFlipCheckBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.shouldFlipCheckBox.AutoSize = true;
            this.shouldFlipCheckBox.Location = new System.Drawing.Point(406, 12);
            this.shouldFlipCheckBox.Name = "shouldFlipCheckBox";
            this.shouldFlipCheckBox.Size = new System.Drawing.Size(15, 14);
            this.shouldFlipCheckBox.TabIndex = 22;
            this.shouldFlipCheckBox.UseVisualStyleBackColor = true;
            // 
            // MovementPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertiesLayoutPanel);
            this.Name = "MovementPanel";
            this.Size = new System.Drawing.Size(487, 196);
            this.propertiesLayoutPanel.ResumeLayout(false);
            this.propertiesLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel propertiesLayoutPanel;
        private System.Windows.Forms.TextBox minTextBox;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label xTilesLabel;
        private System.Windows.Forms.Label xOffsetLabel;
        private System.Windows.Forms.TextBox minTilesTextBox;
        private System.Windows.Forms.TextBox minOffsetTextBox;
        private System.Windows.Forms.TextBox maxTilesTextBox;
        private System.Windows.Forms.TextBox maxOffsetTextBox;
        private System.Windows.Forms.Label yTilesLabel;
        private System.Windows.Forms.Label yOffsetLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.Label movementTypeLabel;
        private System.Windows.Forms.Label movementSpeedLabel;
        private System.Windows.Forms.Label initialFlipLabel;
        private System.Windows.Forms.CheckBox initialFlipCheckBox;
        private System.Windows.Forms.TextBox speedTextBox;
        private System.Windows.Forms.ComboBox movementComboBox;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label shouldFlipLabel;
        private System.Windows.Forms.CheckBox shouldFlipCheckBox;
    }
}
