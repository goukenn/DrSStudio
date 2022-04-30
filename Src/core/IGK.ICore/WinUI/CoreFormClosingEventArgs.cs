

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreFormClosingEventArgs.cs
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
file:CoreFormClosingEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    public class CoreFormClosingEventArgs : EventArgs 
    {
        private enuCloseReason m_CloseReason;
        private bool m_Cancel;
        public bool Cancel
        {
            get { return m_Cancel; }
            set
            {
                if (m_Cancel != value)
                {
                    m_Cancel = value;
                }
            }
        }
        public enuCloseReason CloseReason
        {
            get { return m_CloseReason; }
        }
        public CoreFormClosingEventArgs(enuCloseReason reason, bool cancel)
        {
            this.m_Cancel = cancel;
            this.m_CloseReason = reason;
        }
    }
}

