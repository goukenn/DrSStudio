
using IGK.ICore.Menu;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Menu.File
{
    using IGK.DrSStudio.Android.Xamarin.WinUI;
    using IGK.DrSStudio.Android.Xamarin.Xml;
    using IGK.ICore;
    using IGK.ICore.Resources;
    using IGK.ICore.Web;
    using IGK.ICore.Web.WinUI;
    using IGK.ICore.WinUI;
    using System.Runtime.InteropServices;

    [XamarinMenuAttributte("File.New.Android.XamarinAndroidProject", 0x12)]
    public sealed  class XamarinNewEmptyAndroidProject : 
        CoreApplicationMenu ,
        ICoreWebReloadDocumentListener
    {

        public XamarinNewEmptyAndroidProject()
        {
            
        }
        protected override bool PerformAction()
        {

            var s = CoreResources.GetResourceString("xamarin_project", GetType().Assembly );
            if (s == null)
            {
                CoreMessageBox.NotifyMessage("DrSStudio.Xamarin.Error", "Impossible de créer un fichier xamarin");
                return false;
            }
            var dd = new XamarinScriptRow();
            dd.DocumentChanged += (o, e) =>
            {
                dd.Document.InitDocument();
                dd.Document.InitAndroidWebDocument();
            };
            using (var d = Workbench.BuildWebDialog(dd, "title.NewAndroidXamarinProject".R(), s, dd))
            {
              
                d.Size = new Size2i(1105, 540);
                d.WebControl.SetReloadDocumentListener(this);
                if (d.ShowDialog() == enuDialogResult.OK)
                {                    
                    XamarinProjectXmlElement p = dd.Project;
                    var v_s = new XamarinEditorSurface();
                    //s.LoadProject(FileName, p);
                    this.Workbench.AddSurface(v_s, true);
                }
            }

            return false;
        }
        public string getfilename() {
            return "androidProject";
        }
        //[ComVisible(true)]
        //public class DialogProvider : CoreWebScriptObjectBase, ICoreWebDialogProvider
        //{
        //    private XamarinNewEmptyAndroidProject xamarinNewEmptyAndroidProject;

        //    public DialogProvider(XamarinNewEmptyAndroidProject xamarinNewEmptyAndroidProject)
        //    {
        //        // TODO: Complete member initialization
        //        this.xamarinNewEmptyAndroidProject = xamarinNewEmptyAndroidProject;
        //    }
        //    public ICoreWebScriptObject OjectForScripting
        //    {
        //        get {
        //            return this;
        //        }
        //    }
        //    public void update_file(string filename) {
        //        this.xamarinNewEmptyAndroidProject.FileName = filename;
        //        this.Dialog.DialogResult  = enuDialogResult.OK;
        //    }
        //    protected override void OnDocumentChanged(EventArgs e)
        //    {                
        //        this.Document.InitAndroidWebDocument();
        //    }
        //}


        public void ReloadDocument(CoreXmlWebDocument document, bool attach)
        {
            document.InitAndroidWebDocument();
        }
    }
}
