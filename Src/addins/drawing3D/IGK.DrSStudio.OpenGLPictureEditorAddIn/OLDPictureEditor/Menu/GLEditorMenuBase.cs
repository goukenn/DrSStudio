

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorMenuBase.cs
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
file:GLEditorMenuBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Menu
{
    
using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.WinUI;
    public abstract class GLEditorMenuBase : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        public new GLEditorSurface CurrentSurface {
            get {
                return base.CurrentSurface as GLEditorSurface;
            }
        }
        protected override void RegisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.CurrentSurfaceChanged += _CurrentSurfaceChanged;
        }

        
        protected override void UnregisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            workbench.CurrentSurfaceChanged -= _CurrentSurfaceChanged;
           base.UnregisterBenchEvent(workbench);
        }

        private void _CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<DrSStudio.WinUI.ICoreWorkingSurface> e)
        {
        
            if (e.OldElement   is GLEditorSurface)
                UnRegisterSurfaceEvent(e.OldElement as GLEditorSurface);
            if (e.NewElement is GLEditorSurface)
                RegisterBenchEvent(e.NewElement as GLEditorSurface);
            this.SetupEnableAndVisibility();
        }
        protected virtual void RegisterBenchEvent(GLEditorSurface gLEditorSurface)
        {
        }
        protected virtual void UnRegisterSurfaceEvent(GLEditorSurface gLEditorSurface)
        {
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface != null;
        }
        protected override bool IsEnabled()
        {
            return this.CurrentSurface != null;
        }
    }
}

