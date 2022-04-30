

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbManagerBaseMenu.cs
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
file:DbManagerBaseMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.DataBaseManagerAddIn.Menu
{
    using IGK.ICore;using IGK.DrSStudio.DataBaseManagerAddIn.WinUI;
    using IGK.DrSStudio.WinUI;
    /// <summary>
    /// represent the base menu for data application
    /// </summary>
    public class DbManagerBaseMenu : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        /// <summary>
        /// get the db menu surface
        /// </summary>
        public new IDbManagerSurface CurrentSurface {
            get {
                return base.CurrentSurface as IDbManagerSurface;
            }
        }
        protected override void RegisterBenchEvent(ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.CurrentSurfaceChanged += workbench_CurrentSurfaceChanged;
        }

       
        protected override void UnregisterBenchEvent(ICoreWorkbench workbench)
        {
            workbench.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(workbench);
        }
        private void workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
          this.SetupEnableAndVisibility();
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsVisible()
        {
            return true;
        }
    }
}

