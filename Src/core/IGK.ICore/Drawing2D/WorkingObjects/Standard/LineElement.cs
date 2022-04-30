

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LineElement.cs
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
file:LineElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;using IGK.ICore.Drawing2D.Mecanism;
    using IGK.ICore.Drawing2D.MecanismActions;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    using System.ComponentModel;

    [Core2DDrawingStandardElement ("Line", 
        typeof (LineElement.Mecanism),
        Keys=enuKeys.L )]
    public class LineElement : Core2DDrawingLayeredElement , ICore2DLineElement ,ICoreBrushOwner 
    {
        private Vector2f  m_StartPoint;
        private Vector2f  m_EndPoint;
        private ICorePen m_StrokeBrush;
        [Browsable(false)]
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.StrokeOnly;
            }
        }

       
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.Matrix;
#pragma warning disable IDE0054 // Use compound assignment
            this.m_StartPoint = this.m_StartPoint * m;
#pragma warning restore IDE0054 // Use compound assignment
            this.m_EndPoint = this.m_StartPoint * m;
            base.BuildBeforeResetTransform();
        }
        public override bool Contains(Vector2f point)
        {
            return base.Contains(point);
        }
        public ICorePen StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        public Vector2f  EndPoint
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
        public Vector2f  StartPoint
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
        public LineElement()
        {
            this.m_StrokeBrush = new CorePen(this);
            this.m_StrokeBrush.BrushDefinitionChanged += m_StrokeBrush_BrushDefinitionChanged;
        }

        void m_StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(Core2DDrawingChangement.Brush);
        }
        public override void Dispose()
        {
            if (this.m_StrokeBrush != null)
            {
                this.m_StrokeBrush.BrushDefinitionChanged -= m_StrokeBrush_BrushDefinitionChanged;
                this.m_StrokeBrush.Dispose();
                this.m_StrokeBrush = null;
            }
            base.Dispose();
        }
        protected override CoreGraphicsPath CreateGraphicsPath()
        {
            CoreGraphicsPath p = new CoreGraphicsPath(this);
            return p;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {            
            path.Reset();
            path.AddLine(this.StartPoint, this.EndPoint);
        }

       
        /// <summary>
        /// represent a line element mecanism
        /// </summary>
        public class Mecanism : Core2DDrawingLineMecanismBase<LineElement>, ICoreHandStyleMecanism
        {
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
            protected override void InitNewCreatedElement(LineElement element,Vector2f defPoint)
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
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
            }
            protected internal override void InitSnippetsLocation()
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
                                this.Element.StartPoint = new Vector2f (e.FactorPoint.X, this.Element.EndPoint.Y);
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
                                this.Element.StartPoint = new Vector2f(this.Element.EndPoint.X , e.FactorPoint.Y );
                                break;
                            case 1:
                                this.Element.EndPoint = new Vector2f(this.Element.StartPoint.X, e.FactorPoint.Y );
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
                this.EndPoint =  e.FactorPoint;
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
                            this.EndPoint = new Vector2f(this.EndPoint.X, this.StartPoint.Y );
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
       
        public override ICoreBrush GetBrush(enuBrushMode enuBrushMode)
        {
            if (enuBrushMode == ICore.enuBrushMode.Stroke)
                return this.StrokeBrush;
            return null;
        }
    }
}

