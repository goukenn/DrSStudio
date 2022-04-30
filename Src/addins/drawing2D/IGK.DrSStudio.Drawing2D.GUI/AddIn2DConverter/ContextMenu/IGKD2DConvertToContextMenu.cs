

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DConvertToContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.ContextMenu;
using IGK.ICore.ContextMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    /*
     * 
     * base parent of converter context menu
     * 
     * */
    [DrSStudioContextMenu(IGKD2DrawingConstant.CMENU_CONVERTO, 0x300)]
    class IGKD2DConvertToContextMenu : IGKD2DDrawingContextMenuBase
    {

        protected override bool IsVisible()
        {
            return base.IsVisible ();
        }
        protected override bool IsEnabled()
        {
            return (this.Childs.Count > 0) &&  base.IsEnabled();
        }
       
    }
}
