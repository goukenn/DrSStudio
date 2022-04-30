

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:RoundRectangleElement.cs
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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI ;
    using IGK.DrSStudio.WinUI.Configuration;
    [Core2DDrawingStandardItem("RoundRect",
        typeof(Mecanism),
        Keys = Keys.Shift | Keys.R)]
    public class RoundRectangleElement : 
        Core2DDrawingDualBrushBoundElement 
    {
        private Vector2f m_TopLeft;
        private Vector2f m_TopRight;
        private Vector2f m_BottomLeft;
        private Vector2f m_BottomRight;
        private enuRoundStyle m_RoundStyle;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue (enuRoundStyle .Standard )]
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
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public Vector2f BottomRight
        {
            get { return m_BottomRight; }
            set
            {
                if (!m_BottomRight.Equals (value))
                {
                    m_BottomRight = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Vector2f BottomLeft
        {
            get { return m_BottomLeft; }
            set
            {
                if (!m_BottomLeft.Equals (value))
                {
                    m_BottomLeft = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Vector2f TopRight
        {
            get { return m_TopRight; }
            set
            {
                if (!(m_TopRight.Equals ( value)))
                {
                    m_TopRight = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Vector2f TopLeft
        {
            get { return m_TopLeft; }
            set
            {
                if (!m_TopLeft.Equals ( value))
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
        public static RoundRectangleElement Create(Rectangle rc, float radius)
        {
            RoundRectangleElement v_rc = new RoundRectangleElement();
            v_rc.Bound = rc;
            v_rc.m_BottomLeft = new Vector2f(radius, radius);
            v_rc.m_BottomRight = new Vector2f(radius, radius);
            v_rc.m_TopLeft = new Vector2f(radius, radius);
            v_rc.m_TopRight = new Vector2f(radius, radius);
            v_rc.InitElement();
            return v_rc;
        }
        protected override void GeneratePath()
        {
            switch (RoundStyle)
            {
                case enuRoundStyle.Inner:
                    BuildInverRoundPath();
                    break;
                case enuRoundStyle.Flat:
                    BuildFlatRoundPath();
                    break;
                case enuRoundStyle.Plus:
                    BuildPlusPath();
                    break;
                case enuRoundStyle .Outer :
                    BuildOuterPath();
                    break;
                case enuRoundStyle.Standard:
                default:
                    BuildRoundPath();                    
                    break;
            }
        }
        private void BuildOuterPath()
        {
            float vtl_dx = Math.Max(this.m_TopLeft.X * 2, 0.1f);
            float vtl_dy = Math.Max(this.m_TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(this.m_TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(this.m_TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(this.m_BottomRight.X * 2, 0.1f);
            float vbr_dy = Math.Max(this.m_BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(this.m_BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(this.m_BottomLeft.Y * 2, 0.1f);
            RectangleF v_rect = this.Bound;
            CoreGraphicsPath path = new CoreGraphicsPath();
            path.AddArc(new RectangleF(
                new PointF(v_rect.X - vtl_dx/2.0f, v_rect.Y - vtl_dy/2.0f),
                new SizeF(vtl_dx, vtl_dy)), 90, 270.0f);
            path.AddArc(new RectangleF(
                new PointF(v_rect.X + v_rect.Width - vtr_dx/2.0f,
                    v_rect.Y - vtr_dy /2.0f),
                new SizeF(vtr_dx, vtr_dy)),
                180.0f, 270.0f);
            path.AddArc(new RectangleF(
                new PointF(v_rect.X + v_rect.Width - vbr_dx/2.0f, v_rect.Y + v_rect.Height - vbr_dy/2.0f),
                new SizeF(vbr_dx, vbr_dy)), -90.0f, 270.0f);
            path.AddArc(
                new RectangleF(
                    new PointF(v_rect.X- vbl_dx /2.0f, v_rect.Y + v_rect.Height - vbl_dy/2.0f), 
                    new SizeF(vbl_dx, vbl_dy)), 0.0f, 270.0f);
            path.CloseFigure();
            base.SetPath(path);
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters  = base.GetParameters(parameters);
            IGK.DrSStudio.WinUI.ICoreParameterGroup group =  parameters.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("RoundStyle"));
            string defv = string.Empty;
            if (m_BottomLeft.Equals(m_BottomRight) && m_BottomRight.Equals(m_TopLeft)
                && m_TopLeft.Equals(m_TopRight))
            {
                defv = string.Format("{0} {1};", ((CoreUnit)m_TopRight.X),
                    ((CoreUnit)m_TopRight.Y));
            }
            else {
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
                defv ,
                 enuParameterType .Text ,
                new CoreParameterChangedEventHandler ( _ParamRadiusChanged)); 
            return parameters;
        }
        void _ParamRadiusChanged(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs  e)
        {
            string c = Convert.ToString(e.Value);
            if (string.IsNullOrEmpty(c))
                return;
            string[] d = c.Split(new char[]{';', ' '}, StringSplitOptions.RemoveEmptyEntries);
            List<CoreUnit > m_unit = new List<CoreUnit> ();
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
                    this.SetAllRadius (
                        m_unit[0],
                        m_unit[1]);
                    break;
                case 4:
                    SetRadius (
                        m_unit[0],
                        m_unit[1],
                        m_unit[2],
                        m_unit[3]);
                    break ;
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
            float y = ((ICoreUnitPixel)topr ).Value;
            float x = ((ICoreUnitPixel)bottoml ).Value;
            float z = ((ICoreUnitPixel)bottomr ).Value;
            m_BottomLeft = new Vector2f(x, x);
            m_BottomRight = new Vector2f(z, z);
            m_TopRight = new Vector2f(y, y);
            m_TopLeft = new Vector2f(m, m);
            this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        public void SetAllRadius(CoreUnit rx, CoreUnit ry)
        {
            float m =(( ICoreUnitPixel) rx ).Value ;
            float y = ((ICoreUnitPixel)ry).Value;
            m_BottomLeft = new Vector2f(m, y);
            m_BottomRight = new Vector2f(m, y);
            m_TopRight = new Vector2f(m, y);
            m_TopLeft = new Vector2f(m, y);
            this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        private void BuildPlusPath()
        {
            float vtl_dx = Math.Max(this.TopLeft.X, 0.1f);
            float vtl_dy = Math.Max(this.TopLeft.Y, 0.1f);
            float vtr_dx = Math.Max(this.TopRight.X, 0.1f);
            float vtr_dy = Math.Max(this.TopRight.Y, 0.1f);
            float vbr_dx = Math.Max(this.BottomRight.X, 0.1f);
            float vbr_dy = Math.Max(this.BottomRight.Y, 0.1f);
            float vbl_dx = Math.Max(this.BottomLeft.X, 0.1f);
            float vbl_dy = Math.Max(this.BottomLeft.Y, 0.1f);
            RectangleF v_rect = this.Bound;
            CoreGraphicsPath path = new CoreGraphicsPath();
            PointF[] pts = new PointF[12];
            pts[0] = new PointF(v_rect.X, v_rect.Y + vtl_dy);
            pts[1] = new PointF(v_rect.X + vtl_dx, v_rect.Y + vtl_dy);
            pts[2] = new PointF(v_rect.X + vtl_dx, v_rect.Y);
            pts[3] = new PointF(v_rect.X + v_rect.Width - vtr_dx, v_rect.Y);
            pts[4] = new PointF(v_rect.X + v_rect.Width - vtr_dx, v_rect.Y + vtr_dy);
            pts[5] = new PointF(v_rect.X + v_rect.Width, v_rect.Y + vtr_dy);
            pts[6] = new PointF(v_rect.X + v_rect.Width, v_rect.Y + v_rect.Height - vbr_dy);
            pts[7] = new PointF(v_rect.X + v_rect.Width - vbr_dx, v_rect.Y + v_rect.Height - vbr_dy);
            pts[8] = new PointF(v_rect.X + v_rect.Width - vbr_dx, v_rect.Y + v_rect.Height);
            pts[9] = new PointF(v_rect.X + vbl_dx, v_rect.Y + v_rect.Height);
            pts[10] = new PointF(v_rect.X + vbl_dx, v_rect.Y + v_rect.Height - vbl_dy);
            pts[11] = new PointF(v_rect.X, v_rect.Y + v_rect.Height - vbl_dy);
            path.AddPolygon(pts);
            path.CloseFigure();
            base.SetPath(path);
        }
        private void BuildFlatRoundPath()
        {
            CoreGraphicsPath path = new CoreGraphicsPath();
            float vtl_dx = Math.Max(this.TopLeft.X * 2, 0.1f);
            float vtl_dy = Math.Max(this.TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(this.TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(this.TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(this.BottomRight.X * 2, 0.1f);
            float vbr_dy = Math.Max(this.BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(this.BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(this.BottomLeft.Y * 2, 0.1f);
            RectangleF v_rect = this.Bound;
            //top left
            path.AddPolygon(
            new PointF[]{
                //top left
                new PointF(
                v_rect.X,
                v_rect.Y + this.TopLeft.Y),
                new PointF(
                v_rect.X + this.TopLeft.X,
                v_rect.Y ),
                //top - right
                new PointF (
                v_rect.X + v_rect.Width - this.TopRight.X,
                v_rect.Y ),
                new PointF (
                v_rect.X + v_rect.Width,
                v_rect.Y + this.TopRight.Y),
                //bottom- right
                new PointF (
                v_rect.X + v_rect.Width ,
                v_rect.Y + v_rect.Height - this.BottomRight.Y),
                new PointF (
                v_rect.X + v_rect.Width - this.BottomRight.X,
                v_rect.Y + v_rect.Height),
                //bottom -left
                new PointF(
                v_rect.X + this.BottomLeft.X,
                v_rect.Y + v_rect.Height),
                new PointF(
                v_rect.X ,
                v_rect.Y + v_rect.Height - this.BottomLeft.Y)
            });
            path.CloseFigure();
            base.SetPath(path);
        }
        private void BuildInverRoundPath()
        {
            float vtl_dx = Math.Max(this.TopLeft.X * 2, 0.1f);
            float vtl_dy = Math.Max(this.TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(this.TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(this.TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(this.BottomRight.X * 2, 0.1f);
            float vbr_dy = Math.Max(this.BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(this.BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(this.BottomLeft.Y * 2, 0.1f);
            RectangleF v_rect = this.Bound;
            CoreGraphicsPath path = new CoreGraphicsPath();
            //top left
            path.AddArc(new RectangleF(
                v_rect.X - this.TopLeft.X,
                v_rect.Y - this.TopLeft.Y,
                vtl_dx, vtl_dy),
                90.0f, -90.0f);
            //top right
            path.AddArc(new RectangleF(
                v_rect.X + v_rect.Width - this.TopRight.X,
                v_rect.Y - this.TopRight.Y,
                vtr_dx, vtr_dy),
                -180.0f, -90.0f);
            //bottom right
            path.AddArc(new RectangleF(
                v_rect.X + v_rect.Width - this.BottomRight.X,
                v_rect.Y + v_rect.Height - this.BottomRight.Y,
                vbr_dx, vbr_dy),
                -90.0f, -90.0f);
            //bottom left
            path.AddArc(new RectangleF(
                v_rect.X - this.BottomLeft.X,
                v_rect.Y + v_rect.Height - this.BottomLeft.Y,
                vbl_dx,
                vbl_dy),
                0.0f, -90.0f);
            path.CloseFigure();
            base.SetPath(path);
        }
        private void BuildRoundPath()
        {
            float vtl_dx = Math.Max(this.m_TopLeft .X * 2, 0.1f);
            float vtl_dy = Math.Max(this.m_TopLeft.Y * 2, 0.1f);
            float vtr_dx = Math.Max(this.m_TopRight.X * 2, 0.1f);
            float vtr_dy = Math.Max(this.m_TopRight.Y * 2, 0.1f);
            float vbr_dx = Math.Max(this.m_BottomRight .X * 2, 0.1f);
            float vbr_dy = Math.Max(this.m_BottomRight.Y * 2, 0.1f);
            float vbl_dx = Math.Max(this.m_BottomLeft.X * 2, 0.1f);
            float vbl_dy = Math.Max(this.m_BottomLeft.Y * 2, 0.1f);
            RectangleF v_rect = this.Bound;
            CoreGraphicsPath path = new CoreGraphicsPath();
            path.AddArc(new RectangleF(v_rect.Location, new SizeF(vtl_dx, vtl_dy)), 180.0f, 90.0f);
            path.AddArc(new RectangleF(new PointF(v_rect.X + v_rect.Width - vtr_dx, v_rect.Y),
                new SizeF(vtr_dx, vtr_dy)), -90.0f, 90.0f);
            path.AddArc(new RectangleF(new PointF(v_rect.X + v_rect.Width - vbr_dx, v_rect.Y + v_rect.Height - vbr_dy),
                new SizeF(vbr_dx, vbr_dy)), 0.0f, 90.0f);
            path.AddArc(new RectangleF(new PointF(v_rect.X, v_rect.Y + v_rect.Height - vbl_dy), new SizeF(vbl_dx, vbl_dy)), 90.0f, 90.0f);
            path.CloseFigure();
            base.SetPath(path);
        }
        protected new class Mecanism : Core2DDrawingDualBrushBoundElement.Mecanism
        {
            const int SN_TOPLEFT = 10;
            const int SN_TOPRIGHT= 11;
            const int SN_BOTTOMRIGHT = 12;
            const int SN_BOTTOMLEFT = 13;
            public new RoundRectangleElement Element {
                get {
                    return base.Element as RoundRectangleElement ;
                }
                set {
                    base.Element = value;
                }
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.Element != null)
                {
                    Rectanglef v_rc = this.Element.Bound ;
                    RegSnippets[SN_TOPLEFT].Location =
                        CurrentSurface.GetScreenLocation(
                        v_rc.Location + this.Element.TopLeft);
                    RegSnippets[SN_TOPRIGHT].Location =
                        CurrentSurface.GetScreenLocation(
                        new Vector2f(v_rc.Right , v_rc.Y )+
                        new Vector2f(-this.Element.TopRight.X  ,
                            this.Element.TopRight.Y ));
                    RegSnippets[SN_BOTTOMRIGHT ].Location =
                        CurrentSurface.GetScreenLocation(
                        new Vector2f(v_rc.Right, v_rc.Bottom ) - this.Element.BottomRight);
                    RegSnippets[SN_BOTTOMLEFT].Location =
     CurrentSurface.GetScreenLocation(
     new Vector2f(v_rc.X, v_rc.Bottom) + new Vector2f (this.Element.BottomLeft.X , 
         -this.Element.BottomLeft.Y )
         );
                }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element != null)
                {                    
                    AddSnippet(CurrentSurface.CreateSnippet(this, SN_TOPLEFT, SN_TOPLEFT));
                    AddSnippet(CurrentSurface.CreateSnippet(this, SN_TOPRIGHT, SN_TOPRIGHT ));
                    AddSnippet(CurrentSurface.CreateSnippet(this, SN_BOTTOMRIGHT , SN_BOTTOMRIGHT ));
                    AddSnippet(CurrentSurface.CreateSnippet(this, SN_BOTTOMLEFT, SN_BOTTOMLEFT ));
                    this.RegSnippets[SN_TOPLEFT].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                    this.RegSnippets[SN_TOPRIGHT ].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                    this.RegSnippets[SN_BOTTOMLEFT ].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                    this.RegSnippets[SN_BOTTOMRIGHT].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                }
            }
            protected override void UpdateSnippetElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                RectangleF rc = this.Element.Bound;
                float w = 0.0f;
                float h = 0.0f;
                PointF pts = PointF.Empty;
                switch (this.Snippet.Demand )
                { 
                    case SN_TOPLEFT :
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, rc.Location);
                        w = (pts.X > 0) ? Math.Min(pts.X, rc.Width - this.Element.TopRight .X) : 0;
                        h = (pts.Y > 0) ? Math.Min(pts.Y, rc.Height - this.Element.BottomLeft.Y) : 0;
                        this.Element.TopLeft = new Vector2f (w, h);                                          
                        break;
                    case SN_TOPRIGHT :
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new PointF(rc.X + rc.Width, rc.Y));
                        w = (pts.X < 0) ? Math.Min(-pts.X, rc.Width - this.Element.TopLeft.X) : 0;
                        h = (pts.Y > 0) ? Math.Min(pts.Y, rc.Height - this.Element.BottomRight.Y) : 0;
                        this.Element.m_TopRight = new Vector2f(w, h);
                        break;
                    case SN_BOTTOMLEFT :
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new PointF(rc.X, rc.Y + rc.Height));
                        w = (pts.X > 0) ? Math.Min(pts.X, rc.Width - this.Element.BottomRight.X) : 0;
                        h = (pts.Y < 0) ? Math.Min(-pts.Y, rc.Height - this.Element.TopLeft.Y) : 0;
                        this.Element.BottomLeft = new Vector2f(w, h);                                         
                        break;
                    case SN_BOTTOMRIGHT :
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new PointF(rc.X + rc.Width, rc.Y + rc.Height));
                        w = (pts.X < 0) ? Math.Min(-pts.X, rc.Width - this.Element.BottomLeft.X) : 0;
                        h = (pts.Y < 0) ? Math.Min(-pts.Y, rc.Height - this.Element.TopRight.Y) : 0;
                        this.Element.BottomRight = new Vector2f(w, h);
                        break;
                }
                base.UpdateSnippetElement(e);
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Right :
                        if ((this.Snippet != null) && (this.Element !=null))
                        {
                            switch (Snippet.Demand)                            
                            { 
                                case SN_BOTTOMLEFT :
                                case SN_BOTTOMRIGHT :
                                case SN_TOPLEFT :
                                case SN_TOPRIGHT :
                                    InflateAll(e);
                                    return;
                            }
                        }
                        break;
                }
                base.OnMouseMove(e);
            }
            protected override void UpdateCreateElement(CoreMouseEventArgs e)
            {
                base.UpdateCreateElement(e);
            }
            private void InflateAll(CoreMouseEventArgs e )
            {
                RoundRectangleElement b = this.Element;
                RectangleF rc = b.Bound;
                b.Invalidate(false);
                float w = 0.0f;
                float h = 0.0f;
                this.EndPoint = e.FactorPoint;
                PointF pts = Point.Empty;
                switch (this.Snippet.Demand)
                {
                    case SN_TOPLEFT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, rc.Location);
                        w = (pts.X > 0) ? Math.Min(pts.X, rc.Width) : 0;
                        h = (pts.Y > 0) ? Math.Min(pts.Y, rc.Height) : 0;
                        break;
                    case SN_TOPRIGHT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new PointF(rc.X + rc.Width, rc.Y));
                        w = (pts.X < 0) ? Math.Min(-pts.X, rc.Width) : 0;
                        h = (pts.Y > 0) ? Math.Min(pts.Y, rc.Height) : 0;
                        break;
                    case SN_BOTTOMLEFT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new PointF(rc.X, rc.Y + rc.Height));
                        w = (pts.X > 0) ? Math.Min(pts.X, rc.Width) : 0;
                        h = (pts.Y < 0) ? Math.Min(-pts.Y, rc.Height) : 0;
                        break;
                    case SN_BOTTOMRIGHT:
                        pts = CoreMathOperation.GetDistanceP(e.FactorPoint, new PointF(rc.X + rc.Width, rc.Y + rc.Height));
                        w = (pts.X < 0) ? Math.Min(-pts.X, rc.Width) : 0;
                        h = (pts.Y < 0) ? Math.Min(-pts.Y, rc.Height) : 0;
                        break;
                }
                Vector2f v = new Vector2f(w, h);
                b.TopLeft = v;
                b.TopRight = v;
                b.BottomLeft = v;
                b.BottomRight = v;
                RegSnippets[SN_TOPLEFT].Location = (CurrentSurface.GetScreenLocation(new PointF(rc.X + w, rc.Y + h)));
                RegSnippets[SN_TOPRIGHT].Location = (CurrentSurface.GetScreenLocation(new PointF(rc.X + rc.Width - w, rc.Y + h)));
                RegSnippets[SN_BOTTOMLEFT].Location = (CurrentSurface.GetScreenLocation(new PointF(rc.X + w, rc.Y + rc.Height - h)));
                RegSnippets[SN_BOTTOMRIGHT].Location = (CurrentSurface.GetScreenLocation(new PointF(rc.X + rc.Width - w, rc.Y + rc.Height - h)));
                b.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                if (!e.Handled)
                {
                    switch (e.KeyChar)
                    {
                        case 't':
                        case 'T':
                            //toggle brush definition
                            if (this.Element != null)
                            {
                                e.Handled = true;
                                switch (this.Element.RoundStyle)
                                {
                                    case enuRoundStyle.Standard:
                                        this.Element.RoundStyle = enuRoundStyle.Inner;
                                        break;
                                    case enuRoundStyle.Inner:
                                        this.Element.RoundStyle = enuRoundStyle.Flat;
                                        break;
                                    case enuRoundStyle.Flat:
                                        this.Element.RoundStyle = enuRoundStyle.Plus;
                                        break;
                                    case enuRoundStyle.Plus:
                                        this.Element.RoundStyle = enuRoundStyle.Outer ;
                                        break;
                                    case enuRoundStyle.Outer :
                                        this.Element.RoundStyle = enuRoundStyle.Standard;
                                        break;
                                    default:
                                        break;
                                }
                                this.CurrentSurface.Invalidate();
                            }
                            break;
                    }
                }
                base.OnKeyPress(e);
            }
        }
    }
}

