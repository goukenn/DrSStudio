

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidAttributeEnumValue.cs
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

namespace IGK.DrSStudio.Android.Tools
{
    /// <summary>
    /// android 
    /// </summary>
    public class AndroidAttributeEnumValue
    {
        private string m_Name;
        private string m_Value;
        /// <summary>
        /// get or set the value of this attribute
        /// </summary>
        public string Value
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
        /// <summary>
        /// get or set the name this value
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }

        public override string ToString()
        {
            return "Enum: " + this.Name + " : " + Value;
        }

        public string Type { get; set; }
    }
}
