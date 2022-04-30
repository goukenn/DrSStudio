using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore
{
    public interface ICoreBrushBuilder
    {
        void Setup(ICoreBrush brush, params object [] param );
    }
}
