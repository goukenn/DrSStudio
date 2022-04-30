

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreCameraOwner.cs
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
file:ICoreCameraOwner.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing3D
{
    public interface  ICoreCameraOwner
    {
        ICoreCamera Camera { get; }
        /// <summary>
        /// get window cordinate from Vector2i
        /// </summary>
        /// <param name="Vector2i"></param>
        /// <returns></returns>
        Vector3f GetWindowCoord(Vector3f Vector2i);
        /// <summary>
        /// get the word cordinate form window cordinate
        /// </summary>
        /// <param name="vector3f"></param>
        /// <returns></returns>
        Vector3f[] MapWindowCordinate(Vector3f[] vector3f);
    }
}

