using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    public interface ICoreXSDItemArray : ICoreXSDObjectType
    {
        ICoreXSDType Type { get; }
        /// <summary>
        /// get the number of items declared in this sub array
        /// </summary>
        int Count { get; }

        /// <summary>
        /// get if we can add item to this array
        /// </summary>
        bool CanAdd { get;}


        void AddItem(CoreXSDItem item);

        CoreXSDItem CreateNew();

        IEnumerator GetEnumerator();
    }
}
