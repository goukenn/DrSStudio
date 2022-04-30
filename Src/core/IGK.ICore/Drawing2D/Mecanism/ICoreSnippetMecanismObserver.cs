using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// snippet mecanism observer
    /// </summary>
    public interface ICoreSnippetMecanismObserver
    {
        Rectanglef GetClientRectangle(ICoreSnippet snippet);
    }
}
