
using IGK.DrSStudio.Drawing3D.FileBrowser.WinUI;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Drawing3D.FileBrowser.Menu
{
    public class FBSurfaceMenu : FBMenuBase 
    {
        public new FBControlSurface CurrentSurface
        {
            get {
                return base.CurrentSurface as FBControlSurface;
            }
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface != null;
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible();
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.SetupEnableAndVisibility();
        }
    }
}
