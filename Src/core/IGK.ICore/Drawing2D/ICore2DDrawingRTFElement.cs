using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a rtf element
    /// </summary>
    public interface  ICore2DDrawingRTFElement :ICore2DDrawingLayeredElement
    {
        string RtfText { get; }
        Rectanglef Bounds { get; }
    }
}
