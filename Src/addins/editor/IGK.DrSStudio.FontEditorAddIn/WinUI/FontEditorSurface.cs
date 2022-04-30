
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Editor.FontEditor.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.Xml;
    using IGK.ICore.Web;
    using IGK.ICore.IO;
    using System.IO;
    using System.Drawing;
    using IGK.ICore.IO.Font;
    using ICore.Drawing2D;
    using System.Drawing.Text;
    using System.Runtime.InteropServices;
    using IGK.DrSStudio.Editor;

    [CoreSurface("DBFBB7B1-7435-41A8-A29C-85C117A83902")]
    public class FontEditorSurface : IGKXWinCoreWorkingSurface, IIGKWebBrowserControl 
    {
        private System.Drawing.Font m_selectedFont;
        private WebBrowser c_webBrowser;
        private CoreXmlWebDocument m_document;
        //public void ChangeUserAgent()
        //{
        //    // http://stackoverflow.com/a/12648705/107625
        //    //const string ua = @"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
        //    string ua = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 52.0.2743.116 Safari / 537.36 Edge / 15.15063";
        //    // http://stackoverflow.com/q/937573/107625
        //    int i = UrlMkSetSessionOption(UrlmonOptionUseragent, ua, ua.Length, 0);
        //    if (i > 0) {

        //    }
        //}

    //    [DllImport(@"urlmon.dll", CharSet = CharSet.Ansi)]
    //    private static extern int UrlMkSetSessionOption(
    //int dwOption,
    //string pBuffer,
    //int dwBufferLength,
    //int dwReserved);

        private const int UrlmonOptionUseragent = 0x10000001;


        internal void SaveHtmlDpcument(string fileName)
        {
            File.WriteAllText(fileName, this.m_document.RenderXML(null));
        }

        List<string> m_files;
        private CoreFontFile m_font;
        private FontEditorScriptObject m_scriptObject;
        public string FontEditorSurfaceFolder {
            get {
                return PathUtils.GetPath("%temp%/__fonteditor");
            }
        }
        public Font SelectedFont { get {
                if (this.m_selectedFont != null) {
                    if (this.m_mode == FROM_FILE)
                    {
                        try
                        {
                            string n = this.m_selectedFont.Name;
                        }
                        catch (Exception e)
                        {
                            if (piCollection == null)
                            {
                                piCollection = new PrivateFontCollection();
                                piCollection.AddFontFile(this.m_fileName);
                            }
                            CoreLog.WriteDebug ("FontEditor",e.Message );
                            this.m_selectedFont = new Font(piCollection.Families[0], 1.0f);
                        }
                    }

                }
                return this.m_selectedFont;

         } }
        const int FROM_FILE = 1;
        const int FROM_FONT = 0;
        int m_mode = FROM_FONT;
        private string m_fileName;
        private PrivateFontCollection piCollection;
        public string FontFileName => m_fileName;

        public FontEditorSurface(string filename)
        {
            if (!File.Exists(filename)) {
                throw new ArgumentException (nameof (filename));
            } 
            this.m_mode = FROM_FILE;
            this.m_fileName = filename;
            initEditor();
        }

        private void initEditor()
        {
            this.m_files = new List<string>();        
            this.c_webBrowser = new WebBrowser();
            this.c_webBrowser.AllowNavigation = false;
            this.c_webBrowser.AllowWebBrowserDrop = false;
            this.c_webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.c_webBrowser.WebBrowserShortcutsEnabled = false;
            this.m_scriptObject = new FontEditorScriptObject(this);
            this.c_webBrowser.ObjectForScripting = m_scriptObject;
            this.c_webBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(this.c_webBrowser);
            this.Load += _load;
        }

        public FontEditorSurface(System.Drawing.Font font)
        {
            if (font == null)
                throw new ArgumentNullException(nameof(font));
            this.m_selectedFont = font;
            initEditor();
          

        }
        public FontEditorScriptObject ScriptObject {
            get {
                return m_scriptObject;
            }
        }
        private void _initDocument()
        {         
            string f = string.Empty;
            bool v_build = true;
            bool v = false;

            switch (m_mode)
            {
                case FROM_FONT:

                    f = FontEditorUtility.GetFontInfoFile(this.m_selectedFont.Name);


                    if (string.IsNullOrEmpty(f))
                    {//searchingf from windows font

                        foreach (var s in System.IO.Directory.GetFiles(
                            Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                            this.m_selectedFont.Name + ".ttf"))
                        {
                            if (Path.GetFileNameWithoutExtension(s).ToLower() == this.m_selectedFont.Name.ToLower())
                            {
                                f = s;
                                v = true;
                                break;
                            }
                        }

                    }
                    else {
                        v = true;
                    }
                   
                    break;
                case FROM_FILE:
                    f = this.m_fileName;
                    v = true;
                    if (File.Exists(f)) {
                        piCollection = new PrivateFontCollection();
                        //piCollection.AddFontFile (f);
                        byte[] tb = File.ReadAllBytes(f);
                        IntPtr alloc = tb.CopyToCoTaskMemory();
                        piCollection.AddMemoryFont(alloc, tb.Length);//.Add File.ReadAllText(f)
                    if (piCollection.Families.Length > 0)
                        {
                            this.m_selectedFont = new Font(piCollection.Families?[0], 1.0f);
                        }
                    }
                    break ;
            }

            var doc = CoreXmlWebDocument.CreateICoreDocument();
            doc.AddLink(PathUtils.GetPath("%startup%/sdk/lib/bootstrap/css/bootstrap.min.css"));
            doc.AddLink(PathUtils.GetPath("%startup%/sdk/Styles/igk.css"));


            if (!v)
            {
                var dv = doc.Body.addDiv();
                dv.Content = "e.fontfilenotfound_1".R(this.m_selectedFont.Name);
                v_build = false;
            }
            this.m_document = doc;

            if (v_build)
            {
                LoadFileDefinition(f, doc);               
            }
            //render document
            this.m_document.ForWebBrowserDocument = true;
            string indexf = Path.Combine(this.FontEditorSurfaceFolder, Path.GetFileNameWithoutExtension(f) + ".html");
            this.m_files.Add(indexf);
            if (IGK.ICore.IO.PathUtils.CreateDir(Path.GetDirectoryName(indexf)))
            {
                File.WriteAllText(indexf, doc.RenderXML(null));
                this.c_webBrowser.Url = new Uri("file://" + indexf);
                this.c_webBrowser.AllowNavigation = true;
                this.c_webBrowser.ScriptErrorsSuppressed = false;
            }
        }

        private  void LoadFileDefinition(string f, CoreXmlWebDocument doc)
        {
            this.m_fileName = f;

           
            var rf = Path.Combine(FontEditorSurfaceFolder, "font", Path.GetFileName(f));
            string fname = m_mode == 0? this.m_selectedFont?.Name : Path.GetFileNameWithoutExtension (f);
           
            PathUtils.CreateDir(Path.GetDirectoryName(rf));
            string uri = "file://" + rf.Replace("\\", "/");


            string s = $"data:application/font-ttf;charset=utf-8;base64,{Convert.ToBase64String(File.ReadAllBytes(f), Base64FormattingOptions.None)}";

            doc.Head.add("style").setAttribute("type", "text/css")
            .setContent(string.Format(@"
@font-face{{font-family: ""testing""; src: url('{0}') format(""truetype"");}}
.testing-font{{
	font-family: 'testing';
	font-size: 4em;
}}
.v{{
font-family: sans-serif; font-size: 12pt;
}}

", s));

            var root = doc.Body.addDiv().setClass("fith fitw overflow-y-a");
            var data = root.addDiv().setClass("content").addDiv().setClass("igk-container");

            //bool copy =
            PathUtils.CopyFile(f, rf, true);
            m_files.Add(rf);

            var font = CoreFontFile.FontParser(rf, false);
            if (font == null)
            {
                data.addDiv().setClass("igk-danger").Content = "/!\\ Failed to open file";
                return;
            }
            ushort[] list = font.UnicodeList();
            string t = string.Empty;
            var header = data.addDiv();

            var rp = data.addDiv();
            rp.setClass("testing-font");
            bool unicode = list.Length > 0;
            if (unicode)
            {
                var ul = header.add("ul");
                ul.add("li").Content = "lb.Name_1".R(fname);
                ul.add("li").Content = "lb.numberofunicodeglyf_1".R(list.Length);
                ul.add("li").Content = "lb.numberofInnerGlyf_1".R(font.NumberOfGlyphs);
                m_scriptObject.List = list;
                rp.addDiv().Content = m_scriptObject.fe_getpage(1);
            }
            else
            {
                var ul = header.add("ul");
                ul.add("li").Content = "lb.Name_1".R(fname);
                ul.add("li").Content = "lb.numberofglyf_1".R(font.NumberOfGlyphs);
                //1000 per page
                int page = font.NumberOfGlyphs / 1000;
                int p = font.NumberOfGlyphs % 1000;
                for (int i = 0; i < p; i++)
                {
                    var span = rp.addDiv().setClass("floatl").
                     setAttribute("style", "border:1px solid #eee; overflow:hidden; padding: 4px;").
                    add("span");

                    span.addA("#").setAttribute("onclick", "javascript: if (window.external)window.external.fe_edit(" + i + "); return false; ")
                        .setContent("&#x" + i.ToBase(16, 4) + ";");
                    span.getParentNode().addDiv()
                    .setAttribute("class", "v").setContent("&amp; #x" + i.ToBase(16, 4));
                }
            }
            var opts = root.addDiv().addDiv();
            opts.setClass("igk-action-bar");
            opts.addInput("editall","button", "btn.edit.all".R()).setClass("igk-btn")
                .setAttribute("onclick", "javascript:if (window.external)window.external.fe_editall(); return false");
            root.addDiv().setContent("igkdev.copyright".R());
            this.m_font = font;
            this.Title = fname;
        }

        protected override void Dispose(bool disposing)
        {
            _deleteFile();
            base.Dispose(disposing);
        }

        private void _deleteFile()
        {
            foreach (var item in m_files)
            {
                try
                {
                    File.Delete(item);
                }
                catch { 
                }
            }
        }
        private void _load(object sender, EventArgs e)
        {
           //ChangeUserAgent();
            this._initDocument();
        }


        public bool IsBodyDefined
        {
            get {
                return (this.c_webBrowser.Document!=null) && (this.c_webBrowser.Document.Body != null);
            }
        }

        public string DocumentText
        {
            get
            {
                return this.c_webBrowser.DocumentText;
            }
            set
            {
                this.c_webBrowser.DocumentText = value;
            }
        }
        

        public CoreXmlWebDocument Document
        {
            get { return this.m_document; }
        }
        public void setBodyInnerHtml(string text)
        {
            this.c_webBrowser.Document.Body.InnerHtml = text;
        }

        public void InvokeScript(string funcname, string[] parameter)
        {
            this.c_webBrowser.Document.InvokeScript(funcname, parameter);
        }

        internal object  GetGlyphInfo(ushort code)        
        {
            if (this.m_font !=null)
                return this.m_font.GetGlyphUnicode(code);
            return null;
        }
    }
}
