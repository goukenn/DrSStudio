

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterGroup.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Actions;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreParameterGroup.cs
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
using System.Reflection;
using System.Text;
namespace IGK.ICore.WinCore.Configuration
{
    /// <summary>
    /// represent the base implementation of the group item
    /// </summary>
    public class CoreParameterGroup :
        CoreParameterItemBase ,
        ICoreParameterGroup 
    {
        private object m_target;        
        Dictionary<string, ICoreParameterEntry> m_items;
        private ICoreParameterGroup m_ParentGroup;


        public ICoreParameterEntry this[string id] {
            get {
                if (this.m_items.ContainsKey (id))
                    return this.m_items[id];
                return null ;
            }
        }
        /// <summary>
        /// get the parent groups
        /// </summary>
        public ICoreParameterGroup ParentGroup
        {
            get { return m_ParentGroup; }
            internal set { m_ParentGroup = value; }
        }
        public virtual bool IsRootGroup { get{return false ;} }
        public override string ToString()
        {
            return string.Format("ParameterGroup[#{0}]", this.Name);
        }
        /// <summary>
        /// register action type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="captionkey"></param>
        /// <param name="action"></param>
        public void AddActions(string name, string captionkey, ICoreAction action)
        {
            if (!this.m_items.ContainsKey(name))
            {
                CoreParameterActionBase v_action = new CoreParameterActionBase(name, captionkey, action);
                this.AddItem(v_action);
            }
        }
        /// <summary>
        /// add parameter actions
        /// </summary>
        /// <param name="action"></param>
        public void AddActions(IGK.ICore.WinUI.Configuration.ICoreParameterAction action)
        {
            if ((action == null) || string.IsNullOrEmpty(action.Name) || this.m_items.ContainsKey(action.Name ))
                return;
            this.AddItem(action as ICoreParameterEntry );
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="captionKey"></param>
        public CoreParameterGroup(object target, string name ,string captionKey):base(name, captionKey )
        {
            if (target == null)
                throw new ArgumentException($"{nameof(target)}");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();
            this.m_items = new Dictionary<string, ICoreParameterEntry>();
            this.m_target = target;
        }
        #region ICoreParameterGroup Members
        public ICoreParameterItem AddItem(string itemName, string captionKey, CoreParameterChangedEventHandler eventHandler)
        {
            return AddItem(itemName, captionKey, enuParameterType.Text, eventHandler);
        }
        #endregion
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_items.Values.GetEnumerator();
        }
        #endregion
        #region ICoreParameterGroup Members
        public ICoreParameterItem ReplaceItem(string itemName, string captionKey, enuParameterType type, CoreParameterChangedEventHandler eventHandler)
        {
            if (string.IsNullOrEmpty(itemName))
                return null;

            CoreParameterGroupItem item = new CoreParameterGroupItem(
               itemName,
               captionKey,
               this, type, eventHandler);
            if (!this.m_items.ContainsKey(itemName))
            {
           
                this.AddItem(item);
                
            }
            else {
                item.Parent = this;
                this.m_items[item.Name] =  item;
            }
            return item;
        }

        public ICoreParameterItem AddItem(string itemName, string captionKey, enuParameterType type, CoreParameterChangedEventHandler eventHandler)
        {
            if (!this.m_items.ContainsKey(itemName))
            {
                CoreParameterGroupItem item = new CoreParameterGroupItem(
                    itemName,
                    captionKey,
                    this, type, eventHandler);
                this.AddItem(item);
                return item;
            }
            return null;
        }
        public ICoreParameterItem AddItem(string itemName, 
            string captionKey, 
            object defaultValue, 
            enuParameterType type, 
            CoreParameterChangedEventHandler eventHandler)
        {
            if (!this.m_items.ContainsKey(itemName))
            {
                CoreParameterGroupItem item =
                    new CoreParameterGroupItem(
                    itemName,
                    captionKey,
                    this,
                    defaultValue,
                    type,
                    eventHandler);
                this.AddItem(item);
                return item;
            }
            return null;
        }
        /// <summary>
        /// add Parameter Entry
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ICoreParameterEntry item)
        {
            if (item == null) return;
            if (!string.IsNullOrEmpty(item.Name) && !this.m_items.ContainsKey(item.Name))
            {
                item.Parent = this;
                this.m_items.Add(item.Name, item);
            }
        }
        public void AddItem(ICoreParameterItem item)
        {
            this.AddItem(item as ICoreParameterEntry);
        }
        public ICoreParameterItem AddItem(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                return null;
            return AddItem(propertyInfo, 
                string.Format (
                CoreConstant.LABEL_TEXT_FORMAT
                ,
                propertyInfo.Name)
                );
        }
        public ICoreParameterItem AddItem(PropertyInfo propertyInfo, enuParameterType requestedType)
        {
            if (propertyInfo == null)
                return null;
            return this.AddItem(propertyInfo.Name, propertyInfo.Name, requestedType, null);
        }
        public ICoreParameterItem AddItem(PropertyInfo propertyInfo, string captionKey)
        {
            if (propertyInfo == null)
                return null;
            if (!this.m_items.ContainsKey(propertyInfo.Name))
            {
                if (propertyInfo.PropertyType.IsEnum)
                {
                    ICoreParameterItem i = new CoreParameterGroupEnumItem(
                        m_target,
                    propertyInfo,
                    captionKey ,
                    this);
                    this.AddItem(i);
                    return i;
                }
                else
                {
                    if (propertyInfo.PropertyType == typeof(bool))
                    {
                        return this.AddItem(propertyInfo.Name, captionKey,
                            propertyInfo.GetValue(this.m_target, null),
                            enuParameterType.Bool, null);
                    }
                    else
                    {
                        return this.AddItem(propertyInfo.Name, captionKey, enuParameterType.Text, null);
                    }
                }
            }
            return null;
        }
      
