

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreLocalizableProperty.cs
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
file:CoreLocalizableProperty.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    struct CoreLocalizableProperty
    {
        private ICoreWorkingObject m_Object;
        private System.Reflection.PropertyInfo m_Property;
        public System.Reflection.PropertyInfo Property
        {
            get { return m_Property; }
            set
            {
                if (m_Property != value)
                {
                    m_Property = value;
                }
            }
        }
        public ICoreWorkingObject Object
        {
            get { return m_Object; }
            set
            {
                if (m_Object != value)
                {
                    m_Object = value;
                }
            }
        }
        public CoreLocalizableProperty(ICoreWorkingObject @object , System.Reflection.PropertyInfo property)
        {
            this.m_Object = @object;
            this.m_Property = property;
        }
    }
}

