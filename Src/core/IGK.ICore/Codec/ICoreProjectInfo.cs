

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreProjectInfo.cs
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
file:ICoreProjectInfo.cs
*/

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Actions;
    /// <summary>
    /// represent project that will manage the document
    /// </summary>
    public interface ICoreProject : ICoreSerializerService, IEnumerable, ICoreGKDSElementItem
    {
        /// <summary>
        /// get the root ICoreProjectItem
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get the ICoreProjectItem
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ICoreProjectItem this[string key] { get; }
        /// <summary>
        /// get a registrated project info action
        /// </summary>
        ICoreProjectActions Actions{get;}
        /// <summary>
        /// get the surface type bname
        /// </summary>
        string SurfaceType { get; }
        /// <summary>
        /// create project info
        /// </summary>
        /// <param name="prInfo"></param>
        void Copy(ICoreProject prInfo);
    }
}

