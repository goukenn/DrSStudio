

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreDeserializerResources.cs
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
file:ICoreDeserializerResources.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Resources
{
    using IGK.ICore;using IGK.ICore.Codec;
    /// <summary>
    /// represent resources deserializer info
    /// </summary>
    public interface ICoreDeserializerResources
    {
        ICoreResourceItem this[string id] { get; }
        bool Contains(string id);
        IXMLDeserializer Deserializer { get; }
        event EventHandler LoadingComplete;
        /// <summary>
        /// raise the load complete event
        /// </summary>
        void RaiseLoadingComplete();
        void Add(string key, ICoreResourceItem resource);
    }
}

