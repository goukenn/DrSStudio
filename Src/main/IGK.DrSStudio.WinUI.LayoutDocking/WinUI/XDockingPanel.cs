

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDockingPanel.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XDockingPanel.cs
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
    /// represent a docking panel
    /// </summary>
    public class XDockingPanel : IGKXPanel, IDockingPanel 
    {
        private IDockingManager m_layoutManager;
        private XDockingSplitterControl c_splitter;
        private XDockingCaptionBar c_captionBar;
        private XDockingTabBar c_tabbar;
        private XDokingPageCollections m_pages;
        private XDockingContent c_content;
        private IDockingPage m_selectedPage;
        private DockingPageManager m_manager;
        private IDockingOwner m_dockingOwner;
        private IGKXPanel  c_vpanel;
        private int m_maxSize;
        private bool m_Reduce;
        private int m_RestoreSize;
        private enuDockingTabOrientation  m_TabOrientation;

        /// <summary>
        /// represent the display visible rectangle zone
        /// </summary>
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                System.Drawing.Rectangle  rc =  base.DisplayRectangle;
                //remove caption height
                if (this.ShowCaption ){
                              rc.Y += this.c_captionBar.Height;
                              rc.Height -= this.c_captionBar.Height;
                    }
                switch (this.DockingDirection)
                {
                   
                    case enuDockingDirection.Left:
                        if (this.ShowTab)
                        {
                            rc.Width -= XDockingTabBar.SIZE;
                            rc.X += XDockingTabBar.SIZE;
                        }
                        if (this.ShowSplit )
                            rc.Width -=  this.c_splitter.Width;
                        
                        break;
                    case enuDockingDirection.Right:
                        
                        if (this.ShowSplit)
                        {
                            rc.X += this.c_splitter.Width;
                            rc.Width -= this.c_splitter.Width;
                        }
                        if (this.ShowTab)
                        {
                            rc.Width -= XDockingTabBar.SIZE;                            
                        }
                        break;
                    case enuDockingDirection.Top:
                        break;
                    case enuDockingDirection.Bottom:
                        //remove tab bar height height
                        rc.Height -= XDockingTabBar.SIZE;// this.c_captionBar.Height;
                        break;
                    default:
                        break;
                }              
                return rc;
            }
        }
        public enuDockingTabOrientation  TabOrientation
        {
            get { return m_TabOrientation; }
            set
            {
                if (m_TabOrientation != value)
                {
                    m_TabOrientation = value;
                    OnTabOrientationChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler TabOrientationChanged;
        public void OnTabOrientationChanged(EventArgs eventArgs)
        {
            if (TabOrientationChanged != null)
                this.TabOrientationChanged(this, eventArgs);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_layoutManager.Panels.Remove(this);
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// get the docking owner
        /// </summary>
        public IDockingOwner DockingOwner { get {
            return this.m_dockingOwner;
        } }
        public IDockingManager LayoutManager { get {
            return this.m_layoutManager;
        } }
        public bool ShowCaption
        {
            get { return this.c_captionBar.Visible; }
            set
            {
                if (this.c_captionBar.Visible != value)
                {
                    this.c_captionBar.Visible = value;
                }
            }

        }
        public bool ShowSplit        {
            get { return c_splitter .Visible ; 
            }
            set
            {
                this.c_splitter.Visible = value;
            }
        }
        public bool ShowTab
        {
            get { return this.c_tabbar.Visible; }
            set
            {
                this.c_tabbar.Visible = value;
            }
        }
        ///// <summary>
        ///// get the docking owner
        ///// </summary>
        //public IDockingManager DockingOwner {
        //    get { return this.m_dockingOwner; }
        //    set { this.m_dockingOwner = value; }
        //}
        public int RestoreSize
        {
            get { return m_RestoreSize; }
            set
            {
                if (m_RestoreSize != value)
                {
                    m_RestoreSize = value;
                }
            }
        }
        public XDockingSplitterControl  Splitter { get { return this.c_splitter; } }
        /// <summary>
        /// build a docking manager
        /// </summary>
        /// <param name="layoutManager"></param>
        /// <param name="owner"></param>
        public XDockingPanel(IDockingManager layoutManager, IDockingOwner owner):this(layoutManager ) {
            this.m_dockingOwner = owner;
        }
        public XDockingPanel(IDockingManager layoutManager)
        {
            this.m_Reduce = false;
            this.m_layoutManager = layoutManager;
            this.m_pages = new XDokingPageCollections(this);
            this.c_vpanel = new IGKXPanel();
            this.c_splitter = new XDockingSplitterControl();
            this.c_captionBar = new XDockingCaptionBar(this);
            this.c_tabbar = new XDockingTabBar(this);
            this.c_content = new XDockingContent();
            this.m_manager = new DockingPageManager(this);
            this.c_content.DockingPanel = this;
            this.c_content.Dock = DockStyle.None;
            this.c_captionBar.Dock = DockStyle.None;
            this.c_vpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.Controls.Add(c_splitter);
            this.Controls.Add(c_tabbar);
            this.Controls.Add(c_vpanel);
            this.Controls.Add(c_captionBar);

            this.c_vpanel.Controls.Add(c_content);
            
            this.DockChanged += new EventHandler(XDockingPanel_DockChanged);
            this.c_splitter.Dock = DockStyle.None;
            this.c_splitter.MouseDown += c_splitter_MouseDown;
            this.DoubleClick += new EventHandler(XDockingPanel_DoubleClick);
            layoutManager.Panels.Add(this);
            this.ShowTab = true ;
            this.ShowCaption = true;
            this.ShowSplit = true ;
            this.c_vpanel.SizeChanged += _panelSizeChanged;   
        }

        private void _panelSizeChanged(object sender, EventArgs e)
        {
            if (this.c_content != null)
            {//to a a scroll dimension neffect
                this.c_content.Bounds = new System.Drawing.Rectangle (0,0,
                this.c_vpanel.Width , this.c_vpanel.Height -1);
            }
        }
        void XDockingPanel_DoubleClick(object sender, EventArgs e)
        {
            if (this.m_Reduce == true)
            {
                this.Expand();
            }
            else
                this.Reduce();
        }
        void c_splitter_MouseDown(object sender, CoreMouseEventArgs e)
        {
            switch (e.Button)
            {
                case enuMouseButtons.Left:
                    BeginDragSize(e);
                    break;
            }            
        }
        internal void BeginDragSize(CoreMouseEventArgs e)
        {
            XDockingDummyResizingForm drag = new XDockingDummyResizingForm(this);
            drag.FormClosed += new FormClosedEventHandler(drag_FormClosed);
            drag.Show(true);
            Application.AddMessageFilter(drag);
        }
        void drag_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.EndDragSize(sender  as XDockingDummyResizingForm);
        }
        public override string ToString()
        {
            return "XDockingPanel ";
        }
        internal void EndDragSize(XDockingDummyResizingForm form)
        {
            this.SuspendLayout();
            switch (this.DockingDirection)
            {
                case enuDockingDirection.Right:
#pragma warning disable IDE0054 // Use compound assignment
                    this.Width =  this.Width  - form.X;
#pragma warning restore IDE0054 // Use compound assignment
                    //save the max size
                    this.m_maxSize = this.Width;
                    break;
                case enuDockingDirection.Left:
                    this.Width += form.X;
                    //save the max size
                    this.m_maxSize = this.Width;
                    break;
                case enuDockingDirection.Bottom:
                    this.Height -= form.Y;
                    //save the max size
                    this.m_maxSize = this.Height;
                    break;
                case enuDockingDirection.Top:
                    this.Height += form.Y;
                    //save the max size
                    this.m_maxSize = this.Height;
                    break;
            }
            this.ResumeLayout();
        }
        public int GetMinSize()
        {
            switch (DockingDirection)
            {
                case enuDockingDirection.Left:
                case enuDockingDirection.Right:
                    return XDockingTabBar.SIZE + WinCoreSystemInformation.SplitterSize;
                case enuDockingDirection.Top:
                case enuDockingDirection.Bottom:
                    return XDockingTabBar.SIZE + WinCoreSystemInformation.SplitterSize;
            }
            return 0;
        }
        public int GetMaxSize()
        {
            Form frm = this.FindForm ();
            int x = 0; //frm.ClientSize.Width;
            Vector2i c = Vector2i.Zero;
            switch (DockingDirection)
            {
                case enuDockingDirection.Left:
                    x = frm.ClientSize.Width;
                    foreach (IDockingPanel item in this.m_layoutManager.Panels )
	                {
                        if (item == this)
                            continue;
                        if (item .DockingDirection == enuDockingDirection.Right )
                        {
                            if ((item.FindForm() == frm) && (item.Visible ))
                            {                                
                                x = Math.Min(item.Location.X, x);
                            }
                        }
	                }
                    x -= GetMinSize ();
                    return x;
                case enuDockingDirection.Right:
                    x = 0;
                    foreach (IDockingPanel item in this.m_layoutManager.Panels )
	                {
                        if (item == this)
                            continue;
                        if (item .DockingDirection == enuDockingDirection.Left )
                        {
                            if ((item.FindForm() == frm) && (item.Visible))
                            {
                                x = Math.Max(item.Width + item.Location.X, x);
                            }
                        }
	                }
                    x = frm.Width - x - GetMinSize ();
                    return x;                  
                case enuDockingDirection.Top:
                    x = frm.ClientSize.Height ;
                    foreach (IDockingPanel item in this.m_layoutManager.Panels)
                    {
                        if (item == this)
                            continue;
                        if (item.DockingDirection == enuDockingDirection.Bottom)
                        {
                            if ((item.FindForm() == frm) && (item.Visible))
                            {
                                x = Math.Min(item.Location.Y, x);
                            }
                        }
                    }
                    x -= GetMinSize();
                    return x;
                case enuDockingDirection.Bottom:
                    x = 0;
                    foreach (IDockingPanel item in this.m_layoutManager.Panels)
                    {
                        if (item == this)
                            continue;
                        if (item.DockingDirection == enuDockingDirection.Top)
                        {
                            if ((item.FindForm() == frm) && (item.Visible))
                            {
                                x = Math.Max(item.Location.Y, x);
                            }
                        }
                    }
                    x += GetMinSize();
                    return x;
            }
            return 0;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            InitPanelDisplay();
            base.OnSizeChanged(e);
        }
        private void InitPanelDisplay()
        {
            this.SuspendLayout();
            //int w = this.m_Reduce ? this.GetMinSize() : this.Width;
            //int h = this.m_Reduce ? this.GetMinSize() : this.Height ;
            int v_splitsize = ShowSplit ? WinCoreSystemInformation.SplitterSize : 0;
            int v_barSize = ShowTab ? XDockingTabBar.SIZE : 0;
            System.Drawing.Rectangle rc = this.DisplayRectangle;            
            rc.Height -= 1;
            c_vpanel.Bounds = rc;
            //update panel size
            _panelSizeChanged(c_vpanel, EventArgs.Empty);
            switch (this.Dock)
            {
                case System.Windows.Forms.DockStyle.Bottom:
                    this.c_splitter.Bounds = new System.Drawing.Rectangle(0, 0,                    
                        this.Width, v_splitsize );
                    if (this.ShowCaption)
                    {                        
                        this.c_captionBar.Bounds = new System.Drawing.Rectangle(
                            0, v_splitsize,
                            this.Width,
                           XDockingConstant.CAPTION_BAR_SIZE
                            );
                    }
                  
             
                    this.c_tabbar.Bounds = new System.Drawing.Rectangle(
                        0,
                        this.Height - v_barSize,
                        this.Width,
                        v_barSize);
                    this.c_tabbar.Direction = enuTabBarDirection.Horizontal;
                    this.c_splitter.Cursor = Cursors.HSplit;
                    
                           
                    break;
                case System.Windows.Forms.DockStyle.Left:
                    if (this.ShowCaption)
                    {
                        this.c_captionBar.Bounds = new System.Drawing.Rectangle(
                            XDockingTabBar.SIZE, 0,
                            this.Width -( v_splitsize + XDockingTabBar.SIZE),
                            XDockingConstant.CAPTION_BAR_SIZE
                            );
                    }
                    this.c_tabbar.Direction = enuTabBarDirection.VerticalLeft;
                    this.c_splitter.Bounds = new System.Drawing.Rectangle(this.Width -
                v_splitsize, 0,
                        v_splitsize
                        ,                    
                        this.Height );                  
                    this.c_tabbar.Bounds = new System.Drawing.Rectangle(
                        0, 0,
                        v_barSize,
                        this.Height);
                    this.c_splitter.Cursor = Cursors.VSplit;
                    break;
                case System.Windows.Forms.DockStyle.Right:
                    this. c_tabbar.Direction = enuTabBarDirection.VerticalRight;
                    this.c_splitter.Bounds = new System.Drawing.Rectangle(0
                        ,  0,
                        v_splitsize ,
                        this.Height);
                    if (this.ShowCaption)
                    {
                        this.c_captionBar.Bounds = new System.Drawing.Rectangle(
                            rc.X, 0,
                            rc.Width,
                           XDockingConstant.CAPTION_BAR_SIZE
                            );
                    }
                    this.c_tabbar.Bounds = new System.Drawing.Rectangle(
                        this.Width - v_barSize, 0,
                        v_barSize,
                        this.Height);
                    this.c_splitter.Cursor = Cursors.VSplit;
                  
                    break;
                default:
                    this.ShowSplit = false;
                    this.c_vpanel.Visible = true;
                    break;
            }
            this.ResumeLayout();
        }
        void XDockingPanel_DockChanged(object sender, EventArgs e)
        {
            InitPanelDisplay();
        }
        #region IDockingPanel Members
        public IDockingPage SelectedPage
        {
            get
            {
                return this.m_selectedPage;
            }
            set
            {
                if (this.m_selectedPage != value)
                {
                    if (this.m_selectedPage != null)
                    {
                        this.m_selectedPage.HostedControl.Visible = false;
                    }
                        this.m_selectedPage = value;
                        OnSelectedPageChanged(EventArgs.Empty);
                }
            }
        }
        private void OnSelectedPageChanged(EventArgs eventArgs)
        {
            InitPanelDisplay();
            this.c_content.UpdateViewSize(true);
            
            if (this.SelectedPageChanged != null)
                this.SelectedPageChanged(this, eventArgs);
        }
        public IDockingPageCollections Pages
        {
            get { return this.m_pages; }
        }
        public event EventHandler SelectedPageChanged;
        public event DockingPageEventHandler PageAdded;
        public event DockingPageEventHandler PageRemoved;
        #endregion
        /// <summary>
        /// represent a docking page collections
        /// </summary>
        class XDokingPageCollections : IDockingPageCollections
        {
            List<IDockingPage> m_list;
            XDockingPanel m_ower;
            /// <summary>
            /// get the tool name
            /// </summary>
            /// <param name="toolName"></param>
            /// <returns></returns>
            public IDockingPage this[string toolName] {
                get {
                    foreach (var v in m_list)
                    {
                        if (v.Tool.Id == toolName)
                            return v;
                    }
                    return null;
                }
            }
            public XDokingPageCollections(XDockingPanel ower)
            {
                this.m_list = new List<IDockingPage>();
                this.m_ower = ower;
            }
            #region IDockingPageCollections Members
            public void AddPage(IDockingPage page)
            {
                if ((page == null) || (page.Panel == this.m_ower ))
                    return;
                if (!this.m_list.Contains(page))
                {
                    //remove from other panel
                    if (page.Panel != null)
                    {
                        page.Panel.Pages.RemovePage(page);
                    }
                    int i = this.m_list.IndexOf(page);
                    this.m_list.Add(page);
                    page.Panel =this.m_ower;
                    page.HostedControl.Visible = false;
                    this.m_ower.OnPageAdded(new DockingPageEventArgs(page, i));
                }
            }
            public void RemovePage(IDockingPage page)
            {
                if (this.m_list.Contains(page))
                {
                    int i = this.m_list.IndexOf (page);
                    page.Panel = null;
                    this.m_list.Remove (page);
                    this.m_ower.OnPageRemoved(new DockingPageEventArgs(page,i));
                }
            }
            public IDockingPage this[int index]
            {
                get {
                    if (index !=-1)
                    return this.m_list[index];
                    return null;
                }
            }
            public int Count
            {
                get { return this.m_list.Count; }
            }
            public int IndexOf(IDockingPage page)
            {
                return this.m_list.IndexOf(page);
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }
            #endregion
            public bool Contains(IDockingPage page) {
                if (page == null) 
                    return false;
                return this.m_list.Contains(page);
            }
        }
        internal void OnPageAdded(DockingPageEventArgs e)
        {
            m_manager.InitPage(e);
            this.InitPanelDisplay();
            if (this.PageAdded != null)
                this.PageAdded(this, e);
        }
        internal void OnPageRemoved(DockingPageEventArgs e)
        {
            if (this.PageRemoved !=null)
            {
                this.PageRemoved (this, e);
            }
        }
        #region IDockingPanel Members
        public enuDockingDirection DockingDirection
        {
            get {
                switch (this.Dock)
                {
                    case DockStyle.Left :
                        return enuDockingDirection.Left;
                    case DockStyle.Bottom :
                        return enuDockingDirection.Bottom;
                    case DockStyle.Right :
                        return enuDockingDirection.Right;
                    case DockStyle.Top :
                        return enuDockingDirection.Top;
                }
                return enuDockingDirection.Left;
            }
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.Name; }
        }
        #endregion
        /// <summary>
        /// docing page manager
        /// </summary>
        class DockingPageManager
        {
            XDockingPanel m_owner;
            public DockingPageManager(XDockingPanel owner)
            {
                this.m_owner = owner;
                this.m_owner.SelectedPageChanged += new EventHandler(m_owner_SelectedPageChanged);                
                this.m_owner.PageRemoved += new DockingPageEventHandler(m_owner_PageRemove);
                this.m_owner.Disposed += new EventHandler(m_owner_Disposed);
            }
            void m_owner_Disposed(object sender, EventArgs e)
            {
                this.m_owner.SelectedPageChanged -= new EventHandler(m_owner_SelectedPageChanged);
                this.m_owner.PageRemoved -= new DockingPageEventHandler(m_owner_PageRemove);
                this.m_owner.Disposed -= new EventHandler(m_owner_Disposed);
            }
            void m_owner_PageRemove(object sender, DockingPageEventArgs e)
            {
                if (this.m_owner.Pages.Count > 0)
                {
                    this.m_owner.SelectedPage = this.m_owner.Pages[e.Index - 1];                    
                }
                else
                {
                    this.m_owner.SelectedPage = null;
                    this.m_owner.Hide ();//.Reduce();
                }
            }
            void m_owner_SelectedPageChanged(object sender, EventArgs e)
            {
                if (this.m_owner.SelectedPage != null)
                {
                    this.m_owner.c_content.Controls.Add(this.m_owner.SelectedPage.HostedControl as Control);
                    this.m_owner.SelectedPage.HostedControl.Visible = true;
                }
                else {
                    this.m_owner.c_content.Controls.Clear();
                    if (this.m_owner.Pages.Count > 0)
                    {
                        this.m_owner.SelectedPage = this.m_owner.Pages[0];
                    }
                }
            }
            internal void InitPage(DockingPageEventArgs e)
            {
                //select the new page and show tab if not visible
                /*
                 * Maintain the visitiblity
                 * 
                 * */
                this.m_owner.SelectedPage = e.Page;
                if (!this.m_owner.Visible)
                {
                    this.m_owner.Visible = true;
                }    
            }
        }
        public void Expand()
        {
            if (this.m_Reduce)
            {
                this.m_Reduce = false ;
                this.ShowSplit  = true;
                this.c_splitter.Bounds = System.Drawing.Rectangle.Empty;
                switch (this.Dock)
                {
                    case DockStyle.Bottom:
                    case DockStyle.Top:
                        this.Height = RestoreSize;
                        break;
                    case DockStyle.Fill:
                        break;
                    case DockStyle.Left:
                    case DockStyle.Right:
                        this.Width = RestoreSize;
                        break;
                }
                this.InitPanelDisplay();
            }
        }    
        public void Reduce()
        {
            if (this.m_Reduce!= true)
            {
                this.m_Reduce = true ;
                this.ShowSplit = false;
                this.c_splitter.Bounds = System.Drawing.Rectangle.Empty;
                switch (this.Dock)
                {
                    case DockStyle.Bottom:
                    case DockStyle.Top :
                        this.m_RestoreSize = this.Height;
                        this.Height = this.GetMinSize();
                        break;
                    case DockStyle.Left:                        
                    case DockStyle.Right:
                        this.m_RestoreSize = this.Width;
                        this.Width = this.GetMinSize();
                        break;
                }
                this.InitPanelDisplay();
            }
        }
        public void BeginDragContent()
        {
            DragContentHandler drag = new DragContentHandler(this);
            Application.AddMessageFilter(drag);
        }
        public  void EndDragContent()
        {
        }
        //#region IDockingPanel Members
        //public WinCoreLayoutManagerBase LayoutManager
        //{
        //    get { return this.m_layoutManager; }
        //}
        //#endregion
    }
}

