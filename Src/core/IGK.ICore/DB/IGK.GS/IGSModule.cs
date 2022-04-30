using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    /// <summary>
    /// represent a gs module
    /// </summary>
    public interface IGSModule 
    {
        string Name { get; }
        IGSFunctionCollections Functions { get; }
        bool Initilalize();
    }
}
