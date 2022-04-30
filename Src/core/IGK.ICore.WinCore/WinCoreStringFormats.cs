

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreStringFormats.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    static class WinCoreStringFormats
    {
        private static StringFormat sm_TrimmingHorizontalAlignment;

        public static StringFormat TrimmingHorizontalAlignment{
            get {
                return sm_TrimmingHorizontalAlignment;
            }
        }
        static WinCoreStringFormats() {
            sm_TrimmingHorizontalAlignment = new StringFormat();
            sm_TrimmingHorizontalAlignment.Trimming = StringTrimming.Character;
            sm_TrimmingHorizontalAlignment.Alignment = StringAlignment.Near;
            sm_TrimmingHorizontalAlignment.LineAlignment = StringAlignment.Center;
        }
    }
}
