using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    public class IGKXWebBrowserControlHost : IGKXUserControl 
    {
        IGKXWebBrowserControl c_host;
        private IIGKWebBrowserHostListener m_listener;
        public bool AllowNavigation
        {
            get { return this.c_host.AllowNavigation; }
            set { this.c_host.AllowNavigation = value; }
        }
        public WebBrowser WebControl {
            get {
                return this.c_host.Host as WebBrowser;
            }
        }
        public IGKXWebBrowserControlHost( IIGKWebBrowserHostListener listener)
        {
            this.c_host = new IGKXWebBrowserControl
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(this.c_host);
            this.m_listener = listener ?? throw new Exception(nameof(listener)+" is null");
            this.Load += _Load;
        }

        private void _Load(object sender, EventArgs e)
        {
            var g = WebControl;
            if (g != null)
            {
                WinCoreExtensions.IGKApplyBrowserDocumentText(
                       g, this.m_listener.Render());
                g.ObjectForScripting = this.m_listener.ObjectForScripting;
            }
        }
        public void RefreshView()
        { var g = WebControl;
          
                if ((g != null) && (this.m_listener != null))
            {
                WinCoreExtensions.IGKApplyBrowserDocumentText(
                    g, this.m_listener.Render());
            }
        }
    }
}
