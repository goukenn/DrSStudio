using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.XML
{
    public class BalafonDBBSchemaElement : CoreXmlElement
    {
        public BalafonDBBSchemaElement(string tagname):base(tagname )
        {
        }

        public override CoreXmlElement CreateChildNode(string tagName)
        {
            Type t = Type.GetType(string.Format("{0}.BalafonDBBSchema{1}", this.GetType().Namespace, tagName), false, true);

            if ((t != null) && (!t.IsAbstract))
                return t.Assembly.CreateInstance(t.FullName) as CoreXmlElement ;
             return null;
        }
    }
}
