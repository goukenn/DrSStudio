using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Xml
{
    public class CoreXmlWebOptions : CoreXmlSettingOptions 
    {
        private List<string> m_closedTag;
        public bool InlineData { get; set; }

        public CoreXmlWebOptions():base()
        {
            this.m_closedTag = new List<string>();
            this.InlineData = false;
            this.InitCloseTag();
        }
        protected virtual void InitCloseTag() {
            this.m_closedTag.Add("select");
            this.m_closedTag.Add("title");
            this.m_closedTag.Add("html");
            this.m_closedTag.Add("body");
            this.m_closedTag.Add("html");
            this.m_closedTag.Add("script");
            this.m_closedTag.Add("div");
           // this.m_closedTag.Add("input");
            this.m_closedTag.Add("button");
            this.m_closedTag.Add("textarea");
            this.m_closedTag.Add("iframe");
            this.m_closedTag.Add("ul");
            this.m_closedTag.Add("td");
            this.m_closedTag.Add("th");
            this.m_closedTag.Add("table");
        }

        public override bool MustCloseTag(string tagName)
        {
            return (!string.IsNullOrEmpty(tagName) && this.m_closedTag.Contains(tagName.ToLower()));            
        }
    }
}
