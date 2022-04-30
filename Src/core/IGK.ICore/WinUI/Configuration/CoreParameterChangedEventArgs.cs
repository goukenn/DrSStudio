

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterChangedEventArgs.cs
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
file:CoreParameterChangedEventArgs.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI.Configuration
{
    public delegate void CoreParameterChangedEventHandler (object sender, CoreParameterChangedEventArgs e);
    public class CoreParameterChangedEventArgs : EventArgs 
    {
        private object m_Value;
        private ICoreParameterItem m_Item;
        /// <summary>
        /// get the item
        /// </summary>
        public ICoreParameterItem Item
        {
            get { return m_Item; }
        }
        /// <summary>
        /// get the new value
        /// </summary>
        public object Value
        {
            get { return m_Value; }
        }
        public CoreParameterChangedEventArgs(ICoreParameterItem item, object value)
        {
            this.m_Item = item;
            this.m_Value = value;
        }
    }
}

