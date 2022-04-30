

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XHueTrackbackInterval.cs
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
file:XHueTrackbackInterval.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2013
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
    /// represent hte bullet interval hue color track back
    /// </summary>
    public class XHueTrackbackInterval : IGKXControl
    {
        private WinCoreBulletInterval  m_colorInterval;
        private int m_MinValue;
        private int m_MaxValue;
        private Bitmap m_hueBitmap;
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
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(150, 24);
            }
        }
        public XHueTrackbackInterval()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            this.m_colorInterval = new WinCoreBulletInterval(0, 0);
            this.m_MinValue = 0;
            this.m_MaxValue = 360;
            this.MouseDown +=XBulletIntervalTrackBar_MouseDown;
            this.MouseUp += XBulletIntervalTrackBar_MouseUp;
            this.MouseMove += XBulletIntervalTrackBar_MouseMove;
            this.KeyDown += XBulletIntervalTrackBar_KeyDown;
            this.SizeChanged += XHueTrackbackInterval_SizeChanged;
            this.generateHueBitmap();
        }
        private void generateHueBitmap()
        {
            if (this.m_hueBitmap != null)
                this.m_hueBitmap.Dispose();
            this.m_hueBitmap = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage (this.m_hueBitmap ))
            {
                int h = 0;
                LinearGradientBrush lb = null;
            for (int i = 0; i < this.Width; i++)
			{
                    h =(int)(i* 255/(float)this.Width );
                    lb = WinCoreBrushRegister.CreateBrush(
                        new Rectanglef(Vector2f.Zero , new Size2f( 1 , this.Height)),
                        CoreColorHandle.HSVtoColorf(h, 255, 255).CoreConvertFrom<Colorf>(),
                        Colorf.FromFloat (0.5f),
                        0.0f
                        );
                    g.DrawLine(new Pen(lb,1), i, 0, i, this.Height);
			}
            g.Flush();
            }
        }
        void XHueTrackbackInterval_SizeChanged(object sender, EventArgs e)
        {
            this.generateHueBitmap();
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
        void XBulletIntervalTrackBar_KeyDown(object sender, CoreKeyEventArgs e)
        {
            int c = 0;
            switch (e.KeyCode)
            {
                case enuKeys.Left:
                case enuKeys.Down:
                    c = this.m_colorInterval.Min  - 1;
                    if (c >= MinValue)
                        this.m_colorInterval = new WinCoreBulletInterval (
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
            int x = 0;
            switch (e.Button)
            {
                case enuMouseButtons.Left:
                    //set the 
                    x = GetValue(e.X);
                    this.m_colorInterval = new WinCoreBulletInterval(
                        x, Math.Max(x, this.m_colorInterval.Max));
                    OnIntervalChanged(EventArgs.Empty);
                    break;
                case enuMouseButtons.Right:
                    x = GetValue(e.X);
                    this.m_colorInterval = new WinCoreBulletInterval(
                    this.m_colorInterval.Min, Math.Max(this.m_colorInterval.Min, x));
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
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
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
            Color v_sColor = Color.Cyan;
            Color v_eColor = Color.Lime;
            if (this.Focused)
            {
                v_sColor = Color.Red;
                v_eColor = Color.Yellow;
            }
            if ((this.m_hueBitmap !=null) && (this.m_hueBitmap .PixelFormat !=  System.Drawing.Imaging.PixelFormat.Undefined ))
                e.Graphics.DrawImage(this.m_hueBitmap, Point.Empty);
            //using (LinearGradientBrush br = WinCoreBrushRegister.CreateBrush(v_rcback, v_sColor, v_eColor, 90.0f))
            //{
            //    e.Graphics.FillRectangle(br,
            //        v_rcback);
            //}
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
            return o;
        }
    }
}

