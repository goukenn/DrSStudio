using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Interface  , AllowMultiple =false, Inherited=false )]
    public class GSSystemDataTable : Attribute 
    {
        private bool m_IsSystem;

        public bool IsSystem
        {
            get { return m_IsSystem; }
            set
            {
                if (m_IsSystem != value)
                {
                    m_IsSystem = value;
                }
            }
        }
        public GSSystemDataTable()
        {
            this.m_IsSystem = true;
        }
        public static bool IsSystemTable(Type t)
        {
            GSSystemDataTable c = Attribute.GetCustomAttribute (t, typeof(GSSystemDataTable)) as GSSystemDataTable;
                if (c!=null)
                    return c.IsSystem ;
            return false ;
        }
    }
}
