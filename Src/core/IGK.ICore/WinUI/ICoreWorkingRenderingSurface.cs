

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingRenderingSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ICoreWorkingRenderingSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a rendering surface
    /// </summary>
    public interface  ICoreWorkingRenderingSurface : ICoreWorkingSurface 
    {
        /// <summary>
        /// get the handle of the rendering scene. used in conjonction of that the rendering scene can be different from
        /// the working surface
        /// </summary>
        IntPtr SceneHandle { get; }
        /// <summary>
        /// refresh the current scene
        /// </summary>
        void RefreshScene();
        /// <summary>
        /// refresh the current 
        /// </summary>
        /// <param name="forceUpdate">true to force the update</param>
        void RefreshScene(bool forceUpdate);
    }
}

