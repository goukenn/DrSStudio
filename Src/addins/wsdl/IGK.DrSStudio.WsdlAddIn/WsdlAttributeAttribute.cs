

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlAttributeAttribute.cs
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
    public class WsdlAttributeAttribute : Attribute
    {
        private bool m_IsAttribute;
        public static bool IsWsdlAttribute(PropertyInfo pr)
        {
            WsdlAttributeAttribute t = GetCustomAttribute(pr, typeof(WsdlAttributeAttribute)) as WsdlAttributeAttribute;
            return (t != null) && t.IsAttribute;
        }
        public WsdlAttributeAttribute():this(true)
        {

        }

        public WsdlAttributeAttribute(bool p)
        {
            this.m_IsAttribute = p;
        }
        public bool IsAttribute
        {
            get { return m_IsAttribute; }
            set
            {
                if (m_IsAttribute != value)
                {
                    m_IsAttribute = value;
                }
            }
        }
    }
}
