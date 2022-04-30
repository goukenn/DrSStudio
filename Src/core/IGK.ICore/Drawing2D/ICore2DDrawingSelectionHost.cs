using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    using IGK.ICore.GraphicModels;
    public interface  ICore2DDrawingSelectionHost
    {
        ICoreGraphics Device { get; }
        Rectanglef GetScreenBound(Rectanglef rc);
        Vector2f GetScreenLocation(Vector2f loc);
    }
}
