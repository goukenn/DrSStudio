
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Web
{
    public static class CoreWebExtensions
    {

        //string extension
        public static string toUri(this string link) {
            return link.Replace("\\", "/");
        }

        //controls extension
        public static void IGKApplyBrowserDocumentText(this IIGKWebBrowserControl webControl,
            CoreXmlWebDocument document)
        {
            document.ForWebBrowserDocument = true;
            if (webControl.IsBodyDefined)
            {
                webControl.setBodyInnerHtml(document.Body.RenderInnerHTML(null));
            }
            else
            {
                webControl.DocumentText = document.RenderXML(null);
            }
        }
        public static void SetDocument(this IIGKWebBrowserControl webControl, CoreXmlWebDocument doc)
        {
            if (!webControl.IsBodyDefined)
            {
                string g = doc.RenderXML(null);
                webControl.DocumentText = g;
            }
            else
            {
                webControl.setBodyInnerHtml(doc.Body.RenderInnerHTML(null));
                webControl.InvokeScript("eval", new string[] { "(function t(){window.igk.system.evalScript(document.body); return '1'; })();" });
            }
        }
        public static CoreXmlElement addBr(this CoreXmlElement item, string id = null)
        {
            var f = item.Add("br");
            return f;
        }
        public static CoreXmlElement addScript(this CoreXmlElement item, string id = null)
        {
            var f = item.Add("script");
            f["type"] = "text/javascript";
            f["language"] = "javascript";
            f["id"] = id;
            return f;
        }
        public static CoreXmlElement addForm(this CoreXmlElement item, string id = null)
        {
            var f = item.Add("form");
            f["id"] = id;
            f["method"] = "GET";
            f["action"] = ".";
            f["enctype"] = "multipart/form-data";
            return f;
        }
        public static CoreXmlElement getParentNode(this CoreXmlElement item)
        {
            return item.Parent as CoreXmlElement;
        }
        public static CoreXmlElement addDiv(this CoreXmlElement item, string id = null)
        {
            CoreXmlElement dv = CoreXmlElement.CreateXmlNode("div");
            item.Childs.Add(dv);
            var f = dv;// item.Childs.Add(dv);// à,.Add (.Add(dv);
            f["id"] = id;
            return f;
        }
        public static CoreXmlElement addSLabelInput(this CoreXmlElement item, string id = null, string type = "text", string value = "")
        {
            var f = item;
            f.add("label").setAttribute("for", id).Content = ("lb." + id).R();
            f.addInput(id, type, value);
            return f;
        }
        public static CoreXmlElement addSLabelSelect(this CoreXmlElement item, string id = null, Dictionary<string, string> values = null,
            bool t = false, object s = null)
        {
            var f = item;
            f.add("label").setAttribute("for", id).Content = ("lb." + id).R();
            var sl = f.add("select").setId(id);
            if (values != null)
            {
                foreach (var b in values)
                {
                    sl.add("option").
                    setAttribute("value", b.Key).Content = b.Value;
                }
            }
            return f;
        }
        /// <summary>
        /// return the selected item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static CoreXmlElement addSelect(this CoreXmlElement item, string id = null, Dictionary<string, string> values = null)
        {
            var sl = item.add("select").setId(id);
            if (values != null)
            {
                foreach (var b in values)
                {
                    sl.add("option").
                    setAttribute("value", b.Key).Content = b.Value;
                }
            }
            return sl;

        }
        public static CoreXmlElement addSLabelTextarea(this CoreXmlElement item, string id = null, string value = null)
        {
            var f = item;
            f.add("label").setAttribute("for", id).Content = id.R();
            var sl = f.add("textarea").setId(id);
            if (value != null)
            {
                sl.Content = value;
            }
            return f;
        }
        public static CoreXmlElement addLiA(this CoreXmlElement item, string link = null)
        {
            var f = item.Add("li").addA(link);
            return f;
        }
        public static CoreXmlElement addStyle(this CoreXmlElement item)
        {
            var f = item.Add("style");
            if (f != null)
            {
                f["type"] = "text/css";
                return f;
            }
            return null;
        }
        public static CoreXmlElement addLabel(this CoreXmlElement item, string id = null,
            string @for = null,
            object @content = null)
        {
            var f = item.Add("label");
            f["id"] = id;
            f["for"] = @for;
            f.Content = @content;
            return f;
        }
        public static CoreXmlElement addInput(this CoreXmlElement item, string id = null,
           string type = "text",
           string value = null)
        {
            var f = item.Add("input");
            f["id"] = f["name"] = id;
            f["value"] = value;
            f["type"] = type;
            return f;
        }
        public static CoreXmlElement setClass(this CoreXmlElement item, string classValue)
        {
            item["class"] = classValue;
            return item;
        }
        public static CoreXmlElement setId(this CoreXmlElement item, string id)
        {
            item["id"] = item["name"] = id;
            return item;
        }
        public static CoreXmlElement setContent(this CoreXmlElement item, string value)
        {
            item.Content = value;
            return item;
        }
        public static CoreXmlElement addTable(this CoreXmlElement item)
        {
            return item.Add("table").setClass ("igk-table");
        }
        public static CoreXmlElement addHSep(this CoreXmlElement item)
        {
            var d = item.add("div");
            d.setClass("igk-horizontal-separator");
            return d;
        }
        public static CoreXmlElement add(this CoreXmlElement item, string tag)
        {
            if (item == null)
                return null;
            return item.Add(tag);
        }
        public static CoreXmlElement addSpace(this CoreXmlElement item)
        {
            item.AddChild(new CoreXmlElementText() { Value = "&nbsp;" });
            return item;
        }
        /// <summary>
        /// extension render as xml content
        /// </summary>
        /// <param name="item"></param>
        /// <param name="version"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string RenderToXml(this CoreXmlElement item, string version="1.0", string encoding="utf8")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine (string.Format ("<?xml version=\"{0}\" encoding=\"{1}\"?>", version, encoding ));
            sb.Append(item.RenderXML(null));
            return sb.ToString();
        }
        public static CoreXmlElement addA(this CoreXmlElement item, string link = null)
        {
            var a = item.add("a");
            if ((string.IsNullOrEmpty (link ) == false )&&(link.StartsWith ("javascript:")))
            {
                a["onclick"] = link;
                a["href"] = "javascript:void();";
            }
            else
                a["href"] = link;
            return a;
        }
        public static CoreXmlElement setAttribute(this CoreXmlElement item, string key, string value)
        {
            item[key] = value;
            return item;
        }


        public static string ToBase64Data(this ImageElement image) {
            string data = string.Format ("data:image/png;base64,{0}", image.Bitmap.ToStringData(false));
            return data;
        }

        public static void InitDocument(this CoreXmlWebDocument document)
        {
            InitDocument(document, CoreSystem.Instance.Workbench as ICoreWorkbenchDocumentInitializer);
        }

        public static void InitDocument(this CoreXmlWebDocument document, 
            ICoreWorkbenchDocumentInitializer workbench)
        {
            if ((workbench != null) && (document !=null))
                workbench.InitDocument(document);
        }

        public static void addPickFolder(CoreXmlElement frm, string Name)
        {
            var dd = frm.addDiv().setClass("input-group");
            dd.addInput(Name).setClass("fitw form-control");
            dd.add("span").setClass("input-group-btn").add("button")
                .setClass("btn btn-default")
                .setAttribute("type", "button")
                .setContent("...")
                .setAttribute("onclick", "javascript: this.form."+Name +".value = winex.pickFolder(this.form."+Name+".value); return false;");
        }
        public static void addPickFile(CoreXmlElement frm, string Name)
        {
            var dd = frm.addDiv().setClass("input-group");
            dd.addInput(Name).setClass("fitw form-control");
            dd.add("span").setClass("input-group-btn").add("button")
                .setClass("btn btn-default")
                .setAttribute("type", "button")
                .setContent("...")
                .setAttribute("onclick", "javascript: this.form." + Name + ".value = winex.pickFile(this.form." + Name + ".value); return false;");
        }

        public static bool GetBoolean(object p)
        {
            if (p == null) return false;

            string vp = p.ToString().ToLower();
            switch (vp)
            {
                case "1":
                case "on":
                case "true":
                    return true;
                default:
                    break;
            }
            return false;
        }
    }
}
