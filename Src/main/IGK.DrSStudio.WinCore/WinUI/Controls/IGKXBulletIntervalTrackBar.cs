

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXBulletIntervalTrackBar.cs
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
file:IGKXBulletIntervalTrackBar.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;

    /// <summary>
    /// represent hte bullet intervaleur used for trackback
    /// </summary>
    public class IGKXBulletIntervalTrackBar : IGKXControl
    {
        private WinCoreBulletInterval  m_colorInterval;
        private int m_MinValue;
        private int m_MaxValue;
        public int MaxValue
        {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }
        public int MinValue
        {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }
        /// <summary>
        /// get or set the interval
        /// </summary>
        public WinCoreBulletInterval Interval
        {
            get
            {
                return this.m_colorInterval;
            }
            set
            {
                this.m_colorInterval = value;
                OnIntervalChanged(EventArgs.Empty);
            }
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(150, 24);
            }
        }
        public IGKXBulletIntervalTrackBar()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            this.m_colorInterval = new WinCoreBulletInterval(0, 0);
            this.m_MinValue = 0;
            this.m_MaxValue = 255;
            this.MouseDown += XBulletIntervalTrackBar_MouseDown;
            this.MouseUp += XBulletIntervalTrackBar_MouseUp;
            this.MouseMove += XBulletIntervalTrackBar_MouseMove;
            this.KeyDown += XBulletIntervalTrackBar_KeyDown;
            this.KeyPress += XBulletIntervalTrackBar_KeyPress;
        }
        void XBulletIntervalTrackBar_KeyPress(object sender, CoreKeyEventArgs e)
        {
        }
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        public event PaintEventHandler PaintCursor;

        void XBulletIntervalTrackBar_KeyDown(object sender, CoreKeyEventArgs e)
        {
            int c = 0;
            switch (e.KeyCode)
            {
                case enuKeys.Left:
                case enuKeys.Down:
                    c = this.m_colorInterval.Min  - 1;
                    if (c >= MinValue)
                        this.m_colorInterval = new WinCoreBulletInterval(
                           (byte)c, (byte)c);
                    OnIntervalChanged(EventArgs.Empty);
                    break;
                case enuKeys.Right:
                case enuKeys.Up:
                    c = this.m_colorInterval.Max + 1;
                    if (c <= MaxValue)
                        this.m_colorInterval = new WinCoreBulletInterval(
                           (byte)c, (byte)c);
                    OnIntervalChanged(EventArgs.Empty);
                    break;
            }
        }
        void XBulletIntervalTrackBar_MouseMove(object sender, CoreMouseEventArgs e)
        {
            this.updateMouse(e);
        }
        private void updateMouse(CoreMouseEventArgs e)
        {
            switch (e.Button)
            {
                case enuMouseButtons.Left:
                    //set the 
                    this.m_colorInterval = new WinCoreBulletInterval(
                        GetValue(e.X), Math.Max(GetValue(e.X), this.m_colorInterval.Max));
                    OnIntervalChanged(EventArgs.Empty);
                    break;
                case enuMouseButtons.Right:
                    this.m_colorInterval = new WinCoreBulletInterval(
                    this.m_colorInterval.Min, Math.Max(this.m_colorInterval.Min, GetValue(e.X)));
                    OnIntervalChanged(EventArgs.Empty);
                    break;
            }
        }
        void XBulletIntervalTrackBar_MouseUp(object sender, CoreMouseEventArgs e)
        {
            this.updateMouse(e);
        }
        void XBulletIntervalTrackBar_MouseDown(object sender, CoreMouseEventArgs e)
        {
            if (!this.Focused)
                this.Focus();
            this.updateMouse(e);
        }
        protected override void OnGotFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnLostFocus(e);
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XBulletIntervalTrackBar
            // 
            this.ResumeLayout(false);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle v_rc = this.ClientRectangle;
            int y = 0;
            y = (int)Math.Floor(v_rc.Height / 2.0f);
            Rectangle v_rcback = new Rectangle(0, y, v_rc.Width, 0);
            v_rcback.Inflate(0, 2);
            e.Graphics.FillRectangle(Brushes.DarkGray,
                v_rcback);
            ControlPaint.DrawBorder(
                e.Graphics,
                v_rcback,
                Color.Black, ButtonBorderStyle.Solid);
            //draw bar 
            v_rcback = new Rectangle(
                GetPoint(this.Interval.Min), 0,
               Math.Max(1, GetPoint(this.Interval.Max) - GetPoint(this.Interval.Min)), this.Height);
            Colorf v_sColor = Colorf.Cyan;
            Colorf v_eColor = Colorf.Lime;
            if (this.Focused)
            {
                v_sColor = Colorf.Red;
                v_eColor = Colorf.Yellow;
            }
            if (this.PaintCursor != null)
            {
                this.PaintCursor?.Invoke(this, new PaintEventArgs (e.Graphics, v_rcback));
            }
            else
            {
                using (LinearGradientBrush br = WinCoreBrushRegister.CreateBrush<LinearGradientBrush>(
                    v_rcback,
                    v_sColor,
                    v_eColor,
                    90.0f))
                {
                    e.Graphics.FillRectangle(br,
                        v_rcback);
                }

            }
            ControlPaint.DrawBorder(
                e.Graphics,
                v_rcback,
                Color.Black, ButtonBorderStyle.Solid);
            base.OnPaint(e);
        }
        public event EventHandler IntervalChanged;
        protected virtual void OnIntervalChanged(System.EventArgs e)
        {
            this.Invalidate();
            if (IntervalChanged != null)
                IntervalChanged(this, e);
        }
        /// <summary>
        /// determine the point of the 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private int GetPoint(int b)
        {
            int w = this.Width - 2;
            int d = this.MaxValue - this.MinValue;
            int h = (int)Math.Floor((b * w) / (float)d);
            return h + 1;
        }
        private int GetValue(int p)
        {
            int w = this.Width - 3;
            int d = this.MaxValue - this.MinValue;
            int o = 0;
            o = (int)Math.Floor((d * p) / (float)w);
            if (o > this.MaxValue)
                return this.MaxValue;
            if (o < this.MinValue)
                return this.MinValue;
            return  o;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
    }
}

