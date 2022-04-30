using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.WorkingObjects.Standard
{
    [Core2DDrawingStandardElement("Torus2D",
       typeof(Torus2DElement.Mecanism))]
    public class Torus2DElement :
             Core2DDrawingDualBrushElement
    {

        private float m_InnerRadius;
        private float m_OuterRadius;
        private float m_SweepAngle;
        private float m_StartAngle;
        private Vector2f m_Center;
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
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);//(EventArgs.Empty);

                }
            }
        }
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public float StartAngle
        {
            get { return m_StartAngle; }
            set
            {
                if (m_StartAngle != value)
                {
                    m_StartAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);//(EventArgs.Empty);

                }
            }
        }
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public float SweepAngle
        {
            get { return m_SweepAngle; }
            set
            {
                if (m_SweepAngle != value)
                {
                    m_SweepAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);//(EventArgs.Empty);

                }
            }
        }
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public float OuterRadius
        {
            get { return m_OuterRadius; }
            set
            {
                if (m_OuterRadius != value)
                {
                    m_OuterRadius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);//(EventArgs.Empty);

                }
            }
        }
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public float InnerRadius
        {
            get { return m_InnerRadius; }
            set
            {
                if (m_InnerRadius != value)
                {
                    m_InnerRadius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);//(EventArgs.Empty);
                }
            }
        }

        public Torus2DElement() : base()
        {

        }
        public static Torus2DElement Create(float innerRadius, float outerRadius,
            float startAngle,
            float sweepAngle
            )
        {
            Torus2DElement g = new Torus2DElement();
            g.SuspendLayout();
            g.InnerRadius = innerRadius;
            g.OuterRadius = outerRadius;
            g.SweepAngle = sweepAngle;
            g.StartAngle = startAngle;
            g.InitElement();
            g.ResumeLayout();
            return g;

        }

        public override void Align(enuCore2DAlignElement alignment, Rectanglef bounds)
        {
            Core2DDrawingUtility.AlignElementWithCenter(this, alignment, 
                bounds, 
                this.Center,
                this.GetAlignmentBound());
           
        }
        public  override Rectanglef GetAlignmentBound()
        {
            return CoreMathOperation.GetBounds(this.Center, Math.Max (this.InnerRadius , this.OuterRadius));// base.GetAlignmentBound();
    }

        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity || m.IsDisposed)
                return;

            this.m_Center = CoreMathOperation.TransformVector2fPoint(m, new Vector2f[] { this.m_Center })[0];
            ///m_Center = m_Center * m;
            //m_Radius = CoreMathOperation.TransformVector2fVector(this.Matrix, m_Radius);
            base.BuildBeforeResetTransform();
        }

        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            Vector2f v_pt1 = CoreMathOperation.GetPoint(this.Center, this.InnerRadius, this.StartAngle);
            Vector2f v_pt2 = CoreMathOperation.GetPoint(this.Center, this.OuterRadius, this.StartAngle);

            Segments.PathSegment pth = new Segments.PathSegment();
            pth.AddLine(v_pt1, v_pt2);
            pth.AddArc(this.Center, new Vector2f(this.OuterRadius), this.StartAngle, this.SweepAngle, false);
            pth.LineTo(CoreMathOperation.GetPoint(this.Center, this.InnerRadius, this.StartAngle + this.SweepAngle));
            pth.AddArc(this.Center, new Vector2f(this.InnerRadius), this.StartAngle + this.SweepAngle,
                -this.SweepAngle, false);


            path.AddSegment(pth);

        }


        new class Mecanism : Core2DDrawingSurfaceMecanismBase<Torus2DElement>
        {
            const int SN_INNER_RADIUS=0;
            const int SN_OUTER_RADIUS=1;
            const int SN_SWEEPANGLE=2;
            const int SN_CENTER=3;

            protected override void InitNewCreatedElement(Torus2DElement element, Vector2f location)
            {
                base.InitNewCreatedElement(element, location);
                element.SuspendLayout();
                element.OuterRadius = element.InnerRadius + 10f;
                element.StartAngle = 0 ;
                element.SweepAngle = 90;
                element.ResumeLayout();
            }
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.RegSnippets.Add (CurrentSurface.CreateSnippet (this, 0,0));
                this.RegSnippets.Add (CurrentSurface.CreateSnippet (this, 0,1));
                this.RegSnippets.Add (CurrentSurface.CreateSnippet (this, 0,2));
                this.RegSnippets.Add (CurrentSurface.CreateSnippet (this, 0,3));
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                var v_e = this.Element;
                v_e.Center = this.StartPoint;
                v_e.OuterRadius = v_e.Center.Distance (e.FactorPoint );//this.StartPoint.Distance(e.Location);
                v_e.InnerRadius = v_e.OuterRadius/2.0f;
                //v_e.EndPoint = e.FactorPoint;

                v_e.InitElement();
                this.Invalidate();
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                var v_e = this.Element;
                if (v_e==null)
                    return ;
                Vector2f v_vpt = e.FactorPoint ;
                v_e.SuspendLayout ();
                if (this.Snippet.Demand == 0)
                {
                    switch (this.Snippet.Index)
                    {
                        case SN_CENTER:
                            v_e.Center = e.FactorPoint;
                    break;
                        case SN_INNER_RADIUS:

                            float f = Math.Min (v_e.OuterRadius,
                                v_e.Center.Distance(e.FactorPoint)
                                );
                            v_e.InnerRadius = f;
                            v_e.StartAngle = CoreMathOperation.GetAngle(v_e.Center, v_vpt) *
                                CoreMathOperation.ConvRdToDEGREE;

                            break;
                        case SN_OUTER_RADIUS:                           
                            v_e.OuterRadius = v_e.Center.Distance(e.FactorPoint);
                            v_e.StartAngle = CoreMathOperation.GetAngle (v_e.Center,v_vpt)* CoreMathOperation.ConvRdToDEGREE;
                            break ;
                        case SN_SWEEPANGLE:
                            float ga =( CoreMathOperation.GetAngle(v_e.Center, v_vpt)* CoreMathOperation.ConvRdToDEGREE)-
                                v_e.StartAngle ;
                        Console.WriteLine (ga);
                            if (ga<0)
                                ga +=360;
                            v_e.SweepAngle = ga ;//- v_e.StartAngle;

                            break;
                        default:
                            break;
                    }
                }
                this.Snippet.Location = e.Location;
                v_e.ResumeLayout();
                v_e.InitElement ();
                this.Invalidate();
            }
            protected internal override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                var v_e = this.Element;
                if (v_e == null)
                    return;
                if (this.RegSnippets.Count >=4) {

                    this.RegSnippets[SN_CENTER].Location = CurrentSurface.GetScreenLocation(v_e.Center);
                    this.RegSnippets[SN_INNER_RADIUS].Location = CurrentSurface.GetScreenLocation(
                        CoreMathOperation.GetPoint (v_e.Center, v_e.InnerRadius, v_e.StartAngle));
                    this.RegSnippets[SN_OUTER_RADIUS].Location = CurrentSurface.GetScreenLocation(
    CoreMathOperation.GetPoint(v_e.Center, v_e.OuterRadius, v_e.StartAngle));
                    this.RegSnippets[SN_SWEEPANGLE].Location = CurrentSurface.GetScreenLocation(
CoreMathOperation.GetPoint(v_e.Center, v_e.OuterRadius, v_e.StartAngle + v_e.SweepAngle));
                }
            }
        }
    }
}
