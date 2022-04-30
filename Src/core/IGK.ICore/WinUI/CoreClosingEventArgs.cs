

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreClosingEventArgs.cs
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

namespace IGK.ICore.WinUI
{
    public class CoreClosingEventArgs : EventArgs 
    {
        private bool m_cancel;
        private enuContextCloseReason m_closeReason;

        public bool Cancel {
            get {
                return this.m_cancel;
            }
            set {
                this.m_cancel = value;
            }
        }
        public enuContextCloseReason CloseReason {
            get {
                return this.m_closeReason;
            }
        }
        public CoreClosingEventArgs(bool cancel, enuContextCloseReason reason)
        {
            
            this.m_cancel = cancel;
            this.m_closeReason = reason;
        }
    }
}
