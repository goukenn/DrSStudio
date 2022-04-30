using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.WebGLLib.WinUI
{
    /// <summary>
    /// represent a global interface
    /// </summary>
    public interface IWebGLDesignSurface
    {
        IWebGLGameScene CurrentScene { get; set; }


        event EventHandler CurrentSceneChanged;
    }
}
