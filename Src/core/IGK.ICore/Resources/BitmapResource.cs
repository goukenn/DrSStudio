

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BitmapResource.cs
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
file:BitmapResource.cs
*/
using System;
using System.IO;
namespace IGK.ICore.Resources
{
    using IGK.ICore;using IGK.ICore.Drawing2D;
    public class BitmapResource : CoreResourceItemBase
    {
        public BitmapResource()
        {
        }
        private ICoreBitmap m_Bitmap;
        /// <summary>
        /// get or set the bitmap
        /// </summary>
        public ICoreBitmap  Bitmap
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
        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.Bitmap; }
        }
        public override object GetData()
        {
            return this.Bitmap;
        }
        public override string GetDefinition()
        {
           // return CoreBitmapOperation.BitmapToBase64String(this.m_Bitmap, false);
            return string.Empty;
        }
       
    }
}

