using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent an icore system application workbench
    /// </summary>
    public interface  ICoreApplicationWorkbench : ICoreWorkbench 
    {
        ///<summary>
        /// get or set the start form
        /// </summary>
        ICoreStartForm StartForm { get; }
        /// <summary>
        /// get the main form
        /// </summary>
        ICoreMainForm MainForm { get; }

        /// <summary>
        /// dispatch message
        /// </summary>
        /// <param name="message">message to dispatch</param>
        void DispatchMessage(ICoreMessage message);
    }
}
