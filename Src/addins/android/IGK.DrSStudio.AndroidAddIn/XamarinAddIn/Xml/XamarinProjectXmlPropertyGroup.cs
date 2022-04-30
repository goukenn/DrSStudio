using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    public class XamarinProjectXmlPropertyGroup : XamarinProjectXmlElementBase
    {
        private XamarinProjectXmlElement m_owner;

        //for special configuration node properties

        public string Configuration { get { return this.GetProperty("Configuration"); } set { this.SetProperty("Configuration", value); } }
        public string Platform { get { return this.GetElementProperty("Platform"); } set { this.SetElementProperty("Platform", value); } }
        public string ProjectTypeGuids { get { return this.GetElementProperty("ProjectTypeGuids"); } set { this.SetElementProperty("ProjectTypeGuids", value); } }
        public string ProjectGuid { get { return this.GetElementProperty("ProjectGuid"); } set { this.SetElementProperty("ProjectGuid", value); } }
        public string OutputType { get { return this.GetElementProperty("OutputType"); } set { this.SetElementProperty("OutputType", value); } }
        public string RootNamespace { get { return this.GetElementProperty("RootNamespace"); } set { this.SetElementProperty("RootNamespace", value); } }
        public string MonoAndroidResourcePrefix { get { return this.GetElementProperty("MonoAndroidResourcePrefix"); } set { this.SetElementProperty("MonoAndroidResourcePrefix", value); } }
        public string MonoAndroidAssetsPrefix { get { return this.GetElementProperty("MonoAndroidAssetsPrefix"); } set { this.SetElementProperty("MonoAndroidAssetsPrefix", value); } }
        public string AndroidUseLatestPlatformSdk { get { return this.GetElementProperty("AndroidUseLatestPlatformSdk"); } set { this.SetElementProperty("AndroidUseLatestPlatformSdk", value); } }
        public string AndroidApplication { get { return this.GetElementProperty("AndroidApplication"); } set { this.SetElementProperty("AndroidApplication", value); } }
        public string AndroidResgenFile { get { return this.GetElementProperty("AndroidResgenFile"); } set { this.SetElementProperty("AndroidResgenFile", value); } }
        public string AndroidResgenClass { get { return this.GetElementProperty("AndroidResgenClass"); } set { this.SetElementProperty("AndroidResgenClass", value); } }
        public string AssemblyName { get { return this.GetElementProperty("AssemblyName"); } set { this.SetElementProperty("AssemblyName", value); } }
        public string TargetFrameworkVersion { get { return this.GetElementProperty("TargetFrameworkVersion"); } set { this.SetElementProperty("TargetFrameworkVersion", value); } }
        public string AndroidManifest { get { return this.GetElementProperty("AndroidManifest"); } set { this.SetElementProperty("AndroidManifest", value); } }
        public string DebugSymbols { get { return this.GetElementProperty("DebugSymbols"); } set { this.SetElementProperty("DebugSymbols", value); } }
        public string DebugType { get { return this.GetElementProperty("DebugType"); } set { this.SetElementProperty("DebugType", value); } }
        public string Optimize { get { return this.GetElementProperty("Optimize"); } set { this.SetElementProperty("Optimize", value); } }
        public string OutputPath { get { return this.GetElementProperty("OutputPath"); } set { this.SetElementProperty("OutputPath", value); } }
        public string DefineConstants { get { return this.GetElementProperty("DefineConstants"); } set { this.SetElementProperty("DefineConstants", value); } }
        public string ErrorReport { get { return this.GetElementProperty("ErrorReport"); } set { this.SetElementProperty("ErrorReport", value); } }
        public string WarningLevel { get { return this.GetElementProperty("WarningLevel"); } set { this.SetElementProperty("WarningLevel", value); } }
        public string AndroidLinkMode { get { return this.GetElementProperty("AndroidLinkMode"); } set { this.SetElementProperty("AndroidLinkMode", value); } }
        public string ConsolePause { get { return this.GetElementProperty("ConsolePause"); } set { this.SetElementProperty("ConsolePause", value); } }
        public string EmbedAssembliesIntoApk { get { return this.GetElementProperty("EmbedAssembliesIntoApk"); } set { this.SetElementProperty("EmbedAssembliesIntoApk", value); } }
        public string AndroidUseSharedRuntime { get { return this.GetElementProperty("AndroidUseSharedRuntime"); } set { this.SetElementProperty("AndroidUseSharedRuntime", value); } }


       
        public XamarinProjectXmlPropertyGroup(XamarinProjectXmlElement xamarinProjectXmlElement)
            :base(XamarinConstant.PROJECT_PROPERTY_GROUP_TAG)
        {
            this.m_owner = xamarinProjectXmlElement;
        }
        //public override string RenderInnerHTML(IXmlOptions option)
        //{
        //    if (option == null)
        //        option = CreateXmlOptions();
        //    var sb = new StringBuilder ( base.RenderInnerHTML(option));
        //    if (this.Properties.Count > 0)
        //    {
        //        option.Depth++;
        //        bool t = !(sb.Length > 0);
        //        foreach (var item in this.Properties)
        //        {
        //            CoreXmlElement d = CoreXmlElement.CreateXmlNode(item.Key);
        //            d.Content = item.Value;
        //            if (t && option.Indent)
        //                sb.AppendLine();
        //            if (t == false)
        //            {
        //                option.Depth--;
        //                sb.Append(d.Render(option));
        //                option.Depth++;
        //            }
        //            else {
        //                sb.Append(d.Render(option));
        //            }
        //            t = true;
        //        }
        //    }
          
        //    option.Depth--;
        //    if (option.Indent && (this.Properties.Count > 0) && (option.Depth > 0))
        //    {
        //        sb.AppendLine();
        //        WriteDepth(sb, option.Depth);
        //    }
        //    return sb.ToString();
        //}
      
    }
}
