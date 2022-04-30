

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreControlPaint.cs
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
file:WinCoreControlPaint.cs
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
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio
{
    using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.WinUI;

    /// <summary>
    /// represent wincore painting 
    /// </summary>
    public static class WinCoreControlPaint
    {
        public static void DrawSelectionRectangle(ICoreGraphics device,
            float x , float y, float width , float height)
        {
            object obj = device.Save();
            lock (device)
            {
                device.DrawRectangle(Colorf.White,
               x, y, width - 1, height - 1);
                device.DrawRectangle(Colorf.Black, 1.0f, enuDashStyle.Dot,
                    x, y, width - 1, height - 1);
            }
            device.Restore(obj);

        }

        public static void DrawBorder(Graphics g, Rectangle rc, Color cl, ButtonBorderStyle style)
        {
            ControlPaint.DrawBorder(g, rc, cl, style);
        }
        public static void Draw3DBorder(Graphics g, Rectangle rc, Color cl,
            Border3DStyle style,
            Border3DSide side)
        {
            ControlPaint.DrawBorder3D(g, rc, style, side);
        }
        public static GraphicsPath BuildRoundRectangle(Rectanglef rc,
            float rtopx,
            float rtopy,
            float rbottomx,
            float rbottomy)
        {
            RectangleF v_rect = new RectangleF (rc.X, rc.Y , rc.Width , rc.Height );
            //top left
            float vtl_dx = Math.Max(rtopx, 0.001f);
            //top right
            float vtr_dx = Math.Max(rtopy, 0.001f);
            //bottom right
            float vbr_dy = Math.Max(rbottomx, 0.001f);
            //bottom left
            float vbl_dy = Math.Max(rbottomy, 0.001f);
            GraphicsPath path = new GraphicsPath();
            SizeF v_size = new SizeF(2 * vtl_dx, 2 * vtl_dx);
            path.AddArc(new RectangleF(v_rect.Location, v_size), 180.0f, 90.0f);
            v_size = new SizeF(2 * vtr_dx, 2 * vtr_dx);
            path.AddArc(new RectangleF(new PointF(v_rect.X + v_rect.Width - 2 * vtr_dx, v_rect.Y),
                v_size), -90.0f, 90.0f);
            v_size = new SizeF(2 * vbr_dy, 2 * vbr_dy);
            path.AddArc(new RectangleF(new PointF(v_rect.X + v_rect.Width - v_size.Width,
                v_rect.Y + v_rect.Height - v_size.Height),
                v_size), 0.0f, 90.0f);
            v_size = new SizeF(2 * vbl_dy, 2 * vbl_dy);
            path.AddArc(new RectangleF(new PointF(v_rect.X,
                v_rect.Y + v_rect.Height - v_size.Height),
                v_size), 90.0f, 90.0f);
            path.CloseFigure();
            return path;
        }
        public static GraphicsPath BuildRoundRectangle(Rectanglef  rc,
            float radius)
        {
            RectangleF v_rect = new RectangleF(rc.X, rc.Y, rc.Width, rc.Height);
            float vtr_dx = radius;
            float vbr_dx = radius;
            float vbr_dy = radius;
            float vbl_dy = radius;
            GraphicsPath path = new GraphicsPath();
            SizeF v_size = new SizeF(2 * radius, 2 * radius);
            path.AddArc(new RectangleF(v_rect.Location, v_size), 180.0f, 90.0f);
            path.AddArc(new RectangleF(new  PointF (v_rect.X+ v_rect.Width - 2 * vtr_dx, v_rect.Y),
                v_size), -90.0f, 90.0f);
            path.AddArc(new RectangleF(new PointF(v_rect.X + v_rect.Width - 2 * vbr_dx, v_rect.Y + v_rect.Height - 2 * vbr_dy),
                v_size), 0.0f, 90.0f);
            path.AddArc(new RectangleF(new PointF(v_rect.X, v_rect.Y + v_rect.Height - 2 * vbl_dy),
                v_size), 90.0f, 90.0f);
            path.CloseFigure();
            return path;
        }
        public static void DrawRoundRect(Graphics g, Colorf cl, float width, DashStyle style, Rectanglef rc, int radius)
        {
#pragma warning disable IDE0054 // Use compound assignment
            rc.Width = rc.Width - 1;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
            rc.Height = rc.Height - 1;
#pragma warning restore IDE0054 // Use compound assignment
            GraphicsPath vp = BuildRoundRectangle(rc, radius);
            Pen p = CoreBrushRegisterManager.GetPen<Pen>(cl);
            p.Width = width;
            p.DashStyle = style;
            p.Alignment = PenAlignment.Center;
            p.LineJoin = LineJoin.Round;
            g.DrawPath(p, vp);
            vp.Dispose();
            //restore default pen property    
            p.Width = 1.0f;
            p.DashStyle = DashStyle.Solid;
            p.Alignment = PenAlignment.Center;
        }
        public static void FillRoundRect(Graphics g, Colorf cl, Rectanglef rc, int radius)
        {
            GraphicsPath vp = BuildRoundRectangle(rc, radius);
            Brush p = CoreBrushRegisterManager.GetBrush<Brush>(cl);
            g.FillPath(p, vp);
            vp.Dispose();
        }
        public static void FillRoundRect(Graphics g, Brush brush, Rectanglef rc, int radius)
        {
            GraphicsPath vp = BuildRoundRectangle(rc, radius);            
            g.FillPath(brush, vp);
            vp.Dispose();
        }
        public static void DrawComboboxItem(ComboBox combobox, Type type, DrawItemEventArgs e)
        {
            if ((e.Index == -1) || (type == null) || (combobox == null))
                return;
            string v_text = "";
            if (type.IsEnum)
            {
                v_text = CoreSystem.GetString("Enum." + Enum.GetName(type, combobox.Items[e.Index]));
            }
            e.DrawBackground();
            Colorf v_cl = e.ForeColor.CoreConvertFrom<Colorf>();
            Rectangle rc = e.Bounds;
            rc.Inflate(-1, -1);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Near;
            Brush br = CoreBrushRegisterManager.GetBrush<Brush>(v_cl);
            e.Graphics.DrawString(v_text, combobox.Font, br, rc, sf);
            sf.Dispose();
            if (e.Index == combobox.SelectedIndex)
                e.DrawFocusRectangle();
        }
        /// <summary>
        /// get the dark color 
        /// </summary>
        /// <param name="cl">color to get</param>
        /// <param name="percent">percent 0 to 100%</param>
        /// <returns>new Color</returns>
        internal static Color GetDarkColor(Color cl, float percent)
        {
            Color v_cl = Color.Empty;
            float v_d = (1 - (percent / 100.0f));
            int v_r = (int)(cl.R * v_d);
            int v_g = (int)(cl.G * v_d);
            int v_b = (int)(cl.B * v_d);
            if (v_r < 0) v_r = 0;
            if (v_g < 0) v_g = 0;
            if (v_b < 0) v_b = 0;
            v_cl = Color.FromArgb(cl.A, v_r, v_g, v_b);
            return v_cl;
        }
        /// <summary>
        /// get the light color 
        /// </summary>
        /// <param name="cl">color to get</param>
        /// <param name="percent">percent 0 to 100%</param>
        /// <returns>new Color</returns>
        internal static Color GetLightColor(Color cl, float percent)
        {
            Color v_cl = Color.Empty;
            float v_d = (1 + (percent / 100.0f));
            int v_r = (int)(cl.R * v_d);
            int v_g = (int)(cl.G * v_d);
            int v_b = (int)(cl.B * v_d);
            if (v_r > 255) v_r = 0;
            if (v_g > 255) v_g = 0;
            if (v_b > 255) v_b = 0;
            v_cl = Color.FromArgb(cl.A, v_r, v_g, v_b);
            return v_cl;
        }
        public static void DrawSplitterBackground(PaintEventArgs e, Control control)
        {          
            e.Graphics.FillRectangle(
                CoreBrushRegisterManager .GetBrush<Brush>(CoreRenderer.SplitterBackgroundColor )
                , control.ClientRectangle);
            Vector2f  v_pt = CoreMathOperation.GetCenter(new Rectanglef (
                control.ClientRectangle.X ,
                control.ClientRectangle.Y ,
                control.ClientRectangle.Width  ,
                control.ClientRectangle.Height 
                ));
            bool v_isVertical = control.Width < control.Height;
            Rectanglef rc = new Rectanglef(v_pt, Size2f.Empty);
            rc.Inflate(1,1);
            Vector2f[] tab = new Vector2f[3];
            tab[0] = new Vector2f(rc.X, rc.Y);
            if (v_isVertical)
            {
                tab[1] = new Vector2f(rc.X, rc.Y - 5 -rc.Height);
                tab[2] = new Vector2f(rc.X, rc.Y + 5 + rc.Height);
            }
            else {
                tab[1] = new Vector2f(rc.X - 5, rc.Y );
                tab[2] = new Vector2f(rc.X + 5, rc.Y );
            }
            for (int i = 0; i < tab.Length; i++)
            {
                e.Graphics.FillRectangle(Brushes.White, new Rectanglef(tab[i].X + 1, tab[i].Y + 1, rc.Width, rc.Height));
                e.Graphics.FillRectangle(Brushes.Black, new Rectanglef(tab[i].X , tab[i].Y , rc.Width, rc.Height));
            }
        }
        /// <summary>
        /// draw a selection rectangle
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="rc"></param>
        public static void DrawSelectionRectangle(Graphics graphics, Rectanglei rc)
        {
            Rectangle v_gdiRec = new Rectangle(rc.X, rc.Y, rc.Width, rc.Height);
            System.Windows.Forms.ControlPaint.DrawBorder(
    graphics,
    v_gdiRec,
    Color.White,
     ButtonBorderStyle.Solid);
            System.Windows.Forms.ControlPaint.DrawBorder(
                graphics,
                v_gdiRec,
                System.Drawing.Color.Black,
                 System.Windows.Forms.ButtonBorderStyle.Dotted);
        }
        /// <summary>
        /// build a round onglet
        /// </summary>
        /// <param name="v_r">rectangle</param>
        /// <param name="p">round size</param>
        /// <returns>graphics path</returns>
        public static GraphicsPath BuildOnglet(Rectangle rc, int SIZE)
        {
            GraphicsPath v_p = new GraphicsPath();
            Rectangle id = new Rectangle(rc.Left, rc.Bottom - SIZE, SIZE, SIZE);
            Rectangle yd = new Rectangle(rc.Left + SIZE, rc.Top, SIZE, SIZE);
            Rectangle yh = new Rectangle(rc.Right - SIZE, rc.Bottom - SIZE, SIZE, SIZE);
            Rectangle ih = new Rectangle(rc.Right - SIZE - SIZE, rc.Top, SIZE, SIZE);
            v_p.AddLine(rc.Left, rc.Bottom, rc.Left + SIZE / 2, rc.Bottom);
            v_p.AddArc(id, 90, -90);
            v_p.AddArc(yd, 180, 90);
            v_p.AddArc(ih, -90, 90);
            v_p.AddArc(yh, 180, -90);
            v_p.AddLine(rc.Right - SIZE / 2, rc.Bottom, rc.Right, rc.Bottom);
            return v_p;
        }
        public static void RegisterImgList(ImageList imageList, string name, Bitmap bitmap)
        {
            if ((bitmap != null) && !string.IsNullOrEmpty (name) && (!imageList.Images.ContainsKey (name )))
            {
                imageList.Images.Add(name,
                        bitmap);
            }
        }
        public static GraphicsPath BuildPolygon(Vector2f vector2f, float radius, int count, float offsetAngle)
        {
            if (count <= 0)return null;
            GraphicsPath vp = new GraphicsPath();
            List<PointF> vt = new List<PointF>();
            float vrda  = offsetAngle * CoreMathOperation .ConvDgToRadian;
            float vstep = (360 / (float ) count ) * CoreMathOperation.ConvDgToRadian ; 
            for (int i = 0; i < count; i++)
            {
                vt.Add(new PointF(
                    (float) (vector2f.X + radius * Math.Cos(vrda + (vstep * i))),
                    (float )(vector2f.Y + radius * Math.Sin (vrda + (vstep *i)))
                    ));
            }
            if (vt.Count > 2)
            {
                vp.AddPolygon(vt.ToArray());
                vp.CloseFigure();
            }
            return vp;
        }
        public static void DrawPolygon(Graphics graphics, Pen pen, Vector2f vector2f, float radius, int count, float offsetAngle)
        {
            using (GraphicsPath vp = BuildPolygon(vector2f, radius, count, offsetAngle))
            {
                graphics.DrawPath(pen, vp);
            }
        }
        public static void FillPolygon(Graphics graphics, Brush  brush, Vector2f vector2f, float radius,  int count, float offsetAngle)
        {
            using (GraphicsPath vp = BuildPolygon(vector2f, radius, count, offsetAngle))
            {
                graphics.FillPath(brush, vp);
            }
        }
        /// <summary>
        /// render a dashed background on rectangle
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="rectangle"></param>
        public static void RenderDashBackground(Graphics graphics, Rectanglei rectangle)
        {
            GraphicsState  v_state = graphics.Save();
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            try
            {
                Brush br = CoreBrushRegisterManager.GetBrush<Brush>(CoreSystem.Instance.Resources.GetImage("DASH"));
                if (br != null)
                {
                    graphics.FillRectangle(br, rectangle.X, rectangle.Y, rectangle.Width , rectangle.Height );
                }
            }
            catch { 
            }
            graphics.Restore(v_state);
        }
        public static GraphicsPath BuildRoundRectangle(Rectangle v_trc, int p1, int p2, int p3, int p4)
        {
            return BuildRoundRectangle(new Rectanglef(v_trc.X, v_trc.Y, v_trc.Width, v_trc.Height),
                p1, p2, p3, p4);
        }
        internal static void DrawBorder(ICoreGraphics coreGraphics, Rectangle rectangle, Color color, ButtonBorderStyle buttonBorderStyle)
        {
            coreGraphics.DrawRectangle(color.ToIGKColor(),
                rectangle.X,
                rectangle.Y,
                rectangle.Width - 1,
                rectangle.Height - 1);
        }
    }
}

