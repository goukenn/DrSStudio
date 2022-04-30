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
    /// <summary>
    /// Represent a GS Configuration model
    /// </summary>
    class GSConfiguration
    {

        
        class GSConfigurationProperties
        {
            private string m_Value;
            private string path;

            internal GSConfigurationProperties()
            { 

            }
            public GSConfigurationProperties(string path, string value)
            {
                if (path == null)
                    throw new ArgumentNullException("path");
                this.path = path;
                this.m_Value = value;
            }
            public override string ToString()
            {
                return this.path + "," + m_Value;
            }

            public string Value
            {
                get { return m_Value; }
                set
                {
                    if (m_Value != value)
                    {
                        m_Value = value;
                    }
                }
            }
        }
        static Dictionary<string, GSConfigurationProperties> sm_configuration;

        static GSConfiguration() {
            sm_configuration = new Dictionary<string, GSConfigurationProperties>();
        }
        public static string GetValue(string path)
        {
            if (sm_configuration.ContainsKey(path))
            {
                return sm_configuration[path].Value;
            }
            return string.Empty;
        }

        public  static void SetVAlue(string path, string value)
        {
            if (sm_configuration.ContainsKey(path))
            {
                sm_configuration[path].Value = value;
            }
            else {
                if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(value))
                {
                    sm_configuration.Add(path, new GSConfigurationProperties(path, value));
                }
            }
        }
    }
}
