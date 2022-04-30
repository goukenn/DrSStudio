

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingLayerMoveUp.cs
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
file:MoveUp.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Layer
{
    [IGKD2DDrawingLayerMenuAttribute("resetMatrix", 0x50   )]
    class IGKD2DDrawingLayerResetTransformMenu : IGKD2DDrawingLayerMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.CurrentLayer is Core2DDrawingLayer)
            {
                (this.CurrentLayer as Core2DDrawingLayer).ResetMatrix();
                this.CurrentSurface.RefreshScene();
                return true;
            }
            return false;
        }
    }
}

