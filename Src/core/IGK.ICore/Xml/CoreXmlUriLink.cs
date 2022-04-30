using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml
{
    class CoreXmlUriLink : CoreXmlAttributeValue 
    {
        public CoreXmlUriLink(string value):base()
        {
            this.Value = value;
        }
        public override string GetValue()
        {
            string g = this.Value as string;
            if (string.IsNullOrEmpty(g))
                return null;
            if (File.Exists(g))
            { 
                string s =  Uri.UriSchemeFile+"://"+Path.GetFullPath (g).Replace ("\\","/");
                return s;
            }
            return string.Empty ;
        }
    }
}
