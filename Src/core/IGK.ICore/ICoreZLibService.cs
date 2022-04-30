using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// Zlib service
    /// </summary>
    public interface ICoreZLibService : ICoreServiceManager
    {
        bool Encode(string filename, params object[] data);
    }
}
