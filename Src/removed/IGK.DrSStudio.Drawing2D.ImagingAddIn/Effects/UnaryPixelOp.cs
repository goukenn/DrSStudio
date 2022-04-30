

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UnaryPixelOp.cs
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
file:UnaryPixelOp.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Effects
{
    public abstract class UnaryPixelOp
    {
        public abstract Colorf Apply(Colorf data);
        public void  Apply(CoreBitmapData data)
        {
            for (int j = 0; j < data.Height; j++)
            {
                for (int i = 0; i < data.Width; i++)
                {
                    data.WritePixel(Apply(data.ReadPixel(i, j)), i, j);
                }
            }
        }
    }
}

