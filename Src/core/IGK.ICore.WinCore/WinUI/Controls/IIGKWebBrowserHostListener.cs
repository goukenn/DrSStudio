using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    public interface IIGKWebBrowserHostListener
    {

        CoreXmlWebDocument Render();

        object ObjectForScripting { get; }
    }
}
