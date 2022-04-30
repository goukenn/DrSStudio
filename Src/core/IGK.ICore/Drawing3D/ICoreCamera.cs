

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreCamera.cs
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
file:ICoreCamera.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing3D
{
    public interface ICoreCamera
    {
        /// <summary>
        /// projection matrix
        /// </summary>
        Matrix Projection { get; }
        /// <summary>
        /// model view matrix
        /// </summary>
        Matrix ModelView { get; }
        /// <summary>
        /// get or set the viewport
        /// </summary>
        Rectanglei Viewport { get; set; }
    }
}

