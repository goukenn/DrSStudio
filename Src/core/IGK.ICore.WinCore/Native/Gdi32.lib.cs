

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Gdi32.lib.cs
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
file:Gdi32.lib.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace IGK.ICore.Windows.Native
{
    internal class Gdi32
    {
        [DllImport("user32.dll")]
        internal extern static IntPtr GetDC(IntPtr hwnd);
        [DllImport("gdi32.dll")]
        internal extern static IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        internal extern static IntPtr SelectObject(IntPtr hdc, IntPtr hObj);
        [DllImport("gdi32.dll")]
        internal extern static IntPtr DeleteDC(IntPtr dc);
        [DllImport("gdi32.dll")]
        internal extern static IntPtr DeleteObject(IntPtr hobj);
    }
}

