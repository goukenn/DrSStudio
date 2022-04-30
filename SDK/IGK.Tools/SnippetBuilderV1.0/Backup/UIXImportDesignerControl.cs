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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms ;

namespace WinSnippetBuilder
{
    class UIXImportDesignerControl : UserControl 
    {
        private ListBox c_lsbSelection;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton c_btnRemove;
        private ToolStripButton c_btnAdd;
        private ToolStrip toolStrip1;
        private ToolStripButton c_btnClear;
        ISnippetImportCollection m_importCollection;
        public UIXImportDesignerControl(ISnippetImportCollection tab)
        {
            m_importCollection = tab;
            this.InitializeComponent();

            this.Populate();
        }

        private void Populate()
        {
            this.c_lsbSelection.Items.Clear();

            foreach (ISnippetImport tp in this.m_importCollection )
            {
                this.c_lsbSelection.Items.Add(tp);
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIXImportDesignerControl));
            this.c_lsbSelection = new System.Windows.Forms.ListBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.c_btnRemove = new System.Windows.Forms.ToolStripButton();
            this.c_btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.c_btnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_lsbSelection
            // 
            this.c_lsbSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_lsbSelection.FormattingEnabled = true;
            this.c_lsbSelection.IntegralHeight = false;
            this.c_lsbSelection.Location = new System.Drawing.Point(0, 25);
            this.c_lsbSelection.Name = "c_lsbSelection";
            this.c_lsbSelection.Size = new System.Drawing.Size(352, 156);
            this.c_lsbSelection.TabIndex = 3;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // c_btnRemove
            // 
            this.c_btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("c_btnRemove.Image")));
            this.c_btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btnRemove.Name = "c_btnRemove";
            this.c_btnRemove.Size = new System.Drawing.Size(23, 22);
            this.c_btnRemove.Text = "toolStripButton2";
            this.c_btnRemove.Click += new System.EventHandler(this.c_btnRemove_Click);
            // 
            // c_btnAdd
            // 
            this.c_btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("c_btnAdd.Image")));
            this.c_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btnAdd.Name = "c_btnAdd";
            this.c_btnAdd.Size = new System.Drawing.Size(23, 22);
            this.c_btnAdd.Text = "toolStripButton1";
            this.c_btnAdd.Click += new System.EventHandler(this.c_btnAdd_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_btnAdd,
            this.c_btnRemove,
            this.toolStripSeparator1,
            this.c_btnClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(352, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // c_btnClear
            // 
            this.c_btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btnClear.Image = ((System.Drawing.Image)(resources.GetObject("c_btnClear.Image")));
            this.c_btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btnClear.Name = "c_btnClear";
            this.c_btnClear.Size = new System.Drawing.Size(23, 22);
            this.c_btnClear.Text = "toolStripButton3";
            this.c_btnClear.Click += new System.EventHandler(this.c_btnClear_Click);
            // 
            // UIXImportDesignerControl
            // 
            this.Controls.Add(this.c_lsbSelection);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UIXImportDesignerControl";
            this.Size = new System.Drawing.Size(352, 181);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void c_btnAdd_Click(object sender, EventArgs e)
        {
            using (Form frm = new Form())
            {
                UIXNameSelection ctr = new UIXNameSelection();
                ctr.ReturnName = "Namespace";
                frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                ctr.Dock = DockStyle.Fill;

                frm.Text = "Add Import Namespace";
                frm.Controls.Add(ctr);
                frm.Owner = this.ParentForm;
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(ctr.ReturnName))
                    {
                        int c = this.m_importCollection.Count;

                        SnippetImport vf = new SnippetImport (ctr.ReturnName);
                        this.m_importCollection .Add(vf);
                        if (c < this.m_importCollection.Count)
                        {
                            this.c_lsbSelection.Items.Add(vf);
                        }
                    }
                }
            }
        }

        private void c_btnRemove_Click(object sender, EventArgs e)
        {
            foreach (object obj in this.c_lsbSelection.SelectedItems)
            {
                this.m_importCollection.Remove(obj as ISnippetImport);
            }
            this.Populate();
        }

        private void c_btnClear_Click(object sender, EventArgs e)
        {
            this.m_importCollection.Clear();
            this.Populate();
        }
    }
}
