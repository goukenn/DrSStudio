using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Balafon.Xml
{
    public class BalafonProjectItemGroup : BalafonCoreXmlElement
    {

        List<string> m_keys;
        public virtual bool Contains(string file) {
            return !string.IsNullOrEmpty (file) && m_keys.Contains(file);            
        }
        public BalafonProjectItemGroup():base(BalafonConstant.PROJECT_ITEM_GROUP_TAG)
        {
            m_keys = new List<string>();
        }
        public override ICore.Xml.CoreXmlElement CreateChildNode(string tagName)
        {
            return new BalafonProjectItemFile(tagName);
        }
        public override bool AddChild(ICore.Xml.CoreXmlElementBase c)
        {
            if (c is BalafonProjectItemFile)
            {
                var b = c as BalafonProjectItemFile;
                if ((b.Include == null) || !this.Contains(b.Include))
                {
                    this.m_keys.Add(b.Include);
                    return base.AddChild(c);
                }
            }
            return false;
        }
    }
}
