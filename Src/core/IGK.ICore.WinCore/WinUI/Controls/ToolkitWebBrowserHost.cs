using IGK.ICore.JSon;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Win32.UI.Controls.WinForms;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    /// <summary>
    /// represet a toolkit web browser for windows 10 interface
    /// </summary>
    public  class ToolkitWebBrowserHost : IWebBrowserHost, IUriToStreamResolver
    {
        private WebView c_webview;
        private IWebBrowserHostStreamResolver m_streamResolver;

        public event KeyEventHandler AccelerateKeyEvent;

        public Control Host => c_webview;

        public object ObjectForScripting { get; set; }

        public bool NotLoaded => true;

        public event EventHandler LoadingCompleted;

        public void Dispose()
        {
            c_webview?.Dispose();
            c_webview = null;
        }

        public void Reset() {
            this.c_webview.Refresh();
        }
        public async Task<string> InvokeScript(string p, params string[] args)
        {
            try
            {
                return await c_webview.InvokeScriptAsync(p, args);
            }
            catch (Exception ex) {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public void SetBody(string v)
        {
            throw new NotImplementedException();
        }

        public void SetDocument(string html)
        {
            this.c_webview.NavigateToString(html);
            //this.c_webview.NavigateToLocalStreamUri(new Uri("http://localhost:80"), this);
            // this.c_webview.Navigate("https://www.google.com");// ToString(html);
        }

        ///<summary>
        ///public .ctr
        ///</summary>
        public ToolkitWebBrowserHost()
        {
            this.c_webview = new WebView();
            ((ISupportInitialize)c_webview).BeginInit();
            c_webview.IsPrivateNetworkClientServerCapabilityEnabled = true;
            c_webview.IsJavaScriptEnabled = true;
            c_webview.IsScriptNotifyAllowed = true;
            c_webview.IsIndexedDBEnabled = true;
            
            
            c_webview.Dock = DockStyle.Fill;
    

            c_webview.NavigationCompleted += Wvc_NavigationCompleted;
            c_webview.PermissionRequested += Wvc_PermissionRequested;
            c_webview.NewWindowRequested += Wvc_NewWindowRequested;
            c_webview.ContentLoading += Wvc_ContentLoading;
            c_webview.UnviewableContentIdentified += Wvc_UnviewableContentIdentified;
            c_webview.ScriptNotify += C_webview_ScriptNotify;
            c_webview.NavigationStarting += _StartNavigating;
            c_webview.UnsupportedUriSchemeIdentified += C_webview_UnsupportedUriSchemeIdentified;
            c_webview.AcceleratorKeyPressed += C_webview_AcceleratorKeyPressed;
            ((ISupportInitialize)c_webview).EndInit();


        }

        private void C_webview_AcceleratorKeyPressed(object sender, WebViewControlAcceleratorKeyPressedEventArgs e)
        {
           
            this.AccelerateKeyEvent?.Invoke(this,
                new KeyEventArgs((Keys)e.VirtualKey));

            //this.AccelerateKeyEvent?.Invoke(new Message() {
            //    HWnd = this.Host.Handle,
            //    WParam = IntPtr.Zero 
            //});

            e.Handled = true;
        }

        private void C_webview_UnsupportedUriSchemeIdentified(object sender, WebViewControlUnsupportedUriSchemeIdentifiedEventArgs e)
        {
            
            throw new NotImplementedException();
        }

        private void _StartNavigating(object sender, WebViewControlNavigationStartingEventArgs e)
        {
            
        }

        private void C_webview_ScriptNotify(object sender, WebViewControlScriptNotifyEventArgs e)
        {
            if (this.ObjectForScripting != null) {
                var mt = this.ObjectForScripting.GetType().GetMethod("notify", System.Reflection.BindingFlags.Instance |
    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase);

                if (mt != null)
                {
                    mt.Invoke(this.ObjectForScripting, new object[] {
                    e.Value
                });

                }
                else
                {
                    var d = CoreJSonReader.Load(e.Value);
                     
                    string m = d["method"]?.ToString();
                    if (string.IsNullOrEmpty(m) == false)
                    {
                        object o = this.ObjectForScripting;
                        object[] v_par;
                        if (d.ContainsKey("param"))
                        {
                            v_par = GetArgs(d["param"]);
                        }
                        else
                        {
                            v_par = new object[0];
                        }
                        mt = o.GetType().GetMethod(m, System.Reflection.BindingFlags.Instance |
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase);
                        try
                        {
                            mt?.Invoke(o, v_par);
                        }
                        catch (Exception ex) {
                            MessageBox.Show(ex.Message, "Error".R(), MessageBoxButtons.OK , MessageBoxIcon.Error );
                        }
                    }
                }
                
            }
        }

        private object[] GetArgs(object v)
        {
            if (v is string) {
                return new object[] { v };
            }

            if(v is Array)
                return (object[]) v;
            return new object[0];
        }

        private void Wvc_NavigationCompleted(object sender, WebViewControlNavigationCompletedEventArgs e)
        {
            LoadingCompleted?.Invoke(sender, e);
      
         }

        private void Wvc_PermissionRequested(object sender, WebViewControlPermissionRequestedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Wvc_NewWindowRequested(object sender, WebViewControlNewWindowRequestedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Wvc_ContentLoading(object sender, WebViewControlContentLoadingEventArgs e)
        {
        }

        private void Wvc_UnviewableContentIdentified(object sender, WebViewControlUnviewableContentIdentifiedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public Stream UriToStream(Uri uri)
        {
            return this.m_streamResolver.Resolve(uri);
        }

        object IWebBrowserHost.InvokeScript(string p, params string[] args)
        {

            //this.c_webview.InvokeScript(p, args);


            Thread th = new Thread(() =>
            {
                var h = Task<string>.Run(async () =>
                {
                    var x = await this.InvokeScript(p, args);
                    return x;
                }).Result;
            });
            th.Start();
            return null;
        }

        public void LoadHtmlString(string htmltext)
        {
            this.c_webview.NavigateToString(htmltext);
        }

        public void LoadFromUriStream(string uri)
        {
            this.c_webview.NavigateToLocalStreamUri(new Uri(uri, UriKind.Relative), this);
        }

        public void SetStreamUriResolver(IWebBrowserHostStreamResolver streamResolver) => m_streamResolver = streamResolver;
       
    }
}