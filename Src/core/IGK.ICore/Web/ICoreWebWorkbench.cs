using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Web
{
    public interface ICoreWebWorkbench : ICoreWorkbench
    {
        ICoreWebDialogForm CreateWebBrowserDialog(ICoreWebScriptObject objectForScripting);
        ICoreWebDialogForm CreateWebBrowserDialog(ICoreWebScriptObject objectForScripting, string document);
    }
}
