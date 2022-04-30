using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    /// <summary>
    /// represent a log host message
    /// </summary>
    interface ICoreLog
    {
        void WriteError(string message);
    }
}
