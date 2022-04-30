

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFConstant.cs
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
file:PDFConstant.cs
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
    public static class PDFConstant
    {
        public const string EOF = "\xD\xA";
        public const string DOC_EOF = "%%EOF";
        public const byte SPACE = 0x20;
        public const string OBJ = "obj";
        public const string ENDOBJ = "endobj";
        public const string STREAM = "stream";
        public const string ENDSTREAM = "endstream";
        public const string TRAILER = "trailer";
        public const string STARTXREF = "startxref";
        public const string PARENT = "Parent";
        public const string MEDIABOX = "MediaBox";
        public const string TYPE = "Type";
        public const string PAGE = "Page";
        public const string PAGES = "Pages";
        public const string XREF = "xref";
        public const string SUBTYPE = "Subtype";
        public const string RESOURCES = "Resources";
        public const string PROCSET = "ProcSet";
        public const string FONT = "Font";
        public const string CONTENTS = "Contents";
        public const string DEVICE_GRAY = "DeviceGray";
        public const string DEVICE_RGB = "DeviceRGB";
        public const string DEVICE_CMYK = "DeviceCMYK";
    }
}

