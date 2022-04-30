

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDockingCaptionBar.cs
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
file:XDockingCaptionBar.cs
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore.Resources;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.DrSStudio.WinUI.Native;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinCore.WinUI;
    public class XDockingCaptionBar : 
        IGKXControl ,
        IDockingCaptionBar
    {
       private IGKXButton c_close;
       private IGKXButton c_reduce;
       private IGKXButton c_undock;
        private IDockingPanel m_Panel;
        /// <summary>
        /// Get or set the docking panel
        /// </summary>
        public IDockingPanel DockingPanel
        {
            get { return m_Panel; }            
        }
        internal XDockingCaptionBar(IDockingPanel panel)        
        {
            this.m_Panel = panel;
            c_close = new IGKXButton();
            c_reduce = new IGKXButton();
            c_undock = new IGKXButton();
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.Controls.Add(c_close);
            this.Controls.Add(c_reduce);
            this.Controls.Add(c_undock);
            this.MinimumSize = new Size(0, XDockingConstant. CAPTION_BAR_SIZE);
            c_close.Click += new EventHandler(c_close_Click);
            c_reduce.Click += new EventHandler(c_reduce_Click);
            c_undock.Click += new EventHandler(c_undock_Click);
            c_close.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(CoreImageKeys.WINUI_LD_BTN_CLOSE_GKDS));
            c_reduce.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(CoreImageKeys.BTN_REDUCE_GKDS));
            c_undock.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(CoreImageKeys.BTN_UNDOCK_GKDS));
            this.m_Panel.SelectedPageChanged += new EventHandler(m_Panel_SelectedPageChanged);
            this.Paint += _Paint;
        }
        void _Paint(object sender, CorePaintEventArgs e)
        {

            RenderCaptionBar(e);
            //render border
            Rectangle v_rc = this.ClientRectangle;
            v_rc.Width--;
            v_rc.Height--;
            e.Graphics.SmoothingMode = enuSmoothingMode.AntiAliazed;
            e.Graphics.DrawRectangle(Colorf.FromFloat(0.7f, 0.1f), v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);
            e.Graphics.SmoothingMode = enuSmoothingMode.None;

        }
        void c_undock_Click(object sender, EventArgs e)
        {
            if (this.m_Panel .SelectedPage !=null)
                this.m_Panel.SelectedPage.Undock();
        }
        void m_Panel_SelectedPageChanged(object sender, EventArgs e)
        {
            this.Invalidate(true );
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //caption bar click 
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (this.m_Panel.DockingOwner is IDockingForm)
                    {
                        this.Capture = false;
                        User32.SendMessage(this.Parent.Handle, User32.WM_NCLBUTTONDOWN,
                            (IntPtr)User32.HTCAPTION, IntPtr.Zero);
                    }
                    else
                    {
                        if (this.DockingPanel  is XDockingPanel)
                        {
                            //start drawing pages parent
                            (this.m_Panel as XDockingPanel).BeginDragContent();
                        }
                    }
                    break;
                case MouseButtons.Right:
                    if (this.DockingPanel.DockingOwner  is IDockingForm)
                    {
                        this.Capture = false;
                        User32.SendMessage(this.Parent.Handle, 
                            User32.WM_NCLBUTTONDOWN, 
                            (IntPtr)User32.HTMENU, 
                            IntPtr.Zero);
                    }
                    break;
            }
        }
        void c_reduce_Click(object sender, EventArgs e)
        {
            this.DockingPanel.Reduce();
        }
        void c_close_Click(object sender, EventArgs e)
        {
            //remove the tab bar
            if (this.DockingPanel.SelectedPage != null)
            {
                this.DockingPanel.Pages.RemovePage(this.DockingPanel.SelectedPage);
            }
        }
        private void RenderCaptionBar(CorePaintEventArgs e)
        {
            using (LinearGradientBrush ln =
                WinCoreBrushRegister.CreateBrush<LinearGradientBrush>(
                0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height,
                WinCoreControlRenderer.DialogCaptionStartColor,
                WinCoreControlRenderer.DialogCaptionEndColor,
                WinCoreControlRenderer.DialogCaptionAngle)
                )
            {
                e.Graphics.FillRectangle(ln, this.ClientRectangle);
            }
            if ((this.DockingPanel != null) && (this.DockingPanel.SelectedPage != null))
            {
                string text = this.DockingPanel.SelectedPage.Title;
                Rectangle v_rc = this.ClientRectangle;
                v_rc.Inflate(-2, -2);
                v_rc.X += 18;
                v_rc.Width = c_undock.Bounds.Left - 20;
                if (v_rc.Width > 0)
                {
                    if (this.DockingPanel.SelectedPage.Document != null)
                    {
                        this.DockingPanel.SelectedPage.Document.Draw(
                            e.Graphics,
                            new Rectanglei(
                                2, 2,
                                16, 16));
                    }
                    StringFormat v_sf = new StringFormat();
                    v_sf.Trimming = StringTrimming.EllipsisCharacter;
                    v_sf.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
                    v_sf.Alignment = StringAlignment.Near;
                    v_sf.LineAlignment = StringAlignment.Center;
                    Size2f v_size = e.Graphics.MeasureString (text,
                        this.Font, Size2f.MaxShortSize,
                        v_sf );
                    Rectanglef v_rcf = new Rectanglef(v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);
                    e.Graphics.DrawString (text,
                        this.Font,
                        WinCoreBrushRegister.GetBrush(WinCoreControlRenderer.DialogCaptionForeColor),
                        v_rcf ,
                        v_sf);


                    HatchBrush hbr = new HatchBrush(HatchStyle.Percent20 , Color.White , Color.Transparent);
                    Pen p = new Pen(hbr, 6);
                    p.Alignment = PenAlignment.Center;
                    float v_half = this.Height / 2.0f;
                    e.Graphics.DrawLine(p, v_rc.X + v_size.Width, v_half , v_rc.X + v_rc.Width, v_half);
                    p.Dispose();
                    hbr.Dispose();
                    v_sf.Dispose();
                }
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            InitButtonBound();
        }
        private void InitButtonBound()
        {
            Rectangle v_rc =  new Rectangle (
                this.Width - XDockingConstant.CAPTION_BAR_SIZE - 2,
                (this.Height - 16)/2,
                16,
                16);
            v_rc.Inflate (-1,-1);
            c_close.Bounds = v_rc;
            v_rc.X -= v_rc.Width  + 2;
            c_reduce.Bounds = v_rc;
            v_rc.X -= v_rc .Width + 2;
            c_undock.Bounds = v_rc;
        }
        #region IDockingPanelItem Members
        IDockingPanel IDockingPanelItem.DockingPanel
        {
            get
            {
                return this.DockingPanel;
            }
            set
            {
            }
        }
        #endregion
    }
}

