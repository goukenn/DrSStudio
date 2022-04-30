

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXHistoryControlHost.cs
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
file:UIXHistoryControlHost.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.DrSStudio.History;
    /// <summary>
    /// represent a history control host
    /// </summary>
    public sealed class UIXHistoryControlHost :
        UIXToolConfigControlBase,
        IGK.DrSStudio.Tools.ICoreToolHostedControl 
    {
        UIXHistoryControl c_historyControl;
        private HistorySurfaceManager m_historyManager;
        private XToolStrip c_toolStrip;
        private XToolStripButton c_btn_clearHistory;
        private XToolStripButton c_btn_Undo;
        private XToolStripButton c_btn_Redo;
        public UIXHistoryControlHost(HistorySurfaceManager historyManager)
        {
            this.m_historyManager = historyManager;
            this.c_historyControl = new UIXHistoryControl(historyManager);
            this.c_toolStrip = new XToolStrip();
            this.c_btn_Redo = new XToolStripButton();
            this.c_btn_Undo = new XToolStripButton();
            this.c_btn_clearHistory = new XToolStripButton();
            this.c_toolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_historyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_btn_clearHistory.Image = CoreResources.GetDocumentImage("menu_clear");
            this.c_btn_Redo.Image =     CoreResources.GetDocumentImage ("menu_redo");
            this.c_btn_Undo.Image = CoreResources.GetDocumentImage("menu_undo");
            this.c_btn_Undo.Click += new EventHandler(c_btn_Undo_Click);
            this.c_btn_Redo.Click += new EventHandler(c_btn_Redo_Click);
            this.c_btn_clearHistory.Click += new EventHandler(c_btn_clearHistory_Click);
            this.c_toolStrip.Items.Add(c_btn_clearHistory);
            this.c_toolStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            this.c_toolStrip.Items.Add(c_btn_Undo);
            this.c_toolStrip.Items.Add(c_btn_Redo);
            this.Controls.Add(c_historyControl);
            this.Controls.Add(c_toolStrip);
        }
        void c_btn_clearHistory_Click(object sender, EventArgs e)
        {
            this.m_historyManager.ClearHistory();
        }
        void c_btn_Redo_Click(object sender, EventArgs e)
        {
            this.m_historyManager.Redo();
        }
        void c_btn_Undo_Click(object sender, EventArgs e)
        {
            this.m_historyManager.Undo();
        }
        public override string ToString()
        {
            return this.GetType().Name;
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
        }
        public IHistoryActionList HistoryList {get{return this.c_historyControl.HistoryList;} }
        /// <summary>
        /// edit history action list
        /// </summary>
        /// <param name="obj"></param>
        public void Edit(IHistoryActionList obj)
        {
            this.c_historyControl.Edit(obj as HistoryActionsList );
        }
    }
}

