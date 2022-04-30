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
    partial class WinDeclarationDesigner
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c_lsbDeclaration = new System.Windows.Forms.ListBox();
            this.c_btnAdd = new System.Windows.Forms.Button();
            this.c_btnRemove = new System.Windows.Forms.Button();
            this.c_prGrid = new System.Windows.Forms.PropertyGrid();
            this.c_btnOK = new System.Windows.Forms.Button();
            this.c_cmbType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // c_lsbDeclaration
            // 
            this.c_lsbDeclaration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.c_lsbDeclaration.FormattingEnabled = true;
            this.c_lsbDeclaration.Location = new System.Drawing.Point(12, 38);
            this.c_lsbDeclaration.Name = "c_lsbDeclaration";
            this.c_lsbDeclaration.Size = new System.Drawing.Size(204, 264);
            this.c_lsbDeclaration.TabIndex = 0;
            this.c_lsbDeclaration.SelectedIndexChanged += new System.EventHandler(this.c_lsbDeclaration_SelectedIndexChanged);
            // 
            // c_btnAdd
            // 
            this.c_btnAdd.Location = new System.Drawing.Point(222, 38);
            this.c_btnAdd.Name = "c_btnAdd";
            this.c_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.c_btnAdd.TabIndex = 1;
            this.c_btnAdd.Text = "Add";
            this.c_btnAdd.UseVisualStyleBackColor = true;
            this.c_btnAdd.Click += new System.EventHandler(this.c_btnAdd_Click);
            // 
            // c_btnRemove
            // 
            this.c_btnRemove.Location = new System.Drawing.Point(222, 67);
            this.c_btnRemove.Name = "c_btnRemove";
            this.c_btnRemove.Size = new System.Drawing.Size(75, 23);
            this.c_btnRemove.TabIndex = 2;
            this.c_btnRemove.Text = "Remove";
            this.c_btnRemove.UseVisualStyleBackColor = true;
            this.c_btnRemove.Click += new System.EventHandler(this.c_btnRemove_Click);
            // 
            // c_prGrid
            // 
            this.c_prGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c_prGrid.Location = new System.Drawing.Point(303, 11);
            this.c_prGrid.Name = "c_prGrid";
            this.c_prGrid.Size = new System.Drawing.Size(318, 295);
            this.c_prGrid.TabIndex = 3;
            this.c_prGrid.Click += new System.EventHandler(this.c_prGrid_Click);
            this.c_prGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.c_prGrid_PropertyValueChanged);
            // 
            // c_btnOK
            // 
            this.c_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.c_btnOK.Location = new System.Drawing.Point(546, 333);
            this.c_btnOK.Name = "c_btnOK";
            this.c_btnOK.Size = new System.Drawing.Size(75, 23);
            this.c_btnOK.TabIndex = 4;
            this.c_btnOK.Text = "&OK";
            this.c_btnOK.UseVisualStyleBackColor = true;
            // 
            // c_cmbType
            // 
            this.c_cmbType.FormattingEnabled = true;
            this.c_cmbType.Location = new System.Drawing.Point(12, 11);
            this.c_cmbType.Name = "c_cmbType";
            this.c_cmbType.Size = new System.Drawing.Size(204, 21);
            this.c_cmbType.TabIndex = 5;
            this.c_cmbType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // WinDeclarationDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 362);
            this.Controls.Add(this.c_cmbType);
            this.Controls.Add(this.c_btnOK);
            this.Controls.Add(this.c_prGrid);
            this.Controls.Add(this.c_btnRemove);
            this.Controls.Add(this.c_btnAdd);
            this.Controls.Add(this.c_lsbDeclaration);
            this.Name = "WinDeclarationDesigner";
            this.Text = "WinDeclarationDesigner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox c_lsbDeclaration;
        private System.Windows.Forms.Button c_btnAdd;
        private System.Windows.Forms.Button c_btnRemove;
        private System.Windows.Forms.PropertyGrid c_prGrid;
        private System.Windows.Forms.Button c_btnOK;
        private System.Windows.Forms.ComboBox c_cmbType;
    }
}