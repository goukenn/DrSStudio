

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreProjectItem.cs
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
file:ICoreProjectItem.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// represent a project item
    /// </summary>
    /// <remarks>
    /// remarqs: if (haschild) value must be null
    /// </remarks>
    public interface ICoreProjectItem: ICoreSerializerService, ICloneable 
    {
        ICoreProject Owner { get; }
        event EventHandler ValueChanged;             
        /// <summary>
        /// get the value associate with the tag
        /// </summary>
        string Value { get; set; }
        /// <summary>
        /// release the project item
        /// </summary>
        void Release();
    }
}

