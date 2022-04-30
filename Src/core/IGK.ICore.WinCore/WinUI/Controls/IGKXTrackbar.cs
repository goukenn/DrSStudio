

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXTrackbar.cs
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
file:IGKXTrackbar.cs
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
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace IGK.ICore.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinCore.WinUI;
    /// <summary>
    /// represent the trackback
    /// </summary>
    public class IGKXTrackbar : IGKXControl 
    {
        Timer m_accelerator;
        int step = 1;
        static readonly ICore2DDrawingDocument btn_trackDocument =
            CoreResources.GetDocument(CoreImageKeys.C_TRACKCURSOR_GKDS);
        protected override void Dispose(bool disposing)
        {
            if (disposing )
            {
                if (this.m_accelerator != null)
                {
                    this.m_accelerator.Dispose();
                    this.m_accelerator = null;
                }
            }
            base.Dispose(disposing);
        }
        public IGKXTrackbar()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint , true);
            this.SetStyle(ControlStyles.ResizeRedraw, true); 
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            this.MinimumSize = new Size(100, 16);
            this.Cursor = Cursors.Hand;
            this.MouseDown += _MouseUpdateCur;
            this.MouseMove += _MouseUpdateCur;
            this.MouseUp += _MouseUpdateCur;
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Left:
                case System.Windows.Forms.Keys.Right:
                case System.Windows.Forms.Keys.Up:
                case System.Windows.Forms.Keys.Down:
                    return true;
                default:
                    break;
            }
            return base.IsInputKey(keyData);
        }
        void _MouseUpdateCur(object sender, CoreMouseEventArgs e)
        {
            if (e.Button == enuMouseButtons.Left)
            { 
                Rectangle rc = GetArea ();
                if (rc.Width == 0) 
                    return ;
                float x = (e.X - rc.X )/(float)rc.Width ;
                if (x >= 1.0F)
                    x = 1.0f;
                if (x < 0.0f)
                    x = 0.0f;
                this.Value = Convert.ToInt32 ( (Max - Min) * x + Min);
                Invalidate();
            }
        }
        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(100, 16); 
            }
        }
        private int m_Min;
        private int m_Max;
        private int m_Value;
        public int Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                    this.Refresh();
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler  ValueChanged;
        private void OnValueChanged(EventArgs eventArgs)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, eventArgs);
            }
        }
        public int Max
        {
            get { return m_Max; }
            set
            {
                if (m_Max != value)
                {
                    m_Max = value;
                }
            }
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!this.Focused)
                this.Focus();
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (this.m_accelerator != null)
            {
                this.m_accelerator.Dispose();
                this.m_accelerator = null;
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!this.Focused)
            {
                return;
            }
            base.OnKeyDown(e);
            switch (e.KeyData)
            {
                case Keys.Left:
                case Keys.Down:
                    InitAccelerator();
                    if (Value > (Min + step))
                    {
                        this.Value -= step;
                    }
                    else
                    {
                        this.Value = Min;
                    }
                    break;
                case Keys.Right:
                case Keys.Up:
                    InitAccelerator();
                    if (Value < (Max - step))
                    {
                        this.Value += step;
                    }
                    else
                        this.Value = Max;
                    break;
            }
        }
        private void InitAccelerator()
        {
            if (this.m_accelerator == null)
            {
                this.m_accelerator = new Timer();
                this.step = 1;
                this.m_accelerator.Interval = 3000;
                this.m_accelerator.Tick += new EventHandler(m_accelerator_Tick);
                this.m_accelerator.Enabled = true;
            }
        }
        void m_accelerator_Tick(object sender, EventArgs e)
        {
            switch (step)
            { 
                case 1:
                    this.step = 3;
                    break;
                case 3 :
                    this.step = 10;
                    break;
                case 10:
                    this.step = 25;
                    break;
            }
        }
        public int Min
        {
            get { return m_Min; }
            set
            {
                if (m_Min != value)
                {
                    m_Min = value;
                }
            }
        }
        public Rectangle GetArea()
        {
            Rectangle vr = this.ClientRectangle;
            vr.X++;
            vr.Y++;
            vr.Width -= 2;
            vr.Height -= 2;
            return vr;
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle vr = GetArea();
            if (this.Enabled)
            {
                using (LinearGradientBrush brush = WinCoreBrushRegister.CreateBrush(vr, 
                    WinCoreControlRenderer.TrackbarBackgroundStartColor,
                    WinCoreControlRenderer.TrackbarBackgroundEndColor, 90.0f))
                {
                    e.Graphics.FillRectangle(brush, vr);
                }
                //get proportionnality
                int w = 0;
                if (Max > Min)
                {
                    w = (this.Value - Min) * vr.Width / (Max - Min);
                    using (LinearGradientBrush brush = WinCoreBrushRegister.CreateBrush(vr,
                        WinCoreControlRenderer.TrackbarForeStartColor,
                        WinCoreControlRenderer.TrackbarForeEndColor,
                        -90.0f))
                    {
                        e.Graphics.FillRectangle(brush, new Rectangle(vr.X,
                            vr.Y + 4,
                            w,
                            vr.Height - 4));
                    }
                }
                if (btn_trackDocument != null)
                {
                    btn_trackDocument.Draw(
                        e.Graphics,
                        new Rectanglei(w-8, vr .Y, 16,8));
                }
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.InactiveBorder, vr);
                e.Graphics.DrawRectangle(Pens.Black, vr);
            }
        }
    }
}

