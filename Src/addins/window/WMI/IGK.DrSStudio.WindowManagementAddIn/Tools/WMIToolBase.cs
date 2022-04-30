using IGK.ICore.Tools;
using IGK.Management.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Management.Tools
{
    class WMIToolBase : CoreToolBase
    {
        public new WMIEditorSurface CurrentSurface
        {
            get
            {
                return base.CurrentSurface as WMIEditorSurface;
            }
        }
    }
}
