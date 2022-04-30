namespace IGK.DrSStudio.FileBrowser.App
{
    partial class MainForm
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
            this.fbSurface1 = new IGK.DrSStudio.FileBrowser.WinUI.FBSurface();
            this.SuspendLayout();
            // 
            // fbSurface1
            // 
            this.fbSurface1.CaptionKey = null;
            this.fbSurface1.CurrentChild = null;
            this.fbSurface1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fbSurface1.Location = new System.Drawing.Point(0, 0);
            this.fbSurface1.Name = "fbSurface1";
            this.fbSurface1.ParentSurface = null;
            this.fbSurface1.Size = new System.Drawing.Size(743, 261);
            this.fbSurface1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 261);
            this.Controls.Add(this.fbSurface1);
            this.Name = "MainForm";
            this.Text = "File Browser App";
            this.ResumeLayout(false);

        }

        #endregion

        private WinUI.FBSurface fbSurface1;
    }
}

