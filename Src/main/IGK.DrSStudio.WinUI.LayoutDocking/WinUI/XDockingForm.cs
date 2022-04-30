

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDockingForm.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XDockingForm.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.Native;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent the form used to dock an undock tool property
    /// </summary>
    public class XDockingForm : XFormDialog ,IDockingForm 
    {
        XDockingFormCaptionBar m_CaptionBar;
        XDockingPanel m_dockingPanel;
        IDockingManager m_layoutManager;
        private bool m_CanDock;
       
        public bool CanDock
        {
            get { return m_CanDock; }
            set
            {
                if (m_CanDock != value)
                {
                    m_CanDock = value;
                }
            }
        }
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                System.Drawing.Rectangle v_rc = base.DisplayRectangle;
                if (this.WindowState == FormWindowState.Normal)
                    v_rc.Inflate(-2, -2);
                return v_rc;
            }
        }
        public XDockingForm(IDockingManager layoutManager)
        {
            this.m_layoutManager = layoutManager;
            this.Size = new System.Drawing.Size(300, 300);
            this.ShowInTaskbar = false;
            this.m_dockingPanel = new XDockingPanel(layoutManager);            
            this.m_dockingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dockingPanel.ShowTab = false;
            this.m_dockingPanel.ShowCaption = false;
            this.m_dockingPanel.ShowSplit = false;
            this.m_CaptionBar = new XDockingFormCaptionBar(this, this.m_dockingPanel);
            this.m_CaptionBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Controls.Add(this.m_dockingPanel as Control);
            this.Controls.Add(this.m_CaptionBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None ;
            this.AllowHitTest = true;
        }
        #region IDockingForm Members
        IDockingCaptionBar IDockingForm.CaptionBar
        {
            get {
                return this.m_CaptionBar;
            }
        }
        //public IDockingTabBar TabBar
        //{
        //    get {
        //        return this.m_TabBar;
        //    }
        //}
        public IDockingPanel DockingPanel
        {
            get {
                return m_dockingPanel;
            }
        }        
        #endregion
        #region IDockingForm Members
        public IDockingTabBar TabBar
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
        protected override void WndProc(ref Message m)
        {
            switch ((enuWindowMessage)m.Msg)
            {
                case enuWindowMessage.WM_NCACTIVATE:
                    if (this.WindowState != FormWindowState.Minimized)
                    {
                        RenderNonClientArea(m.HWnd, new IntPtr(1));
                        m.Result = new IntPtr(1);
                        Invalidate(true);
                        return;
                    }
                    break;
                case enuWindowMessage.WM_NCHITTEST:
                    if (AllowHitTest)
                    {
                        OnNCHitTest(ref m);
                        return;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}

