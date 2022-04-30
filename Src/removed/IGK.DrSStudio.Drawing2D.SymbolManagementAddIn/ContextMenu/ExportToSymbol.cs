

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExportToSymbol.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ExportToSymbol.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Drawing2D.SymbolManagementAddIn.Tools;
namespace IGK.DrSStudio.Drawing2D.SymbolManagementAddIn.ContextMenu
{
    [CoreMenu("ExportTo.SymbolObject", 1000)]
    class ExportToSymbol : IGK.DrSStudio.Drawing2D.ContextMenu.Core2DContextMenuBase 
    {
        protected override bool PerformAction()
        {
            ICore2DDrawingLayeredElement[]  r =  this.CurrentSurface.CurrentLayer .SelectedElements.ToArray ();
            if (r.Length  >= 1)
            { 
                for (int i = 0; i < r.Length; i++)
			    {
                    SymbolManager.Register(r[i]);
			    }
            }
            return base.PerformAction();
        }
    }
}

