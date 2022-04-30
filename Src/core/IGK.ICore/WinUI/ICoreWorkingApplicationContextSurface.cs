
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    using IGK.ICore.WinUI.Dispatch;

    /// <summary>
    /// for context management
    /// </summary>
    public interface ICoreWorkingApplicationContextSurface : ICoreWorkingSurface 
    {
        /// <summary>
        /// get the dispatcher attacher to the context surface
        /// </summary>
        ICoreDispatcher Dispatcher { get; }
    }
}
