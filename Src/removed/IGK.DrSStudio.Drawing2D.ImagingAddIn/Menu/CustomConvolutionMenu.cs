

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CustomConvolutionMenu.cs
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
file:CustomConvolutionMenu.cs
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
    [IGK.DrSStudio.Menu .CoreMenu("Image.Convolutions.Custom", Int32.MaxValue, SeparatorBefore=true )]
    public class CustomConvolutionMenu : ImageMenuBase 
    {
        protected override bool PerformAction()
        {
            IGK.DrSStudio.Drawing2D.WinUI.XCustomConvolutionControl ctr = new IGK.DrSStudio.Drawing2D.WinUI.XCustomConvolutionControl();
            using (IGK.DrSStudio.WinUI.ICoreDialogForm frm = Workbench.CreateNewDialog(ctr))
            {
                ctr.Dock = System.Windows.Forms.DockStyle.Fill;
                ctr.ImageElement = this.ImageElement;
                frm.Caption = CoreSystem.GetString("DLG.CustomConvolution.Caption");
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ctr.ApplyConvolution(false);
                }
                else { 
                    //restore default
                    ctr.Restore();
                }
            }
            return false;
        }
    }
}

