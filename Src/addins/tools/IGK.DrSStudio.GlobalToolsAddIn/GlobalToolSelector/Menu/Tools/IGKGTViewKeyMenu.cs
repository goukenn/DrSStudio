

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKGTViewKeyMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKGTViewKeyMenu.cs
*/
using IGK.ICore.Menu;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Tools;
using IGK.ICore.Actions;
namespace IGK.DrSStudio.GlobalToolSelectorAddIn.Menu
{
    /// <summary>
    /// represent the global tool menu
    /// </summary>
    [DrSStudioMenu("Tools.SelectGlobalToolOnSurface", 0, 
        IsVisible=false, Shortcut=enuKeys.Control | enuKeys.T )]
    class IGKGTViewKeyMenu : CoreApplicationSurfaceMenuBase
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
            return false;
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.SetupEnableAndVisibility();
        }
        protected override bool PerformAction()
        {
            if (this.Workbench is ICoreWorkbenchMessageFilter m)
                CoreActionBase.StartFilteringAction(
      m,
      IGKGTShortcutManager.Instance);
            return false;
        }
    }
}

