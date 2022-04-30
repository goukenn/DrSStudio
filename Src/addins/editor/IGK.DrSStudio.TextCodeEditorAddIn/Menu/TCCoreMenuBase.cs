

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TCCoreMenuBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.DrSStudio.TextCodeEditorAddIn.WinUI;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.TextCodeEditorAddIn.Menu
{
    public class TCCoreMenuBase : CoreApplicationSurfaceMenuBase
    {
        public new TCEditorSurface CurrentSurface {
            get {
                return base.CurrentSurface as TCEditorSurface;
            }
        }       
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.SetupEnableAndVisibility();
        }
        
    }
}
