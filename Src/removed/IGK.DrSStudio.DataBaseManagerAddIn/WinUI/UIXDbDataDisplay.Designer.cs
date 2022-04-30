

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXDbDataDisplay.Designer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:UIXDbDataDisplay.Designer.cs
*/
namespace IGK.DrSStudio.DataBaseManagerAddIn.WinUI
{
    partial class UIXDbDataDisplay
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.c_storedProcedure = new System.Windows.Forms.ComboBox();
            this.c_tables = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.c_storedProcedure, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.c_tables, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(669, 27);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // c_storedProcedure
            // 
            this.c_storedProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_storedProcedure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.c_storedProcedure.FormattingEnabled = true;
            this.c_storedProcedure.Location = new System.Drawing.Point(337, 3);
            this.c_storedProcedure.Name = "c_storedProcedure";
            this.c_storedProcedure.Size = new System.Drawing.Size(329, 21);
            this.c_storedProcedure.TabIndex = 1;
            // 
            // c_tables
            // 
            this.c_tables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.c_tables.FormattingEnabled = true;
            this.c_tables.Location = new System.Drawing.Point(3, 3);
            this.c_tables.Name = "c_tables";
            this.c_tables.Size = new System.Drawing.Size(328, 21);
            this.c_tables.TabIndex = 0;
            // 
            // UIXDbDataDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UIXDbDataDisplay";
            this.Size = new System.Drawing.Size(669, 27);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox c_storedProcedure;
        private System.Windows.Forms.ComboBox c_tables;
    }
}

