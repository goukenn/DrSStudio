

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MenuPickSystemColor.cs
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
file:_MenuPickSystemColor.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Menu;
using IGK.DrSStudio.PickSystemColorAddin.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Menu;
namespace IGK.DrSStudio.PickSystemColorAddin.Menu
{
    [DrSStudioMenu("Tools.PickSystemColor", 0x3001)]
    class _MenuPickSystemColor : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            PickSCGlobalTool.Instance .PickColor ();
            return false;
        }
    }
}

