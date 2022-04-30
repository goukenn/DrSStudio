using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    public interface ICoreGKDSElementItem : ICoreWorkingObject 
    {
        GKDSElement Parent { get; }
    }
}
