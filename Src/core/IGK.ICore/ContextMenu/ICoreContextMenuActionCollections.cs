

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreContextMenuActionCollections.cs
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
file:ICoreContextMenuActionCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.ContextMenu
{
    public interface ICoreContextMenuActionCollections
        :System.Collections .IEnumerable 
    {
        ICoreContextMenuAction this[int index] { get; }
        void Add(ICoreContextMenuAction action);
        void Remove(ICoreContextMenuAction actions);
        int Count { get; }
        ICoreContextMenuAction[] ToArray();
        void Clear();
        void AddRange(ICoreContextMenuAction[] v_tab);
        void Sort();
    }
}

