
using IGK.DrSStudio.Android.AndroidThemeBuilder.WinUI;
using IGK.DrSStudio.Android.Entities;
using IGK.DrSStudio.Android.Web;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.Menu;
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

#pragma warning disable IDE1006
#pragma warning disable IDE0017

namespace IGK.DrSStudio.Android.AndroidThemeBuilder.Menu
{
    [CoreMenu("File.New.Android.ThemeFileEditor", 0x5)]
    public class NewAndroidThemeFileEditorMenu : CoreApplicationMenu , ICoreWebReloadDocumentListener
    {
        private ICore.WinUI.ICoreDialogForm m_Dialog;
        private AndroidTargetInfo m_TargetInfo; //primary target
        private AndroidTheme.AndroidThemeCollections m_themes;

        private string Theme { get;set;} // selected theme theme
        
        protected override bool PerformAction()
        {
            Theme = "Theme";//global theme name
            CoreXmlWebDocument v_doc = CoreXmlWebDocument.CreateICoreDocument();
            v_doc.InitAndroidWebDocument();
            v_doc.ForWebBrowserDocument = true;
            this.ReloadDocument(v_doc, false );

            using (var d = this.Workbench.CreateWebBrowserDialog(
                new AndroidResponse(this)
                {
                    Document = v_doc
                }))
            {
                d.Title = "title.android.newTheme".R();
                d.Size = new Size2i(300, 350);
                d.WebControl.SetReloadDocumentListener(this);
                this.m_Dialog = d;
                d.ShowDialog();
            }
            return false;
        }
        public string getParentList() {
            this.m_themes =null;
            StringBuilder sb = new StringBuilder();
            var s = AndroidTheme.GetAndroidThemes(this.m_TargetInfo?.TargetName);
            if (s != null)
            {
                //s.Sort( (a, b) =>
                //{
                //    return a.Name.CompareTo(b.Name);
                //});
                foreach (AndroidTheme item in s)
                {
                    sb.Append(string.Format("<option value=\"{0}\" {1}>{0}</option>", item.Name,
                        (this.Theme == item.Name) ? "selected=\"true\" " : ""
                        ));
                }
                this.m_themes = s;
            }
            else {
                sb.Append(string.Format("<option value=\"-1\" >No Parent </option>"));    
            }
            return sb.ToString();
        }
        public string getPlateformList() {
            string v_sb =  AndroidWebUtils.GetPlateformList(ref this.m_TargetInfo);
            if (string.IsNullOrEmpty(v_sb)) {
                return "<option value='-1'>" + "android.platformlist.failed".R() + "</option>";
            }
            return v_sb;       
        }

        public void ReloadDocument(CoreXmlWebDocument document, bool attachDocument)
        {
            document.Body.Clear();
            document.Body.LoadString(
             IGK.ICore.Web.CoreWebUtils.EvalWebStringExpression(

             Encoding.UTF8.GetString(Properties.Resources.newThemeEditor),
          
             this));
        }

        [ComVisible(true)]
        public class AndroidResponse : CoreWebScriptObjectBase, ICoreWebDialogProvider
        {
            private NewAndroidThemeFileEditorMenu owner;
            private string m_fileName;
            private string m_parentTemplate;
            private string m_platformName;

            public ICoreWebScriptObject OjectForScripting
            {
                get { return this; }
            }



            public AndroidResponse(NewAndroidThemeFileEditorMenu newAndroidThemeFileEditor)
            {
                this.owner = newAndroidThemeFileEditor;
            }
            public void create_theme(string fname, string plateform, string parentTemplate)
            {


                if (!string.IsNullOrEmpty(fname)
                    && !string.IsNullOrEmpty(plateform)
                    && !string.IsNullOrEmpty(parentTemplate))
                {
                    this.m_fileName = fname;
                    this.m_parentTemplate = parentTemplate;
                    this.m_platformName = plateform;
                    this.create_surface();
                }
            }

            public void create_surface()
            {
                this.owner.m_Dialog.DialogResult = enuDialogResult.OK;
                ATBThemeFile f = new ATBThemeFile();
                f.DefaultPlateForm = AndroidSystemManager.GetAndroidTargetByName(this.m_platformName);
                f.CurrentTheme.Parent = this.m_parentTemplate;

               //var g =   AndroidTheme.GetAndroidTheme(this.owner.m_themes, this.m_parentTemplate);
                
                f.FileName = this.m_fileName;

                var v_s = AndroidThemeFileEditorSurface.Create(f);
                this.owner.Workbench.AddSurface(v_s, true);
            }
            public string changePlateform(string n)
            {
                var s = AndroidSystemManager.GetAndroidTargets();
                foreach (var item in s)
                {
                    if (item.TargetName == n)
                    {
                        this.owner.m_TargetInfo = item;
                        break;
                    }
                }
                return string.Format("<select>{0}</select>", this.owner.getParentList());
            }

        }

    }
}
