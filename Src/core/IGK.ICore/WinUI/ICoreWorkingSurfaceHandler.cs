using System;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a global surface handler
    /// </summary>
    public interface ICoreWorkingSurfaceHandler:  ICoreWorkingSurfaceHost
    {

        /// <summary>
        /// get or set the current surface
        /// </summary>
        new ICoreWorkingSurface CurrentSurface { get; set; }

        event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>> CurrentSurfaceChanged;
    }
}