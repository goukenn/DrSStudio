

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingFilter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.Imaging
{
    public interface ICore2DDrawingFilter
    {

        
        /// <summary>
        /// get the name of the filter
        /// </summary>
        string Name { get; }
        /// <summary>
        /// get if this filter can't applied
        /// </summary>
        bool CanApply { get; }
        /// <summary>
        /// all requirement
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="requireSize"></param>
        void Apply(ICoreBitmap bitmap, Rectanglei requireSize);
    }
}