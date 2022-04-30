using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Web
{
    /// <summary>
    /// represent interface that will host a document
    /// </summary>
    public interface ICoreWebDocumentHost
    {
        CoreXmlWebDocument Document { get; set; }
    }
}
