

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuAVIVideoFormat.cs
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
file:enuAVIVideoFormat.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.AVIApi.AVI
{
    using System.Drawing.Imaging;
    public enum enuAVIVideoFormat
    {
        //standard
        Format32S = PixelFormat.Format32bppRgb,
        Format32P = PixelFormat.Format32bppPArgb ,
        Format32 = PixelFormat.Format32bppArgb ,
        Format24 = PixelFormat.Format24bppRgb ,
        Format16 = PixelFormat.Format16bppGrayScale 
    }
}

