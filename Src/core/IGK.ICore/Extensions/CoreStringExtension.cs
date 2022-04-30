using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.ICore.Extensions
{
    public static class CoreStringRegexExtension
    {
        public static CoreMatchExtensionCollection MatchExpression(this string value,
            string pattern, RegexOptions exp = RegexOptions.IgnoreCase)
        {
            var v_regex = new Regex(pattern, exp);
            var g = v_regex.Matches(value);
            if (g != null && g.Count > 0)
            {

                CoreMatchExtensionCollection c = new CoreMatchExtensionCollection();
                for (int i = 0; i < g.Count; i++)
                {
                    var t = g[i];
                    foreach (var item in v_regex.GetGroupNames())
                    {
                        c.Add(item, t.Groups[item].Value);
                    }

                }
                return c;
            }
            return null;
        }
    }

    public class CoreMatchExtensionCollection
    {
        Dictionary<string, string> v_list;
        public string this[string s]
        {
            get
            {
                if (string.IsNullOrEmpty(s) || !this.v_list.ContainsKey(s))
                    return null;
                return v_list[s];
            }
        }
        internal CoreMatchExtensionCollection()
        {
            this.v_list = new Dictionary<string, string>();
        }
        internal void Add(string item, string value)
        {
            if (string.IsNullOrEmpty(item))
                return;
            this.v_list.Add(item, value);
        }
    }

}
