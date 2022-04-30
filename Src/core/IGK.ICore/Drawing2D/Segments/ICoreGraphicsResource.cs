using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D.Segments
{
    public  interface ICoreGraphicsResource
    {
        bool GetGraphicsDefinition(Vector2f[] vector2f, bool p, out Vector2f[] pt, out byte[] def);
    }
}
