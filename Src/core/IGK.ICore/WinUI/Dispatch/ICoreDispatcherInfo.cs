using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Dispatch
{
    /// <summary>
    /// represent a dispatcher info
    /// </summary>
    public interface  ICoreDispatcherInfo
    {
        string EventName { get; }
        void AttachHandle(ICoreWorkingObject wObject, MulticastDelegate @delegate);
        void RemoveHandle(ICoreWorkingObject wObject, MulticastDelegate @delegate);
        /// <summary>
        /// invoke delegate with arguments
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Invoke(EventArgs t);
    }
}
