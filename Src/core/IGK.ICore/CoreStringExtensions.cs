

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreStringExtensions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// present string pattern utility
    /// </summary>
    public static class CoreStringExtensions
    {
        public static string ToRegex(this string entry)
        { 
            return entry.Replace("+","\\+").Replace(".", "\\.").Replace("*", "(.)+").Replace("[","\\[");
        }
        public static string LastSegment(this string lastSegment, StringSplitOptions options, params char[] separator)
        {
            var t = lastSegment.Split(separator, options);
            if (t.Length > 0)
                return t[t.Length - 1];
            return null;
        }
        public static string LastSegment(this string lastSegment, params char[] separator)
        {
            return LastSegment (lastSegment, StringSplitOptions.RemoveEmptyEntries, separator );
        }
        /// <summary>
        /// get string table in branket the fist is the whole 1 match result
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string[] ReadInBrancked(this string s, char start, char end)
        {
            List<string> g = new List<string>();
            int i = s.IndexOf(start);
            if (i != -1)
            {
                //get depth outer string
                //next 
                int r = s.IndexOf(start, i + 1);
                int rg = s.IndexOf(end, i + 1);

                if (r == -1)
                { //no more breaked found
                    var ln = rg - i - 1;
                    if (ln > 0)
                        g.Add(s.Substring(i + 1, ln));
                }
                else
                {
                    //get end matching brancket
                    int depth = 1;
                    int kindex = r;
                    bool mark = false;
                    List<Vector2i> offsets = new List<Vector2i>();
                    int bsegment = 0;
                    for (int xs = i + 1; xs < s.Length; xs++)
                    {
                        if (s[xs] == start)
                        {
                            depth++;
                            offsets.Add(new Vector2i(xs, 0));
                            bsegment = offsets.Count;
                            continue;
                        }
                        if (s[xs] == end)
                        {

                            depth--;
                            if (depth == 0)
                            {
                                kindex = xs;
                                mark = true;
                                break;
                            }
                            bsegment--;
                            var tg = offsets[bsegment];
                            tg.Y = xs;
                            offsets[bsegment] = tg;
                        }
                    }
                    if (mark)
                    {
                        g.Add(s.Substring(i + 1, kindex - i - 1));
                        foreach (var item in offsets)
                        {
                            var tt = ReadInBrancked(s.Substring(item.X, item.Y - item.X + 1), start, end);
                            //add only new segment
                            foreach (var item2 in tt)
                            {
                                if (g.Contains(item2))
                                    continue;
                                g.Add(item2);
                            }

                        }

                    }
                }
            }
            return g.ToArray();
        }

        /// <summary>
        /// get the next match pattern number
        /// </summary>
        /// <param name="input"></param>
        /// <param name="valuePattern">%d% pattern</param>
        /// <returns></returns>
        public static int GetNextPatternNumber(string input, string valuePattern) {
            if (string.IsNullOrEmpty (input) && string.IsNullOrEmpty (valuePattern ))
                return -1;
            valuePattern = valuePattern.Replace("%d%", "(?<value>\\d+)");
            string g = input;
            int i = 1;
            foreach (Match m in Regex.Matches(g, valuePattern, RegexOptions.IgnoreCase))
            {
                int ii = Int32.Parse(m.Groups["value"].Value);
                if (i < ii)
                {
                    i = Math.Max(i, ii);
                    i++;
                }
            }
            return i;
        }

        public static string Last(this string[] t) {
            if ((t!=null) &&( t.Length > 0))
                return t[t.Length-1];
            return string.Empty ;
        }
    }
}
