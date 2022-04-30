

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ChartBoxElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent a chart box Element
    /// </summary>
    [ChartElement ("ChartBox", typeof (Mecanism))]
    public class ChartBoxElement : RectangleElement 
    {
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();

            Rectanglef rcf = new Rectanglef(0, 0, 256, 256);
            p.AddRectangle(rcf);
            p.AddLine(10, 10, 10, 200);
            p.AddLine(10, 200, 200, 200);

            Matrix m = new Matrix ();
            m.Scale(this.Bounds.Width / 256, this.Bounds.Height / 256, enuMatrixOrder.Append );
            m.Translate(this.Bounds.X, this.Bounds.Y, enuMatrixOrder.Append);
            p.Transform(m);
            m.Dispose();
        }
        new class Mecanism : RectangleElement.Mecanism
        { 
        }
    }
}
