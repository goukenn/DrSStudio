using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Xml
{
    public interface IXmlChildCollections : System.Collections.IEnumerable
    {
        int Count { get; }
        CoreXmlElementBase this[int i]{get;}
        int IndexOf(CoreXmlElementBase element);
        void Remove(CoreXmlElementBase xmlElement);
        void Add(CoreXmlElementBase t);
        void Sort(Comparison<CoreXmlElementBase> callback);
    }
}
