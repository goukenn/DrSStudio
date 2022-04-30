using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.ICore.JSon
{
    /// <summary>
    /// represent simple system json evaluator
    /// </summary>
    public class CoreJSon
    {
        public static  readonly string ExpressionRegex =  "\\{(?<expression>(.)+)\\}";

        public bool Evaluate(ICoreWorkingObject target, IJSonObjectListener host, string value)
        {
            //var t = target as ICoreWorkingAttachedPropertyObject;
            foreach (Match m in Regex.Matches(value, IGK.ICore.JSon.CoreJSon.ExpressionRegex))
            {
                string s = m.Groups["expression"].Value;
                string[] tab = s.Split (new char[]{',', '='});

                for (int i = 0; i < tab.Length; i+=2 )
                {
                    host.SetValue(tab[i].Trim(), tab[i + 1]);
                  
                }
                return true;
            }
            return false;
        }
        public Dictionary<string, object> ToDictionary(string expression)
        {
            var t = CoreJSonReader.Load(expression);
            return t;
                
            /*new Dictionary<string, object>();
            

            //var t = target as ICoreWorkingAttachedPropertyObject;
            Dictionary<string , string> v_data = new Dictionary<string,string> ();
            foreach (Match m in Regex.Matches(expression, IGK.ICore.JSon.CoreJSon.ExpressionRegex))
            {
                string s = m.Groups["expression"].Value;

                s = Regex.Replace(s, @":\s*\[(?<value>([^\]])+)\]", (tmatch)=>{ 
                        string data = "@data"+ v_data.Count ;
                        v_data.Add(data, tmatch.Groups["value"].Value);
                        return ":"+data;
                });
                string[] tab = s.Split(new char[] { ',', '=', ':' });

                for (int i = 0; i < tab.Length; i += 2)
                {
                    string n = getName(tab[i].Trim());
                    string v = tab[i + 1];
                    if (v_data.ContainsKey(v))
                    {
                        //data is array
                        t[n] = getData(v_data[v]);
                    }
                    else
                    {
                        t.Add(n, v);
                    }
                }
            }
            return t;*/
        }

        private object getData(string p)
        {
            var t = p.Split(',');
            List<string> v = new List<string>();
            for (int i = 0; i < t.Length; i++)
            {
                v.Add(t[i].Trim());
            }            
            return v.ToArray();
        }
        public Dictionary<string, object> ToDictionary(string expression, string filter)
        {
            if (string.IsNullOrEmpty(expression))
                return null;
            var d = ToDictionary(expression);
            Dictionary<string, object> m = new Dictionary<string, object>();
            foreach (var item in d)
            {
                if (Regex.IsMatch(item.Key, filter))
                {
                    m.Add(item.Key, item.Value);
                }
            }
            return m;
        }
        private static string getName(string p)
        {
            var m = Regex.Match(p, "('|\")?(?<name>(.)+)(\\1)");
            if (m.Success)
                return m.Groups["name"].Value;
            return p;
        }
    }
}
