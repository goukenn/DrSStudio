using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Dependency
{
    [AttributeUsage (AttributeTargets.Class , Inherited = false , AllowMultiple=false )]
    /// <summary>
    /// used to register dependency Attribute name
    /// </summary>
    public class CoreDependencyNameAttribute : CoreAttribute 
    {
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
    

        public CoreDependencyNameAttribute(string name):base()
        {
            this.m_Name = name;
        }
    }
}
