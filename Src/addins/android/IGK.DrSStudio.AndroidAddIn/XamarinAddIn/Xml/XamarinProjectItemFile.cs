using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    public class XamarinProjectItemFile : XamarinProjectXmlElementBase, IXamarinProjectFile, IXamarinProjectItemGroupChild
    {
        public string Include { get { return this["Include"]; } set { this["Include"] = value; } }

        protected XamarinProjectItemFile(string tagName):base(tagName )
        {
            
        }
        public static XamarinProjectItemFile CreateFile(enuXamarinBuildMode buildmode, string filepath)
        {
            Type t = Type.GetType (string.Format("{0}.XamarinProject{1}ItemFile",
                MethodInfo.GetCurrentMethod().DeclaringType.Namespace,
                buildmode.ToString()),false, true) ;
            XamarinProjectItemFile g = null;
            if ((t!=null) && !t.IsAbstract )
                g = (XamarinProjectItemFile)t.Assembly.CreateInstance (t.FullName );
            else 
                g = new XamarinProjectItemFile(buildmode.ToString());
            g.Include = filepath;
            return g;
        }
    }
}
