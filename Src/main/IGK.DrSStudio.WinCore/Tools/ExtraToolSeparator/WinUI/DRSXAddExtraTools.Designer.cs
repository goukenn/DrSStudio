

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DRSXAddExtraTools.Designer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AddExtraTools.Designer.cs
*/
namespace IGK.DrSStudio.WinUI
{
    partial class DRSXAddExtraTools
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
            this.btn_add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_Ext = new System.Windows.Forms.TextBox();
            this.txb_appPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_add.Location = new System.Drawing.Point(295, 84);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(74, 30);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Extension";
            // 
            // txb_Ext
            // 
            this.txb_Ext.Location = new System.Drawing.Point(87, 9);
            this.txb_Ext.Name = "txb_Ext";
            this.txb_Ext.Size = new System.Drawing.Size(210, 20);
            this.txb_Ext.TabIndex = 2;
            this.txb_Ext.TextChanged += new System.EventHandler(this.txb_Ext_TextChanged);
            // 
            // txb_appPath
            // 
            this.txb_appPath.Location = new System.Drawing.Point(87, 35);
            this.txb_appPath.Name = "txb_appPath";
            this.txb_appPath.ReadOnly = true;
            this.txb_appPath.Size = new System.Drawing.Size(210, 20);
            this.txb_appPath.TabIndex = 4;
            this.txb_appPath.TextChanged += new System.EventHandler(this.txb_appPath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Application";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(303, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 21);
            this.button2.TabIndex = 5;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddExtraTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txb_appPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_Ext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_add);
            this.Name = "AddExtraTools";
            this.Size = new System.Drawing.Size(382, 117);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Ext;
        private System.Windows.Forms.TextBox txb_appPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}

