

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToOutlinePath.cs
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
file:_ConvertToOutlinePath.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    /// <summary>
    /// 
    /// </summary>
    [IGKD2DConvertToContextMenuAttribute("Outlinepath", 0)]
    class _ConvertToOutlinePathContextMenu : IGKD2DChildContextMenuBase
    {
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null) &&
                  (this.CurrentSurface.CurrentLayer.SelectedElements.Count > 0)
                  &&
                  CheckOverSingleElement();
        }
        protected override bool PerformAction()
        {
            Workbench.CallAction(ConverterConstant.MENU_CONVERTTO_OUTLINEPATH);
            return false;
        }
    }
}

