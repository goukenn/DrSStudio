using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    public class XamarinProjectItemFolder : XamarinProjectXmlElementBase, IXamarinProjectItemGroupChild, IXamarinProjectFile
    {
        public string Include { get { return this["Include"]; } set { this["Include"] = value; } }
        public XamarinProjectItemFolder():base("Folder")
        {
            
        }
    }
}
