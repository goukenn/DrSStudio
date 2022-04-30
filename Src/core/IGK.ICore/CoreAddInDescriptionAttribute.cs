

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAddInDescriptionAttribute.cs
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
file:CoreAddInDescriptionAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true , Inherited = false  ) ]
    public class CoreAddInDescriptionAttribute : Attribute 
    {
        private string m_Description;
        private string m_Lang;
        public string Lang
        {
            get { return m_Lang; }            
        }
        public string Description
        {
            get { return m_Description; }
        }
        public CoreAddInDescriptionAttribute(string description):this(description, "Fr")
        {
        }
        public CoreAddInDescriptionAttribute(string description, string lang)
        {
            this.m_Description = description;
            this.m_Lang = lang ;
        }
        public override string ToString()
        {
            return string.Format("{0},{1}", Lang, Description);
        }
    }
}

