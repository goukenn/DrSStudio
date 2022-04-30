

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayoutManagerPanelPropertyEventArgs.cs
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
file:LayoutManagerPanelPropertyEventArgs.cs
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
namespace IGK.DrSStudio.WinLauncher
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Tools ;
    /// <summary>
    /// 
    /// Represent a core layout manager extender tool 
    /// </summary>
    public class LayoutManagerPanelPropertyEventArgs : CoreToolEventArgs 
    {
        private WinLauncherLayoutManager.WinLauncherToolPanelProperty m_Panel;
        internal WinLauncherLayoutManager.WinLauncherToolPanelProperty Panel
        {
            get { return m_Panel; }
        }
        internal LayoutManagerPanelPropertyEventArgs(ICoreTool tool, WinLauncherLayoutManager.WinLauncherToolPanelProperty panel )
            :base(tool )
        {
            this.m_Panel = panel;
        }
    }
}

