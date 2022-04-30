

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPF3DObjectBase.cs
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
file:WPF3DObjectBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPF3DObjects
{
    /// <summary>
    /// represent a 3D Element Object Base
    /// </summary>
    public abstract class WPF3DObjectBase : WPFLayeredElement  
    {
        private Viewport3D m_Viewport;
        private Rectangled m_Bounds;
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public Rectangled Bounds
        {
            get { return m_Bounds; }
            set
            {
                if (m_Bounds.Equals (value) == false)
                {
                    m_Bounds = value;
                    OnPropertyChanged(WPFPropertyChanged.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Camera Camera{
            get {
                return this.m_Viewport.Camera;
            }
            set {
                this.m_Viewport.Camera = value;
            }
        }
        public Viewport3D Viewport
        {
            get { return m_Viewport; }
        }
        public WPF3DObjectBase()
        {
            this.m_Viewport = new Viewport3D();
            this.Shape = this.m_Viewport;
        }
        public override Rectangled GetBound()
        {
            return this.m_Bounds;
        }
        protected override void InitPath()
        {
            this.m_Viewport.Height = m_Bounds.Height;
            this.m_Viewport.Width = m_Bounds.Width;
            this.m_Viewport.SetValue(System.Windows.Controls.Canvas.LeftProperty, m_Bounds.X);
            this.m_Viewport.SetValue(System.Windows.Controls.Canvas.TopProperty, m_Bounds.Y);
        }
        //protected abstract System.Windows.UIElement3D CreateShape();
        public class Mecanism : WPFBaseMecanism
        {
            public new WPF3DObjectBase Element {
                get {
                    return base.Element as WPF3DObjectBase;
                }
                set {
                    base.Element = value;
                }
            }
            protected override void OnMouseDown(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                switch (e.ChangedButton)
                {
                    case System.Windows.Input.MouseButton.Left:
                        this.Element = this.CreateElement() as WPF3DObjectBase;
                        if (this.Element != null)
                        {
                            this.CurrentLayer.Elements.Add(this.Element);
                            this.CurrentLayer.Select(this.Element);
                            this.State = ST_CREATING;
                            this.StartPoint = e.Location;
                            this.EndPoint = e.Location;
                            this.BeginCreateElement(e);
                        }
                        break;
                }
                base.OnMouseDown(e);
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
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                this.EndPoint = e.Location;
                Rectangled f = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                this.Element.m_Bounds = f;
                this.Element.InitPath();
            }
        }
    }
}

