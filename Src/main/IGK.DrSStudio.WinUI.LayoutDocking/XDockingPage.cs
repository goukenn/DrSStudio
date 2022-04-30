

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDockingPage.cs
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
file:XDockingPage.cs
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
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent the docking page entity
    /// </summary>
    public class XDockingPage :  IDockingPage 
    {
        private ICoreTool m_Tool;
        private IDockingPanel m_owner;
        private WinCoreLayoutManagerBase  m_layoutManager;
        private enuLayoutToolDisplay m_ToolDisplay;
        private IDockingForm m_DockingForm;

        /// <summary>
        /// get or set the docking form
        /// </summary>
        public IDockingForm DockingForm
        {
            get { return m_DockingForm; }
            set
            {
                if (m_DockingForm != value)
                {
                    m_DockingForm = value;
                }
            }
        }
        /// <summary>
        /// get the current display of this page
        /// </summary>
        public enuLayoutToolDisplay ToolDisplay
        {
            get { return m_ToolDisplay; }
            set
            {
                if (m_ToolDisplay != value)
                {
                    m_ToolDisplay = value;
                    OnToolDisplayChanged(EventArgs.Empty);
                }
            }
        }
        public event System.EventHandler ToolDisplayChanged;
        ///<summary>
        ///raise the ToolDisplayChanged 
        ///</summary>
        protected virtual void OnToolDisplayChanged(System.EventArgs e)
        {
            if (ToolDisplayChanged != null)
                ToolDisplayChanged(this, e);
        }
        public WinCoreLayoutManagerBase LayoutManager {
            get {
                return this.m_layoutManager;
            }
        }
        /// <summary>
        /// get the panel owner
        /// </summary>
        public IDockingPanel Panel {
            get { 
                return this.m_owner; 
            } 
            set {
                if (this.m_owner != value)
                {
                    this.m_owner = value;
                    if ((value != null) && (!value.Pages.Contains(this )))
                    {
                        this.m_owner.Pages.AddPage (this);
                    }
                }
            } 
        }
        public ICoreToolHostedControl HostedControl
        {
            get { return m_Tool.HostedControl; }
        }
        public ICoreTool Tool
        {
            get { return m_Tool; }
        }
        /// <summary>
        /// construct a docking Page
        /// </summary>
        /// <param name="tool"></param>
        /// <param name="layoutManager"></param>
        public XDockingPage(ICoreTool tool, WinCoreLayoutManagerBase layoutManager)
        {
            this.m_Tool = tool;
            this.m_layoutManager = layoutManager;
        }
        #region IDockingPage Members
        public string Title
        {
            get { return CoreSystem.GetString ( this.HostedControl.CaptionKey); }
        }
        public ICore2DDrawingDocument Document
        {
            get { return this.HostedControl .ToolDocument; }
        }
        #endregion
        #region IDockingPage Members
        public void DockTo(enuDockingDirection dock)
        {
            if (this.Panel != null)
            {
                if (this.Panel.DockingDirection != dock)
                {
                    this.Panel.Pages.RemovePage(this);
                    this.m_ToolDisplay = (enuLayoutToolDisplay)dock;
                    this.LayoutManager.ShowTool(this.m_Tool);
                }
            }
            else {
                this.m_ToolDisplay = (enuLayoutToolDisplay)dock;
                this.LayoutManager.ShowTool(this.m_Tool);
            }
        }
        public void Undock()
        {
            if (this.Panel != null)
            {
                this.Panel.Pages.RemovePage(this);
            }
            this.m_ToolDisplay = enuLayoutToolDisplay.Float;
            this.LayoutManager.ShowTool(this.m_Tool);
        }
        #endregion

        public void HideTool()
        {
            this.m_layoutManager.HideTool (this.m_Tool);
        }
    }
}

