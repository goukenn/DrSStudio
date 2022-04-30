using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    public class XamarinProjectItemGroup : XamarinProjectXmlElementBase 
    {
        List<string> m_keys;
        public XamarinProjectItemGroup() : base (XamarinConstant .PROJECT_PROPERTY_ITEM_GROUP_TAG )
        {
            m_keys = new List<string>();
        }
        internal bool Contains(string key)
        {
            return m_keys.Contains(key);
        }
        public override CoreXmlElement CreateChildNode(string tagName)
        {
            if (Enum.IsDefined (typeof (enuXamarinBuildMode), tagName))
            {
                //XamarinProjectItemFile f = new XamarinProjectItemFile(tagName );
               var g =  XamarinProjectItemFile.CreateFile((enuXamarinBuildMode)Enum.Parse(typeof(enuXamarinBuildMode), tagName),
                    string.Empty);
               return g;
            }
            return base.CreateChildNode(tagName );
        }
        public override bool AddChild(CoreXmlElementBase c)
        {
            IXamarinProjectItemGroupChild g = c as IXamarinProjectItemGroupChild;            
            if (g!=null)
            {
                if (Contains(g.Include) == false)
                {
                    if (base.AddChild(c)) {
                        m_keys.Add(g.Include);
                        return true;
                    }
                }
            }
            return false;
        }
      
    }
}
