

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MenuToolManager.cs
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
file:MenuToolManager.cs
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
using System.Windows.Forms ;
namespace IGK.DrSStudio.Menu
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Tools;
    [CoreMenu ("Tools.ToolsManager", 
        -1, 
        IsVisible=false, 
        Shortcut= Keys.Control | Keys.T )]
    sealed class MenuToolManager : CoreApplicationMenu
    {
        public new ICoreWorkingToolManagerSurface CurrentSurface
        {
            get
            {
                return base.CurrentSurface as ICoreWorkingToolManagerSurface;
            }
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override void RegisterBenchEvent(ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.CurrentSurfaceChanged += new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
        }
        protected override void UnregisterBenchEvent(ICoreWorkbench workbench)
        {
            workbench.CurrentSurfaceChanged -= new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
            base.UnregisterBenchEvent(workbench);
        }
        void workbench_CurrentSurfaceChanged(object o, CoreWorkingSurfaceChangedEventArgs e)
        {
            SetupEnableAndVisibility();
        }
        protected override bool PerformAction()
        {
            IGK.DrSStudio.Actions.CoreActionBase.StartFilteringAction(
                   this.Workbench,
                   ToolManager.Instance);
            return true;
        }
    }
}

