

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreZIndexComparer.cs
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
file:CoreZIndexComparer.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace IGK.ICore
{
    using IGK.ICore;
    public class CoreZIndexComparer<T> : 
        IComparer,
        IComparer<T> where T : ICoreWorkingPositionableObject
    {
        #region IComparer Members
        public int Compare(object x, object y)
        {
            ICoreWorkingPositionableObject l = x as ICoreWorkingPositionableObject;
            ICoreWorkingPositionableObject v = y as ICoreWorkingPositionableObject;
            return l.ZIndex.CompareTo(v.ZIndex);
        }
        #endregion
        #region IComparer<ICoreWorkingPositionableObject> Members
        public int Compare(T x, T y)
        {
            return x.ZIndex.CompareTo(y.ZIndex );
        }
        #endregion
    }
}

