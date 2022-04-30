

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterGroupEnumItem.cs
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
file:CoreParameterGroupEnumItem.cs
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
using System.Reflection ;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore;
namespace IGK.ICore.WinCore.Configuration
{
    /// <summary>
    /// represent a group paramenter enum Item
    /// </summary>
    public class CoreParameterGroupEnumItem : 
        CoreParameterGroupItem ,
        ICoreParameterGroupEnumItem
    {
        private Type m_enumType;
        private PropertyInfo m_prInfo;
        private EnumItemElement m_selectedItem;
        private object m_target;
        public PropertyInfo PropertyInfo
        {
            get { return m_prInfo; }
        }
        public CoreParameterGroupEnumItem(object target, 
            string name , 
            string captionkey, 
            Type enumType,
            ICoreParameterGroup group,
            object defaultValue,
            CoreParameterChangedEventHandler _event):
            base(name, captionkey, group ,defaultValue, enuParameterType .EnumType , _event)
        {
            if ((enumType == null) || (!enumType .IsEnum ))
                throw new CoreException (enuExceptionType.ArgumentNotValid , "enumType");
            this.m_target = target;
            this.m_prInfo = null;
            this.m_selectedItem = new EnumItemElement(defaultValue, defaultValue as string );
            this.m_enumType = enumType;
        }
        public CoreParameterGroupEnumItem(
            object Taget,
            PropertyInfo prInfo, 
            string captionKey,
            ICoreParameterGroup group)
            :base(prInfo.Name,captionKey,group, enuParameterType.EnumType ,null)
        {
            this.m_target = Taget;
            this.m_prInfo = prInfo;
        }
        public override void Invoke(
            ICoreWorkingConfigurableObject item,
            object value)
        {
            if (m_prInfo != null)
            {
                m_prInfo.SetValue(item, ((EnumItemElement)value).Value, null);
            }
            else {
                base.Invoke(item, ((EnumItemElement)value).Value);
            }
        }
        /// <summary>
        /// retreive the default values;
        /// </summary>
        /// <returns></returns>
        public override object GetDefaultValues()
        {
            List<EnumItemElement> m_l = new List<EnumItemElement>();
            EnumItemElement v_m = null;
            Type v_type = null;
            object v_def = null;
            if (this.DefaultValue == null)
            {
                if (this.PropertyInfo == null)
                {
                    v_type = this.m_enumType;
                    v_def = Enum.GetValues(v_type).GetValue(0);
                }
                else
                {
                    v_type = PropertyInfo.PropertyType;
                    v_def = PropertyInfo.GetValue(this.m_target, null);
                }
                this.DefaultValue = v_def;
            }
            else {
                v_def = this.DefaultValue;
                v_type = this.m_enumType;
            }
            foreach (object c in Enum.GetValues(v_type))
            {
                v_m = new EnumItemElement(c, "enum." + c.ToString());
                if (v_def.Equals (c))
                    this.m_selectedItem = v_m;
                m_l.Add (v_m);
            }
            return m_l.ToArray();
        }
        /// <summary>
        /// enum Selected Element
        /// </summary>
        internal class EnumItemElement
        {
            private string m_CaptionKey;
            public string CaptionKey
            {
                get { return CoreSystem.GetString (m_CaptionKey); }
            }
            private object m_Value;
            public object Value
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
            public EnumItemElement(object o, string captionkey)
            {
                this.m_Value = o;
                this.m_CaptionKey = captionkey;
            }
        }
        #region ICoreParameterGroupItemEnum Members
        /// <summary>
        /// return the selected enum item
        /// </summary>
        /// <returns></returns>
        public object GetSelectedItem()
        {
            return m_selectedItem;
        }
        #endregion
    }
}

