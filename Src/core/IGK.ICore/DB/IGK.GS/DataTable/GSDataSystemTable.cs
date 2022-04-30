using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS.DataTable
{
    [AttributeUsage( AttributeTargets.Interface, AllowMultiple =  false , Inherited =false  )]
    /// <summary>
    /// used to register primary system table
    /// </summary>
    public class GSDataSystemTableAttribute : Attribute
    {
        private string m_name;

        public string Name {
            get {
                return m_name;
            }
        }
        public GSDataSystemTableAttribute(string name)
        {
            this.m_name = name;
        }
    }
}
