using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    [AttributeUsage(AttributeTargets.Assembly,Inherited=false, AllowMultiple=false)]
    public class GSApplicationInitializerAttribute : Attribute 
    {
        private Type m_Type;
        /// <summary>
        /// get or set the type used to initialize your application
        /// </summary>
        public Type Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type != value)
                {
                    m_Type = value;
                }
            }
        }
    }
}
