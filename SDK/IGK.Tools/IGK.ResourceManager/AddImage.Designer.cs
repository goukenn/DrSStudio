/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
namespace IGK.ResMan
{
    partial class AddImage
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
            this.bt_close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_Value = new System.Windows.Forms.TextBox();
            this.txb_Title = new System.Windows.Forms.TextBox();
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_add = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_close
            // 
            this.bt_close.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_close.Location = new System.Drawing.Point(230, 61);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(65, 23);
            this.bt_close.TabIndex = 9;
            this.bt_close.Text = "&Close";
            this.bt_close.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Value";
            // 
            // txb_Value
            // 
            this.txb_Value.Location = new System.Drawing.Point(104, 35);
            this.txb_Value.Name = "txb_Value";
            this.txb_Value.ReadOnly = true;
            this.txb_Value.Size = new System.Drawing.Size(191, 20);
            this.txb_Value.TabIndex = 6;
            // 
            // txb_Title
            // 
            this.txb_Title.Location = new System.Drawing.Point(104, 8);
            this.txb_Title.Name = "txb_Title";
            this.txb_Title.Size = new System.Drawing.Size(191, 20);
            this.txb_Title.TabIndex = 5;
            // 
            // lb_title
            // 
            this.lb_title.AutoSize = true;
            this.lb_title.Location = new System.Drawing.Point(7, 16);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(27, 13);
            this.lb_title.TabIndex = 7;
            this.lb_title.Text = "Title";
            // 
            // bt_add
            // 
            this.bt_add.Location = new System.Drawing.Point(149, 61);
            this.bt_add.Name = "bt_add";
            this.bt_add.Size = new System.Drawing.Size(75, 23);
            this.bt_add.TabIndex = 8;
            this.bt_add.Text = "&Add";
            this.bt_add.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bt_close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_Value);
            this.Controls.Add(this.txb_Title);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.bt_add);
            this.Name = "AddImage";
            this.Size = new System.Drawing.Size(319, 103);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Value;
        private System.Windows.Forms.TextBox txb_Title;
        private System.Windows.Forms.Label lb_title;
        internal System.Windows.Forms.Button bt_add;
        private System.Windows.Forms.Button button1;
    }
}
