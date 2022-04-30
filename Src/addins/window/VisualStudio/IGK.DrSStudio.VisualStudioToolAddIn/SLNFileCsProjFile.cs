using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Window.VS
{
    using IGK.ICore;
    using IGK.ICore.WinUI;

    public class SLNFileCsProjFile : CoreXmlElement
    {
        public SLNFileCsProjFile():base("Project")
        {

        }
        public static SLNFileCsProjFile Open(string filename)
        {
            if (File.Exists(filename) == false) return null;
            CoreXmlElement dummy = CoreXmlElement.CreateXmlNode("dummy");
            dummy.LoadString(File.ReadAllText(filename));

            SLNFileCsProjFile rg = new SLNFileCsProjFile();
            var s = dummy.getElementsByTagName("Project").CoreGetValue<CoreXmlElementBase>(0)
                as CoreXmlElement ;
            if (s != null)
            {
                if (s.HasAttributes)
                    rg.CopyAttributes(s);
                foreach (CoreXmlElementBase  item in s.Childs)
                {
                    rg.AddChild(item);
                }
            }
            return rg;
        }
    }
}
