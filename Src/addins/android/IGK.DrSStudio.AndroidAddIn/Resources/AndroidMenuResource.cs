using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Resources
{
    public class AndroidMenuResource : AndroidResourceItemBase 
    {
        public AndroidMenuResource():base("menu")
        {
            this.SetProperty("Name", "Menu");
            this.SetProperty(AndroidResourceItemBase.ATTRIBUTENAME,  "Menu");            
            this["xmlns:android"] = AndroidConstant.ANDROID_NAMESPACE;            
        }
        public override CoreXmlElement CreateChildNode(string tagName)
        {
            return base.CreateChildNode(tagName);
        }
    }
}
