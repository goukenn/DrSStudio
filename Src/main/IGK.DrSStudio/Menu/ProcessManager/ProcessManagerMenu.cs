

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ProcessManagerMenu.cs
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
file:ProcessManagerMenu.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Menu.ProcessManager
{
#if DEBUG
    [DrSStudioMenu("Process", 600)]
    class ProcessManagerMenu : CoreApplicationMenu 
    {
    }
    [DrSStudioMenu("Process.Collect", 0)]
    sealed class CollectAllMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            GC.Collect();
            return base.PerformAction();
        }
    }
    //[DrSStudioMenu("Process.ShowInfo", 1)]
    //sealed class ShowInfoAllMenu : DrSStudioMenuBase, ICoreWorkingConfigurableObject 
    //{
    //}
#endif
}

