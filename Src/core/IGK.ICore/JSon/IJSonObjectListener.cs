using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.JSon
{
    /// <summary>
    /// interface used to get or store json object properties
    /// </summary>
    public interface  IJSonObjectListener
    {
        string ToJSonString();
        void SetValue(string name, string value);
        object GetValue(string name);
        T GetValue<T>(string name);
    }
}
