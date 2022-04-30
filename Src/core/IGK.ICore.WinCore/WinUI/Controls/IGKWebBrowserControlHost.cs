using mshtml;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    /// <summary>
    /// primary webbrowser host
    /// </summary>
    internal class IGKWebBrowserControlHost : IWebBrowserHost
    {
        private WebBrowser c_webbrowser;
        private IWebBrowserHostStreamResolver m_streamResolver;

        public IGKWebBrowserControlHost()
        {
            c_webbrowser = new WebBrowser();
            //com failed 
            this.c_webbrowser.AllowWebBrowserDrop = false;
            this.c_webbrowser.AllowNavigation = false;
            this.c_webbrowser.Dock = DockStyle.Fill;
            this.c_webbrowser.IsWebBrowserContextMenuEnabled = false;
            this.c_webbrowser.ScriptErrorsSuppressed = false;
            this.c_webbrowser.ScrollBarsEnabled = false;
            this.c_webbrowser.WebBrowserShortcutsEnabled = true;


            this.c_webbrowser.Navigated += C_webbrowser_Navigated;
        }

        private void C_webbrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.Navigated?.Invoke(sender, e);
        }

        public event EventHandler Navigated;

        public Control Host => c_webbrowser;

        public object ObjectForScripting { get =>
                 this.c_webbrowser.ObjectForScripting; set => this.c_webbrowser.ObjectForScripting = value; }

        public bool NotLoaded => (c_webbrowser.Document == null) || (c_webbrowser.Document.Body?.InnerHtml == null);

        public event EventHandler LoadingCompleted;
        public event KeyEventHandler AccelerateKeyEvent;

        private void OnAccelerateKeyEven(KeyEventArgs e) => AccelerateKeyEvent?.Invoke(this, e);
        private void OnLoadingCompleted(EventArgs e) {
            this.LoadingCompleted?.Invoke(this, e);
        }

        public void Dispose()
        {
            
        }

 

        public object InvokeScript(string p, params string[] args)
        {
            return this.c_webbrowser.Document.InvokeScript(p, args);
        }

        public void SetBody(string v)
        {
        }

        public void SetDocument(string html)
        {
            this.c_webbrowser.DocumentText = html;
        }

        public void Reset()
        {
            this.c_webbrowser.Document.Cookie?.Remove(0);
            this.c_webbrowser.DocumentText = string.Empty;
            // this.c_webbrowser.Refresh(WebBrowserRefreshOption.Completely);
        }

        public void LoadHtmlString(string htmltext)
        {
            this.c_webbrowser.DocumentText = htmltext;

        }

        public void LoadFromUriStream(string uri)
        {

            //this.c_webbrowser.Navigate(uri);
            if (this.m_streamResolver!=null)
            {
                this.c_webbrowser.DocumentStream =
                    this.m_streamResolver.Resolve(new Uri(uri));

            }
            else
            {
                WebClient client = new WebClient();                
                this.c_webbrowser.DocumentStream =  client.OpenRead(new Uri(uri));
            }
        }
        

        private void C_webbrowser_Navigating1(object sender, WebBrowserNavigatingEventArgs e)
        {
            //if (e.Url.LocalPath == "blank")
            //    e.Cancel = true;
            Debug.WriteLine("Start Navigating : " + e.Url);
        }

        private void C_webbrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
          
        }

        private void C_webbrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

            Debug.WriteLine("request "+e.Url);
        }

        public void SetStreamUriResolver(IWebBrowserHostStreamResolver streamResolver) => m_streamResolver = streamResolver;
    }
}