

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSettingPropertyAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Settings
{
    /// <summary>
    /// mark a property as a setting property
    /// </summary>
    public class CoreSettingPropertyAttribute : Attribute
    {
        private bool m_IsProperty;

        public bool IsProperty
        {
            get { return m_IsProperty; }
            set
            {
                if (m_IsProperty != value)
                {
                    m_IsProperty = value;
                }
            }
        }
        public CoreSettingPropertyAttribute()
        {
            this.m_IsProperty = true;
        }
    }
}
