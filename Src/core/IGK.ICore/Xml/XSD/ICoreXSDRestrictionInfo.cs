using System.Collections.Generic;

namespace IGK.ICore.Xml
{
    public interface ICoreXSDRestrictionInfo: IEnumerable<KeyValuePair<string, object>>
    {
        string BaseType { get;}
    }
}