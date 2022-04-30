using IGK.ICore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Registrable
{
    /// <summary>
    /// represent a web browser control
    /// </summary>
    public interface  IWebBrowserControl : ICoreControl , ICoreWebControl
    {
        event EventHandler Load;
    }
}
