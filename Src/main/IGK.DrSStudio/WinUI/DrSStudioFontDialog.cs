

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioFontDialog.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    class DrSStudioFontDialog :DrSStudioCommonDialog, IXCoreFontDialog
    {
        FontDialog ftDialog;
        private string m_title;
        public DrSStudioFontDialog()
        {
            this.ftDialog = new FontDialog();

        }
        public override string Title
        {
            get
            {
                return this.m_title;
            }
            set
            {
                this.m_title = value;
            }
        }

        public override enuDialogResult ShowDialog()
        {
            return (enuDialogResult)this.ftDialog.ShowDialog();
        }

        public override void Dispose()
        {
            ftDialog.Dispose();
        }

        public ICoreFont Font
        {
            get { 
                return CoreFont.CreateFrom (this.Font.FontName , null);
            }
        }
    }
}
