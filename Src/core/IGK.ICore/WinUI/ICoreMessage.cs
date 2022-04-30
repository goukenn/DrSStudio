

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMessage.cs
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
file:ICoreMessage.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent message used in IcoreMessageFilter procedure
    /// </summary>
    public  interface ICoreMessage
    {
        /// <summary>
        /// get or set the result
        /// </summary>
        IntPtr Result { get; set; }
        /// <summary>
        /// get or set the w param
        /// </summary>
        IntPtr LParam { get; set; }
        /// <summary>
        /// get or set 
        /// </summary>
        IntPtr WParam { get; set; }
        /// <summary>
        /// get the window handle
        /// </summary>
        IntPtr HWnd { get; }
        /// <summary>
        /// get the message
        /// </summary>
        int Msg { get; }

        bool IsKeyInputMessage();
    }
}

