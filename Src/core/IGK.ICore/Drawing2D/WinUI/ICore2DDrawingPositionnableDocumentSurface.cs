using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D.WinUI
{
    /// <summary>
    /// a surface that support zindex positionning 
    /// </summary>
    public interface ICore2DDrawingPositionnableDocumentSurface : ICore2DDrawingSurface
    {
        event CoreWorkingObjectZIndexChangedHandler DocumentZIndexChanged;
    }
}
