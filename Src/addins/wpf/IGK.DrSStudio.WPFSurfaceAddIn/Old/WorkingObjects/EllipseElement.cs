

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:EllipseElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [WPFElement("Ellipse", typeof(Mecanism), Keys= System.Windows.Forms.Keys.E   )]
    public class EllipseElement : WPFDualBrushElementBase
    {
        private Vector2d m_Center;
        private Vector2d m_Radius;
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public Vector2d Radius
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
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Vector2d Center
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
        public EllipseElement()
        {
        }
        protected override void InitPath()
        {
            System.Windows.Shapes.Ellipse d = this.Shape as System.Windows.Shapes.Ellipse;
            d.SetValue(System.Windows.Controls.Canvas.LeftProperty, this.Center.X - this.Radius.X);
            d.SetValue(System.Windows.Controls.Canvas.TopProperty, this.Center.Y - this.Radius.Y);
            d.Width = this.Radius.X * 2;
            d.Height = this.Radius.Y * 2;
        }
        protected override System.Windows.Shapes.Shape CreateShape()
        {
            return new System.Windows.Shapes.Ellipse();
        }
        public override Rectangled GetBound()
        {
            return CoreMathOperation.GetBounds(this.Center, this.Radius.X, this.Radius.Y);
        }
        public override bool Contains(Vector2d point)
        {
            System.Windows.Media.Transform t = new System.Windows.Media.MatrixTransform(this.GetMatrix());
            return t.TransformBounds(this.GetBound ().ToWPFRect ()).Contains(point.ToWPFPoint());            
        }
        public new class Mecanism : WPFDualBrushElementBase.Mecanism 
        {
            public new EllipseElement Element
            {
                get { return base.Element as EllipseElement; }
            }
            protected override void BeginCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                this.StartPoint = e.Location;
                this.EndPoint= e.Location;
                this.Element.m_Center = e.Location;
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                this.EndPoint = e.Location;
                System.Windows.Size f = Vector2d.DistanceP(this.Element.Center, e.Location).ToWPFSize ();
                System.Windows.Shapes.Shape sp = this.Element.Shape;
                if (IsShiftKey)
                {//draw circle 
                    double r = CoreMathOperation.GetDistance(e.Location, this.StartPoint);
                    //f = new Rectangled(this.StartPoint .X - r, this.StartPoint.Y - r, 2 * r, 2 * r);
                    //sp.SetValue(System.Windows.Controls.Canvas.LeftProperty, f.X);
                    //sp.SetValue(System.Windows.Controls.Canvas.TopProperty, f.Y);
                    //sp.Width = 2 *r ;
                    //sp.Height = 2 *r;
                    Element.m_Radius = new Vector2d(r, r);
                }
                else{
                    Element.m_Radius = new Vector2d(f.Width  , f.Height ); 
                }
                Element.InitPath();
            }
        }
    }
}

