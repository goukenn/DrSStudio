

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MMIOMsgHandler.cs
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
file:MMIOMsgHandler.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.AVIApi.MMIO
{
        public delegate int MMIOMsgHandler(
          MMIOMsgEventArgs e);    
        public class MMIOMsgEventArgs : EventArgs 
        {
            private enuMMIOMessage m_Msg;
            private IntPtr  m_lParam1;
            private IntPtr m_lParam2;
            public long DiskOffset {
                get {
                    return mH.lpmmioinfo.lDiskOffset;
                }
                set {
                    mH.lpmmioinfo.lDiskOffset =value ;
                }
            }
            public IntPtr lParam2
            {
                get { return m_lParam2; }
            }
            public IntPtr  lParam1
            {
                get { return m_lParam1; }
            }
            public enuMMIOMessage Msg
            {
                get { return m_Msg; }
            }
            MMIOManager.MessageHANDLER mH;
            internal MMIOMsgEventArgs(
                MMIOManager.MessageHANDLER mH,
                enuMMIOMessage message,
                IntPtr lParam1,
                IntPtr lParam2)
            {
                this.mH = mH;
                this.m_Msg = message;
                this.m_lParam1 = lParam1;
                this.m_lParam2 = lParam2;
            }
            public void SetEndRead(IntPtr offset)
            {
                this.mH.SetEndRead(offset);
            }
            public void SetEndWrite(IntPtr offset)
            {
                this.mH.SetEndWrite(offset);
            }
        }
}

