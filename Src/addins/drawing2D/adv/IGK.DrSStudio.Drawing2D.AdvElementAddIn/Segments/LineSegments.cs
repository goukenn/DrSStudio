using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.Drawing2D.MecanismActions;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Segments
{
    [IGKD2DDrawingLineSegments("LineSegments", typeof(Mecanism))]
    class LineSegments : Core2DDrawingLayeredElement, ICore2DLineElement
    {
        class SegmentCollection {
            private LineSegments m_owner;
            private List<LineSegments.Segment> m_lsegment;

            public SegmentCollection(LineSegments lineSegments)
            {
                this.m_owner = lineSegments;
                this.m_lsegment = new List<LineSegments.Segment>();
            } 

        }

        public class Segment {
            private Vector2f m_Location;
            private enuSegmentDirection m_Direction;

            public enuSegmentDirection Direction
            {
                get { return m_Direction; }
                set
                {
                    if (m_Direction != value)
                    {
                        m_Direction = value;
                    }
                }
            }
            public Vector2f Location
            {
                get { return m_Location; }
                set
                {
                    if (m_Location != value)
                    {
                        m_Location = value;
                    }
                }
            }
        }
        private Vector2f m_StartPoint;
        private Vector2f m_EndPoint;
        private SegmentCollection m_segments;
        private ICorePen m_StrokeBrush;

        public ICorePen StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.StrokeOnly;
            }
        }
        public Vector2f EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                 
                if (m_EndPoint != value)
                {
                    m_EndPoint = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public Vector2f StartPoint
        {
            get { return m_StartPoint; }
            set
            {
                if (m_StartPoint != value)
                {
                    m_StartPoint = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public override ICoreBrush GetBrush(enuBrushMode enuBrushMode)
        {
            if (enuBrushMode == ICore.enuBrushMode.Stroke)
                return this.StrokeBrush;
            return null;
        }
        public LineSegments():base()
        {             
            this.m_segments = new SegmentCollection(this);
            this.m_StrokeBrush = new CorePen(this);
            this.m_StrokeBrush.BrushDefinitionChanged += m_StrokeBrush_BrushDefinitionChanged;
        }

        void m_StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(Core2DDrawingChangement.Brush);
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
        }

      
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();

            //LineElement 
            var v1 = this.StartPoint;
            var v2 = this.EndPoint;
            var w = v2 - v1;
            var w2 = w.X/2.0f;
            var h2 = w.Y/2.0f;

            path.AddLine(v1 , new Vector2f(v1.X + w2 , v1.Y ));
            path.AddLine( new Vector2f(v2.X - w2, v2.Y), v2);
        }

        protected override CoreGraphicsPath CreateGraphicsPath()
        {
            CoreGraphicsPath p = new CoreGraphicsPath(this);
            return p;
        }
        
        class Mecanism : Core2DDrawingLineMecanismBase<LineSegments>, ICoreHandStyleMecanism
        {
            public enuHandStyle HandStyle
            {
                get
                {
                    return m_handStyle;
                }
                set
                {
                    if (this.m_handStyle != value)
                    {
                        this.m_handStyle = value;
                        OnHandStyleChanged(EventArgs.Empty);
                    }
                }
            }

            private void OnHandStyleChanged(EventArgs eventArgs)
            {
                if (HandStyleChanged != null) {
                    HandStyleChanged(this, eventArgs);
                }
            }

            public event EventHandler HandStyleChanged;
            private enuHandStyle m_handStyle;

            public override void Render(ICoreGraphics device)
            {
                base.Render(device);
                var v_l = this.Element;
                if (v_l == null)
                    return;

                device.DrawLine(Colorf.Pink,
                   CurrentSurface.GetScreenLocation ( v_l.StartPoint),
                   CurrentSurface.GetScreenLocation (v_l.EndPoint) );

            }


            protected override void InitNewCreatedElement(LineSegments element, Vector2f defPoint)
            {
                base.InitNewCreatedElement(element, defPoint);
                element.StrokeBrush.Copy(this.CurrentSurface.StrokeBrush);
            }
            protected override void GenerateActions()
            {
                //toggle vertical
                base.GenerateActions();
                Core2DDrawingToogleHandMecanismActions v = new Core2DDrawingToogleHandMecanismActions();
                this.AddAction(enuKeys.V, v);
                this.AddAction(enuKeys.H, v);
                this.AddAction(enuKeys.F, v);
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
            }
            protected override void InitSnippetsLocation()
            {
                RegSnippets[0].Location = CurrentSurface.GetScreenLocation(Element.StartPoint);
                RegSnippets[1].Location = CurrentSurface.GetScreenLocation(Element.EndPoint);
            }
            protected override void BeginSnippetEdit(CoreMouseEventArgs e)
            {
                this.State = ST_EDITING;
                this.Element.SuspendLayout();
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                switch (this.HandStyle)
                {
                    case enuHandStyle.FreeHand:
                        switch (this.Snippet.Demand)
                        {
                            case 0://start point
                                this.Element.StartPoint = e.FactorPoint;
                                break;
                            case 1:
                                this.Element.EndPoint = e.FactorPoint;
                                this.EndPoint = e.FactorPoint;
                                break;
                            default:
                                break;
                        }
                        break;
                    case enuHandStyle.Horizontal:
                        switch (this.Snippet.Demand)
                        {
                            case 0:
                                this.Element.StartPoint = new Vector2f(e.FactorPoint.X, this.Element.EndPoint.Y);
                                break;
                            case 1:
                                this.Element.EndPoint = new Vector2f(e.FactorPoint.X, this.Element.StartPoint.Y);
                                this.EndPoint = this.Element.EndPoint;
                                break;
                            default:
                                break;
                        }
                        break;
                    case enuHandStyle.Vertical:
                        switch (this.Snippet.Demand)
                        {
                            case 0:
                                this.Element.StartPoint = new Vector2f(this.Element.EndPoint.X, e.FactorPoint.Y);
                                break;
                            case 1:
                                this.Element.EndPoint = new Vector2f(this.Element.StartPoint.X, e.FactorPoint.Y);
                                this.EndPoint = this.Element.EndPoint;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                this.Element.InitElement();
                this.Invalidate();
                this.InitSnippetsLocation();
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                UpdateSnippetEdit(e);
                this.Element.ResumeLayout();
                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                this.StartPoint = (this.IsShiftKey) ? this.EndPoint : e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                this.Element.SuspendLayout();
                this.Element.StartPoint = this.StartPoint;
                this.Element.EndPoint = this.EndPoint;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.State = ST_CREATING;
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                if (this.IsControlKey)
                {
                    this.EndPoint = CoreMathOperation.GetAnglePoint(this.StartPoint, e.FactorPoint, 45);
                }
                else
                {
                    this.EndPoint = e.FactorPoint;
                    switch (this.HandStyle)
                    {
                        case enuHandStyle.Horizontal:
                            this.EndPoint = new Vector2f(this.EndPoint.X, this.StartPoint.Y);
                            break;
                        case enuHandStyle.Vertical:
                            this.EndPoint = new Vector2f(this.StartPoint.X, this.EndPoint.Y);
                            break;
                        default:
                            break;
                    }
                }
                this.Element.EndPoint = this.EndPoint;
                this.Element.InitElement();
                this.InitSnippetsLocation();
                this.CurrentSurface.RefreshScene();
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                this.UpdateDrawing(e);
                this.Element.ResumeLayout();
                this.State = ST_EDITING;
                this.EnabledSnippet();
            }
        
        }

      
    }

    /// <summary>
    /// segment direction
    /// </summary>
    public enum enuSegmentDirection { 
        /// <summary>
        /// horizontal
        /// </summary>
        H,
        /// <summary>
        /// vertical
        /// </summary>
        V
    }

}
