

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
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
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    [IGK.DrSStudio.Menu.CoreMenu("Image.GdiColorMatrix", 21, ImageKey ="Menu_matColor")]
    class _ColorMatrixMenu : ImageMenuBase 
    {
        protected override bool PerformAction()
        {
            XGdiMatrixControl ctr = new XGdiMatrixControl();
            using (IGK.DrSStudio.WinUI.ICoreDialogForm frm = Workbench.CreateNewDialog(ctr))
            {
                ctr.ImageElement = this.ImageElement;
                ctr.Dock = System.Windows.Forms.DockStyle.Fill;
                frm.Caption = CoreSystem.GetString("DLG.GidColorMatrix.Caption");
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ctr.ApplyMatrix(false);
                }
                else
                {
                    //restore default
                    ctr.Restore();
                }
            }
            return false;
        }
    }
}

