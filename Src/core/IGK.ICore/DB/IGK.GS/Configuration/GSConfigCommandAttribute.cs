using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Configuration
{
    [AttributeUsage (AttributeTargets.Class )]
    public class GSConfigCommandAttribute : Attribute 
    {
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
        }
        public GSConfigCommandAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            this.m_Name = name;
        }
    }
}
