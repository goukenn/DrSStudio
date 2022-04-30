using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore
{
    public interface ICorePathBrushGenerator
    {
        object Generate(ICoreBrushBuilder builder);
    }
}
