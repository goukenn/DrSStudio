

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreTransformObject.cs
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
file:ICoreTransformObject.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public interface ICoreTransformObject<T, M>
    {
        bool CanReSize2i { get; }
        bool CanRotate { get; }
        bool CanScale { get; }
        bool CanTranslate { get; }
        void ClearTransform();
        void ResetTransform();
        void FlipX();
        void FlipY();
        void Align(enuCore2DAlignElement alignment, M Bounds);
        void Dock(enuCore2DDockElement dock, M docBound);
        void Translate(T dx, T dy, enuTransformOrder order);
        void Scale(T ex, T ey, enuTransformOrder order);
        void Rotate(T angle, enuTransformOrder order);
    }
}

