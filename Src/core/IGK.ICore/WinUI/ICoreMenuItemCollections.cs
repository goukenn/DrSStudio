

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMenuItemCollections.cs
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
file:ICoreMenuItemCollections.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a menu item collection
    /// </summary>
    public interface  ICoreMenuItemCollections : IEnumerable
    {
        int Count { get; }
        void Add(IXCoreMenuItem item);
        void Remove(IXCoreMenuItem item);
        int IndexOf(IXCoreContextMenuItemContainer coreContextMenuItem);
        void Insert(int i, IXCoreMenuItemSeparator coreMenuItemSeparator);
    }
}

