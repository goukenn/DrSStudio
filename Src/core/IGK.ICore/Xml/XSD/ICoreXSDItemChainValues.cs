using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    public interface ICoreXSDItemChainValues
    {
        void SetValue(string key, object value, bool append);
        bool Contains(string key);
        IEnumerable<KeyValuePair<string, ICoreXSDItemValue>> GetEnumerator();
        /// <summary>
        /// get value of that type. create new if not exists and if is in chain
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        object GetValue(string v);
        void SetAttributeValue(string name, string attrName, string value);
    }
}
