using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon
{
    /// <summary>
    /// scripting object for web browser control
    /// </summary>
    public interface IBalafonJSWebScriptObject
    {
        object callFunc(string cmd, string args);
    }
}
