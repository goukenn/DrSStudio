

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SelectAll.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Menu.Edit
{
    using IGK.DrSStudio.Menu;
    using IGK.ICore.Drawing2D.Menu;

    [DrSStudioMenu("Edit.SelectAll", 0x40, Shortcut = enuKeys.Control | enuKeys.A)]
    class SelectAll : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            this.CurrentSurface.SelectAll();
            return false;
        }
    }
}
