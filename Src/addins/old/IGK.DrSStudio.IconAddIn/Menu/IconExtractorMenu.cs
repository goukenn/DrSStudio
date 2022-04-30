

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IconExtractorMenu.cs
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
file:IconExtractorMenu.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Menu;
using IGK.DrSStudio.ContextMenu;
namespace IGK.DrSStudio.XIconAddin
{
    using IGK.DrSStudio;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Menu;
    [CoreMenu("Tools.IconExtractor", 40, SeparatorBefore=true)]
    class IconExtractorMenu : CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            IconLibExplorerControl ctr = new IconLibExplorerControl();
            using (ICoreDialogForm frm = this.Workbench.CreateNewDialog (ctr))
            {
                frm.Title = CoreSystem.GetString(IconConstant.EXTRACTOR_TITLE);
                frm.StartPosition = enuFormStartPosition.CenterParent;
                ctr.Disposed += new EventHandler(ctr_Disposed);
                frm.ShowDialog();
            }
            ctr.Dispose();
            return true;
        }
        void ctr_Disposed(object sender, EventArgs e)
        {
            IconLibExplorerControl ctr = sender as IconLibExplorerControl;
            if (ctr.FindForm() != null)
            {
                ctr.FindForm().Close();
            }
        }
    }
}

