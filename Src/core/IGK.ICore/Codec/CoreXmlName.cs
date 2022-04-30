using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Codec
{
    public class CoreXMLNameAttribute : Attribute 
    {        
        private string m_name;

        public string name
        {
            get { return m_name; }
        }
        public CoreXMLNameAttribute(string name)
        {
            this.m_name = name;
        }
    }
}
