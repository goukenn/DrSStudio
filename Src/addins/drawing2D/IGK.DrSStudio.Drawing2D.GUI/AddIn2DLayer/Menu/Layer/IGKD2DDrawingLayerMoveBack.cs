

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingLayerMoveBack.cs
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
file:MoveBack.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Layer
{
    [IGKD2DDrawingLayerMenuAttribute("MoveBack", 0x40, 
        IsShortcutMenuChild = true, Shortcut = enuKeys.Shift | enuKeys.Up,
        ImageKey = "menu_move_back", SeparatorBefore =true)]
    class IGKD2DDrawingLayerMoveBack : IGKD2DDrawingLayerMenuBase
    {
        protected override bool PerformAction()
        {
            int i = this.CurrentDocument.Layers.IndexOf(this.CurrentLayer);
            if (i > 0)
            {
                this.CurrentDocument.Layers.MoveToBack(this.CurrentLayer);
                this.CurrentSurface.RefreshScene();
                return true;
            }
            return false;
        }
    }
}

