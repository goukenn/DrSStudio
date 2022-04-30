
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    internal static class DocExtensions
    {
        public static CoreXmlElement addForm(this CoreXmlElement element)
        {
            var frm = element.Add("form");
            frm["method"] = "GET";
            frm["action"] = ".";            
            return frm;
        }
    }
}
