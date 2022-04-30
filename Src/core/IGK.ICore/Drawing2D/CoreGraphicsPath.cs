

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreGraphicsPath.cs
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
file:CoreGraphicsPath.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D.Segments;
using IGK.ICore.Drawing3D.Segments;
using IGK.ICore.GraphicModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    public class CoreGraphicsPath : 
        ICoreGraphicsPath, 
        ICloneable 
    {
        private ICoreGraphicsSegmentCollections m_Segments;
        private CoreGraphicsMarkerSegmentCollections m_markers;


        /// <summary>
        /// explose this graphics path in section of simple graphics path
        /// </summary>
        /// <returns></returns>
        public ICoreGraphicsPath[]  Explode(){
            byte[] b = null;
            Vector2f[] t = null;
            GetAllDefinition(out t, out b);
            List<ICoreGraphicsPath> c = new List<ICoreGraphicsPath>();

            List<Vector2f> v_pc = new List<Vector2f>();
            List<byte> v_t = new List<byte>();
            
            for (int i = 0; i < b.Length; i++)
            {
                if ((i > 0) && (b[i] == 0) && v_pc.Count>0)
                {
                    var path = new CoreGraphicsPath();
                    path.AddDefinition(v_pc.ToArray(), v_t.ToArray());
                    c.Add(path);
                    v_t.Clear();
                    v_pc.Clear();
                }

                v_pc.Add(t[i]);
                v_t.Add(b[i]);
                if ((b[i] & 0x80) == 0x80) {
                    
                    var path = new CoreGraphicsPath();
                    path.AddDefinition(v_pc.ToArray(), v_t.ToArray());
                    c.Add(path);
                    v_t.Clear();
                    v_pc.Clear();                    
                    continue;
                }
                
            }

            if (v_t.Count > 0) {
                var path = new CoreGraphicsPath();
                path.AddDefinition(v_pc.ToArray(), v_t.ToArray());
                c.Add(path);
            }
            return c.ToArray();
        }

        /// <summary>
        /// get the graphics utils
        /// </summary>
        protected  ICoreD2DPathUtils GraphicsUtils{
            get{
                return CoreApplicationManager.Application.GraphicsPathUtils;
            }
        }
        public Vector2f[] PathPoints { get {
            byte[] b = null;
            Vector2f[] t = null;
            GetAllDefinition (out t, out b);
            return t;
        } }
        public Byte[] PathTypes { get{
            byte[] b = null;
            Vector2f[] t = null;
            GetAllDefinition(out t, out b);
            return b;
        } }
        private ICoreBrushOwner m_owner;
        public ICorePen StrokeBrush { get {
            if (this.m_owner !=null)
            return this.m_owner.GetBrush(enuBrushMode.Stroke) as ICorePen;
            return null;
        } }

     

        public ICoreBrush FillBrush { get{
            if (this.m_owner != null)
            return this.m_owner.GetBrush(enuBrushMode.Fill);
            return null;
        } 
        }
        /// <summary>
        /// set the markers in this graphics path
        /// </summary>
        public void SetMarkers()
        {
            this.m_markers.Add(this.m_Segments.Count);
        }
        public void FillRegion(Colorf color, ICoreGraphicsRegion region)
        {
        }
        public static Size2f MeasureString(string t, CoreFont font)
        {
           return  CoreApplicationManager.Application.ResourcesManager.MeasureString(t, font);
        }
        public ICoreGraphicsSegmentCollections Segments
        {
            get { return  m_Segments; }
        }
        public CoreGraphicsPath()
        {
            this.m_Segments = new CoreGraphicsPathSegments(this);
            this.m_markers = new CoreGraphicsMarkerSegmentCollections(this);
        }
        public CoreGraphicsPath(ICoreBrushOwner owner): this()
        {
            this.m_owner = owner;
        }
        public bool IsEmpty
        {
            get { return (this.m_Segments.Count == 0); }
        }
        private enuFillMode m_FillMode;

        public enuFillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                }
            }
        }
        private float m_Tension;
        public float Tension
        {
            get { return m_Tension; }
            set
            {
                if (m_Tension != value)
                {
                    m_Tension = value;
                }
            }
        }
        public void AddLine(float x1, float y1, float x2, float y2) {
            this.AddLine(new Vector2f(x1, y1), new Vector2f(x2, y2));
        }
        public void AddLine(float x1, float y1, float z1, float x2, float y2, float z2) {
            this.AddLine(new Vector3f(x1, y1, z1), new Vector3f(x2, y2, z2));
        }
        private bool MustarStartNewFigure()
        {
            return (this.m_Segments.Count == 0) ||
                this.m_Segments[this.m_Segments.Count - 1].IsClosed;
        }
        public void AddLine(Vector2f startPoint, Vector2f endPoint)
        {
            this.m_Segments.Add(new LineSegment(startPoint, endPoint, this.MustarStartNewFigure()));
        }
        public void AddRectangle(Rectanglef rectangle)
        {
            this.m_Segments.Add(new RectangleSegment(rectangle));
        }
        public void AddLine(Vector3f startPoint, Vector3f endPoint)
        {
            this.m_Segments.Add(new Line3DSegment(startPoint, endPoint));
        }
        public void AddEllipse(Vector2f center, Vector2f radius)
        {
            this.m_Segments.Add(new CircleSegment(center, radius));
        }
        public void AddArc(Vector2f center, Vector2f radius, float startAngle, float sweepAngle, bool closed)
        {
            this.m_Segments.Add(new ArcSegment(center, radius, startAngle, sweepAngle, closed ));
        }
        public void AddPolygon(Vector2f[] points)
        {
            if ((points == null) || (points.Length == 0))
                return;
            this.m_Segments.Add(new PolygonSegment(points));
        }
        public void AddPolygon(Vector3f[] points)
        {
            throw new NotImplementedException(nameof(AddPolygon));
        }
        /// <summary>
        /// get the bounds of this graphics path with core application Measurer utility
        /// </summary>
        /// <returns></returns>
        public Rectanglef GetBounds()
        {
            var p = CoreApplicationManager.Application.GraphicsPathUtils?.Measurer;
            if (p!=null)
                return p.GetBounds(this);
            return this.GetPrimaryBounds();
        }

        public Rectanglef GetPrimaryBounds()
        {
            Vector2f[] c = null;
            Byte[] tab = null;
            this.GetAllDefinition(out c, out tab);
            if ((c == null) || (c.Length == 0))
                return Rectanglef.Empty;
            return CoreMathOperation.GetBounds(c);
        }
        public void Dispose()
        {
        }
        public object Clone()
        {
            CoreGraphicsPath c = new CoreGraphicsPath();
            Vector2f[] def = null;
            byte[] tab = null;
            this.GetAllDefinition(out def, out tab);
            c.AddDefinition(def, tab);
            return c;
        }
        public bool IsVisible(Vector2f point)
        {
            if (this.IsEmpty)
                return false;
            Vector2f[] c = null;
            Byte[] tab = null;
            this.GetAllDefinition(out c, out tab);
            ICoreD2DPathUtils u = CoreApplicationManager.Application.GraphicsPathUtils;
            if (u != null)
            {
                return u.IsVisible(c, tab, this.FillMode, point);
            }
            return GetBounds().Contains(point);
        }
        public bool IsVisible(Vector3f point)
        {
            return IsVisible(new Vector2f(point.X, point.Y));
        }
        /// <summary>
        /// transform the segment with the matrix
        /// </summary>
        /// <param name="matrix">matrix used to transform</param>
        public  void Transform(Matrix matrix)
        {
            foreach (ICoreGraphicsSegment item in this.m_Segments)
            {
                item.Transform(matrix);
            }
        }
        public void AddDefinition(Vector2f[] points, byte[] types)
        {
            if ((points != null) && (types != null) && (points.Length == types.Length))
            {
                PathSegment v_psegment = new PathSegment();
                v_psegment.Append(points, types);
                this.m_Segments.Add(v_psegment );
            }
        }
        public void AddPie(Rectanglef bound, float StartAngle, float sweepAngle)
        {
            this.m_Segments .Add (new PieSegment(
                bound.Center ,
                new Vector2f(bound.Width /2.0f,
                bound.Height  /2.0f),
                StartAngle,
                sweepAngle));
        }
        public void CloseAllFigures()
        {
            throw new NotImplementedException();
        }
         /// <summary>
        /// is out line visible
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsOutilineVisible(Vector2f point)
        {
            float v_width = 1.0F;
            if (this.m_owner != null)
            {
                var p = this.m_owner.GetBrush(enuBrushMode.Stroke) as ICorePen;
                if (p != null)
                    v_width = p.Width;
            }
            foreach (ICoreGraphicsSegment item in this.Segments)
            {
                if (item.IsOutilineVisible(point, v_width))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// get point definition of this path
        /// </summary>
        /// <param name="points"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public bool GetAllDefinition(out Vector2f[] points, out byte [] types)
        {
            List<Vector2f> t = new List<Vector2f>();
            List<Byte> b = new List<byte>();
            foreach (ICoreGraphicsSegment item in this.m_Segments)
            {
                if (!item.IsEmpty)
                {
                    t.AddRange(item.GetPathPoints());
                    b.AddRange(item.GetPathTypes());
                }
            }
            points = t.ToArray();
            types = b.ToArray();
            return true;
        }
        /// <summary>
        /// invert the graphics path
        /// </summary>
        public void Invert()
        {
            foreach (ICoreGraphicsSegment item in this.m_Segments)
            {
                item.Invert();
            }
        }
        public void AddBezier(Vector2f start, Vector2f Control1, Vector2f Control2, Vector2f endPoint)
        {
            this.m_Segments.Add(new BezierSegment(start, Control1, Control2, endPoint));
        }
        public void AddClosedCurve(Vector2f[] point)
        {
            this.m_Segments.Add(new SplineSegment(point, 0.5f, true));
        }
        public void AddClosedCurve(Vector2f[] Points, float Tension)
        {
            this.Segments.Add(new SplineSegment(Points, Tension, true ));
        }
        public void AddCurve(Vector2f[]  points, float tension, bool closed)
        {
            this.m_Segments.Add(new SplineSegment(points, tension, closed));
        }
        public void AddCurve(Vector2f[] points)
        {
            this.m_Segments.Add(new SplineSegment(points, 0.5f, false));
        }
        public void CloseFigure()
        {
            if (this.m_Segments.Count > 0)
            {
                this.m_Segments[this.m_Segments.Count - 1].CloseFigure();
            }            
        }
        public void Warp(Vector2f[] tab,             
                    Rectanglef bounds,
                    Matrix matrix,
                    enuWarpMode WarpMode,
                    float Flatness)
        {
            ICore2DPathDefinition def = this.GraphicsUtils.Warp(this, tab, bounds , 
                matrix,WarpMode, Flatness);
            if (def != null)
            {
                this.Reset();
                this.AddDefinition(def.Points, def.Types);
            }
        }
        public void Widen(ICorePen pen, Matrix matrix, float flatness)
        {
            ICore2DPathDefinition def = this.GraphicsUtils.Widen(this, pen, matrix, flatness);
            if (def != null)
            {
                this.Reset();
                this.AddDefinition(def.Points, def.Types);
            }
        }
        public void Flatten(float flatness)
        {
            ICore2DPathDefinition def =  this.GraphicsUtils.Flatten(this, CorePen.Black, Matrix.Identity , flatness);
            if (def != null)
            {
                this.Reset();
                this.AddDefinition(def.Points, def.Types);
            }
        }
        public void AddArc(Rectangled v_rc, float startAngle, float sweepAngle)
        {
            this.AddArc(new Rectanglef((float)v_rc.X,
                (float)v_rc.Y,
                (float)v_rc.Width,
                (float)v_rc.Height), startAngle, sweepAngle);
        }
        public void AddArc(Rectanglef v_rc, float startAngle, float sweepAngle)
        { 
            this.AddArc (v_rc, startAngle, sweepAngle, false );
        }
        public void AddArc(Rectanglef v_rc, float startAngle, float sweepAngle, bool closed)
        {
            this.Segments.Add(new ArcSegment(v_rc.Center,
                new Vector2f(v_rc.Width / 2.0f, v_rc.Height /2.0f)
                , startAngle, sweepAngle, closed ));
        }
        public Rectanglef GetBounds(Matrix matrix)
        {
            Vector2f[] t = CoreMathOperation.GetPoints(this.GetBounds());
            CoreMathOperation.MultMatrixTransformPoint(matrix, t);
            return CoreMathOperation.GetBounds(t);
        }
        public void Reset()
        {
            this.m_Segments.Clear();            
        }
        internal void AddPath(Vector2f[] vector2f, byte[] p)
        {
            if ((vector2f == null) || 
                (p==null)|| 
                (vector2f.Length != p.Length ))
                return;
            this.m_Segments.Add(new PathSegments(vector2f, p));
        }
        public void AddText(string text, Rectanglef bounds, CoreFont coreFont)
        {
            if (coreFont == null) 
                return;

            var s  =  TextSegment.CreateSegment(text, bounds, coreFont, coreFont.HorizontalAlignment , coreFont.VerticalAlignment );
            if (s != null)
            {
                this.m_Segments.Add(s);
            }
        }
        public void AddText(string text, string fontName, float fontSize, int fontStyle, Vector2f location)
        {
            if (string.IsNullOrEmpty(text))
                return;
            CoreFont ft = CoreFont.CreateFrom(fontName, null);
            if (ft == null)
                return;
            ft.FontSize = fontSize;
            ft.FontStyle = (enuFontStyle)fontStyle;
            Size2f s = MeasureString (text,ft);
            AddText(text, new Rectanglef(location, s), ft);
        }
        public void AddSegment(PathSegment pSegment)
        {
            if (pSegment !=null)
            this.m_Segments.Add(pSegment);
        }
        /// <summary>
        /// add graphics path
        /// </summary>
        /// <param name="path"></param>
        public void Add(ICoreGraphicsPath path)
        {
            Vector2f[] t = null;
            Byte[] b = null;
            path.GetAllDefinition(out t, out b);
            if ((t != null) && (b != null) && (t.Length == b.Length))
            {
                this.AddDefinition(t, b);
            }
        }
        public void AddLines(Vector2f[] tab)
        {
            if ((tab == null) || (tab.Length < 2))
                return;
            PathSegment p = new PathSegment();
            p.AddLines(tab);
            this.m_Segments.Add (p);
        }

        public void AddRectangle(int x, int y, int width, int height)
        {
            this.AddRectangle(new Rectanglef(x, y, width, height));
        }
        public void AddRectangle(float  x, float y, float width, float height)
        {
            this.AddRectangle(new Rectanglef(x, y, width, height));
        }


        public void AddEllipse(Rectanglef rectangle)
        {
            this.AddEllipse(rectangle.Center, rectangle.BottomRight - rectangle.Center);
        }
        public class CoreGraphicsPathSegments : ICoreGraphicsSegmentCollections
        {
            private CoreGraphicsPath m_graphics;
            List<ICoreGraphicsSegment> m_segments;
            internal CoreGraphicsPathSegments(CoreGraphicsPath vpath)
            {
                this.m_graphics = vpath;
                this.m_segments = new List<ICoreGraphicsSegment>();
            }
            public void Clear()
            {
                this.m_segments.Clear();
            }
            public ICoreGraphicsPath CoreGraphicsPath
            {
                get { return this.m_graphics; }
            }
            public int Count
            {
                get { return this.m_segments.Count; }
            }
            public void Add(ICoreGraphicsSegment segment)
            {
                if (!this.m_segments.Contains(segment))
                {
                    this.m_segments.Add(segment);
                }
            }
            public void Remove(ICoreGraphicsSegment segment)
            {
                if (this.m_segments.Contains(segment))
                    this.m_segments.Remove(segment);
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_segments.GetEnumerator();
            }
            ICoreGraphicsPath ICoreGraphicsSegmentCollections.CoreGraphicsPath
            {
                get { return m_graphics; }
            }
            public ICoreGraphicsSegment this[int index]
            {
                get
                {
                    if (this.m_segments.IndexExists(index))
                        return this.m_segments[index];
                    return null;
                }
            }

        }


        class CoreGraphicsMarkerSegmentCollections : IEnumerable
        {
            private CoreGraphicsPath m_coreGraphicsPath;
            private List<int> m_markers;

            public CoreGraphicsMarkerSegmentCollections(CoreGraphicsPath coreGraphicsPath)
            {
                this.m_coreGraphicsPath = coreGraphicsPath;
                this.m_markers = new List<int>();
            }
            public int Count { get { return this.m_markers.Count; } }
            public IEnumerator GetEnumerator()
            {
                return this.m_markers.GetEnumerator();
            }

            internal void Add(int p)
            {
                this.m_markers.Add(p);
            }
            internal void Clear()
            {
                this.m_markers.Clear ();
            }
        }
        /// <summary>
        /// get point definition of this path
        /// </summary>
        /// <returns></returns>
        public CoreGraphicsPathDefinition GetAllDefinition()
        {
            CoreGraphicsPathDefinition def = new CoreGraphicsPathDefinition();
            Vector2f[] points = null;
            byte[] types = null;
            GetAllDefinition(out points, out types);
            def.Points = points;
            def.Types = types;
            return def;
        }

        internal void Reverse()
        {
            var h = CoreApplicationManager.Application.GraphicsPathUtils.Reverse(this);
            this.Reset();
            this.AddDefinition(h.PathPoints, h.PathTypes);
            h.Dispose();
        }
    }
}

