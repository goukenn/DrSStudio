using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Codec
{
    public class CoreVisitorManager
    {
        static Dictionary<string, Type> c = new Dictionary<string, Type>();
        static CoreVisitorManager() {
            //init visitor


            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadAssembly(item);

            }

            AppDomain.CurrentDomain.AssemblyLoad += InitVisitor;
        }

        private static void LoadAssembly(Assembly item)
        {
            foreach (var s in item.GetTypes())
            {
                if (s.GetCustomAttribute(typeof(CoreVisitorAttribute)) is CoreVisitorAttribute ct)
                 {
                    c.Add(ct.Name, s);          
                }
            }
        }

        private static void InitVisitor(object sender, AssemblyLoadEventArgs args)
        {
            LoadAssembly(args.LoadedAssembly);
        }
        /// <summary>
        /// get and create a new visitor
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CoreEncoderVisitor GetVisitor(string name) {
            if (!c.ContainsKey(name))
                return null;


            CoreEncoderVisitor g = null;
            var t = c[name];
            g = t.Assembly.CreateInstance(t.FullName) as CoreEncoderVisitor;


            return g;
        } 
    }
}
