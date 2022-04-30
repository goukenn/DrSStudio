

/*
IGKDEV @ 2008-2016
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
    using IGK.ICore.Drawing2D.Tools;
    
using System.Windows.Forms.Layout;
    using System.Drawing;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.ICore.WinCore.Controls;
    /// <summary>
    /// repreent the class to configure the history tool
    /// </summary>
    public class UIXHistoryControl :
        IGKXToolHostedControl 
        
    {
        private List<XHistoryItemControl> m_list;
        private HistorySurfaceManager m_historyTool;
        private HistoryActionsList m_currentHistoryActionList;
        private HistoryControlLayoutEngine m_lEngine;


        class HistoryControlLayoutEngine : LayoutEngine
        {

            public override void InitLayout(object child, System.Windows.Forms.BoundsSpecified specified)
            {
                base.InitLayout(child, specified);
            }
            public override bool Layout(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
            {
                UIXHistoryControl s  = container as UIXHistoryControl;
                int w = 0;
                bool vscrollVisible = s.VerticalScroll.Visible ;
                w = vscrollVisible ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0;
                int v = vscrollVisible ? s.VerticalScroll.Value : 0;
                int size = XHistoryItemControl.SIZE;
                int index = 0;
                if (s.m_list != null)
                {
                    foreach (XHistoryItemControl item in s.m_list)
                    {
                        index = item.History.Index;

                        item.Margin = System.Windows.Forms.Padding.Empty;
                        item.Padding = System.Windows.Forms.Padding.Empty;
                        item.Bounds = new Rectangle(0, index * 16, s.Width - w, size);
                        item.Location = new Point(0, (size * index) - v);
                        //index++;
                    }
                }
                return false;
            }
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (m_lEngine ==null)
                m_lEngine = new HistoryControlLayoutEngine();
                return m_lEngine;
            }
        }

       // private IGKXVerticalScrollBar c_vScroll;
        //public bool ScrollVisible {
        //    get {
        //        return c_vScroll.Visible;
        //    }
        //}
        //internal int ScrollValue {
        //    get {
        //        return (c_vScroll.Value - c_vScroll.MinValue );
        //    }
        //}
        /// <summary>
        /// get the history action list
        /// </summary>
        public HistoryActionsList HistoryList { get { return m_currentHistoryActionList; } }

        
        public UIXHistoryControl(HistorySurfaceManager historyTool)
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.m_list = new List<XHistoryItemControl>();
            this.Margin = System.Windows.Forms.Padding.Empty;
            this.Padding = System.Windows.Forms.Padding.Empty;
            this.AutoScroll = true;            
            this.m_historyTool = historyTool;
            
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

        //void c_vScroll_Scroll(object sender,EventArgs e)
        //{
        //    this.OnScroll(new System.Windows.Forms.ScrollEventArgs (System.Windows.Forms.ScrollEventType.SmallDecrement , this.c_vScroll.Value ) );
        //}
        private void InitScrollBound()
        {
           
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

            int s = this.m_currentHistoryActionList.HistoryIndex + 1;
           

            if( (m_list.Count > 0)&& (s< this.m_list.Count ))
            {
                this.SuspendLayout();
                int c = this.m_list.Count;
                
                for (int i = 0;
                    i < this.m_list.Count ;
                    i++)
                {
                    if (this.m_list[i].History.Index == -1)
                    {
                        this.Controls.Remove(m_list[i]);
                        m_list[i].Dispose();
                        m_list.Remove(m_list[i]);
                        i--;
                        
                    }
                }
                
            
                this.ResumeLayout();
                if (this.m_currentHistoryActionList.Count != this.m_list.Count)
                {
                    System.Windows.Forms.MessageBox.Show("Index don't match");
                }
            }
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
   
        }

        void m_currentHistoryActionList_HistoryItemAdded(object o, HistoryItemEventArgs e)
        {
            //this.SuspendLayout();
            
            IHistoryAction h = e.HistoryAction;            
            XHistoryItemControl ctr = new XHistoryItemControl(this, h);
            this.m_list.Add(ctr);
            this.Controls.Add(ctr);
            //this.ResumeLayout(true);
            //this.PerformLayout();
            
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
