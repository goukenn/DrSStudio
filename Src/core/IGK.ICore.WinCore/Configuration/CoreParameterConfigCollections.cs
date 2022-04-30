

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterConfigCollections.cs
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
file:CoreParameterConfigCollections.cs
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
using System.Text;
namespace IGK.ICore.WinCore
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.WinCore.Configuration;
    /// <summary>
    /// represent the base class of the parameter config collection property
    /// </summary>
    public class CoreParameterConfigCollections :
        CoreParameterEntryBase,
        ICoreParameterConfigCollections
    {
        internal CoreParameterConfigCollections()
            : base()
        {
        }
        //default group
        internal const string DEFAULT = "DEFAULT";
        CoreParameterTabCollections m_tabCollections;
        public ICoreParamterTabCollections Tabs { get { return this.m_tabCollections; } }
        private ICoreParameterGroupCollections m_groups;
        //private Dictionary<string, ICoreParameterGroup> m_groups;
        private object m_target; //the target
        private bool m_CanCancel;
        private ICoreParameterGroup m_ParentGroup;
        /// <summary>
        /// get parent group
        /// </summary>
        public ICoreParameterGroup ParentGroup
        {
            get { return m_ParentGroup; }
        }
        /// <summary>
        /// get or set if this property collection can be cancelled
        /// </summary>
        public bool CanCancel
        {
            get { return m_CanCancel; }
            set
            {
                m_CanCancel = value;
            }
        }
        public event CoreParameterChangedEventHandler PropertyChanged;
        protected virtual void OnPropetyChanged(CoreParameterChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
        public void Reload()
        {
        }
        public CoreParameterConfigCollections(object target)
            : this(target, null)
        {
        }
        /// <summary>
        /// .ctr parent group
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parentGroup"></param>
        public CoreParameterConfigCollections(object target, ICoreParameterGroup parentGroup)
        {
            this.m_groups = new CoreParameterGroupCollections(this);
            this.m_tabCollections = new CoreParameterTabCollections(this);
            this.m_target = target;
            this.m_ParentGroup = parentGroup;
        }
        public ICoreParameterGroup this[string name]
        {
            get
            {
                if (this.m_groups.ContainsKey(name))
                {
                    return this.m_groups[name];
                }
                return null;
            }
        }
        public ICoreParameterGroup GetGroupById(string name)
        {
            if (this.m_groups.ContainsKey(name))
            {
                return this.m_groups[name];
            }
            return null;
        }
        public ICoreParameterGroup AddGroup(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return m_groups[DEFAULT];
            }
            if (!this.m_groups.ContainsKey(groupName))
            {
                CoreParameterGroup group = new CoreParameterGroup(this.m_target, groupName, string.Format (
                    CoreConstant.GROUP_CAPTION_FORMAT
                    ,
                    groupName).ToLower ());
                group.ParentGroup = this.ParentGroup;
                m_groups.Add(group);
            }
            return m_groups[groupName];
        }
        public ICoreParameterGroup AddGroup(string groupName, string captionKey)
        {
            if (!this.m_groups.ContainsKey(groupName))
            {
                CoreParameterGroup group = new CoreParameterGroup(this.m_target, groupName, captionKey);
                m_groups.Add(group);
            }
            return m_groups[groupName];
        }
        public ICoreParameterItem AddItem(string itemName, string captionKey,
            enuParameterType type,
            object defaultValue,
            CoreParameterChangedEventHandler eventHandler)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            return group.AddItem(itemName, captionKey, defaultValue, type, eventHandler);
        }
        public ICoreParameterItem AddItem(
            string itemName,
            string captionKey,
            CoreParameterChangedEventHandler eventHandler)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            return group.AddItem(itemName, captionKey, eventHandler);
        }
        public ICoreParameterItem AddItem(
            string itemName,
            string captionKey,
            string groupName,
            CoreParameterChangedEventHandler eventHandler)
        {
            ICoreParameterGroup group = this.AddGroup(groupName);
            return group.AddItem(itemName, captionKey, eventHandler);
        }
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_groups.GetEnumerator();
        }
        #endregion
        public ICoreParameterItem AddItem(string itemName, string captionKey, enuParameterType type, CoreParameterChangedEventHandler eventHandler)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            return group.AddItem(itemName, captionKey, type, eventHandler);
        }
        public ICoreParameterItem AddItem(string itemName, string captionKey, string groupName, enuParameterType type, CoreParameterChangedEventHandler eventHandler)
        {
            ICoreParameterGroup group = this.AddGroup(groupName);
            return group.AddItem(itemName, captionKey, type, eventHandler);
        }
        public void Clear()
        {
            this.m_groups.Clear();
        }
        public ICoreParameterItem AddItem(System.Reflection.PropertyInfo property)
        {
            if ((property == null) || !(property.CanRead && property.CanWrite))
                return null;
            ICoreParameterGroup group = AddGroup(DEFAULT);
            if (property.PropertyType.IsEnum)
            {
                return AddEnumItem(property, property.Name, group.Name);
            }
            return group.AddItem(property);
        }
        public ICoreParameterItem AddItem(System.Reflection.PropertyInfo propertyInfo, string group)
        {
            ICoreParameterGroup v_group = this.AddGroup(group);
            return v_group.AddItem(propertyInfo);
        }
        public ICoreParameterItem AddItem(System.Reflection.PropertyInfo propertyInfo, string captionKey, string group)
        {
            if (!string.IsNullOrEmpty(group))
            {
                ICoreParameterGroup v_group = this.AddGroup(group);
                return v_group.AddItem(propertyInfo, captionKey);
            }
            else
            {
                return null;
            }
        }
        public ICoreParameterItem AddEnumItem(System.Reflection.PropertyInfo propertyInfo, string captionKey, string group)
        {
            if (!propertyInfo.PropertyType.IsEnum)
                return null;
            ICoreParameterGroup v_group = AddGroup(group);
            ICoreParameterItem i = new CoreParameterGroupEnumItem(m_target, propertyInfo, captionKey, v_group);
            v_group.AddItem(i);
            return i;
        }
        #region ICoreParameterConfigCollections Members
        public ICoreParameterConfigCollections CreateEmptyConfig(object target)
        {
            if (target == null)
            {
                return null;
            }
            return new CoreParameterConfigCollections(target);
        }
        #endregion
        #region ICoreParameterConfigCollections Members
        public void RemoveGroup(string p)
        {
            if (this.m_groups.ContainsKey(p))
            {
                this.m_groups.Remove(p);
            }
        }
        #endregion
        #region ICoreParameterConfigCollections Members
        public void RestoreDefault()
        {
        }
        #endregion
        #region ICoreParameterConfigCollections Members
        public virtual ICoreParameterStatus CreateStatusItem(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return null;
            return new CoreParameterStatus(Name, string.Format(CoreConstant.LB_CAPTION, Name), m_target);
        }
        #endregion
        #region ICoreParameterConfigCollections Members
        public ICoreParameterTab AddTab(string tabName, string tabCaptionKey)
        {
            CoreParameterTab tab = new CoreParameterTab(this.m_target,
                tabName, tabCaptionKey);
            if (this.m_groups.Count > 0)
            {
                //create a default groups and add present tagto is
                CoreParameterTab v = new CoreParameterTab(this.m_target, "Default", "Default");
                foreach (ICoreParameterGroup item in this.m_groups)
                {
                    v.AddGroup(item);
                }
                this.m_tabCollections.Add(v);
                this.m_groups = v.Groups;
            }
            this.m_tabCollections.Add(tab);
            return tab;
        }
        #endregion
        #region ICoreParameterConfigCollections Members
        public void BuildParameterInfo(ICoreWorkingConfigurableObject @object)
        {
            if (@object == null)
                return;
            Type t = @object.GetType();
            Type v_ta = typeof(CoreConfigurablePropertyAttribute);
            CoreConfigurablePropertyAttribute v_attrib;
            PropropertyBuilder v_b;
            List<PropropertyBuilder> v_list = new List<PropropertyBuilder>();
            foreach (System.Reflection.PropertyInfo v_pr in t.GetProperties())
            {
                if (v_pr.CanRead && v_pr.CanWrite)
                {
                    v_attrib = Attribute.GetCustomAttribute(v_pr, v_ta) as CoreConfigurablePropertyAttribute;
                    if (v_attrib != null)
                    {
                        v_b = new PropropertyBuilder();
                        v_b.attrib = v_attrib;
                        v_b.v_prInfo = v_pr;
                        v_b.v_target = @object;
                        v_list.Add(v_b);
                    }
                }
            }
            v_list.Sort(new PropropertyBuilder());
            foreach (PropropertyBuilder item in v_list)
            {
                if (!string.IsNullOrEmpty(item.attrib.AttribName))
                {
                    this.AddItem(item.v_prInfo, item.attrib.CaptionKey, item.attrib.Group);
                }
                else
                {
                    this.AddItem(item.v_prInfo, string.Format("lb.{0}.caption", item.v_prInfo.Name),
                        item.attrib.Group);
                }
            }
        }
        #endregion
        public ICoreParameterItem AddTrackbar(string itemName, string captionkey, int begin, int end, int defaulValue, CoreParameterChangedEventHandler PROC)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            return group.AddTrackbar(itemName, captionkey, begin, end, defaulValue, PROC);
        }
        public ICoreParameterItem AddTrackbar(System.Reflection.PropertyInfo propertyInfo, int begin, int end, int defaulValue, CoreParameterChangedEventHandler PROC)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            return group.AddTrackbar(propertyInfo, begin, end, defaulValue, PROC);
        }
        public ICoreParameterItem AddItem(string itemName, string captionKey, object defaultValue, enuParameterType type, CoreParameterChangedEventHandler eventHandler)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            return group.AddItem(itemName, captionKey, defaultValue, type, eventHandler);
        }
        public void AddItem(ICoreParameterItem item)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            group.AddItem(item);
        }
        public ICoreParameterItem AddItem(string name, string captionkey)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            return group.AddItem(name, captionkey);
        }
        public ICoreParameterItem AddItem(string name, string captionkey, ICoreControl control)
        {
            ICoreParameterGroup group = this.AddGroup(DEFAULT);
            return group.AddItem(name, captionkey, control);
        }
        /// <summary>
        /// reprensent the tab
        /// </summary>
        public class CoreParameterTabCollections : ICoreParamterTabCollections, System.Collections.IEnumerable
        {
            List<ICoreParameterTab> m_tabs;
            CoreParameterConfigCollections m_owner;
            public CoreParameterTabCollections(CoreParameterConfigCollections owner)
            {
                this.m_owner = owner;
                this.m_tabs = new List<ICoreParameterTab>();
            }
            public int Count { get { return this.m_tabs.Count; } }
            public void Add(ICoreParameterTab Tab)
            {
                if ((Tab != null) && (!this.m_tabs.Contains(Tab)))
                    this.m_tabs.Add(Tab);
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_tabs.GetEnumerator();
            }
            #endregion
        }
        public class CoreParameterGroupCollections : ICoreParameterGroupCollections
        {
            Dictionary<string, ICoreParameterGroup> m_groups;
            CoreParameterConfigCollections m_owner;
            public CoreParameterGroupCollections(CoreParameterConfigCollections owner)
            {
                this.m_groups = new Dictionary<string, ICoreParameterGroup>();
                this.m_owner = owner;
            }
            #region ICoreParameterGroupCollections Members
            public int Count
            {
                get { return this.m_groups.Count; }
            }
            public ICoreParameterGroup this[string keys]
            {
                get
                {
                    return m_groups[keys];
                }
            }
            public ICoreDialogToolRenderer Host
            {
                get
                {
                    return this.m_owner.Host;
                }
            }
            public void Add(ICoreParameterGroup group)
            {
                group.Parent = this.m_owner;
                this.m_groups.Add(group.Name, group);
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_groups.Values.GetEnumerator();
            }
            #endregion
            #region ICoreParameterGroupCollections Members
            public bool ContainsKey(string name)
            {
                return this.m_groups.ContainsKey(name);
            }
            #endregion
            #region ICoreParameterGroupCollections Members
            public void Clear()
            {
                this.m_groups.Clear();
            }
            #endregion
            #region ICoreParameterGroupCollections Members
            public void Remove(string name)
            {
                if (ContainsKey(name))
                {
                    m_groups[name].Parent = null;
                    m_groups.Remove(name);
                }
            }
            #endregion
        }
        struct PropropertyBuilder : IComparer<PropropertyBuilder>
        {
            internal CoreConfigurablePropertyAttribute attrib;
            internal System.Reflection.PropertyInfo v_prInfo;
            internal ICoreWorkingConfigurableObject v_target;
            #region IComparer<PropropertyBuilder> Members
            //internal void PropChanged(object sender, CoreParameterChangedEventArgs  e)
            //{
            //    System.ComponentModel.TypeConverter conv   = CoreTypeDescriptor.GetConverter(v_prInfo.PropertyType);
            //    object o = conv.ConvertFrom(e.Value);
            //    v_prInfo.SetValue (v_target , o,null);
            //}
            public int Compare(PropropertyBuilder x, PropropertyBuilder y)
            {
                return x.attrib.Index.CompareTo(x.attrib.Index);
            }
            #endregion
        }
        ICoreDialogToolRenderer m_host;
        public override ICoreDialogToolRenderer Host
        {
            get
            {
                return m_host;
            }
        }
        public void setHost(ICoreDialogToolRenderer Host)
        {
           this.m_host = Host;            
        }
        ICoreDialogToolRenderer ICoreParameterConfigCollections.Host
        {
            get
            {
                return this.Host;
            }
            set
            {
                this.setHost(value );
            }
        }


        public ICoreParameterItem GetItem(string id)
        {
            ICoreParameterItem s = null;
            foreach (ICoreParameterGroup h in this.m_groups)
            {
                s = h[id] as ICoreParameterItem;
                if (s != null)
                    return s;
            }
             return null;
        }


        public Size2i PreferedSize
        {
            get;
            set;
        }
    }
}

