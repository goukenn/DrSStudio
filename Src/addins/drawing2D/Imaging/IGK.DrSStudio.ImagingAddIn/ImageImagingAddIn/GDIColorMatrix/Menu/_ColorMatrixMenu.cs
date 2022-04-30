

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ColorMatrixMenu.cs
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
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ColorMatrixMenu.cs
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
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.Drawing2D.Menu
{
    [DrSStudioMenu("Image.GdiColorMatrix", 21, ImageKey ="Menu_matColor")]
    class _ColorMatrixMenu : ImageMenuBase 
    {
        protected override bool PerformAction()
        {
            using (XGdiMatrixControl ctr = new XGdiMatrixControl())
            {
                using (ICoreDialogForm frm = Workbench.CreateNewDialog(ctr))
                {
                    ctr.ImageElement = this.ImageElement;
                    ctr.CurrentSurface = this.CurrentSurface;
                    ctr.Dock = System.Windows.Forms.DockStyle.Fill;
                    frm.Title = "title.GdiColorMatrix.Caption".R();
                    if (frm.ShowDialog().Equals(enuDialogResult.OK))
                    {
                        ctr.ApplyMatrix(false);
                    }
                    else
                    {
                        //restore default
                        ctr.Restore();
                    }
                }
            }
            return false;
        }
    }
}

