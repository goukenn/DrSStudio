

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreContextMenuAction.cs
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
file:ICoreContextMenuAction.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.ContextMenu
{
    using IGK.ICore;using IGK.ICore.Actions;
    /// <summary>
    /// represent the core context menu action
    /// </summary>
    public interface ICoreContextMenuAction :
        ICoreWorkbenchAction,
        ICoreCaptionItem,
        IDisposable 
    {
        ICoreContextMenuAction Parent { get; }
        int Index { get; }
        bool Visible { get; }
        bool Enabled { get; }
        bool IsRootMenu { get; }
        event EventHandler VisibleChanged;
        event EventHandler EnabledChanged;
        ICoreContextMenuActionCollections Childs { get; }
    }
}

