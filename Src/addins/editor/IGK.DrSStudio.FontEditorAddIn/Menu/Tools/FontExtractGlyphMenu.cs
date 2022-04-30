using IGK.DrSStudio.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Menu
{
    using IGK.DrSStudio.Editor.FontEditor;
    using IGK.ICore;
    using IGK.ICore.JSon;
    using IGK.ICore.Web;
    using IGK.ICore.Web.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Common;
    using IGK.ICore.Xml;
    using System.Runtime.InteropServices;

    [CoreMenu("Tools.Font.FontExtractGlyph", 0x5003)]
    public sealed class FontExtractGlyphMenu : CoreApplicationMenu, ICoreWebReloadDocumentListener
    {
        [ComVisible(true)]
        public class ScriptObject : CoreWebScriptObjectBase, ICoreWebDialogProvider
        {
            public ICoreWebScriptObject OjectForScripting
            {
                get { return this; }
            }

           

            public override void Submit(object data)
            {
                var d = new CoreJSon().ToDictionary((string)data);
                this.Menu.m_FontFile = d["clFile"].ToString();
                this.Menu.m_OutputFile = d["clFolder"].ToString();
                this.confirm();
            }
            public void submitdata(string filename, string folder)
            {
                this.Menu.m_FontFile = filename;
                this.Menu.m_OutputFile = System.IO.Path.Combine(folder,System.IO.Path.GetFileNameWithoutExtension( filename)+".gkds");
                this.confirm();
            }
            public void confirm() {
                this.DialogResult = enuDialogResult.OK;
            }
            public string pickFolder(string value)
            {
                using (var s = this.Menu.Workbench.CreateCommonDialog<FolderNamePicker>())
                {
                    s.SelectedFolder = value;
                    if (s.ShowDialog() == enuDialogResult.OK)
                    {
                        return s.SelectedFolder;
                    }
                }
                return string.Empty;

            }
            public string pickFile(string value)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.FileName = ofd.FileName;
                    ofd.Filter = "True type font | *.ttf";
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        return ofd.FileName;
                    }
                }
                //{
                //    s.SelectedFolder = value;
                //    if (s.ShowDialog() == enuDialogResult.OK)
                //    {
                //        return s.SelectedFolder;
                //    }
                //}
                return value;

            }
            public FontExtractGlyphMenu Menu { get; set; }
        }

        private string m_FontFile;
        private string m_OutputFile;

        public string OutputFile
        {
            get { return m_OutputFile; }
            set
            {
                if (m_OutputFile != value)
                {
                    m_OutputFile = value;
                }
            }
        }
        public string FontFile
        {
            get { return m_FontFile; }
            set
            {
                if (m_FontFile != value)
                {
                    m_FontFile = value;
                }
            }
        }
        protected override bool PerformAction()
        {
            CoreXmlWebDocument v_document = CoreXmlWebDocument.CreateICoreDocument();
            v_document.InitDocument(this.Workbench as ICoreWorkbenchDocumentInitializer);
            
            var d = v_document.Body.addDiv().setClass ("container");
            var frm = d.addForm();
            frm.addLabel().Content = "lb.FontFile".R();
            CoreWebExtensions.addPickFile(frm, "clFile");
            frm.addLabel().Content = "lb.OutputFolder".R();
            //pick folder
            CoreWebExtensions.addPickFolder(frm, "clFolder");

       

        //        <div class="input-group">
        //  <input type="text" value="" igk-hint="name" id="clFolder" class="fitw form-control"/>
        //  <span class="input-group-btn"  >
        //    <button class="btn btn-default" type="button" onclick="javascript:pickfolder(this.form); return false;" >...</button>
        //  </span>
        //</div>


            frm.addHSep();
            frm.addInput("btn.confirm", "submit", "btn.extract".R()).setClass("btn btn-default");
            frm["onsubmit"] = "winex.submitdata(this.clFile.value,this.clFolder.value); return false;";
            var p = new ScriptObject()
            {
                Menu = this,
                Document = v_document 
            };
            p.SetReloadListener (this);
            

            //Render(v_document);
            using (var dial = this.Workbench.CreateWebBrowserDialog(p))
            {
                
                dial.Title = "title.ExtractGlyphFromFont".R();
                dial.Size = new Size2i(400, 230);
                dial.WebControl.SetReloadDocumentListener(this);
                if (dial.ShowDialog() == enuDialogResult.OK)
                {
                    ExtractGlyph();                  
                }
            }
           
            return base.PerformAction();
        }
        private void ExtractGlyph() {
            if (FontEditorUtility.ExtractGlyphToGkds(this.FontFile, this.OutputFile)) {
                CoreMessageBox.Show("Done".R());
            }
        }

        public void ReloadDocument(CoreXmlWebDocument document, bool attach)
        {
        }
    }

    
}
