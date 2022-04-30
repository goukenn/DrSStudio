

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFPathElement.cs
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
file:WPFPathElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Drawing;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [WPFElement ("Path", typeof (Mecanism ), IsVisible =false )]
    /// <summary>
    /// represent a wpf path element
    /// </summary>
    public class WPFPathElement : WPFDualBrushElementBase  
    {
        public new System.Windows.Shapes.Path Shape {
            get {
                return base.Shape as System.Windows.Shapes.Path;
            }
        }
        public WPFPathElement()
        {
        }
        protected override System.Windows.Shapes.Shape CreateShape()
        {
            return new System.Windows.Shapes.Path();
        }
        protected override  void InitPath()
        { 
        }
        public static WPFPathElement FromGraphicsPath(Graphics path)
        {
            WPFPathElement p = new WPFPathElement();
            PathGeometry cp = new PathGeometry ();
            p.Shape.Data = cp;
            return null;
        }
        public new class Mecanism : WPFDualBrushElementBase.Mecanism
        {
            protected override void UpdateCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                base.UpdateCreateElement(e);
            }
        }
    }
}

