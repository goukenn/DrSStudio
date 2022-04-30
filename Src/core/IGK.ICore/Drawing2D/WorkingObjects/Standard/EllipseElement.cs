

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EllipseElement.cs
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
file:EllipseElement.cs
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.Mecanism;
    using IGK.ICore.Drawing2D.MecanismActions;
    using IGK.ICore.MecanismActions;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;

        [Core2DDrawingStandardElement("Ellipse", 
            typeof(Mecanism),
        Keys = enuKeys.E)]
    public class EllipseElement : 
            Core2DDrawingDualBrushElement , 
            ICore2DEllipseElement ,
            ICore2DFillModeElement
    {
        private enuEllipseModel m_Model;
        private Vector2f m_Center;
        private Vector2f[] m_Radius;
        private enuFillMode m_FillMode;
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity || m.IsDisposed)
                return;
            
            this.m_Center = CoreMathOperation.TransformVector2fPoint(m, new Vector2f[] { this.m_Center })[0];
            ///m_Center = m_Center * m;
            m_Radius = CoreMathOperation.TransformVector2fVector(this.Matrix, m_Radius);
            base.BuildBeforeResetTransform();
        }
            
 [
       CoreXMLAttribute(),
        CoreXMLDefaultAttributeValue(enuFillMode.Alternate)
        ]
        /// <summary>
        /// get or set the fillmode
        /// </summary>
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
 [CoreXMLAttribute()]
 [CoreConfigurableProperty()]
        /// <summary>
        /// get 
        /// </summary>
        public Vector2f[] Radius
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
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public Vector2f Center
        {
            get { return m_Center; }
            set
            {
                if (m_Center != value)
                {
                    m_Center = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuEllipseModel.Ellipse)]
        /// <summary>
        /// get or set the circle model
        /// </summary>
        public enuEllipseModel Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model != value)
                {
                    m_Model = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
            
            /// <summary>
            /// .ctrl
            /// </summary>
        public EllipseElement()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Radius = new Vector2f[1];
            this.m_Model = enuEllipseModel.Ellipse;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            path.FillMode = this.FillMode;
            if ((this.m_Radius == null) || (this.m_Radius.Length == 0) || 
                ((this.m_Radius.Length ==1)&& (this.m_Radius[0]==Vector2f.Zero )))
                return;
          //  this.m_Center = new Vector2f(100, 100);
          //this.m_Radius[0] = new Vector2f(100, 100);
            if (this.m_Model == enuEllipseModel.Ellipse)
            {
                foreach (Vector2f  radius in this.Radius)
                {
                    path.AddEllipse(Center, radius);
                }
            }
            else
            {
                foreach (Vector2f  radius in this.Radius)
                {
                    float v_radiusX = (float)(Math.Abs (radius.X ));
                    float v_radiusY = (float)(Math.Abs (radius.Y ));
                    path.AddRectangle(
                        new Rectanglef(
                            Center.X - v_radiusX,
                            Center.Y - v_radiusY,
                            2 * v_radiusX,
                            2 * v_radiusY));
                }
            }
            //ICoreDrawingPath c = CoreApplicationManager.Instance.GraphicsPathUtils.CreateNewPath();
            //if (c != null)
            //{
            //    foreach (Vector2f radius in this.Radius)
            //    {
            //        c.AddEllipse(Center, radius);
            //    }
            //    byte[] Data1 = null;
            //    Vector2f[] points1 = null;
            //    path.GetAllDefinition(out points1, out Data1);

            //    byte[] Data = null;
            //    Vector2f[] points = null;
            //    c.GetPathDefinition(out points, out  Data);
            //    c.Dispose();
            //}
        }
            

            
        new public class Mecanism : Core2DDrawingSurfaceMecanismBase<EllipseElement>
        {
            private bool m_radiusAdded;
            class ToogleEllipseModel : CoreMecanismActionBase
            {
                private Mecanism m_mecanism;
                public ToogleEllipseModel(Mecanism mecanism)
                {
                    this.m_mecanism = mecanism;
                }
                protected override bool PerformAction()
                {
                    if (this.m_mecanism.Element != null)
                    {
                        if (this.m_mecanism.Element.Model == enuEllipseModel.Ellipse)
                        {
                            this.m_mecanism.Element.Model = enuEllipseModel.Rectangle;
                        }
                        else
                        {
                            this.m_mecanism.Element.Model = enuEllipseModel.Ellipse;
                        }
                        this.m_mecanism.InitSnippetsLocation();
                        this.m_mecanism.Invalidate();
                    }
                    return true;
                }
                public new EllipseElement.Mecanism Mecanism { get{return this.m_mecanism ;} }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();                
                this.AddAction(enuKeys.T, new ToogleEllipseModel(this));
            }
          
            protected internal override void InitSnippetsLocation()
            {
                this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(this.Element.Center);
                Vector2f[] v_radius = this.Element.Radius;
                for (int i = 0; i < v_radius.Length; i++)
                {
                    this.RegSnippets[1 + i].Location = CurrentSurface.GetScreenLocation(
                         this.Element.Center +                        
                        new Vector2f (
                            (float)(v_radius[i].X),
                            (float)(v_radius[i].Y)
                            ));
                }
            }
            protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<EllipseElement> e)
            {
                base.OnElementChanged(e);
                if (e.NewElement != null)
                    this.GenerateSnippets();
                else
                    this.DisposeSnippet();
            }
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.AddSnippet(CreateSnippet(0, 0));
                for (int i = 0; i < this.Element.Radius.Length; i++)
                {
                    this.AddSnippet(this.CreateSnippet(1, 1 + i));
                }
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
                this.Element.SuspendLayout();
                this.State = ST_CREATING;
                this.DisableSnippet();
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                this.Element.Center = this.StartPoint;
                this.EndPoint = e.FactorPoint;
                if (this.IsShiftKey)
                {
                    this.Element.Radius[0] = Vector2f.From(CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint));
                }
                else 
                    this.Element.Radius[0] = CoreMathOperation.GetDistanceP(this.EndPoint ,this.StartPoint);
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                this.UpdateDrawing(e);
                this.Element.ResumeLayout();
                this.State = ST_EDITING;
                this.GenerateSnippets();
                this.EnabledSnippet();
                this.InitSnippetsLocation();
                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
            protected override void BeginSnippetEdit(CoreMouseEventArgs e)
            {
                
                if (this.Snippet.Demand == 0)
                {
                    //move center
                }
                else
                {
                    if (this.IsShiftKey)
                    {
                        //add new Point
                        List<Vector2f> v_pts = new List<Vector2f>();
                        v_pts.AddRange(this.Element.m_Radius);
                        v_pts.Insert(this.Snippet.Index - 1, CoreMathOperation.GetDistanceP( e.FactorPoint,this.Element.m_Center));
                        this.Element.m_Radius = v_pts.ToArray();
                        this.m_radiusAdded = true;
                    }
                }
                base.BeginSnippetEdit(e);
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                switch (this.Snippet.Demand)
                {
                    case 0:
                        this.Element.Center = e.FactorPoint;
                        this.Snippet.Location = e.Location;
                        break;
                    case 1:
                        this.Element.m_Radius[this.Snippet.Index - 1] = CoreMathOperation.GetDistanceP(e.FactorPoint ,this.Element.m_Center );
                        this.Snippet.Location = e.Location;
                        break;
                    default:
                        break;
                }
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                UpdateSnippetEdit(e);
                this.Element.ResumeLayout();
                if (this.m_radiusAdded)
                {
                    this.GenerateSnippets();
                }
                this.EnabledSnippet();
                this.InitSnippetsLocation();
                this.m_radiusAdded = false;
                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                if (e.Button == enuMouseButtons.Right)
                {
                    if ((this.Element != null) && ((this.Snippet != null) && (this.Snippet.Demand == 1) && this.Element.m_Radius.Length > 1))
                    {
                        RemoveRadius(this.Snippet.Index - 1);
                        return;
                    }
                }
                base.OnMouseDown(e);
            }
            protected void RemoveRadius(int index)
            {
                List<Vector2f> v_pts = new List<Vector2f>();
                v_pts.AddRange(this.Element.m_Radius);
                v_pts.RemoveAt(index);
                this.Element.m_Radius = v_pts.ToArray();
                this.Element.InitElement();
                this.Invalidate();
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
    }
}

