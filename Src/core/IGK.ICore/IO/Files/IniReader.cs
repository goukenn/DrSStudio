

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IniReader.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IniReader.cs
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace IGK.ICore.IO.Files
{
    /// <summary>
    /// represent a ini file reader
    /// </summary>
    public class IniReader
    {
        Dictionary<string, List<IniValue>> m_values;
        public int GroupCount {
            get {
                return m_values.Count;
            }
        }
        public string[] GetKeys() {
            return m_values.Keys.ToArray();
        }
        public IniValue[] this[string name]
        {
            get {
                if (m_values.ContainsKey (name ))
                    return m_values[name].ToArray();
                return null;
            }
        }
        private IniReader ()
        {
            m_values = new Dictionary<string, List<IniValue>>();
        }
        public static IniReader LoadIni(string filename)
        {
            if (!File.Exists (filename ))
                return null;
            IniReader r = new IniReader ();
            string[] t = File.ReadAllLines (filename );
            Regex block = new Regex(@"\[\s*(?<name>[0-9a-z\{\}\.\-_]+)\s*\]", RegexOptions.IgnoreCase );
            Regex entry = new Regex(@"(?<name>[0-9a-z\.\-_]+)\s*=\s*(?<value>(.)+)$", RegexOptions.IgnoreCase );
            string s = string.Empty;
            string v_c = string.Empty ;
            for (int i = 0; i < t.Length; i++)
			{
                s = t[i];
                if (block.IsMatch(s))
                {
                    string n = block.Match(s).Groups["name"].Value;
                    if (!r.m_values.ContainsKey(n))
                    {
                        r.m_values.Add(n, new List<IniValue>());
                    }
                    v_c = n;
                }
                else if(entry.IsMatch (s))
                {
                    if (string.IsNullOrEmpty(v_c))
                    {
                        continue;
                    }
                    Match  p = entry.Match(s);
                    r.m_values[v_c].Add(new IniValue()
                    {
                        Name = p.Groups["name"].Value,
                        Value = p.Groups["value"].Value
                    });
                }
			}
            return r;
        }
        public class IniValue
        {
            public string Name{get;set;}
            public string Value{get;set;}
        }
    }
}

