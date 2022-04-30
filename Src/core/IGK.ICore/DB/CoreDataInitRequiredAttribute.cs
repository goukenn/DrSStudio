using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    [AttributeUsage(AttributeTargets.Interface , AllowMultiple=true , Inherited=false)]
    /// <summary>
    /// get the type
    /// </summary>
    public class CoreDataInitRequiredAttribute : Attribute 
    {
        private Type m_type;

        /// <summary>
        /// get the type
        /// </summary>
        public Type Type
        {
            get { return m_type; }
        }
        
        public CoreDataInitRequiredAttribute(Type interfaceType)
        {
            this.m_type = interfaceType ?? throw new ArgumentException(nameof(interfaceType));
        }
    }
}
