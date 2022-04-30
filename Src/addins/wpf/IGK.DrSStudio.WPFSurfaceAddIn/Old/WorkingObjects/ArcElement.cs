

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ArcElement.cs
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
file:ArcElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WinUI.Configuration;
    [WPFElement("Arc", typeof(Mecanism), Keys = System.Windows.Forms.Keys.A )]
    public class ArcElement : WPFPathElement ,
        IWPFArcElement
    {
        private double  m_RotationAngle;
        private Vector2d  m_StartPoint;
        private Vector2d m_Size;
        private SweepDirection m_Direction;
        private Vector2d m_EndPoint;
        private bool m_IsLargeArc;
        private bool m_IsStroked;
        public bool IsStroked
        {
            get { return m_IsStroked; }
            set
            {
                if (m_IsStroked != value)
                {
                    m_IsStroked = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);               
                }
            }
        }
        public bool IsLargeArc
        {
            get { return m_IsLargeArc; }
            set
            {
                if (m_IsLargeArc != value)
                {
                    m_IsLargeArc = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);               
                }
            }
        }
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
        public SweepDirection Direction
        {
            get { return m_Direction; }
            set
            {
                if (m_Direction != value)
                {
                    m_Direction = value;                    
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);       
                }
            }
        }
        public Vector2d Size
        {
            get { return m_Size; }
            set
            {
                if (m_Size != value)
                {
                    m_Size = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public Vector2d  StartPoint
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
        public double RotationAngle
        {
            get { return m_RotationAngle; }
            set
            {
                if (m_RotationAngle != value)
                {
                    m_RotationAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters  = base.GetParameters(parameters);
            var g = parameters.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(GetType().GetProperty("Direction"));
            g.AddItem(GetType().GetProperty("RotationAngle"));
            g.AddItem(GetType().GetProperty("Size"));
            g.AddItem(GetType().GetProperty("IsLargeArc" ));
            g.AddItem(GetType().GetProperty("IsStroked" ));
            return parameters ;
        }
        protected override void InitPath()
        {
            PathFigure figure = new PathFigure();
            figure.StartPoint = this.StartPoint.ToWPFPoint();
            PathGeometry pg = new PathGeometry();
            ArcSegment sg = new ArcSegment(
               this.EndPoint .ToWPFPoint (),
               this.Size .ToWPFSize (),
               this.RotationAngle ,
               this.IsLargeArc,
               this.Direction ,
               this.IsStroked    );
            figure.Segments.Add(sg);
            pg.Figures.Add(figure);
            this.Shape.Data = pg;
         }
        public new class Mecanism : WPFDualBrushElementBase.Mecanism
        {
            public new ArcElement Element {
                get {
                    return base.Element as ArcElement;
                }
            }
            protected override void InitNewCreateElement(WPFElementBase v_element)
            {
                base.InitNewCreateElement(v_element);
            }
            protected override void BeginCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                base.BeginCreateElement(e);
                this.Element.IsStroked = true;
                this.Element.IsLargeArc = false;
                ArcElement l = this.Element  as ArcElement;
                l.m_StartPoint = e.Location;
                l.m_EndPoint = e.Location;
                l.InitPath();
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                //base.UpdateCreateElement(e);
                this.EndPoint = e.Location ;
                Vector2d d = Vector2d.DistanceP(e.Location, this.StartPoint );
                this.Element.m_EndPoint = this.EndPoint;
               // this.Element.m_Size = d;
                this.Element.InitPath();
            }
        }
    }
}

