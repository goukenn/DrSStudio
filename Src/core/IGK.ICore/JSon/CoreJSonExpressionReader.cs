using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.JSon
{
    /// <summary>
    /// used to read expression a return a collection of key value expression value
    /// </summary>
    public class CoreJSonExpressionReader
    {
        private string m_data;
        private StringBuilder m_sb;
        private string m_Name;
        private string m_Value;

        public string Value
        {
            get { return m_Value; }
        }
        public string Name
        {
            get { return m_Name; }         
        }
        private CoreJSonExpressionReader(string data)
        {
            this.m_data = data;
            this.m_Name = string.Empty;
            this.m_Value = string.Empty;
            this.m_sb = new StringBuilder(data);
        }
        public static CoreJSonDataExpression ReadExpression(string data)
        {
            CoreJSonDataExpression v_exp = new CoreJSonDataExpression();
            CoreJSonExpressionReader v_reader = new CoreJSonExpressionReader(data);
            while (v_reader._Read())
            {
                v_exp.Add(v_reader.Name, v_reader.Value);
            }
            return v_exp;
        }
        public bool _Read() {
            return false;    
        }
    }
}
