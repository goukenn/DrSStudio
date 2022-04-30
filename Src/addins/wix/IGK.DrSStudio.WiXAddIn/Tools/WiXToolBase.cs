

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXToolBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.DrSStudio.WiXAddIn.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXToolBase.cs
*/
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn.Tools
{
    public abstract class WiXToolBase : CoreToolBase
    {
        public new WiXSurface CurrentSurface {
            get {
                return base.CurrentSurface as WiXSurface;
            }
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }
        private void workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>  e)
        {         
            if (e.OldElement   is WiXSurface)
            {
                UnRegisterWixSurface(e.OldElement  as WiXSurface);
            }
            if (e.NewElement  is WiXSurface)
            {
                RegisterWixSurface(e.NewElement as WiXSurface);
            }
        }
        protected virtual void RegisterWixSurface(WiXSurface surface)
        {
        }
        protected virtual void UnRegisterWixSurface(WiXSurface surface)
        {
        }
    }
}

