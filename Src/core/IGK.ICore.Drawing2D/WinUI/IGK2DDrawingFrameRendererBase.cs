
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D.WinUI
{
    using IGK.ICore.GraphicModels;

    /// <summary>
    /// reprensent the base frame renderer entity
    /// </summary>
    public abstract class IGK2DDrawingFrameRendererBase : ICore2DDrawingFrameRenderer 
    {
        public abstract void Render(ICoreGraphics device);
    }
}
