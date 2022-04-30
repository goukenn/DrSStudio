using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WiXAddIn
{
    /// <summary>
    /// represent a wix project variables
    /// </summary>
    public class WiXProjectVariableCollections : IEnumerable 
    {
        private WiXProject wiXProject;
        private Dictionary<string, object> m_data;

        internal  WiXProjectVariableCollections(WiXProject wiXProject)
        {            
            this.wiXProject = wiXProject;
            this.m_data = new Dictionary<string, object>();
        }
        public int Count { get { return this.m_data.Count; } }
        public void Add(string name, object data) {
            this.m_data.Add(name, data);
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_data.GetEnumerator();
        }

        internal void Clear()
        {
            this.m_data.Clear();
        }
    }
}
