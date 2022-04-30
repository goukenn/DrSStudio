using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public interface  ICoreWorkingDisposableObject : ICoreDisposableObject 
    {
        event EventHandler Disposed;
    }
}
