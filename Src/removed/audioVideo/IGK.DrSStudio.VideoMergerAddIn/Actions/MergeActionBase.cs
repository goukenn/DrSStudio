

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MergeActionBase.cs
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
file:MergeActionBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Actions
{
    public abstract class MergeActionBase
    {
        private System.Windows.Forms.ToolStripItem  m_ContextMenuItem;
        public System.Windows.Forms.ToolStripItem  ContextMenuItem
        {
            get { return m_ContextMenuItem; }
        }
        protected abstract void PerformAction();
        public abstract string Text { get; }
        public System.Windows.Forms.Control SourceControl
        {
            get
            {
                if (this.ContextMenuItem.Owner is System.Windows.Forms.ContextMenuStrip)
                    return (this.ContextMenuItem.Owner as System.Windows.Forms.ContextMenuStrip).SourceControl ;
                return this.ContextMenuItem.Owner.ContextMenuStrip.SourceControl;
            }
        }
        public MergeActionBase()
        {
            this.m_ContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenuItem.Text = this.Text;
            this.m_ContextMenuItem.Click += new EventHandler(m_ContextMenuItem_Click);
        }
        void m_ContextMenuItem_Click(object sender, EventArgs e)
        {
            PerformAction();
        }
    }
}

