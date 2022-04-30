

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MoveElementToBackContextMenu.cs
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
using IGK.DrSStudio.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Moves.cs
*/
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.DrSStudio.ContextMenu;
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
  
   
    [DrSStudioContextMenu("Drawing2DEdit.Disposition.MoveToBack", 2, ImageKey ="CMenu_BringBack" )]
    class MoveElementToBackContextMenu : DispositionChild
    {
        protected override bool PerformAction()
        {
            Workbench.CallAction(IGKElementTransformConstant.MENU_LAYER_MOVE_ELEMENT_TOSTART);
            
            return false;
        }
    }
 
}

