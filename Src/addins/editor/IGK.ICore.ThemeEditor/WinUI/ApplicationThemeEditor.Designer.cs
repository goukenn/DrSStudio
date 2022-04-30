using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.SkinEditorAddin.WinUI
{
    partial class ApplicationThemeEditor
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.c_pan_left = new IGKXPanel();
            this.c_lsb_items = new System.Windows.Forms.ListBox();
            this.c_pan_content = new IGKXPanel();
            this.c_btn_apply = new IGKXButton();
            this.c_btn_save = new IGKXButton();
            this.c_splitter = new IGKXSplitter();
            this.igkxButton1 = new IGKXButton();
            this.c_pan_left.SuspendLayout();
            this.c_pan_content.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_pan_left
            // 
            this.c_pan_left.CaptionKey = null;
            this.c_pan_left.Controls.Add(this.c_lsb_items);
            this.c_pan_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_pan_left.Location = new System.Drawing.Point(0, 0);
            this.c_pan_left.Name = "c_pan_left";
            this.c_pan_left.Size = new System.Drawing.Size(221, 303);
            this.c_pan_left.TabIndex = 0;
            // 
            // c_lsb_items
            // 
            this.c_lsb_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_lsb_items.FormattingEnabled = true;
            this.c_lsb_items.Location = new System.Drawing.Point(0, 0);
            this.c_lsb_items.Name = "c_lsb_items";
            this.c_lsb_items.Size = new System.Drawing.Size(221, 303);
            this.c_lsb_items.TabIndex = 0;
            // 
            // c_pan_content
            // 
            this.c_pan_content.CaptionKey = null;
            this.c_pan_content.Controls.Add(this.c_btn_apply);
            this.c_pan_content.Controls.Add(this.c_btn_save);
            this.c_pan_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_content.Location = new System.Drawing.Point(223, 0);
            this.c_pan_content.Name = "c_pan_content";
            this.c_pan_content.Size = new System.Drawing.Size(340, 303);
            this.c_pan_content.TabIndex = 1;
            // 
            // c_btn_apply
            // 
            this.c_btn_apply.CaptionKey = "btn.apply";
            this.c_btn_apply.Checked = false;
            this.c_btn_apply.DialogResult = IGK.ICore.WinUI.enuDialogResult.None;
            this.c_btn_apply.Location = new System.Drawing.Point(6, 64);
            this.c_btn_apply.Name = "c_btn_apply";
            this.c_btn_apply.ShowButtonImage = false;
            this.c_btn_apply.Size = new System.Drawing.Size(143, 32);
            this.c_btn_apply.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_apply.TabIndex = 1;
            this.c_btn_apply.Click += new System.EventHandler(this.c_btn_apply_Click);
            // 
            // c_btn_save
            // 
            this.c_btn_save.CaptionKey = "btn.save";
            this.c_btn_save.Checked = false;
            this.c_btn_save.DialogResult = IGK.ICore.WinUI.enuDialogResult.None;
            this.c_btn_save.Location = new System.Drawing.Point(6, 16);
            this.c_btn_save.Name = "c_btn_save";
            this.c_btn_save.ShowButtonImage = false;
            this.c_btn_save.Size = new System.Drawing.Size(143, 32);
            this.c_btn_save.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_save.TabIndex = 0;
            this.c_btn_save.Click += new System.EventHandler(this.c_btn_save_Click);
            // 
            // c_splitter
            // 
            this.c_splitter.Location = new System.Drawing.Point(221, 0);
            this.c_splitter.Name = "c_splitter";
            this.c_splitter.Size = new System.Drawing.Size(2, 303);
            this.c_splitter.TabIndex = 0;
            this.c_splitter.TabStop = false;
            // 
            // igkxButton1
            // 
            this.igkxButton1.CaptionKey = null;
            this.igkxButton1.Checked = false;
            this.igkxButton1.DialogResult = IGK.ICore.WinUI.enuDialogResult.None;
            this.igkxButton1.Location = new System.Drawing.Point(0, 0);
            this.igkxButton1.Name = "igkxButton1";
            this.igkxButton1.ShowButtonImage = false;
            this.igkxButton1.Size = new System.Drawing.Size(32, 32);
            this.igkxButton1.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.igkxButton1.TabIndex = 0;
            // 
            // ApplicationThemeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.c_pan_content);
            this.Controls.Add(this.c_splitter);
            this.Controls.Add(this.c_pan_left);
            this.Name = "ApplicationThemeEditor";
            this.Size = new System.Drawing.Size(563, 303);
            this.c_pan_left.ResumeLayout(false);
            this.c_pan_content.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private IGKXPanel c_pan_left;
        private IGKXPanel c_pan_content;
        private IGKXSplitter c_splitter;
        private System.Windows.Forms.ListBox c_lsb_items;
        private IGKXButton igkxButton1;
        private IGKXButton c_btn_apply;
        private IGKXButton c_btn_save;
    }
}
