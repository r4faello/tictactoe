
namespace tictactoe
{
    partial class Game
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
            this.lblXO = new System.Windows.Forms.Label();
            this.lblEnemy = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblXO
            // 
            this.lblXO.AutoSize = true;
            this.lblXO.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblXO.Location = new System.Drawing.Point(369, 24);
            this.lblXO.Name = "lblXO";
            this.lblXO.Size = new System.Drawing.Size(145, 30);
            this.lblXO.TabIndex = 0;
            this.lblXO.Text = "Your position: ";
            // 
            // lblEnemy
            // 
            this.lblEnemy.AutoSize = true;
            this.lblEnemy.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEnemy.Location = new System.Drawing.Point(369, 64);
            this.lblEnemy.Name = "lblEnemy";
            this.lblEnemy.Size = new System.Drawing.Size(86, 30);
            this.lblEnemy.TabIndex = 1;
            this.lblEnemy.Text = "Enemy: ";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblEnemy);
            this.Controls.Add(this.lblXO);
            this.Name = "Game";
            this.Size = new System.Drawing.Size(1000, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblXO;
        private System.Windows.Forms.Label lblEnemy;
    }
}
