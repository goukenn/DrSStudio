using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// used to easy write dictinary data expression
    /// </summary>
    public sealed class GSDictionary
    {
        private Dictionary<string, object> m_dic;        

        public int Count
        {
            get { return m_dic.Count; }
        }
        public GSDictionary()
        {
            this.m_dic = new Dictionary<string, object>();
        }
        public Dictionary<string, object> ToDictionary() {
            return this.m_dic;
        }

        public GSDictionary Add(string key, Object value) {
            this.m_dic.Add(key, value);
            return this;
        }
    }
}
