

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioColorDialog.cs
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    class DrSStudioColorDialog :DrSStudioCommonDialog, IXCoreColorDialog
    {
        ColorDialog dialog = new ColorDialog();
        IntPtr clDialogHandle = IntPtr.Zero;

        public DrSStudioColorDialog()
        {
            this.dialog = new ColorDialog();
            this.dialog.AllowFullOpen = true;
        }
        public override string Title
        {
            get
            {
                return GetTitle();
            }
            set
            {
                this.SetTitle(value);
            }
        }

        private string GetTitle()
        {
            return string.Empty;
        }

        private void SetTitle(string value)
        {
            
        }

        public override enuDialogResult ShowDialog()
        {
            return (enuDialogResult)dialog.ShowDialog();
        }

        public override void Dispose()
        {
            dialog.Dispose();
        }

        public Colorf Color
        {
            get
            {
                return Colorf.FromIntArgb(dialog.Color.ToArgb());
            }
            set
            {
                this.dialog.Color = value.CoreConvertTo<Color>();
            }
        }
    }
}
