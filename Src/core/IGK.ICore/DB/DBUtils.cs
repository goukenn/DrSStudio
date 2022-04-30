using IGK.ICore.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IGK.ICore.DB
{
    public class DBUtils
    {
        public static T Convert<T>(Dictionary<string, object> entry, string name,T defaultValue = default (T))
        {
            if (!entry.ContainsKey(name))
                return defaultValue;
            object v_obj = entry[name];
            if (v_obj == null)
                return defaultValue;
            if (v_obj.GetType() == typeof(T))
            {
                return (T)v_obj;
            }
            TypeConverter conv = CoreTypeDescriptor.GetConverter(typeof(T));
            if ((conv != null) && conv.CanConvertFrom(v_obj.GetType()))
                return (T)conv.ConvertFrom(v_obj);
            return defaultValue;
        }
        
        public static T GetValue<T>(object item, T p = default (T))
        {
            Type t = typeof(T);
            if (item is T)
                return (T)item;
            if ((t == typeof (string)) && (item !=null))
            {
                return (T) (object ) item.ToString();
            }
            TypeConverter conv = CoreTypeDescriptor.GetConverter(typeof(T));
            if ((item!=null) && (conv != null) && conv.CanConvertFrom(item.GetType()))
                return (T)conv.ConvertFrom(item);
            return p;
        }
    }
}
