

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PolyLineElement.cs
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
file:PolyLineElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [WPFElement("Polyline", typeof(Mecanism), Keys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.L)]
    public class PolyLineElement : WPFDualBrushElementBase
    {
        public new System.Windows.Shapes.Polyline Shape { get { return base.Shape as System.Windows.Shapes.Polyline; } }
        protected override System.Windows.Shapes.Shape CreateShape()
        {
            return new System.Windows.Shapes.Polyline();
        }
        protected override void InitPath()
        {
        }
        public new class Mecanism : WPFDualBrushElementBase.Mecanism
        {
            int m_index;
            public new PolyLineElement Element { get { return base.Element as PolyLineElement; } }
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
            protected override void OnMouseDown(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                switch (e.LeftButton)
                {
                    case System.Windows.Input.MouseButtonState.Pressed:
                        switch (this.State)
                        {
                            case ST_EDITING:
                                //add new polyline
                                if (this.Element != null)
                                {
                                    this.Element.Shape.Points.Add(e.Location.ToWPFPoint());
                                    this.m_index = this.Element.Shape.Points.Count-1;
                                }
                                return;
                        } 
                        break;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Released)
                {
                    if (this.Element == null)
                        this.State = ST_NONE;
                    else 
                        this.State = ST_EDITING;
                }
                base.OnMouseUp(e);
            }
            protected override void BeginCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                System.Windows.Shapes.Polyline polyline =
                    this.Element.Shape as System.Windows.Shapes.Polyline;
                polyline.Points.Add( this.StartPoint.ToWPFPoint());
                polyline.Points.Add( this.EndPoint.ToWPFPoint());
                this.m_index = 1;
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                if (this.Element == null)
                    return;
                System.Windows.Shapes.Polyline polyline =
                    this.Element.Shape as System.Windows.Shapes.Polyline;
                polyline.Points[m_index] = new System.Windows.Point (e.Location.X ,
                    e.Location.Y );
            }
        }
    }
}

