using IGK.DrSStudio.BalafonDesigner.Tools;
using IGK.ICore;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DRSStudio.BalafonDesigner.WinUI
{

    [CoreSurface(BalafonViewDesignerConstants.SURFACE_GUID)]
    /// <summary>
    /// represent a balafon designer surface
    /// </summary>
    internal class BalafonViewDesignerSurface :
        IGKXWinCoreWorkingSurface,
        ICoreWorkingReloadViewSurface,
        IBalafonViewDesignerSurface
    {

        //web view host
        private BalafonViewDesignerWebViewHost m_surface;
        private string m_baseUri;
        ///<summary>
        ///public .ctr
        ///</summary>
        public BalafonViewDesignerSurface()
        {

            m_surface = BalafonViewDesignerWebViewHost.CreateSurface(this) ??
                throw new BalafonDesignerException("failed to create surface");
            this.m_baseUri =
               // "/!@res//google/cssfont/Roboto100x200x400x700x900/KFOkCnqEu92Fr1MmgVxHIzIFKw.woff2";
                //"/!@res//getgooglefont?uri=aHR0cHM6Ly9mb250cy5nb29nbGVhcGlzLmNvbS9jc3M/ZmFtaWx5PU9wZW4rU2FucytDb25kZW5zZWQ6MzAw";
                "/index.php";// BindTool?.BaseUri;
            this.Controls.Add(m_surface.Control);
        }

        public IBindToolHost BindTool { get; set; }


        public BalafonViewDesignerWebViewHost WebHost => m_surface;

        public string BaseUri
        {
            get => m_baseUri;
            set => m_baseUri = value;
        }
    

      

        public void Reload()
        {
            this.m_surface.Reload();
        }

        public void SetUriStreamResolver(IWebBrowserHostStreamResolver resolver)
        {
            this.m_surface.SetUriStreamResolver(resolver);
        }

        /// <summary>
        /// navigate to destination
        /// </summary>
        public void Navigate()
        {
            string uri = "http://localhost:" + this.BindTool.Port + this.BaseUri;            
            this.m_surface.Navigate(uri);
        }

        public void Navigate(string uri)
        {
            this.BaseUri = uri;
            this.Navigate();
        }
    }
}
