using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    public interface IGSValueProvider
    {
        object GetValue(string name, object dicValue);
    }
}
