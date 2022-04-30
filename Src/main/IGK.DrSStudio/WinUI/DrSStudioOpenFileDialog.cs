

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioOpenFileDialog.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    class DrSStudioOpenFileDialog : DrSStudioCommonDialog, IXCoreOpenDialog
    {
        OpenFileDialog sfd = new OpenFileDialog();

        public override string Title
        {
            get
            {
                return sfd.Title;
            }
            set
            {
                sfd.Title = value;
            }
        }

        public override enuDialogResult ShowDialog()
        {
            return (enuDialogResult)sfd.ShowDialog();
        }

        public override void Dispose()
        {
            this.sfd.Dispose();
        }

        public string FileName
        {
            get
            {
                return this.sfd.FileName;
            }
            set
            {
                this.sfd.FileName = value;
            }
        }

        public string Filter
        {
            get
            {
                return this.sfd.Filter;
            }
            set
            {
                this.sfd.Filter = value;
            }
        }
    }
}
