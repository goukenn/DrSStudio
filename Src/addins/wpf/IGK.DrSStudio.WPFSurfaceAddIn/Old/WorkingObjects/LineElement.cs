

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:LineElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [WPFElement("Line", typeof(Mecanism), Keys = System.Windows.Forms.Keys.L )]
    public class LineElement : WPFShapeElement
    {
        private IGK.DrSStudio.Drawing2D.CorePen m_StrokeBrush;
        private Vector2d m_StartPoint;
        private Vector2d m_EndPoint;
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public Vector2d EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                if (m_EndPoint != value)
                {
                    m_EndPoint = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Vector2d StartPoint
        {
            get { return m_StartPoint; }
            set
            {
                if (m_StartPoint != value)
                {
                    m_StartPoint = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public override IGK.DrSStudio.Drawing2D.enuBrushSupport BrushSupport
        {
            get
            {
                return IGK.DrSStudio.Drawing2D.enuBrushSupport.GdiStroke | IGK.DrSStudio.Drawing2D.enuBrushSupport.StrokeOnly ;
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public IGK.DrSStudio.Drawing2D.CorePen StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        public LineElement()
        {
            this.m_StrokeBrush = new IGK.DrSStudio.Drawing2D.CorePen(this);
            this.m_StrokeBrush.BrushDefinitionChanged += new EventHandler(m_StrokeBrush_BrushDefinitionChanged);
        }
        public override Rectangled GetBound()
        {
            return CoreMathOperation.GetBounds(
                new Vector2d(this.Shape.X1, this.Shape.Y1),
                new Vector2d(this.Shape.X2, this.Shape.Y2));            
        }
        protected override void InitPath()
        {
            System.Windows.Shapes.Line l = this.Shape as System.Windows.Shapes.Line;
            l.X1 = this.StartPoint.X;
            l.Y1 = this.StartPoint.Y;
            l.X2 = this.EndPoint.X;
            l.Y2 = this.EndPoint.Y;
        }
        void m_StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.Shape.Stroke = this.StrokeBrush.ToWPFDefinition();
        }
        public new System.Windows.Shapes.Line Shape
        {
            get { return base.Shape as System.Windows.Shapes.Line; }
        }
        protected override System.Windows.Shapes.Shape CreateShape()
        {
            return new System.Windows.Shapes.Line();
        }
        new class Mecanism : WPFShapeElement .Mecanism 
        {
            protected override void BeginCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                base.BeginCreateElement(e);
                LineElement l = Element as LineElement;
                l.EndPoint = e.Location;
                l.StartPoint = e.Location;
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                this.EndPoint = e.Location;
                LineElement l = Element as LineElement;
                l.EndPoint = e.Location;
                l.InitPath();
            }
            protected override void InitNewCreateElement(WPFElementBase v_element)
            {
                base.InitNewCreateElement(v_element);
                LineElement l = v_element as LineElement;
                l.m_StrokeBrush.Copy(this.CurrentSurface.StrokeBrush );
            }
        }
    }
}

