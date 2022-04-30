

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RSShowOwnlyNewMatchMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.ResourcesManager.WinUI;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ResourcesManager.Menu.Action
{
    [RSMenuAction(RSConstant.RSMENU_ACTION + ".ShowOnlyNewMatch", RSConstant.RSMENU_ACTION_INDEX+1)]
    class RSShowOwnlyNewMatchMenu : RSMenuActionBase 
    {
        protected override bool PerformAction()
        {
            this.CurrentSurface.ShowOnlyNewMatch = !this.CurrentSurface.ShowOnlyNewMatch;
            return false;
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            UnRegisterSurfaceEvent(e.OldElement  as XResourceSurface);
            RegisterSurfaceEvent(e.NewElement as XResourceSurface);
        }

        private void RegisterSurfaceEvent(XResourceSurface surface)
        {
            if (surface == null)
                return;
            surface.ShowOnlyNewMatchChanged += surface_ShowOnlyNewMatchChanged;
            this.MenuItem.Checked = CurrentSurface.ShowOnlyNewMatch;
        }

        private void UnRegisterSurfaceEvent(XResourceSurface surface)
        {
            if (surface == null)
                return;
            surface.ShowOnlyNewMatchChanged -= surface_ShowOnlyNewMatchChanged;
        }

        void surface_ShowOnlyNewMatchChanged(object sender, EventArgs e)
        {
            this.MenuItem.Checked = CurrentSurface.ShowOnlyNewMatch;
        }
        
        
    }
}
