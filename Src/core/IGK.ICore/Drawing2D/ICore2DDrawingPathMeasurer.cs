using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    public interface  ICore2DDrawingPathMeasurer
    {
        Rectanglef GetBounds(CoreGraphicsPath path);
    }
}
