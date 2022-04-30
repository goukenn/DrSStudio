using IGK.ICore;
using IGK.ICore.Resources;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio
{
    class DrSStudioResourceManager : WinCoreResourceManager
    {
        public override void Init()
        {
            CoreResources.Register(CoreConstant.RES_APP_ICON, Properties.Resources.drsstudio_app );
        }
    }
}
