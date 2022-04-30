

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreLocalizeAttribute.cs
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
file:CoreLocalizeAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent an attribute is localize
    /// </summary>
    [AttributeUsage (AttributeTargets.Property , AllowMultiple =false , Inherited=false )]
    public class CoreLocalizeAttribute : Attribute 
    {
        private bool m_Localize;
        public bool Localize
        {
            get { return m_Localize; }
            set
            {
                if (m_Localize != value)
                {
                    m_Localize = value;
                }
            }
        }
        public CoreLocalizeAttribute(bool localize)
        {
            this.m_Localize = localize;
        }
    }
}

