

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RubanElement.cs
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
file:RubanElement.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.WinUI;
    [Core2DDrawingStandardElement ("Ruban", 
        typeof (Mecanism),
        Keys = enuKeys.Control | enuKeys.R )]
    public sealed class RubanElement : Core2DDrawingLayeredDualBrushElement ,
        ICore2DFillModeElement ,
        ICore2DClosableElement ,
        ICore2DTensionElement ,
        ICore2DSymbolElement 
    {
        private Vector2f [] m_Points;
        private FillMode m_FillMode;
        private ICore2DDrawingLayeredElement m_CustomRubanStyle;
        private bool m_Closed;
        private float m_Radius;
        private enuRubanPresentation m_RubanPresentation;
        private float m_Orientation;
        private float m_Tension;
        private bool m_EnableTension;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(false)]
        public bool EnableTension
        {
            get { return m_EnableTension; }
            set
            {
                if (m_EnableTension != value)
                {
                    m_EnableTension = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(0.0f)]
        public float Tension
        {
            get { return m_Tension; }
            set
            {
                if (m_Tension != value)
                {
                    m_Tension = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public float Orientation
        {
            get { return m_Orientation; }
            set
            {
                if (m_Orientation != value)
                {
                    m_Orientation = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public enuRubanPresentation RubanPresentation
        {
            get { return m_RubanPresentation; }
            set
            {
                if (m_RubanPresentation != value)
                {
                    m_RubanPresentation = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        /// <summary>
        /// get the ruban radius
        /// </summary>
        public float Radius
        {
            get { return m_Radius; }
            set
            {
                if (m_Radius != value)
                {
                    m_Radius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        private enuRubanStyle m_RubanStyle;
        public RubanElement()
        {
            this.m_RubanStyle = enuRubanStyle.Curve;
            this.m_Radius = 1.0f;
            this.m_EnableTension = false;
            this.m_Tension = 0.0f;
            this.m_RubanPresentation = enuRubanPresentation.Default;
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue (enuRubanStyle .Curve )]
        public enuRubanStyle RubanStyle
        {
            get { return m_RubanStyle; }
            set
            {
                if (m_RubanStyle != value)
                {
                    m_RubanStyle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix v_m = this.GetMatrix();
            if (v_m.IsIdentity)
                return;
           this.m_Points =  CoreMathOperation.TransformVector2fPoint (v_m, m_Points);
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        //[IGK.DrSStudio.Codec.CoreXMLElement()]
        /// <summary>
        /// get or set the element motif
        /// </summary>
        public ICore2DDrawingLayeredElement CustomRubanStyle
        {
            get { return m_CustomRubanStyle; }
            set
            {
                if (m_CustomRubanStyle != value)
                {
                    m_CustomRubanStyle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public FillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        [System.ComponentModel.TypeConverter (typeof (Vector2f .Vector2fArrayTypeConverter ))]
        public Vector2f [] Points
        {
            get { return m_Points; }
            set
            {
                if (m_Points != value)
                {
                    m_Points = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            if ((this.CustomRubanStyle !=null) && (this.RubanStyle == enuRubanStyle.Custom ))
            {
                xwriter.WriteStartElement("CustomRubanStyle");
                this.CustomRubanStyle.Serialize(xwriter);
                xwriter.WriteEndElement();
            }
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadElements(this, xreader, customPROC);
        }
        bool customPROC(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            if (xreader.Name == "CustomRubanStyle")
            {
                string s = xreader.ReadInnerXml();
                ICoreWorkingObject[] t = IGK.DrSStudio.Codec.CoreXMLSerializerUtility.DeserializeWorkingObject(s);
                if (t.Length == 1)
                {
                    this.CustomRubanStyle = t[0] as ICore2DDrawingLayeredElement;
                }
            }
            return true;
        }
        protected override void GeneratePath()
        {
            if ((this.m_Points == null) || (m_Points.Length < 2))
            {
                this.SetPath(null);
                return;
            }
            PointF[] v_tp =  BuildPoints();
            int v_square = 0;
            GraphicsPath vp = new GraphicsPath();
            switch (this.m_RubanStyle)
            {
                case enuRubanStyle.Quads:
                    for (int i = 0; i < v_tp.Length - 3; i += 2)
                    {
                        vp.SetMarkers();
                        vp.AddPolygon(
                            new PointF[]{
                                v_tp[i +   v_square ],                                
                                v_tp[i+1 - v_square ],
                                v_tp[i+2],                                
                                v_tp[i+3]
                            });
                        if (v_square != 1)
                            v_square = 1;
                    }
                    break;
                case enuRubanStyle.Polygon:
                    for (int i = 0; i < v_tp.Length - 3; i += 2)
                    {
                        vp.SetMarkers();
                        SplitAdd(vp,
                           new PointF[]{
                                v_tp[i +   v_square ],                                
                                v_tp[i+1 - v_square ],
                                v_tp[i+2],                                
                                v_tp[i+3]
                            });
                        if (v_square != 1)
                            v_square = 1;
                    }
                    break;
                case enuRubanStyle.Triangle:
                    for (int i = 0; i < v_tp.Length - 3; i += 2)
                    {
                        vp.SetMarkers();
                        vp.AddPolygon(
                          new PointF[]{
                                v_tp[i +   v_square ],                                
                                v_tp[i+1 - v_square ],
                                v_tp[i+2], 
                            });
                        if (v_square != 1)
                            v_square = 1;
                    }
                    break;
                case enuRubanStyle.Rectangle:
                    for (int i = 0; i < v_tp.Length - 3; i += 2)
                    {
                        vp.SetMarkers();
                        vp.AddRectangle(CoreMathOperation.GetBounds(
                        new PointF[]{
                                  v_tp[i +   v_square ],                                
                                v_tp[i+1 - v_square ],
                                v_tp[i+2],                                
                                v_tp[i+3]}));
                        if (v_square != 1)
                            v_square = 1;
                    }
                    break;
                case enuRubanStyle.Ellipse:
                    for (int i = 0; i < v_tp.Length - 3; i += 2)
                    {
                        vp.SetMarkers();
                        vp.AddEllipse(CoreMathOperation.GetBounds(
                                                new PointF[]{
                                  v_tp[i +   v_square ],                                
                                v_tp[i+1 - v_square ],
                                v_tp[i+2],                                
                                v_tp[i+3]}));
                        if (v_square != 1)
                            v_square = 1;
                    }
                    break;
                case enuRubanStyle.Path:
                case enuRubanStyle.Curve:
                default:
                    if (this.Closed)
                    {
                        PointF[] t  = new PointF[v_tp .Length +1];
                        Array.Copy (v_tp , t, v_tp.Length );
                        t[t.Length - 1] = t[0];
                        if (this.EnableTension)
                        {
                            vp.AddClosedCurve(t, this.Tension);
                        }
                        else 
                            vp.AddClosedCurve(t);
                    }
                    else
                    {
                        if (this.EnableTension)
                            vp.AddCurve(v_tp, this.Tension);
                        else
                        {
                            vp.AddCurve(v_tp);
                        }
                    }
                    break;
                case enuRubanStyle .Custom :
                    if (this.CustomRubanStyle == null)
                        goto case enuRubanStyle.Path;
                    GraphicsPath cvp = this.CustomRubanStyle.GetPath ();
                    if (cvp !=null)
                    {
                        cvp = cvp.Clone () as GraphicsPath ;
                    for (int i = 0; i < v_tp.Length - 3; i += 2)
                    {
                        vp.SetMarkers();
                        CoreMathOperation.DockPath(cvp , 
                            CoreMathOperation.GetBounds(
                                                new PointF[]{
                                  v_tp[i +   v_square ],                                
                                v_tp[i+1 - v_square ],
                                v_tp[i+2],                                
                                v_tp[i+3]}));
                        vp.AddPath(cvp, false );
                        if (v_square != 1)
                            v_square = 1;
                    }
                        cvp.Dispose ();
                    }
                    break;
            }
            vp.FillMode = this.FillMode;
            this.SetPath(vp);
        }
        private PointF[] BuildPoints()
        {
            float v_radius = this.m_Radius;
            float v_c_angle = (float)Math.PI / 1.0F;
            float v_angle = 0.0f;
            List<PointF> v_lpoints = new List<PointF>();
            if (v_angle > 0)
                v_angle = (float)(2 * Math.PI + v_angle);
            v_c_angle = v_angle / v_c_angle;
            PointF v_stP = PointF.Empty;
            PointF v_enP = PointF.Empty;
            // v_radius  *= v_c_angle;
            PointF pt1 = Point.Empty;
            PointF pt2 = Point.Empty;
            PointF pt3 = Point.Empty;
            PointF pt4 = Point.Empty;
            //alrogith3
            //nextangle 
            float v_nextAngle = 0.0f;
            float v_corientation = (m_Orientation * CoreMathOperation.ConvDgToRadian);
            for (int i = 0; i < this.m_Points.Length - 1; i++)
            {
                v_stP = this.m_Points[i];
                v_enP = this.m_Points[i + 1];
                switch (this.RubanPresentation)
                {
                    case enuRubanPresentation.OnlyOneAngle:
                        if (i == 0)
                        {
                            v_angle = (float)(CoreMathOperation.GetAngle(v_stP, v_enP) + v_corientation);
                        }
                        break;
                    case enuRubanPresentation.FollowPreviousAngle:
                        if (i == 0)
                        {
                            v_angle = (float)(CoreMathOperation.GetAngle(v_stP, v_enP) + v_corientation);
                            v_nextAngle
                                  = (float)(CoreMathOperation.GetAngle(
                                  new PointF(
                                        (float)(v_stP.X - v_radius * Math.Cos(v_angle)),
                                        (float)(v_stP.Y - v_radius * Math.Sin(v_angle))),
                                  new PointF(
                                        (float)(v_stP.X + v_radius * Math.Cos(v_angle)),
                                        (float)(v_stP.Y + v_radius * Math.Sin(v_angle)))) + v_corientation);
                        }
                        else
                        {
                            v_angle = v_nextAngle;
                            v_nextAngle = (float)(
                                CoreMathOperation.GetAngle(
                                v_stP, v_enP
                                //v_lpoints[v_lpoints.Count - 1],
                                //v_lpoints[v_lpoints.Count - 2]
                                ) + v_corientation + Math.PI / 6.0f);
                        }
                        break;
                    case enuRubanPresentation.FollowCurrentAngle:
                        if (i == 0)
                        {
                            v_angle = (float)(CoreMathOperation.GetAngle(v_stP, v_enP) + (v_corientation));
                        }
                        else
                        {
                            v_angle = (float)(
                                CoreMathOperation.GetAngle(
                                v_lpoints[v_lpoints.Count - 1],
                                v_lpoints[v_lpoints.Count - 2]
                                ) + (v_corientation));
                        }
                        break;
                    case enuRubanPresentation.Default:
                        v_angle = (float)(CoreMathOperation.GetAngle(v_stP, v_enP) + v_corientation);
                        break;
                }
                if (v_lpoints.Count == 0)
                {
                    pt1 = new PointF(
                        (float)(v_stP.X - v_radius * Math.Cos(v_angle)),
                        (float)(v_stP.Y - v_radius * Math.Sin(v_angle)));
                    pt2 = new PointF(
                        (float)(v_stP.X + v_radius * Math.Cos(v_angle)),
                        (float)(v_stP.Y + v_radius * Math.Sin(v_angle)));
                }
                else
                {
                    pt1 = v_lpoints[v_lpoints.Count - 2];
                    pt2 = v_lpoints[v_lpoints.Count - 2];
                }
                pt3 = new PointF(
                    (float)(v_enP.X + v_radius * Math.Cos(v_angle)),
                    (float)(v_enP.Y + v_radius * Math.Sin(v_angle)));
                pt4 = new PointF(
                    (float)(v_enP.X - v_radius * Math.Cos(v_angle)),
                    (float)(v_enP.Y - v_radius * Math.Sin(v_angle)));
                if (v_lpoints.Count == 0)
                {
                    v_lpoints.Add(pt1);
                    v_lpoints.Add(pt2);
                }
                v_lpoints.Add(pt3);
                v_lpoints.Add(pt4);
            }
            return v_lpoints.ToArray();
        }
        private void SplitAdd(GraphicsPath path, PointF[] pointF)
        {
            path.AddPolygon(new PointF[] { 
                pointF[0],
                pointF[1],
                pointF[3],
            });
            path.AddPolygon(new PointF[] { 
                pointF[3],
                pointF[1],
                pointF[2],
            });
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            base.GetParameters(parameters);
            IGK.DrSStudio.WinUI.ICoreParameterGroup group =  parameters.AddGroup("Properties");
            group.AddItem(GetType().GetProperty("FillMode"));
            group.AddItem(GetType().GetProperty("Closed"));
            group.AddItem(GetType().GetProperty("EnableTension"));
            group.AddItem(GetType().GetProperty("Tension"));
            group = parameters.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("Radius"));
            group.AddItem(GetType().GetProperty("Orientation"));
            group.AddItem(GetType().GetProperty("RubanStyle"));
            group.AddItem(GetType().GetProperty("RubanPresentation"));
            return parameters;
        }
        /// <summary>
        /// represent the class mecanism for ruban
        /// </summary>
        new class Mecanism : Core2DDrawingLayeredDualBrushElement .Mecanism
        {
            private List<Vector2f> m_lPoints;
            public Mecanism()
            {
                this.m_Step = 3;
            }
            private float m_Step;
            /// <summary>
            /// represent the radial step
            /// </summary>
            public float Step
            {
                get { return m_Step; }
                set
                {
                    if (m_Step != value)
                    {
                        m_Step = value;
                    }
                }
            }
            public new RubanElement Element
            {
                get { return base.Element as RubanElement ; }
                set
                {
                    base.Element = value;
                }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element != null)
                {
                    Vector2f [] v_t = this.Element.m_Points;
                    for (int i = 0; i < v_t.Length; i++)
                    {
                        this.AddSnippet(this.CurrentSurface.CreateSnippet(this, i, i));
                    }
                }
            }
            protected override void InitSnippetsLocation()
            {
                if (this.Element != null)
                {
                    Vector2f[] v_t = this.Element.m_Points;
                    for (int i = 0; i < v_t.Length; i++)
                    {
                        this.RegSnippets[i].Location = this.CurrentSurface.GetScreenLocation(v_t[i]);
                    }
                }
            }
            protected override void InitNewCreateElement(ICore2DDrawingElement element)
            {
                base.InitNewCreateElement(element);
                if (this.m_lPoints == null)
                    this.m_lPoints = new List<Vector2f> ();
                this.m_lPoints.Clear();
            }
            protected override void UpdateDrawing(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                if (this.m_lPoints.Count > 0)
                {
                    //disallow the possibility to add element that match the target
                    if (this.m_lPoints[this.m_lPoints.Count - 1].Equals(e.FactorPoint))
                        return;
                    if (CoreMathOperation.GetDistance(e.FactorPoint,
                        this.m_lPoints[this.m_lPoints.Count - 1]) < this.m_Step )
                        return;
                }
                this.m_lPoints.Add(e.FactorPoint);
                this.Element.m_Points = this.m_lPoints.ToArray();
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            const int GET_RADIUS = -100;
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                if ((this.IsShiftKey) &&
                    (e.Button == MouseButtons.Left) &&
                    (this.Element != null))
                {
                    this.State = GET_RADIUS;
                    this.Element.m_Radius = 0.0f;
                    this.StartPoint = e.FactorPoint;
                    this.EndPoint = e.FactorPoint;
                    this.Element.InitElement();
                    this.CurrentSurface.Invalidate();
                    return ;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                if (this.State == GET_RADIUS)
                {
                    if (this.IsShiftKey)
                    {
                        this.State = GET_RADIUS;
                        this.Element.m_Radius = CoreMathOperation.GetDistance(e.FactorPoint, this.StartPoint);
                        this.Element.InitElement();
                        this.CurrentSurface.Invalidate();
                    }
                    else {
                        this.State = ST_EDITING;
                    }
                    this.CurrentSurface.Invalidate();
                    return;
                }
                base.OnMouseMove(e);
            }
            protected override void UpdateSnippetEdit(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                if (this.Element == null)
                    return;
                Vector2f[] t = this.Element.m_Points;
                t[this.Snippet.Index] = e.FactorPoint;
                this.Snippet.Location = e.Location;
                this.Element.InitElement();
                this.CurrentSurface .Invalidate();
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                if ((this.State == GET_RADIUS)
                    &&    (e.Button == MouseButtons.Left) &&
                    (this.Element != null))
                {
                    this.State = GET_RADIUS;
                    this.Element.m_Radius = CoreMathOperation.GetDistance(e.FactorPoint, this.StartPoint);
                    this.Element.InitElement();
                    this.State = ST_EDITING;
                    return;
                }
                switch (e.Button)
                {
                    case MouseButtons .Right :                    
                        //remove point, end edition or go to default tool
                        if (this.Element != null)
                        {
                            if (this.Snippet != null)
                                this.RemovePoint();
                            else
                                EndEdition();
                            return;
                        }
                        return;
                }
                base.OnMouseUp(e);
            }
            private void RemovePoint()
            {
                List<Vector2f> v_p = new List<Vector2f>();
                v_p.AddRange(this.Element.Points);
                if (v_p.Count <=2  )
                    return;
                v_p.RemoveAt(this.Snippet.Index);
                this.Element.m_Points = v_p.ToArray();
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
                this.GenerateSnippets();
                this.InitSnippetsLocation();
            }
        }
        #region ICore2DSymbolElement Members
        ICore2DDrawingLayeredElement ICore2DSymbolElement .SymbolItem
        {
            get
            {
                return this.CustomRubanStyle ;
            }
            set
            {
                if ((this.CustomRubanStyle != value) && (this.RubanStyle == enuRubanStyle.Custom ))
                {
                    this.CustomRubanStyle = value;
                    this.InitElement();
                }
            }
        }
        #endregion
    }
}

