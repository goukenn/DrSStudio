

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreItemEventArgs.cs
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

namespace IGK.ICore
{
    public class CoreItemEventArgs : EventArgs 
    {
        public CoreItemEventArgs(object  item)
        {
            this.m_Item = item;
        }
        private object  m_Item;
        /// <summary>
        /// get or set the object
        /// </summary>
        public object  Item
        {
            get { return m_Item; }
            set
            {
                if (m_Item != value)
                {
                    m_Item = value;
                }
            }
        }
    }
}
