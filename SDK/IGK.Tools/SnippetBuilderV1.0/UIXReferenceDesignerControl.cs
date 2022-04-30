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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSnippetBuilder
{
    sealed class UIXReferenceDesignerControl<T> : UserControl where T : System.Collections.IEnumerable, ISnippetItemCollection
    {
        //private ISnippetReferenceCollection m_refCollection;
        private T m_refCollection;
        private ToolStrip toolStrip1;
        private ToolStripButton c_btnRemove;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton c_btnClear;
        private ListBox c_lsbSelection;
        private ToolStripButton c_btnAdd;

        

        //internal UIXReferenceDesignerControl(ISnippetReferenceCollection refCollection)
        //{
        //    this.InitializeComponent();
        //    this.m_refCollection = refCollection;
        //    this.c_lsbSelection.Items.Clear();

        //    foreach (ISnippetReference rf in this.m_refCollection)
        //    {
        //        this.c_lsbSelection.Items.Add(rf);
        //    }
        //}
        internal UIXReferenceDesignerControl(T refCollection)
        {
            this.InitializeComponent();
            this.m_refCollection = refCollection;
            this.c_lsbSelection.Items.Clear();
            
            foreach (object  rf in this.m_refCollection)
            {
                this.c_lsbSelection.Items.Add(rf);
            }
        }
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.c_btnClear = new System.Windows.Forms.ToolStripButton();
            this.c_lsbSelection = new System.Windows.Forms.ListBox();
            this.c_btnAdd = new System.Windows.Forms.ToolStripButton();
            this.c_btnRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_btnAdd,
            this.c_btnRemove,
            this.toolStripSeparator1,
            this.c_btnClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 303);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(239, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // c_btnClear
            // 
            this.c_btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btnClear.Image = global::WinSnippetBuilder.Properties.Resources.c_btnClear_Image;
            this.c_btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btnClear.Name = "c_btnClear";
            this.c_btnClear.Size = new System.Drawing.Size(23, 22);
            this.c_btnClear.Text = "toolStripButton3";
            this.c_btnClear.Click += new System.EventHandler(this.c_btnClear_Click);
            // 
            // c_lsbSelection
            // 
            this.c_lsbSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_lsbSelection.FormattingEnabled = true;
            this.c_lsbSelection.Location = new System.Drawing.Point(0, 0);
            this.c_lsbSelection.Name = "c_lsbSelection";
            this.c_lsbSelection.Size = new System.Drawing.Size(239, 303);
            this.c_lsbSelection.TabIndex = 1;
            // 
            // c_btnAdd
            // 
            this.c_btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btnAdd.Image = global::WinSnippetBuilder.Properties.Resources.c_btnAdd_Image;
            this.c_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btnAdd.Name = "c_btnAdd";
            this.c_btnAdd.Size = new System.Drawing.Size(23, 22);
            this.c_btnAdd.Text = "toolStripButton1";
            this.c_btnAdd.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // c_btnRemove
            // 
            this.c_btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btnRemove.Image = global::WinSnippetBuilder.Properties.Resources.c_btnRemove_Image;
            this.c_btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btnRemove.Name = "c_btnRemove";
            this.c_btnRemove.Size = new System.Drawing.Size(23, 22);
            this.c_btnRemove.Text = "toolStripButton2";
            this.c_btnRemove.Click += new System.EventHandler(this.c_btnRemove_Click);
            // 
            // UIXReferenceDesignerControl
            // 
            this.Controls.Add(this.c_lsbSelection);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UIXReferenceDesignerControl";
            this.Size = new System.Drawing.Size(239, 328);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void c_btnClear_Click(object sender, EventArgs e)
        {
            this.c_lsbSelection.Items.Clear();
            this.m_refCollection.Clear();
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (Form frm = new Form())
            {
                UIXNameSelection ctr = new UIXNameSelection();
                ctr.ReturnName = "Expansion, SurroundsWith,...";
                frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                ctr.Dock = DockStyle.Fill;

                frm.Text = "Add Snippets categories";
                frm.Controls.Add(ctr);
                frm.Owner = this.ParentForm;
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(ctr.ReturnName))
                    {
                        int c = this.m_refCollection.Count;

                        SnippetItem vf = SnippetDefinition.CreateItem(ctr.ReturnName);
                        this.m_refCollection.Add(vf);
                        if (c < this.m_refCollection.Count)
                        {
                            this.c_lsbSelection.Items.Add(vf);
                        }
                    }
                }
            }
        }

        private void c_btnRemove_Click(object sender, EventArgs e)
        {
            if (this.c_lsbSelection.SelectedItem != null)
            {
                //this.m_refCollection.Remove(this.c_lsbSelection.SelectedItem as ISnippetReference);
                this.m_refCollection.Remove(this.c_lsbSelection.SelectedItem as ISnippetReader );
                this.c_lsbSelection.Items.Remove(this.c_lsbSelection.SelectedItem);
            }
        }
    }
}
