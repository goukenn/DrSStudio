using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited =true)]
    public class CoreServiceAttribute : CoreAttribute 
    {
        private string m_name;
        internal Type m_type; //type that will receive that attribute

        public string Name => m_name;
        public Type AttachedTo => m_type;
        ///<summary>
        ///public .ctr
        ///</summary>
        public CoreServiceAttribute(string registerName)
        {
            this.m_name = registerName;
            m_type = null;
        }
    }
}
