

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PolygonElement.cs
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
file:PolygonElement.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [WPFElement("Polygon", typeof(Mecanism))]
    public class PolygonElement : PolygonPathElementBase 
    {
        [IGK.DrSStudio.Codec.CoreXMLElement ()]
        [System.ComponentModel.TypeConverter(typeof(Vector2d.Vector2dArrayTypeConverter))]
        public Vector2d[] Points
        {
            get {
                Vector2d[] c = new Vector2d[this.Shape.Points.Count];
                for (int i = 0; i < c.Length; i++)
			{
                    c[i] = this.Shape.Points[i].ToVector2d();
            }
                return c;
            }
            set
            {
                    if (value != null)
                    {
                        this.Shape.Points.Clear();
                        for (int i = 0; i < value.Length ; i++)
                        {
                            this.Shape.Points.Add(value[i].ToWPFPoint ());
                        }
                    }
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
           }
        }
        protected override void InitPath()
        {
        }
        public new class Mecanism : PolygonPathElementBase.Mecanism 
        {
            int m_index;
            public new PolygonElement Element { get { return base.Element as PolygonElement; }
                set { base.Element = value as PolygonElement; }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                System.Windows.Shapes.Polygon v_p =
                   this.Element.Shape as System.Windows.Shapes.Polygon;
                int i = 0;
                foreach (var  pt in v_p.Points)
                {
                    this.RegSnippets.Add (i, CurrentSurface .CreateSnippet (this, i,i));
                    i++;
                }
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                int i = 0;
                System.Windows.Shapes.Polygon v_p =
                   this.Element.Shape as System.Windows.Shapes.Polygon;
                foreach (var  pt in v_p.Points)
                {
                    this.RegSnippets[i].Location = pt.ToVector2d ();
                    i++;
                }
            }
            public override WPFElementBase CreateElement()
            {
                return base.CreateElement();
            }
            protected override void BeginCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                System.Windows.Shapes.Polygon v_p =
                    this.Element.Shape as System.Windows.Shapes.Polygon;
                v_p.FillRule = System.Windows.Media.FillRule.Nonzero ;
                v_p.Points.Add(this.StartPoint.ToWPFPoint());
                v_p.Points.Add(this.EndPoint.ToWPFPoint());
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.m_index = 1;
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
               // base.UpdateCreateElement(e);
                System.Windows.Shapes.Polygon polyline =
                    this.Element.Shape as System.Windows.Shapes.Polygon;
                polyline.Points[m_index] = e.Location.ToWPFPoint();
                this.RegSnippets[m_index].Location = e.Location;
            }
            protected override void OnMouseMove(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                base.OnMouseMove(e);
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
                                    if (this.Snippet == null)
                                    {
                                        this.Element.Shape.Points.Add(e.Location.ToWPFPoint());
                                        this.m_index = this.Element.Shape.Points.Count - 1;
                                        this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, m_index, m_index));
                                        this.RegSnippets[m_index].Location = e.Location;
                                    }
                                }
                                return;
                        } 
                        break;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                if (e.ChangedButton == System.Windows.Input.MouseButton.Left )
                {
                    if (this.Element != null)
                    {
                        this.State = ST_EDITING;
                    }
                    else
                        this.State = ST_NONE;
                    return;
                }
                base.OnMouseUp(e);
            }
            protected override void UpdateSnippetElement(WPFMouseEventArgs e)
            {
                this.Snippet.Location = e.Location.ToFloat();
                this.Element.Shape.Points[this.Snippet.Index] = e.Location.ToWPFPoint();
            }
        }
    }
}

