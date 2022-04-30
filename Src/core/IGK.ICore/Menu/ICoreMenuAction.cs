

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMenuAction.cs
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
file:ICoreMenuAction.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Menu
{
    using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.WinUI;
    public interface ICoreMenuAction : 
        ICoreWorkbenchAction ,
        ICoreCaptionItem,
        IDisposable 
    {
        /// <summary>
        /// get the index of this menu item
        /// </summary>
        int Index { get; }
        /// <summary>
        /// get if the menu can be visible
        /// </summary>
        bool CanShow { get; }     
        /// <summary>
        /// get or set if the menu is visible according to the CanShow property
        /// </summary>
        bool Visible { get; set; }
        bool Enabled { get; set; }
        bool IsRootMenu { get; }
        event EventHandler VisibleChanged;
        event EventHandler EnabledChanged;
        ICoreMenuAction Parent { get; }
        /// <summary>
        /// get the menu child collection
        /// </summary>
        ICoreMenuActionCollections Childs { get; }
    }
}

