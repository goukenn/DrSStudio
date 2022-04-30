using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.JSon
{
    public class CoreJSonReader
    {
        const int NONE = 0;
        const int NAME = 1;
        const int VALUE = 2;

        int m_Type = NONE;
        int m_state = NONE;

        const int START_BLOCK = 1;
        const int END_BLOCK = 2;
        const int END_NAME = 7;
        const int START_VALUE  = 3;
        const int END_VALUE = 4;
        string m_value;

        const int VALUE_TYPE_DEF = 1;
            const int VALUE_TYPE_JSON = 2;
            const int VALUE_TYPE_ARRAY= 3;

        private StringReader SR;
        private int m_depth;
        private char startBox;
        private int m_json;

        static CoreJSonReader() { 
        }
        private CoreJSonReader() { 
        }
        public  static Dictionary<string, object> Load(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return null;
            CoreJSonReader c = new CoreJSonReader();

            StringReader sr = new StringReader(expression);
            c.SR = sr;
            //string t = string.Empty;

                var t = c.Load();
                return t;
        }

        private Dictionary<string, object> Load()
        {

            var t = new Dictionary<string, object>();
            string n = string.Empty;
            while (this.Read())
            {
                switch (this.m_Type)
                {
                    case NAME :
                        n = this.m_value.Trim();
                        break;
                    case VALUE +10 : 
                    case VALUE:
                        var s = this.m_value.Trim();
                        switch (this.m_json)
                        {
                            case VALUE_TYPE_JSON:
                                t.Add(n, CoreJSonReader.Load(s));
                                break;
                            case VALUE_TYPE_ARRAY:
                                t.Add(n, CoreJSonReader.LoadArray(s));
                                break;
                            default:
                                t.Add(n, s);
                                break;
                        }
                        this.m_Type = NONE;
                        this.m_state = START_BLOCK;
                        this.m_json = VALUE_TYPE_DEF;
                        n = null;
                        break;
                    default:
                        break;
                }
            }
            return t;
        }

        private static Array  LoadArray(string s)
        {
            List<object> obj = new List<object>();
            var h = s.ReadInBrancked('[', ']');
            if (h.Length == 1)
            {
                obj.AddRange(h[0].Split(','));
            }
            else { 
                //empty array
                //throw new NotImplementedException (" json load not implement");
            }          
            return obj.ToArray();
            
        }

        private bool Read()
        {
            int i = SR.Read();
            if (i == -1) { 
                return false ;
                
            }
            StringBuilder sb = new StringBuilder ();
            char ch = (char)i;
            bool end =false;
            while (!end)
            {
                switch (ch)
                {
                    case ',':
                        if (this.m_Type == NAME)
                        {
                            m_Type = VALUE;
                            m_state = END_VALUE;
                            m_value = string.Empty;
                            end = true;
                            return true;                        
                        }
                        break;
                    case '{':
                        if (m_state == NONE)
                        {
                            m_depth++;
                            m_state = START_BLOCK;
                        }
                        else if (m_state == END_NAME)
                        {
                            
                            sb.Append(ch);
                            sb.Append(ReadValue('}'));
                            m_Type = VALUE;
                            m_state = END_VALUE;
                            this.m_json =  VALUE_TYPE_JSON  ;
                            this.m_value = sb.ToString();
                        }
                        break;
                    case '}':                        
                        m_depth--;
                        break;
                    case ':':
                        if (m_state == END_NAME)
                        {
                            sb.Append(ReadValue());
                            m_Type = VALUE;
                            m_state = END_VALUE;
                            this.m_value = sb.ToString();
                            return true;
                        }
                        else {
                            if ((sb.Length > 0) && (m_state == 0))
                            {
                                this.m_value = sb.ToString();
                                this.m_state = END_NAME;
                                m_Type = NAME;
                                return true;
                            }
                        }
                        break;
                    case '[':
                        //array value
                        //read till closed value
                        
                        string bs = ReadValue(']');
                         m_Type = VALUE+10;//array value
                         m_state = END_VALUE;
                         m_json = VALUE_TYPE_ARRAY;
                         sb.Append("["+bs+"]");
                         this.m_value = sb.ToString();
                         return true;
                       
                    default:
                        switch (ch)
                        { 
                            case '"':
                            case '\'':
                                if (m_state == START_BLOCK)
                                {
                                    startBox = ch;
                                    sb.Append(ReadName());
                                    m_Type = NAME;
                                    m_state = END_NAME;
                                    m_value = sb.ToString();                                
                                }
                                else {
                                    if (m_state == END_NAME) {


                                    }
                                    m_state = START_BLOCK;
                                    m_Type = VALUE;
                                    m_value = ReadValue(ch);
                                    
                                }
                                return true;
                                
                            case ' ':
                                break;
                            default :
                                
                                switch  (m_state)
                                {
                                    case START_BLOCK:                                
                                    startBox = ':';
                                    sb.Append(ch);
                                    sb.Append(ReadName());
                                    m_Type = NAME;
                                    m_state = END_NAME;
                                    m_value = sb.ToString();
                                    return true;                                
                                    case  END_NAME:
                                        sb.Append(ch);
                                        sb.Append(ReadValue());
                                        m_Type = VALUE;
                                        m_state = END_VALUE;
                                        this.m_value = sb.ToString();
                                        return true ;
                                }
                                
                                break;
                        }
                        sb.Append(ch);
                        break;
                }
                i = SR.Read();
                if (i == -1)
                    break;
                ch = (char)i;
            }

            m_value = sb.ToString();
            return true;
        }
        private string ReadName() {
            int i = 0;            
            StringBuilder sb = new StringBuilder();
            while ((i = SR.Read()) != - 1)
            {
                if ((char)i == startBox)
                    break;
                sb.Append((char)i);
            }
            return sb.ToString();
        }
        private string ReadValue(char endbox=',') {
            int i = 0;
            StringBuilder sb = new StringBuilder();
            bool end = false;
            bool rbloc = false;
            bool cguillemet = false;

            if (endbox == '"' || endbox == '\'')
            {
                cguillemet = true;
                while (((i = SR.Read()) != -1) && (cguillemet || (i != endbox)))
                {
                    var ch = (char)i;

                    if (ch == endbox)
                    {
                        if (sb.Length > 0)
                        {//check if escaped guillemet
                            if (sb[sb.Length - 1] == '\\')
                            {
                                sb[sb.Length - 1] = ch;
                                continue;
                            }
                        }
                        end = true;
                    }
                    if (end)
                        break;
                    sb.Append(ch);
                }
                return sb.ToString();
            }

            while (((i = SR.Read()) != -1) && (cguillemet || (i != endbox )))
            {
                var ch = (char)i;
                switch (ch)
                {
                    case '"':
                        //if is escaped "
                        if (sb.Length > 0)
                        {//check if escaped guillemet
                            if (sb[sb.Length - 1] == '\\') {
                                sb[sb.Length - 1] = ch;
                                continue;
                            }
                        }
                        cguillemet = !cguillemet;//toogle cguillement 
                        continue;
                    case '{'://start ready a new expression if empty
                        rbloc = true;
                        break;
                    case '[':
                        break;
                    case ']':
                        if (endbox == '[')
                        {
                            m_json = VALUE_TYPE_ARRAY;
                        }
                        break;
                    case '}':
                        if (!rbloc)
                        {
                            if (endbox == '}')
                                sb.Append('}');
                            end = true;
                        }
                        break;
                    default:
                        
                        if (!cguillemet  &&  (ch ==  endbox))
                        {
                            end = true;
                        }
                        break;
                }
                if (end)
                    break;
                sb.Append(ch);
               
            }
            return sb.ToString();
        }
    }
}
