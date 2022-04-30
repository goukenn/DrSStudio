

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMenu.cs
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
file:ICoreMenu.cs
*/
using IGK.ICore;using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent the base menu collections
    /// </summary>
    public interface ICoreMenu
    {
        int Count { get; }
        void Add(CoreMenuActionBase menuItem);
        void Remove(CoreMenuActionBase menuItem);
        /// <summary>
        /// get or set is this menu is enabled
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// get or set if this menu is visible
        /// </summary>
        bool Visible { get; set; }
    }
}

