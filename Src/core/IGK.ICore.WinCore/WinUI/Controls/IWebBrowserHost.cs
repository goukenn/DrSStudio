using System;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    public interface IWebBrowserHost : IDisposable
    {
        Control Host { get; }
        object ObjectForScripting { get; set; }
        bool NotLoaded { get; }

        event EventHandler LoadingCompleted;
        event KeyEventHandler AccelerateKeyEvent;


        object InvokeScript(string p, params string[] args);
        void SetBody(string v);
        void SetDocument(string html);
        void Reset();

        void SetStreamUriResolver(IWebBrowserHostStreamResolver streamResolver);
        void LoadHtmlString(string htmltext);
        void LoadFromUriStream(string uri);
    }
}