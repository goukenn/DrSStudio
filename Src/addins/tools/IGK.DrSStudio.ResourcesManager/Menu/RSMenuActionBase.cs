

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RSMenuActionBase.cs
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
file:RSMenuActionBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ResourcesManager.Menu
{
    using IGK.ICore.WinCore;
    using IGK.DrSStudio.ResourcesManager.WinUI;
    using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent Resource application menu
    /// </summary>
    public abstract class RSMenuActionBase : CoreApplicationSurfaceMenuBase 
    {
        public new XResourceSurface CurrentSurface
        {
            get {
                return base.CurrentSurface as XResourceSurface ;
            }
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            SetupEnableAndVisibility();
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
    }
}

