
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// android resource builder class
    /// </summary>
    public class AndroidResourceBuilder
    {
        public static void Store(string filename, string p)
        {
            using (StreamWriter v_sw = new StreamWriter(File.Create(filename), Encoding.UTF8 ))
            {
                v_sw.WriteLine(CoreXmlPreprocessor.XMLDEFINITION_UTF8_VERSION1);
                v_sw.Write(p);
                v_sw.Flush();
            }
        }

        public static CoreXmlElement CreateResourcesNode()
        {
            return CoreXmlElement.CreateXmlNode("resources"); 
        }
    }
}
