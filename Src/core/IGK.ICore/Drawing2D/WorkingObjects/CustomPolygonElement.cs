

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CustomPolygonElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.Codec;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CustomPolygonElement.cs
*/
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
     [Core2DDrawingStandardElement("CustomPolygon",
        typeof(Mecanism),
        Keys = enuKeys.Shift | enuKeys.P )]
    public class CustomPolygonElement : Core2DDrawingDualBrushElement ,
         ICore2DFillModeElement ,
         ICore2DClosableElement,
         ICore2DTensionElement 
    {
        private Vector2f[] m_Points;
        private enuFillMode m_FillMode;
        private bool m_Closed;
         private bool m_EnableTension;
         private float m_Tension;
         protected override void BuildBeforeResetTransform()
         {
             Matrix m = this.Matrix;
             this.m_Points = CoreMathOperation.TransformVector2fPoint(this.Matrix, m_Points);
             base.BuildBeforeResetTransform();
         }
        [CoreXMLDefaultAttributeValue(0)]
         public float Tension
         {
             get { return m_Tension; }
             set
             {
                 if (m_Tension != value)
                 {
                     m_Tension = value;
                     OnPropertyChanged(Core2DDrawingChangement.Definition);
                 }
             }
         }
        [CoreXMLDefaultAttributeValue(false)]
        public bool EnableTension
         {
             get { return m_EnableTension; }
             set
             {
                 if (m_EnableTension != value)
                 {
                     m_EnableTension = value;
                     OnPropertyChanged(Core2DDrawingChangement.Definition);
                 }
             }
         }

        [CoreXMLDefaultAttributeValue(false)]
        /// <summary>
        /// get or set if element is closed
        /// </summary>
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreXMLDefaultAttributeValue(enuFillMode.Alternate)]
        public enuFillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
         [CoreXMLAttribute()]
        public Vector2f[] Points
        {
            get { return m_Points; }
            set
            {
                if (m_Points != value)
                {
                    m_Points = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public CustomPolygonElement()
        {
            this.m_Points = null;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            ICoreParameterGroup group = parameters.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("enuFillMode"));
            return parameters;
        }
         protected override void InitGraphicPath(CoreGraphicsPath path)
         {
             path.Reset();
             if ((this.m_Points == null) ||(this.m_Points.Length ==0))
                 return;
             if (this.EnableTension)
             {
                 if (this.Closed)
                     path.AddClosedCurve(this.Points, this.Tension);
                 else
                 {
                     path.AddCurve(this.Points, this.Tension,this.Closed);
                 }
             }
             else
             {
                 path.AddPolygon(this.Points);
             }
             path.FillMode = this.FillMode;
         }
        public new class Mecanism : Core2DDrawingRectangleMecanismBase<CustomPolygonElement>        
        {
            private int m_index;
            protected const int ST_INSERTNEWPOINT = ST_CONFIGURING + 100;
            private enuHandStyle m_HandStyle;
            /// <summary>
            /// get or set the hand style
            /// </summary>
            public enuHandStyle HandStyle
            {
                get { return m_HandStyle; }
                set
                {
                    if (m_HandStyle != value)
                    {
                        m_HandStyle = value;
                        OnHandlStyleChanged(EventArgs.Empty);
                    }
                }
            }
            public event EventHandler HandStyleChanged;
            protected virtual void OnHandlStyleChanged(EventArgs eventArgs)
            {
                if (this.HandStyleChanged != null)
                {
                    this.HandStyleChanged(this, eventArgs);
                }
            }
            protected override void InitNewCreatedElement(CustomPolygonElement element, Vector2f defPoint)
            {
                base.InitNewCreatedElement(element, defPoint);
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
            }
          
            public new CustomPolygonElement Element
            {
                get
                {
                    return base.Element as CustomPolygonElement;
                }
                set
                {
                    base.Element = value;
                }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        this.EndPoint = e.FactorPoint;
                        this.StartPoint = e.FactorPoint;
                        if (this.Element == null)
                        {
                            this.Element = CreateNewElement() as CustomPolygonElement;
                            if (this.Element != null)
                            {
                                this.InitNewCreatedElement(this.Element, e.FactorPoint );
                                this.CurrentLayer.Elements.Add(this.Element);
                                this.CurrentLayer.Select(this.Element);

                                //add 2 points
                                this.Element.m_Points = new Vector2f[] { 
                                    e.FactorPoint ,
                                    e.FactorPoint 
                                };
                                this.Element.InitElement();
                                this.BeginDrawing(e);
                                this.State = ST_CREATING;
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                
                                this.m_index = 1;
                            }
                        }
                        else
                        {
                            if (this.Snippet == null)
                            {
                                //add new point
                                List<Vector2f> vl = new List<Vector2f>();
                                vl.AddRange(this.Element.Points);
                                vl.Add(e.FactorPoint);
                                this.Element.m_Points = vl.ToArray();
                                this.Element.InitElement();
                                this.m_index = vl.Count - 1;
                                this.State = ST_CREATING;
                                this.CurrentSurface.RefreshScene();
                            }
                            else
                            {
                                if (this.IsControlKey)
                                {
                                    //insert new point at index
                                    InsertNewPoint(e);
                                }
                                else
                                {
                                    //move the snippet point
                                    this.m_index = this.Snippet.Demand;
                                    this.State = ST_CREATING;
                                }
                            }
                        }
                        break;
                }
            }
            protected void UpdateIndex(Vector2f fpoint, int index)
            {
                CustomPolygonElement g = this.Element;
                if ((index < 0) || (index >= g.m_Points.Length))
                    return;
                if (index == 0)
                {
                    g.m_Points[index] = fpoint;
                    return;
                }
                Vector2f v_c = g.m_Points[index - 1];
                if (this.IsShiftKey)
                {
                    g.m_Points[index] = CoreMathOperation.GetAnglePoint(v_c, fpoint, 45);
                }
                else
                {
                    switch (this.HandStyle)
                    {
                        case enuHandStyle.Horizontal:
                            g.m_Points[index] = new Vector2f(fpoint.X, v_c.Y);
                            break;
                        case enuHandStyle.Vertical:
                            g.m_Points[index] = new Vector2f(v_c.X, fpoint.Y);
                            break;
                        case enuHandStyle.FreeHand:
                        default:
                            g.m_Points[index] = fpoint;
                            break;
                    }
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                UpdateDrawing (e);
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                this.State = ST_EDITING;
                                this.Element.OnPropertyChanged(
                                   Core2DDrawingElementPropertyChangeEventArgs.Definition);
                                break;
                            case ST_INSERTNEWPOINT:
                            case ST_EDITING:
                            case ST_CONFIGURING:
                                if (this.Snippet != null)
                                {
                                    this.m_index = this.Snippet.Index;
                                    UpdateDrawing (e);
                                    this.Element.OnPropertyChanged(
                                        Core2DDrawingElementPropertyChangeEventArgs.Definition);
                                    this.GenerateSnippets();
                                    this.InitSnippetsLocation();
                                    this.State = ST_EDITING;
                                }
                                break;
                        }
                        break;
                    case enuMouseButtons.Right:
                        //remove point, end edition or go to default tool
                        if (this.Element != null)
                        {
                            if (this.Snippet != null)
                                this.RemovePoint();
                            else
                                EndEdition();
                        }
                        else
                        {
                            GotoDefaultTool();
                        }
                        break;
                }
            }
            protected override void OnMouseClick(CoreMouseEventArgs e)
            {
                //do nothing
            }
            private void RemovePoint()
            {
                List<Vector2f> v_p = new List<Vector2f>();
                v_p.AddRange(this.Element.Points);
                if (v_p.Count <= 2)
                    return;
                int index = this.Snippet.Index;
                if ((index >= 0) && (index < v_p.Count))
                {
                    v_p.RemoveAt(this.Snippet.Index);
                    this.Element.m_Points = v_p.ToArray();
                    this.Element.InitElement();
                    this.CurrentSurface.RefreshScene();
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                UpdateDrawing(e);
                                break;
                            case ST_CONFIGURING:
                            case ST_INSERTNEWPOINT:
                                if (this.Snippet != null)
                                {
                                    this.UpdateSnippetEdit(e);
                                }
                                break;
                        }
                        break;
                }
            }
            private void InsertNewPoint(CoreMouseEventArgs e)
            {
                this.State = ST_INSERTNEWPOINT; //configure insertion point
                List<Vector2f> vl = new List<Vector2f>();
                vl.AddRange(this.Element.Points);
                int index = this.Snippet.Index + 1;
                vl.Insert(index, e.FactorPoint);
                this.Element.m_Points = vl.ToArray();
                int c = this.Element.m_Points.Length - 1;
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, c, c));
                this.UpdateSnippetEdit (e);
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                switch (this.State)
                {
                    case ST_INSERTNEWPOINT:
                    case ST_CONFIGURING:
                        if (this.Snippet == null)
                            return;
                        CustomPolygonElement v_l = this.Element;
                        UpdateIndex(e.FactorPoint, this.Snippet.Index);
                        //v_l.m_Points[this.Snippet.Index] = e.FactorPoint;
                        v_l.InitElement();
                        this.CurrentSurface.RefreshScene();
                        this.Snippet.Location = e.Location;
                        break;
                }
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                base.EndDrawing(e);
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                bool init =
                    this.State == ST_INSERTNEWPOINT;

                this.UpdateSnippetEdit(e);
                if (init) {
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                    this.EnabledSnippet();
                }
                this.State = ST_CONFIGURING;
                
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {             
                this.UpdateIndex(e.FactorPoint, m_index);
                this.Element.InitElement();
                this.Invalidate();
                if (this.Snippet != null)
                    this.Snippet.Location = e.Location;
            }
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if ((this.Element == null)|| (this.Element.Points==null)) return;
                for (int i = 0; i < this.Element.Points.Length; i++)
                {
                    AddSnippet(CurrentSurface.CreateSnippet(this, i, i));
                }
            }
            protected internal override void InitSnippetsLocation()
            {
                if ((this.Element == null) ||(this.Element.Points==null))
                    return;
                for (int i = 0; (this.RegSnippets.Count ==
                    this.Element.Points.Length) &&
                    (i < this.Element.Points.Length); i++)
                {
                    this.RegSnippets[i].Location = this.CurrentSurface.GetScreenLocation(
                        this.Element.Points[i]);
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
            }
        }
    }
}

