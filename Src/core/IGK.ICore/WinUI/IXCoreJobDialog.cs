

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IXCoreJobDialog.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    using IGK.ICore;using IGK.ICore.WinUI.Common;

    /// <summary>
    /// represent interface of a job dialog
    /// </summary>
    public interface IXCoreJobDialog : IXCommonDialog
    {
        /// <summary>
        /// get or set the job label message
        /// </summary>
        string JobMessage { get; set; }
        /// <summary>
        /// get or set the progression. value range [0,100]
        /// </summary>
        int  Progress { get; set; }
        /// <summary>
        /// get or set if this dialog can't cancel the current job. 
        /// </summary>
        bool CanCancel { get; set; }

        CoreMethodHandler CancelCallback { get; set; }
    }
}
