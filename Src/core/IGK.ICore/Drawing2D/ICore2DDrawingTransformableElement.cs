

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingTransformableElement.cs
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
file:ICore2DDrawingTransformableElement.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    public interface ICore2DDrawingTransformableElement:  
        ICore2DDrawingElement  ,       
        ICore2DDrawingTransform
    {
        bool CanScale { get; }
        bool CanRotate { get; }
        bool CanReSize { get; }
        bool CanTranslate { get; }
        /// <summary>
        /// get the current matrix
        /// </summary>
        /// <returns></returns>
        Matrix GetMatrix();
        /// <summary>
        /// apply the matrix to the drawing element
        /// </summary>
        /// <param name="matrix"></param>
        void Transform(Matrix matrix);
        /// <summary>
        /// multiply the current matrix with this specified matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="order"></param>
        void MultTransform(Matrix matrix, enuMatrixOrder order);
        /// <summary>
        /// reset transform after apply the current matrix to definition;
        /// </summary>
        void ResetTransform();
        /// <summary>
        /// clear current tranform. element is set the the creation model.
        /// </summary>
        void ClearTransform();

        void Inflate(float ex, float ey, enuMatrixOrder mat);
        void Align(enuCore2DAlignElement alignment);
        void Align(enuCore2DAlignElement alignment, Rectanglef Bounds);
        void Dock(enuCore2DDockElement dock);
        void Dock(enuCore2DDockElement dock, Rectanglef docBound);
        void FlipX();
        void FlipY();        
    }
}

