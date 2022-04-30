

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreToolSurfaceContextMenuManager.cs
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
file:CoreToolSurfaceContextMenuManager.cs
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Tools
{
    /// <summary>
    /// this tool is used to setup surface with a valid context menu.
    /// </summary>
    [CoreTools("Tool.WinCore.ApplicationSurfaceContextMenuManager")]
    sealed class CoreToolSurfaceContextMenuManager : CoreToolBase 
    {
        private static CoreToolSurfaceContextMenuManager sm_instance;
        private CoreToolSurfaceContextMenuManager()
        {
        }
        public static CoreToolSurfaceContextMenuManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreToolSurfaceContextMenuManager()
        {
            sm_instance = new CoreToolSurfaceContextMenuManager();
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
            s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }
        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if ((e.OldElement != null)&&(e.OldElement.AppContextMenu !=null))
                e.OldElement.AppContextMenu = null;
            if ((e.NewElement != null) && (e.NewElement.AppContextMenu == null))
                e.NewElement.AppContextMenu = this.MainForm !=null?  this.MainForm.AppContextMenu : null;
        }
    }
}

