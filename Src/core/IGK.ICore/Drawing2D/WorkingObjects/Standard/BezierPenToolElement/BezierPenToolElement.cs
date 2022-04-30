using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.GraphicModels;
using IGK.ICore.Drawing2D.Segments;
using IGK.ICore.MecanismActions;

namespace IGK.ICore.Drawing2D.WorkingObjects.Standard
{

    /// <summary>
    /// used to edit or create a graphics path
    /// </summary>
    [Core2DDrawingStandardElement("BezierPenTool",
   typeof(Mecanism),
   Keys = ICore.WinUI.enuKeys.Shift| enuKeys.B,
   Edition=new Type[] { typeof(PathElement), typeof(QuadraticElement) }
   )]
    class BezierPenToolElement : Core2DDrawingDualBrushElement, ICore2DDrawingVisitable,
        ICore2DClosableElement
    {
        private List<PenToolSegment> m_segments;
        private int m_editSegment = -1;
        private Mecanism m_designer;

        public bool Closed { get; set; }

        private PenToolSegment GetCurrentSegment()
        {
            if ((m_editSegment == -1)||(m_editSegment >= this.m_segments.Count ))
                return null;
            return this.m_segments[m_editSegment];
        }
        ///<summary>
        ///public .ctr
        ///</summary>
        public BezierPenToolElement()
        {
            this.m_segments = new List<PenToolSegment>();
        }
        private void AddSegment(Vector2f point, Vector2f handleIn, Vector2f handleOut)
        {
            this.m_segments.Add(new PenToolSegment() {
                Point = point,
                HandleIn=handleIn,
                HandleOut = handleOut
            });
            this.m_editSegment = this.m_segments.Count - 1;
        }
        public void ClearSegments() {
            this.m_segments.Clear();
        }
        public void RemoveSegment(int index) {

            if ((index < 0) || (index >= m_segments.Count))
                return;
            m_segments.RemoveAt(index);
            this.m_editSegment = this.m_segments.Count - 1;
        }


       

        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            //
        }
      

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return true;
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            //if (this.m_segments.Count == 0)
            //    return;

