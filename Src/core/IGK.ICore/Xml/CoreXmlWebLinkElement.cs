using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Xml
{
    class CoreXmlWebLinkElement :  CoreXmlWebElement
    {
        public CoreXmlWebLinkElement():base("link")
        {

        }
        public override string RenderXML(IXmlOptions option)
        {
            string s = this["href"];
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            if ((option is CoreXmlWebOptions i) && (i.InlineData))
            {
                string fname = s.Substring(7);
                if (System.IO.File.Exists(fname))
                {

                    this["href"] = "data:text/css;base64, " + Convert.ToBase64String(System.IO.File.ReadAllBytes(fname));
                }
            }


            return base.RenderXML(option);
        }
    }
}
