

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDockingFormCaptionBar.cs
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
file:XDockingFormCaptionBar.cs
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
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore.Resources;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinCore.WinUI;

    public class XDockingFormCaptionBar : IGKXControl, IDockingCaptionBar 
    {
        private IDockingPanel m_DockingPanel;
        private IDockingForm  m_Form;
        private IGKXButton c_close;
        public IDockingForm  Form
        {
            get { return m_Form; }
            set
            {
                if (m_Form != value)
                {
                    m_Form = value;
                }
            }
        }
        #region IDockingPanelItem Members
        public IDockingPanel DockingPanel
        {
            get
            {
                return this.m_DockingPanel;
            }
            set
            {
                this.m_DockingPanel = value;
            }
        }
        #endregion
        public XDockingFormCaptionBar(XDockingForm form, IDockingPanel panel)
        {
            this.m_Form = form;
            this.m_DockingPanel = panel;
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            c_close = new IGKXButton();
            c_close.Click += new EventHandler(c_close_Click);
            this.MinimumSize = new Size(0,XDockingConstant.CAPTION_BAR_SIZE);
            this.Controls.Add(c_close);
            this.MouseDown += _MouseDown;
            c_close.ButtonDocument = CoreButtonDocument.CreateFromRes(
                CoreImageKeys.WINUI_LD_BTN_CLOSE_GKDS);
            this.Paint += _Paint;
        }

        private void _Paint(object sender, CorePaintEventArgs e)
        {            
            RenderCaptionBar(e);
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
                if (this.DockingPanel.SelectedPage.Document != null)
                {
                    this.DockingPanel.SelectedPage.Document.Draw(
                        e.Graphics,
                        new Rectanglei(
                            2, 2,
                            16, 16));
                }
                Rectangle v_rc = this.ClientRectangle;
                //v_rc.Inflate(-2, -2);
                v_rc.X += 18;
                v_rc.Width = (this.c_close.Right - this.c_close.Width) - v_rc.X;
                v_rc.Height--;
                if (v_rc.Width > 0)
                {
                    StringFormat v_sf = new StringFormat();
                    v_sf.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
                    v_sf.Trimming = StringTrimming.EllipsisCharacter;
                    v_sf.Alignment = StringAlignment.Center;
                    v_sf.LineAlignment = StringAlignment.Center;
                    Rectanglef v_rcf = new Rectanglef(v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);
                    e.Graphics.DrawString(text,
                        this.Font,
                        WinCoreBrushRegister.GetBrush(WinCoreControlRenderer.DialogCaptionForeColor),
                        v_rcf,
                        v_sf);
                    v_sf.Dispose();

    //                e.Graphics.DrawRectangle(Colorf.White,
    //v_rcf.X,
    //v_rcf.Y,
    //v_rcf.Width,
    //v_rcf.Height);

                }
            }
        }
        void c_close_Click(object sender, EventArgs e)
        {
            this.m_Form.Close();
        }
        void _MouseDown(object sender, CoreMouseEventArgs e)
        {
            switch (e.Button)
            {
                case enuMouseButtons.Left :
                    //move the form
                    this.Capture = false;
                    //User32.SendMessage(this.m_Form.Handle, User32.WM_NCLBUTTONDOWN,
                    //    (IntPtr)User32.HTCAPTION, IntPtr.Zero);
                    if (this.m_DockingPanel is XDockingPanel)
                    {
                        DragContentHandler drag = new DragContentHandler(this.m_Form,
                            new  Vector2i (
                                Control.MousePosition.X ,
                                Control.MousePosition.Y));
                        Application.AddMessageFilter(drag);
                    }
                    break;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            InitButtonBound();
        }
        private void InitButtonBound()
        {
            Rectangle v_rc = new Rectangle(
                this.Width - XDockingConstant.CAPTION_BAR_SIZE - 2,
                1,
                18,
                18);
            v_rc.Inflate(-1, -1);
            c_close.Bounds = v_rc;
            v_rc.X -= v_rc.Width  + 2;
        }
    }
}

