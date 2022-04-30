using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public interface ICoreMessageFilterApplication
    {
        void AddMessageFilter(ICoreMessageFilter filter);
        void RemoveMessageFilter(ICoreMessageFilter filter);
    }
}
