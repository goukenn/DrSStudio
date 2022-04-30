using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.OGLGame
{
    /// <summary>
    /// represent public delegate to create and select custom opengl version
    /// </summary>
    /// <param name="hdc"></param>
    /// <returns></returns>
    public delegate IntPtr OGLGameCreateDeviceProc(IntPtr hdc);
}
