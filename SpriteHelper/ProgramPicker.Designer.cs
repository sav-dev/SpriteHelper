namespace SpriteHelper
{
    partial class ProgramPicker
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
            this.animationButton = new System.Windows.Forms.Button();
            this.levelEditorButton = new System.Windows.Forms.Button();
            this.palettesButton = new System.Windows.Forms.Button();
            this.backgroundButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // animationButton
            // 
            this.animationButton.Location = new System.Drawing.Point(35, 12);
            this.animationButton.Name = "animationButton";
            this.animationButton.Size = new System.Drawing.Size(172, 23);
            this.animationButton.TabIndex = 0;
            this.animationButton.Text = "Animation";
            this.animationButton.UseVisualStyleBackColor = true;
            this.animationButton.Click += new System.EventHandler(this.AnimationButtonClick);
            // 
            // levelEditorButton
            // 
            this.levelEditorButton.Location = new System.Drawing.Point(35, 123);
            this.levelEditorButton.Name = "levelEditorButton";
            this.levelEditorButton.Size = new System.Drawing.Size(172, 23);
            this.levelEditorButton.TabIndex = 1;
            this.levelEditorButton.Text = "Level Editor";
            this.levelEditorButton.UseVisualStyleBackColor = true;
            this.levelEditorButton.Click += new System.EventHandler(this.LevelEditorButtonClick);
            // 
            // palettesButton
            // 
            this.palettesButton.Location = new System.Drawing.Point(35, 49);
            this.palettesButton.Name = "palettesButton";
            this.palettesButton.Size = new System.Drawing.Size(172, 23);
            this.palettesButton.TabIndex = 2;
            this.palettesButton.Text = "Palettes";
            this.palettesButton.UseVisualStyleBackColor = true;
            this.palettesButton.Click += new System.EventHandler(this.PalettesButtonClick);
            // 
            // backgroundButton
            // 
            this.backgroundButton.Location = new System.Drawing.Point(35, 86);
            this.backgroundButton.Name = "backgroundButton";
            this.backgroundButton.Size = new System.Drawing.Size(172, 23);
            this.backgroundButton.TabIndex = 3;
            this.backgroundButton.Text = "Background";
            this.backgroundButton.UseVisualStyleBackColor = true;
            this.backgroundButton.Click += new System.EventHandler(this.BackgroundButtonClick);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(35, 187);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(172, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // ProgramPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 222);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.backgroundButton);
            this.Controls.Add(this.palettesButton);
            this.Controls.Add(this.levelEditorButton);
            this.Controls.Add(this.animationButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgramPicker";
            this.Text = "ProgramPicker";
            this.Load += new System.EventHandler(this.ProgramPickerLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button animationButton;
        private System.Windows.Forms.Button levelEditorButton;
        private System.Windows.Forms.Button palettesButton;
        private System.Windows.Forms.Button backgroundButton;
        private System.Windows.Forms.Button closeButton;
    }
}