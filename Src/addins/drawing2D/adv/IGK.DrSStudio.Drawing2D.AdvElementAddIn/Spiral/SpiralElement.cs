

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SpiralElement.cs
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
file:SpiralElement.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.Mecanism;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    [Core2DDrawingStandardElement("Spiral", 
        typeof(SpiralElement.Mecanism),
        Keys = enuKeys.Shift | enuKeys.N)]
    class SpiralElement :
        Core2DDrawingDualBrushElement  , 
        ICoreSpiralElement,
        ICore2DCircleElement ,
        ICore2DClosableElement, 
        ICore2DTensionElement ,
        ICore2DFillModeElement 
    {
        const int MAX_TOUR = 200;
        private float m_factor;
        private Vector2f[] m_tab;
        private Vector2f m_center;
        private float[] m_radius;
        private float m_tour = 2;
        private float m_angle = 0;
        private float m_stepangle = 1.0f; //angle de pas en dégré            
        private bool m_closed;
        private bool m_EnabledTension;
        private float  m_Tension;
        private enuFillMode  m_FillMode;
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            ICoreParameterGroup group =  parameters.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("Tour"));
            group.AddItem(GetType().GetProperty("Factor"));
            group.AddItem(GetType().GetProperty("FillMode"));
            group.AddItem(GetType().GetProperty("Closed"));
            group.AddItem(GetType().GetProperty("StepAngle"));
            group.AddItem(GetType().GetProperty("Angle"));
            return parameters;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuFillMode.Alternate )]
        /// <summary>
        /// Get or set the fill mode element
        /// </summary>
        public enuFillMode  FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
        /// <summary>
        /// get the tension
        /// </summary>
        public float  Tension
        {
            get { return m_Tension; }
            set
            {
                if (m_Tension != value)
                {
                    m_Tension = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
        public bool EnableTension
        {
            get { return m_EnabledTension; }
            set
            {
                if (m_EnabledTension != value)
                {
                    m_EnabledTension = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public Vector2f[] Points
        {
            get { return this.m_tab; }
            protected set { this.m_tab = value; }
        }
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue (false )]
        public bool Closed
        {
            get { return this.m_closed; }
            set { 
                this.m_closed = value;
                this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs .Definition );
            }
        }
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue (1.0f)]
        public float Factor {
            get {
                return this.m_factor;
            }
            set {
                if ((this.m_factor != value) && (value >= 0.01f) && (value < 10))
                {
                    this.m_factor = value;
                this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs .Definition );
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
        public float Angle
        {
            get
            {
                return (float)(this.m_angle * 180.0f / Math.PI );
            }
            set
            {
                this.m_angle = (float)(value * Math.PI / 180.0f) ;
                this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs .Definition );
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(1.0f)]
        public float StepAngle
        {
            get
            {
                return this.m_stepangle;
            }
            set {
                if ((value >=1) && (Math.Abs (value) < 360))
                {
                    this.m_stepangle = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(2)]
        public float Tour { get { return m_tour; } set {
            if ((value  > 0) && (value  < MAX_TOUR)) //limit
            {
                m_tour = value;
                this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        } 
        }
        protected override void  BuildBeforeResetTransform()
        {
 	        Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            Vector2f[] tb = new Vector2f[] { m_center, new Vector2f(m_center.X + Radius[0], m_center.Y) };
            m.TransformPoints(tb);
            m_center = tb[0];
            m_radius[0] = CoreMathOperation.GetDistance(tb[1], tb[0]);
            m.Reset();
            this.InitElement();            
        }
        protected void InitProperty(Vector2f center, float radius, float tour, float step)
        {
            this.m_center = center;
            this.m_radius[0] = radius;
            this.m_stepangle = step;
            this.m_tour = tour;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(1.0f)]
        public float[] Radius
        {
            get
            {
                return m_radius;
            }
            set
            {
                if (this.m_radius != value )
                {
                    m_radius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue("0;0")]
        public Vector2f Center
        {
            get
            {
                return this.m_center;
            }
            set
            {
                this.m_center = value;
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        protected new class Mecanism : Core2DDrawingCircleMecanismBase<SpiralElement >
        {
            protected override void InitNewCreatedElement(SpiralElement element, Vector2f defPoint)
            {
                base.InitNewCreatedElement(element, defPoint);
                            
            }
            protected override void GenerateSnippets()
            {
                this.DisposeSnippet();
                //center
                this.AddSnippet (this.CurrentSurface .CreateSnippet (this, 0, 0));
                //radius
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                SpiralElement c = this.Element as SpiralElement;
                if ((c != null)&&(this.RegSnippets.Count >=2))
                {
                    RegSnippets[0].Location = (CurrentSurface.GetScreenLocation(
                        c.m_center));
                    RegSnippets[1].Location = (CurrentSurface.GetScreenLocation(
                        CoreMathOperation.GetPoint(c.m_center, c.m_radius[0], c.Angle )
                        ));
                }
            }

            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
                SpiralElement v = this.Element;
                v.SuspendLayout();
                v.m_center = StartPoint;
                v.m_radius[0] = CoreMathOperation.GetDistance(EndPoint, StartPoint);
                v.m_angle = CoreMathOperation.GetAngle(StartPoint, EndPoint);
                v.InitElement();   
                this.State = ST_CREATING;
                this.UpdateDrawing(e);
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                UpdateDrawing(e);
                this.Element.ResumeLayout();
                this.InitSnippetsLocation();
                this.EnabledSnippet();
       
                this.State = ST_EDITING;
                this.Invalidate();
            }
          
            protected override void  UpdateDrawing(CoreMouseEventArgs e)
            {
                SpiralElement c = this.Element; 	            
                c.m_radius[0] = CoreMathOperation.GetDistance(c.m_center, e.FactorPoint);
                c.m_angle = CoreMathOperation.GetAngle(c.m_center, e.FactorPoint);
                this.Element.InitElement ();
                this.Invalidate ();
            }
           
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                SpiralElement c = this.Element;
                if (c != null)
                {
                    switch (this.Snippet.Index)
                    {
                        case 0:
                            c.m_center = e.FactorPoint;
                            break;
                        case 1:
                            c.m_radius[0] = CoreMathOperation.GetDistance(c.m_center, e.FactorPoint);
                            c.m_angle = CoreMathOperation.GetAngle(c.m_center, e.FactorPoint);
                            break;
                    }
                    this.Snippet.Location = e.Location;
                    c.InitElement();                    
                    Invalidate();
                    return;
                }
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                base.EndSnippetEdit(e);
                this.InitSnippetsLocation();
            }
        }
        
        /// <summary>
        /// public constrcutor
        /// </summary>
        public SpiralElement()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Tension = 0.0f;
            this.m_stepangle = 1;
            this.m_factor = 1.0f;
            this.m_radius = new float[1];
        }
        public static SpiralElement CreateElement()
        {
            SpiralElement s = new SpiralElement();
            return s;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if ((this.Radius == null) || (this.Radius.Length == 0))
                return;
            List<Vector2f> pts = new List<Vector2f>();
            const double _2PI = 2 * Math.PI;
            double _TOUR = this.m_tour * _2PI;
            double step = m_stepangle * Math.PI / 180.0f;
            //calcur de la progression du rayon
          //  double r = this.Radius / _TOUR;
            if (step == 0) 
                step = Math.PI / 180.0f;
            Vector2f pt;
            float K = 0.5f;
            float r = 0.0f;
            r = this.Radius[0];
            for (double t = 0; t < _TOUR; t += step)
            {
                K = (float)(Math.Pow (t,m_factor ) / Math.Pow (_TOUR, m_factor ));
                pt = new Vector2f(
                    this.Center.X + (float)( r * K * Math.Cos(t + this.m_angle )),
                    this.Center.Y + (float)( r * K  * Math.Sin(t + this.m_angle ))
                    );        
                pts.Add(pt);
            }
            if (pts.Count > 2)
            {
                if (this.EnableTension )
                {
                   path.AddCurve(pts.ToArray(), this.Tension, this.Closed);
                }
                else 
                {
                    path.AddCurve(pts.ToArray());
                }
            }
            if (this.Closed)
            {
                path.CloseFigure();
            }
            path.FillMode = FillMode;           
            this.m_tab = pts.ToArray();            
        }
    }
}

