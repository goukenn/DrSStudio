

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UndoableMenucAction.cs
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
file:UndoableMenucAction.cs
*/
    using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.Menu.Edit
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    public abstract class UndoableMenucAction : CoreApplicationMenu
    {
        public new ICoreWorkingUndoableSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingUndoableSurface;
            }
        }
        protected override bool IsVisible()
        {
            return base.CurrentSurface is ICoreWorkingUndoableSurface;
        }
        protected override bool IsEnabled()
        {
            return base.CurrentSurface is ICoreWorkingUndoableSurface;
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(workbench);
        }
        void workbench_CurrentSurfaceChanged(object o, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}

