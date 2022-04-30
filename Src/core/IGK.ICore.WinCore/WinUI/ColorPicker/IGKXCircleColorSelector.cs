

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXCircleColorSelector.cs
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
file:IGKXCircleColorSelector.cs
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
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Drawing.Imaging;

namespace IGK.ICore.WinUI

{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent the default circle color selector
    /// </summary>
    public class IGKXCircleColorSelector : 
        IGKXControl        
    {
        const int COLOR_COUNT = 360;
        const int DX = 5;
        const int DY = 5;
        const int DEFAULT_WIDTH = 145;
        const int DEFAULT_HEIGHT = 145;
      
        private Bitmap m_bmp;
        private Vector2f m_curLocation;
        private float m_radius;
        private Vector2f m_center;
        private bool ms_select; //color selected by mouse
        //use for color selection
        private float m_distance;
        private float m_angle; //in radian
        private Colorf m_color; //represented color
        /// <summary>
        /// get or set the color
        /// </summary>
        public Colorf Color
        {
            get
            {
                return this.m_color;
            }
            set
            {
                if (!this.m_color.Equals (value))
                {
                    this.m_color = value;
                    OnColorChanged(EventArgs.Empty);
                }
            }
        }
        #region "events"
        public event EventHandler ColorChanged;
        #endregion
        private void OnColorChanged(EventArgs eventArgs)
        {
            if (!ms_select)
            {
                //calculate representative color
                Colorf v_cl = this.m_color;
                CoreColorHandle.RGB b = new CoreColorHandle.RGB();
                b.Red = (byte)(v_cl.R * 255);
                b.Green = (byte)(v_cl.G * 255);
                b.Blue = (byte)(v_cl.B * 255);
                CoreColorHandle.HSV v = CoreColorHandle.RGBtoHSV(b);
                this.m_angle = (float)((v.Hue * 2 * Math.PI) / 255.0f);
                //get dispante points
                this.m_distance = (v.Saturation / 255.0f) * m_radius;
                //get system disce
                m_distance = Math.Min(m_radius, m_distance);
                Vector2f  c = this.m_center;
                //c.X += DX;
                //c.Y += DY;
                this.m_curLocation = new Vector2f (
                   c.X + (float)(m_distance * Math.Cos(m_angle)),
                   c.Y + (float)(m_distance * Math.Sin(m_angle)));
       
            }
            this.Invalidate();
            this.Update();
            if (this.ColorChanged != null)
                this.ColorChanged(this, eventArgs);
        }
      
        //.ctr
        public IGKXCircleColorSelector()
        {
            this.Width = DEFAULT_WIDTH;
            this.Height = DEFAULT_HEIGHT;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.GenerateBitmap(false);
            this.SetCursorLocation(new Vector2f(m_center.X + DX, m_center.Y + DY));
            this.BackColor = global::System.Drawing.Color.Transparent;
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            GenerateBitmap(!Enabled);
            Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Rectangle rc = this.ClientRectangle;
            this.m_center = rc.GetCenter();
            this.GenerateBitmap(!this.Enabled);
            int w = Math.Min(rc.Width, rc.Height);
            rc.Width = w;
            rc.Height = w;
            //rc.Inflate(-1, -1);
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(rc);
            this.Region = new Region(p);
            p.Dispose();
            this.SetCursorLocation(m_center);
            this.Invalidate();
        }
        private void GenerateBitmap(bool bw)
        {
            if (this.DesignMode)
                return;
            if (this.m_bmp != null)
            {
                this.m_bmp.Dispose();
                this.m_bmp = null;
            }
            Rectangle r = this.ClientRectangle;
            r.Inflate(-4, -4);
            int w = Math.Min(r.Width, r.Height);
            if (w <= 0) return;
            this.m_bmp = new Bitmap(w, w);
            Graphics g = Graphics.FromImage(this.m_bmp);
            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            GraphicsPath p = new GraphicsPath();
            //seet the center and radius in the bitmap
            m_radius = w / 2.0f;
            m_center = new Vector2f(m_radius, m_radius);
            PointF[] tb = GetPoints(m_radius, m_center).CoreConvertTo<PointF[]>();
            if (tb != null)
            {
                GraphicsPath v_gp = new GraphicsPath();
                v_gp.AddPolygon(tb);
                v_gp.CloseFigure();
                using (PathGradientBrush br = new PathGradientBrush(v_gp))
                {
                    br.CenterColor = global::System.Drawing.Color.White;
                    br.SurroundColors = GetColors().CoreConvertTo<Color[]>();
                    g.FillRectangle(br, br.Rectangle);
                }
                v_gp.Dispose();
            }
            //set the center and radius in this projet
            m_radius = w / 2.0f;
            m_center = new Vector2f (r.X + m_radius, r.Y + m_radius);
            if (bw)
            {
                //make black and white / gray
                System.Drawing.Imaging.ColorMatrix cl = new System.Drawing.Imaging.ColorMatrix(
                    new float[][]{
                        new float[]{0.3f, 0.3f, 0.3f,0.0f,0.0f},
                        new float[]{0.3f, 0.3f, 0.3f,0.0f,0.0f},
                        new float[]{0.3f, 0.3f, 0.3f,0.0f,0.0f},
                        new float[]{0.0f, 0.0f, 0.0f,1.0f,0.0f},
                        new float[]{0.0f, 0.0f, 0.0f,0.0f,1.0f}                                                                       
                    }
                    );
                ImageAttributes att = new ImageAttributes();
                att.SetColorMatrix(cl);
                Rectangle rec = new Rectangle(0, 0, m_bmp.Width, m_bmp.Height);
                g.DrawImage(m_bmp, rec, rec.X, rec.Y, rec.Width, rec.Height,
                    GraphicsUnit.Pixel,
                    att);
                att.Dispose();
            }
            g.Dispose();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle cli = this.ClientRectangle;
            cli.Inflate(-2, -2);
            base.OnPaint(e);
            if (this.DesignMode){
                var center = new Point(cli.Width / 2, cli.Height / 2);
                var radius = Math.Min(cli.Width / 2.0, cli.Height / 2.0);
                e.Graphics.DrawEllipse(Pens.Black, cli);
                return;
            }

            if (!this.DesignMode && (this.m_bmp != null) && (this.m_bmp.PixelFormat != PixelFormat.Undefined))
            {
           
                Rectangle rc = new Rectangle(cli.Location, m_bmp.Size);
                //draw image
                Graphics g = e.Graphics;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(this.m_bmp, cli.Location);
                g.SmoothingMode = SmoothingMode.HighQuality;
                Pen p = CoreBrushRegisterManager.GetPen<Pen>(Colorf.White);
                p.Width = 2.0f;
                p.DashStyle = DashStyle.Solid;
                g.DrawEllipse(p, rc);
                if (this.Enabled)
                {
                    //draw cursor
                    DrawCursor(e);
                }
                //restrause width size
                p.Width = 1.0f;
            }
        }
        private void DrawCursor(PaintEventArgs e)
        {
            const int w = 3;
            Rectanglef r = new Rectanglef
            {
                Location = m_curLocation
            };
            r.Inflate(w, w);
            e.Graphics.FillEllipse(Brushes.Black , r.X, r.Y, r.Width , r.Height );
            using (Pen p = new Pen(System.Drawing.Color.White, 2))
            {
                e.Graphics.DrawEllipse(p, r.X, r.Y, r.Width, r.Height);
            }
        }
        /// <summary>
        /// get most pf cpmpr tp generate circle color
        /// </summary>
        /// <returns></returns>
        private Colorf[] GetColors()
        {
            Colorf[] t = new Colorf[COLOR_COUNT];
            float step = (float)((255 / (float)COLOR_COUNT));
            float v = 0;
            for (int i = 0; i < t.Length; i++)
            {
                v = i * step;
                //((double)(i * 255) * step/ (float) COLOR_COUNT)
                t[i] = CoreColorHandle.HSVtoColorf((int)v, 255, 255);
            }
            return t;
        }
        public Vector2f[] GetPoints(float radius, Vector2f centerPoint)
        {
            //get circle point
            Vector2f[] t = new Vector2f[COLOR_COUNT];
            float step = (float)((360 / (float)COLOR_COUNT) * (Math.PI / 180.0f));
            for (int i = 0; i < COLOR_COUNT; i++)
            {
                t[i] = new Vector2f
                    (
                    (float)(centerPoint.X + (radius * Math.Cos(step * i))),
                    (float)(centerPoint.Y + (radius * Math.Sin(step * i)))
                    );
            }
            return t;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.Cursor = Cursors.Hand;
                    break;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            SetColorFromMouse(e);
            this.Cursor = Cursors.Default;
        }
        private void SetColorFromMouse(MouseEventArgs e)
        {
            this.ms_select = true;
            SetCursorLocation(new Vector2f(e.X, e.Y));
            SetColor();
            this.ms_select = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
                SetColorFromMouse(e);
        }
        private void SetColor()
        {
            //int h = (int)((angle * CoreMathOperation.ConvRdToDEGREE)  * 255 / 360.0f);
            int h = (int)Math.Ceiling((m_angle * CoreMathOperation.ConvRdToDEGREE) * 255 / 360.0f);
            int s = (int)Math.Ceiling(((m_distance / m_radius) * 255.0f));
            int v = 255;
            if (h < 0)
                h = 255 + h;
            //h = 360;
            this.Color = CoreColorHandle.HSVtoColorf(h,
                s,
                v);
        }
        private void SetCursorLocation(Vector2f  point)
        {
            Rectangle rc = this.ClientRectangle;
            rc.Inflate(-2, -2);
            Vector2f c = m_center;
            float dx = point.X - c.X;
            float dy = point.Y - c.Y;
            m_distance = (float)Math.Sqrt((dx * dx) + (dy * dy));
            m_angle = CoreMathOperation.GetAngle(c, point);
            m_distance = Math.Min(m_radius, m_distance);
            this.m_curLocation = new Vector2f (
                c.X + (float)(m_distance * Math.Cos(m_angle)),
                c.Y + (float)(m_distance * Math.Sin(m_angle)));
            this.Invalidate();
        }
        //private float GetAngle(Vector2f  point1, Vector2f  point2)
        //{
        //    float angle = 0.0f;
        //    angle = CoreMathOperation.GetAngle(point1, point2);
        //    return angle;
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_bmp != null)
                {
                    this.m_bmp.Dispose();
                    this.m_bmp = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}

