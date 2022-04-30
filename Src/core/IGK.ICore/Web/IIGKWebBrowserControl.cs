using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Web
{
    public interface IIGKWebBrowserControl
    {
        bool IsBodyDefined { get; }
        string DocumentText{get;set;}
        void setBodyInnerHtml(string text);

        void InvokeScript(string function, string[] parameters);
    }
}
