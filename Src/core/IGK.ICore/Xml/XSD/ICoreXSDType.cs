using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// represent a xsd type
    /// </summary>
    public interface  ICoreXSDType: ICoreXSDObjectType , ICoreXSDRestrictType
    {
        string Name { get; }
        /// <summary>
        /// return the array of child that this item can host
        /// </summary>
        ICoreXSDType[] Items { get;}
        int MinOccurs { get;  }
        int MaxOccurs { get; }
        /// <summary>
        /// get the requested type name of this
        /// </summary>
        string TypeName { get;  }
        bool HasAttribute { get;  }
        bool IsRequired { get;}
        bool IsInSequence { get;}
        bool IsInChoice { get; }
        /// <summary>
        /// get the index of this item on parent . if no parent return -1
        /// </summary>
        int Index { get;}

        /// <summary>
        /// get the real parent of this node
        /// </summary>
        ICoreXSDType Parent { get;}
        ICoreXSDAttribute[] GetAttributes();
        void SetRestriction(string baseType, IEnumerable<KeyValuePair<string, object>> r);
        /// <summary>
        /// get attribute by name
        /// </summary>
        /// <param name="attrName"></param>
        /// <returns></returns>
        ICoreXSDAttribute GetAttribute(string attrName);
        /// <summary>
        /// get the chain elements of this type. 
        /// </summary>
        /// <param name="sourceType">sourceType represent the source type that will be used to get Item not present when.can be null if already call GetChain </param>
        /// <returns></returns>
        ICoreXSDItemChain GetChain(ICoreXSDSourceType sourceType);
    }
}
