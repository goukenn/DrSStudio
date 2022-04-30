

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlElementAttribute.cs
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
using System.Reflection;
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    public class WsdlElementAttribute : Attribute
    {
        private bool m_IsElement;

        public bool IsElement
        {
            get { return m_IsElement; }
            set
            {
                if (m_IsElement != value)
                {
                    m_IsElement = value;
                }
            }
        }
        public WsdlElementAttribute()
        {
            this.m_IsElement = true;
        }
        public WsdlElementAttribute(bool attribute)
        {
            this.m_IsElement = attribute;
        }

        public static bool IsWsdlElement(PropertyInfo pr)
        {
            WsdlElementAttribute t = GetCustomAttribute(pr, typeof(WsdlElementAttribute)) as WsdlElementAttribute;
            return (t != null) && t.IsElement;
        }
    }
}
