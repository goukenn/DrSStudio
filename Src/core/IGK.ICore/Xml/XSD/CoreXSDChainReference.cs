using System;
using System.Collections.Generic;
using IGK.ICore.Xml.XSD;

namespace IGK.ICore.Xml
{
    internal class CoreXSDChainReference : ICoreXSDType
    {
        private ICoreXSDType m_chainType;
        private ICoreXSDType m_source;
        private bool m_equal ;
        public CoreXSDChainReference(ICoreXSDType chainType, ICoreXSDType source)
        {
            m_equal = chainType == source;
            this.m_chainType = chainType;
            this.m_source = source;
        }

        public bool HasAttribute => this.m_source.HasAttribute;

        public bool HasRestriction => this.m_source.HasRestriction;

        public bool IsInChoice=>m_chainType.IsInChoice;

        public bool IsInSequence=>m_chainType.IsInSequence;

        public bool IsRequired=>m_chainType.IsRequired;

        public ICoreXSDType[] Items=>m_source.Items;

        public int MaxOccurs=>m_chainType.MaxOccurs;

        public int MinOccurs=>m_chainType.MinOccurs;

        public string Name=>m_source.Name;

        public ICoreXSDType Parent=>m_chainType.Parent;

        public string TypeName=>m_source.TypeName;

        public int Index { get; internal protected set; }

        public ICoreXSDAttribute GetAttribute(string attrName)
        {
            throw new NotImplementedException();
        }

        public ICoreXSDAttribute[] GetAttributes()
        {
            return m_source.GetAttributes ();
        }

        public ICoreXSDItemChain GetChain(ICoreXSDSourceType sourceType)
        {
            return m_source.GetChain(sourceType);
        }

        public IEnumerable<KeyValuePair<string, object>> GetRestriction()
        {
            return m_source.GetRestriction();
        }

        public void SetRestriction(string baseType, IEnumerable<KeyValuePair<string, object>> r)
        {
        }
    }
}