namespace Stupide_Vautour
{
    partial class MainBoard
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainBoard));
            this.panelCardAnimal = new System.Windows.Forms.PictureBox();
            this.labelScore4 = new System.Windows.Forms.Label();
            this.labelScore1 = new System.Windows.Forms.Label();
            this.labelScore3 = new System.Windows.Forms.Label();
            this.labelScore2 = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelCarteJoue1 = new System.Windows.Forms.PictureBox();
            this.panelCarteJoue3 = new System.Windows.Forms.PictureBox();
            this.panelCarteJoue2 = new System.Windows.Forms.PictureBox();
            this.panelCarteJoue4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelCardAnimal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarteJoue1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarteJoue3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarteJoue2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarteJoue4)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCardAnimal
            // 
            this.panelCardAnimal.Image = global::Stupide_Vautour.Properties.Resources.carte;
            resources.ApplyResources(this.panelCardAnimal, "panelCardAnimal");
            this.panelCardAnimal.Name = "panelCardAnimal";
            this.panelCardAnimal.TabStop = false;
            // 
            // labelScore4
            // 
            resources.ApplyResources(this.labelScore4, "labelScore4");
            this.labelScore4.BackColor = System.Drawing.Color.Transparent;
            this.labelScore4.Name = "labelScore4";
            // 
            // labelScore1
            // 
            resources.ApplyResources(this.labelScore1, "labelScore1");
            this.labelScore1.BackColor = System.Drawing.Color.Transparent;
            this.labelScore1.Name = "labelScore1";
            // 
            // labelScore3
            // 
            resources.ApplyResources(this.labelScore3, "labelScore3");
            this.labelScore3.BackColor = System.Drawing.Color.Transparent;
            this.labelScore3.Name = "labelScore3";
            // 
            // labelScore2
            // 
            resources.ApplyResources(this.labelScore2, "labelScore2");
            this.labelScore2.BackColor = System.Drawing.Color.Transparent;
            this.labelScore2.Name = "labelScore2";
            // 
            // buttonPlay
            // 
            resources.ApplyResources(this.buttonPlay, "buttonPlay");
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.lancerPartie);
            // 
            // panelCarteJoue1
            // 
            resources.ApplyResources(this.panelCarteJoue1, "panelCarteJoue1");
            this.panelCarteJoue1.Name = "panelCarteJoue1";
            this.panelCarteJoue1.TabStop = false;
            // 
            // panelCarteJoue3
            // 
            resources.ApplyResources(this.panelCarteJoue3, "panelCarteJoue3");
            this.panelCarteJoue3.Name = "panelCarteJoue3";
            this.panelCarteJoue3.TabStop = false;
            // 
            // panelCarteJoue2
            // 
            resources.ApplyResources(this.panelCarteJoue2, "panelCarteJoue2");
            this.panelCarteJoue2.Name = "panelCarteJoue2";
            this.panelCarteJoue2.TabStop = false;
            // 
            // panelCarteJoue4
            // 
            resources.ApplyResources(this.panelCarteJoue4, "panelCarteJoue4");
            this.panelCarteJoue4.Name = "panelCarteJoue4";
            this.panelCarteJoue4.TabStop = false;
            // 
            // MainBoard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Stupide_Vautour.Properties.Resources.boardgame;
            this.Controls.Add(this.panelCarteJoue4);
            this.Controls.Add(this.panelCarteJoue2);
            this.Controls.Add(this.panelCarteJoue3);
            this.Controls.Add(this.panelCarteJoue1);
            this.Controls.Add(this.panelCardAnimal);
            this.Controls.Add(this.labelScore2);
            this.Controls.Add(this.labelScore4);
            this.Controls.Add(this.labelScore1);
            this.Controls.Add(this.labelScore3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainBoard";
            ((System.ComponentModel.ISupportInitialize)(this.panelCardAnimal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarteJoue1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarteJoue3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarteJoue2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCarteJoue4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label labelScore4;
        private System.Windows.Forms.Label labelScore1;
        private System.Windows.Forms.Label labelScore3;
        private System.Windows.Forms.Label labelScore2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox panelCardAnimal;
        private System.Windows.Forms.PictureBox panelCarteJoue1;
        private System.Windows.Forms.PictureBox panelCarteJoue3;
        private System.Windows.Forms.PictureBox panelCarteJoue2;
        private System.Windows.Forms.PictureBox panelCarteJoue4;
    }
}

