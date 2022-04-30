

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BezierElement.cs
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
file:BezierElement.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.GraphicModels;
using IGK.ICore.Drawing2D.MecanismActions;
using IGK.ICore.ComponentModel;

namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("Bezier",
       typeof(Mecanism),
       Keys = enuKeys.B)]
    public class BezierElement : 
        Core2DDrawingDualBrushElement,
        ICore2DFillModeElement ,
        ICore2DClosableElement 
    {
        private Vector2f[] m_Points;
        private enuFillMode m_FillMode;
        private bool m_Closed;

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
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.Matrix;
            this.m_Points = CoreMathOperation.TransformVector2fPoint(this.Matrix, m_Points);
            base.BuildBeforeResetTransform();
        }
         [
        CoreXMLAttribute(),
        CoreXMLDefaultAttributeValue(enuFillMode.Alternate)
        ]
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
        /// <summary>
        /// get or set the bezier element
        /// </summary>
        public Vector2f[] Points
        {
            get { return m_Points; }
            set {
                if ((m_Points != value) && (value != null) && (value.Length >= 4))
                {
                    this.m_Points = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public BezierElement()
        {
            this.m_Points = new Vector2f[4];
        }
        protected override void ReadAttributes(ICore.Codec.IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
            string g = xreader.GetAttribute ("Def");
            if (!string.IsNullOrEmpty(g))
            { 
            TypeConverter c = CoreTypeDescriptor.GetConverter(typeof(Vector2f[]));
            Vector2f[] v_c = c.ConvertFromString (xreader.GetAttribute ("Def")) as Vector2f[];
            if ((v_c !=null) && (v_c.Length ==4))
                this.m_Points = v_c;
                }
        }
        protected override void WriteAttributes(ICore.Codec.IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
            TypeConverter c = CoreTypeDescriptor.GetConverter (typeof (Vector2f[]));
            xwriter.WriteAttributeString ("Def",c.ConvertToString (this.Points ));
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if ((this.m_Points != null) && (this.m_Points.Length >= 4))
            {
                List<byte> def = new List<byte>();
                List<Vector2f> r = new List<Vector2f>();


                for (int i = 0; i < this.m_Points.Length ; i += 3)
                {
                    if (i == 0)
                    {
                        def.Add(0);
                        def.Add(3);
                        def.Add(3);
                        def.Add(3);
                        r.Add(this.Points[0]);
                        r.Add(this.Points[1]);
                        r.Add(this.Points[2]);
                        r.Add(this.Points[3]);
                        i++;
                    }
                    else
                    {
                        def.Add(3);
                        def.Add(3);
                        def.Add(3);
                        r.Add(this.Points[i+0]);
                        r.Add(this.Points[i+1]);
                        r.Add(this.Points[i+2]);
                    
                    }
                }
                if (this.Closed)
                {
                    def.Add(3);
                    def.Add(3);
                    def.Add(3 | (byte) enuGdiGraphicPathType.EndPoint);
                    r.Add(this.Points[0]);
                    r.Add(this.Points[0]);
                    r.Add(this.Points[0]);
                }
                path.AddDefinition(r.ToArray(), def.ToArray());
                //for (int i = 0; i < this.m_Points.Length-3 ; i+=3)
                //{
                //    if (i == 0)
                //    {
                //        path.AddBezier(this.Points[0],
                //        this.Points[1],
                //        this.Points[2],
                //        this.Points[3]
                //        );
                //    }
                //    else {
                //        path.AddBezier(this.Points[i+1],
                //      this.Points[i+2],
                //      this.Points[i+4],
                //      this.Points[i+3]
                //      );
                //    }
                //}
                //if (this.Closed)
                //{
                //    path.AddBezier(this.m_Points[this.m_Points.Length - 1],
                //    this.m_Points[0],
                //    this.m_Points[0],
                //    this.m_Points[0]);
                //}
                //path.AddBezier(this.Points[0],
                //    this.Points[1],
                //    this.Points[2],
                //    this.Points[3]
                //    );
                //if (this.Closed)
                //    path.CloseFigure();
            }
        }
        public new class Mecanism : Core2DDrawingSurfaceMecanismBase<BezierElement>
        {
            private int m_segment;
            private enuBezierPathDrawingMode m_bezierDrawingMode;
            private enuBezierPathControlMode m_controlMode;
            const int DEF_POINT = 1;        //;definition point
            const int DEF_MPOINT = 2;       //;man point

        protected override void OnElementPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            switch ((enu2DPropertyChangedType)e.ID)
            {
                case enu2DPropertyChangedType.MatrixChanged :
                    this.Element.ResetTransform(false);
                    this.InitSnippetsLocation();
                    this.Invalidate();                    
                    break;
            }
        }
            public Mecanism()
            {
                m_segment = -1;
                m_bezierDrawingMode = enuBezierPathDrawingMode.singleSegment;
                m_controlMode = enuBezierPathControlMode.Free;
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                AddAction(enuKeys.N, new GoToNextSegmentAction());
                AddAction(enuKeys.P, new GoToPreviousSegmentAction());
                AddAction(enuKeys.T, new ToggleControlModeAction());
                AddAction(enuKeys.R, new ResetSegementAction());
                AddAction(enuKeys.M, new BezierToggleViewMode());
               }
            
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {
                    AddSnippet(CurrentSurface.CreateSnippet(this, DEF_POINT, 0));
                    AddSnippet(CurrentSurface.CreateSnippet(this, DEF_MPOINT, 1));
                    AddSnippet(CurrentSurface.CreateSnippet(this, DEF_MPOINT, 2));
                    AddSnippet(CurrentSurface.CreateSnippet(this, DEF_POINT, 3));

                    this.RegSnippets[1].Shape = IGK.ICore.WinUI.enuSnippetShape.Circle;
                    this.RegSnippets[2].Shape = IGK.ICore.WinUI.enuSnippetShape.Circle;
                }
                else
                {
                    if (this.Element != null)
                    {
                        Vector2f[] tb = this.Element.Points;
                        for (int i = 0; i < tb.Length; i++)
                        {
                            if ((i % 3) == 0)
                            {
                                AddSnippet(CurrentSurface.CreateSnippet(this, DEF_POINT, i));
                                this.RegSnippets[i].Shape = IGK.ICore.WinUI.enuSnippetShape.Square;
                            }
                            else
                            {
                                AddSnippet(CurrentSurface.CreateSnippet(this, DEF_MPOINT, i));
                                this.RegSnippets[i].Shape = IGK.ICore.WinUI.enuSnippetShape.Circle ;
                            }
                        }
                    }
                }
            }
            
            protected internal override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {
                    int v_i = GetIndex();
                    var v_p = this.Element.m_Points;
                    if ((v_i >= 0) && (v_i +3 < v_p.Length))
                    {
                        this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(v_p[v_i]);
                        this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(v_p[v_i + 1]);
                        this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(v_p[v_i + 2]);
                        this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(v_p[v_i + 3]);
                    }
                }
                else
                {
                    if (this.Element != null)
                    {
                        Vector2f[] tb = this.Element.Points;
                        for (int i = 0; (this.RegSnippets.Count == tb.Length) && (i < tb.Length); i++)
                        {
                            this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(tb[i]);
                        }
                    }
                }

            }
            public override void Render(ICoreGraphics e)
            {
                
                BezierElement b = this.Element;
                if (b == null)
                    return;
                e.SmoothingMode = enuSmoothingMode.AntiAliazed;
                Colorf v_color = CoreRenderer.BezierCurveSelectionColor;
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {
                    int v_i = this.GetIndex();
                    if (RegSnippets.Count != 4)
                        return;
                    Vector2f[] v_pt = new Vector2f[4];
                    v_pt[0] = RegSnippets[0].Location;
                    v_pt[1] = RegSnippets[1].Location;
                    v_pt[2] = RegSnippets[2].Location;
                    v_pt[3] = RegSnippets[3].Location;
                    e.DrawLine(v_color,
                        v_pt[0],
                        v_pt[1]);
                    e.DrawLine(v_color,
                        v_pt[2],
                        v_pt[3]);
                    //ICorePen pen = CoreBrushRegisterManager.GetPen(Colorf.DarkGray);
                    //pen.Width = 1;
                    //pen.DashStyle = enuDashStyle.DashDot ;
                    //e.DrawLine(pen, v_pt[1], v_pt[2]);
                    //e.Graphics.DrawLine(Pens.Pink, v_pt[1], v_pt[2]);
                }
                else
                {
                    Vector2f[] tb = b.m_Points;
                   // e.CompositingQuality = enuCompositingQuality.HighQuality;
                    e.SmoothingMode = enuSmoothingMode.AntiAliazed;
                    for (int i = 1; i < tb.Length; i += 3)
                    {
                        e.DrawLine(v_color,
                            this.CurrentSurface.GetScreenLocation(tb[i]),
                            this.CurrentSurface.GetScreenLocation(tb[i + 1]));
                    }
                    e.SmoothingMode = enuSmoothingMode.None ;
                }
                e.SmoothingMode = enuSmoothingMode.None;
            }
            public override void Edit(BezierElement element)
            {
                this.m_segment = 0;
                base.Edit(element);
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                                if ((this.Snippet == null) && (this.Element !=null))
                                {
                                    //no action
                                    AddSegment(e);
                                    return;
                                }
                                break;
                        }
                        break;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                                this.InitSnippetsLocation();
                                //continue editing
                                return;
                        }
                        break;
                    case enuMouseButtons.Right:
                        if (this.Snippet != null)
                        {
                            //remove segment
                            this.RemoveSegment(this.m_segment);
                            this.InitSnippetsLocation();
                            return;
                        }
                        break;
                }
                base.OnMouseUp(e);
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                { 
                    case enuMouseButtons.Left :
                        switch (this.State)
                        { 
                            case ST_EDITING :
                                if ((this.Snippet == null) && (this.Element !=null))
                                {
                                    //update selected segment
                                    this.UpdateDrawing(e);
                                    return ;
                                }
                                break;
                        }
                        break;
                }

                //switch (e.Button)
                //{
                //    case enuMouseButtons.Left:
                //        switch (this.State)
                //        {
                //            case ST_CREATING:
                //                if (this.Element != null)
                //                {
                //                    this.UpdateCreateElement(e);
                //                }
                //                else
                //                {
                //                    this.State = ST_NONE;
                //                }
                //                break;
                //            case ST_EDITING:
                //                if (this.Snippet != null)
                //                    this.UpdateSnippetElement(e);
                //                else
                //                {
                //                    //update new add segment
                //                    this.State = ST_CREATING;
                //                    this.UpdateCreateElement(e);
                //                }
                //                break;
                //        }
                //        break;
                //}
                base.OnMouseMove(e);
            }

            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                BezierElement v_l = this.Element;
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {

                    int v_i = GetIndex();
                    if (v_i < 0)
                        return;

                    int hi = this.Snippet.Index;

                    switch (hi)
                    {
                        case 0:
                        case 3:
                            if (hi + v_i < v_l.m_Points.Length)
                            {

                                this.Snippet.Location = e.Location;
                                v_l.m_Points[v_i + hi] = e.FactorPoint;
                                this.Element.InitElement();
                                this.CurrentSurface.RefreshScene();
                                return;
                            }
                            break;
                    }
                    switch (this.ControlMode)
                    {
                        case enuBezierPathControlMode.StartPoint:
                            {
                                Vector2f def = CoreMathOperation.GetDistanceP(e.FactorPoint, v_l.m_Points[v_i]);
                                v_l.m_Points[v_i + 1] = e.FactorPoint;
                                v_l.m_Points[v_i + 2] = v_l.m_Points[v_i] - def;
                            }
                            break;
                        case enuBezierPathControlMode.EndPoint:
                            {
                                Vector2f def = CoreMathOperation.GetDistanceP(e.FactorPoint, v_l.m_Points[v_i + 3]);
                                v_l.m_Points[v_i + 1] = v_l.m_Points[v_i + 3] + def;
                                v_l.m_Points[v_i + 2] = v_l.m_Points[v_i + 3] - def;

                            }
                            break;
                        case enuBezierPathControlMode.CenterPoint:
                            {
                                Vector2f mid = CoreMathOperation.GetMiddlePoint(v_l.m_Points[v_i],
                                    v_l.m_Points[v_i + 3]);
                                Vector2f def = CoreMathOperation.GetDistanceP(e.FactorPoint, mid);
                                v_l.m_Points[v_i + 1] = e.FactorPoint;
                                v_l.m_Points[v_i + 2] = mid - def;
                            }
                            break;
                        case enuBezierPathControlMode.BothMiddle:
                            v_l.m_Points[v_i + 1] = e.FactorPoint;
                            v_l.m_Points[v_i + 2] = e.FactorPoint;
                            break;
                        case enuBezierPathControlMode.Tangeant :
                            //
                            //calculate the tangeant point
                            //
                            Vector2f q0 = v_l.m_Points[v_i];
                            Vector2f q1 = e.FactorPoint;
                            Vector2f q2 = v_l.m_Points[v_i+3];

                            Vector2f c1 = q0 + ((2 / 3.0f) * (q1 - q0));
                            Vector2f c2 = q2 + ((2 / 3.0f) * (q1 - q2));
                            v_l.m_Points[v_i + 1] = c1;
                            v_l.m_Points[v_i + 2] = c2;


                            //Transform 4 point to 3 point
                            //q0 = p0
                            //q1 = (3/4) * (c1 + c2 -  (q0/3)  - (q2/3))
                            //q2 = p3


                            break;
                        case enuBezierPathControlMode.Free:
                        default:
                             this.Element.m_Points[v_i + this.Snippet.Index] = e.FactorPoint;
                            break;
                    }
                    RegSnippets[1].Location = this.CurrentSurface.GetScreenLocation(v_l.m_Points[v_i + 1]);
                    RegSnippets[2].Location = this.CurrentSurface.GetScreenLocation(v_l.m_Points[v_i + 2]);// e.FactorPoint;
                }
                else
                {
                    //for all segement
                    this.Snippet.Location = e.Location;
                    v_l.m_Points[this.Snippet.Index] = e.FactorPoint;
                }
                this.Element.InitElement();
                this.CurrentSurface.RefreshScene();
            }

            protected override void BeginDrawing(CoreMouseEventArgs e)
            {

                this.State = ST_CREATING;
                this.Element.m_Points = new Vector2f[4];
                this.Element.m_Points[0] = e.FactorPoint;
                this.Element.m_Points[1] = e.FactorPoint;
                this.Element.m_Points[2] = e.FactorPoint;
                this.Element.m_Points[3] = e.FactorPoint;
                this.m_segment = 0;
                this.Element.InitElement();
            }

            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                if (this.Element == null)
                {
                    this.State = ST_NONE;
                    return;
                }
                int v_i = GetIndex();
                var v_p = this.Element.m_Points;
                if (v_i + 4 <= v_p.Length)
                {
                    switch (this.m_controlMode)
                    {
                        case enuBezierPathControlMode.EndPoint:
                            v_p[v_i + 1] =
                            v_p[v_i + 2] =
                            v_p[v_i + 3] = e.FactorPoint;
                            break;
                        default:

                            Vector2f v_mid = CoreMathOperation.GetMiddlePoint(
                                this.Element.m_Points[v_i],
                                e.FactorPoint);
                            v_p[v_i + 1] = v_mid;
                            v_p[v_i + 2] = v_mid;
                            v_p[v_i + 3] = e.FactorPoint;

                            break;
                    }
                }
                this.Element.InitElement();
                this.Invalidate();

            }
            /// <summary>
            /// get segment type index
            /// </summary>
            /// <returns></returns>
            private int GetIndex()
            {
                return this.m_segment * 3;
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {

                base.UpdateDrawing(e);
                this.State = ST_EDITING;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
            }



            internal void GoToPreviousSegment()
            {
                if (m_segment > 0)
                {
                    this.m_segment--;
                    this.InitSnippetsLocation();
                    this.Invalidate();
                }
            }

            internal void GoToNextSegment()
            {
                int i = GetNumberOfSegment();
                if ((i > 0) && (m_segment < (i - 1)))
                {
                    this.m_segment++;
                    this.InitSnippetsLocation();
                    this.Invalidate();
                }

            }

            void AddSegment(CoreMouseEventArgs e)
            {
                if (this.Element == null)
                    return;
                Vector2f v_end = this.Element.m_Points[this.Element.m_Points.Length - 1];
                Vector2f[] v_copy = new Vector2f[this.Element.m_Points.Length + 3];
                Array.Copy(this.Element.m_Points, v_copy, this.Element.m_Points.Length);
                m_segment++;
                v_copy[v_copy.Length - 1] = v_end;
                v_copy[v_copy.Length - 2] = v_end;
                v_copy[v_copy.Length - 3] = v_end;
                this.Element.m_Points = v_copy;
            }

            /// <summary>
            /// remove th current segment
            /// </summary>
            /// <param name="segment"></param>
            /// <returns></returns>
            int RemoveSegment(int segment)
            {
                if (this.m_bezierDrawingMode != enuBezierPathDrawingMode.singleSegment)
                    return segment;
                int v_numberOfSegment = GetNumberOfSegment();
                if (v_numberOfSegment == 1)
                    return 0;
                if ((segment >= 0) && (segment < v_numberOfSegment))
                {
                    Vector2f[] v_copy = new Vector2f[this.Element.m_Points.Length - 3];
                    List<Vector2f> v_tab = new List<Vector2f>();
                    v_tab.AddRange(this.Element.m_Points);
                    v_tab.RemoveRange(segment * 3 + 1, 3);
                    this.Element.m_Points = v_tab.ToArray();
                    this.m_segment = Math.Max (0, segment - 1);
                    this.Element.InitElement();
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                    this.EnabledSnippet();
                    this.CurrentSurface.RefreshScene();
                    return 1;
                }
                return 0;
            }

            private int GetNumberOfSegment()
            {
                return ((this.Element.m_Points.Length - 1) / 3);
            }

            internal void ResetSegmentAction()
            {
                BezierElement bl = this.Element;
                if (bl == null) return;

                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {
                    List<Vector2f> v_tab = new List<Vector2f>();
                    v_tab.AddRange(this.Element.m_Points);
                    int i = this.m_segment * 3;
                    Vector2f  c = CoreMathOperation.GetMiddlePoint(v_tab[i], v_tab[i + 3]);
                    v_tab[i + 1] = c;
                    v_tab[i + 2] = c;
                    this.Element.m_Points = v_tab.ToArray();
                    this.Element.InitElement();
                    this.InitSnippetsLocation();
                    this.CurrentSurface.RefreshScene();
                }
            }

            internal void ToogleViewMode()
            {
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.allSegment)
                    this.m_bezierDrawingMode = enuBezierPathDrawingMode.singleSegment;
                else
                    this.m_bezierDrawingMode = enuBezierPathDrawingMode.allSegment;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.CurrentSurface.RefreshScene();
            }
            public enuBezierPathDrawingMode BezierDrawingMode
            {
                get
                {
                    return m_bezierDrawingMode;
                }
                set
                {
                    m_bezierDrawingMode = value;
                }
            }
            public enuBezierPathControlMode ControlMode
            {
                get
                {
                    return m_controlMode;
                }
                set
                {
                    m_controlMode = value;
                }
            }
          
         
        }
    }
}

