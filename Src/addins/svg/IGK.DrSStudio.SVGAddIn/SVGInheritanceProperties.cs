using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SVGAddIn
{
    /// <summary>
    /// used to store inheritance properties
    /// </summary>
    class SVGInheritanceProperties : IEnumerable
    {
        Dictionary<string, string> m_values;
        ///<summary>
        ///public .ctr
        ///</summary>
        public SVGInheritanceProperties()
        {
            this.m_values = new Dictionary<string, string>();
        }
        public string GetDefinition(string name) {
            if (m_values.ContainsKey(name))
                return m_values[name];
            return null;
        }

        public T GetObject<T>(string name){

            return default(T);
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_values.GetEnumerator();
        }
        /// <summary>
        /// number of value stored
        /// </summary>
        public int Count => m_values.Count;

        public SVGDefElement Defs { get; internal set; }

        public void SetDefinition(string name, string value) {
            if (m_values.ContainsKey(name))
            {
                if (value == null)
                    m_values.Remove(name);
                else
                    m_values[name] = value;
            }
            else if (value != null) {
                m_values.Add(name, value);
            }
        }
        
    }
}
