

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InvertColorFilter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:InvertColorFilter.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Filters
{
    /// <summary>
    /// invert color filter
    /// </summary>
   public sealed class InvertColorFilter : Core2DDrawingFilterBase
    {
        public override bool Activated
        {
            get { return true; }
        }
        public override string Name
        {
            get { return CoreConstant.FILTER_INVERTCOLOR; }
        }
        public override bool ApplyFilter(ref System.Drawing.Bitmap bmp)
        {
            System.Drawing.Bitmap v_bkbmp = bmp;
            bmp = CoreBitmapOperation.InvertColor(bmp);
            v_bkbmp.Dispose();
            return true;
        }
    }
}

