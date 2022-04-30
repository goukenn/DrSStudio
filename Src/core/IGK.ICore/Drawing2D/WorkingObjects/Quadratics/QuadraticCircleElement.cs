using IGK.ICore.Drawing2D.Mecanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;

namespace IGK.ICore.Drawing2D.WorkingObjects.Standard
{
    [Core2DDrawingStandardElement("QuadraticCircle", typeof(Mecanism), IsVisible = true)]
    class QuadraticCircleElement:
        QuadraticElement,
        ICoreQuadraticPath
    {
        private float m_Radius;
        private Vector2f m_Center;

        public Vector2f Center
        {
            get { return m_Center; }
            set
            {
                if (m_Center != value)
                {
                    m_Center = value;
                }
            }
        }
        public float Radius
        {
            get { return m_Radius; }
            set
            {
                if (m_Radius != value)
                {
                    m_Radius = value;
                }
            }
        }
        ///<summary>
        ///public .ctr
        ///</summary>
        public QuadraticCircleElement()
        {

        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {

            //init path point
            List<Vector2f> v_tabs = new List<Vector2f>();
            List<byte> v_types = new List<byte>();
            var r = this.Radius;
            v_tabs.Add(new Vector2f() {
                X = Center.X + Radius ,
                Y = Center.Y
            });
            v_tabs.Add(new Vector2f()
            {
                X = Center.X + Radius,
                Y = Center.Y + Radius,
            });

            v_tabs.Add(new Vector2f()
            {
                X = Center.X,
                Y = Center.Y + Radius,
            });
            v_tabs.Add(new Vector2f()
            {
                X = Center.X - Radius,
                Y = Center.Y + Radius
            });
            v_tabs.Add(new Vector2f()            
            {
                X = Center.X - Radius,
                Y = Center.Y 
            });

            v_tabs.Add(new Vector2f()
            {
                X = Center.X - Radius,
                Y = Center.Y - Radius
            });
            v_tabs.Add(new Vector2f()
            {
                X = Center.X ,
                Y = Center.Y - Radius
            });

            v_tabs.Add(new Vector2f()
            {
                X = Center.X+ Radius,
                Y = Center.Y - Radius
            });

            //v_types.AddRange({ });
            
            this.m_Points = v_tabs.ToArray();
            this.m_PointTypes = new byte[] {0,3,1,3,1,3,1| 0x80 };

            base.InitGraphicPath(path);

        }

        
        protected override void InitializeElement()
        {
            base.InitializeElement();
        }

        new class Mecanism : Core2DDrawingSurfaceMecanismBase<QuadraticCircleElement>
        {
            protected override QuadraticCircleElement CreateNewElement()
            {
                return base.CreateNewElement();
            }
            protected internal override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
            }
          

            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
                this.Element.SuspendLayout();
                this.State = ST_CREATING;
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                if (this.Element == null)
                {
                    this.State = ST_NONE;
                    return;
                }
                this.Element.Center = this.StartPoint;
                this.EndPoint = e.FactorPoint;
                this.Element.Radius = CoreMathOperation.GetDistance(this.StartPoint, this.EndPoint);
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                this.UpdateDrawing(e);
                this.Element.ResumeLayout();
                this.State = ST_EDITING;
                this.ShowSnippets();
                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
    }
}
