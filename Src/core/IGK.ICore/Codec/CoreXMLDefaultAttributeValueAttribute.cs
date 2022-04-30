

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXMLDefaultAttributeValueAttribute.cs
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
file:CoreXMLDefaultAttributeValueAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]  
    public class CoreXMLDefaultAttributeValueAttribute : Attribute 
    {
        private object  m_Value;


        public object   Value
        {
            get { return m_Value; }
        }
        public CoreXMLDefaultAttributeValueAttribute(object value)
        {
            this.m_Value = value;
        }

        public virtual bool IsDefaultValue(object value) 
        {
            return (value.Equals(this.Value));
        }
    }
}

