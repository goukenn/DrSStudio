using IGK.DrSStudio.Android.Xamarin.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Xamarin
{
    /// <summary>
    /// reprensent an evaluator object used to setup build data
    /// </summary>
    class XamarinBuilderEvaluator
    {
        private XamarinBuilder xamarinBuilder;
        public XamarinProjectXmlElement BindProject
        {
            get
            {
                return xamarinBuilder.Project;
            }
        }
        public XamarinBuilderEvaluator(XamarinBuilder xamarinBuilder)
        {
            this.xamarinBuilder = xamarinBuilder;
        }
        public string getUsings()
        {
            return "using System.IO;";
        }
        public string getDefaultNS()
        {
            return this.BindProject.SystemPropertyGroup.RootNamespace;
        }
        public string getAppName()
        {
            return this.BindProject.Name;
        }
        public string getClassName()
        {
            return Path.GetFileNameWithoutExtension(xamarinBuilder.FileName);
        }
        public string inherit()
        {
            string s = this.xamarinBuilder.BindNode.GetAttributeValue<string>("inherits");
            if (!string.IsNullOrEmpty(s))
                return ":" + s;
            return null;
        }
        public string getclassdef()
        {
            return null;
        }
        public string getAppLabel()
        {
            return this.BindProject.AppTitle;
        }
        public string getContact()
        {
            return "bondje.doue@igkdev.be";
        }
        public string getAuthor()
        {
            return "C.A.D. BONDJE DOUE";
        }
        public string getDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        public string getAppPrefix() {
            return this.BindProject.Prefix;
        }
        public string getMainTheme() {
            var s = this.BindProject.MainTheme;
            if (string.IsNullOrEmpty(s))
                return "null";

            return string.Format("\"@android:style/{0}\"", s);
        }
        public static string getMainSplashScreenTheme(){
            return "\"@style/splashscreen\"";
        }
    }
}
