

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GoToNextLayer.cs
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
file:GoToNextLayer.cs
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
    [IGKD2DDrawingLayerMenuAttribute("GoToNextLayer", 0X21, IsShortcutMenuChild = true, Shortcut = enuKeys.Shift | enuKeys.Next)]
    class GoToNexLayer : IGKD2DDrawingLayerMenuBase
    {
        protected override bool PerformAction()
        {
            int i = this.CurrentDocument.Layers.IndexOf(this.CurrentLayer);
            if (i < this.CurrentDocument.Layers.Count -1)
            {
                this.CurrentDocument.CurrentLayer = this.CurrentDocument.Layers[i + 1];
                return true;
            }
            return false;
        }
    }
}

