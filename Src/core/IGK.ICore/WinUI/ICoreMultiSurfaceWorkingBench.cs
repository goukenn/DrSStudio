using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    public interface ICoreMultiSurfaceWorkingBench : ICoreWorkbench
    {
        ICoreWorkingSurfaceCollections Surfaces { get; }
    }
}
