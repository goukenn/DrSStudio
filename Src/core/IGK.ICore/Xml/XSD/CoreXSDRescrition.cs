using System;
using System.Collections;
using System.Collections.Generic;

namespace IGK.ICore.Xml.XSD
{
    internal class CoreXSDRescrition : ICoreXSDRestricAttributeData
    {
        private string m_pattern;

        public virtual bool HasRestriction=>true;

        public IEnumerator GetEnumerator() {
            Dictionary<string, object> r = new Dictionary<string, object> ();
            if (m_pattern !=null)
                r.Add ("pattern", m_pattern );
            return r.GetEnumerator();
        }
        public CoreXSDRescrition()
        {
        }

        internal CoreXSDRescrition SetPattern(string v)
        {
            m_pattern = v;
            return this;
        }

        public IEnumerable<KeyValuePair<string, object>> GetRestriction()
        {
            Dictionary<string, object> r = new Dictionary<string, object>();
            if (m_pattern != null)
                r.Add("xs:pattern", m_pattern);
            return r;
        }
    }
}