

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GdiBlendingBrushElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:GdiBlendingBrushElement.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.DrSStudio.Drawing2D.Tools;
    /// <summary>
    /// Represent blending brush element render mecanism
    /// </summary>
    [Core2DDrawingImageBrushItemAttribute("GdiBlendBrushMarker", typeof(Mecanism))]
    class GdiBlendingBrushElement : Core2DDrawingLayeredDualBrushElement
    {
        ImageElement m_currentElement;
        private float m_Opacity;
        private CompositingQuality m_SourceQuality;
        private CompositingQuality m_DestQuality;
        private InterpolationMode m_SourceInterpolation;
        private InterpolationMode m_DestInterpolation;
        private int m_Size;
        public int Size
        {
            get { return m_Size; }
            set
            {
                if (m_Size != value)
                {
                    m_Size = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void GeneratePath()
        {
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(this.GetBound());
            this.SetPath(p);
        }
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        public override Rectanglef GetBound()
        {
            return new Rectanglef(Vector2f.Zero, new Size2f(m_Size, m_Size));
        }
        public override void RenderSelection(Graphics g, ICore2DDrawingSurface surface)
        {
            //base.RenderSelection(g, surface);
            if (this.m_currentElement != null)
            {
                this.m_currentElement.RenderSelection(g, surface);
            }
        }
        public InterpolationMode DestInterpolation
        {
            get { return m_DestInterpolation; }
            set
            {
                if (m_DestInterpolation != value)
                {
                    m_DestInterpolation = value;
                }
            }
        }
        public InterpolationMode SourceInterpolation
        {
            get { return m_SourceInterpolation; }
            set
            {
                if (m_SourceInterpolation != value)
                {
                    m_SourceInterpolation = value;
                }
            }
        }
        public CompositingQuality DestQuality
        {
            get { return m_DestQuality; }
            set
            {
                if (m_DestQuality != value)
                {
                    m_DestQuality = value;
                }
            }
        }
        public CompositingQuality SourceQuality
        {
            get { return m_SourceQuality; }
            set
            {
                if (m_SourceQuality != value)
                {
                    m_SourceQuality = value;
                }
            }
        }
        public float Opacity
        {
            get { return m_Opacity; }
            set
            {
                if ((m_Opacity != value) && (value >= 0) && (value <= 1.0f))
                {
                    m_Opacity = value;
                }
            }
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.Solid | enuBrushSupport.LinearGradient | enuBrushSupport.PathGradient | enuBrushSupport.Fill;
            }
        }
        public GdiBlendingBrushElement()
        {
            this.m_Opacity = 1.0f;
            this.m_DestQuality = CompositingQuality.Default;
            this.m_SourceQuality = CompositingQuality.Default;
            this.m_DestInterpolation = InterpolationMode.HighQualityBilinear;
            this.m_SourceInterpolation = InterpolationMode.Default;
            this.m_Size = 16;
        }
        public override DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return DrSStudio.WinUI.Configuration.enuParamConfigType.ParameterConfig;
        }
        public override DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("MarkerDefinition");
            Type t = this.GetType();
            group.AddItem(t.GetProperty("Opacity"));
            group.AddItem(t.GetProperty("SourceQuality"));
            group.AddItem(t.GetProperty("SourceInterpolation"));
            group.AddItem(t.GetProperty("DestQuality"));
            group.AddItem(t.GetProperty("DestInterpolation"));
            group.AddItem(t.GetProperty("Size"));
            return parameters;
        }
        /// <summary>
        /// represent a mecanism element
        /// </summary>
        protected  new class Mecanism : Core2DDrawingMecanismBase
        {
            ImageElement m_selectedImageElement;
            GdiBlendingBrushElement m_markerElement;
            Bitmap m_offbmp;
            //Rectanglei Square;
            protected bool m_hasChanged = false;
            public int Size { get { return this.m_markerElement.Size; } set { this.m_markerElement.Size = value; } }
            /// <summary>
            /// get the marker element
            /// </summary>
            public GdiBlendingBrushElement MarkerElement { get { return this.m_markerElement; } }
            public ImageElement SelectedImageElement
            {
                get { return m_selectedImageElement; }
            }
            const int ST_GETTING_SIZE = 0x400;
            public override void Dispose()
            {
                if (m_offbmp != null)
                    this.m_offbmp.Dispose();
                this.m_markerElement.Dispose();
                base.Dispose();
            }
            protected override void RegisterSurface(ICore2DDrawingSurface surface)
            {
                base.RegisterSurface(surface);
                this.CurrentSurface.ElementToConfigure = this.m_markerElement;
                ToolGdiBrushManager.Instance.ShowTool(this.m_markerElement);
            }
            protected override void UnRegisterSurface(ICore2DDrawingSurface surface)
            {
                ToolGdiBrushManager.Instance.HideTool (this.m_markerElement);
                surface.Invalidate();
                surface.ElementToConfigure = null;
                base.UnRegisterSurface(surface);
            }
            public Mecanism()
            {
                this.m_markerElement = CreateMarker();
                this.Size = 16;
                //this.Square = new Rectanglei(0, 0, Size, Size);
                this.m_offbmp = null;
            }
            protected virtual  GdiBlendingBrushElement CreateMarker()
            {
                return new GdiBlendingBrushElement();
            }
            private bool SelectElement(CoreMouseEventArgs e)
            {
                //check first image element
                ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                for (int i = l.Elements.Count - 1; i >= 0; --i)
                {
                    if (l.Elements[i].Contains(e.FactorPoint) && (l.Elements[i] is ImageElement))
                    {
                        m_selectedImageElement = l.Elements[i] as ImageElement;
                       // l.Select(m_selectedImageElement);
                        this.m_markerElement.m_currentElement = m_selectedImageElement;
                        this.CurrentSurface.ElementToConfigure = this.m_markerElement;
                        OnImageElementChanged(EventArgs.Empty);
                        return true;
                    }
                }
                this.m_markerElement.m_currentElement = null;
                return false;
            }
            protected virtual void OnImageElementChanged(EventArgs eventArgs)
            {
                //get the changed element changed 
            }
            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
            }
            protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
            {
                base.OnMouseClick(e);
            }
            protected override void OnMouseDown(WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                            this.StartPoint = e.FactorPoint;
                            this.EndPoint = e.FactorPoint;
                        if (this.IsShiftKey)
                        {
                            //start get size                        
                            this.State = ST_GETTING_SIZE;
                        }
                        else { 
                             if (this.m_selectedImageElement != null)
                            {
                                ApplyBrush(EndPoint);
                            }
                        }
                        break;
                    case System.Windows.Forms.MouseButtons.Middle:
                        break;
                    case System.Windows.Forms.MouseButtons.None:
                        break;
                    case System.Windows.Forms.MouseButtons.Right:
                        break;
                    case System.Windows.Forms.MouseButtons.XButton1:
                        break;
                    case System.Windows.Forms.MouseButtons.XButton2:
                        break;
                    default:
                        break;
                }
            }
            protected override void OnMouseMove(WinUI.CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        if (this.State == ST_GETTING_SIZE)
                        {
                            this.Size = (int)Math.Max(1.0f, CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint));
                            this.CurrentSurface.Invalidate();
                            return;
                        }
                        else if (this.m_selectedImageElement != null)
                        {
                            ApplyBrush(EndPoint);
                        }
                        break;
                }
                this.CurrentSurface.Invalidate();
            }
            protected virtual  void ApplyBrush(Vector2f EndPoint)
            {
                  Vector2f v_c = EndPoint;
                            Matrix m = this.m_selectedImageElement.GetMatrix().Clone();
                            if (!m.IsIdentity && m.IsInvertible)
                            {
                                m.Invert();
                                Vector2f[] t = new Vector2f[] { this.EndPoint };
                                m.TransformPoints(t);
                                v_c = t[0];
                            }
                            using (Bitmap bmp = new Bitmap(Size, Size))
                            {
                                Graphics v_g = Graphics.FromImage(bmp);
                                Bitmap v_bmp = this.m_selectedImageElement.Bitmap;
                                using (Graphics g = Graphics.FromImage(v_bmp))
                                {
                                    Rectanglef v_r = new Rectanglef(new Vector2f(v_c.X - (Size / 2),
                                            v_c.Y - (Size / 2)),
                                            new SizeF(Size, Size));
                                    ImageAttributes attr = new ImageAttributes();
                                    //this.m_markerElement.Opacity
                                    attr.SetColorMatrix(new ColorMatrix(
                                        new float[][]{
                                new float []{1,0,0,0,0},
                                new float []{0,1,0,0,0},
                                new float []{0,0,1,0,0},
                                new float []{0,0,0,this.m_markerElement.Opacity  ,0},
                                new float []{0,0,0,0,1}
                            }
                                        ));
                                    GraphicsPath p = new GraphicsPath();
                                    v_g.Clear(Color.Transparent );
                                    p.AddEllipse(new Rectangle(Point.Empty, bmp.Size));
                                    v_g.SmoothingMode = global::System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                    v_g.FillPath (this.m_markerElement.FillBrush.GetBrush (),p);// new Rectangle(Point.Empty, bmp.Size));
                                    //v_g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias ;
                                    //v_g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;//.HighQualityBilinea
                                    //v_g.CompositingQuality = CompositingQuality.GammaCorrected;
                                    //v_g.DrawImage(v_bmp, new Rectangle(Point.Empty, bmp.Size), v_r, GraphicsUnit.Pixel);
                                    //g.DrawImage(
                                    //    this.pictureBox1.Image,
                                    //   v_r,
                                    //   v_r.X, v_r.Y, v_r.Width, v_r.Height,
                                    //    GraphicsUnit.Pixel,
                                    //    attr
                                    //    );
                                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                                    g.SmoothingMode = global::System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                    g.CompositingQuality = this.m_markerElement.m_DestQuality;
                                    g.InterpolationMode = this.m_markerElement.m_DestInterpolation;
                                    p.Reset();
                                    p.AddEllipse(v_r);
                                    Region rg = new System.Drawing.Region(p);
                                    g.Clip = (rg);
                                    g.CompositingMode = this.m_markerElement.CompositingMode;
                                    g.DrawImage(
                                      bmp,
                                     Rectangle.Round(v_r),
                                     0, 0, bmp.Width, bmp.Height,
                                      GraphicsUnit.Pixel,
                                      attr
                                      );
                                }
                            }
                        this.m_hasChanged = true;
            }
            protected override void OnMouseUp(WinUI.CoreMouseEventArgs e)
            {
                if ((e.Button == System.Windows.Forms.MouseButtons.Left))
                {
                    if (this.State == ST_GETTING_SIZE)
                    {
                        this.State = ST_NONE;
                        return;
                    }
                    if (this.m_selectedImageElement == null)
                    {
                        this.SelectElement(e);
                        //apply changement
                    }
                    else if (this.m_hasChanged)
                    {
                        Bitmap bmp = new Bitmap(this.m_selectedImageElement.Bitmap);
                        this.m_selectedImageElement.SetBitmap(bmp, false);
                        this.m_hasChanged = false;
                    }
                    return;
                }
            }
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                base.OnPaint(e);
                if (System.Windows.Forms.Control.MouseButtons == System.Windows.Forms.MouseButtons.None)
                {
                    Rectanglef v_r = new Rectanglef(new Vector2f(this.EndPoint.X - (Size / 2),
                                                      this.EndPoint.Y - (Size / 2)),
                                                      new SizeF(Size, Size));
                    GraphicsState st = e.Graphics.Save();
                    this.ApplyCurrentSurfaceTransform(e.Graphics);
                    e.Graphics.DrawRectangle(Pens.Yellow, v_r.X, v_r.Y, v_r.Width - 1, v_r.Height - 1);
                    e.Graphics.Restore(st);
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
            }
        }
    }
}

