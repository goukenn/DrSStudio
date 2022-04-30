

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingLayerKeyManagerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D.Tools;
using IGK.ICore.Actions;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDrawingLayerKeyManagerTool.cs
*/
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Layer
{
    [IGKD2DDrawingLayerMenuAttribute("KeyManager", 0,
       Shortcut = enuKeys.Control | enuKeys.L,
       IsVisible = false,
       IsShortcutMenuChild = false)]
    sealed class IGKD2DDrawingLayerKeyManagerTool : IGKD2DDrawingLayerMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.Workbench is ICoreWorkbenchMessageFilter m)
            CoreActionBase.StartFilteringAction(
             m,
             IGKD2DLayerKeyManagerTool.Instance);
            return true;
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
    }
}

