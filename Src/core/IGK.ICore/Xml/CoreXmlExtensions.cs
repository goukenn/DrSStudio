using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.ICore.Xml
{
    public static class CoreXmlExtensions
    {
        public static string ToBase64Html(this string txt, string mimetype) {

            string h = Convert.ToBase64String(Encoding.Default.GetBytes(txt));
            return $"data:{mimetype};base64," + h;
        }


        public static Dictionary<string, string> GetAttributesDictionary(this XmlReader i) {
            i.MoveToElement();
            if (i.NodeType == XmlNodeType.None)
                i.Read();

            Dictionary<string, string> g = new Dictionary<string, string>();
            if (i.AttributeCount > 0) {
                i.MoveToFirstAttribute();
                g.Add(i.Name.ToLower(), i.Value);
                while (i.MoveToNextAttribute()) {
                    g.Add(i.Name.ToLower (), i.Value);
                }
                i.MoveToElement();
            }
            return g;
        
        }
    }
}
