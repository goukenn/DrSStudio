using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.WebGLLib
{

    /// <summary>
    /// represent a javascript utility class
    /// </summary>
    public static class JSUtils
    {
        public static string EscapeString(this string m){
            if (string.IsNullOrEmpty(m))
                return null;
            StringBuilder sb = new StringBuilder();
            char p = (char)0;
            for (int i = 0; i < m.Length; i++)
            {
                switch(m[i]){
                    case '\'':
                        if (p != '\'') {
                            //already escaped
                            sb.Append("\\'");
                            continue;
                        }                        
                        break;
                }
                p = m[i];
                sb.Append(p);
            }
            return sb.ToString();
        }
    }
}
