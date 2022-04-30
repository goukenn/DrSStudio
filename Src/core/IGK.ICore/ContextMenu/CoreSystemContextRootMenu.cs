

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSystemContextRootMenu.cs
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
file:CoreSystemContextRootMenu.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.ContextMenu
{
    using IGK.ICore;using IGK.ICore.ContextMenu ;
    /// <summary>
    /// only used in internal and provide dummy option. if this type is registrated in contextmenu
    /// that mean element is not registrated
    /// </summary>
    internal class CoreSystemContextRootMenu : CoreContextMenuBase  
    {
        public CoreSystemContextRootMenu()
        {
            this.Index = int.MaxValue ;
        }
        public override string ToString()
        {
            return string.Format("CoreSystemRootContextMenu : {0}", this.Id);
        }
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
    }
}

