
using System;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Registrable;
    using System.ComponentModel;
    using IGK.ICore.Xml;
    using IGK.ICore.Web;

    [CoreRegistrableControl(typeof(IWebBrowserControl), IsRegistrable = true)]
    public class IGKXWebBrowserControl :
        IGKXUserControl,
        IWebBrowserControl
    {
        IWebBrowserHost c_webBrowser;
        private ICoreWebReloadDocumentListener m_reloadDocumentListener;
        private CoreXmlWebDocument m_document;


     


        public event KeyEventHandler AccelerateKeyEvent
        {
            add
            {
                this.c_webBrowser.AccelerateKeyEvent += value;
            }
            remove
            {
                this.c_webBrowser.AccelerateKeyEvent -= value;
            }
        }
        public event EventHandler DocumentComplete
        {
            add
            {
                this.c_webBrowser.LoadingCompleted += value;
            }
            remove
            {
                this.c_webBrowser.LoadingCompleted -= value;
            }
        }
        private RefreshWebViewListener _listenerObjFilter;

        public void Reload()
        {
            //if (this.c_webBrowser.Host is WebBrowser d) {
            //    d.DocumentStream = null;
                
            //    .Refresh();
            //}
        }

        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }

        public void SetUriStreamResolver(IWebBrowserHostStreamResolver resolver)
        {
            this.c_webBrowser.SetStreamUriResolver(resolver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.SetReloadDocumentListener(null);
                this.c_webBrowser.Dispose();

            }
            base.Dispose(disposing);
        }
        public IGKXWebBrowserControl()
        {
            this.c_webBrowser = CreateBrowserHost();
            this.Controls.Add(c_webBrowser.Host);
        }
        public Control Host => c_webBrowser.Host;


        internal static bool IsWindow10(OperatingSystem oSVersion)
        {
            return (oSVersion.Version.Major >= 6) &&
                (oSVersion.Version.Minor >= 0) &&
                oSVersion.Version.Build > 0;
        }

        /// <summary>
        /// Create a web browser host
        /// </summary>
        /// <returns></returns>
        private IWebBrowserHost CreateBrowserHost()
        {
            return new IGKWebBrowserControlHost();
        }



        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ObjectForScripting
        {
            get
            {
                return this.c_webBrowser.ObjectForScripting;
            }
            set
            {
                this.c_webBrowser.ObjectForScripting = value;
            }
        }

        public bool AllowNavigation { get; set; }

        public void SetReloadDocumentListener(ICoreWebReloadDocumentListener listener)
        {
            if (this.m_reloadDocumentListener != listener)
            {
                Application.RemoveMessageFilter(_listenerObjFilter);
                this.m_reloadDocumentListener = listener;
                if (this.m_reloadDocumentListener != null)
                {
                    if (_listenerObjFilter == null)
                        _listenerObjFilter = new RefreshWebViewListener(this);
                    Application.AddMessageFilter(_listenerObjFilter);
                }
            }
        }

        public void SetDocumentString(string v)
        {
            this.c_webBrowser.SetDocument(v);
        }
        public void Reset()
        {
            //reset the document
            
            this.c_webBrowser.Reset();
        }


        /// <summary>
        /// refresh web view listener
        /// </summary>
        internal class RefreshWebViewListener : IMessageFilter
        {
            private IGKXWebBrowserControl m_owner;

            public RefreshWebViewListener(IGKXWebBrowserControl iGKXWebBrowserControl)
            {
                this.m_owner = iGKXWebBrowserControl;
                this.m_owner.Disposed += _Owner_Disposed;
            }

            void _Owner_Disposed(object sender, EventArgs e)
            {
                Application.RemoveMessageFilter(this);
                this.m_owner.SetReloadDocumentListener(null);
            }
            public bool PreFilterMessage(ref Message m)
            {

                if (m.Msg == 0x100)
                {
                    if (m.WParam.ToInt32() == 0x74)
                    {//handler F5
                        if ((this.m_owner.m_reloadDocumentListener != null) && (this.m_owner.m_document != null))
                        {
                            var f = Form.ActiveForm;
                            var g = this.m_owner.FindForm();
                            if ((f != null) && (f == g))
                            {

                                var c = this.m_owner.ActiveControl;
                                this.m_owner.m_reloadDocumentListener.ReloadDocument(this.m_owner.m_document, false);
                                m_owner.AttachDocument(this.m_owner.m_document);
                                return true;

                            }
                        }

                        //return (this.m_owner.m_document!=null);
                    }
                }

                return false;
            }
        }
        public void AttachDocument(CoreXmlWebDocument document, bool forceview = true)
        {
            if (document != null)
            {

                if (forceview || (c_webBrowser.NotLoaded))
                {
                    string g = document.RenderXML(new CoreXmlHtmlOptions());
                    c_webBrowser.SetDocument(g);
                }
                else
                {
                    c_webBrowser.SetBody(document.Body.RenderInnerHTML(new CoreXmlHtmlOptions()));
                    c_webBrowser.InvokeScript("eval", new string[] { "(function t(){if (window.igk) window.igk.system.evalScript(document.body); return '1'; })();" });
                }
            }
            this.m_document = document;
        }

        public object InvokeScript(string p, params string[] args)
        {
            return c_webBrowser.InvokeScript(p, args);

        }


        public void AttachDocument(CoreXmlWebDocument document)
        {
            this.AttachDocument(document, true);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        public void LoadHtmlString(string htmltext)
        {
            this.c_webBrowser.LoadHtmlString(htmltext);
        }

        public void LoadFromUriStream(string uri)
        {
            this.c_webBrowser.LoadFromUriStream(uri);
        }
    }
}
