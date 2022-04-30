using System.Collections;
using System.Collections.Generic;

namespace IGK.ICore.Xml.XSD
{
    public interface ICoreXSDRestrictType
    {
        bool HasRestriction { get; }

        //IEnumerator GetEnumerator();
        IEnumerable<KeyValuePair<string, object>> GetRestriction();

    }
}