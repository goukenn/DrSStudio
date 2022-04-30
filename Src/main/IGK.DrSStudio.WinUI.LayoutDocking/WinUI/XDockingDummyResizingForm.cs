

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDockingDummyResizingForm.cs
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
file:XDockingDummyResizingForm.cs
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
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Windows.Forms;
using IGK.ICore.WinCore;
using IGK.ICore;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// dummy for used for sezizeing
    /// </summary>
    class XDockingDummyResizingForm : Form , 
        IMessageFilter 
    {
        IDockingPanel m_panel;
        private Rectangle m_screenRectangle;
        int m_x;
        int m_y;
        public int X { get { return this.m_x; } }
        public int Y { get { return this.m_y; } }
        public XDockingDummyResizingForm(IDockingPanel panel)
        {
            this.m_panel = panel;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(-9999, 0);
            this.Size = Size.Empty;
            this.BackColor = Color.Black;
            this.Opacity = 0.35f;
            this.Owner = (panel as Control).FindForm();            
            m_screenRectangle = this.Owner.DesktopBounds;
            this.ShowInTaskbar = false;
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.ClientSize = m_panel.Splitter.ClientSize;
            Point v_loc = this.m_panel.Splitter.PointToScreen(m_panel.Splitter.Location);
            Form frm = (this.m_panel as Control).FindForm();
            Point v_loc1 = frm.Location;
            this.Location = new Point (
                (int) Math.Round ((float)(v_loc1.X +Math.Abs (v_loc .X ))),
                (int) v_loc .Y);
        }
        public void Show(bool activate)
        {
            this.Show();
            if (activate)
                this.Focus();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                Application.RemoveMessageFilter(this);
                this.Close();
            }
            base.OnMouseMove(e);
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
        }
        #region IMessageFilter Members
        internal const int WM_MOUSEMOVE = 0x200;
        internal const int WM_LBUTTONUP = 0x0202;
        public static int GET_Y_LPARAM(IntPtr lParam)
        {
            long i = lParam.ToInt64();
            return (int)((short)((i & 0xFFFF0000) >> 16));
        }
        public static int GET_X_LPARAM(IntPtr lParam)
        {
            long i = lParam.ToInt64();
            return (int)(((short)i) & 0x0000FFFF);
        }
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEMOVE :
                    int x = GET_X_LPARAM(m.LParam);
                    int y = GET_Y_LPARAM(m.LParam);
                    if ((x & 0x8000) != 0)
                    {
#pragma warning disable IDE0054 // Use compound assignment
                        x = x - ushort.MaxValue;
#pragma warning restore IDE0054 // Use compound assignment
                    }
#pragma warning disable IDE0054 // Use compound assignment
                    if ((y > 0) && ((y & 0x8000) != 0)) { y = y - ushort.MaxValue; }
#pragma warning restore IDE0054 // Use compound assignment
                    //get the point of the move 
                    Point v_p = new Point(x, y);
                    //get the point to splitter screen
                    Point p = m_panel.Splitter.PointToScreen(v_p);
                    Vector2i  v_po = m_panel.PointToScreen(this.m_panel.Location);
                    int min = m_panel.GetMinSize();
                    int max_width = m_panel.GetMaxSize(); // this.m_screenRectangle.Width - min;
                    int v_rc = 0;
                    if (m_screenRectangle.Contains(p))
                    {
                        switch (m_panel.DockingDirection)
                        {
                            case enuDockingDirection.Left:
                                //int c = m_panel.Width + v_p.X;
                                //int h = m_panel.Width - v_p.X;
                                v_rc = Math.Abs(p.X - v_po.X ) ;
                                if ((v_rc>= min)&&  (v_rc <= max_width))
                                {
                                    this.Left = p.X;
                                    m_x = v_p.X;
                                }
                                break;
                            case enuDockingDirection.Right:
                                {
                                    v_rc = Math.Abs(m_panel.Width - v_p.X);
                                    if ((v_rc > min) && (v_rc <= max_width))//;m_panel.GetMinSize())
                                    {
                                        this.Left = p.X;
                                        m_x = v_p.X;
                                    }
                                }
                                break;
                            case enuDockingDirection.Bottom:
                                {
                                    v_rc = Math.Abs(-v_p.Y + m_panel.Height);
                                    if (v_rc >= min)
                                    {
                                        this.Left = v_po.X;
                                        this.Top = p.Y;
                                        m_y = v_p.Y;
                                    }
                                }
                                break;
                            case enuDockingDirection.Top:
                                v_rc = Math.Abs(m_panel.Height - v_p.Y);
                                if (v_rc >= min) 
                                {
                                        this.Left = v_po.X;
                                        this.Top = p.Y;
                                        m_y = v_p.Y;
                                }
                                break;
                        }
                    }
                    break;
                case WM_LBUTTONUP:
                    Application.RemoveMessageFilter(this);
                    this.Close();
                    break;
            }
            return false ;
        }
        #endregion
    }
}

