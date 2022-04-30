

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSettingBase.cs
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
file:CoreSettingBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Reflection;
namespace IGK.ICore.Settings
{
    using IGK.ICore;using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.WinUI;
    using IGK.ICore.ComponentModel;
    /// <summary>
    /// represent the core setting base class
    /// </summary>
    public abstract class CoreSettingBase : 
        MarshalByRefObject,
        ICoreSetting, 
        ICoreWorkingObject 
    {
        private string m_id;//id of this setting
        private int m_index;
        private string m_imageKey;//image key
        private int m_groupIndex;
        private Dictionary<string, ICoreSettingValue> m_settings;
        private CoreAttributeSettingBase m_attrib;

        /// <summary>
        /// event raised when setting loaded 
        /// </summary>
        public event EventHandler SettingLoaded;

        public event EventHandler<CoreSettingChangedEventArgs> SettingChanged;

        protected virtual void OnSettingChanged(CoreSettingChangedEventArgs e)
        {
            if (SettingChanged != null)
                this.SettingChanged(this, e);

        }
        /// <summary>
        /// raised when setting loaded on coresystem
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnSettingLoaded(EventArgs e) {
            if (this.SettingLoaded != null)
                this.SettingLoaded(this, e);
        }
        /// <summary>
        /// get the group name
        /// </summary>
        public string GroupName {
            get {
                return this.m_attrib.GroupName;
            }
        }
        /// <summary>
        /// get value or register default value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public object GetValue(string key, object defaultvalue)
        {
            if (m_settings.ContainsKey(key))
            {
                return m_settings[key].Value;
            }
            else if (defaultvalue !=null)
            {
                DummySetting dum = new DummySetting(key);
                dum.Value = defaultvalue;
                m_settings.Add(key, dum);
                return dum.Value;
            }
            return null;
        }
        public CoreSettingBase()
        {
            this.m_settings = new Dictionary<string, ICoreSettingValue>();
           CoreAttributeSettingBase attr= Attribute.GetCustomAttribute(this.GetType(), typeof(CoreAttributeSettingBase)) as
                CoreAttributeSettingBase;
           if (attr != null)
               this.SetAttribValue(attr);
           InitDefaultPropertiesValues();
        }
        public override System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
        {
            return base.CreateObjRef(requestedType);
        }
        public override object InitializeLifetimeService()
        {
            return null;
        }
        /// <summary>
        /// get if this property contains the environment 
        /// </summary>
        /// <param name="key">return true if contains otherwise return false</param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            if (this.m_settings.ContainsKey(key))
                return true;
            return false;
        }
        /// <summary>
        /// load default properties
        /// </summary>
        private void InitDefaultPropertiesValues()
        {
            CoreSettingDefaultValueAttribute v_attr = null;
            Type v_type = this.GetType();
            foreach (PropertyInfo v_pr in v_type.GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance))
            {
                v_attr = Attribute.GetCustomAttribute(
                    v_pr, typeof(CoreSettingDefaultValueAttribute)) as CoreSettingDefaultValueAttribute;
                if (v_attr != null)
                {
                    InitDefaultProperty(v_pr, v_attr);
                }
            }
        }


        protected virtual void LoadSettingProperty(ICoreParameterGroup g)
        {
            CoreSettingPropertyAttribute v_attr = null;
            foreach (PropertyInfo inf in GetType().GetProperties())
            {
                v_attr = Attribute.GetCustomAttribute(
                    inf, typeof(CoreSettingPropertyAttribute)) as CoreSettingPropertyAttribute;
                if (v_attr != null)
                {
                    g.AddItem(inf);
                }
            }
        }
        /// <summary>
        /// init default property value with attribute value
        /// </summary>
        /// <param name="prInfo"></param>
        /// <param name="attrib"></param>
        internal protected virtual void InitDefaultProperty(PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {         
            //init default property name
            this[prInfo.Name] = new CorePropertySetting(prInfo.Name, attrib.Type, attrib.Value);
        }
        /// <summary>
        /// set internal attribute
        /// </summary>
        /// <param name="attr"></param>
        internal protected void SetAttribValue(CoreAttributeSettingBase attr)
        {
            this.m_id = attr.Name;
            this.m_index = attr.Index;
            this.m_imageKey = attr.ImageKey;
            this.m_groupIndex = attr.GroupIndex;
            this.m_attrib = attr;
        }
         protected void Add(ICoreSettingValue setting)
        {
            if (setting == null) return;
            if (this.m_settings.ContainsKey(setting.Name))
            {
                this.m_settings[setting.Name ] = setting;
            }
            else
                this.m_settings.Add (setting .Name ,setting);
        }
        protected void Add(string name, object defaultValue, EventHandler e)
        {
            if (this.m_settings.ContainsKey(name))
            {
                ICoreSettingValue setting = this.m_settings[name];
                setting.Value = defaultValue;
                setting.ValueChanged += e;
            }
            else
            {
                DummySetting s = new DummySetting(name);
                s.Value = defaultValue;
                s.ValueChanged += e;
                this.m_settings[name] = s;
            }
        }
        #region ICoreSetting Members
        /// <summary>
        /// register settings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ICoreSettingValue this[string key]
        {
            get { 
                 if (m_settings.ContainsKey(key))
                 {
                     return m_settings[key];
                 }
                 else
                 {
                     DummySetting dum = new DummySetting(key);
                     m_settings.Add(key, dum);
                     return dum;
                 }
            }
            set {
                if (m_settings.ContainsKey(key))
                {
                    if (value == null)
                        m_settings.Remove(key);
                    else
                        m_settings[key] = value;
                }
                else {
                    if (value != null)
                        m_settings.Add(key, value);
                }
            }
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.m_id; }
            protected set { this.m_id = value; }
        }
        #endregion
        #region ICoreSetting Members
        /// <summary>
        /// get the number of child settings property attribute
        /// </summary>
        public int Count
        {
            get { return this.m_settings.Count; }
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_settings.GetEnumerator();
        }
        /// <summary>
        /// load setting
        /// </summary>
        /// <param name="setting"></param>
        void ICoreSetting.Load(ICoreSetting setting)
        {
            this.Load(setting);
        }
        /// <summary>
        /// load the dummy setting
        /// </summary>
        /// <param name="setting"></param>
        protected virtual void Load(ICoreSetting setting)
        {
            if (!(setting is DummySetting ))
                return ;
            CoreLog.WriteDebug($"Load Setting : {setting.Id}");
            System.ComponentModel.TypeConverter v_conv = null;
            System.ComponentModel.TypeConverterAttribute v_attr=null;
            foreach (KeyValuePair<string,ICoreSettingValue>  d in setting)
            {
                if (this.m_settings.ContainsKey(d.Value.Name) == false)
                {
                    if (!this.LoadDummyChildSetting(d))
                    this.m_settings.Add(d.Value.Name, d.Value);
                }
                else
                {
                    PropertyInfo prInfo = GetType().GetProperty(d.Value.Name);
                    if ((prInfo != null) && (prInfo.PropertyType != typeof(string)))
                    {
                        v_attr = System.ComponentModel.TypeConverterAttribute.GetCustomAttribute(prInfo,
                           typeof(System.ComponentModel.TypeConverterAttribute), false)
                           as System.ComponentModel.TypeConverterAttribute;
                        if (v_attr != null)
                        {
                            Type t = System.Type.GetType(v_attr.ConverterTypeName);
                            v_conv = t.Assembly.CreateInstance(t.FullName) as System.ComponentModel.TypeConverter;
                        }
                        else
                            v_conv =
                                CoreTypeDescriptor.GetConverter(prInfo.PropertyType);
                        if (v_conv.CanConvertFrom(typeof(string)))
                        {
                            this[d.Value.Name].Value = v_conv.ConvertFrom(d.Value.Value);
                        }
                        else
                        {
                            this[d.Value.Name].Value = d.Value.Value;
                        }
                    }
                    else
                    {
                        if (!this.LoadDummyChildSetting(d))
                        this[d.Value.Name].Value = d.Value.Value;
                    }
                }
            }
        }
        /// <summary>
        /// override this to load system dummy setting
        /// </summary>
        /// <param name="d">setting data</param>
        /// <returns>true if loaded otherwise false</returns>
        protected virtual bool LoadDummyChildSetting(KeyValuePair<string, ICoreSettingValue> d)
        {
            return false;
        }
        #endregion
        /// <summary>
        /// clear the property setting
        /// </summary>
        public virtual void Clear()
        {
            this.m_settings.Clear();
            this.InitDefaultPropertiesValues();
        }
        #region ICoreSetting Membres
        public bool CanConfigure
        {
            get { return (this.GetConfigType() != enuParamConfigType.NoConfig); }
        }
        public  virtual  enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.NoConfig;
        }
        public virtual ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {

            if (this.GetConfigType() == enuParamConfigType.ParameterConfig) {



            }

            return parameters;
        }
        public virtual ICoreControl GetConfigControl()
        {
            return null;
        }
        #endregion
        protected virtual void OnSettingChanged( CoreParameterChangedEventArgs e)
        {
            this[e.Item.Name].Value = e.Value;
        }
        /// <summary>
        /// Used for loading
        /// </summary>
        public sealed class DummySetting :
            MarshalByRefObject,
            ICoreSetting,
            ICoreSettingValue
        {
            public override System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
            {
                return base.CreateObjRef(requestedType);
            }
            public override object InitializeLifetimeService()
            {
                return null;
            }
            private Object m_Value;
            private Dictionary<string, ICoreSettingValue> m_setting;
            private string m_name;
            public DummySetting(string name)
            {
                this.m_setting = new Dictionary<string, ICoreSettingValue>();
                this.m_name = name;
            }
            /// <summary>
            /// add setting
            /// </summary>
            /// <param name="setting"></param>
            public void Add(ICoreSettingValue setting) { 
                if ((setting == null) || (m_name.Contains(setting.Name ))){
                    return ;
                }
                this.m_setting.Add (setting.Name , setting );
            }
            /// <summary>
            /// remove setting
            /// </summary>
            /// <param name="setting"></param>
            public void Remove(ICoreSettingValue setting) { 
                 if ((setting == null) || (!m_name.Contains(setting.Name ))){
                    return ;
                }
                this.m_setting.Remove (setting.Name );
            }
            public Object Value
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
            private void OnValueChanged(EventArgs eventArgs)
            {
                if (this.ValueChanged != null)
                    this.ValueChanged(this, eventArgs);
            }
            public ICoreSettingValue this[string name]
            {
                get
                {
                    if (m_setting.ContainsKey(name))
                        return m_setting[name];
                    return null;
                }
            }
            public string Name
            {
                get { return this.m_name; }
            }
            public bool HasChild
            {
                get { return this.m_setting.Count > 0; }
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_setting.GetEnumerator();
            }
            internal void Load(System.Xml.XmlReader xvreader)
            {
                while (xvreader.Read())
                {
                    switch (xvreader.NodeType)
                    {
                        case System.Xml.XmlNodeType.Text:
                            this.m_Value = xvreader.Value;
                            break;
                        case System.Xml.XmlNodeType.Element:
                            if (xvreader.Depth == 0)
                                continue;
                            DummySetting dum = new DummySetting(xvreader.Name);
                            this.m_setting.Add(dum.Name, dum);
                            if (xvreader.IsEmptyElement) continue;
                            dum.Load(xvreader.ReadSubtree());
                            break;
                    }
                }
            }
            public int Count
            {
                get { return this.m_setting.Count; }
            }
            public string Id
            {
                get { return this.m_name; }
            }
            public void Load(ICoreSetting setting)
            {
                CoreLog.WriteDebug("Not loading");
            }
            public bool CanConfigure
            {
                get { return false; }
            }
            public string Category { get { return null; } }
            public enuParamConfigType GetConfigType()
            {
                return enuParamConfigType.NoConfig;
            }
            public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
            {
                throw new NotImplementedException();
            }
            public ICoreControl GetConfigControl()
            {
                throw new NotImplementedException();
            }
            public event EventHandler ValueChanged;
            public int Index
            {
                get { return -1; }
            }
            public  string ImageKey
            {
                get { return null; }
            }
            public int GroupIndex {
                get { return -1; }
            }
            public string GroupName
            {
                get { return CoreConstant.DUMMY_GROUP;  }
            }
        }
        /// <summary>
        /// get the image key
        /// </summary>
        public virtual string ImageKey
        {
            get { return this.m_imageKey; }
        }
        /// <summary>
        /// require Index
        /// </summary>
        public int Index
        {
            get { return m_index; }
        }
        public int GroupIndex
        {
            get { return this.m_groupIndex; }
        }

        public static void Store()
        {
            var r = CoreSystem.GetSettings();
                if (r!=null)
                r.SaveSetting();   
        }
    }
}

