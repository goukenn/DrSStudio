

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PropertyObjectTool.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PropertyObjectTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.PropertyWindowAddIn.Tools
{
    using IGK.ICore.Tools;
    using IGK.DrSStudio.PropertyWindowAddIn.WinUI;
    using ICore.Resources;
    [CoreTools("Tool.PropertyToolWindows", 
        ImageKey= CoreImageKeys.MENU_PROPERTY_WINDOW_GKDS)]
    sealed class PropertyObjectTool : CoreToolBase
    {
        private static PropertyObjectTool sm_instance;
        private PropertyObjectTool()
        {
        }
        public static PropertyObjectTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static PropertyObjectTool()
        {
            sm_instance = new PropertyObjectTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new XPropertyUserControl() { 
                CaptionKey = "title.PropertyToolWindows",
                Tool = this,
                 ToolDocument = CoreResources.GetDocument(this.ToolImageKey),
            };
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
        private void workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            (this.HostedControl as XPropertyUserControl).ObjectToConfigure = null;
            if (e.OldElement !=null)
            UnregisterSurfaceEvent(e.OldElement);
            if (e.NewElement != null) 
              RegisterSurfaceEvent(e.NewElement);
        }
        private void RegisterSurfaceEvent(ICoreWorkingSurface coreWorkingSurface)
        {
            ICoreWorkingConfigElementSurface vs = coreWorkingSurface as
                ICoreWorkingConfigElementSurface;
            if (vs != null)
            {
                vs.ElementToConfigureChanged += vs_ElementToConfigureChanged;
                (this.HostedControl as XPropertyUserControl).ObjectToConfigure = vs.ElementToConfigure;
            }
        }
        void vs_ElementToConfigureChanged(object sender, EventArgs e)
        {
            ICoreWorkingConfigElementSurface vs = this.CurrentSurface as
            ICoreWorkingConfigElementSurface;
            if (vs != null)
            {
                (this.HostedControl as XPropertyUserControl).ObjectToConfigure = vs.ElementToConfigure;
            }
        }
        private void UnregisterSurfaceEvent(ICoreWorkingSurface coreWorkingSurface)
        {
            ICoreWorkingConfigElementSurface vs = coreWorkingSurface as
             ICoreWorkingConfigElementSurface;
            if (vs != null)
            {
                vs.ElementToConfigureChanged -= vs_ElementToConfigureChanged;
            }
        }
    }
}

