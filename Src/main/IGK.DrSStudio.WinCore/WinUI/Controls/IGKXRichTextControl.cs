

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXRichTextControl.cs
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    public class IGKXRichTextControl : RichTextBox, IXRichtTextControl , IXTextControl 
    {
        public IGKXRichTextControl()
        {

        }

        public virtual string ToXml()
        {
            return this.Text;
        }

        Rectanglef IXTextControl.Bounds
        {
            get
            {
                Rectangle rc = base.Bounds;
                return new Rectanglef(rc.X,
                    rc.Y,
                    rc.Width,
                    rc.Height);
            }
            set
            {
                this.Bounds = new System.Drawing.Rectangle(
                    (int)value.X,
                    (int)value .Y ,
                    (int)value.Width,
                    (int)value.Height 
                    );
            }
        }

        public void SetFont(ICoreFont ft)
        {
            this.Font = ft.ToGdiFont();
        }

        public void SetFont(ICoreFont ft, float fsize, enuFontStyle style)
        {
            this.Font = ft.ToGdiFont(fsize, style);
        }
    }
}
