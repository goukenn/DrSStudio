using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Dispatch
{
    /// <summary>
    /// represent a mouse click dispatcher
    /// </summary>
    public interface ICoreMouseClickDispatcher : ICoreMouseEventDispatcher
    {        
        new void Invoke(CoreMouseEventArgs e);
    }
}
