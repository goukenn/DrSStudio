using IGK.ICore;
using IGK.ICore.Xml;
using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.Xml;

namespace IGK.DrSStudio.ColladaEngine
{
    internal class ColladaType : ColladaTypeBase, ICoreXSDDeclaringType
    {
        private enuXSDType m_typesimple;

        public ColladaType(string name):base()
        {
            this.Name = name ;
        }

        public ColladaType(string name, enuXSDType simple) : this(name)
        {
            this.m_typesimple = simple;
        }

        public enuXSDType XSDType
        {
            get
            {
                return this.m_typesimple;
            }
        }

        public override string ToString()
        {
            return $"ColladaType[{Name}, {m_typesimple}]";
        }

        internal ColladaType LoadDef(XmlReader xmlReader)
        {
            this.Infos = xmlReader.GetAttributesDictionary();
            return this;
        }
    }
}