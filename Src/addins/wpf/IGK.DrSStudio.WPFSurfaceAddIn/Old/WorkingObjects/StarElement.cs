

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: StarElement.cs
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
file:StarElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Codec;
    [WPFElement("Star", typeof(Mecanism), Keys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S )]
    public class StarElement : PolygonPathElementBase 
    {
        private Vector2d m_Center;
        private int m_Count;
        private double  m_Angle;
        private double  m_OffsetAngle;
        private Vector2d m_InnerRadius;
        private Vector2d m_OuterRadius;
        public StarElement()
        {
            this.m_Count = 5;
        }
        [CoreXMLAttribute()]
        public Vector2d OuterRadius
        {
            get { return m_OuterRadius; }
            set
            {
                if (m_OuterRadius != value)
                {
                    m_OuterRadius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2d InnerRadius
        {
            get { return m_InnerRadius; }
            set
            {
                if (m_InnerRadius != value)
                {
                    m_InnerRadius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public double  OffsetAngle
        {
            get { return m_OffsetAngle; }
            set
            {
                if (m_OffsetAngle != value)
                {
                    m_OffsetAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public double Angle
        {
            get { return m_Angle; }
            set
            {
                if (m_Angle != value)
                {
                    m_Angle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if ((m_Count != value)&& (value >2))
                {
                    m_Count = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
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
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters  = base.GetParameters(parameters);
            IGK.DrSStudio.WinUI.ICoreParameterGroup group =  parameters.AddGroup("StarInfo");
            group.AddItem(GetType().GetProperty("Count"));
            group.AddItem(GetType().GetProperty("Angle"));
            group.AddItem(GetType().GetProperty("OffsetAngle"));
            return parameters;
        }
        protected override void InitPath()
        {
            int p = this.Count * 2;
            //Vector2d[] vtab = new Vector2d [p];
            float step = (float)((360 / (float)p) * CoreMathOperation.ConvDgToRadian);
            float vangle = (float)(this.Angle * CoreMathOperation.ConvDgToRadian);
            float v_offAngle = (float)(this.OffsetAngle * CoreMathOperation.ConvDgToRadian);
            this.Shape.Points.Clear();
            for (int i = 0; i < p; i++)
            {
                if ((i % 2) == 0)
                {
                    //for inner radius
                    this.Shape.Points.Add (new Vector2d (
                        (this.Center.X + this.InnerRadius.X  * Math.Cos(i * step + vangle + v_offAngle)),
                        (Center.Y + this.InnerRadius.Y  * Math.Sin(i * step + vangle + v_offAngle))).ToWPFPoint());
                }
                else
                {
                   this.Shape.Points.Add (new Vector2d (
                        (Center.X + this.OuterRadius.X  * Math.Cos(i * step + vangle)),
                        (Center.Y + this.OuterRadius.Y * Math.Sin(i * step + vangle))).ToWPFPoint ());
                }
            }
            this.Shape.FillRule = this.FillRule;
        }
        public new class Mecanism : WPFDualBrushElementBase.Mecanism
        {
            public new StarElement Element
            {
                get { return base.Element as StarElement; }
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                this.EndPoint = e.Location;
                StarElement c = this.Element;
                if (this.IsShiftKey)
                {
                    double d = Vector2d.Distance(this.StartPoint, this.EndPoint);
                    c.m_InnerRadius = new Vector2d (d / 2.0, d/2.0);
                    c.m_OuterRadius = new Vector2d (d,d);
                }
                else {
                    Vector2d d = Vector2d.DistanceP(this.StartPoint, this.EndPoint);
                    c.m_InnerRadius = d / 2.0;
                    c.m_OuterRadius = d;
                }
                double v_angle = Vector2d.GetAngle(c.Center , this.EndPoint ) * CoreMathOperation.ConvRdToDEGREE ;
                c.m_Angle   = v_angle;
                c.InitPath();
            }
            protected override void BeginCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                this.StartPoint = e.Location;
                this.EndPoint = e.Location;
                StarElement c = this.Element ;
                c.m_Angle = 0.0f;
                c.m_Center = e.Location ;
                c.m_InnerRadius = Vector2d.Zero;
                c.m_OuterRadius = Vector2d.Zero;
                c.InitPath();
            }
        }
    }
}

