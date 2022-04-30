

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCursors.cs
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
file:CoreCursors.cs
*/
using IGK.ICore;using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    public static class CoreCursors
    {
        public static readonly ICoreCursor Hand;
        public static readonly ICoreCursor Normal;
        public static readonly ICoreCursor Wait;
        static CoreCursors()
        {
            Hand = CoreResources.GetCursor("Hand") as ICoreCursor;
            Normal = CoreResources.GetCursor("Normal") as ICoreCursor ;
            Wait = CoreResources.GetCursor("Wait") as ICoreCursor;
        }
    }
}

