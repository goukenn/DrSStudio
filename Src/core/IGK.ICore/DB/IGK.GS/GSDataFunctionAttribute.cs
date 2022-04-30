using IGK.ICore.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    [Description("Mark a method that will be used for database function")]    
    [AttributeUsage (AttributeTargets.Method )]
    public class GSDataFunctionAttribute : Attribute 
    {
        public GSDataFunctionAttribute()
        {
        }

        
        public static MethodInfo[] GetMethods()
        {
            List<MethodInfo> mt = new List<MethodInfo>();
            try
            {
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var t in asm.GetTypes())
                    {

                        foreach (var m in t.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
                        {
                            if (Attribute.IsDefined(m, typeof(GSDataFunctionAttribute)))
                            {
                                mt.Add(m);
                            }
                        }

                    }
                }
            }
            catch { 
            }
            return mt.ToArray();
        }
    }
}
