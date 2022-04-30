namespace IGK.CssPropertiesBuilder.WinUI
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
            this.c_webBrowser = new IGK.ICore.WinCore.WinUI.Controls.IGKXWebBrowserControl();
            this.SuspendLayout();
            // 
            // c_webBrowser
            // 
            this.c_webBrowser.CaptionKey = null;
            this.c_webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_webBrowser.Location = new System.Drawing.Point(0, 0);
            this.c_webBrowser.Name = "c_webBrowser";
            this.c_webBrowser.Size = new System.Drawing.Size(611, 261);
            this.c_webBrowser.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 261);
            this.Controls.Add(this.c_webBrowser);
            this.Name = "MainForm";
            this.Text = "Css Property Builder. V.1.0";
            this.ResumeLayout(false);

        }

        #endregion

        private ICore.WinCore.WinUI.Controls.IGKXWebBrowserControl c_webBrowser;
    }
}

