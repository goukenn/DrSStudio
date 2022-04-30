

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageElement.cs
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
file:ImageElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    [Core2DStandarItem("Image", typeof(RectangleMecanism<ImageElement>), IsVisible =false ) ]
    public class ImageElement : RectangleElement 
    {
        private ICoreBitmap m_Bitmap;
        public ICoreBitmap Bitmap
        {
            get { return m_Bitmap; }
            set
            {
                if (m_Bitmap != value)
                {
                    m_Bitmap = value;
                }
            }
        }
    }
}

