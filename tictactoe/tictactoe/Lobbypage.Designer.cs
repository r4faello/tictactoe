
namespace tictactoe
{
    partial class Lobbypage
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
            this.btnPlay = new System.Windows.Forms.Button();
            this.lblLookingForPlayers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPlay.Location = new System.Drawing.Point(425, 215);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(150, 70);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // lblLookingForPlayers
            // 
            this.lblLookingForPlayers.AutoSize = true;
            this.lblLookingForPlayers.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLookingForPlayers.Location = new System.Drawing.Point(409, 429);
            this.lblLookingForPlayers.Name = "lblLookingForPlayers";
            this.lblLookingForPlayers.Size = new System.Drawing.Size(200, 22);
            this.lblLookingForPlayers.TabIndex = 2;
            this.lblLookingForPlayers.Text = "Looking for players";
            this.lblLookingForPlayers.Visible = false;
            // 
            // Lobbypage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLookingForPlayers);
            this.Controls.Add(this.btnPlay);
            this.Name = "Lobbypage";
            this.Size = new System.Drawing.Size(1000, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblLookingForPlayers;
    }
}
