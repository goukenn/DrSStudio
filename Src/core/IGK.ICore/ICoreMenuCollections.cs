

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMenuCollections.cs
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
file:ICoreMenuCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Menu;
using IGK.ICore.WinUI;
    /// <summary>
    /// represent global menu
    /// </summary>
    public interface ICoreMenuCollections:
        System.Collections.IEnumerable 
    {
        ICoreMenuAction this[string key] { get; }
        int Count { get; }
        /// <summary>
        /// Get roots Menus 
        /// </summary>
        /// <returns></returns>
        ICoreMenuAction[] GetRootMenus ();
        void Sort();
        /// <summary>
        /// register custom menu item
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        bool Register(CoreMenuAttribute attr, ICoreMenuAction ch);
        void GenerateMenu(
            ICoreMenu menu,
            ICoreWorkbench Workbench
            );
        void ExportMenuAsXML(string filename);
    }
}

