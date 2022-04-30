using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    public interface IGSFunctionCollections
    {
        int Count { get;  }
        object Call(string name, object[] args);
    }
}
