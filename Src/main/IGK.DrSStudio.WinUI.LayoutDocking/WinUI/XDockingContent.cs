

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDockingContent.cs
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
file:XDockingContent.cs
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
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent the control that will show the docking control
    /// </summary>
    public class XDockingContent : IGKXUserControl , IDockingContent 
    {
        private XDockingPage m_Page;
        private IDockingPanel m_DockingPanel;
        private Control m_currentParent;

        /// <summary>
        /// get the docking panel container
        /// </summary>
        public IDockingPanel DockingPanel
        {
            get { return m_DockingPanel; }
            set
            {
                if (m_DockingPanel != value)
                {
                    m_DockingPanel = value;
                }
            }
        }

        /// <summary>
        /// get the dock property
        /// </summary>
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                if (value == DockStyle.None )
                    base.Dock = value;
            }
        }
        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
        }
        public XDockingPage Page
        {
            get { return m_Page; }
            set
            {
                if (m_Page != value)
                {
                    m_Page = value;
                    if (m_Page == null)
                        this.Controls.Clear();
                    else
                        this.Controls.Add(this.m_Page.HostedControl as Control);
                }
            }
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            Rectanglei b = new Rectanglei(0, 0, this.Width, this.Height);
            b.Inflate(-2, -2);
            e.Control.Bounds = new System.Drawing.Rectangle(b.X, b.Y, b.Width, b.Height);
        }
        public XDockingContent()
        {
            this.Margin = System.Windows.Forms.Padding.Empty;
            this.Padding = System.Windows.Forms.Padding.Empty;
            this.SizeChanged += _SizeChanged;
            this.AutoScroll = false;
            this.Layout += _Layout;
        }

        void _Layout(object sender, LayoutEventArgs e)
        {
            
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
            {
                if (this.m_currentParent != null)
                {
                    //remove current parent event
                    this.m_currentParent.SizeChanged -= Parent_SizeChanged;
                }
                
                //register 
                this.m_currentParent = this.Parent;
                if (this.m_currentParent != null)
                {
                    this.Parent.SizeChanged += Parent_SizeChanged;
                    
                }
            }
        }

        void Parent_SizeChanged(object sender, EventArgs e)
        {
            this.UpdateViewSize(false );
        }
        internal void UpdateViewSize(bool size)
        {
            //update bound content
            //System.Drawing.Rectangle r = (this.DockingPanel as Control).DisplayRectangle;           
            if (size)
            {
                this.UpdateContentViewSize();
            }
        }

        private void UpdateContentViewSize()
        {
            this.SuspendLayout();
            //set the document size
            if (this.Page != null)
            {
                Rectanglei b = new Rectanglei(0, 0, this.Width, this.Height);
                b.Inflate(-2, -2);
                // e.Control.Bounds = new System.Drawing.Rectangle(b.X, b.Y, b.Width, b.Height);
                this.Page.HostedControl.Size = new Size2i(this.ClientSize.Width, this.ClientSize.Height);
            }
            else
            {
                if (this.DockingPanel.SelectedPage != null)
                {
                    Control c = this.DockingPanel.SelectedPage.HostedControl as Control;
                    if (c != null)
                    {
                        c.SuspendLayout();
                        Rectanglei b = new Rectanglei(0, 0, this.Width, this.Height);
                        b.Inflate(-2, -2);
                        if (HScroll)
                            b.Height -= System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight - 5;
                        if (VScroll)
                            b.Width -= System.Windows.Forms.SystemInformation.VerticalScrollBarWidth - 2;
                        c.Bounds = new System.Drawing.Rectangle(b.X, b.Y, b.Width, b.Height);
                        c.ResumeLayout();
                    }
                }
            }
            this.ResumeLayout(true);
        }
     
        void _SizeChanged(object sender, EventArgs e)
        {
            this.UpdateContentViewSize();
        }
        #region IDockingContent Members
        IDockingPage IDockingContent.Page
        {
            get
            {
                return this.Page;
            }
            set
            {
                this.Page = value as XDockingPage;
            }
        }
        #endregion


       
    }
}

