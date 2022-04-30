using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ColorPicker
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public partial class IGKXColorPickerControl : UserControl
    {
        public IGKXColorPickerControl()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint 
                | ControlStyles.ResizeRedraw 
                | ControlStyles.OptimizedDoubleBuffer 
                , true);
            this.GenerateBitmap(false);
            this.SetCursorLocation(new Vector2f(center.X + DX, center.Y + DY));
            this.BackColor = global::System.Drawing.Color.Transparent;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
      

           const int COLOR_COUNT = 360;
        const int DX = 5;
        const int DY = 5;
        const int DEFAULT_WIDTH = 145;
        const int DEFAULT_HEIGHT = 145;
        Bitmap bmp;
        Vector2f curLocation;
        private float radius;
        private Vector2f center;
        private bool ms_select; //color selected by mouse
        //use for color selection
        private float distance;
        private float angle; //in radian
        private Colorf m_color;
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
                Colorf v_cl = this.m_color;
                CoreColorHandle.RGB b = new CoreColorHandle.RGB();
                b.Red = (byte)(v_cl.R * 255);
                b.Green = (byte)(v_cl.G * 255);
                b.Blue = (byte)(v_cl.B * 255);
                CoreColorHandle.HSV v = CoreColorHandle.RGBtoHSV(b);
                this.angle = (float)((v.Hue * 2 * Math.PI) / 255.0f);
                //get dispante points
                this.distance = (v.Saturation / 255.0f) * radius;
                //get system disce
                distance = Math.Min(radius, distance);
                Vector2f  c = this.center;
                //c.X += DX;
                //c.Y += DY;
                this.curLocation = new Vector2f (
                   c.X + (float)(distance * Math.Cos(angle)),
                   c.Y + (float)(distance * Math.Sin(angle)));
       
            }
            this.Invalidate();
            this.Update();
            if (this.ColorChanged != null)
                this.ColorChanged(this, eventArgs);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                //createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return createParams;
            }
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
            this.center = rc.GetCenter();
            this.GenerateBitmap(!this.Enabled);
            int w = Math.Min(rc.Width, rc.Height);
            rc.Width = w;
            rc.Height = w;
            //rc.Inflate(-1, -1);
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(rc);
            this.Region = new Region(p);
            p.Dispose();
            this.SetCursorLocation(center);
            this.Invalidate();
        }
        private void GenerateBitmap(bool bw)
        {
            if (this.bmp != null)
            {
                this.bmp.Dispose();
                this.bmp = null;
            }
            Rectangle r = this.ClientRectangle;
            r.Inflate(-4, -4);
            int w = Math.Min(r.Width, r.Height);
            if (w <= 0) return;
            this.bmp = new Bitmap(w, w);
            Graphics g = Graphics.FromImage(this.bmp);
            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            GraphicsPath p = new GraphicsPath();
            //seet the center and radius in the bitmap
            radius = w / 2.0f;
            center = new Vector2f(radius, radius);
            PointF[] tb = GetPoints(radius, center).CoreConvertTo<PointF[]>();
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
            radius = w / 2.0f;
            center = new Vector2f (r.X + radius, r.Y + radius);
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
                Rectangle rec = new Rectangle(0, 0, bmp.Width, bmp.Height);
                g.DrawImage(bmp, rec, rec.X, rec.Y, rec.Width, rec.Height,
                    GraphicsUnit.Pixel,
                    att);
                att.Dispose();
            }
            g.Dispose();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.DesignMode == false)
            {
                e.Graphics.Clear(Colorf.WhiteSmoke);
                base.OnPaint(e);
                //renderCircle      
                if ((this.bmp != null) && (this.bmp.PixelFormat != PixelFormat.Undefined))
                {
                    Rectangle cli = this.ClientRectangle;
                    cli.Inflate(-2, -2);
                    Rectangle rc = new Rectangle(cli.Location, bmp.Size);
                    //draw image
                    Graphics g = e.Graphics;
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.DrawImage(this.bmp, cli.Location);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    Pen p = CoreBrushRegister.GetPen<Pen>(Colorf.White);
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
            else {
                base.OnPaint(e);
            }
        }
        private void DrawCursor(PaintEventArgs e)
        {
            const int w = 3;
            Rectanglef r = new Rectanglef();
            r.Location = curLocation;
            r.Inflate(w, w);
            e.Graphics.FillEllipse(Brushes.Black , r.X, r.Y, r.Width , r.Height );
            using (Pen p = new Pen(System.Drawing.Color.White, 2))
            {
                e.Graphics.DrawEllipse(p, r.X, r.Y, r.Width, r.Height);
            }
        }
        private Colorf[] GetColors()
        {
            Colorf[] t = new Colorf[COLOR_COUNT];
            float step = (float)((255 / (float)COLOR_COUNT));
            float v = 0;
            for (int i = 0; i < t.Length; i++)
            {
                v = i * step;
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
            int h = (int)Math.Ceiling((angle * CoreMathOperation.ConvRdToDEGREE) * 255 / 360.0f);
            int s = (int)Math.Ceiling(((distance / radius) * 255.0f));
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
            Vector2f c = center;
            //c.X += DX;
            //c.Y += DY;
            float dx = point.X - c.X;
            float dy = point.Y - c.Y;
            distance = (float)Math.Sqrt((dx * dx) + (dy * dy));
            angle = GetAngle(c, point);
            distance = Math.Min(radius, distance);
            this.curLocation = new Vector2f (
                c.X + (float)(distance * Math.Cos(angle)),
                c.Y + (float)(distance * Math.Sin(angle)));
            this.Invalidate();
        }
        private float GetAngle(Vector2f  point1, Vector2f  point2)
        {
            float angle = 0.0f;
            //float dx = point2.X - point1.X;
            //float dy = point2.Y - point1.Y;
            angle = CoreMathOperation.GetAngle(point1, point2);
            //if (dx == 0.0f)
            //{
            //    if (dy > 0)
            //    {
            //        return (float)(Math.PI / 2.0f);
            //    }
            //    else
            //        return (float)(-Math.PI / 2.0f);
            //}
            //angle = (float)Math.Atan(dy / dx);
            //if (dx < 0)
            //    angle += (float)Math.PI;
            return angle;
        }
        protected override void Dispose(bool disposing)
        {

            if (disposing && (components != null))
            {
                components.Dispose();            
                if (this.bmp != null)
                {
                    this.bmp.Dispose();
                    this.bmp = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
