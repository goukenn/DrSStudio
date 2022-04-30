using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a worbench that can manipulate multiple surface
    /// </summary>
    public interface  ICoreSurfaceManagerWorkbench : ICoreWorkbench 
    {
        ICoreWorkingSurfaceCollections Surfaces { get; }
        event EventHandler<CoreItemEventArgs<ICoreWorkingSurface>> SurfaceAdded;
        event EventHandler<CoreItemEventArgs<ICoreWorkingSurface>> SurfaceRemoved;
        event EventHandler<CoreSurfaceClosedEventArgs> SurfaceClosed;
    }
}
