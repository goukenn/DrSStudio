/* 
-------------------------------------------------------------------
Company: IGKDEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGKDEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGKDEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
App : DrSStudio
powered by IGKDEV 2008-2011
------------------------------------------------------------------- 
*/
  
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
namespace WinSnippetBuilder
{
    partial class UIXNameSelection
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
            this.c_lbName = new System.Windows.Forms.Label();
            this.c_txbName = new System.Windows.Forms.TextBox();
            this.c_btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // c_lbName
            // 
            this.c_lbName.AutoSize = true;
            this.c_lbName.Location = new System.Drawing.Point(21, 25);
            this.c_lbName.Name = "c_lbName";
            this.c_lbName.Size = new System.Drawing.Size(29, 13);
            this.c_lbName.TabIndex = 0;
            this.c_lbName.Text = "Nom";
            // 
            // c_txbName
            // 
            this.c_txbName.Location = new System.Drawing.Point(69, 18);
            this.c_txbName.Name = "c_txbName";
            this.c_txbName.Size = new System.Drawing.Size(221, 20);
            this.c_txbName.TabIndex = 1;
            this.c_txbName.Text = "Name";
            // 
            // c_btnOk
            // 
            this.c_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.c_btnOk.Location = new System.Drawing.Point(215, 60);
            this.c_btnOk.Name = "c_btnOk";
            this.c_btnOk.Size = new System.Drawing.Size(75, 23);
            this.c_btnOk.TabIndex = 2;
            this.c_btnOk.Text = "&OK";
            this.c_btnOk.UseVisualStyleBackColor = true;
            // 
            // UIXNameSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.c_btnOk);
            this.Controls.Add(this.c_txbName);
            this.Controls.Add(this.c_lbName);
            this.Name = "UIXNameSelection";
            this.Size = new System.Drawing.Size(316, 84);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label c_lbName;
        private System.Windows.Forms.TextBox c_txbName;
        private System.Windows.Forms.Button c_btnOk;
    }
}
