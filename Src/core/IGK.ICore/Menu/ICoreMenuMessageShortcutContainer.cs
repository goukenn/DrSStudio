

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMenuMessageShortcutContainer.cs
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
file:ICoreMenuMessageShortcutContainer.cs
*/

﻿using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Menu
{
    /// <summary>
    /// represent a filter shortcut menu container
    /// </summary>
    public interface ICoreMenuMessageShortcutContainer
    {
        void Register(ICoreMenuAction menu);
        void Unregister(ICoreMenuAction menu);
            bool IsFiltering { get; }
            void StartFilter();
            void EndFilter();
            bool Contains(enuKeys key);
            void Call(enuKeys key);
    }
}

