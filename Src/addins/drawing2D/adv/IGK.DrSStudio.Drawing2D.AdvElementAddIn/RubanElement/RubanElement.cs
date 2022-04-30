

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
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
using System; 
using IGK.ICore;  using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    [Core2DDrawingStandardElement ("Ruban", 
        typeof (Mecanism),
        Keys = enuKeys.Control | enuKeys.R )]
    public sealed class RubanElement : Core2DDrawingDualBrushElement ,
        ICore2DFillModeElement ,
        ICore2DClosableElement ,
        ICore2DTensionElement ,
        ICore2DSymbolElement 
    {
        private Vector2f [] m_Points;
        private enuFillMode m_FillMode;
        private ICore2DDrawingLayeredElement m_CustomRubanStyle;
        private bool m_Closed;
        private float m_Radius;
        private enuRubanPresentation m_RubanPresentation;
        private float m_Orientation;
        private float m_Tension;
        private bool m_EnableTension;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
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
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
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
        [CoreXMLAttribute()]
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
        [CoreXMLAttribute()]
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
        [CoreXMLAttribute()]
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
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (enuRubanStyle .Curve )]
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
        [CoreXMLAttribute()]
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
        //[CoreXMLElement()]
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
        [CoreXMLAttribute()]
        public enuFillMode FillMode
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
        [CoreXMLAttribute ()]
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
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            if ((this.CustomRubanStyle !=null) && (this.RubanStyle == enuRubanStyle.Custom ))
            {
                xwriter.WriteStartElement("CustomRubanStyle");
                this.CustomRubanStyle.Serialize(xwriter);
                xwriter.WriteEndElement();
            }
        }
        protected override void ReadElements(IXMLDeserializer xreader)
        {
            CoreXMLSerializerUtility.ReadElements(this, xreader, customPROC);
        }
        bool customPROC(IXMLDeserializer xreader)
        {
            if (xreader.Name == "CustomRubanStyle")
            {
                string s = xreader.ReadInnerXml();
                ICoreWorkingObject[] t = CoreXMLSerializerUtility.GetAllObjects(s);
                if (t.Length == 1)
                {
                    this.CustomRubanStyle = t[0] as ICore2DDrawingLayeredElement;
                }
            }
            return true;
        }
        protected override void InitGraphicPath(CoreGraphicsPath vp)
        {
            vp.Reset();
            
            if ((this.m_Points == null) || (m_Points.Length < 2))
            {
                return;
            }
            Vector2f[] v_tp =  BuildPoints();
            int v_square = 0;
            switch (this.m_RubanStyle)
            {
                case enuRubanStyle.Quads:
                    for (int i = 0; i < v_tp.Length - 3; i += 2)
                    {
                        vp.SetMarkers();
                        vp.AddPolygon(
                            new Vector2f[]{
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
                           new Vector2f[]{
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
                          new Vector2f[]{
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
                        new Vector2f[]{
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
                                                new Vector2f[]{
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
                        Vector2f[] t  = new Vector2f[v_tp .Length +1];
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
                            vp.AddCurve(v_tp, this.Tension, this.Closed);
                        else
                        {
                            vp.AddCurve(v_tp);
                        }
                    }
                    break;
                case enuRubanStyle .Custom :
                    if (this.CustomRubanStyle == null)
                        goto case enuRubanStyle.Path;
                    ICoreGraphicsPath cvp = this.CustomRubanStyle.GetPath ();
                    if (cvp !=null)
                    {
                        cvp = cvp.Clone () as CoreGraphicsPath ;
                    for (int i = 0; i < v_tp.Length - 3; i += 2)
                    {
                        vp.SetMarkers();
                       CoreMathOperation.DockPath(cvp , 
                            CoreMathOperation.GetBounds(
                                                new Vector2f[]{
                                  v_tp[i +   v_square ],                                
                                v_tp[i+1 - v_square ],
                                v_tp[i+2],                                
                                v_tp[i+3]}));
                       vp.Add(cvp);
                        if (v_square != 1)
                            v_square = 1;
                    }
                        cvp.Dispose ();
                    }
                    break;
            }
            vp.FillMode = this.FillMode;
        }
        private Vector2f[] BuildPoints()
        {
            float v_radius = this.m_Radius;
            float v_c_angle = (float)Math.PI / 1.0F;
            float v_angle = 0.0f;
            List<Vector2f> v_lpoints = new List<Vector2f>();
            if (v_angle > 0)
                v_angle = (float)(2 * Math.PI + v_angle);
            v_c_angle = v_angle / v_c_angle;
            Vector2f v_stP = Vector2f.Zero;
            Vector2f v_enP = Vector2f.Zero ;
            // v_radius  *= v_c_angle;
            Vector2f pt1 = Vector2f.Zero;
            Vector2f pt2 = Vector2f.Zero;
            Vector2f pt3 = Vector2f.Zero;
            Vector2f pt4 = Vector2f.Zero;
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
                                  new Vector2f(
                                        (float)(v_stP.X - v_radius * Math.Cos(v_angle)),
                                        (float)(v_stP.Y - v_radius * Math.Sin(v_angle))),
                                  new Vector2f(
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
                    pt1 = new Vector2f(
                        (float)(v_stP.X - v_radius * Math.Cos(v_angle)),
                        (float)(v_stP.Y - v_radius * Math.Sin(v_angle)));
                    pt2 = new Vector2f(
                        (float)(v_stP.X + v_radius * Math.Cos(v_angle)),
                        (float)(v_stP.Y + v_radius * Math.Sin(v_angle)));
                }
                else
                {
                    pt1 = v_lpoints[v_lpoints.Count - 2];
                    pt2 = v_lpoints[v_lpoints.Count - 2];
                }
                pt3 = new Vector2f(
                    (float)(v_enP.X + v_radius * Math.Cos(v_angle)),
                    (float)(v_enP.Y + v_radius * Math.Sin(v_angle)));
                pt4 = new Vector2f(
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
        private void SplitAdd(CoreGraphicsPath path, Vector2f[] pointF)
        {
            path.AddPolygon(new Vector2f[] { 
                pointF[0],
                pointF[1],
                pointF[3],
            });
            path.AddPolygon(new Vector2f[] { 
                pointF[3],
                pointF[1],
                pointF[2],
            });
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            base.GetParameters(parameters);
            ICoreParameterGroup group =  parameters.AddGroup("Properties");
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
        new class Mecanism : Core2DDrawingDualBrushElement .Mecanism
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
                    if (v_t != null)
                    {
                        for (int i = 0; i < v_t.Length; i++)
                        {
                            this.AddSnippet(this.CurrentSurface.CreateSnippet(this, i, i));
                        }
                    }
                }
            }
            protected override void InitSnippetsLocation()
            {
                if (this.Element != null)
                {
                    Vector2f[] v_t = this.Element.m_Points;
                    if ((v_t != null) && (v_t .Length <= this.RegSnippets.Count ))
                    {
                        for (int i = 0; i < v_t.Length; i++)
                        {
                            this.RegSnippets[i].Location = this.CurrentSurface.GetScreenLocation(v_t[i]);
                        }
                    }
                }
            }
            protected override void InitNewCreatedElement(Core2DDrawingDualBrushElement element, Vector2f defPoint)
            {
                base.InitNewCreatedElement(element, defPoint );
                if (this.m_lPoints == null)
                    this.m_lPoints = new List<Vector2f>();
                this.m_lPoints.Clear();
            }
            /*
             * end drawing override
             * */
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                this.UpdateDrawing(e);
                this.State = ST_EDITING;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.EnabledSnippet();
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
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
                this.Invalidate();
            }
            const int GET_RADIUS = -100;
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                if ((this.IsShiftKey) &&
                    (e.Button == enuMouseButtons.Left) &&
                    (this.Element != null))
                {
                    this.State = GET_RADIUS;
                    this.Element.m_Radius = 0.0f;
                    this.StartPoint = e.FactorPoint;
                    this.EndPoint = e.FactorPoint;
                    this.Element.InitElement();
                    this.Invalidate();
                    return ;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                if (this.State == GET_RADIUS)
                {
                    if (this.IsShiftKey)
                    {
                        this.State = GET_RADIUS;
                        this.Element.m_Radius = CoreMathOperation.GetDistance(e.FactorPoint, this.StartPoint);
                        this.Element.InitElement();
                        this.Invalidate();
                    }
                    else {
                        this.State = ST_EDITING;
                    }
                    this.Invalidate();
                    return;
                }
                base.OnMouseMove(e);
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                if (this.Element == null)
                    return;
                Vector2f[] t = this.Element.m_Points;
                t[this.Snippet.Index] = e.FactorPoint;
                this.Snippet.Location = e.Location;
                this.Element.InitElement();
                this.CurrentSurface .Invalidate();
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                if ((this.State == GET_RADIUS)
                    &&    (e.Button == enuMouseButtons.Left) &&
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
                    case enuMouseButtons .Right :                    
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
                this.Invalidate();
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

