

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSViewColorToolMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools.Menu
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    [CoreViewMenuAttribute("Colors", 
        0x200, Shortcut = enuKeys.C, ShortcutText = "C",
        ImageKey = CoreImageKeys.MENU_COLOR_GKDS)]
    class GCSViewColorToolMenu : CoreViewToolMenuBase
    {
        public GCSViewColorToolMenu():base(GCSGlobalColorTool.Instance)
        {
        }
        protected override void InitMenu()
        {
            base.InitMenu();
        }
    }
}
