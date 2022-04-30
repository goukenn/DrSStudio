

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingDeleteLayerMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddIn2DLayer.Menu.Layer
{
        [IGKD2DDrawingLayerMenuAttribute("DeleteLayer", 3,
        IsShortcutMenuChild = true,
        Shortcut = enuKeys.Delete,
        ImageKey=CoreImageKeys.MENU_DELETE_GKDS)]
    sealed class IGKD2DDrawingDeleteLayerMenu : IGKD2DDrawingLayerMenuBase
    {
            protected override bool PerformAction()
            {
                if (this.CurrentDocument.Layers.Count > 1)
                {
                    this.CurrentDocument.Layers.Remove(this.CurrentLayer);
                    return true;
                }
                return false;
            }
    }
}
