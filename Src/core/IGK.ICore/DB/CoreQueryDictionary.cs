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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB{

    /// <summary>
    /// represent a safe query dictionary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CoreQueryDictionary<T>
    {
        private T m_instance;
        private Dictionary<string, object> t;

        public CoreQueryDictionary()
        {
            this.m_instance = CoreDBManager.CreateInterfaceInstance<T>();
            t = new Dictionary<string, object>();
            if (/*(GSSystem.Instance != null) && */(CoreDBManager.Adapter != null))
            {
                foreach (ICoreDataColumnInfo i in CoreDataContext.GetColumnInfo(typeof(T), CoreDBManager.Adapter))
                {
                    if (!t.ContainsKey(i.clName))
                        t.Add(i.clName, null);
                }
            }
#if DEBUG
            else {
                CoreLog.WriteDebug("No global data adapter found . to initialize. GSQueryDictionary");

            }
#endif
        }

        public CoreQueryDictionary(Dictionary<string , object> dic):this()
        {
            foreach (KeyValuePair<string, object> item in dic)
            {
#if DEBUG                 
                Debug.Assert(t.ContainsKey(item.Key),"Column " + item.Key +" not valid for datatable : " + typeof(T));                
#endif
                if (t.ContainsKey(item.Key))
                {
                    this.Add(item.Key, item.Value);
                }
            }
        }
        public void Add(string name, object data)
        {
            if (t.ContainsKey(name))
                t[name] = data;
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
        /// <param name="ignoreNullValue">true to ignore empty value</param>
        /// <returns>ignore value</returns>
        public Dictionary<string, object> ToDictionary(bool ignoreNullValue)
        {

            Dictionary<string, object> rt = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> data in t)
            {
                if ((data.Value == null) && (ignoreNullValue))
                    continue;
                rt.Add(data.Key, data.Value);
            }
            return rt;
        }

    }
      
    
}
