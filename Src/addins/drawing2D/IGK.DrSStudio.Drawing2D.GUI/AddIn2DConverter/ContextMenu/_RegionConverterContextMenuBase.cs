

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _RegionConverterContextMenuBase.cs
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
file:_RegionConverterContextMenuBase.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
    abstract class _RegionConverterContextMenuBase : IGKD2DChildContextMenuBase 
    {
        public _RegionConverterContextMenuBase()
        {
            IsRootMenu = false;
        }

        protected override bool IsVisible()
        {
            ICore2DDrawingSurface v_s =
                this.CurrentSurface;
            return base.IsVisible() && this.AllowContextMenu && (v_s.CurrentLayer.SelectedElements.Count > 0);
        }
    }
}

