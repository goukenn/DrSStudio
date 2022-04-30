
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Web.WinUI
{
    using IGK.ICore.Web;
    using IGK.ICore.WinUI;
    using IGK.ICore.Xml;

    public interface ICoreWebDialogProvider : ICoreWebDocumentHost
    {
        ICoreWebScriptObject OjectForScripting { get; }
        ICoreDialogForm Dialog { get; set; }
        bool UpdateData(string data);
    }
}
