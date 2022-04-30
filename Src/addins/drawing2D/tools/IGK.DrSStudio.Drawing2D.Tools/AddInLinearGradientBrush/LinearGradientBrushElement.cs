using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInLinearGradientBrush
{

    [Core2DDrawingBrushElement ("LinearBrush", 
        typeof (Mecanism ),
        ImageKey = CoreImageKeys.DE_LINEARBRUSH_GKDS)]
    public sealed class LinearGradientBrushElement : 
        Core2DDrawingLayeredElement, 
        ICore2DDrawingVisitable,
        ICoreBrushOwner,
        ICore2DDrawingSelectionView
    {
        private Vector2f m_Start;
        private Vector2f m_End;
        private LinearBrush m_FillBrush;

        public override bool CanReSize => false;
        public override bool CanRotate => false;
        public override bool CanScale => false;
        public override enuBrushSupport BrushSupport=>
                enuBrushSupport.Fill | enuBrushSupport.LinearGradient;
        public ICoreBrush FillBrush=>m_FillBrush; 

        [CoreXMLAttribute()]
        public Vector2f End
        {
            get { return m_End; }
            set
            {
                if (m_End != value)
                {
                    m_End = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2f Start
        {
            get { return m_Start; }
            set
            {
                if (m_Start != value)
                {
                    m_Start = value; OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }



        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Fill)
                return m_FillBrush;
            return null;
        }
       
        

        public LinearGradientBrushElement()
        {
            this.m_FillBrush = new LinearBrush(this);
            this.m_FillBrush.BrushDefinitionChanged += _BrushDefinitionChanged;
        }
        void _BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(Core2DDrawingChangement.Brush);
        }
       
        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }
        /// <summary>
        /// get the bound of the parent document
        /// </summary>
        /// <returns></returns>
        public Rectanglef GetParentBounds()
        {
            var s = this.ParentDocument;
            if (s != null) {
                return new Rectanglef(0, 0, s.Width, s.Height);
            }
            return Rectanglef.Empty;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            var v_pDoc = this.GetParentBounds();
            if (v_pDoc.IsEmpty)
                return;
            path.AddRectangle(v_pDoc);
        }
        public void Visit(ICore2DDrawingVisitor visitor)
        {
            var v_pDoc = this.GetParentBounds();
            if (v_pDoc.IsEmpty)
                return;

            CoreGraphicsPath p = new CoreGraphicsPath();
            p.AddRectangle(v_pDoc);


            var c = v_pDoc.Center;
            var dx = v_pDoc.Right + c.X;
            var dy = v_pDoc.Bottom + c.Y;

            this.m_FillBrush.SetLine(this.Start, this.End);

            visitor.FillPath(this.FillBrush, p);
        }

        public void RenderSelection(ICore2DDrawingSelectionHost host)
        {

        }

        public class Mecanism : Core2DDrawingLineMecanismBase<LinearGradientBrushElement>
        {
            protected override LinearGradientBrushElement CreateNewElement()
            {
                return base.CreateNewElement();
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                var l = this.Element;
                this.EndPoint = e.FactorPoint;

                l.Start = this.StartPoint;
                l.End = this.EndPoint;
                l.InitElement();

                this.Invalidate();

            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                base.EndDrawing(e);
            }
            public override void EndEdition()
            {
                base.EndEdition();
            }
            public override void Render(ICoreGraphics device)
            {
                var l = this.Element;
                if (l == null)
                    return;

                CoreGraphicsPath p = new CoreGraphicsPath();
                p.AddLine(this.CurrentSurface.GetScreenLocation(l.m_Start), this.CurrentSurface.GetScreenLocation(l.m_End));

                device.DrawPath(Colorf.Black, p);
                p.Dispose();
            }

            protected override void UpdateMove(CoreMouseEventArgs e)
            {

            }
            protected override void BeginMove(CoreMouseEventArgs e)
            {

            }
            protected override void EndMove(CoreMouseEventArgs e)
            {

            }

            protected override void BeginSnippetEdit(CoreMouseEventArgs e)
            {
                base.BeginSnippetEdit(e);
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();

                var l = this.Element;
                this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(l.Start);
                this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(l.End );


            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                var l = this.Element;
                switch (this.Snippet.Index)
                {
                    case 0:
                        l.Start = e.FactorPoint;
                        break;
                    case 1:
                        l.End = e.FactorPoint;
                        break;
                    default:
                        break;
                }
                
                l.InitElement();
                this.Invalidate();
            }
        }

        class LinearBrush : CoreBrush
        {
            public void SetLine(Vector2f startF, Vector2f endF)
            {
                var v_rc = CoreMathOperation.GetBounds(startF, endF);
                if (v_rc.Equals(this.Bounds) == false)
                {
                    this.Bounds = v_rc;
                    this.Angle = CoreMathOperation.GetAngle(startF, endF) * CoreMathOperation.ConvRdToDEGREE ;
                    this.OnBrushDefinitionChanged(EventArgs.Empty);
                }
            }
            public LinearBrush(ICoreBrushOwner owner)
                : base(owner)
            {
                
            }
            protected override void InitBrush()
            {
                this.SetLinearBrush(Colorf.Black, Colorf.White, 0);
                this.AutoSize = false;                
            }
            public override ICore.Resources.enuCoreResourceType ResourceType
            {
                get
                {
                    return ICore.Resources.enuCoreResourceType.Brush;
                }
            }
        }
      
    }
}
