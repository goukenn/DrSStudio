

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreDependencyPropertyMetadata.cs
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
file:CoreDependencyPropertyMetadata.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// represent a core dependency meta data
    /// </summary>
    public class CoreDependencyPropertyMetadata
    {
        private object m_DefaultValue;
        /// <summary>
        /// get or set the default value
        /// </summary>
        public object DefaultValue
        {
            get { return m_DefaultValue; }
            set
            {
                if (m_DefaultValue != value)
                {
                    m_DefaultValue = value;
                }
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="defaultValue"></param>
        public CoreDependencyPropertyMetadata(object defaultValue)
        {
            this.m_DefaultValue = defaultValue;
        }
    }
}

