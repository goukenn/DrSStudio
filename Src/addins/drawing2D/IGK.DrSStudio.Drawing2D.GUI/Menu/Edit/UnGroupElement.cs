

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UnGroupElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Menu;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.Menu;

namespace IGK.DrSStudio.Drawing2D.Menu.Edit
{
    [DrSStudioMenu(IGKD2DrawingConstant.MENU_UNGROUP, 0x41,  
        Shortcut=enuKeys.Control | enuKeys.Shift | enuKeys.G)]
    class UnGroupElementMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            GroupElement p = this.CurrentSurface.CurrentLayer.SelectedElements[0] as
                GroupElement ;
            if (p != null)
            {
                Matrix m = p.GetMatrix();
                ICore2DDrawingLayeredElement[] t = p.Elements.ToArray();
                this.CurrentSurface.CurrentLayer.Elements.Remove(p);
                
                for (int i = 0; i < t.Length; i++)
                {
                    t[i].MultTransform(m, enuMatrixOrder.Append);
                }
                p.Elements.Clear();
                p.Dispose();
                this.CurrentSurface.CurrentLayer.Elements.AddRange(t);
                return true;
            }

            return false;
        }
    }
}
