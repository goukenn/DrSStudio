using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Web
{
    public class CoreXmlHtmlOptions : CoreXmlSettingOptions
    {
        private List<string> m_closedTag;

        public CoreXmlHtmlOptions()
        {
            this.m_closedTag = new List<string>();

            this.m_closedTag.AddRange(new string[]{"a","div","script",
            "select",
            "title",
            "html",
            "body",
            "html",
            "script",
            "div",
            "th",
           // "input",
            "button",
            "textarea",
            "iframe",
            "td",
            "table"
            });
        }

        public override bool MustCloseTag(string tagName)
        {
           
            if (string.IsNullOrEmpty(tagName))
                return false;
            return this.m_closedTag.Contains(tagName.ToLower ());
        }

    }
}
