using IGK.ICore.WinCore.WinUI.Controls;
using System;

namespace IGK.DRSStudio.BalafonDesigner.WinUI
{
    public interface IBalafonViewDesignerSurface
    {
        IBindToolHost BindTool { get; set; }        
   
        void Reload();
        void SetUriStreamResolver(IWebBrowserHostStreamResolver resolver);

        /// <summary>
        /// savigate to base uri
        /// </summary>
        void Navigate();
        /// <summary>
        /// navigate 
        /// </summary>
        /// <param name="uri">base uri to start tag</param>
        void Navigate(string uri);
    }
}