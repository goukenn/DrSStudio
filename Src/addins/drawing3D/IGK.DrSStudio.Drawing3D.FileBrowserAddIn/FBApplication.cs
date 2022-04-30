
using IGK.ICore;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing3D.FileBrowser
{
    [CoreApplication("FileBrowserApp")]
    /// <summary>
    /// represent a file browser application
    /// </summary>
    class FBApplication : IGK.ICore.WinCore.WinCoreApplication  
    {

        public override void AddMessageFilter(ICore.WinUI.ICoreMessageFilter messageFilter)
        {
            throw new NotImplementedException();
        }

        public override void RemoveMessageFilter(ICore.WinUI.ICoreMessageFilter messageFilter)
        {
            throw new NotImplementedException();
        }

        public override bool RegisterServerSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return true;
        }

        public override bool RegisterClientSystem(Func<CoreSystem> __initInstance)
        {
            return false;
        }
    }
}
