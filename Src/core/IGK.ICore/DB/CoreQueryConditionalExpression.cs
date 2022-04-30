using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    public class CoreQueryConditionalExpression : ICoreDataQueryExpression, ICoreQueryConditionalExpression
    {
        private List<CoreQueryConditional> m_expression;
        private bool m_operand;
        

        public CoreQueryConditionalExpression()
        {
            m_expression = new List<CoreQueryConditional>();
        }
        public ICoreQueryConditionalExpression add(string expression, string operand = "AND")
        {
            if (string.IsNullOrEmpty(expression))
                return this;
            if (this.Count == 0)
            {
                this.m_expression.Add(new CoreQueryConditional(expression));                
            }
            else {
                if (!this.m_operand)
                this.m_expression.Add(operand == "AND" ? CoreQueryConditionalOperand.And : CoreQueryConditionalOperand.Or);
                this.m_expression.Add(new CoreQueryConditional(expression));
                if (this.m_operand)
                    this.m_operand = false;
            }
            //this.m_nextOperant = operand;
            return this;
        }
        public void addSeparator(string operand = "AND")
        {
            if (Count == 0)
                return;
            this.m_expression.Add(operand == "AND" ? CoreQueryConditionalOperand.And : CoreQueryConditionalOperand.Or);
            m_operand = true;
        }
        public void Clear()
        {
            m_expression.Clear();
        }

        public int Count
        {
            get { return m_expression.Count; }
        }
        class CoreQueryConditional
        {
            private string value;

            public CoreQueryConditional(string value)
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this.value = value;
            }
            public override string ToString()
            {
                return this.value;
            }
        }

        class CoreQueryConditionalValue : CoreQueryConditional
        {
            public CoreQueryConditionalValue(string name, object value):
                base(string.Format ("`{0}`={1}", name, value ))
            {

            }
        }

        sealed class CoreQueryConditionalOperand : CoreQueryConditional
        {
            public static readonly CoreQueryConditionalOperand And;
            public static readonly CoreQueryConditionalOperand Or;

            static CoreQueryConditionalOperand() {
                And = new CoreQueryConditionalOperand("And");
                Or = new CoreQueryConditionalOperand("Or");
            }
            private CoreQueryConditionalOperand(string value):base(value)
            {

            }
        }

        public string Expression
        {
            get {
                if (this.Count == 0)
                    return string.Empty;
                StringBuilder sb = new StringBuilder();
                foreach (var item in this.m_expression)
                {
                    if (sb.Length > 0) sb.Append(" ");
                    sb.Append(item.ToString());
                }
                return sb.ToString();
            }
        }

      
    }
}
