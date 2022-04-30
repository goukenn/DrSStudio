

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PolygonPathElementBase.cs
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
file:PolygonPathElementBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Codec;
    /// <summary>
    /// represent the polygon path element base
    /// </summary>
    public abstract class PolygonPathElementBase : WPFDualBrushElementBase 
    {
        private System.Windows.Media.FillRule m_FillRule;
        public new System.Windows.Shapes.Polygon Shape
        {
            get { return base.Shape as System.Windows.Shapes.Polygon; }
        }
        [CoreXMLAttribute()]
        public System.Windows.Media.FillRule FillRule
        {
            get { return m_FillRule; }
            set
            {
                if (m_FillRule != value)
                {
                    m_FillRule = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public PolygonPathElementBase()
        {
        }
        protected override System.Windows.Shapes.Shape CreateShape()
        {
            return new System.Windows.Shapes.Polygon();
        }
        //public override Rectangled GetBound()
        //{
        //    return base.GetBound();
        //}
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters  = base.GetParameters(parameters);            
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(GetType().GetProperty("FillRule"));
            return parameters;
        }
        public new class Mecanism : WPFDualBrushElementBase.Mecanism
        {
            public new PolygonPathElementBase Element {
                get { return base.Element as PolygonPathElementBase; }
                set { base.Element = value; }
            }
            protected override void OnMouseDown(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                //switch (e.ChangedButton)
                //{
                //    case System.Windows.Input.MouseButton.Left:
                //        switch (this.State)
                //        {
                //            case ST_CREATING:                            
                //                if (this.Element != null)
                //                {
                //                    this.State = ST_EDITING;
                //                }
                //                break;
                //        }
                //        break;
                //}
                base.OnMouseUp(e);
            }
            protected override void OnMouseMove(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                switch (e.LeftButton )
                {
                    case System.Windows.Input.MouseButtonState.Pressed:
                        switch (this.State)
                        {
                            case ST_CREATING:
                            case ST_EDITING:
                                if (this.Element != null)
                                {
                                    if (this.Snippet == null)
                                        this.UpdateCreateElement(e);
                                    else
                                        this.UpdateSnippetElement(e);
                                }
                                else {
                                    this.State = ST_NONE;
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }
}

