

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXAngleSelector.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{
    public class IGKXAngleSelector : IGKXControl 
    {
          float m_angle;
        Bitmap bmp;
        RectangleF drawRegion;
        //events
        public event EventHandler AngleChanged;



        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                DisposeBitmap();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
        //Property
        /// <summary>
        /// Get angle in degre
        /// </summary>
        public float Angle
        {
            get
            {
                return m_angle;
            }
            set
            {
                if (m_angle > 360)
                {
#pragma warning disable IDE0054 // Use compound assignment
                    value = value % 360;
#pragma warning restore IDE0054 // Use compound assignment
                }
                m_angle = value;
                Refresh();
                OnAngleChanged(EventArgs.Empty);
            }
        }

        private void OnAngleChanged(EventArgs eventArgs)
        {
            if (this.AngleChanged != null)
                this.AngleChanged(this, eventArgs);
        }

        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(50, 50);
            }
        }
        protected override System.Drawing.Size DefaultMinimumSize
        {
            get
            {
                return new System.Drawing.Size(50, 50);
            }
        }

        public IGKXAngleSelector()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            InitializeComponent();
            GenCircle();
        }


        public IGKXAngleSelector(IContainer container) : this()
        {
            container.Add(this);                        
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            GenCircle();
            base.OnSizeChanged(e);
        }

        private void DisposeBitmap()
        {
            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }
        }

        private void GenCircle()
        {
            // bmp = new Bitmap(this.Width*4, this.Height*4);

            // Graphics g = Graphics.FromImage(bmp);
            //// g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            // g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic ;
            // g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            // Pen p = new Pen (Color.Black , 8.0F);
            // p.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset ;
            // g.DrawEllipse(p, 0, 0, bmp.Width, bmp.Height);
            // g.DrawEllipse(p, (bmp.Width / 2.0f) - 4, (bmp.Height / 2.0f) - 4, 8, 8);
            // p.Dispose();
            // g.Flush();
            // g.Dispose();

            this.drawRegion = this.ClientRectangle;
            this.drawRegion.Inflate(-2, -2);
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(this.drawRegion);
            //this.Region = new Region(p);
            p.Dispose();
            this.drawRegion.Inflate(-2, -2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            Pen v_outline = CoreBrushRegisterManager.GetPen<Pen>(Colorf.WhiteSmoke);
            v_outline.Width = 2.0f;

            Brush fill = CoreBrushRegisterManager.GetBrush<Brush>(Colorf.Maroon);

            float r = this.Width / 2.0f;
            v_outline.Alignment = PenAlignment.Inset;


            g.FillEllipse(fill, drawRegion);
            g.DrawEllipse(Pens.SkyBlue, drawRegion);

            PointF c = new PointF(r, r);
            PointF p = new PointF((float)(c.Y + r * Math.Cos(Angle * Math.PI / 180.0F)),
                (float)(c.X + r * Math.Sin(Angle * Math.PI / 180.0F)));

            RectangleF vr = new RectangleF(c, Size.Empty);
            vr.Inflate(2, 2);

            //e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            //resource soum
            v_outline.Width = 1.0f;
            e.Graphics.DrawRectangle(v_outline, vr.X, vr.Y, vr.Width, vr.Height);
            e.Graphics.DrawLine(v_outline, c, p);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                SetAngle(e.Location);
            }
        }

        private void SetAngle(Point point)
        {
            float r = this.Width / 2.0f;
            PointF c = new PointF(r, r);
            this.Angle = GetAngle(c, point);

        }

        public static float GetAngle(PointF startPoint, PointF endPoint)
        {
            float dx, dy;
            dx = endPoint.X - startPoint.X;
            dy = endPoint.Y - startPoint.Y;
            float angle = 0;
            if (dx == 0.0f)
            {
                if (dy > 0)
                {
                    angle = (float)(Math.PI / 2.0f);
                }
                else
                    angle = (float)(-Math.PI / 2.0f);
            }
            else
                angle = (float)Math.Atan(dy / dx);
            if (dx < 0)
                angle += (float)Math.PI;
            return (float)(angle * 180.0f / Math.PI);

        }
        
    }
}
