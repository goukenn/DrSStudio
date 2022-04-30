using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.ICore.Xml
{
    public class CoreXmlWebScriptElement : CoreXmlWebElement
    {
        
        CoreXmlWebDocument m_webDocumentOwner;
        public CoreXmlWebDocument Document {
            get {
                return this.m_webDocumentOwner;
            }
            internal set {
                this.m_webDocumentOwner = value;
            }
        }

        public bool ForWebDocument { get; set; }

        public CoreXmlWebScriptElement():base("script")
        {
        }
        public override string RenderXML(IXmlOptions option)
        {
            StringBuilder sb = new StringBuilder();
            string src = this["src"];
            CoreXmlUriLink lnk = this["src"] as CoreXmlUriLink;
            string file = string.Empty;
            string inner = RenderInnerHTML(option);
            if (string.IsNullOrEmpty(src) && string.IsNullOrEmpty (inner))
                return string.Empty ;
            if (option?.Context == "inlinedata")
            {
                if ((lnk?.Value != null) && File.Exists((string)lnk.Value))
                {
                    string h = Convert.ToBase64String(File.ReadAllBytes((string)lnk.Value));
                    this["src"] = "data:text/javascript;base64," + h;
                }
            }
            return base.RenderXML(option);




            //force the content to be inline 
            //if ((this.m_webDocumentOwner != null) && ((this.m_webDocumentOwner.ForWebBrowserDocument))
            //    && Regex.IsMatch(src, "^file://(.)+$"))
            //{
            //    file = src.Substring(7);
            //    this["src"] = null;


            //    sb.Append("<script ");
            //    sb.Append(this.RenderAttributes(option));
            //    sb.Append(">");
            //    if (!string.IsNullOrEmpty(file) && File.Exists(file))
            //    {
            //        sb.Append(File.ReadAllText(file));
            //    }
            //    sb.Append(inner);
            //    sb.Append("</script>");
            //    //restore data
            //    if (lnk != null)
            //        this["src"] = lnk;
            //    else
            //        this["src"] = src;
            //    return sb.ToString();
            //}
            //else {                
            //    return base.RenderXML(option);
            //}
        }
        public override string RenderInnerHTML(IXmlOptions option)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CoreXmlUtility.GetStringValue(this.Content));
            foreach (CoreXmlElementBase item in this.Childs)
            {
                sb.Append(item.RenderXML(option));
            }
            return sb.ToString();
            
        }

    }
}
