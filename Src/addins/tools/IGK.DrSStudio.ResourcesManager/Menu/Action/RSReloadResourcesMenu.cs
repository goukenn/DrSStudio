

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RSReloadResourcesMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ResourcesManager.Menu.Action
{
     [RSMenuAction(RSConstant.RSMENU_ACTION + ".Reload", RSConstant.RSMENU_ACTION_INDEX)]
    class RSReloadResourcesMenu : RSMenuActionBase
    {
         protected override bool PerformAction()
         {
             this.CurrentSurface.Reload();
             return base.PerformAction();
         }
    }
}
