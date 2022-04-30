

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HtmlStringVirtualLineInfo.cs
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
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// will store virtual line 
    /// </summary>
    class HtmlStringVirtualLine 
    {
        private HtmlStringElement m_owner;
        private System.Type m_name;
        
        public System.Type name
        {
            get { return m_name; }
            set
            {
                if (m_name != value)
                {
                    m_name = value;
                }
            }
        }
        public HtmlStringVirtualLine(HtmlStringElement owner)
        {
            this.m_owner = owner;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
