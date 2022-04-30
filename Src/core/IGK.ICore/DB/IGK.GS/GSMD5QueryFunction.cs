using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// represent a md5 query function
    /// </summary>
    public sealed class GSMD5QueryFunction : GSDataExpression
    {
        private string m_value;

        public GSMD5QueryFunction(string value)
        {
            this.m_value = value;
        }
        public override string ToString()
        {
            return string.Format("MD5('{0}')", this.m_value);
        }
    }
}
