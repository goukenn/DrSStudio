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

namespace IGK.ICore.DB
{
    /// <summary>
    /// represent a single column indentifier
    /// </summary>
    public struct CoreDataTableColumnIdentifier : ICoreDataTableColumnIdentifier
    {
        string m_name;
        object m_value;
        Dictionary<string, object> m_dic;

        public string Name { get { return this.m_name; } set { this.m_name = value; } }
        public object Value { get { return this.m_value; } set { this.m_value = value; } }

        public CoreDataTableColumnIdentifier(string name, object value)
        {
            this.m_name = name;
            this.m_value = value;
            this.m_dic = new Dictionary<string, object>();
            
        }

        public Dictionary<string, object> ToDictionary()
        {
            if (this.m_dic.Count == 0)
            {
                return new Dictionary<string, object>() { 
                {this.m_name, this.m_value }
                };
            }
            var b = new Dictionary<string, object>() { 
                {this.m_name, this.m_value }
                };
            foreach (var item in this.m_dic)
            {
                b.Add(item.Key, item.Value);
            }
            return b;
        }
        public ICoreDataTableColumnIdentifier Add(ICoreDataTableColumnIdentifier identifier)
        {
            if ((identifier != null) && (identifier.Equals (this) ==false ))
            {
                var c = identifier.ToDictionary();
                foreach (var item in c)
                {
                    if (this.m_dic.ContainsKey(item.Key) || (item.Key == Name ) )
                        continue;
                    this.m_dic.Add(item.Key, item.Value);
                }
            }
            return this;   
        }
    }
}
