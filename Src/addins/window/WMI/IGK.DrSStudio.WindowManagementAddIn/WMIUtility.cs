using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Management
{
    /// <summary>
    /// utility class
    /// </summary>
    class WMIUtility
    {
        static string[] classes;

        /// <summary>
        /// get all management class
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllManagementClass()
        {
            if (classes == null)
            {
                var scope = new ManagementScope("\\\\.\\root\\cimv2");
                var ob = new System.Management.SelectQuery(true, string.Empty);
                List<string> sb = new List<string>();
                foreach (var tt in (new ManagementObjectSearcher(scope, ob)).Get())
                {
                    sb.Add(tt.ClassPath.ClassName);
                }
                sb.Sort();
                classes = sb.ToArray();
            }
            return classes;
        }
    }
}
