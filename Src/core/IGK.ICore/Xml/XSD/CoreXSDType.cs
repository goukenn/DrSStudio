using System;
using System.Xml;
using IGK.ICore.Xml.XSD;

namespace IGK.ICore.Xml
{
    internal class CoreXSDType: CoreXSDTypeBase, ICoreXSDType, ICoreXSDDeclaringType
    {
        private enuXSDType m_xsdType;

        public CoreXSDType(string n, enuXSDType simple)
        {
            this.Name = n;
            this.m_xsdType = simple;
        }
        public enuXSDType XSDType => m_xsdType ;

        internal protected virtual CoreXSDType LoadDef(XmlReader xmlReader)
        {
            return this;
        }
    }
    
}