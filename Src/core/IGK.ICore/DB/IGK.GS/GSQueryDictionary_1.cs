using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    public sealed class GSQueryDictionary
    {
        object  m_instance;
        Dictionary<string, object> m_t;
        public GSQueryDictionary(Type type)
        {
            if (type == null)
                throw new ArgumentException("type");
            this.m_instance = GSDB.CreateInterfaceInstance(type, null);
            m_t = new Dictionary<string, object>();
            foreach (GSDBColumnInfo i in GSDataContext.GetColumnInfo(type, GSSystem.Instance.DataAdapter))
            {
                if (!m_t.ContainsKey(i.clName))
                    m_t.Add(i.clName, null);
            }
        }
        
        public void Add(string name, object data)
        {
            if (m_t.ContainsKey(name))
                m_t[name] = data;

        }
        /// <summary>
        /// to dictionary. false;
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            return ToDictionary(false);
        }
        /// <summary>
        /// to dictionary
        /// </summary>
        /// <param name="ignoreNullValue"></param>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary(bool ignoreNullValue)
        {

            Dictionary<string, object> rt = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> data in m_t)
            {
                if ((data.Value == null) && (ignoreNullValue))
                    continue;
                rt.Add(data.Key, data.Value);
            }
            return rt;
        }

    }


}
