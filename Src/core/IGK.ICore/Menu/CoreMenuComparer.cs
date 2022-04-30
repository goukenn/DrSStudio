

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMenuComparer.cs
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
file:CoreMenuComparer.cs
*/

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Menu
{
    using IGK.ICore;using IGK.ICore.ContextMenu ;
    /// <summary>
    /// represent the default comparer of the class
    /// </summary>
    public class CoreMenuComparer : 
        IComparer ,
        IComparer<ICoreMenuAction >,
        IComparer<ICoreContextMenuAction >
    {
        public int Compare(object x, object y)
        {
            ICoreMenuAction menu1 = x as ICoreMenuAction ;
            ICoreMenuAction menu2 = y as ICoreMenuAction;
            if (menu1.Parent == menu2.Parent)
            {
                return menu1.Index.CompareTo(menu2.Index);
            }
            else
            {
                return menu1.Id.CompareTo(menu2.Id);
            }
        }
        public int Compare(ICoreMenuAction x, ICoreMenuAction y)
        {
            ICoreMenuAction menu1 = x;
            ICoreMenuAction menu2 = y;
            if (menu1.Parent == menu2.Parent)
            {
                return menu1.Index.CompareTo(menu2.Index);
            }
            else
            {
                int vlevel1 = menu1.Id.Split('.').Length;
                int vlevel2 = menu2.Id.Split('.').Length;
                if (vlevel1 == vlevel2)
                {
                    return menu1.Id.CompareTo(menu2.Id);
                }
                return menu1.Id.CompareTo(menu2.Id);
            }
        }
        public int Compare(ICoreContextMenuAction x, ICoreContextMenuAction y)
        {
            if (x.Parent == y.Parent)
            {
                return x.Index.CompareTo(y.Index);
            }
            else
            {
                return x.Id.CompareTo(y.Id);
            }
        }
    }
}

