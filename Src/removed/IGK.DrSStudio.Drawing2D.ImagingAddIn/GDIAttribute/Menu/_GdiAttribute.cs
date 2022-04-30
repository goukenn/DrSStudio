

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _GdiAttribute.cs
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
file:_GdiAttribute.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ImageAddIn.GDIAttribute.Menu
{
    using WinUI;
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.Menu;
    [IGK.DrSStudio.Menu.CoreMenu ("Image.GdiAttributeTransform",
        20, 
        SeparatorBefore=true,
        ImageKey="MENU_GDI")]
    class _GdiAttribute : ImageMenuBase
    {
        protected override bool PerformAction()
        {
            XGdiConfigAttribute attrib = new XGdiConfigAttribute(this.ImageElement);
            using (IGK.DrSStudio.WinUI.ICoreDialogForm dialog = Workbench.CreateNewDialog(attrib))
            {
                dialog.CanMaximize = false;
                dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                dialog.Caption = CoreSystem.GetString("dlg.GDITranform");
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Workbench.CurrentSurface.Invalidate();
                }
            }
            return false;
        }
    }
}

