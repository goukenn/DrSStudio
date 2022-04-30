

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RSStoreMenu..cs
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
file:RSStore.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ResourcesManager.Menu.Action
{
    using IGK.ICore.WinCore;
using IGK.ICore;

    [RSMenuAction(RSConstant.RSMENU_ACTION+".Store", RSConstant.RSMENU_ACTION_INDEX)]
    public sealed class RSStore : RSMenuActionBase
    {
        protected override bool PerformAction()
        {
            this.MainForm.SetCursor(System.Windows.Forms.Cursors.WaitCursor);
            this.CurrentSurface.Store();
            this.MainForm.SetCursor(System.Windows.Forms.Cursors.Default);
            return true;
        }
    }
}

