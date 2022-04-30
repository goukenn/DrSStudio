using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.ICore
{
    public static class CoreTypeExtensions
    {
        public static string[] GetMethodLists(this Type t, BindingFlags flag)
        {
            if (t == null)
                return null;
            List<string> methodName = new List<string>();
            foreach (var m in t.GetMethods(flag))
            {
                if (methodName.Contains(m.Name))
                    continue;
                methodName.Add(m.Name);
            }
            return methodName.ToArray();
        }

        public static Type[] GetTypes(this object[] param) {
            if (param == null)
                return new Type[0];
            Type[] t = new Type[param.Length];
            for (int i = 0; i < t.Length; i++)
            {
                object b = param[i];
                if (b == null)
                {
                    t[i] = typeof(object);
                }
                else {
                    t[i] = b.GetType();
                }
            }
            return t;
        }
    }
}
