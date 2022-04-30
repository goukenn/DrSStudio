

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreKeyEventArgs.cs
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
file:CoreKeyEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// reprsent a key event args
    /// </summary>
    public class CoreKeyEventArgs : EventArgs 
    {
        private enuKeys m_keys;
        public CoreKeyEventArgs(enuKeys key)
        {
            this.m_keys = key;
        }
        public enuKeys KeyCode { get { return this.m_keys; } }

        
        /// <summary>
        /// Get the modifier keys
        /// </summary>
        public enuKeys Modifiers {
            get {
                return CoreApplicationManager.ModifierKeys;
            }
        }
    }
}

