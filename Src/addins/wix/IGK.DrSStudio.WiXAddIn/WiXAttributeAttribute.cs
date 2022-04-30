

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXAttributeAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXAttributeAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    [AttributeUsage (AttributeTargets.Property)]
    public class WiXAttributeAttribute : Attribute 
    {
        private bool m_IsAttribute;
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
        public WiXAttributeAttribute()
        {
            this.m_IsAttribute = true;
        }
        public static bool IsWixAttribute(System.Reflection.PropertyInfo prInfo)
        {
           WiXAttributeAttribute t =  GetCustomAttribute (prInfo , MethodInfo.GetCurrentMethod().DeclaringType )  as WiXAttributeAttribute  ;
           return (t != null) && t.IsAttribute ;
        }
    }
}
