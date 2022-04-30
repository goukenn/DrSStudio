using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// dictionary extensions
    /// </summary>
    public static class CoreDictionaryExtensions
    {
        public static  T CoreGetValue<T,M>(this Dictionary<string, M> data, string key, T defaultValue=default (T))
        {
            if ((data == null) || !data.ContainsKey(key))
                return defaultValue;
            return CoreExtensions.CoreGetValue<T>(data[key], defaultValue);
        }
        public static T CoreGetValue<T>(this Dictionary<string, object> data, string p, T defaultv = default(T))
        {
            return data.GetValue<string, object, T>(p, defaultv);
        }
    }
}
