namespace SpriteHelper.Dialogs
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
            this.playerButton = new System.Windows.Forms.Button();
            this.levelEditorButton = new System.Windows.Forms.Button();
            this.palettesButton = new System.Windows.Forms.Button();
            this.backgroundButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.animationsButton = new System.Windows.Forms.Button();
            this.chrButton = new System.Windows.Forms.Button();
            this.enemiesButton = new System.Windows.Forms.Button();
            this.explosionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playerButton
            // 
            this.playerButton.Location = new System.Drawing.Point(35, 12);
            this.playerButton.Name = "playerButton";
            this.playerButton.Size = new System.Drawing.Size(172, 23);
            this.playerButton.TabIndex = 0;
            this.playerButton.Text = "Player";
            this.playerButton.UseVisualStyleBackColor = true;
            this.playerButton.Click += new System.EventHandler(this.PlayerButtonClick);
            // 
            // levelEditorButton
            // 
            this.levelEditorButton.Location = new System.Drawing.Point(35, 236);
            this.levelEditorButton.Name = "levelEditorButton";
            this.levelEditorButton.Size = new System.Drawing.Size(172, 23);
            this.levelEditorButton.TabIndex = 1;
            this.levelEditorButton.Text = "Level Editor";
            this.levelEditorButton.UseVisualStyleBackColor = true;
            this.levelEditorButton.Click += new System.EventHandler(this.LevelEditorButtonClick);
            // 
            // palettesButton
            // 
            this.palettesButton.Location = new System.Drawing.Point(35, 172);
            this.palettesButton.Name = "palettesButton";
            this.palettesButton.Size = new System.Drawing.Size(172, 23);
            this.palettesButton.TabIndex = 2;
            this.palettesButton.Text = "Palettes";
            this.palettesButton.UseVisualStyleBackColor = true;
            this.palettesButton.Click += new System.EventHandler(this.PalettesButtonClick);
            // 
            // backgroundButton
            // 
            this.backgroundButton.Location = new System.Drawing.Point(35, 204);
            this.backgroundButton.Name = "backgroundButton";
            this.backgroundButton.Size = new System.Drawing.Size(172, 23);
            this.backgroundButton.TabIndex = 3;
            this.backgroundButton.Text = "Background";
            this.backgroundButton.UseVisualStyleBackColor = true;
            this.backgroundButton.Click += new System.EventHandler(this.BackgroundButtonClick);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(35, 303);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(172, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // animationsButton
            // 
            this.animationsButton.Location = new System.Drawing.Point(35, 140);
            this.animationsButton.Name = "animationsButton";
            this.animationsButton.Size = new System.Drawing.Size(172, 23);
            this.animationsButton.TabIndex = 5;
            this.animationsButton.Text = "Animations";
            this.animationsButton.UseVisualStyleBackColor = true;
            this.animationsButton.Click += new System.EventHandler(this.AnimationsButtonClick);
            // 
            // chrButton
            // 
            this.chrButton.Location = new System.Drawing.Point(35, 108);
            this.chrButton.Name = "chrButton";
            this.chrButton.Size = new System.Drawing.Size(172, 23);
            this.chrButton.TabIndex = 6;
            this.chrButton.Text = "CHR combine";
            this.chrButton.UseVisualStyleBackColor = true;
            this.chrButton.Click += new System.EventHandler(this.ChrButtonClick);
            // 
            // enemiesButton
            // 
            this.enemiesButton.Location = new System.Drawing.Point(35, 76);
            this.enemiesButton.Name = "enemiesButton";
            this.enemiesButton.Size = new System.Drawing.Size(172, 23);
            this.enemiesButton.TabIndex = 7;
            this.enemiesButton.Text = "Enemies";
            this.enemiesButton.UseVisualStyleBackColor = true;
            this.enemiesButton.Click += new System.EventHandler(this.EnemiesButtonClick);
            // 
            // explosionButton
            // 
            this.explosionButton.Location = new System.Drawing.Point(35, 44);
            this.explosionButton.Name = "explosionButton";
            this.explosionButton.Size = new System.Drawing.Size(172, 23);
            this.explosionButton.TabIndex = 8;
            this.explosionButton.Text = "Explosion";
            this.explosionButton.UseVisualStyleBackColor = true;
            this.explosionButton.Click += new System.EventHandler(this.ExplosionButtonClick);
            // 
            // ProgramPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 371);
            this.Controls.Add(this.explosionButton);
            this.Controls.Add(this.enemiesButton);
            this.Controls.Add(this.chrButton);
            this.Controls.Add(this.animationsButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.backgroundButton);
            this.Controls.Add(this.palettesButton);
            this.Controls.Add(this.levelEditorButton);
            this.Controls.Add(this.playerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgramPicker";
            this.Text = "ProgramPicker";
            this.Load += new System.EventHandler(this.ProgramPickerLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playerButton;
        private System.Windows.Forms.Button levelEditorButton;
        private System.Windows.Forms.Button palettesButton;
        private System.Windows.Forms.Button backgroundButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button animationsButton;
        private System.Windows.Forms.Button chrButton;
        private System.Windows.Forms.Button enemiesButton;
        private System.Windows.Forms.Button explosionButton;
    }
}