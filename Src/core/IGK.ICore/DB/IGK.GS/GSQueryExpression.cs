using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// represent as sealed expression.    
    /// </summary>
    public sealed class GSQueryExpression : IGSQueryExpression
    {        
        private string m_expression;
        
        public string Expression
        {
            get { return m_expression; }
            set
            {
                if ((m_expression != value)&&(value !=null))
                {
                    m_expression = value;
                }
            }
        }

        public GSQueryExpression(string expression)
        {
            if (string.IsNullOrEmpty (expression))
                throw new  ArgumentException("expression");
            this.m_expression = expression;
        }
        public override string ToString()
        {
            return this.m_expression;
        }
    }
}
