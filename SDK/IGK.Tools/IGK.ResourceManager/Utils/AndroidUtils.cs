using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.ResMan
{
    class AndroidUtils
    {
        internal static void ExportToFile(string p, Dictionary<string,string > values)
        {
            StringBuilder sb = new StringBuilder ();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.Encoding = UTF8Encoding.UTF7 ;
            setting.OmitXmlDeclaration = true;
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            XmlWriter xwriter = XmlWriter.Create(sb, setting );

            xwriter.WriteStartElement("resources");
            foreach (KeyValuePair<string,string> item in values)
            {
                xwriter.WriteStartElement("string");
                xwriter.WriteAttributeString("name", item.Key);
                xwriter.WriteValue(escapestring(item.Value));
                xwriter.WriteEndElement();
            }
            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();
            File.WriteAllText(p, sb.ToString());
        }

        private static string escapestring(string p)
        {
            p = Regex.Replace(p, "'", (r) =>
            {
                if (r.Index > 0)
                {
                    var c = p[r.Index - 1];
                    if (c == '\\')
                    {
                        return "'";
                    }
                }
                return "\\'";
            });
           

            return p;
        }
    }
}
