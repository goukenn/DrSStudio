using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Xml
{
    public interface IXmlElement
    {
        IXmlChildCollections Childs { get; }
    }
}
