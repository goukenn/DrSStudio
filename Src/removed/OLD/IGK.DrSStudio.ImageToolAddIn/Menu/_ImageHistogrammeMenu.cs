

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
namespace IGK.DrSStudio.Drawing2D.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("Image.Tools.HistogrammeMenu", 6,
        ImageKey="Menu_Histogram")]
    sealed class _ImageHistogrammeMenu : ImageMenuBase
    {
        protected override void InitMenu()
        {
            base.InitMenu();
            IGK.DrSStudio.Drawing2D.Tools.HistogramViewManagerTool.Instance.VisibleChanged += new EventHandler(Instance_VisibleChanged);
            this.MenuItem.Checked = IGK.DrSStudio.Drawing2D.Tools.HistogramViewManagerTool.Instance.Visible;
        }
        void Instance_VisibleChanged(object sender, EventArgs e)
        {
            this.MenuItem.Checked = IGK.DrSStudio.Drawing2D.Tools.HistogramViewManagerTool.Instance.Visible;
        }
        protected override bool PerformAction()
        {
            if (IGK.DrSStudio.Drawing2D.Tools.HistogramViewManagerTool.Instance.CanShow)
            {
                this.Workbench.LayoutManager.ShowTool(IGK.DrSStudio.Drawing2D.Tools.HistogramViewManagerTool.Instance);
            }
            return false;
        }
    }
}

