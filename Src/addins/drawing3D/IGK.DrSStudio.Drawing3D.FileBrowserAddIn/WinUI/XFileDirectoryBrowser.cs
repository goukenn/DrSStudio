

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XFileDirectoryBrowser.cs
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
file:XFileDirectoryBrowser.cs
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
using System.Collections ;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
namespace IGK.DrSStudio.FBAddIn.WinUI
{
    
using IGK.DrSStudio.WinUI;
    using IGK.ICore.IO;
    using IGK.ICore;
    /// <summary>
    /// represent the direcotyr file browser
    /// </summary>
    public class XFileDirectoryBrowser : IGKXControl, IFBDirectoryBrowser ,IDirectoryBrowserItemOwner
    {
        private const int ITEM_HEIGHT = 16;
        private FBDirectoryBrowserItem.FBDirectoryBrowserItemCollection  m_Nodes;
        private char m_DirectorySeparator = Path.DirectorySeparatorChar ;
        private FBDirectoryBrowserItem m_topNode;
        private int m_ItemHeight = ITEM_HEIGHT ;
        //private XVerticalScrollBar c_vScrollBar;
        //private XHorizontalScrollBar c_hScrollBar;
        public FBDirectoryBrowserItem.FBDirectoryBrowserItemCollection Nodes
        {
            get {
                return this.m_Nodes;
            }
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(210, 300);
            }
        }
        /// <summary>
        /// get the item heih
        /// </summary>
        public int ItemHeight
        {
            get { return m_ItemHeight; }
            set
            {
                if (m_ItemHeight != value)
                {
                    m_ItemHeight = value;
                }
            }
        }
        public char DirectorySeparator
        {
            get { return m_DirectorySeparator; }
            set
            {
                if (m_DirectorySeparator != value)
                {
                    m_DirectorySeparator = value;
                }
            }
        }
        public string SelectedFolder
        {
            get { return this.surface.SelectedFolder ; }
            set
            {
                this.surface.SelectedFolder = value;
            }
        }
        public event EventHandler SelectedFolderChanged {
            add { this.surface.SelectedFolderChanged += value; }
            remove { this.surface.SelectedFolderChanged -= value; }
        }
        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(40, 0);
            }
        }
        FBControlSurface surface;
        public XFileDirectoryBrowser( FBControlSurface surface)
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);            
            this.surface = surface;
            this.Paint += _Paint;        
            this.m_Nodes = new FBDirectoryBrowserItem.FBDirectoryBrowserItemCollection(null);
            //this.c_hScrollBar = new XHorizontalScrollBar();
            //this.c_vScrollBar = new XVerticalScrollBar();
            //this.Controls.Add(this.c_hScrollBar);
            //this.Controls.Add(this.c_vScrollBar);
            this.m_ItemHeight = ITEM_HEIGHT;
            Initialize();
            this.SizeChanged += new EventHandler(XFileDirectoryBrowser_SizeChanged);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
        void XFileDirectoryBrowser_SizeChanged(object sender, EventArgs e)
        {
            SetupScroll();
        }
        void SetupScroll()
        {
            int h = System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight;
            int w = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            //this.c_vScrollBar.Bounds = new Rectangle(
            //    this.Width - w,
            //    0,
            //    w,
            //    this.Height - h);
            //this.c_hScrollBar.Bounds = new Rectangle(
            //    0,
            //    this.Bottom  - h,
            //    this.Width - w ,                
            //    h);
        }
        public int VisibleCount {
            get {
                return this.Height / ITEM_HEIGHT;
            }
        }
        public int VisibleRCount {
            get {
                return 0;
            }
        }
        public FBDirectoryBrowserItem GetItemAt(int x, int y)
        {
            IFBDirectoryBrowserItemEnumerator e = null;
            int v_nodes = -1;
            foreach (FBDirectoryBrowserItem  h in this.Nodes)
            {
                e = h.Nodes;
                e.Reset();
                while (e.MoveNext())
                { 
                }
                v_nodes++;
            }
            return null;
        }
        public FBDirectoryBrowserItem GetItemAt(Point pt)
        {
            return GetItemAt(pt.X, pt.Y);
        }
        public void GetScrollTopNode()
        {
            TopNodeEnumerator d = new TopNodeEnumerator(this.Nodes);
            while (d.MoveNext())
            {
            }
            IFBDirectoryBrowserItemEnumerator e = null;
            int v_nodes = -1;
            foreach (FBDirectoryBrowserItem h in this.Nodes)
            {
                e = h.Nodes;
                e.Reset();
                while (e.MoveNext())
                {
                    if (v_nodes == c_vScrollBar.Value)
                    {
                        this.m_topNode = e.Current;
                        this.Invalidate();
                        return;
                    }
                    v_nodes++;
                }
                v_nodes++;
            }
        }
        private void Initialize()
        {
            Environment.SpecialFolder[] t = new Environment.SpecialFolder[] { 
                 Environment.SpecialFolder.Desktop,
                 Environment.SpecialFolder.MyDocuments ,
                 Environment.SpecialFolder.MyPictures 
            };
            for (int i = 0; i < t.Length; i++)
            {
                FBDirectoryBrowserItem c =     this.Nodes.Add(Environment.GetFolderPath(t[i]),
                        PathUtils.GetDisplayName(t[i])) as FBDirectoryBrowserItem ;               
            }
            FBDirectoryBrowserItem db = new FBComputerDirectoryBrowserItem( PathUtils.GetDisplayName(Environment.SpecialFolder.MyComputer));
            this.Nodes.Add(db);
        }
        public event FBDirectoryBrowserItemEventHandler  BrowserItemAdded;
        public event FBDirectoryBrowserItemEventHandler BrowserItemRemoved;
        ///<summary>
        ///raise the BrowserItemRemoved 
        ///</summary>
        protected virtual void OnBrowserItemRemoved(FBDirectoryBrowserItemEventArgs e)
        {
            if (BrowserItemRemoved != null)
                BrowserItemRemoved(this, e);
        }
        ///<summary>
        ///raise the BrowserItemAdded 
        ///</summary>
        protected virtual void OnBrowserItemAdded(FBDirectoryBrowserItemEventArgs e)
        {
            if (this.Created)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate()
                    {
                        this.Controls.Add(e.Item as Control);
                    });
                }
                else
                    this.Controls.Add(e.Item as Control);
            }
            if (BrowserItemAdded != null)
                BrowserItemAdded(this, e);
        }
        #region IDirectoryBrowserItemOwner Members
        IDirectoryBrowserItemCollections IDirectoryBrowserItemOwner.Nodes
        {
            get { return m_Nodes; }
        }
        #endregion
        void _Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            this.Render(e.Graphics);
        }
        private void Render(Graphics g)
        {
            int offset_x = 0;
            int offset_y = 0;
            int y = -offset_y;
            int i = 0;
            bool alternate = false;
            Colorf cl1 = FileBrowserRenderer.FileBrowserHeightLight1;
            Colorf cl2 = FileBrowserRenderer.FileBrowserHeightLight2;
            Brush br = null;
            Pen p = CoreBrushRegister.GetPen(Colorf.FromFloat(0.3f));
            p.DashStyle = DashStyle.Dot;
            int v_yoffset = (int)Math.Ceiling (ITEM_HEIGHT / 2.0F);
            int v_xoffset = 5 - offset_x ;
            //clear background
            while (y < this.Height)
            {
                if (alternate)
                    br = WinCoreBrushRegister.GetBrush(cl1);
                else
                    br = WinCoreBrushRegister.GetBrush(cl2);
                g.FillRectangle(br, new Rectanglef(0, y, this.Width, this.ItemHeight ));
                alternate = ((i % 2) == 0);
                i++;
                y += this.ItemHeight;
            }
            TopNodeEnumerator d = new TopNodeEnumerator(Nodes);
            Rectangle v_bounds = new Rectangle (16,0, 150, ITEM_HEIGHT );
            while (d.MoveNext ())
            {
                g.DrawString(d.Current.DisplayName, this.Font, SystemBrushes.HighlightText, v_bounds);
                v_bounds.Y += ITEM_HEIGHT;
                //if (d.Current.Expended)
                //{
                //    v_bounds.X += ITEM_HEIGHT * (d.Current .Depth + 1);
                //    while (d.MoveNext())
                //    {
                //        g.DrawString(d.Current.DisplayName, this.Font, SystemBrushes.HighlightText, v_bounds);
                //        v_bounds.Y += ITEM_HEIGHT;
                //    }
                //    v_bounds.X -= ITEM_HEIGHT * (item.Depth + 1);
                //}
            }
            p.DashStyle = DashStyle.Solid;
        }
        class TopNodeEnumerator
        {
            private FBDirectoryBrowserItem m_Current;
            private FBDirectoryBrowserItem.FBDirectoryBrowserItemCollection m_nodes;
            private bool m_first;
            private int m_level;
            private int m_index;
            public FBDirectoryBrowserItem Current {
                get {
                    return m_Current;
                }
            }
            public bool MoveNext()
            {
                if (this.m_first)
                {
                    if (this.m_nodes.Count > 0)
                    {
                        this.m_Current = this.m_nodes[0];
                        m_index = 0;
                        this.m_first = false;
                        return true;
                    }
                    return false;
                }
                if (this.m_Current == null)
                    return false;
                if (this.Current .Expended && (this.m_Current.Nodes.Count > 0))
                {
                    this.m_Current = this.m_Current.Nodes[0];
                    this.m_level++;
                }
                else {
                        FBDirectoryBrowserItem v_next = this.m_Current.Next;
                        FBDirectoryBrowserItem v_parent = this.m_Current.Parent;
                        while (v_next == null)
                        {
                            if (v_parent == null)
                            {
                                if (m_index < (this.m_nodes.Count - 1))
                                {
                                    this.m_Current = this.m_nodes[m_index + 1];
                                    m_index++;
                                    return true;
                                }
                                else
                                {
                                    //no parent
                                    v_next = this.m_Current;
                                    return false;
                                }
                            }
                            else
                            {
                                //go to parent
                                v_parent = v_parent.Parent;
                                this.m_level--;
                            }
                        }
                        this.m_Current = v_next;
                }
                return true;
            }
            public void Reset()
            {
                this.m_first = true ;
                this.m_Current = null;
                this.m_index = -1;
            }
            public TopNodeEnumerator(FBDirectoryBrowserItem.FBDirectoryBrowserItemCollection nodes)
            {
                this.m_nodes = nodes;
                this.m_first = true;
            }
        }
    }
}

