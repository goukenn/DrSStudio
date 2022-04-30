

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePropertySetting.cs
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
file:CorePropertySetting.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore.Settings
{
    /// <summary>
    /// represent a base property setting class
    /// </summary>
    public class CorePropertySetting : 
        ICoreSettingValue
    {
        private string m_name;
        private object m_Value;
        private Type m_type;
        private CoreApplicationSettingCollections m_Childs;
		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public Type ValueType{
			get{return this.m_type ;}
		}
        public event EventHandler ValueChanged;
        public bool Contains(string key)
        {
            return this.m_Childs.Contains(key);
        }
        public void Clear()
        {
            this.m_Childs.Clear();
        }
        public CorePropertySetting(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(name)}");
            this.m_Childs = new CoreApplicationSettingCollections(this);
            this.m_name = name;
            this.m_Value = null;
        }
        public CorePropertySetting(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(name)}");
            this.m_Childs = new CoreApplicationSettingCollections(this);
            this.m_name = name;
            this.m_Value = value ?? throw new ArgumentNullException($"{nameof(value)}");
        }
        public CorePropertySetting(string name, Type t , object _default): this(name,_default)
        {
            this.m_name = name;
            this.m_type = t ?? throw new ArgumentException($"{nameof(t)}");
            if (_default != null)
            {
                if (_default.GetType() == t)
                {
                    this.m_Value = _default ;
                }
                else
                {
                    TypeConverter v_conv = CoreTypeDescriptor.GetConverter(t);
                    this.m_Value = v_conv.ConvertFrom(_default.ToString());
                }
            }
        }
        public object Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual void OnValueChanged(EventArgs eventArgs)
        {
            this.ValueChanged?.Invoke(this, eventArgs);
        }
        #region ICoreApplicationSetting Members
        public virtual ICoreSettingValue this[string name]
        {
            get {
                return this.m_Childs[name];
            }
        }
        public string Name
        {
            get { return this.m_name; }
        }
        #endregion
        public override string ToString() => string.Format($"{GetType().Name}: [{this.Name}]");
      
        /// <summary>
        /// add child property
        /// </summary>
        /// <param name="setting"></param>
        public void Add(ICoreSettingValue setting)
        {
            if (setting == null)
                return;
            this.m_Childs.Add(setting);
        }
        /// <summary>
        /// add child to event properties.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        public bool Add(string name, object value, EventHandler evt)
        {
            return this.m_Childs.Add(name, value, evt);
        }
        /// <summary>
        /// remove child property
        /// </summary>
        /// <param name="setting"></param>
        public void Remove(ICoreSettingValue setting) {
            if (setting == null)
                return ;
            this.m_Childs.Remove(setting.Name);
        }
        class CoreApplicationSettingCollections :
            System.Collections.IEnumerable
        {
            CorePropertySetting m_owner = null;
            Dictionary<string, ICoreSettingValue> m_settings;
			public CorePropertySetting Owner{
				get{
					return this.m_owner ;
				}
			}
            public ICoreSettingValue this[string name]{
                get{
                    if (this.m_settings .ContainsKey (name))
                        return this.m_settings[name];
                    return null;
                }
            }
            public int Count { get { return this.m_settings.Count; } }
            public void Add(ICoreSettingValue setting)
            {
                if (!this.m_settings.ContainsKey(setting.Name))
                {
                    this.m_settings.Add(setting.Name, setting);
                }
            }
            public void Remove(string name)
            {
                if (this.m_settings.ContainsKey(name))
                {
                    this.m_settings.Remove (name);
                }
            }
            public void Clear()
            {
                this.m_settings.Clear();
            }
            public CoreApplicationSettingCollections(CorePropertySetting owner)
            {
                this.m_owner = owner;
                this.m_settings = new Dictionary<string, ICoreSettingValue>();
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_settings.GetEnumerator ();
            }
            #endregion
            /// <summary>
            /// add and empty event
            /// </summary>
            /// <param name="name"></param>
            /// <param name="value"></param>
            /// <param name="evt"></param>
            /// <returns></returns>
            public bool Add(string name, object value, EventHandler evt)
            {
                if (string.IsNullOrEmpty (name) || this.m_settings.ContainsKey(name))
                    return false;
                CorePropertySetting setting = new CorePropertySetting(name);
                setting.Value = value;
                setting.ValueChanged = evt;
                this.Add(setting);
                return true;
            }
            internal bool Contains(string key)
            {
                if (string.IsNullOrEmpty(key))
                    return false;
                return m_settings.ContainsKey(key);
            }
        }
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_Childs.GetEnumerator();
        }
        #endregion
        #region ICoreApplicationSetting Members
        public bool HasChild
        {
            get { return (this.m_Childs.Count > 0); }
        }
        #endregion
    }
}

