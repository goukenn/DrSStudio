

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreToolsCollections.cs
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
file:ICoreToolsCollections.cs
*/

﻿using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Tools ;
    /// <summary>
    /// represent the tools collections
    /// </summary>
    public interface ICoreToolsCollections : 
        IEnumerable 
    {
        /// <summary>
        /// get the number of registrated tools
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get the tool associate with name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ICoreTool this[string name] { get; }
       }
}

