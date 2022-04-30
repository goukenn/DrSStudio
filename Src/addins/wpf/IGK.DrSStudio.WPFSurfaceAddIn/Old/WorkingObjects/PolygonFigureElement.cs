

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PolygonFigureElement.cs
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
file:PolygonFigureElement.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [WPFElement("PolygonFigure", typeof(Mecanism), Keys = System.Windows.Forms.Keys.P )]
    public class PolygonFigureElement : PolygonPathElementBase 
    {
        private int m_Count;
        private Vector2d m_Radius;
        private Vector2d m_Center;
        private double m_Angle;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
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
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
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
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if (m_Count != value)
                {
                    m_Count = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public PolygonFigureElement()
        {
            this.m_Count = 6;
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters =  base.GetParameters(parameters);
            var group = parameters.AddGroup("PolygonFirgure");
            group.AddItem(GetType().GetProperty("Count"));
            group.AddItem(GetType().GetProperty("Angle"));
            return parameters;
        }
        protected override void InitPath()
        {
            int p = this.Count;
            //Vector2d[] vtab = new Vector2d [p];
            float step = (float)((360 / (float)p) * CoreMathOperation.ConvDgToRadian);
            float vangle = (float)(this.Angle * CoreMathOperation.ConvDgToRadian);
            this.Shape.Points.Clear();
            for (int i = 0; i < p; i++)
            {
                    this.Shape.Points.Add(new Vector2d(
                        (this.Center.X + this.Radius.X * Math.Cos(i * step + vangle)),
                        (this.Center.Y + this.Radius.Y * Math.Sin(i * step + vangle ))).ToWPFPoint());              
            }
            this.Shape.FillRule = this.FillRule;
        }
        public new class Mecanism : PolygonPathElementBase.Mecanism
        {
            public new PolygonFigureElement  Element
            {
                get { return base.Element as PolygonFigureElement; }
                set { base.Element = value as PolygonFigureElement; }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
            }
            public override WPFElementBase CreateElement()
            {
                return base.CreateElement();
            }
            protected override void BeginCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                PolygonFigureElement l = this.Element;
                l.m_Center = e.Location;
                l.m_Radius = Vector2d.Zero;
                l.InitPath();
            }
            protected override void UpdateCreateElement(WPFMouseEventArgs e)
            {
                // base.UpdateCreateElement(e);
                PolygonFigureElement l = this.Element;
                if (this.IsShiftKey)
                {
                    double d = Vector2d.Distance(l.Center ,e.Location );
                    l.m_Radius= new Vector2d(d, d);
                }
                else
                {
                    l.m_Radius = Vector2d.DistanceP(l.Center, e.Location);
                }
                double v_angle = Vector2d.GetAngle(l.Center, e.Location ) * CoreMathOperation.ConvRdToDEGREE;
                l.m_Angle = v_angle;                
                l.InitPath();
            }
            protected override void OnMouseDown(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                switch (e.LeftButton)
                {
                    case System.Windows.Input.MouseButtonState.Pressed:
                        switch (this.State)
                        {
                            case ST_EDITING:
                                //add new polyline
                                return;
                        }
                        break;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                base.OnMouseUp(e);
            }
        }
    }
}

