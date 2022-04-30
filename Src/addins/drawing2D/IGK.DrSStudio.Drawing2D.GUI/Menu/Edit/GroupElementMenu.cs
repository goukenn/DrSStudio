

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GroupElementMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Menu;
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
    [DrSStudioMenu(IGKD2DrawingConstant.MENU_GROUP, 0x40, Shortcut=enuKeys.Control | enuKeys.G)]
    class GroupElementMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {

            ICore2DDrawingLayeredElement[] t = this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
            if (t.Length > 1)
            {
                {
                    GroupElement g = new GroupElement();
                    for (int i = 0; i < t.Length; i++)
                    {
                        g.Elements.Add(t[i] as Core2DDrawingLayeredElement);
                    }

                    this.CurrentSurface.CurrentLayer.Elements.RemoveAll(t);
                    this.CurrentSurface.CurrentLayer.Elements.Add(g);
                    return true;
                }
            }
                return false;
            
        }
    }
}
