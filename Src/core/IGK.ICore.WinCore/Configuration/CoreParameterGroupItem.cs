

using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.ComponentModel;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterGroupItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreParameterGroupItem.cs
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
namespace IGK.ICore.WinCore.Configuration
{
    /// <summary>
    /// represent items that will be addes to a group
    /// </summary>
    public class CoreParameterGroupItem : 
        CoreParameterItemBase ,
        ICoreParameterGroupItem 
    {
        ICoreParameterGroup m_group;
        private CoreParameterChangedEventHandler m_Event;
        public override string ToString()
        {
            return this.Name + ":[" + this.ParamType+"]";
        }
        public CoreParameterChangedEventHandler Event
        {
            get { return m_Event; }
            protected set { m_Event = value; }
        }
        public CoreParameterGroupItem(string name, string caption, ICoreParameterGroup owner):base(name, caption )
        {
            this.m_group = owner;
        }
        public CoreParameterGroupItem(
            string name,
            string captionkey,
            ICoreParameterGroup owner, 
            enuParameterType type, 
            CoreParameterChangedEventHandler devent):
            this(name ,captionkey, owner, null,type,devent )
        {
        }
        public CoreParameterGroupItem(
           string name,
           string captionkey,
           ICoreParameterGroup owner,
            object defaultValue,
           enuParameterType type,
           CoreParameterChangedEventHandler devent):base(name, captionkey )
        {
            this.DefaultValue = defaultValue;
            this.ParamType = type;
            this.m_group = owner;            
            this.m_Event = devent;
        }
        #region ICoreParameterGroupItem Members
        public override void Invoke(ICoreWorkingConfigurableObject item, object value)
        {
            if (this.m_Event != null)
                this.m_Event(this, new CoreParameterChangedEventArgs(this, value));
            else {
                System.Reflection.PropertyInfo v_prInfo =
                    item.GetType().GetProperty(this.Name);
                if ((v_prInfo != null) && (v_prInfo.CanWrite))
                {
                    System.ComponentModel.TypeConverter v_conv = CoreTypeDescriptor.GetConverter(
                        v_prInfo.PropertyType);
                    try
                    {
                        v_prInfo.SetValue(item, v_conv.ConvertFrom(value.ToString()), null);
                    }
                    catch (Exception ex){
                        CoreLog.WriteDebug("Error Append : " + ex.Message);
                    }
                }
            }
        }
        #endregion
        #region ICoreParameterGroupItem Members
        public override object GetDefaultValues()
        {            
            return this.DefaultValue;
        }
        #endregion
        #region ICoreParameterItem Members
        public override  void RestoreDefault()
        {            
            this.Invoke(null, this.DefaultValue);  
        }
        #endregion
    }
}

