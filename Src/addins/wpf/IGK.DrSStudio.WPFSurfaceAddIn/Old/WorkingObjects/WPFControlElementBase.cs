

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFControlElementBase.cs
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
file:WPFControlElementBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent the WPFControl element base
    /// </summary>
    public abstract class WPFControlElement : WorkingObjects.WPFLayeredElement
    {
        public abstract Type ControlType();
        private Rectangled m_Bound;
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public Rectangled Bound
        {
            get { return m_Bound; }
            set
            {
                if (!m_Bound.Equals(value))
                {
                    m_Bound = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public WPFControlElement()
        {
            Type t = ControlType();
            this.Shape = t.Assembly.CreateInstance(t.FullName) as
                 System.Windows.DependencyObject;
        }
        protected override void InitPath()
        {
            this.Shape.SetValue(System.Windows.Controls.Canvas.LeftProperty, this.m_Bound.X);
            this.Shape.SetValue(System.Windows.Controls.Canvas.TopProperty, this.m_Bound.Y);
            this.Shape.SetValue(System.Windows.Controls.Canvas.HeightProperty, this.m_Bound.Height);
            this.Shape.SetValue(System.Windows.Controls.Canvas.WidthProperty, this.m_Bound.Width);
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.CustomControl;
        }
        public override string ToString()
        {
            return "Control [" + this.ControlType().Name + "]";
        }
        public override ICoreControl GetConfigControl()
        {
            //TOGO : Create SHAPE
            return null;
 //           return null;
 //new XPropertyGrid()
 //            {
 //                SelectedObject = this.Shape,
 //                Dock = System.Windows.Forms.DockStyle.Fill
 //            };
        }
        public class ControlMecanismBase : WPFBaseMecanism
        {
            public new WPFControlElement Element
            {
                get { return base.Element as WPFControlElement; }
                set { base.Element = value; }
            }
            public override IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFElementBase CreateElement()
            {
                IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFElementBase l = base.CreateElement();
                return l;
            }
            protected override void OnMouseDown(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                switch (e.ChangedButton)
                {
                    case System.Windows.Input.MouseButton.Left:
                        WPFControlElement l = this.CreateElement() as
                            WPFControlElement;
                        if (l != null)
                        {
                            this.Element = l;
                            this.CurrentLayer.Elements.Add(l);
                            this.CurrentLayer.Select(l);
                            this.State = ST_CREATING;
                            this.StartPoint = e.Location;
                            this.EndPoint = e.Location;
                            this.BeginCreateElement(e);
                        }
                        break;
                }
            }
            protected override void OnMouseMove(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                switch (e.LeftButton)
                {
                    case System.Windows.Input.MouseButtonState.Pressed:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                UpdateCreateElement(e);
                                break;
                            case ST_EDITING:
                                if (this.Snippet == null)
                                    UpdateCreateElement(e);
                                else
                                    UpdateSnippetElement(e);
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseUp(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                switch (e.ChangedButton)
                {
                    case System.Windows.Input.MouseButton.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                this.State = ST_EDITING;
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                break;
                        }
                        break;
                    case System.Windows.Input.MouseButton.Right:
                        if (this.Element == null)
                            this.GotoDefaultTool();
                        else
                        {
                            this.EndEdition();
                        }
                        break;
                }
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                this.EndPoint = e.Location;
                Rectangled f = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                this.Element.m_Bound = f;
                this.Element.InitPath();
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.Element !=null)
                {
                    Rectangled rd = this.Element .GetBound ();
                    Vector2d[] d = rd.GetResizePoints ();
                    this.RegSnippets[0].Location = d[1];
                    this.RegSnippets[1].Location = d[3];
                    this.RegSnippets[2].Location = d[5];
                    this.RegSnippets[3].Location = d[7];
                }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.RegSnippets.Add(this.CurrentSurface.CreateSnippet(this, 0, 0));
                this.RegSnippets.Add(this.CurrentSurface.CreateSnippet(this, 1, 1));
                this.RegSnippets.Add(this.CurrentSurface.CreateSnippet(this, 2, 2));
                this.RegSnippets.Add(this.CurrentSurface.CreateSnippet(this, 3, 3));
            }
        }
    }
}

