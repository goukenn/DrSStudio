using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Web
{
    public interface  ICoreWebReloadDocumentListener
    {
        /// <summary>
        /// reload a document
        /// </summary>
        /// <param name="document"></param>
        void ReloadDocument(CoreXmlWebDocument document, bool attachDocument);
    }
}
