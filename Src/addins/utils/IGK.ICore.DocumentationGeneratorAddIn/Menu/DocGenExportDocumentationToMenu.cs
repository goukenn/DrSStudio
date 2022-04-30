
using IGK.ICore;using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.WinUI.Registrable;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.Menu
{
    [CoreMenu ("Tools.ExportDocumentation", 0x300)]
    public sealed class DocGenExportDocumentationToMenu : CoreApplicationMenu
    {

        [ComVisible(true)]
        public class ScriptObject
        {
            private DocGenExportDocumentationToMenu m_menu;
            internal ICoreDialogForm Dialog;
            internal bool overWriteExisting;
            internal string OutputFolder;

            public ScriptObject()
            {
            }
            public ScriptObject(DocGenExportDocumentationToMenu docGenExportDocumentationToMenu)
            {
                this.m_menu = docGenExportDocumentationToMenu;
            }
            public void validate(string outputfolder,string overwriteexisting)
            {
                this.OutputFolder = outputfolder;
                this.overWriteExisting =  Convert.ToBoolean(overwriteexisting);
                this.Dialog.DialogResult = enuDialogResult.OK;
            }
            public string  getOutputDir() {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.ShowNewFolderButton = true;

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        return fbd.SelectedPath;
                    }
                }
                return string.Empty;
            }
        }

        protected override bool PerformAction()
        {

            CoreXmlWebDocument doc = CoreXmlWebDocument.CreateICoreDocument();
            IWebBrowserControl control = CoreControlFactory.CreateControl(
                typeof(IWebBrowserControl).Name)
                as IWebBrowserControl;
            if (control != null)
            {
                ScriptObject obj = new ScriptObject(this);
                var frm = doc.Body.addForm();
                //window.external.submit(this.OutputDir.value, this.OverrideExisting.value); 
                frm["onsubmit"] = " window.external.validate(this.OutputDir.value ,this.OverrideExisting.checked?'true':'false'); return false;";
                frm["action"] = "__self";
                var div = frm.Add ("div").Add("ul");

                var li  = div.Add ("li");
                li.Add("label").Content = "lb.OverrirdeExistingFile".R();
                li.Add("input", new Dictionary<string, string>() {
                {"id", "OverrideExisting"},
                {"type", "checkbox"},
                {"value", "true"}
            });
                li = div.Add("li");
                li.Add("label").Content = "lb.Outputdir".R();
                li.Add("input", new Dictionary<string, string>() {
                {"id", "OutputDir"},
                {"type", "text"},
                {"value", "c:\\output"}
            });

                li.Add("input", new Dictionary<string, string>() {
                {"id", "btn_getOuputDir"},
                {"type", "button"},
                {"value", "..."},
                {"onclick", "this.form.OutputDir.value = window.external.getOutputDir(); return false;"}
                });


                li = div.Add("li");
                li.Add("input", new Dictionary<string, string>()
                {
                    {"id","btn_submit"},
                    {"type","submit"},
                    {"value","frm.btn.submit".R()},
                    {"class","btn btn-validate"}
                });
                string v_g = doc.RenderXML(null); 

                control.ObjectForScripting = obj;
                //control.Document = doc;
                
                using (var g = Workbench.CreateNewDialog(control))
                {
                    obj.Dialog = g;
                    g.Title = "title.ExportDocumentation".R();
                    if (g.ShowDialog() == enuDialogResult.OK)
                    {
                        DocGenManager.ExtractDocumentationTo(obj.OutputFolder,
                            obj.overWriteExisting);
                    }
                }
            }
            else
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.ShowNewFolderButton = true;

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        DocGenManager.ExtractDocumentationTo(fbd.SelectedPath, true);
                    }
                }
            }
            return false;
        }
    }
}
