using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Tools;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Menu
{
    /// <summary>
    /// menu only visible when a RSCTool is Visible
    /// </summary>
    public abstract class RSCMenuBase : CoreApplicationMenu
    {
        protected override bool IsEnabled()
        {
            return RSCTool.Instance.Visible;
        }
        protected override bool IsVisible()
        {
            return RSCTool.Instance.Visible;
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            RSCTool.Instance.VisibleChanged += Instance_VisibleChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            RSCTool.Instance.VisibleChanged -= Instance_VisibleChanged;
            base.UnregisterBenchEvent(Workbench);
        }

        void Instance_VisibleChanged(object sender, EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
       
    }
}
