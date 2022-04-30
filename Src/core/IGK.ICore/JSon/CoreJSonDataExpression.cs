using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.JSon
{
    /// <summary>
    /// represent an expression value
    /// </summary>
    public class CoreJSonDataExpression
    {
        Dictionary<string, string> m_data;
        public CoreJSonDataExpression()
        {

            this.m_data = new Dictionary<string, string>();
        }
        internal void Add(string name, string value)
        {
            m_data.Add(name, value);
        }
    }
}
