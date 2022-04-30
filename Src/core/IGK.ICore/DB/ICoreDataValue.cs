using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    public interface ICoreDataValue
    {
        object GetValue(string name);
        T GetValue<T>(string name, T defaul = default (T));
    }
}
