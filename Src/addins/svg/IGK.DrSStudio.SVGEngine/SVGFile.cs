using IGK.ICore.Xml;
using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SVGEngine
{
    public class SVGFile : 
        CoreXSDFileBase
    {
        public SVGFile()
        {
            this.SetManagerItemListener(SVGFileManager.Instance);
        }
        public override CoreXSDItem CreateItem(string name)
        {
            return this.CreateNewItem(SVGConstants.ROOT_NODE , name);
        }
        public override bool ContainsType(string typeName)
        {
            return SVGFileManager.Instance.ContainsType(typeName);
        }
        public override ICoreXSDType GetItemType(string typeName)
        {
            return SVGFileManager.Instance.GetItemType(typeName);
        }
        public override object CreateItem(ICoreXSDType type)
        {
            return SVGFileManager.Instance.CreateItem (type);
        }
        public override ICoreXSDDeclaringType GetTypeInfo(string typeName)
        {
            return null;
        }

        public override bool Open(string filename)
        {
           return false ;
        }

       

        public override bool Save(string filename)
        {
            var e = CoreXmlElement.CreateXmlNode(SVGConstants.ROOT_NODE);
            var s = SVGFileManager.Instance.GetItem(SVGConstants.ROOT_NODE);
            e["xmlns"] = SVGConstants.NAMESPACE;
            e["xmlns:xlink"] = "http://www.w3.org/1999/xlink";
            e["xmlns:xml"] = CoreXSDConstant.XML_XSD;// "http://www.w3.org/1999/xlink";
            //build data to export
            //e["version"] = this.Version;
            this.storeEntries(e, s);
            global::System.IO.File.WriteAllText(filename, e.RenderXML(null));
            return true;
        }

        public override ICoreXSDType GetItemDefinition(string v)
        {
            return SVGFileManager.Instance.GetItem(v);//base.GetItemDefinition(v);
        }

      
    }
}
