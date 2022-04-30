using IGK.ICore.Web;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI.Registrable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DRSStudio.BalafonDesigner.WinUI
{
    internal class BalafonViewDesignerWebViewHost
    {
        private Control c_control;
        private BalafonViewDesignerWebProfile m_profile;
        private string m_baseuri;

        public Control Control {
            get => c_control;
        }
        private IGKXWebBrowserControl WebControl {
            get => c_control as IGKXWebBrowserControl;
        }
       

       
        ///<summary>
        ///public .ctr
        ///</summary>
        public BalafonViewDesignerWebViewHost()
        {
            m_profile = new BalafonViewDesignerWebProfile(this);
        }
        internal static BalafonViewDesignerWebViewHost CreateSurface(BalafonViewDesignerSurface balafonViewDesignerSurface)
        {
            IGKXWebBrowserControl c_browser = new IGKXWebBrowserControl
            {
                Dock = DockStyle.Fill
            };
            BalafonViewDesignerWebViewHost host = new BalafonViewDesignerWebViewHost()
            {
                c_control = c_browser
            };
            host.Initialize();
            return host; 
        }

        internal void Reload()
        {
            if (!string.IsNullOrEmpty(this.m_baseuri))
                this.Navigate(this.m_baseuri);
            else {
                this.Navigate("about:blank");
            }
        }

      

        internal void SetUriStreamResolver(IWebBrowserHostStreamResolver resolver)
        {
            if (c_control is IGKXWebBrowserControl c)
            {
                c.SetUriStreamResolver(resolver);
                //(baseUri);
            }
        }

        internal void Navigate(string baseUri)
        {
            if (c_control is ICoreWebControl c)
            {
                c.LoadFromUriStream(baseUri);
                this.m_baseuri = baseUri;
            }


        }
        public event KeyEventHandler AccelerateKeyEvent {
            add {
                this.WebControl.AccelerateKeyEvent += value;
            }
            remove
            {
                this.WebControl.AccelerateKeyEvent -= value;
            }
        }

        private void Initialize()
        {
           
        }
    }
}