        public void AddParameters(string name, ICoreParameterConfigCollections d)
        {
            foreach (ICoreParameterItem  item in d)
            {
                this.AddItem(item);
            }            
        }
      
        public void AddEnumItem(string itemName, string captionKey, Type enumType, object defaultValue, CoreParameterChangedEventHandler paramChanged)
        {
            if (this.m_items.ContainsKey(itemName))
                return;
            ICoreParameterItem i = new CoreParameterGroupEnumItem
                (
                this.m_target,
                itemName ,
                captionKey,
                enumType ,
                this,
                defaultValue  ,
                paramChanged );
            this.AddItem(i);            
        }
        public void AddEnumItem(string itemName, string captionKey, object datasource, CoreParameterChangedEventHandler paramChanged)
        {
            if (this.m_items.ContainsKey(itemName))
                return;
            ICoreParameterItem i = new CoreParameterGroupDataItem
                (
                this.m_target,
                itemName,
                captionKey,
                datasource,
                this,
                null,
                paramChanged);
            this.AddItem(i);
        }
        public override  void RestoreDefault()
        {
            foreach (KeyValuePair<string, ICoreParameterEntry > item in this.m_items )
            {
                if (item.Value is ICoreParameterEntry )
                    (item.Value as ICoreParameterItem ).RestoreDefault();
            }
        }
        public ICoreParameterItem AddTrackbar(string itemName, string captionkey, int min,
            int max,
            int defaulValue, CoreParameterChangedEventHandler PROC)
        {
            if (!string.IsNullOrEmpty(itemName) && !this.m_items.ContainsKey(itemName))
            {
                CoreParameterTrackbar bar = new CoreParameterTrackbar(itemName, captionkey, this, defaulValue, PROC);
                bar.min = min;
                bar.max = max;
                this.AddItem(bar);
                return bar;
            }
            return null;
        }
     
        public ICoreParameterItem AddTrackbar(
            PropertyInfo propertyInfo, 
            int beging, 
            int end,
            int defaulValue, 
            CoreParameterChangedEventHandler PROC)
        {
            if (propertyInfo == null)
                return null;
            if (!this.m_items.ContainsKey(propertyInfo .Name ))
            {
                if (PROC == null)
                {
                    PROC = delegate(object sender, CoreParameterChangedEventArgs e)
                    {
                        propertyInfo.SetValue(this.m_target, e.Value, null);
                    };
                }
                CoreParameterTrackbar bar = new CoreParameterTrackbar(
                    propertyInfo.Name , string.Format ("lb.{0}.caption",
                    propertyInfo.Name ),
                    this,
                    defaulValue, 
                    PROC);
                bar.min = beging ;
                bar.max = end ;
                this.AddItem(bar);
                return bar;
            }
            return null;
        }
        #endregion
        #region ICoreParameterGroup Members
        public void AddConfigObject(ICoreWorkingConfigurableObject obj)
        {
            if (!this.m_items.ContainsKey(obj.Id))
            {
                this.m_items.Add(obj.Id, new CoreConfigurableObjectParameterItem(obj));                
            }
        }
        #endregion
        #region ICoreParameterGroup Members
        public int Count
        {
            get { return this.m_items.Count; }
        }
        #endregion
        public ICoreParameterItem AddItem(string name, string captionkey)
        {
            return this.AddItem(name, captionkey, enuParameterType.Text ,  null);
        }
        public ICoreParameterItem AddItem(string name, string captionkey, ICoreControl control)
        {
            if (!this.m_items.ContainsKey (name ))
            {
                CoreParameterControl c = new CoreParameterControl(name, captionkey, control);
                this.m_items.Add(name, c);
                return c;
            }
            return null;
        }
        //public void AddItem(string name,         captionkey,          ICoreControl control)
        //{
        //    if (!this.m_items.ContainsKey(name))
        //    {
        //        CoreParameterControl ctr = new CoreParameterControl(name, captionkey, control);
        //        this.AddItem(ctr);
        //    }
        //}


        public ICoreParameterItem AddLabel(string name)
        {
            return this.AddItem(name, name, enuParameterType.Label, null);
        }
    }
}

