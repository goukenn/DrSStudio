

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SplineElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
        [Core2DDrawingStandardElement("Spline",
        typeof(Mecanism),
        Keys = enuKeys.N)]
    public class SplineElement : CustomPolygonElement
    {

            public static SplineElement Create(Vector2f[] t)
            {
                if ((t == null) || (t.Length == 0))
                    return null;
                SplineElement e = new SplineElement();
                e.SuspendLayout ();
                e.Points = t;
                e.ResumeLayout();
                return e;
            }
            protected override void InitGraphicPath(CoreGraphicsPath path)
            {
                path.Reset();
                if ((this.Points != null) && (this.Points.Length >=2 ))
                {
                    if (this.EnableTension)
                    {
                        path.AddCurve(this.Points, this.Tension, this.Closed);
                    }
                    else
                    {
                        if (this.Closed)
                        {
                            path.AddClosedCurve(this.Points);
                        }
                        else
                        {
                            path.AddCurve(this.Points);
                        }
                    }
                }
            }
            public new class Mecanism : CustomPolygonElement.Mecanism
            { 
            }
    }
}
