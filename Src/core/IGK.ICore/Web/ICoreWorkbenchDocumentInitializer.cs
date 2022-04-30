using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Web
{
    /// <summary>
    /// initialize the document
    /// </summary>
    public interface  ICoreWorkbenchDocumentInitializer
    {
        void InitDocument(CoreXmlWebDocument document); 
    }
}
