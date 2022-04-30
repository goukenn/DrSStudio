using IGK.ICore.Menu;
using IGK.Management.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Management.Menu
{
    class WMIMenuBase : CoreApplicationMenu
    {
        public new WMIEditorSurface CurrentSurface {
            get {
                return base.CurrentSurface as WMIEditorSurface;
            }
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
