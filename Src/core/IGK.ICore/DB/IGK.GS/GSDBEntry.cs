using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    class GSDBEntry
    {
        private System.Type m_Type;
        private Dictionary<string, string> m_entry;
        

        public System.Type Type
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
        internal GSDBEntry(Type typeofInterface, Dictionary<string,string> entry)
        {
            this.m_Type = typeofInterface;
            this.m_entry = entry;

        }
        public static GSDBEntry CreateEntry(Type typeofInterface, Dictionary<string, string> entry)
        {
            return null;
        }

        public string GetValue(string name)
        {
            return m_entry[name];
        }
        public void SetValue(string name, string value)
        {
            m_entry[name] = name;
        }

        public void Commit()
        { 
        }
    }
}
