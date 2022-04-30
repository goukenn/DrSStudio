using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SVGAddIn
{
    internal class SVGStyleAttributeReader
    {
        private StringReader m_stringBuilder;
        private string m_name;
        private string m_value;

        public string Name => m_name;
        public string Value => m_value;
        const int READ_NAME = 0;
        const int READ_VALUE = 1;
        const int READ_VALUE_STRING = 3; //value in string
        ///<summary>
        ///public .ctr
        ///</summary>
        public SVGStyleAttributeReader(string v)
        {
            this.m_stringBuilder = new StringReader(v);
        }
        public bool Read()
        {
            StringBuilder sb = new StringBuilder();
            char[] buffer = new char[1];
            int mode = READ_NAME;

            while (this.m_stringBuilder.Read(buffer, 0, 1) > 0)
            {

                switch (buffer[0])
                {
                    case '\'':
                    case '"':
                        if (mode == READ_VALUE)
                        {
                            mode = READ_VALUE_STRING;
                        }
                        else if (mode == READ_VALUE_STRING)
                        {
                            mode = READ_VALUE;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case ';':
                        if (mode == READ_VALUE)
                        {
                            this.m_value = sb.ToString();
                            return true;
                        }
                        break;
                    case ':'://separator
                        if (mode == READ_NAME)
                        {
                            this.m_name = sb.ToString().Trim();
                            this.m_value = string.Empty;
                            sb.Length = 0;
                            mode = READ_VALUE;
                            continue;
                        }
                        else if (mode == READ_VALUE)
                        {
                        }
                        break;
                    default:
                        break;
                }
                sb.Append(buffer[0]);

            }
            if (mode == READ_VALUE)
            {
                this.m_value = sb.ToString();
                return true;
            }

            return false;
        }

        internal static Dictionary<string, string> ReadAll(string v)
        {
            var dic = new Dictionary<string, string>();
            var h = new SVGStyleAttributeReader(v);
            while (h.Read()) {
                if (dic.ContainsKey(h.Name)) {
                    dic[h.Name] = h.Value;
                }
                else
                dic.Add(h.Name, h.Value);
            }

            return dic;
        }
    }
}
