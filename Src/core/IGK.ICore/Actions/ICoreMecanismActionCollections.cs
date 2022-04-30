

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMecanismActionCollections.cs
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
file:ICoreMecanismActionCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Actions
{
    using IGK.ICore;using IGK.ICore.WinUI;
    /// <summary>
    /// represent mecanism acion collection
    /// </summary>
    public interface ICoreMecanismActionCollections :  ICoreFilterMessageAction, ICoreMessageFilter 
    {
        void Add(enuKeys key, ICoreMecanismAction action);
        void Remove(enuKeys key);
        void Clear();
        ICoreMecanismAction this[enuKeys key] { get; set; }
        bool Contains(enuKeys key);
        /// <summary>
        /// Get the number of register action
        /// </summary>
        int Count { get; }
    }
}

