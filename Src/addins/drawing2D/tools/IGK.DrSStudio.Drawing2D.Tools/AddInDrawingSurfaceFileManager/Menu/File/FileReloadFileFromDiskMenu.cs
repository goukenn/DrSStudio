

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FileReloadFileFromDiskMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Menu.File
{
    [DrSStudioMenu("File.ReloadFileFromDisk", 0x30)]
    class FileReloadFileFromDiskMenu : CoreApplicationSurfaceMenuBase
    {
        public new ICoreWorkingFilemanagerSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingFilemanagerSurface;
            }
        }
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.CurrentSurface !=null);
        }
        protected override bool PerformAction()
        {
            ICoreWorkingFilemanagerSurface s = this.CurrentSurface;
            if (s != null)
            {
                s.ReloadFileFromDisk();
                return true;
            }
            return false;
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.SetupEnableAndVisibility();
        }
    }
}
