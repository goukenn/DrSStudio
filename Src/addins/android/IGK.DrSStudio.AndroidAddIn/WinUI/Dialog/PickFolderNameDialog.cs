
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.WinUI.Dialog
{
    using IGK.DrSStudio.Android.Web;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Common;
    using IGK.ICore.WinUI.Registrable;
    using IGK.ICore.Xml;
    using IGK.ICore.Web;
    using System.IO;

    public sealed class PickFolderNameDialog : 
        IDisposable ,
        ICoreWebReloadDocumentListener
    {
        private string m_OuputFolder;
        private string m_Name;
        private ICoreDialogForm m_dialog;
     //   private CoreXmlWebDocument m_document;

        /// <summary>
        /// get or the name
        /// </summary>
        public string FileName
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        /// <summary>
        /// get or set the output folder
        /// </summary>
        public string OuputFolder
        {
            get { return m_OuputFolder; }
            set
            {
                if (m_OuputFolder != value)
                {
                    m_OuputFolder = value;
                }
            }
        }
       
        [ComVisible(true)]
        public sealed class AndroidScriptObject
        {
            private PickFolderNameDialog m_owner;
            private ICoreSystemWorkbench m_bench;

            public AndroidScriptObject(PickFolderNameDialog menu, ICoreSystemWorkbench bench)
            {
                this.m_owner = menu ?? throw new ArgumentNullException(nameof(menu));
                this.m_bench = bench ?? throw new ArgumentNullException(nameof(bench));
            }
            public void confirm(string fname, string directory)
            {
                this.m_owner.m_Name = fname;
                this.m_owner.m_OuputFolder = directory;
                if (!string.IsNullOrEmpty(fname))
                {
                    this.m_owner.m_dialog.DialogResult = enuDialogResult.OK;
                }
            }
            public string pickFolder()
            {
                return CoreCommonDialogUtility.PickFolder(this.m_bench);
            }
        }


        public void Dispose()
        {
            if (this.m_dialog != null)
            {
                this.m_dialog.Dispose();
            }
        }

        public enuDialogResult ShowDialog(ICoreSystemWorkbench bench, string title)
        {
            IWebBrowserControl control = CoreControlFactory.CreateControl(
                typeof(IWebBrowserControl).Name)
                as IWebBrowserControl;
            
            if (control != null)
            {
                control.ObjectForScripting = new AndroidScriptObject(this, bench );
                var doc = IGK.ICore.Xml.CoreXmlWebDocument.CreateICoreDocument ();
                doc.InitDocument ();
             
                this.ReloadDocument(doc, false );
                control.AttachDocument(doc);
                control.SetReloadDocumentListener(this);
                using (var frm = bench.CreateNewDialog(control))
                {
                    this.m_dialog = frm;
                    frm.Title = title;
                    //size from measure
                    frm.Size = new Size2i(420, 305);//frm.Height 
                    var g =  frm.ShowDialog();
                    return g;
                }
            }
            return enuDialogResult.None;
        }

       

        public void ReloadDocument(CoreXmlWebDocument document , bool attachDocument)
        {
            document.Body.ClearChilds();
            var d = document.Body.addDiv();
            d["class"] = "igk-android-background";
            d["style"] = "height:100%;height:100%; position:absolute; padding: 0px 16px;";

            d.addDiv().setClass("igk-title-4").Content = "title.options".R();
            var frm = d.addForm();
            frm["class"] = "form-group";
            frm["onsubmit"] = "external.confirm(this.clFileName.value, this.clDirectory.value); return false;";

            frm.addLabel(@for: "clFileName").Content = "lb.fileName".R();
            var f = frm.addInput("clFileName", "text", Path.GetFileNameWithoutExtension(this.FileName));
            f["class"] = "form-control";

            frm.addLabel(@for: "clDirectory").Content = "lb.Directory".R();
            d = frm.addDiv();
            d["class"] = "input-group";
            d["style"] = "padding:8px 0px;";
            f = d.addInput("clDirectory", "text", this.OuputFolder);
            f["class"] = "form-control";

            var btn = d.add("span").setClass("input-group-btn").addInput("btn_getfolder", "button", "...");
            btn["class"] = "btn igk-btn igk-btn-default";

            btn["onclick"] = "this.form.clDirectory.value = window.external.pickFolder();";
            btn = frm.addDiv().addInput("btn_getfolder", "submit", "lb.ok".R());
            btn["class"] = "igk-btn igk-btn-default fitw";
           
        }
    }
}
