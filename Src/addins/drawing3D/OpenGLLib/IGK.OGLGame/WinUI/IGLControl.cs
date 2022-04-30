


using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGLControl.cs
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
file:IGLControl.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.OGLGame.WinUI.GLControls
{
    public interface IGLControl : IDisposable, IOGLGGraphicsView
    {
        int Width { get; }
        int Height { get; }
        /// <summary>
        /// get or set the background color
        /// </summary>
        Colorf SceneBackgroundColor{ get; set; }
        IGK.OGLGame.Graphics.OGLGraphicsDevice GraphicsDevice { get; }
        IGLControlItemCollections GLControls { get; }
        void Render();
    }
}

