using IGK.DrSStudio.Android.AndroidThemeBuilder.WinUI;
using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder.Actions
{
    [CoreAction("Android.EditThemeInfo")]
    class AndroidEditThemeInfoAction : CoreActionBase, ICoreWebReloadDocumentListener, IPrivateResponseListener
    {
        private ICore.WinUI.ICoreDialogForm m_Dialog;
        //private AndroidTargetInfo m_TargetInfo; //primary target
        private ATBThemeFile m_theme;
        private ATBInvacationScriptManager m_manager;

        protected override bool PerformAction()
        {
            

            var wb = CoreSystem.Instance.Workbench;
            if (wb.CurrentSurface is AndroidThemeFileEditorSurface s)
            {
                this.m_theme = s.ThemeFile;


                if (m_manager == null)
                {
                    m_manager = new ATBInvacationScriptManager()
                    {
                        Theme = m_theme.CurrentTheme.Name,
                        DefaultParent = m_theme.CurrentTheme.Parent
                };
                }
                else {
                    m_manager.Theme = m_theme.CurrentTheme.Name;
                    m_manager.DefaultParent = m_theme.CurrentTheme.Parent;
                }

                CoreXmlWebDocument v_doc = CoreXmlWebDocument.CreateICoreDocument();
                v_doc.InitAndroidWebDocument();
                v_doc.ForWebBrowserDocument = true;
                this.ReloadDocument(v_doc, false);

                using (var d = wb.CreateWebBrowserDialog(
                    new PrivateResponse(this) {
                        Document = v_doc 
                    }))
                {
                    d.Title = "title.android.editCurrentTheme".R();
                    d.Size = new Size2i(300, 350);
                    d.WebControl.SetReloadDocumentListener(this);
                    this.m_Dialog = d;
                    d.ShowDialog();
                }
            }
            return false;
        }

        public void ReloadDocument(CoreXmlWebDocument document, bool attachDocument)
        {
          
            document.Body.Clear();
            document.Body.LoadString(
            IGK.ICore.Web.CoreWebUtils.EvalWebStringExpression(
            Encoding.UTF8.GetString(Properties.Resources.editTheme),
           m_manager));
        }

        public void SetTargetInfo(AndroidTargetInfo item)
        {
            m_manager.SetTargetInfo(item);
        }

        public string GetParentList()
        {
            return m_manager.GetParentList();
        }

        public void UpdateValue(string fname, string plateform, string parentTemplate)
        {
            this.m_theme.CurrentTheme.Name = fname;
            this.m_theme.DefaultPlateForm = AndroidSystemManager.GetAndroidTargetByName(plateform ?? IGK.DrSStudio.Android.Settings.AndroidSetting.Instance.DefaultPlatform );
            this.m_theme.CurrentTheme.Parent = parentTemplate;
            this.m_Dialog.DialogResult = ICore.WinUI.enuDialogResult.OK;
        }
    }
    public interface IPrivateResponseListener
    {
        void SetTargetInfo(AndroidTargetInfo item);
        string GetParentList();
        void UpdateValue(string fname, string plateform, string parentTemplate);
    }
    [ComVisible(true)]
    public class PrivateResponse : CoreWebScriptObjectBase, ICoreWebDialogProvider
    {
        private IPrivateResponseListener m_privateListener;
        public ICoreWebScriptObject OjectForScripting => this;

        ///<summary>
        ///public .ctr
        ///</summary>
        public PrivateResponse(IPrivateResponseListener listener)
        {
            this.m_privateListener = listener;
        }
        public string ChangePlateform(string value) {
            var s = AndroidSystemManager.GetAndroidTargets();
            foreach (var item in s)
            {
                if (item.TargetName == value)
                {
                    //this.owner.m_TargetInfo = item;
                    this.m_privateListener.SetTargetInfo(item);
                    break;
                }
            }
            return string.Format("<select>{0}</select>", this.m_privateListener.GetParentList());
        }

        public void AndroidChangeThemeInfo(string fname, string plateform, string parentTemplate) {
            this.m_privateListener.UpdateValue(fname, plateform, parentTemplate);
        }
    }
}
