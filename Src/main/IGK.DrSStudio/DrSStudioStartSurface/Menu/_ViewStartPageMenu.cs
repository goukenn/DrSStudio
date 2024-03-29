

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ViewStartPage.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ViewStartPage.cs
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
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
    
    using IGK.ICore.Menu;
    using IGK.DrSStudio.Menu;
    using IGK.ICore;
    [CoreMenu("View.StartPage", -100, ImageKey=CoreImageKeys.MENU_STARTPAGE_GKDS) ]
    class _ViewStartPageMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            if (this.Workbench.GetLayoutManager() is DrSStudioLayoutManager l)
                l.ShowStartPage();
            return false;
        }
        protected override bool IsVisible()
        {
            return base.IsVisible() && (this.Workbench != null) && (this.Workbench.GetLayoutManager() is DrSStudioLayoutManager); 
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.Workbench !=null) && (this.Workbench.GetLayoutManager() is DrSStudioLayoutManager); 
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
              var fb = workbench as ICoreSurfaceManagerWorkbench;
              if (fb != null)
              {
                  fb.SurfaceAdded += _workbench_SurfaceAdded;
                  fb.SurfaceRemoved += _workbench_SurfaceRemoved;
              }
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            if (workbench is ICoreSurfaceManagerWorkbench fb)
            {
                fb.SurfaceAdded -= _workbench_SurfaceAdded;
                fb.SurfaceRemoved -= _workbench_SurfaceRemoved;
            }
            base.UnregisterBenchEvent(workbench);
        }
        void _workbench_SurfaceAdded(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            if (this.Workbench.GetLayoutManager() is DrSStudioLayoutManager l)
            {
               this.MenuItem.Checked = l.StartSurface == e.Item;
           }
        }
        void _workbench_SurfaceRemoved(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            if (this.Workbench.GetLayoutManager() is DrSStudioLayoutManager l)            
            {
                this.MenuItem.Checked = !(l.StartSurface == e.Item );
            }
        }
    }
}

