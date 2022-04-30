

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ReplacementUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ReplacementUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.PageManagerAddIn
{
    class ReplacementUtils
    {
        private string m_Pattern;
        private string m_Value;
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
        public string Pattern
        {
            get { return m_Pattern; }
            set
            {
                if (m_Pattern != value)
                {
                    m_Pattern = value;
                }
            }
        }
        public ReplacementUtils(string Pattern, string value )
        {
            this.m_Pattern = Pattern;
            this.m_Value = value;
        }
    }
}

