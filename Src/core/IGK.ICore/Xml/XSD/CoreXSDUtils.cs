using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IGK.ICore.Xml.XSD
{
    public static class CoreXSDUtils
    {

        public static ICoreXSDType[] GetXSDTypeItems(CoreXSDFileBase _file, ICoreXSDType item)
        {
            if (item.TypeName == null)
            {
                return item.Items;
            }
            else
            {
                return _file.GetItemType(_file.GetTypeName(item.TypeName))?.Items;
            }
        }


        internal static void FetchElement(CoreXSDFileBase _file,ICoreXSDType s, Func<object, object> p)
        {

            if((s==null) ||  (p==null))
                return ;
            Queue<CoreXSDFileEntryInfo> ee = new Queue<CoreXSDFileEntryInfo>();

            ee.Enqueue(new CoreXSDFileEntryInfo(s, 0));

            string v_key = string.Empty;
            ICoreXSDType[] g = null;
           // var v_level = 0;
         //   List<CoreXSDOutSortItem> o_dic = new List<CoreXSDOutSortItem>();
       
            CoreXSDFileEntryInfo v_e = CoreXSDFileEntryInfo.Empty;
            Dictionary<ICoreXSDType, int> rd = new Dictionary<ICoreXSDType, int>();
            string levelPath = string.Empty;
            while (ee.Count > 0)
            {
                v_e = ee.Dequeue();
                g = CoreXSDUtils.GetXSDTypeItems (_file, v_e.Item);

                if (g == null) continue;
                for (int i = v_e.Index; i < g.Length; i++)
                {
                    var item = g[i];
                    if (item is ICoreXSDItemContainer)
                    {
                     
                        ee.Enqueue(new CoreXSDFileEntryInfo(item, 0));
                        ee.Enqueue(new CoreXSDFileEntryInfo(v_e.Item, i + 1));
                        break;
                    }
                    p(item);
                }
              
            }


        }
        /// <summary>
        /// utility used to validate file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="xsd"></param>
        /// <returns></returns>
        public static bool ValidateFile(string fileName, string targetNamespace, string xsd) {

            //XmlSchemaSet schema = new XmlSchemaSet();
            //schema.Add(string.Empty, XmlReader.Create(new StringReader (xsd)));

            XmlReaderSettings setting = new XmlReaderSettings ();
            setting.Schemas.Add (targetNamespace, XmlReader.Create(new StringReader(xsd)));
            bool v_error = false ;
            setting.ValidationEventHandler += (o,e)=> {
                switch (e.Severity)
                {
                    case XmlSeverityType.Error:
                        v_error = true;
                        break;
                    case XmlSeverityType.Warning:
                        break;
                    default:
                        break;
                }
            };
            setting.ValidationType = ValidationType.Schema;

            string xml = File.ReadAllText (fileName);

            XmlReader sb = XmlReader.Create (new StringReader (xml), setting);
            while (sb.Read()) {
            }

            return v_error ;

        }

        private static void Setting_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
