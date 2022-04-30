

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreContextMenuCollections.cs
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
file:ICoreContextMenuCollections.cs
*/

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.ContextMenu ;
    /// <summary>
    /// represent Context menu item attribute
    /// </summary>
    public interface ICoreContextMenuCollections : IEnumerable 
    {
        /// <summary>
        /// get the context menu
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ICoreContextMenuAction this[string key] { get; }
        /// <summary>
        /// get the number of context menu item
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get an array of root menu
        /// </summary>
        /// <returns></returns>
        ICoreContextMenuAction[] RootMenus();
        void Sort();
        bool Register(CoreContextMenuAttribute v_attr, ICoreContextMenuAction ch);
        void ExportMenuAsXML(string filename);
    }
}

