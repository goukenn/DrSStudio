

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreInitializationParam.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    /// <summary>
    /// reprenset a initialization params
    /// </summary>
    internal class CoreInitializationParam : ICoreInitializatorParam
    {
            Dictionary<string, string> m_params;
            public override string ToString()
            {
                return "InitializaTor: Count: " + m_params.Count + "";
            }
            internal CoreInitializationParam(string _params)
            {
                //initialize param
                string[] v_str = _params.Split(':', ';');
                m_params = new Dictionary<string, string>();
                string v_h = string.Empty;
                for (int i = 0; i < v_str.Length; i += 2)
                {
                    if ((i + 1) >= v_str.Length)
                        break;
                    v_h = v_str[i].Trim().ToLower();
                    if (!m_params.ContainsKey(v_h))
                    {
                        m_params.Add(v_h, v_str[i + 1].Trim());
                    }
                    else
                        m_params[v_h] = v_str[i].Trim();
                }
            }
            #region ICoreInitiazatorParam Members
            public string this[string key]
            {
                get
                {
                    if (this.m_params.ContainsKey(key.ToLower()))
                    {
                        return this.m_params[key.ToLower()];
                    }
                    return string.Empty;
                }
            }
            public int Count
            {
                get
                {
                    return this.m_params.Count;
                }
            }
            public bool Contains(string key)
            {
                if (string.IsNullOrEmpty(key)) return false;
                return this.m_params.ContainsKey(key.ToLower());
            }
            #endregion
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_params.GetEnumerator();
            }
        }
    
}
