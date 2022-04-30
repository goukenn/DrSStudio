using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.OGLGame.V2
{
    public interface IGLGame : IGLGameComponent
    {
        string Title { get; set; }
        Size2i WindowSize { get; set; }
    }
}
