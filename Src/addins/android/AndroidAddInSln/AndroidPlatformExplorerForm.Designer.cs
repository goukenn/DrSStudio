namespace AndroidAddInSln
{
    partial class AndroidPlatformExplorerForm
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
            this.c_surface = new IGK.DrSStudio.Android.WinUI.AndroidSdkManagerSurface();
            this.SuspendLayout();
            // 
            // c_surface
            // 
            this.c_surface.CaptionKey = null;
            this.c_surface.CurrentChild = null;
            this.c_surface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_surface.Location = new System.Drawing.Point(0, 0);
            this.c_surface.Name = "c_surface";
            this.c_surface.ParentSurface = null;
            this.c_surface.Project = null;
            this.c_surface.Size = new System.Drawing.Size(636, 393);
            this.c_surface.TabIndex = 0;
            // 
            // AndroidPlatformExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 393);
            this.Controls.Add(this.c_surface);
            this.Name = "AndroidPlatformExplorerForm";
            this.Text = "Android Plateform Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

