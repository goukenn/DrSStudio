

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSettingCollections.cs
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
file:CoreSettingCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace IGK.ICore.Settings
{
    using IGK.ICore;using IGK.ICore.WinUI;
    /// <summary>
    /// represent collection of setting in winCore
    /// </summary>
    public  class CoreSettingCollections : MarshalByRefObject, System.Collections.IEnumerable
    {
        //id of setting , the setting
        Dictionary<string, ICoreSetting> m_settings;
        ICoreSystem m_core;
        public ICoreSetting this[string key] {
            get {
                if (this.m_settings.ContainsKey(key))
                    return this.m_settings[key];
                return null;
            }
        }
        /// <summary>
        /// deternmine if key is a root keys
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key) {
            return this.m_settings.ContainsKey(key);
        }
        public CoreSettingCollections(ICoreSystem core)
        {
            this.m_settings = new Dictionary<string, ICoreSetting>();
            this.m_core = core;
            this.m_core.RegisterTypeLoader(LoadType);
        }
        internal void Add(string name, ICoreSetting setting)
        {
            if (!this.m_settings.ContainsKey(name) && (setting != null))
            {
                this.m_settings.Add(name, setting);
            }
        }
        /// <summary>
        /// load current setting according to configuration
        /// </summary>
        /// <param name="t"></param>
        void LoadType(Type t )
        {
            if (t.IsSubclassOf(typeof(CoreSettingBase)))
            {
                CoreAttributeSettingBase attr = CoreAttributeSettingBase.GetAttribute(t);
                if (attr != null)
                {
                    PropertyInfo pr = t.GetProperty(CoreConstant.INSTANCE_PROPERTY,
                        System.Reflection.BindingFlags.Static |
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                    if (pr != null)
                    {
                        ICoreSetting setting = pr.GetValue(null, null) as ICoreSetting;
                        if (m_settings.ContainsKey(setting.Id))
                        {
                            CoreSettingBase.DummySetting v_m = m_settings[setting.Id] as CoreSettingBase.DummySetting;
                            if (v_m == null)
                            {
                                CoreLog.WriteDebug(string.Format("setting {0} already register: Not a dummy", setting.Id));
                            }
                            else
                            {
                                //
                                //replace dummy setting 
                                //
                                setting.Load(v_m);                                
                                this.m_settings[setting.Id] = setting;
                                if (setting is CoreSettingBase)
                                    (setting as CoreSettingBase).OnSettingLoaded(EventArgs.Empty);
                            }
                        }
                        else
                        {
                            m_settings.Add(setting.Id, setting);
                        }
                    }
                }
            }
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_settings.GetEnumerator();
        }
        /// <summary>
        /// save setting
        /// </summary>
        internal void SaveSetting()
        {
            SettingManager.SaveSetting();
        }
        public string[] SortKeys()
        {
            var list = m_settings.Keys.ToList();
            list.Sort();
            return list.ToArray();
        }
    }
}

