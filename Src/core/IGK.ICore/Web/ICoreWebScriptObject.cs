using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Web
{
    public interface ICoreWebScriptObject : ICoreWebDocumentHost
    {
        ICoreDialogForm Dialog { get; }
        /// <summary>
        /// reload the document
        /// </summary>
        void Reload();
        /// <summary>
        /// submit data
        /// </summary>
        /// <param name="data"></param>
        void Submit(object data);
    }
}
