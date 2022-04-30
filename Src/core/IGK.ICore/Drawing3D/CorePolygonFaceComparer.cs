

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePolygonFaceComparer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CorePolygonFaceComparer.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing3D
{
    public sealed class CorePolygonFaceComparer : 
         System.Collections.IComparer,
         System.Collections.Generic.IComparer<ICorePolygonFace>
        {
            public int Compare(object x, object y)
            {
                ICorePolygonFace c1 = (ICorePolygonFace)x;
                ICorePolygonFace c2 = (ICorePolygonFace)y;
                return c1.GetCullFactor().CompareTo(c2.GetCullFactor());
            }
            #region IComparer<PolygonFace> Members
            public int Compare(ICorePolygonFace x, ICorePolygonFace y)
            {
                if ((x == null)|| (y == null))
                    return -1;
                return y.GetCullFactor().CompareTo(x.GetCullFactor());
            }
            #endregion
        }
    }

