using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// represent a primary xsd item array
    /// </summary>
    public abstract class CoreXSDItemArray : ICoreXSDItemArray
    {
        public abstract bool CanAdd { get; }
        public abstract int Count { get; }
        public abstract ICoreXSDType Type { get; }

        public abstract void AddItem(CoreXSDItem item);
        public abstract CoreXSDItem CreateNew();
        public abstract IEnumerator GetEnumerator();
    }
}
