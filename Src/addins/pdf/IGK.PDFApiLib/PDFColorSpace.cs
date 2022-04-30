

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFColorSpace.cs
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
file:PDFColorSpace.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.PDFApi
{
    public sealed class PDFColorSpace : PDFDataBase 
    {
        public static readonly PDFColorSpace DeviceGray;
        public static readonly PDFColorSpace DeviceRGB;
        public static readonly PDFColorSpace DeviceCMYK;
        private string m_value;
        static PDFColorSpace()
        {
            DeviceGray = new PDFColorSpace(PDFConstant.DEVICE_GRAY);
            DeviceRGB = new PDFColorSpace(PDFConstant.DEVICE_RGB);
            DeviceCMYK = new PDFColorSpace(PDFConstant.DEVICE_CMYK);
        }
        private  PDFColorSpace(string value)
        {
            this.m_value = value;
        }        
        public override string ToPdf()
        {
            return this.m_value;
        }
    }
}

