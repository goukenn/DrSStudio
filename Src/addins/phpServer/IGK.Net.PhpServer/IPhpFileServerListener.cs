using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.Net
{
    public interface IPhpFileServerListener
    {
        void SendResponse(PhpResponseBase phpAsyncResponse);
    }
}
