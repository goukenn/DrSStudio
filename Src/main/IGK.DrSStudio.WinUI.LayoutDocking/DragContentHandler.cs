

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DragContentHandler.cs
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
file:DragContentHandler.cs
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
using System.Windows.Forms ;
using System.Drawing ;
using System.Runtime.InteropServices ;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;
    using IGK.DrSStudio.WinUI.Native;
    using IGK.ICore;
    using IGK.ICore.WinCore;

    /// <summary>
    /// represent a drag 
    /// </summary>
    public class DragContentHandler : IMessageFilter
    {
        const int DRAG_SIZE = 32;
        internal IDockingPanel m_panel;
        internal IDockingForm m_dockingForm;
        private DragForm m_OverForm;
        internal bool Performed;
        private Vector2i  m_startPoint;
        private Vector2i m_offset;
        DockingDragViewForm m_dragRight;
        DockingDragViewForm m_dragBottom;
        DockingDragViewForm m_dragLeft;
        enuDragingState m_dragingState;
        /// <exclue/>            
        public enuDragingState DragingState { get { return this.m_dragingState; } }
        public IDockingPanel  Panel { get { return this.m_panel; } }
        public IDockingForm DraginForm { get { return this.m_dockingForm; } }
        private IXForm m_oldOwner;
        /// <summary>
        /// draw form hander
        /// </summary>
        /// <param name="form">form to dock</param>
        /// <param name="mousePosition">start postion</param>
        /// <param name="offset">offset</param>
        internal DragContentHandler(IDockingForm form, Vector2i mousePosition)
        {
            if (form == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "form");
            this.m_dockingForm = form;
            this.m_dragingState = enuDragingState.Form;
            this.m_startPoint = mousePosition;
            this.m_offset = form.PointToClient(mousePosition);
            this.InitHandler();
            this.m_oldOwner = this.m_dockingForm.Owner;
            this.m_dockingForm.Owner = this.m_OverForm;
        }
        internal DragContentHandler(IDockingPanel panel)
        {
            this.m_panel = panel;
            this.m_dragingState = enuDragingState.Panel;
            InitHandler();
        }
        private void InitHandler()
        {
            this.m_OverForm = new DragForm(this);
            this.m_OverForm.StartPosition = FormStartPosition.Manual;
            this.m_OverForm.Location = new System.Drawing.Point(-999, -999);
            this.m_OverForm.Owner = CoreSystem.Instance.MainForm as Form;
            this.m_OverForm.Bounds = this.m_OverForm.Owner.Bounds;
            this.m_OverForm.Show();
            this.m_OverForm.Capture = true;
            this.m_OverForm.FormClosed += new FormClosedEventHandler(m_OverForm_FormClosed);
            this.m_OverForm.Disposed += m_OverForm_Disposed;
            this.m_OverForm.MouseMove += m_OverForm_MouseMove;
            this.Performed = false;
            m_dragRight = new DockingDragViewForm();
            m_dragLeft = new DockingDragViewForm();
            m_dragBottom = new DockingDragViewForm();
            Rectangle m_screnRectangle = this.m_OverForm.Bounds;
            m_dragRight.Owner = this.m_OverForm;
            m_dragLeft.Owner = this.m_OverForm;
            m_dragBottom.Owner = this.m_OverForm;
            m_dragRight.m_BackGroundDocument =Clone( CoreResources.GetDocument(XDockingConstant.DRAGING_RIGHT ));
            m_dragBottom.m_BackGroundDocument = Clone(CoreResources.GetDocument(XDockingConstant .DRAGING_BOTTOM));
            m_dragLeft.m_BackGroundDocument = Clone(CoreResources.GetDocument(XDockingConstant.DRAGING_LEFT));
            m_dragRight.Tag = enuLayoutToolDisplay.Right;
            m_dragLeft.Tag = enuLayoutToolDisplay.Left;
            m_dragBottom.Tag = enuLayoutToolDisplay.Bottom;
            m_dragRight.Bounds = new Rectangle(m_screnRectangle.X + m_screnRectangle.Width -
                DRAG_SIZE,
                m_screnRectangle.Y + (m_screnRectangle.Height / 2) - (DRAG_SIZE / 2),
                DRAG_SIZE,
                DRAG_SIZE);
            m_dragLeft.Bounds = new Rectangle(m_screnRectangle.X,
               m_screnRectangle.Y + (m_screnRectangle.Height / 2) - (DRAG_SIZE / 2),
               DRAG_SIZE,
               DRAG_SIZE);
            m_dragBottom.Bounds = new Rectangle(
                m_screnRectangle.X + (m_screnRectangle.Width / 2) - (DRAG_SIZE / 2),
               m_screnRectangle.Y + m_screnRectangle.Height - DRAG_SIZE,
               DRAG_SIZE,
               DRAG_SIZE);
            m_dragRight.Show();
            m_dragLeft.Show();
            m_dragBottom.Show();
        }

        private ICore2DDrawingDocument Clone(ICore2DDrawingDocument document)
        {
            if (document == null)
                return null;
            return document.Clone() as ICore2DDrawingDocument;
        }
        void m_OverForm_MouseMove(object sender, CoreMouseEventArgs e)
        {
            if ((e.Button == enuMouseButtons.None) && (this.m_dockingForm !=null))
            {
                //restore the old owner
                //---------------------
                this.m_dockingForm.Owner = null;
                this.m_OverForm.Close();
            }
        }
        void m_OverForm_Disposed(object sender, EventArgs e)
        {
            this.EndDrag();
        }
        void m_OverForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_dragRight.Close();
            m_dragLeft.Close();
            m_dragBottom.Close();
        }
        #region IMessageFilter Members
        public bool PreFilterMessage(ref Message m)
        {
            switch (this.DragingState)
            { 
                case enuDragingState.Panel :
                    return DragPanel(ref m);
                case enuDragingState.Form :
                    return DragForm(ref m);
            }
            return false;
        }
        private bool DragForm(ref Message m)
        {
            switch (m.Msg)
            {
                case User32.WM_MOUSEMOVE:                   
                    {                       
                        int x = User32.GET_X_LPARAM(m.LParam);
                        int y = User32.GET_Y_LPARAM(m.LParam);
                        Point pt = new Point(x, y);
                        Point mpos = Control.MousePosition;
                        //set the location
                        if (this.m_dockingForm != null)
                        {
                            Form frm = (m_dockingForm as Form);
                            if (this.m_OverForm.Capture)
                            {
                                Vector2i  c = this.m_startPoint;
                                (m_dockingForm as Form).Bounds = new Rectangle(
                                    c.X - m_offset.X + (mpos.X - c.X),// frm.Width / 2,
                                    c.Y - m_offset.Y + (mpos.Y - c.Y),
                                    m_dockingForm.Bounds.Size.Width,
                                    m_dockingForm.Bounds.Size.Height);
                            }
                        }
                        if (m_OverForm.RectangleToClient(m_dragLeft.Bounds).Contains(pt))
                        {
                            m_dragLeft.Capture = true;
                        }
                        else if (m_OverForm.RectangleToClient(m_dragRight.Bounds).Contains(pt))
                        {
                            m_dragRight.Capture = true;
                        }
                        else if (m_OverForm.RectangleToClient(m_dragBottom.Bounds).Contains(pt))
                        {
                            m_dragBottom.Capture = true;
                        }
                    }
                    break;
                case User32.WM_LBUTTONUP:
                    {
                        int x = User32.GET_X_LPARAM(m.LParam);
                        int y = User32.GET_Y_LPARAM(m.LParam);
                        Point pt = new Point(x, y);
                        if (!Performed && (this.m_OverForm.Handle == m.HWnd))
                        {
                            //set the new  location
                            //---------------------
                            Form frm = (this.m_dockingForm as Form);
                            Point v_pt = Control.MousePosition;
                            frm.Location = new Point(
                                v_pt.X - m_offset.X,
                                v_pt.Y - m_offset.Y
                                   );
                            this.m_dockingForm.Owner = null;
                            this.m_dockingForm.Owner = this.m_oldOwner;
                        }
                        else
                        {
                            //remove selected panel before sending to content
                            //-----------------------------------------------
                            User32.SendMessage(m.HWnd, m.Msg, m.WParam, m.LParam);
                            this.m_dockingForm.Owner = null;
                            this.m_dockingForm.Owner = this.m_oldOwner;
                            this.m_dockingForm.Close();
                        }
                        //remove filter message
                        //---------------------
                        this.EndDrag();                        
                        this.m_OverForm.Capture = false;                        
                        this.m_OverForm.Close();
                        this.m_OverForm.Dispose();
                        GC.SuppressFinalize(m_OverForm);
                        CoreSystem.Instance.MainForm.Show();
                        CoreSystem.Instance.MainForm.Focus();
                        return true;
                    }
            }
            return false;
        }
        private bool DragPanel(ref Message m)
        {
            switch (m.Msg)
            {
                case User32.WM_MOUSEMOVE:
                    {
                        if (Control.MouseButtons == MouseButtons.None)
                        {
                            return false;
                        }
                        int x = User32.GET_X_LPARAM(m.LParam);
                        int y = User32.GET_Y_LPARAM(m.LParam);
                        Point pt = new Point(x, y);
                        Point mpos = Control.MousePosition;
                        //set the location
                        if (this.m_dockingForm != null)
                        {
                            Form frm = (m_dockingForm as Form);
                            if (this.m_OverForm.Capture)
                            {
                                Vector2i  c = this.m_startPoint;
                                (m_dockingForm as Form).Bounds = new Rectangle(
                                    c.X - m_offset.X + (mpos.X - c.X),// frm.Width / 2,
                                    c.Y - m_offset.Y + (mpos.Y - c.Y),
                                    m_dockingForm.Bounds.Size.Width,
                                    m_dockingForm.Bounds.Size.Height);
                            }
                        }
                        if (m_OverForm.RectangleToClient(m_dragLeft.Bounds).Contains(pt))
                        {
                            m_dragLeft.Capture = true;
                        }
                        else if (m_OverForm.RectangleToClient(m_dragRight.Bounds).Contains(pt))
                        {
                            m_dragRight.Capture = true;
                        }
                        else if (m_OverForm.RectangleToClient(m_dragBottom.Bounds).Contains(pt))
                        {
                            m_dragBottom.Capture = true;
                        }
                    }
                    break;
                case User32.WM_LBUTTONUP:
                    {
                        if (this.m_panel.SelectedPage == null)
                        {
                            this.EndDrag();
                            return false;
                        }
                        int x = User32.GET_X_LPARAM(m.LParam);
                        int y = User32.GET_Y_LPARAM(m.LParam);
                        Point pt = new Point(x, y);
                        if (!Performed && (this.m_OverForm.Handle == m.HWnd))
                        {
                            //undoc selected pages
                            IDockingPage page = this.m_panel.SelectedPage as IDockingPage;
                            //remove page from page collection
                            this.m_panel.Pages.RemovePage(page);
                            //float the element
                            page.ToolDisplay = enuLayoutToolDisplay.Float;
                            //show floating tool 
                            //this.m_panel.LayoutManager.ShowTool(page.Tool);
                            if (page.DockingForm != null)
                            {
                                page.DockingForm.Location =
                                    new Vector2i (Control.MousePosition.X ,
                                        Control.MousePosition .Y );
                            }
                        }
                        else
                        {
                            User32.SendMessage(m.HWnd, m.Msg, m.WParam, m.LParam);
                        }
                        this.EndDrag();
                        this.m_OverForm.Capture = false;
                        this.m_OverForm.Hide();
                        this.m_OverForm.Close();
                        this.m_OverForm.Dispose();
                        GC.SuppressFinalize(m_OverForm);
                        CoreSystem.Instance.MainForm.Activate();                        
                        return true;
                    }
            }
            return false;
        }
        private void EndDrag()
        {
            Application.RemoveMessageFilter(this);
        }
        #endregion
    }
    //mark width sealed
    class DockingDragViewForm : XForm
    {
        internal ICore2DDrawingDocument m_BackGroundDocument;
        protected override Size DefaultMinimumSize
        {
            get
            {
                return Size.Empty;
            }
        }
        internal DockingDragViewForm()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.SuspendLayout();
            //this.TransparencyKey = Color.Fuchsia;// ;.Transparent ;//.Lime;
            //this.BackColor = Color.Fuchsia;//.Transparent ;//.Lime;
            this.ShowInTaskbar = false;
            this.ControlBox = false;
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new Size(0, 0);
            this.Size = new Size(128, 128);
            this.TopMost = true;
            this.Capture = false;
            this.Opacity = 0.5f;
            this.ResumeLayout();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_BackGroundDocument != null)
                {
                    this.m_BackGroundDocument.Dispose();
                    this.m_BackGroundDocument = null;
                }
            }
            base.Dispose(disposing);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Opacity = 1.0f;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Opacity = .5f;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!this.ClientRectangle.Contains(e.Location))
            {
                this.Owner.Capture = true;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    enuLayoutToolDisplay v_display = (enuLayoutToolDisplay)this.Tag;
                    DragContentHandler g = (this.Owner as DragForm).m_dragHandler;
                    IDockingPage page = null;
                    switch (g.DragingState)
                    {
                        case enuDragingState.Panel:
                            {
                                page = g.m_panel.SelectedPage;
                                if (page.ToolDisplay != v_display)
                                {
                                    g.Performed = true;
                                    g.m_panel.Pages.RemovePage(page);
                                    //change the tool display
                                    page.ToolDisplay = v_display;
                                    //g.m_panel.LayoutManager.ShowTool(page.Tool);
                                }
                            }
                            break;
                        case enuDragingState.Form :                            
                            page = g.m_dockingForm.DockingPanel.SelectedPage ;
                            g.Performed = true;
                            page.ToolDisplay = v_display;
                            //g.m_dockingForm.DockingPanel.LayoutManager.ShowTool(page.Tool);                            
                            break;
                    }
                    break;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.m_BackGroundDocument != null)
            {
                this.m_BackGroundDocument.Draw(e.Graphics, this.ClientRectangle);
            }
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case User32.WM_GETMINMAXINFO:
                    MINMAXINFO v_maxinfo = (MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(MINMAXINFO));
                    Rectangle v_rc = Rectangle.Empty;
                    //change the minimum size
                    v_maxinfo.ptMinTrackSize = new Point(0, 0);
                    Marshal.StructureToPtr(v_maxinfo, m.LParam, true);
                    m.Result = IntPtr.Zero;
                    return;
            }
            base.WndProc(ref m);
        }
    }
    //mark with sealed dummy. represent the form to display
    sealed class DragForm : XForm
    {
        internal DragContentHandler m_dragHandler;
        internal DragForm(DragContentHandler dragHandler)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.m_dragHandler = dragHandler;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.Opacity = 0.15f;
            this.BackColor = Color.Black;
        }
    }
}

