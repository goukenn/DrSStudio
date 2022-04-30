

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingSurfaceCollections.cs
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
file:ICoreWorkingSurfaceCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.WinUI ;
    public interface ICoreWorkingSurfaceCollections :
        System.Collections .IEnumerable 
    {
        bool CanAdd { get;}
        bool CanRemove{ get; }
        /// <summary>
        /// get the working surface at 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ICoreWorkingSurface this[int index] { get; }
        ICoreWorkingSurface this[string key] { get; }
        int Count { get; }
        void Add(ICoreWorkingSurface surface);
        void Remove(ICoreWorkingSurface surface);
        ICoreWorkingSurface[] ToArray();
        /// <summary>
        /// check if this collection constains the currrent surface
        /// </summary>
        /// <param name="globalIGKDrSStudioWinLauncherWinUIDrSStartSurface"></param>
        /// <returns></returns>
        bool Contains(ICoreWorkingSurface surface);
        int IndexOf(ICoreWorkingSurface iCoreWorkingSurface);
    }
}

