

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioJobDialog.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WinUI
{
    class DrSStudioJobDialog : DrSStudioCommonDialog, IXCoreJobDialog 
    {
        public string JobMessage
        {
            get;
            set;
        }

        public int Progress
        {
            get;
            set;
        }

        public bool CanCancel
        {
            get;
            set;
        }

        public CoreMethodHandler CancelCallback
        {
            get;
            set;
        }

        public override string Title
        {
            get
            {
                return "title.jobinprocess".R();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override enuDialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
           
        }
    }
}
