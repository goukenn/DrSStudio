

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OperationExtension.cs
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
file:OperationExtension.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Data.Math
{
    public static class OperationExtension
    {
        public static float MinLog(this int i, float pow)
        {
            return (float)System.Math.Pow(2, (float)System.Math.Floor(System.Math.Log10(i) / System.Math.Log10(pow)));
        }
        public static float MaxLog(this int i, float pow)
        {
            float f = (float)System.Math.Pow(2, (float)System.Math.Floor(System.Math.Log10(i) / System.Math.Log10(pow)) + 1);
            return f;
        }
    }
}

