

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CodeBarENA13Manager.cs
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
file:ENA13_Manager.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    static class CodeBarENA13Manager
    {
        static Dictionary<int, string> m_segments;
        static Dictionary<int, string> m_ElementsA;
        static Dictionary<int, string> m_ElementsB;
        static Dictionary<int, string> m_ElementsC;
         static CodeBarENA13Manager()
        {
            m_segments = new Dictionary<int, string>();
            m_ElementsA = new Dictionary<int, string>();
            m_ElementsB = new Dictionary<int, string>();
            m_ElementsC = new Dictionary<int, string>();
            m_segments.Add(0, "AAAAAA");
            m_segments.Add(1, "AABABB");
            m_segments.Add(2, "AABBAB");
            m_segments.Add(3, "AABBBA");
            m_segments.Add(4, "ABAABB");
            m_segments.Add(5, "ABBAAB");
            m_segments.Add(6, "ABBBAA");
            m_segments.Add(7, "ABABAB");
            m_segments.Add(8, "ABABBA");
            m_segments.Add(9, "ABBABA");
            m_ElementsA.Add(0, "0001101");
            m_ElementsA.Add(1, "0011001");
            m_ElementsA.Add(2, "0010011");
            m_ElementsA.Add(3, "0111101");
            m_ElementsA.Add(4, "0100011");
            m_ElementsA.Add(5, "0110001");
            m_ElementsA.Add(6, "0101111");
            m_ElementsA.Add(7, "0111011");
            m_ElementsA.Add(8, "0110111");
            m_ElementsA.Add(9, "0001011");
            m_ElementsB.Add(0, "0100111");
            m_ElementsB.Add(1, "0110011");
            m_ElementsB.Add(2, "0011011");
            m_ElementsB.Add(3, "0100001");
            m_ElementsB.Add(4, "0011101");
            m_ElementsB.Add(5, "0111001");
            m_ElementsB.Add(6, "0000101");
            m_ElementsB.Add(7, "0010001");
            m_ElementsB.Add(8, "0001001");
            m_ElementsB.Add(9, "0010111");
            string v_f = string.Empty ;
            foreach (KeyValuePair<int, string> item in m_ElementsA)
            {
                v_f = item.Value ;
                v_f = v_f.Replace ("0","_");
                v_f = v_f.Replace ("1","0");
                v_f = v_f.Replace ("_","1");
                m_ElementsC.Add(item.Key ,v_f  );
            }
        }
        public static string GetSequence(int i)
        {
            if (m_segments .ContainsKey (i))
                return m_segments[i];
            return null;
        }
        /// <summary>
        /// get the element type
        /// </summary>
        /// <param name="type">Type must be A, B or C</param>
        /// <param name="index">index is de décimal value from 0 to 9</param>
        /// <returns></returns>
        public static string GetElement(string type, int index)
        {
            switch (type)
            {
                case "A":
                    return m_ElementsA[index];                    
                case "B":
                    return m_ElementsB[index];
                case "C":
                    return m_ElementsC[index];
            }
            return null;
        }
        /// <summary>
        /// calculate the check value a return the new value with the check
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetCheckCode(string value)
        {
            if (string.IsNullOrEmpty(value) ||(value.Length != 12))
                return null;
            int p = 0;
            int imp = 0;
            for (int i = 0; i < 12; i++)
            {
                if ((i % 2) == 0)
                {
                    p += Int32.Parse(value[i].ToString());
                }
                else {
                    imp += Int32.Parse(value[i].ToString ());
                }
            }
            int t = 10 - ((3 * imp) + p) % 10;
            t = (t == 10) ? 0 : t;
            return string.Format("{0}{1}", value, t);
        }
    }
}

