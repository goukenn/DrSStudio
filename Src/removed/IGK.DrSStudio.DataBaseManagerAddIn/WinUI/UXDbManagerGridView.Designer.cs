

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UXDbManagerGridView.Designer.cs
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
file:UXDbManagerGridView.Designer.cs
*/
namespace IGK.DrSStudio.DataBaseManagerAddIn.WinUI
{
    partial class UXDbManagerGridView
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
            this.c_dataGridView = new System.Windows.Forms.DataGridView();
            this.c_dataDisplay = new IGK.DrSStudio.DataBaseManagerAddIn.WinUI.UIXDbDataDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.c_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // c_dataGridView
            // 
            this.c_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.c_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_dataGridView.Location = new System.Drawing.Point(0, 26);
            this.c_dataGridView.Name = "c_dataGridView";
            this.c_dataGridView.Size = new System.Drawing.Size(600, 296);
            this.c_dataGridView.TabIndex = 0;
            // 
            // c_dataDisplay
            // 
            this.c_dataDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_dataDisplay.Location = new System.Drawing.Point(0, 0);
            this.c_dataDisplay.Name = "c_dataDisplay";
            this.c_dataDisplay.Size = new System.Drawing.Size(600, 26);
            this.c_dataDisplay.StroredProcedure = null;
            this.c_dataDisplay.TabIndex = 1;
            this.c_dataDisplay.Tables = null;
            // 
            // UXDbManagerGridView
            // ;
            this.Controls.Add(this.c_dataGridView);
            this.Controls.Add(this.c_dataDisplay);
            this.Name = "UXDbManagerGridView";
            this.Size = new System.Drawing.Size(600, 322);
            ((System.ComponentModel.ISupportInitialize)(this.c_dataGridView)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.DataGridView c_dataGridView;
        private UIXDbDataDisplay c_dataDisplay;
    }
}

