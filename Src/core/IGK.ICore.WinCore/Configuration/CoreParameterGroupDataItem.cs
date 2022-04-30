

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterGroupDataItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreParameterGroupDataItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinCore.Configuration
{
    class CoreParameterGroupDataItem : CoreParameterGroupItem, ICoreParameterItem, ICoreParameterGroupEnumItem
    {
        object m_target;
        object m_datasource;
        public CoreParameterGroupDataItem(object target,
            string name,
            string captionkey,
            object datasource,
            ICoreParameterGroup group,
            object defaultValue,
            CoreParameterChangedEventHandler _event) :
            base(name, captionkey, group, defaultValue, enuParameterType.EnumType, _event)
        {
            this.m_target = target;
            this.m_datasource = datasource;
        }
        public override object GetDefaultValues()
        {
            return m_datasource  ;
        }
        public object GetSelectedItem()
        {
            Type t = this.m_datasource.GetType();
            if (t.IsArray)
            {
                return ((Array)this.m_datasource).GetValue(0);
            }
            return null;
        }
    }
}

