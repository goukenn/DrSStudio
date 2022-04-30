using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Xml
{
    class CoreXmlAttributeItem
    {
        private string m_Prefix;
        private string m_NameSpace;
        private CoreXmlAttributeValue m_Value;
        /// <summary>
        /// get or set the value of this XmlAttributeItem element
        /// </summary>
        public CoreXmlAttributeValue Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }

        public CoreXmlAttributeItem(string name, string prefix, string snamespace)
        {
            this.m_Name = name;
            this.m_Prefix = prefix;
            this.m_NameSpace = snamespace;
        }
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        /// <summary>
        /// get the prefix of this attribute
        /// </summary>
        public string Prefix
        {
            get { return m_Prefix; }
            set
            {
                if (m_Prefix != value)
                {
                    m_Prefix = value;
                }
            }
        }

        public string NameSpace
        {
            get { return m_NameSpace; }
            set
            {
                if (m_NameSpace != value)
                {
                    m_NameSpace = value;
                }
            }
        }


    }
}
