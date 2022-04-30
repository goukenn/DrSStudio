
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Web
{
    using IGK.ICore.IO;
    using IGK.ICore.Web;

    class AndroidWinUIEditor
    {
        public static void InitDocument(CoreXmlWebDocument doc)
        {
            //add bootstrap if exist
            doc.AddLink(PathUtils.GetPath("%startup%/Sdk/lib/bootstrap/css/bootstrap.min.css"));
            doc.Head.addStyle().Content = Properties.Resources.android; 
        }
    }
}
