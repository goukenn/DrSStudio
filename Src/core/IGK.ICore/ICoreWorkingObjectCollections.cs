

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingObjectCollections.cs
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
file:ICoreWorkingObjectCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent the working object collection
    /// </summary>
    public interface ICoreWorkingObjectCollections :
        System.Collections.IEnumerable 
    {
        /// <summary>
        /// get the type of the working object.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Type this[string key]{get;}
        /// <summary>
        /// count the number of object
        /// </summary>
        int Count { get; }

        Type[] GetEditionTools(Type type);
    }
}

