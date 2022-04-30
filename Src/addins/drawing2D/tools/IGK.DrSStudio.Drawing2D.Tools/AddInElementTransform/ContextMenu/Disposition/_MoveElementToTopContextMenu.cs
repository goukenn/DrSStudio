

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MoveElementToTopContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore.ContextMenu;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.ContextMenu;

namespace IGK.DrSStudio.Drawing2D.ContextMenu.Disposition
{
    
    [DrSStudioContextMenu("Drawing2DEdit.Disposition.MoveToTop", 3, 
        ImageKey=CoreImageKeys.CMENU_BRINGFRONT_GKDS)]
    class _MoveElementToTopContextMenu: DispositionChild
    {
        protected override bool PerformAction()
        {
            Workbench.CallAction(IGKElementTransformConstant.MENU_LAYER_MOVE_ELEMENT_TOEND);
            return false;
        }
    }
}
