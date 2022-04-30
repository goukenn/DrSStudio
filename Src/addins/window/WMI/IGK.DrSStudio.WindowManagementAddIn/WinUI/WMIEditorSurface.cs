using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Management.WinUI
{
    using IGK.ICore;
    using IGK.ICore.Resources;
    using IGK.ICore.Web;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.Xml;
    using IGK.ICore.WinUI;
    using IGK.ICore.IO;

    [CoreSurface("CB1EB4D1-C76A-4F39-8E46-573EBF3432B1")]
    public class WMIEditorSurface : 
        IGKXWinCoreWorkingSurface,
        ICoreWebReloadDocumentListener,
        ICoreWebScriptListener
         
    {
        private CoreXmlWebDocument m_document;
        private IGKXWebBrowserControl c_webbrowser;
        
        public CoreXmlWebDocument Document { get { return this.m_document;  } }
        public WMIEditorSurface(){
            this.Title = "WMI Editor";
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //
            this.m_document = CoreXmlWebDocument.CreateICoreDocument();
            this.m_document.InitDocument();
            this.c_webbrowser = new IGKXWebBrowserControl();
            this.c_webbrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(this.c_webbrowser);

            var f = new WMIScriptProfile(this);
            f.SetScriptListener(this);
            this.c_webbrowser.ObjectForScripting = f;
            this.c_webbrowser.SetReloadDocumentListener(this);
            //this.m_WebBrowser.InvokeScript(script);
            //this.c_webbrowser.AttachDocument(this.m_document);
            //init document
            this.ReloadDocument(this.m_document, true);

        }

        public void ReloadDocument(CoreXmlWebDocument document, bool attachDocument)
        {
            if (document == null)
                return;

            #if DEBUG
            string s =
 CoreFileUtils.ReadAllFile(CoreConstant.DRS_SRC + @"\addins\window\WMI\IGK.DrSStudio.WindowManagementAddIn\Resources\main.html") ??
 CoreResources.GetResourceString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ":/resources/main.html");

#else
            string s = CoreResources.GetResourceString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name+ ":/resources/main.html");
#endif
            s = s.EvalWebStringExpression(this.c_webbrowser.ObjectForScripting);
            document.Body.setContent(s);
#if DEBUG
            s = CoreFileUtils.ReadAllFile(CoreConstant.DRS_SRC+@"\addins\window\WMI\IGK.DrSStudio.WindowManagementAddIn\Resources\wmi.js")
                ?? CoreResources.GetResourceString("resources/wmi.js");
#else
            s = CoreResources.GetResourceString("resources/wmi.js");
#endif

            document.Body.AppendScriptSource("resources/wmi.js", s);


            if (attachDocument) {
                this.c_webbrowser.AttachDocument(document);
            }
        }

        public object InvokeScript(string script)
        {
            return this.c_webbrowser.InvokeScript(script);
        }
    }
}
