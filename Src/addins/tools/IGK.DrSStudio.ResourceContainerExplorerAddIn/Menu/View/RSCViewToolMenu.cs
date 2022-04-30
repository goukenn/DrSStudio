using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.View
{
    using IGK.DrSStudio.Tools;
    using IGK.ICore.Menu;

    [CoreMenu("View.RSCViewTool", 0x33)]
    sealed class RSCViewToolMenu : CoreViewToolMenuBase
    {
        public RSCViewToolMenu():base(RSCTool.Instance)
        {
        }       
    }
}
