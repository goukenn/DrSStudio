using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Dependency
{
    public static class CoreDependencyExtension
    {
       static Dictionary<object , Dictionary<string, object >> sm_depencies;

        static CoreDependencyExtension(){ 
            sm_depencies = new Dictionary<object,Dictionary<string,object>> ();
        }
        public static string[] GetParamKeys(this object obj){
        if(sm_depencies.ContainsKey (obj)){
            return sm_depencies[obj].Keys .ToArray<string>();
        }
        return null;}
        public static  IEnumerable GetParams(this object obj){
            if(sm_depencies.ContainsKey (obj)){
                return sm_depencies[obj];
            }
            return null;
        }
        public static object  GetParam(this object obj, string key)
        {
            if (sm_depencies.ContainsKey(obj))
            {
                return sm_depencies[obj].ContainsKey (key)? sm_depencies[obj][key] : null;
            }
            return null;
        }
        public static bool SetParam(this object obj, string key, object value) {
            if (sm_depencies.ContainsKey(obj))
            {
                var g = sm_depencies[obj];
                if (g.ContainsKey(key))
                {
                    g[key] = value;
                }else
                g.Add(key, value);
                return true;
            }
            else
            {
                var g = new Dictionary<string, object>();
                g.Add(key, value);
                sm_depencies.Add(obj, g);
            }
            return false;
        }
    }
}
