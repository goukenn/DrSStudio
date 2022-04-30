

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMenuActionCollections.cs
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
file:ICoreMenuActionCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Menu
{
    public interface ICoreMenuActionCollections :
        System.Collections .IEnumerable 
    {
        ICoreMenuAction this[int index] { get; }
        void Add(ICoreMenuAction action);
        void Remove(ICoreMenuAction actions);
        int Count { get; }
        ICoreMenuAction[] ToArray();
        void Clear();
        void AddRange(ICoreMenuAction[] v_tab);
        void Sort();
    }
}

