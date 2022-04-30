

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXHistoryControl.cs
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
file:UIXHistoryControl.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
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
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.DrSStudio.History;
    /// <summary>
    /// repreent the class to configure the history tool
    /// </summary>
    public class UIXHistoryControl :
        UIXToolConfigControlBase,//UIXConfigControlBase,
        IGK.DrSStudio.Tools.ICoreToolHostedControl 
    {
        private List<XHistoryItemControl> m_list;
        private HistorySurfaceManager m_historyTool;
        private HistoryActionsList m_currentHistoryActionList;
        private XVerticalScrollBar c_vScroll;
        public bool ScrollVisible {
            get {
                return c_vScroll.Visible;
            }
        }
        internal int ScrollValue {
            get {
                return (c_vScroll.Value - c_vScroll.Minimum );
            }
        }
        /// <summary>
        /// get the history action list
        /// </summary>
        public HistoryActionsList HistoryList { get { return m_currentHistoryActionList; } }
        public UIXHistoryControl(HistorySurfaceManager historyTool):base(historyTool ) 
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.AutoScroll = false;
            this.m_list = new List<XHistoryItemControl>();
            this.m_historyTool = historyTool;
            this.c_vScroll = new XVerticalScrollBar();
            this.Controls.Add(c_vScroll);
            this.c_vScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(c_vScroll_Scroll);
            InitScrollBound();
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.InitScrollBound();
        }
        void c_vScroll_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            this.OnScroll(e);
        }
        private void InitScrollBound()
        {
            this.c_vScroll.Bounds = new System.Drawing.Rectangle(
                this.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth,
                0, System.Windows.Forms.SystemInformation.VerticalScrollBarWidth,
                this.Height);
            int v_diff = 0;
            //get number of item that can be show
            int h  = this.Height / XHistoryItemControl.SIZE;
            if (this.m_list.Count > h)
            {
                v_diff = Math.Abs(this.m_list.Count - h);
            }
            int cv = this.ScrollValue;
            this.c_vScroll.Minimum = this.c_vScroll.Height ;
            this.c_vScroll.Maximum = this.c_vScroll.Height + v_diff;
            this.c_vScroll.Value = this.c_vScroll.Height;
        }
        internal void Edit(HistoryActionsList hList)
        {
            UnregisterHistoryActionList();
            this.m_currentHistoryActionList = hList;
            RegisterHistoryActionList();
            if (hList == null)
            {            
                Clear ();
                this.Enabled = false;
                return;
            }
            this.SuspendLayout();
            this.Enabled = true;
            Clear();
            XHistoryItemControl ctr = null;
            foreach (HistoryActionBase h in hList)
            {
                ctr = new XHistoryItemControl(this,h);
                this.m_list.Add(ctr);
                this.Controls.Add(ctr);
            }
            this.ResumeLayout();
            this.Invalidate();
        }
        private void UnregisterHistoryActionList()
        {
            if (this.m_currentHistoryActionList == null) return;
            this.m_currentHistoryActionList.HistoryItemAdded-= new HistoryItemEventHandler(m_currentHistoryActionList_HistoryItemAdded);
            this.m_currentHistoryActionList.HistoryChanged -= new HistoryChangedEventHandler(m_currentHistoryActionList_HistoryChanged);
            this.m_currentHistoryActionList.HistoryClear -= new EventHandler(m_currentHistoryActionList_HistoryClear);
            this.m_currentHistoryActionList.HistoryClearAt -= new EventHandler(m_currentHistoryActionList_HistoryClearAt);
        }
        private void RegisterHistoryActionList()
        {
            if (this.m_currentHistoryActionList == null) return;
            this.m_currentHistoryActionList.HistoryItemAdded += new HistoryItemEventHandler(m_currentHistoryActionList_HistoryItemAdded);
            this.m_currentHistoryActionList.HistoryChanged += new HistoryChangedEventHandler(m_currentHistoryActionList_HistoryChanged);
            this.m_currentHistoryActionList.HistoryClear += new EventHandler(m_currentHistoryActionList_HistoryClear);
            this.m_currentHistoryActionList.HistoryClearAt += new EventHandler(m_currentHistoryActionList_HistoryClearAt);
        }
        void m_currentHistoryActionList_HistoryClearAt(object sender, EventArgs e)
        {
            this.SuspendLayout();
            int c = this.m_list.Count;
            int s = this.m_currentHistoryActionList.HistoryIndex;
            for (int i = s;
                i < c; 
                i++)
            {
                this.Controls.Remove(m_list[i]);
                m_list[i].Dispose();
            }
            m_list.RemoveRange(s,
                c - s);
            this.ResumeLayout();
        }
        void m_currentHistoryActionList_HistoryClear(object sender, EventArgs e)
        {
            this.SuspendLayout();
            foreach (XHistoryItemControl item in this.m_list)
            {
                this.Controls.Remove(item);
                item.Dispose();
            }
            this.m_list.Clear();
            this.ResumeLayout();
        }
        void m_currentHistoryActionList_HistoryChanged(object o, HistoryChangedEventArgs  e)
        {
            //get the current history list index
            //this.SuspendLayout();
            //this.ResumeLayout();
            //this.Invalidate();
        }
        void m_currentHistoryActionList_HistoryItemAdded(object o, HistoryItemEventArgs e)
        {
            this.SuspendLayout();
            IHistoryAction h = e.HistoryAction;            
            XHistoryItemControl ctr = new XHistoryItemControl(this, h);
            this.m_list.Add(ctr);
            this.Controls.Add(ctr);
            this.InitScrollBound();
            this.ResumeLayout();
        }
        private void Clear()
        {
            foreach (XHistoryItemControl h in this.m_list)
            {
                this.Controls.Remove(h);
                h.Dispose();
            }
            m_list.Clear();
        }
    }
}

