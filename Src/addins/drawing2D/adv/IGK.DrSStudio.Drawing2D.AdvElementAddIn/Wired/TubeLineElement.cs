

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TubeLineElement.cs
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
file:TubeLineElement.cs
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
namespace IGK.DrSStudio.Drawing2D.Wired
{
    using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Drawing2D;
    [LineCorner ("TubeLine", typeof (Mecanism))]
    class TubeLineElement :
        Core2DDrawingDualBrushElement ,
        ICore2DClosableElement 
    {
            private Vector2f[] m_points;
            private float m_radius;
            private bool m_closed;
            private enuTubeLineMode m_Mode;
            private int m_Count;
            private float m_Angle;
            [CoreXMLAttribute()]
            [CoreXMLDefaultAttributeValue(3)]
        /// <summary>
        /// get or set the angle orientation of all item
        /// </summary>
            public float Angle
            {
                get { return m_Angle; }
                set
                {
                    if ((m_Angle != value)&& (Math.Abs (value )<360))
                    {
                        m_Angle = value;
                        OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    }
                }
            }
            [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (3)]
        /// <summary>
        /// get the number of vertex. used only if element mode is Polygon
        /// </summary>
            public int Count
            {
                get { return m_Count; }
                set
                {
                    if (m_Count != value)
                    {
                        m_Count = value;
                        OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    }
                }
            }
            [CoreXMLAttribute()]
            public enuTubeLineMode Mode
            {
                get { return m_Mode; }
                set
                {
                    if (m_Mode != value)
                    {
                        m_Mode = value;
                        OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    }
                }
            }
            [CoreXMLAttribute()]
            public Vector2f[] Points { get { return this.m_points; } set {
                if (this.m_points != value)
                {
                    this.m_points = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            } }
            [CoreXMLAttribute ()]
            public float Radius {
                get { return this.m_radius; }
                set { 
                    if ((this.m_radius != value ) && (value >= 0.0f))
                    {
                        this.m_radius = value;
                        OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    }
                }
            }
            protected override void BuildBeforeResetTransform()
            {
                Matrix m = this.GetMatrix();
                if (m.IsIdentity) return;
                this.m_points = CoreMathOperation.TransformVector2fPoint(m, this.m_points);
            }
            public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
            {
                ICoreParameterConfigCollections c = base.GetParameters (parameters);
                ICoreParameterGroup group =  c.AddGroup("Definition");
                group.AddItem(GetType().GetProperty("Radius"));
                group.AddItem(GetType().GetProperty("Mode"));
                group.AddItem(GetType().GetProperty("Count"));
                group.AddItem(GetType().GetProperty("Angle"));
                return c;
            }
            protected override void InitGraphicPath(CoreGraphicsPath v_path)
            {
                v_path.Reset();
                if ((m_points == null) || (m_points.Length <= 1))                
                return;
                
                Rectanglef rc = Rectanglef.Empty ;
                v_path.SetMarkers();
                Vector2f[] t = new Vector2f[this.m_points.Length];
                for (int i = 0; i < t.Length; i++)
                {
                    t[i] = this.m_points[i];
                }
                v_path.AddLines(t);
                if (this.m_closed)
                    v_path.CloseFigure();
                for (int i = 0; i < m_points.Length; i++)
			    {
                    rc = CoreMathOperation.GetBounds (m_points [i] , m_radius);
                    v_path.SetMarkers();
                    switch (this.Mode)
                    {
                        case enuTubeLineMode.Custom :
                            //set the custom element
                            break;
                        case enuTubeLineMode.Polygon :
                            v_path.AddPolygon(CoreMathOperation.GetPolygons(
                                m_points[i], m_radius, this.m_Count , m_Angle));
                            break;
                        case enuTubeLineMode.Square :
                            v_path.AddRectangle(rc);
                            break;
                        case enuTubeLineMode.Circle:
                        default:
                            v_path.AddEllipse(rc);
                            break;
                    }
                }
            }
            //public override void Draw(ICoreGraphics g)
            //{
            //    if (!this.View)
            //        return;
            //    Brush v_br = this.FillBrush.GetBrush();
            //    Pen v_pen = this.StrokeBrush.GetPen();
            //    GraphicsPath v_path = this.GetPath();
            //    if (v_path == null)
            //        return;
            //    object v_st = g.Save();
            //    this.SetGraphicsProperty(g);
            //    GraphicsPathIterator v_iterator = new GraphicsPathIterator(this.GetPath());
            //    using (GraphicsPath v_outpath = new GraphicsPath())
            //    {
            //        int v_index = 0;
            //        for (int i = 0; i < v_iterator.SubpathCount; i++)
            //        {
            //            v_outpath.Reset();
            //            v_index = v_iterator.NextMarker(v_outpath);
            //            if (v_br != null) g.FillPath(v_br, v_outpath);
            //            if (v_pen != null) g.DrawPath(v_pen, v_outpath);
            //        }
            //    }
            //    v_iterator.Dispose();
            //    g.Restore(v_st);
            //}
            public TubeLineElement()
            {
                this.m_points = new Vector2f[2];
                this.m_Count = 3;
                this.m_closed = false;
                this.m_Mode = enuTubeLineMode.Circle;
            }
            new class Mecanism : Core2DDrawingDualBrushElement.Mecanism 
            {
                internal const  int ST_SETRADIUS = 0x100;
                private List<Vector2f> m_points;
                public new TubeLineElement Element { get { return base.Element as TubeLineElement; }
                    set { base.Element = value; }
                }
                protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<Core2DDrawingDualBrushElement> e)
                {
                    base.OnElementChanged(e);
                    if (this.Element != null)
                    {
                        this.m_points = new List<Vector2f>();
                        this.m_points.AddRange(this.Element.m_points);
                    }
                    else {
                        this.m_points = null;
                    }
                }
                protected override void OnMouseClick(CoreMouseEventArgs e)
                {
                    base.OnMouseClick(e);
                }
                protected override void OnMouseDown(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    {
                        case enuMouseButtons.Left:
                            switch (this.State)
                            { 
                                case ST_NONE :
                                case ST_CREATING :
                                    this.Element = this.CreateNewElement() as TubeLineElement;
                                    if (this.Element != null)
                                    {
                                        this.State = ST_CREATING;
                                        this.StartPoint = e.FactorPoint;
                                        this.EndPoint = e.FactorPoint;
                                        this.CurrentLayer.Elements.Add(this.Element);
                                    }
                                    break;
                                case ST_EDITING :
                                    if (IsShiftKey && (this.Snippet == null))
                                    { 
                                        //start radius x manager
                                        this.StartPoint = e.FactorPoint;
                                        this.EndPoint = e.FactorPoint;
                                        this.State = ST_SETRADIUS; 
                                    }
                                    break;
                            }
                            break;
                        case enuMouseButtons.Middle:
                            break;
                        case enuMouseButtons.None:
                            break;
                        case enuMouseButtons.Right:
                            break;
                        case enuMouseButtons.XButton1:
                            break;
                        case enuMouseButtons.XButton2:
                            break;
                        default:
                            break;
                    }
                    base.OnMouseDown(e);
                }
                protected override void OnMouseMove(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    { 
                        case enuMouseButtons.Left :
                            switch (this.State)
                            { 
                                case ST_CREATING :
                                    this.UpdateDrawing(e);
                                    break;
                                case ST_EDITING :
                                    if (this.Snippet != null)
                                    {
                                        if (this.IsShiftKey)
                                        {
                                            InsertPoint(e.FactorPoint);
                                            this.State = ST_CONFIGURING;
                                        }
                                        else
                                        {
                                            this.UpdateSnippetEdit(e);
                                        }
                                    }
                                    break;
                                case ST_CONFIGURING :
                                    if (this.IsShiftKey)
                                    {
                                        this.UpdateSnippetEdit(e);
                                    }
                                    break;
                                case ST_SETRADIUS :
                                    if (this.IsShiftKey)
                                    {
                                        this.SetRadius(e);
                                    }
                                    break;
                            }
                            break;
                    }
                }
                private void SetRadius(CoreMouseEventArgs e)
                {
                    this.Element.m_radius = CoreMathOperation.GetDistance(
                        e.FactorPoint, this.StartPoint);
                    this.Element.InitElement();
                    this.Invalidate();
                }
                private void RemovePoint()
                {
                    if((Element !=null)&& (this.Snippet != null)&&(this.Element.m_points .Length > 2))
                    {
                        List<Vector2f> v_pts = new List<Vector2f>();
                        v_pts.AddRange(this.Element.m_points);
                        v_pts.RemoveAt(this.Snippet.Index);
                        this.Element.m_points = v_pts.ToArray();
                        this.Element.InitElement();
                        int v_index = v_pts.Count - 1;
                        GenerateSnippets();
                        InitSnippetsLocation();
                        this.Invalidate();
                    }
                }
                private void InsertPoint(Vector2f vector2f)
                {
                    List<Vector2f> v_pts = new List<Vector2f>();
                    v_pts.AddRange(this.Element.m_points);
                    v_pts.Insert(this.Snippet.Index, vector2f);
                    this.Element.m_points = v_pts.ToArray();
                    int v_index= v_pts .Count -1;
                    this.AddSnippet(CurrentSurface.CreateSnippet(this, v_pts.Count - 1, v_pts.Count - 1));
                    this.RegSnippets[v_pts .Count - 1].Location = CurrentSurface.GetScreenLocation (v_pts [v_index ]);
                    this.Element.InitElement();
                    this.Invalidate();
                }
                protected override void OnMouseUp(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    {
                        case enuMouseButtons.Left :
                            switch (this.State)
                            { 
                                case ST_CONFIGURING :
                                    if (this.IsShiftKey)
                                        this.UpdateSnippetEdit(e);
                                    this.State = ST_EDITING;
                                    return;
                                case ST_SETRADIUS :
                                    if (this.IsShiftKey)
                                    {
                                        this.SetRadius(e);
                                    }
                                    this.State = ST_EDITING;
                                    return;
                            }
                            break;
                        case enuMouseButtons .Right :
                            if ((this.Element != null) && (this.Snippet != null))
                            {
                                this.RemovePoint();
                                return;
                            }
                            break;
                    }
                    base.OnMouseUp(e);
                }
              
                protected override void GenerateSnippets()
                {
                    base.GenerateSnippets();
                    if ((this.Element != null) && (this.Element.m_points != null))
                    {
                        for (int i = 0; i < this.Element.m_points.Length; i++)
                        {
                            this.AddSnippet(this.CurrentSurface.CreateSnippet(this, i, i));
                        }
                    }
                }
                protected override void InitSnippetsLocation()
                {
                    base.InitSnippetsLocation();
                    if ((this.Element != null)&&(this.Element.m_points !=null))
                    {
                        for (int i = 0; i < this.Element.m_points .Length; i++)
                        {
                            this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(this.Element.m_points[i]);
                        }
                    }
                }
                protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
                {
                    this.Snippet.Location = e.Location;
                    this.Element.m_points[this.Snippet.Index] = e.FactorPoint;
                    this.Element.InitElement();
                    this.Invalidate();
                }
                protected override void UpdateDrawing(CoreMouseEventArgs e)
                {
                    switch (this.State)
                    { 
                        case ST_CREATING :
                            this.Element.m_points[0] = this.StartPoint;
                            this.Element.m_points[1] = e.FactorPoint;
                            this.Element.InitElement();
                            this.Invalidate();
                            break;
                    }
                }
            }
            #region ICore2DClosableElement Members
            public bool Closed
            {
                get
                {
                    return this.m_closed;
                }
                set
                {
                    if (this.m_closed != value)
                    {
                        this.m_closed = value;
                        OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    }
                }
            }
            #endregion
    }
}

