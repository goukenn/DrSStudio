using IGK.DrSStudio.Android.Web;
using IGK.DrSStudio.Android.Xamarin.Settings;
using IGK.DrSStudio.Android.Xamarin.Web;
using IGK.DrSStudio.Android.Xamarin.Xml;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.JSon;
using IGK.ICore.Resources;
using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Android.Xamarin
{
    /// <summary>
    /// used in new xamarin document tab functions
    /// </summary>
    [ComVisible(true)]
    public class XamarinScriptRow : CoreWebScriptObjectBase, ICoreWebDialogProvider
    {
        public string getContact() {
            return "bondje.doue@igkdev.be";
        }
        public string getAuthor()
        {
            return "C.A.D. BONDJE DOUE";
        }
        public ICoreWebScriptObject OjectForScripting
        {
            get { return this; }
        }
        protected override void OnDocumentChanged(EventArgs eventArgs)
        {
            base.OnDocumentChanged(eventArgs);
        }
        private string m_page;
        public string getTabView() {
            if (string.IsNullOrEmpty(m_page))
                return string.Empty;
            var d = CoreXmlWebElement.CreateXmlNode("div");
            string txt = string.Empty;
#if DEBUG 
            
            txt = PathUtils.GetFileContent(@"D:\DRSStudio 9.0 Src\Src\addins\android\IGK.DrSStudio.AndroidAddIn\Xamarin\Resources\xamarin_new_tab_" + this.m_page + ".html", true);
#else 
            txt = CoreResources.GetResourceString("xamarin_new_tab_" + this.m_page, GetType().Assembly);
#endif

            d.addDiv().LoadString(
                CoreWebUtils.EvalWebStringExpression (txt, this));
            return d.RenderXML(new CoreXmlWebOptions());
        }
        public string navigate(string p) {
            this.m_page = p;
            return getTabView();
        }
        public string pickFolder(string v) {
            if (CoreSystem.Instance.Workbench != null)
            {
                return CoreCommonDialogUtility.PickFolder(
                    CoreSystem.Instance.Workbench, null, v);
            }
            else{
                using (FolderBrowserDialog db = new FolderBrowserDialog())
                {
                    db.SelectedPath = v;
                    if (db.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        return db.SelectedPath;
                    }
                }
            }
            return v;
        }
        
        public string getfilename() {
            return "com.igkdev.androidapp";
        }
        public string getoutputFolder() {
            return Environment.CurrentDirectory;
        }
        public string getPrefix()
        {
            return XamarinSettings.Instance.AppDefaultPrefix;
        }
        /// <summary>
        /// build project
        /// </summary>
        public void build()
        {
            if (this.Project != null)
            {
                XamarinBuilder sb = new XamarinBuilder();
                sb.Project = this.Project;
                sb.BuildProject();
            }
        }
        public void createproject(string jsondata) {
            CoreJSon c = new CoreJSon();
            var data = c.ToDictionary(jsondata);

            XamarinProjectXmlElement tc = new XamarinProjectXmlElement();
            var b = data.GetValue<string, object, string>("clName");

            tc.Name = data["clName"]?.ToString();
            tc.Prefix = data["clPrefix"]?.ToString();
            tc.AppTitle = data["clAppTitle"]?.ToString();
            tc.MainTheme = data["clMainTheme"]?.ToString();
            var g = AndroidSystemManager.GetAndroidTargetByName(data["clPlatformTarget"]?.ToString());
            

            tc.SystemPropertyGroup.TargetFrameworkVersion = XamarinWebUtils.GetTargetFrameworVersion(g) ?? XamarinSettings.Instance.DefaultTargetFrameWork;
            tc.SystemPropertyGroup.RootNamespace = data["clDefaultNS"].ToString();
            tc.SystemPropertyGroup.EmbedAssembliesIntoApk = "True";


           // tc.SystemPropertyGroup.Platform = data.GetValue<string, object, string>("clPlatform",AndroidSystemManager.GetLastAndroidTargetsName());
            tc.SystemPropertyGroup.AssemblyName = tc.Name.ToLower();
          //  tc.BuildProjet(data["clOutputFolder"].ToString());

            XamarinBuilder sb = new XamarinBuilder();
            sb.GenerateProjet(tc, data["clOutputFolder"].ToString(),
                CoreDictionaryExtensions.CoreGetValue<bool, object >(data, "clOverrideExisting", true ));
            this.Project = tc;
            if (this.Dialog != null)
            {
                
                this.Dialog.DialogResult = enuDialogResult.OK;
            }
        }


      
        public void cancel(string d) {
            if (this.Dialog != null)
                this.Dialog.DialogResult = enuDialogResult.Cancel;
        }
        public string getNS() {
            return XamarinSettings.Instance.AppDefaultNameSpace;
        }
        public string getAppTitle()
        {
            return XamarinSettings.Instance.AppDefaultTitle;
        }

        public XamarinProjectXmlElement Project { get; set; }


        #region plateform functions
        public string getinstalled_platforms()
        {
            return XamarinWebUtils.GetPlateformList(null);    
        }
        public string getThemes(string n) { 
            return AndroidWebUtils.GetThemes(n);
        }
        public string get_manifest_setting() {
            
            var ul = CoreXmlWebElement.CreateXmlNode("ul");
            //get manifest options settings
            ul.add("li").addInput("manifest_Internet", "checkbox", "Internet").Content = "Internet";

            return ul.RenderXML(null);
        }
        #endregion

      
    }
}
