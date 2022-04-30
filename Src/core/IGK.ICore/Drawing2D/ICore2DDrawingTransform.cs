using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent some common 2D transformation
    /// </summary>
    public interface ICore2DDrawingTransform
    {
        void Translate(float dx, float dy, enuMatrixOrder mat);
        void Scale(float ex, float ey, enuMatrixOrder mat);
        void Rotate(float angle, Vector2f center, enuMatrixOrder mat);
    }
}
