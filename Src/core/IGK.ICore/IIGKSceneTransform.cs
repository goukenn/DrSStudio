using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public interface IIGKSceneTransform
    {
        float ZoomX { get; }
        float ZoomY { get; }
        void Zoom();

    }
}
