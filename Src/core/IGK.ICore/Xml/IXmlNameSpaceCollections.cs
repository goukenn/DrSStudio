using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Xml
{
    public interface IXmlNameSpaceCollections
    {
        int Count { get; }
        bool IsNamespacePrefix(string name);
        bool ContainsPrefix(string name);
        void Add(CoreXmlElement owner, string name, string value);
        void Remove(CoreXmlElement owner);
    }
}
