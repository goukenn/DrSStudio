

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreResourceSerializer.cs
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
file:ICoreResourceSerializer.cs
*/

﻿using IGK.ICore;using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// interface used to serialize additionnal resources
    /// </summary>
    public interface ICoreResourceSerializer
    {
        IXMLSerializer Serializer { get; }
        /// <summary>
        /// return the string id 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        string Add(ICoreWorkingObject obj, object resources);
        /// <summary>
        /// register a resources and return the new or the existing id
        /// </summary>
        /// <param name="coreBitmap"></param>
        /// <returns></returns>
        string Register(ICoreResourceItem coreBitmap);
    }
}

