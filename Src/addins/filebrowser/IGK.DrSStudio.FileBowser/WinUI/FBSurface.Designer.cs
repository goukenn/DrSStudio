namespace IGK.DrSStudio.FileBrowser.WinUI
{
    partial class FBSurface
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
            this.components = new System.ComponentModel.Container();
            this.c_preview = new IGK.DrSStudio.FileBrowser.WinUI.FBPreview();
            this.c_pan_left = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.c_trv_explorer = new System.Windows.Forms.TreeView();
            this.c_treeview_imglist = new System.Windows.Forms.ImageList(this.components);
            this.c_pan_f = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.c_lst_file = new System.Windows.Forms.ListView();
            this.c_pan_right = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.c_list_imglist = new System.Windows.Forms.ImageList(this.components);
            this.c_pan_left.SuspendLayout();
            this.c_pan_f.SuspendLayout();
            this.c_pan_right.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_preview
            // 
            this.c_preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_preview.FileName = null;
            this.c_preview.Location = new System.Drawing.Point(0, 0);
            this.c_preview.Name = "c_preview";
            this.c_preview.Size = new System.Drawing.Size(169, 331);
            this.c_preview.TabIndex = 0;
            // 
            // c_pan_left
            // 
            this.c_pan_left.CaptionKey = null;
            this.c_pan_left.Controls.Add(this.c_trv_explorer);
            this.c_pan_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_pan_left.Location = new System.Drawing.Point(0, 0);
            this.c_pan_left.Name = "c_pan_left";
            this.c_pan_left.Size = new System.Drawing.Size(169, 331);
            this.c_pan_left.TabIndex = 0;
            // 
            // c_trv_explorer
            // 
            this.c_trv_explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_trv_explorer.ImageIndex = 0;
            this.c_trv_explorer.ImageList = this.c_treeview_imglist;
            this.c_trv_explorer.Location = new System.Drawing.Point(0, 0);
            this.c_trv_explorer.Name = "c_trv_explorer";
            this.c_trv_explorer.SelectedImageIndex = 0;
            this.c_trv_explorer.Size = new System.Drawing.Size(169, 331);
            this.c_trv_explorer.TabIndex = 0;
            // 
            // c_treeview_imglist
            // 
            this.c_treeview_imglist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.c_treeview_imglist.ImageSize = new System.Drawing.Size(16, 16);
            this.c_treeview_imglist.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // c_pan_f
            // 
            this.c_pan_f.CaptionKey = null;
            this.c_pan_f.Controls.Add(this.c_lst_file);
            this.c_pan_f.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_f.Location = new System.Drawing.Point(172, 0);
            this.c_pan_f.Name = "c_pan_f";
            this.c_pan_f.Size = new System.Drawing.Size(353, 331);
            this.c_pan_f.TabIndex = 1;
            // 
            // c_lst_file
            // 
            this.c_lst_file.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_lst_file.LargeImageList = this.c_list_imglist;
            this.c_lst_file.Location = new System.Drawing.Point(0, 0);
            this.c_lst_file.Name = "c_lst_file";
            this.c_lst_file.Size = new System.Drawing.Size(353, 331);
            this.c_lst_file.SmallImageList = this.c_list_imglist;
            this.c_lst_file.TabIndex = 0;
            this.c_lst_file.UseCompatibleStateImageBehavior = false;
            // 
            // c_pan_right
            // 
            this.c_pan_right.CaptionKey = null;
            this.c_pan_right.Controls.Add(this.c_preview);
            this.c_pan_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.c_pan_right.Location = new System.Drawing.Point(528, 0);
            this.c_pan_right.Name = "c_pan_right";
            this.c_pan_right.Size = new System.Drawing.Size(169, 331);
            this.c_pan_right.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(525, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 331);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(169, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 331);
            this.splitter2.TabIndex = 0;
            this.splitter2.TabStop = false;
            // 
            // c_list_imglist
            // 
            this.c_list_imglist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.c_list_imglist.ImageSize = new System.Drawing.Size(32, 32);
            this.c_list_imglist.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FBSurface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.c_pan_f);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.c_pan_right);
            this.Controls.Add(this.c_pan_left);
            this.Name = "FBSurface";
            this.Size = new System.Drawing.Size(697, 331);
            this.c_pan_left.ResumeLayout(false);
            this.c_pan_f.ResumeLayout(false);
            this.c_pan_right.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ICore.WinCore.WinUI.Controls.IGKXPanel c_pan_left;
        private ICore.WinCore.WinUI.Controls.IGKXPanel c_pan_f;
        private ICore.WinCore.WinUI.Controls.IGKXPanel c_pan_right;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TreeView c_trv_explorer;
        private System.Windows.Forms.ListView c_lst_file;
        private System.Windows.Forms.ImageList c_treeview_imglist;
        private System.Windows.Forms.ImageList c_list_imglist;
    }
}
