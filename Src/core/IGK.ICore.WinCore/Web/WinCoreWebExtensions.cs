using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.Web
{
    public static class WinCoreWebExtensions
    {
        public static void AttachToWebbrowser(this CoreXmlWebDocument document, 
            WebBrowser browser,
            bool forceview=false)
        {
            if ((document == null) || (browser == null) || browser.IsDisposed )
                return;  
            if (forceview || (browser.Document == null) || (browser.Document.Body == null) /*|| (browser.Document.Body.InnerHtml == null)*/)
            {
                string g = document.RenderXML(null);
                browser.DocumentText = g;
            }
            else
            {                
                browser.Document.Body.InnerHtml = document.Body.RenderInnerHTML(new CoreXmlWebOptions());
                browser.Document.InvokeScript("eval", new string[] { "(function t(){if (window.igk) window.igk.system.evalScript(document.body); return '1'; })();" });
            }
        }
        /// <summary>
        /// set web browser document service
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="data"></param>
        /// <param name="mimetype"></param>
        /// <param name="port"></param>
        public static void SetDocumentText(this WebBrowser browser, string data, string mimetype, int port = 8033)
        {

            WinCoreWebServer server = new WinCoreWebServer();
            server.Response = data;
            server.MimeType = mimetype;
            server.Port = port;
            server.Start();
            browser.Url = server.Url;
            browser.DocumentCompleted += (o, e) =>
            {
                server.Stop();
            };
        }
  
    }
}
