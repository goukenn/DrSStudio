using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Window.VS
{
    /// <summary>
    /// represent a SLN file 
    /// </summary>
    public class SLNFile : CoreXmlElement
    {
        public static SLNFile Open(string filename)
        {
            SLNFile f = new SLNFile();
            return f;
        }

        
    }
}
