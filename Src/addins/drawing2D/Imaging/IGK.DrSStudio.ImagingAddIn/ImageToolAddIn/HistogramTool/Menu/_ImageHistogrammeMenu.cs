

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ImageHistogrammeMenu.cs
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
using IGK.DrSStudio.Imaging.Tools;
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:_ImageHistogrammeMenu.cs
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
namespace IGK.DrSStudio.Imaging.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("Image.Tools.HistogrammeMenu", 6,
        ImageKey="Menu_Histogram")]
    sealed class _ImageHistogrammeMenu : ImagingMenuBase
    {
        protected override void InitMenu()
        {
            base.InitMenu();
            HistogramViewManagerTool.Instance.VisibleChanged += new EventHandler(Instance_VisibleChanged);
            this.MenuItem.Checked = HistogramViewManagerTool.Instance.Visible;
        }
        void Instance_VisibleChanged(object sender, EventArgs e)
        {
            this.MenuItem.Checked = HistogramViewManagerTool.Instance.Visible;
        }
        protected override bool PerformAction()
        {
            if (HistogramViewManagerTool.Instance.CanShow)
            {
                this.Workbench.LayoutManager.ShowTool(HistogramViewManagerTool.Instance);
            }
            return false;
        }
    }
}

