using IGK.DrSStudio.Balafon.Xml;
using IGK.ICore;
using IGK.ICore.JSon;
using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.Balafon.Menu.File
{
    [ComVisible(true)]
    public sealed class BalafonScriptRow : CoreWebScriptObjectBase, ICoreWebDialogProvider
    {
        BalafonProject tc = new BalafonProject();//internal project

        internal BalafonProject Project { get; set; }

        public ICoreWebScriptObject OjectForScripting
        {
            get { return this; }
        }

        public void createproject(string jsondata)
        {
            CoreJSon c = new CoreJSon();
            var data = c.ToDictionary(jsondata);

            
            var b = data.GetValue<string, object, string>("clName");
            var outfolder = data.GetValue<string, object, string>("clOutputFolder");
            if (string.IsNullOrEmpty(b)||
                string.IsNullOrEmpty(outfolder )
                )
                return;
            tc.Name = b;
            tc.Prefix = data["clPrefix"].ToString();
            tc.AppTitle = data["clAppTitle"].ToString();


           //tc.SystemPropertyGroup.AppLogin = data.GetValue<string>("clAppLogin");
           //tc.SystemPropertyGroup.AppLoginPwd = data.GetValue<string>("clAppLoginPwd");
            string v_filter = "clSysProp";
           foreach (var item in data.Keys)
           {

               if (item.StartsWith(v_filter ))
               {
                   var s = item.Substring(v_filter.Length);
                   var pr = tc.SystemPropertyGroup.GetType().GetProperty(s, BindingFlags.Public | BindingFlags.Instance);
                   if ((pr != null)&&(pr.CanWrite ))
                       pr.SetValue(tc.SystemPropertyGroup, data[item]);

               }
           }


            //var g = AndroidSystemManager.GetAndroidTargetByName(data["clPlatform"].ToString());
            ////, AndroidSystemManager.GetLastAndroidTargetsName());

            //tc.SystemPropertyGroup.TargetFrameworkVersion = XamarinWebUtils.GetTargetFrameworVersion(g) ?? XamarinSettings.Instance.DefaultTargetFrameWork;
            //tc.SystemPropertyGroup.RootNamespace = data["clDefaultNS"].ToString();
            //tc.SystemPropertyGroup.EmbedAssembliesIntoApk = "True";


            //// tc.SystemPropertyGroup.Platform = data.GetValue<string, object, string>("clPlatform",AndroidSystemManager.GetLastAndroidTargetsName());
            //tc.SystemPropertyGroup.AssemblyName = tc.Name.ToLower();
            //  tc.BuildProjet(data["clOutputFolder"].ToString());

            BalafonManager sb = new BalafonManager();
            sb.Generate (tc, outfolder,
                CoreDictionaryExtensions.CoreGetValue<bool, object>(data, "clOverrideExisting", true));
            this.Project = tc;//setup to external
            if (Dialog != null)
                Dialog.DialogResult = ICore.WinUI.enuDialogResult.OK;
        }

       
        public void cancel() {
            if (Dialog != null)
                Dialog.DialogResult = ICore.WinUI.enuDialogResult.Cancel;
        }

        public string navigate(string page) {

            var d = CoreUtils.GetRessourceData(IGK.ICore.IO.PathUtils.GetPath(
                string.Format (@"%sourcedir%\addins\web\IGK.DrSStudio.BalafonAddIn\Resources\balafon_new_page_{0}.html", page ))
                );
            
            if (d != null)
                return CoreWebUtils.EvalWebStringExpression(Encoding.UTF8.GetString(d), this);

            var m = GetType().GetMethod(string.Format(MethodBase.GetCurrentMethod().Name+"_" + page), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public | BindingFlags.IgnoreCase );
            if (m != null) {
                return (string)m.Invoke(this, null);
            }
            return string.Empty;
        }
        public string navigate_application() {
            return "Application";
        }
        public string getfilename() {
            return BalafonConstant.NEW_FILENAME;
        }
        public string getPrefix() {
            return CoreConstant.IGK_PREFIX;
        }
        public string getOutputFolder()
        {
            return Environment.CurrentDirectory;
        }
        public string pickFolder(string value)
        {
            if (CoreSystem.Instance.Workbench != null)
            {
                return CoreCommonDialogUtility.PickFolder(
                    CoreSystem.Instance.Workbench, null, value);
            }
            else
            {
                using (FolderBrowserDialog db = new FolderBrowserDialog())
                {
                    db.SelectedPath = value;
                    if (db.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        return db.SelectedPath;
                    }
                }
            }
            return string.Empty;
        }

        public string getproperty(string n) {
            var s = n;
            var pr = tc.SystemPropertyGroup.GetType().GetProperty(s, BindingFlags.Public | BindingFlags.Instance);
            if ((pr != null) && (pr.CanRead))
                pr.GetValue(tc.SystemPropertyGroup, null);
            return string.Empty;
        }
        public string getserveruri() {
            return "http://127.0.0.1:4545";
        }
    }
}
