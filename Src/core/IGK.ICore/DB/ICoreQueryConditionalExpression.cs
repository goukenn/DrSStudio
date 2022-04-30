using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    public interface ICoreQueryConditionalExpression
    {
        ICoreQueryConditionalExpression add(string expression, string operand = "AND");       
        void Clear();        
        int Count { get; }
    }
}
