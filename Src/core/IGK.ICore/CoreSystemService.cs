using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// Manage Type attribute. Registrated with CoreServiceAttribute
    /// </summary>
    public sealed class CoreSystemServices
    {
        private static CoreSystemServices sm_instance;
        Dictionary<string, Type> m_services;

        private CoreSystemServices()
        {
            m_services = new Dictionary<string, Type>();
            CoreSystem.Instance?.RegisterAssemblyLoader(InitLoadAssemblyResources);
            __findRegisteredService();
        }

        private void InitLoadAssemblyResources(Assembly assembly)
        {
            foreach (var i in assembly.GetTypes())
            {

                var g = i.GetCustomAttribute(typeof(CoreServiceAttribute)) as CoreServiceAttribute;
                if (g != null)
                {
                    m_services.Add(g.Name, i);
                    g.m_type = i;
                }

            }

        }

        private void __findRegisteredService()
        {
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                InitLoadAssemblyResources(item);
            }
        }

        public static CoreSystemServices Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreSystemServices()
        {
            sm_instance = new CoreSystemServices();
        }

        public static object GetServiceByName(string id)
        {
            if (sm_instance.m_services.ContainsKey(id))
            {
               var  t = sm_instance.m_services[id];
                return t.Assembly.CreateInstance(t.FullName);
            }
            return null;
        }
    }


}
