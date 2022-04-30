

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreLineCap.cs
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
file:ICoreLineCap.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public interface  ICoreLineCap
    {
        /// <summary>
        /// get the path name of the anchor
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// get or set the path element
        /// </summary>
        CoreGraphicsPath  PathElement { get; }
        /// <summary>
        /// get the Line Cap of the pen anchor
        /// </summary>
        enuLineCap LineCap { get; }
        float BaseInset { get; }
        float WidthScale { get; }
        enuLineCap CustomCap { get; }
        //CustomLineCap GetCustomCap();
    }
}

