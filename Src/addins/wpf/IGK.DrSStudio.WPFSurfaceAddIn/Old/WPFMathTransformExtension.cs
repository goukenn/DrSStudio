

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFMathTransformExtension.cs
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
file:WPFMathTransformExtension.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
namespace IGK.DrSStudio.WPFSurfaceAddIn
{
    static class WPFMathTransformExtension
    {
        public static Rectangled Transform(this Rectangled rec , Matrix mat)
        {
            Rectangled v_rc = new Rectangled();            
            Vector2d[] d = rec.GetCornerPoints();
            d.Transform(mat);
            v_rc = d.GetBound();
            return v_rc;
        }
    }
}