            //var o = visitor.Save();
            ////var pt = BuildGraphics();
            //// visitor.DrawPath(Colorf.Black, pt);
            ////foreach (var g in .GetPathPoints()) {
            ////    visitor.FillRectangle(Colorf.Red, new Rectanglef(g.X - 2, g.Y - 2, 4, 4));
            ////}
            //visitor.Restore(o);
        }

        private CoreGraphicsPath BuildGraphics()
        {

            CoreGraphicsPath pt = new CoreGraphicsPath();
            bool first = false;
            PathSegment vp = new PathSegment();

            float curX = 0, curY = 0,
            prevX = 0, prevY = 0,
            inX, inY,
            outX = 0, outY = 0;
            Vector2f handle;
            foreach (var item in this.m_segments)
            {
                curX = item.Point.X;
                curY = item.Point.Y;
                if (!first)
                {
                    first = true;
                    vp.LineTo(item.Point);

                }
                else
                {
                    handle = item.Point - item.HandleIn;
                    inX = curX + handle.X;
                    inY = curY + handle.Y;

                    if (inX == curX && inY == curY
                        && outX == prevX && outY == prevY)
                    {
                        vp.LineTo(curX, curY);
                    }
                    else
                    {
                        vp.AddBezier(outX, outY, inX, inY, curX, curY);
                    }
                }
                prevX = curX;
                prevY = curY;

                handle = item.Point - item.HandleOut;
                outX = prevX + handle.X;
                outY = prevY + handle.Y;

                if (item.Close) {
                    //vp.CloseFigure();
                    pt.AddSegment(vp);
                   // pt.CloseFigure();
                    vp = new PathSegment();
                    first = false;


                }
            }
            if (vp.PointCount > 0)
            {
                pt.AddSegment(vp);
            }
            if (this.Closed)
                pt.CloseFigure();
            return pt;
        }

        public override string ToString()  {
            return $"BezierPenTool : [{this.m_segments.Count}]"; 
        }

        new class Mecanism : Core2DDrawingSurfaceMecanismBase<PathElement>
        {
            private BezierPenToolElement m_toolElement;
            private int m_index;
            //////////////////////////////////////////////////////////////////////////////
            // Actions
            //////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// append last point
            /// </summary>
            sealed class AppendLastPointActions : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism m = this.Mecanism as Mecanism;
                    if (m.m_toolElement.m_segments.Count > 1)
                    {
                        var pt = m.m_toolElement.m_segments[0];
                        m.m_toolElement.AddSegment(pt.Point, pt.Point, pt.Point);
                        m.Element.SetDefinition(m.m_toolElement.BuildGraphics());
                        int h = m.m_toolElement.m_segments.Count - 1;
                        m.RegSnippets.Add(m.CurrentSurface.CreateSnippet(m, h, h));
                        m.RegSnippets[h].Location = m.CurrentSurface.GetScreenLocation(pt.Point);
                        m.Invalidate();
                    }
                    return false;
                }
            }
            
            ///<summary>
            ///public .ctr
            ///</summary>
            public Mecanism()
            {
                m_toolElement = new BezierPenToolElement();
                m_toolElement.m_designer = this;
            }

            protected override PathElement CreateNewElement()
            {
                return new PathElement()
                {
                    Closed = false
                };
            }

            public override void Render(ICoreGraphics device)
            {
                var e = this.Element;
                if (e == null)
                    return;

                base.Render(device);
                var o = device.Save();
                this.ApplyCurrentSurfaceTransform(device);
                m_toolElement.Draw(device);
                device.Restore(o);
                Vector2f _in = Vector2f.Zero;
                Vector2f _out = Vector2f.Zero;
                Vector2f _pt = Vector2f.Zero;

                foreach (var item in m_toolElement.m_segments)
                {
                    _in = CurrentSurface.GetScreenLocation(item.HandleIn);
                    _pt = CurrentSurface.GetScreenLocation(item.Point);
                    _out = CurrentSurface.GetScreenLocation(item.HandleOut);

                    device.DrawLine(Colorf.Aqua, _in, _out);
                    device.FillEllipse(Colorf.Aqua, _in.X - 2, _in.Y - 2, 4, 4);
                    device.FillEllipse(Colorf.Aqua, _pt.X - 2, _pt.Y - 2, 4, 4);
                    device.FillEllipse(Colorf.Aqua, _out.X - 2, _out.Y - 2, 4, 4);
                }
            }

            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.Actions.Add(enuKeys.Y, new AppendLastPointActions());

            }

            protected override void OnLayerSelectedElementChanged(EventArgs eventArgs)
            {
                base.OnLayerSelectedElementChanged(eventArgs);
            }
            public override void EndEdition()
            {
              
                base.EndEdition();
                this.m_toolElement.ClearSegments();
                Invalidate();
            }
            protected internal override void GenerateSnippets()
            {
                var l = this.Element;
                if (l == null)
                {
                    DisposeSnippet();
                    return;
                }

               if (this.State == ST_EDITING)
                return;
                DisposeSnippet();
                int h = 0;
                foreach (var i in this.m_toolElement.m_segments)
                {
                    this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, h, h));
                    h++;
                }
            }

            protected internal override void InitSnippetsLocation()
            {
                int h = 0;

                if (this.m_toolElement.m_segments.Count == this.RegSnippets.Count)
                {

                    foreach (KeyValuePair<int,ICoreSnippet> s in this.RegSnippets)
                    {
                        PenToolSegment i = this.m_toolElement.m_segments[h];
                        s.Value.Location = CurrentSurface.GetScreenLocation(i.Point);
                        h++;
                    }
                }
                else {
                    CoreLog.WriteLine("snippet not equal");
                }
            }

            public override void Edit(ICoreWorkingObject e)
            {
                if (e is QuadraticElement g)
                {
                    var s = PathElement.CreateElement(g.Points, g.PointTypes);      
                    this.CurrentSurface.CurrentDocument.CurrentLayer.Elements.Add(s);
                }
                else
                {
                    base.Edit(e);
                }
            }
            public override void Edit(PathElement element)
            {

                this.m_toolElement.m_segments.Clear();
                if (element == null)
                    return;
                base.Edit(element);

                Vector2f pt = Vector2f.Zero;
                Vector2f[] pts = element.Points;
                bool first = false;
                PenToolSegment v_seg = null;
                bool bezier = false;
                Vector2f lpt = Vector2f.Zero;
                for (int i = 0; i < element.Points.Length; i++)
                {
                    var b = element.PointTypes[i];

                    if (b == 0)
                    {
                        pt = element.Points[i];
                        this.m_toolElement.AddSegment(pt, pt, pt);

                        if (first)
                        {
                            //contains multiple curve
                            v_seg.Close = true;
                            v_seg = this.m_toolElement.m_segments[this.m_toolElement.m_segments.Count - 1];
                        }
                        else
                        {
                            //pt = element.Points[i];
                            //this.m_toolElement.AddSegment(pt, pt, pt);
                            first = true;
                            v_seg = this.m_toolElement.m_segments[0];
                        }
                    }
                    else
                    {
                        //if (b == 129)
                        //{
                        //}
                        //if (b == 131)
                        //{
                        //}
                        if ((b & 3) == 3)
                        {
                            //curve segment
                            pt = pts[i + 2];
                            v_seg.HandleOut = (2 * v_seg.Point) - pts[i];// v_seg.Point + 100;// (pts[i] -pt);
                            lpt = pts[i + 1];
                            v_seg.HandleIn = v_seg.Point + (v_seg.Point - v_seg.HandleOut);// (2 * pt) - pts[i+1];// v_seg.Point + -100;// (pts[i+1]+pt);

                            this.m_toolElement.AddSegment(pt, pt, pt);
                            v_seg = this.m_toolElement.m_segments[this.m_toolElement.m_segments.Count - 1];
                            i += 2;
                            bezier = true;
                        }
                        else
                        {
                            pt = element.Points[i];
                            this.m_toolElement.AddSegment(pt, pt, pt);
                            v_seg = this.m_toolElement.m_segments[this.m_toolElement.m_segments.Count - 1];
                            bezier = false;
                        }
                        if ((b & 0x80) == 0x80)
                        {
                            v_seg.Close = true;
                        }
                    }

                }
                if (bezier)
                {
                    //update the last segment
                    v_seg.HandleIn = (2 * v_seg.Point) - lpt;// v_seg.Point + 100;// (pts[i] -pt);
                    v_seg.HandleOut = v_seg.Point - (v_seg.HandleIn - v_seg.Point);// (2 * pt) - pts[i+1];// v_seg.Point + -100;// (pts[i+1]+pt);

                }
                //CoreLog.WriteDebug("Number or segment " + this.m_toolElement.m_segments.Count);
                this.State = ST_CREATING;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.State = ST_EDITING;
            }

            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Snippet == null) {
                            switch (this.State)
                            {
                                case ST_NONE:
                                    this.m_toolElement.ClearSegments();
                                    break;
                                case ST_CREATING:
                                case ST_EDITING:
                                    if (this.Element != null)
                                    {
                                        this.BeginDrawing(e);
                                        return;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Snippet == null)
                        {
                            switch (this.State)
                            {
                                case ST_CREATING:
                                case ST_EDITING:
                                    UpdateDrawing(e);
                                    return;
                                default:
                                    break;
                            }
                        }
                        else {
                            UpdateSnippetEdit(e);
                            return;
                        }
                        break;
                }
                base.OnMouseMove(e);
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                            case ST_EDITING:
                                if (this.Snippet == null)
                                {
                                    this.EndDrawing(e);
                                    return;
                                }
                                break;
                             
                               
                            default:
                                break;
                        }
                        break;
                }
      
                base.OnMouseUp(e);
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                //if (this.State == ST_NONE)
                //    this.State = ST_CREATING;

                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                var f = e.FactorPoint;
                int h = 0;
                switch (this.State)
                {
                    case ST_NONE:
                        //add segment
                        this.m_index = 0;
                        h = 0;
                        this.DisposeSnippet();
                        this.m_toolElement.ClearSegments();
                        this.m_toolElement.AddSegment(f, f, f);
                        this.m_toolElement.AddSegment(f, f, f);

                        this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, h, h));
                        this.RegSnippets[h].Location = e.Location;
                        h = 1;
                        this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, h, h));
                        this.RegSnippets[h].Location = e.Location;
                        this.State = ST_CREATING;
                        m_index = this.RegSnippets.Count ;
                        break;
                    case ST_CREATING:
                    case ST_EDITING:
                        h = m_index;
                        //add segment
                        this.m_toolElement.AddSegment(f, f, f);
                        this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, h, h));
                        this.RegSnippets[h].Location = e.Location;

                        m_index = this.RegSnippets.Count;
                        //h = 1;
                        //this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, h, h));
                        //this.RegSnippets[h].Location = e.Location;
                        break;
                }
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {

                this.EndPoint = e.FactorPoint;
                PenToolSegment segment = this.m_toolElement.GetCurrentSegment();

                //CoreLog.WriteLine(this.State + " : update drawing : " + e.FactorPoint);
                //var l = this.Element;
                if ((segment != null) && (this.Element is PathElement l))
                {
                    //  int h = this.RegSnippets.Count-1;
                    Vector2f def = this.EndPoint - this.StartPoint;
                    int h = this.m_toolElement.m_editSegment;
                    switch (this.State)
                    {
                        case ST_CREATING:
                            segment.Point = this.EndPoint;
                            segment.HandleIn = this.EndPoint;// segment.Point + def;
                            segment.HandleOut = this.EndPoint; //;// segment.Point - def;
                            break;
                        case ST_EDITING:

                            segment.Point = this.EndPoint;
                            segment.HandleIn = this.EndPoint;// segment.Point + def;
                            segment.HandleOut = this.EndPoint; //;// segment.Point - def;

                            if (this.Snippet != null)
                                this.Snippet.Location = e.Location;
                            //else
                            //    this.RegSnippets[h].Location = e.Location;
                            //if (def != Vector2f.Zero)
                            //{
                            //    segment.HandleIn = segment.Point + def;
                            //    segment.HandleOut = segment.Point - def;
                            //}
                            break;
                    }
                    l.SetDefinition(this.m_toolElement.BuildGraphics());
                    Invalidate();
                }
            }

            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {

                //base.EndSnippetEdit(e);
                if (IsShiftKey)
                {
                    var l = this.Element;
                    int h = Snippet.Index;

                    var s = this.m_toolElement.m_segments[h];
                    Snippet.Location = CurrentSurface.GetScreenLocation(s.Point);
                }
                if (this.State == ST_EDITING + 0x100)
                {
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                    this.State = ST_CREATING;
                }
            }

            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                //base.UpdateSnippetEdit(e);
                var l = this.Element;
                int h = Snippet.Index;
                if ((h < 0) || (h >= this.m_toolElement.m_segments.Count))
                    return;

                var s = this.m_toolElement.m_segments[h];

                if (IsShiftKey)
                {
                    var g = e.FactorPoint - s.Point;
                    s.HandleIn = s.Point - g;
                    s.HandleOut = s.Point + g;
                }
                else
                {
                    var g = s.Point - s.HandleIn;

                    s.Point = e.FactorPoint;
                    s.HandleIn = s.Point - g;
                    s.HandleOut = s.Point + g;
                }

                l.SetDefinition(this.m_toolElement.BuildGraphics());
                Snippet.Location = e.Location;
                Invalidate();

            }
        }

    }
}
