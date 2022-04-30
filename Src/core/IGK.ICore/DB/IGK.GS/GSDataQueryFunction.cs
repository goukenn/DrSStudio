using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// represent a query function
    /// </summary>
    public class GSDataQueryFunction : GSDataExpression
    {
        private string m_functionName;
        private string m_Value;
        public GSDataQueryFunction(string functionName, string Value)
        {
            this.m_functionName = functionName;
            this.m_Value = Value;
        }
        public override string ToString()
        {
            return string.Format("{0}('{1}')", this.m_functionName, this.m_Value);
        }
    }
}
