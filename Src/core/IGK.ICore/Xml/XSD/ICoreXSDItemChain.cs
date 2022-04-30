using System.Collections.Generic;

namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// used to store chain of this data
    /// </summary>
    public interface ICoreXSDItemChain
    {
        ICoreXSDType this[string n] { get;}
        bool ContainKey(string name);
        string[] ChainKeys { get; }
        
        bool Check(List<string> list, ICoreXSDItemChainValues _chainvalues,
            ICoreXSDSourceType _sourceType, string parentName);
    }
}