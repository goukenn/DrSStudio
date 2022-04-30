

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RoundRectangleElement.cs
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
file:RoundRectangleElement.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.Drawing2D.Segments;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.MecanismActions;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("RoundRect",
    typeof(Mecanism),
    Keys = enuKeys.Shift | enuKeys.R)]
    public class RoundRectangleElement : RectangleElement
    {
        private Vector2f m_TopLeft;
        private Vector2f m_TopRight;
        private Vector2f m_BottomLeft;
        private Vector2f m_BottomRight;
        private enuRoundStyle m_RoundStyle;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuRoundStyle.Standard)]
        public enuRoundStyle RoundStyle
        {
            get { return m_RoundStyle; }
            set
            {
                if (m_RoundStyle != value)
                {
                    m_RoundStyle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2f BottomRight
        {
            get { return m_BottomRight; }
            set
            {
                if (!m_BottomRight.Equals(value))
                {
                    m_BottomRight = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2f BottomLeft
        {
            get { return m_BottomLeft; }
            set
            {
                if (!m_BottomLeft.Equals(value))
                {
                    m_BottomLeft = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2f TopRight
        {
            get { return m_TopRight; }
            set
            {
                if (!(m_TopRight.Equals(value)))
                {
                    m_TopRight = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.ICore.Codec.CoreXMLAttribute()]
        public Vector2f TopLeft
        {
            get { return m_TopLeft; }
            set
            {
                if (!m_TopLeft.Equals(value))
                {
                    m_TopLeft = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        /// <summary>
        /// create a round rectangle element
        /// </summary>
        /// <param name="rc"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static RoundRectangleElement Create(Rectanglef rc, float radius)
        {
            RoundRectangleElement v_rc = new RoundRectangleElement();
            v_rc.Bounds = rc;
            v_rc.m_BottomLeft = new Vector2f(radius, radius);
            v_rc.m_BottomRight = new Vector2f(radius, radius);
            v_rc.m_TopLeft = new Vector2f(radius, radius);
            v_rc.m_TopRight = new Vector2f(radius, radius);
            v_rc.InitElement();
            return v_rc;
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            GeneratePath(p);
        }

       
        protected void GeneratePath(CoreGraphicsPath p)
        {
            switch (RoundStyle)
            {
                case enuRoundStyle.Inner:
                    BuildInverRoundPath(p);
                    break;
                case enuRoundStyle.Flat:
                    BuildFlatRoundPath(p);
                    break;
                case enuRoundStyle.Plus:
                    BuildPlusPath(p);
                    break;
                case enuRoundStyle.Outer:
                    BuildOuterPath(p);
                    break;
                case enuRoundStyle.Standard:
                default:
                    BuildRoundPath(p);
                    break;
            }
        }
        private void BuildOuterPath(CoreGraphicsPath path)
        {
            float vtl_dx = Math.Max(this.m_TopLeft.X * 2, 0.1f);
            float vtl_dy = Math.Max(this.m_TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(this.m_TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(this.m_TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(this.m_BottomRight.X * 2, 0.1f);
            float vbr_dy = Math.Max(this.m_BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(this.m_BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(this.m_BottomLeft.Y * 2, 0.1f);
            Rectanglef v_rect = this.Bounds;
            PathSegment pSegment = new PathSegment();
            pSegment.AddArc(new Rectanglef(
                v_rect.X - vtl_dx / 2.0f, v_rect.Y - vtl_dy / 2.0f,
                vtl_dx, vtl_dy), 90, 270.0f);
            pSegment.AddArc(new Rectanglef(
                v_rect.X + v_rect.Width - vtr_dx / 2.0f,
                    v_rect.Y - vtr_dy / 2.0f,
                vtr_dx, vtr_dy),
                180.0f, 270.0f);
            pSegment.AddArc(new Rectanglef(
                v_rect.X + v_rect.Width - vbr_dx / 2.0f,
                v_rect.Y + v_rect.Height - vbr_dy / 2.0f,
                vbr_dx, vbr_dy), -90.0f, 270.0f);
            pSegment.AddArc(
                new Rectanglef(
                    v_rect.X - vbl_dx / 2.0f, v_rect.Y + v_rect.Height - vbl_dy / 2.0f,
                    vbl_dx, vbl_dy), 0.0f, 270.0f);
            pSegment.CloseFigure();
            path.AddSegment(pSegment);
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            ICoreParameterGroup group = parameters.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("RoundStyle"));
            string defv = string.Empty;
            if (m_BottomLeft.Equals(m_BottomRight) && m_BottomRight.Equals(m_TopLeft)
                && m_TopLeft.Equals(m_TopRight))
            {
                defv = string.Format("{0} {1};", ((CoreUnit)m_TopRight.X),
                    ((CoreUnit)m_TopRight.Y));
            }
            else
            {
                defv = string.Format("{0} {1};", ((CoreUnit)m_TopLeft.X),
                    ((CoreUnit)m_TopLeft.Y));
                defv += string.Format("{0} {1};", ((CoreUnit)m_TopRight.X),
                    ((CoreUnit)m_TopRight.Y));
                defv += string.Format("{0} {1};", ((CoreUnit)m_BottomLeft.X),
                    ((CoreUnit)m_BottomLeft.Y));
                defv += string.Format("{0} {1};", ((CoreUnit)m_BottomRight.X),
                    ((CoreUnit)m_BottomRight.Y));
            }
            group.AddItem("Radius",
                "lb.Radius.caption",
                defv,
                 enuParameterType.Text,
                _ParamRadiusChanged);
            return parameters;
        }
        void _ParamRadiusChanged(object sender, CoreParameterChangedEventArgs e)
        {
            string c = Convert.ToString(e.Value);
            if (string.IsNullOrEmpty(c))
                return;
            string[] d = c.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<CoreUnit> m_unit = new List<CoreUnit>();
            for (int i = 0; i < d.Length; i++)
            {
                if (string.IsNullOrEmpty(d[i])) continue;
                m_unit.Add(d[i]);
            }
            switch (m_unit.Count)
            {
                case 1:
                    //for all 
                    this.SetAllRadius(m_unit[0], m_unit[0]);
                    break;
                case 2:
                    this.SetAllRadius(
                        m_unit[0],
                        m_unit[1]);
                    break;
                case 4:
                    SetRadius(
                        m_unit[0],
                        m_unit[1],
                        m_unit[2],
                        m_unit[3]);
                    break;
                case 8:
                    {
                        float m = ((ICoreUnitPixel)m_unit[0]).Value;
                        float y = ((ICoreUnitPixel)m_unit[1]).Value;
                        m_TopLeft = new Vector2f(m, y);
                        m = ((ICoreUnitPixel)m_unit[2]).Value;
                        y = ((ICoreUnitPixel)m_unit[3]).Value;
                        m_TopRight = new Vector2f(m, y);
                        m = ((ICoreUnitPixel)m_unit[4]).Value;
                        y = ((ICoreUnitPixel)m_unit[5]).Value;
                        m_BottomLeft = new Vector2f(m, y);
                        m = ((ICoreUnitPixel)m_unit[6]).Value;
                        y = ((ICoreUnitPixel)m_unit[7]).Value;
                        m_BottomRight = new Vector2f(m, y);
                        this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    }
                    break;
            }
        }
        public void SetRadius(CoreUnit topl, CoreUnit topr, CoreUnit bottoml, CoreUnit bottomr)
        {
            float m = ((ICoreUnitPixel)topl).Value;
            float y = ((ICoreUnitPixel)topr).Value;
            float x = ((ICoreUnitPixel)bottoml).Value;
            float z = ((ICoreUnitPixel)bottomr).Value;
            m_BottomLeft = new Vector2f(x, x);
            m_BottomRight = new Vector2f(z, z);
            m_TopRight = new Vector2f(y, y);
            m_TopLeft = new Vector2f(m, m);
            this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        public void SetAllRadius(CoreUnit rx, CoreUnit ry)
        {
            float m = ((ICoreUnitPixel)rx).Value;
            float y = ((ICoreUnitPixel)ry).Value;
            m_BottomLeft = new Vector2f(m, y);
            m_BottomRight = new Vector2f(m, y);
            m_TopRight = new Vector2f(m, y);
            m_TopLeft = new Vector2f(m, y);
            this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        private void BuildPlusPath(CoreGraphicsPath path)
        {
            float vtl_dx = Math.Max(this.TopLeft.X, 0.1f);
            float vtl_dy = Math.Max(this.TopLeft.Y, 0.1f);
            float vtr_dx = Math.Max(this.TopRight.X, 0.1f);
            float vtr_dy = Math.Max(this.TopRight.Y, 0.1f);
            float vbr_dx = Math.Max(this.BottomRight.X, 0.1f);
            float vbr_dy = Math.Max(this.BottomRight.Y, 0.1f);
            float vbl_dx = Math.Max(this.BottomLeft.X, 0.1f);
            float vbl_dy = Math.Max(this.BottomLeft.Y, 0.1f);
            Rectanglef v_rect = this.Bounds;
            Vector2f[] pts = new Vector2f[12];
            pts[0] = new Vector2f(v_rect.X, v_rect.Y + vtl_dy);
            pts[1] = new Vector2f(v_rect.X + vtl_dx, v_rect.Y + vtl_dy);
            pts[2] = new Vector2f(v_rect.X + vtl_dx, v_rect.Y);
            pts[3] = new Vector2f(v_rect.X + v_rect.Width - vtr_dx, v_rect.Y);
            pts[4] = new Vector2f(v_rect.X + v_rect.Width - vtr_dx, v_rect.Y + vtr_dy);
            pts[5] = new Vector2f(v_rect.X + v_rect.Width, v_rect.Y + vtr_dy);
            pts[6] = new Vector2f(v_rect.X + v_rect.Width, v_rect.Y + v_rect.Height - vbr_dy);
            pts[7] = new Vector2f(v_rect.X + v_rect.Width - vbr_dx, v_rect.Y + v_rect.Height - vbr_dy);
            pts[8] = new Vector2f(v_rect.X + v_rect.Width - vbr_dx, v_rect.Y + v_rect.Height);
            pts[9] = new Vector2f(v_rect.X + vbl_dx, v_rect.Y + v_rect.Height);
            pts[10] = new Vector2f(v_rect.X + vbl_dx, v_rect.Y + v_rect.Height - vbl_dy);
            pts[11] = new Vector2f(v_rect.X, v_rect.Y + v_rect.Height - vbl_dy);
            path.AddPolygon(pts);
            path.CloseFigure();
        }
        private void BuildFlatRoundPath(CoreGraphicsPath path)
        {
            float vtl_dx = Math.Max(this.TopLeft.X * 2, 0.1f);
            float vtl_dy = Math.Max(this.TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(this.TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(this.TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(this.BottomRight.X * 2, 0.1f);
            float vbr_dy = Math.Max(this.BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(this.BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(this.BottomLeft.Y * 2, 0.1f);
            Rectanglef v_rect = this.Bounds;
            //top left
            path.AddPolygon(
            new Vector2f[]{
                //top left
                new Vector2f(
                v_rect.X,
                v_rect.Y + this.TopLeft.Y),
                new Vector2f(
                v_rect.X + this.TopLeft.X,
                v_rect.Y ),
                //top - right
                new Vector2f (
                v_rect.X + v_rect.Width - this.TopRight.X,
                v_rect.Y ),
                new Vector2f (
                v_rect.X + v_rect.Width,
                v_rect.Y + this.TopRight.Y),
                //bottom- right
                new Vector2f (
                v_rect.X + v_rect.Width ,
                v_rect.Y + v_rect.Height - this.BottomRight.Y),
                new Vector2f (
                v_rect.X + v_rect.Width - this.BottomRight.X,
                v_rect.Y + v_rect.Height),
                //bottom -left
                new Vector2f(
                v_rect.X + this.BottomLeft.X,
                v_rect.Y + v_rect.Height),
                new Vector2f(
                v_rect.X ,
                v_rect.Y + v_rect.Height - this.BottomLeft.Y)
            });
            path.CloseFigure();
        }
        private void BuildInverRoundPath(CoreGraphicsPath path)
        {
            float vtl_dx = Math.Max(this.TopLeft.X * 2, 0.1f);
            float vtl_dy = Math.Max(this.TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(this.TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(this.TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(this.BottomRight.X * 2, 0.1f);
            float vbr_dy = Math.Max(this.BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(this.BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(this.BottomLeft.Y * 2, 0.1f);
            Rectanglef v_rect = this.Bounds;
            PathSegment pSegment = new PathSegment();
            //top left
            pSegment.AddArc(new Rectanglef(
                v_rect.X - this.TopLeft.X,
                v_rect.Y - this.TopLeft.Y,
                vtl_dx, vtl_dy),
                90.0f, -90.0f);
            //top right
            pSegment.AddArc(new Rectanglef(
                v_rect.X + v_rect.Width - this.TopRight.X,
                v_rect.Y - this.TopRight.Y,
                vtr_dx, vtr_dy),
                -180.0f, -90.0f);
            //bottom right
            pSegment.AddArc(new Rectanglef(
                v_rect.X + v_rect.Width - this.BottomRight.X,
                v_rect.Y + v_rect.Height - this.BottomRight.Y,
                vbr_dx, vbr_dy),
                -90.0f, -90.0f);
            //bottom left
            pSegment.AddArc(new Rectanglef(
                v_rect.X - this.BottomLeft.X,
                v_rect.Y + v_rect.Height - this.BottomLeft.Y,
                vbl_dx,
                vbl_dy),
                0.0f, -90.0f);
            pSegment.CloseFigure();
            path.AddSegment(pSegment);
        }
        private void BuildRoundPath(CoreGraphicsPath path)
        {
            float vtl_dx = Math.Max(this.m_TopLeft.X * 2, 0.1f);
            float vtl_dy = Math.Max(this.m_TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(this.m_TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(this.m_TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(this.m_BottomRight.X * 2, 0.1f);
            float vbr_dy = Math.Max(this.m_BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(this.m_BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(this.m_BottomLeft.Y * 2, 0.1f);
            Rectanglef v_rect = this.Bounds;
            PathSegment pSegment = new PathSegment();
            pSegment.AddArc(new Rectanglef(v_rect.X, v_rect.Y, vtl_dx, vtl_dy), 180.0f, 90.0f);
            pSegment.AddArc(new Rectanglef(
                v_rect.X + v_rect.Width - vtr_dx, v_rect.Y,
                vtr_dx, vtr_dy), -90.0f, 90.0f);
            pSegment.AddArc(new Rectanglef(
                v_rect.X + v_rect.Width - vbr_dx, v_rect.Y + v_rect.Height - vbr_dy,
                vbr_dx, vbr_dy), 0.0f, 90.0f);
            pSegment.AddArc(
                new Rectanglef(
                    v_rect.X, v_rect.Y + v_rect.Height - vbl_dy,
                    vbl_dx, vbl_dy), 90.0f, 90.0f);
            pSegment.CloseFigure();
            path.AddSegment(pSegment);
        }
        public new class Mecanism : Core2DDrawingRectangleMecanismBase<RoundRectangleElement>
        {
           
            
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                 
                base.BeginDrawing(e);
                this.State = ST_CREATING;
                this.Element.SuspendLayout ();
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                base.UpdateDrawing(e);
                this.EndPoint = e.FactorPoint;
                this.Element.Bounds  = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                //base.EndDrawing(e);
                this.UpdateDrawing(e);
                this.Element.ResumeLayout();
                this.Invalidate();
                this.State = ST_EDITING;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
            }
            const int SN_TOPLEFT = 10;
            const int SN_TOPRIGHT = 11;
            const int SN_BOTTOMRIGHT = 12;
            const int SN_BOTTOMLEFT = 13;
            protected internal override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if ((this.Element == null) || (this.RegSnippets.Count <=0))
                {
                    return;
                }
                    Rectanglef v_rc = this.Element.Bounds;
                                Vector2f[] t = CoreMathOperation.GetResizePoints(v_rc);
                this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(t[1]);
                this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(t[3]);
                this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(t[5]);
                this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(t[7]);
                    RegSnippets[SN_TOPLEFT].Location =
                        CurrentSurface.GetScreenLocation(
                        v_rc.Location + this.Element.TopLeft);
                    RegSnippets[SN_TOPRIGHT].Location =
                        CurrentSurface.GetScreenLocation(
                        new Vector2f(v_rc.Right, v_rc.Y) +
                        new Vector2f(-this.Element.TopRight.X,
                            this.Element.TopRight.Y));
                    RegSnippets[SN_BOTTOMRIGHT].Location =
                        CurrentSurface.GetScreenLocation(
                        new Vector2f(v_rc.Right, v_rc.Bottom) - this.Element.BottomRight);
                    RegSnippets[SN_BOTTOMLEFT].Location =
     CurrentSurface.GetScreenLocation(
     new Vector2f(v_rc.X, v_rc.Bottom) + new Vector2f(this.Element.BottomLeft.X,
         -this.Element.BottomLeft.Y)
         );
            }
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element == null)
                {
                    return;
                }
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, 3, 3));
                    AddSnippet(CurrentSurface.CreateSnippet(this, SN_TOPLEFT, SN_TOPLEFT));
                    AddSnippet(CurrentSurface.CreateSnippet(this, SN_TOPRIGHT, SN_TOPRIGHT));
                    AddSnippet(CurrentSurface.CreateSnippet(this, SN_BOTTOMRIGHT, SN_BOTTOMRIGHT));
                    AddSnippet(CurrentSurface.CreateSnippet(this, SN_BOTTOMLEFT, SN_BOTTOMLEFT));
                    this.RegSnippets[SN_TOPLEFT].Shape =enuSnippetShape.Circle;
                    this.RegSnippets[SN_TOPRIGHT].Shape = enuSnippetShape.Circle;
                    this.RegSnippets[SN_BOTTOMLEFT].Shape = enuSnippetShape.Circle;
                    this.RegSnippets[SN_BOTTOMRIGHT].Shape = enuSnippetShape.Circle;
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {             
                Rectanglef v_rc = this.Element.Bounds;
                float w = 0.0f; 
                float h = 0.0f;
                Vector2f pts = Vector2f.Zero;
                switch (this.Snippet.Index)
                {
                    case 3:
                        if (e.FactorPoint.X < v_rc.Right)
                        {
                            v_rc.Width = Math.Abs(v_rc.Right - e.FactorPoint.X);
                            v_rc.X = e.FactorPoint.X;
                        }
                        break;
                    case 0:
                        if (e.FactorPoint.Y < v_rc.Bottom)
                        {
                            v_rc.Height = Math.Abs(v_rc.Bottom - e.FactorPoint.Y);
                            v_rc.Y = e.FactorPoint.Y;
                        }
                        break;
                    case 2:
                        if (e.FactorPoint.Y > v_rc.Top)
                        {
                            v_rc.Height = Math.Abs(e.FactorPoint.Y - v_rc.Top);
                        }
                        break;
                    case 1:
                        if (e.FactorPoint.X > v_rc.Left)
                        {
                            v_rc.Width = Math.Abs(e.FactorPoint.X - v_rc.Left);
                            //v_rc.X = e.FactorPoint.X;
                        }
                        break;
                }
                this.Element.Bounds = v_rc;
                switch (this.Snippet.Demand)
                {
                    case SN_TOPLEFT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, v_rc.Location);
                        w = (pts.X > 0) ? Math.Min(pts.X, v_rc.Width - this.Element.TopRight.X) : 0;
                        h = (pts.Y > 0) ? Math.Min(pts.Y, v_rc.Height - this.Element.BottomLeft.Y) : 0;
                        this.Element.TopLeft = new Vector2f(w, h);
                        break;
                    case SN_TOPRIGHT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new Vector2f(v_rc.X + v_rc.Width, v_rc.Y));
                        w = (pts.X < 0) ? Math.Min(-pts.X, v_rc.Width - this.Element.TopLeft.X) : 0;
                        h = (pts.Y > 0) ? Math.Min(pts.Y, v_rc.Height - this.Element.BottomRight.Y) : 0;
                        this.Element.m_TopRight = new Vector2f(w, h);
                        break;
                    case SN_BOTTOMLEFT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new Vector2f(v_rc.X, v_rc.Y + v_rc.Height));
                        w = (pts.X > 0) ? Math.Min(pts.X, v_rc.Width - this.Element.BottomRight.X) : 0;
                        h = (pts.Y < 0) ? Math.Min(-pts.Y, v_rc.Height - this.Element.TopLeft.Y) : 0;
                        this.Element.BottomLeft = new Vector2f(w, h);
                        break;
                    case SN_BOTTOMRIGHT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new Vector2f(v_rc.X + v_rc.Width, v_rc.Y + v_rc.Height));
                        w = (pts.X < 0) ? Math.Min(-pts.X, v_rc.Width - this.Element.BottomLeft.X) : 0;
                        h = (pts.Y < 0) ? Math.Min(-pts.Y, v_rc.Height - this.Element.TopRight.Y) : 0;
                        this.Element.BottomRight = new Vector2f(w, h);
                        break;
                }
                this.Element.InitElement();
                this.Invalidate();
                this.InitSnippetsLocation();
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Right:
                        if ((this.Snippet != null) && (this.Element != null))
                        {
                            switch (Snippet.Demand)
                            {
                                case SN_BOTTOMLEFT:
                                case SN_BOTTOMRIGHT:
                                case SN_TOPLEFT:
                                case SN_TOPRIGHT:
                                    InflateAll(e);
                                    return;
                            }
                        }
                        break;
                }
                base.OnMouseMove(e);
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Right:
                        if ((this.Snippet != null) && (this.Element != null))
                        {
                            switch (Snippet.Demand)
                            {
                                case SN_BOTTOMLEFT:
                                case SN_BOTTOMRIGHT:
                                case SN_TOPLEFT:
                                case SN_TOPRIGHT:
                                    this.AllowContextMenu = false;                                    
                                    return;
                            }
                        }
                        else
                            this.AllowContextMenu = false;   
                        break;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseClick(CoreMouseEventArgs e)
            {
                base.OnMouseClick(e);
            }
            public override bool Register(ICore2DDrawingSurface t)
            {
                this.AllowContextMenu = false;
                return base.Register(t);
            }
            public override string ToString()
            {
                return base.ToString();
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Right:
                        if ((this.Snippet != null) && (this.Element != null))
                        {
                            switch (Snippet.Demand)
                            {
                                case SN_BOTTOMLEFT:
                                case SN_BOTTOMRIGHT:
                                case SN_TOPLEFT:
                                case SN_TOPRIGHT:
                                    InflateAll(e);
                                    this.AllowContextMenu = true;
                                    return;
                            }
                        }
                        break;
                }
                base.OnMouseUp(e);
            }
            private void InflateAll(CoreMouseEventArgs e)
            {
                RoundRectangleElement b = this.Element;
                Rectanglef rc = b.Bounds;
                b.Invalidate(false);
                float w = 0.0f;
                float h = 0.0f;
                this.EndPoint = e.FactorPoint;
                Vector2f pts = Vector2f.Zero;
                switch (this.Snippet.Demand)
                {
                    case SN_TOPLEFT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, rc.Location);
                        w = (pts.X > 0) ? Math.Min(pts.X, rc.Width) : 0;
                        h = (pts.Y > 0) ? Math.Min(pts.Y, rc.Height) : 0;
                        break;
                    case SN_TOPRIGHT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new Vector2f(rc.X + rc.Width, rc.Y));
                        w = (pts.X < 0) ? Math.Min(-pts.X, rc.Width) : 0;
                        h = (pts.Y > 0) ? Math.Min(pts.Y, rc.Height) : 0;
                        break;
                    case SN_BOTTOMLEFT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new Vector2f(rc.X, rc.Y + rc.Height));
                        w = (pts.X > 0) ? Math.Min(pts.X, rc.Width) : 0;
                        h = (pts.Y < 0) ? Math.Min(-pts.Y, rc.Height) : 0;
                        break;
                    case SN_BOTTOMRIGHT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new Vector2f(rc.X + rc.Width, rc.Y + rc.Height));
                        w = (pts.X < 0) ? Math.Min(-pts.X, rc.Width) : 0;
                        h = (pts.Y < 0) ? Math.Min(-pts.Y, rc.Height) : 0;
                        break;
                }
                Vector2f v = new Vector2f(w, h);
                b.TopLeft = v;
                b.TopRight = v;
                b.BottomLeft = v;
                b.BottomRight = v;
                RegSnippets[SN_TOPLEFT].Location = (CurrentSurface.GetScreenLocation(new Vector2f(rc.X + w, rc.Y + h)));
                RegSnippets[SN_TOPRIGHT].Location = (CurrentSurface.GetScreenLocation(new Vector2f(rc.X + rc.Width - w, rc.Y + h)));
                RegSnippets[SN_BOTTOMLEFT].Location = (CurrentSurface.GetScreenLocation(new Vector2f(rc.X + w, rc.Y + rc.Height - h)));
                RegSnippets[SN_BOTTOMRIGHT].Location = (CurrentSurface.GetScreenLocation(new Vector2f(rc.X + rc.Width - w, rc.Y + rc.Height - h)));
                b.InitElement();
                this.CurrentSurface.RefreshScene();
            }
            public override void Edit(RoundRectangleElement element)
            {
                base.Edit(element);
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.Actions[enuKeys.T] = new RoundRectangleElementToogleRoundMode(this);
            }
        }
        class RoundRectangleElementToogleRoundMode : CoreMecanismActionBase
        {
            private Mecanism mecanism;
            public RoundRectangleElementToogleRoundMode(Mecanism mecanism)
            {
                this.mecanism = mecanism;
            }

            public override object InitializeLifetimeService()
            {
                return base.InitializeLifetimeService();
            }
           
            protected override bool PerformAction()
            {
                switch (this.mecanism.Element.RoundStyle)
                {
                    case enuRoundStyle.Standard:
                        this.mecanism.Element.RoundStyle = enuRoundStyle.Inner;
                        break;
                    case enuRoundStyle.Inner:
                        this.mecanism.Element.RoundStyle = enuRoundStyle.Flat;
                        break;
                    case enuRoundStyle.Flat:
                        this.mecanism.Element.RoundStyle = enuRoundStyle.Plus;
                        break;
                    case enuRoundStyle.Plus:
                        this.mecanism.Element.RoundStyle = enuRoundStyle.Outer;
                        break;
                    case enuRoundStyle.Outer:
                        this.mecanism.Element.RoundStyle = enuRoundStyle.Standard;
                        break;
                    default:
                        break;
                }
                this.mecanism.Invalidate();
                return true;
            }
            
        }
    }
}

