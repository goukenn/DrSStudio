

/*
IGKDEV @ 2008-2016
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
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.History;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore.Drawing2D.Tools;
    
    using System.Drawing;
    using IGK.ICore.Resources;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.ICore.WinCore.Controls;
    using IGK.ICore.WinCore.WinUI.Controls;


    /// <summary>
    /// represent a history control host
    /// </summary>
    public sealed class UIXHistoryControlHost :
        IGKXToolHostedControl
    {

        UIXHistoryControl c_historyControl;
        private HistorySurfaceManager m_historyManager;
        private IGKXToolStrip c_toolStrip;
        private IGKXToolStripButton c_btn_clearHistory;
        private IGKXToolStripButton c_btn_Undo;
        private IGKXToolStripButton c_btn_Redo;
        


        public UIXHistoryControlHost(HistorySurfaceManager historyManager)
        {
            this.m_historyManager = historyManager;
            this.c_historyControl = new UIXHistoryControl(historyManager);
            this.c_toolStrip = new IGKXToolStrip();

            this.c_btn_Redo = new IGKXToolStripButton();
            this.c_btn_Undo = new IGKXToolStripButton();
            this.c_btn_clearHistory = new IGKXToolStripButton();

            this.c_toolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_historyControl.Dock = System.Windows.Forms.DockStyle.Fill;

            this.c_btn_clearHistory.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_CLEAR_GKDS);
            this.c_btn_Redo.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_REDO_GKDS);
            this.c_btn_Undo.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_UNDO_GKDS);

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
