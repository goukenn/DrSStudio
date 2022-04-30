using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    /// <summary>
    /// represent a stream resolver listerner
    /// </summary>
    public interface IWebBrowserHostStreamResolver
    {
        Stream Resolve(Uri uri);
    }
}
