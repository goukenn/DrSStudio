

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterItemBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreParameterItemBase.cs
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
namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent the base parameter items class 
    /// </summary>
    public abstract class CoreParameterItemBase : 
        ICoreParameterItem ,
        ICoreParameterGroupItem
    {
        private string m_name;
        private string m_captionKey;
        private ICoreParameterGroup m_Group;
        private enuParameterType m_ParamType;
        private object m_DefaultValue;
        private object m_value;
        /// <summary>
        /// Get or set the default value
        /// </summary>
        public object DefaultValue
        {
            get { return this.m_DefaultValue; }
            protected set
            {
                this.m_DefaultValue = value;
            }
        }
        object ICoreParameterGroupItem.DefaultValue
        {
            get
            {
                return this.m_DefaultValue;
            }
        }
        #region ICoreParameterItem Members
        /// <summary>
        /// get the name
        /// </summary>
        public string Name
        {
            get { return this.m_name; }
            protected set { this.m_name = value; }
        }
        /// <summary>
        /// Get the caption keys
        /// </summary>
        public string CaptionKey
        {
            get { return this.m_captionKey; }
            set { this.m_captionKey = value; }
        }
        #endregion
        protected CoreParameterItemBase() { 
        }
        public CoreParameterItemBase(string name , string captionkey)
        {
            this.m_captionKey = captionkey;
            this.m_name = name;
            this.m_ParamType = enuParameterType.Text;
        }
        #region ICoreParameterGroupItem Members
        public ICoreParameterGroup Group
        {
            get { return this.m_Group; }
            internal set { this.m_Group = value; }
        }
        public virtual enuParameterType ParamType
        {
            get { return this.m_ParamType; }
            protected set { this.m_ParamType = value; }
        }
        public virtual void Invoke(ICoreWorkingConfigurableObject ctr, object value)
        {
            throw new NotImplementedException();
        }
        public virtual object GetDefaultValues()
        {
            return null;
        }
        #endregion
        #region ICoreParameterItem Members
        public virtual void RestoreDefault()
        {            
        }
        #endregion
        public object Value
        {
            get
            {
                return this.m_value;
            }
            set
            {
                if (this.m_value != value)
                {
                    this.m_value = value;
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }
        private void OnValueChanged(EventArgs eventArgs)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, eventArgs);
        }
        public event EventHandler ValueChanged;
        private ICoreParameterEntry m_parent;
        public ICoreDialogToolRenderer Host
        {
            get {
                if (this.m_parent !=null)
                    return this.m_parent.Host;
                return null;
            }
        }
        public ICoreParameterEntry Parent
        {
            get { return this.m_parent; }
            set {
                this.m_parent = value;
            }
        }
    }
}

