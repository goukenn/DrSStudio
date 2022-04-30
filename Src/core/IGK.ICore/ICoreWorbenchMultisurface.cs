using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// represent a workbench that can host multiple surface
    /// </summary>
    public interface ICoreWorbenchMultisurface
    {
        ICoreWorkingSurfaceCollections Surfaces{get;}
    }
}
