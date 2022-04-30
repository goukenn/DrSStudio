using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.WinUI
{
    /// <summary>
    /// represent a drawing surface that will support multi framre
    /// </summary>
    public interface ICore2DDrawingMultiFrameSurface : ICore2DDrawingSurface
    {
        ICore2DDrawingFrameRendererCollections Frames { get; }
        ICore2DDrawingFrameRendererCollections OverlayFrames { get; }
    }
}
