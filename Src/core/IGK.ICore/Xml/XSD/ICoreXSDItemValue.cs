using System.Collections.Generic;

namespace IGK.ICore.Xml.XSD
{
    public  interface ICoreXSDItemValue
    {
        ICoreXSDItem Owner { get; }
        string Name { get; }
        object Value { get; }
        bool HasAttribute { get;  }

        ICoreXSDItemValue SetValue(string name, string value);
        ICoreXSDItemValue SetAttribute(string name, string attrName, string value);
        IEnumerable<KeyValuePair<string, string>> GetAttributes();
    }
}