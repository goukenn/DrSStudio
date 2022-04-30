

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFPageMode.cs
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
file:PDFPageMode.cs
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
    public sealed class PDFPageMode : PDFIdentifier 
    {
        public static readonly PDFPageMode UseNoneNeither;    
        public static readonly PDFPageMode FullScreen;
        public static readonly PDFPageMode UseOutlinesDocument;
        public static readonly PDFPageMode UseThumbsThumbnail;        
        public static readonly PDFPageMode UseOC;
        public static readonly PDFPageMode UseAttachments;
        static PDFPageMode()
        {
            UseNoneNeither = new PDFPageMode("UseNoneNeither");
            FullScreen = new PDFPageMode("FullScreen");
            UseOutlinesDocument = new PDFPageMode("UseOutlinesDocument");
            UseThumbsThumbnail = new PDFPageMode("UseThumbsThumbnail");           
            UseOC = new PDFPageMode("UseOC");
            UseAttachments = new PDFPageMode("UseAttachments");
        }
        internal PDFPageMode(string name):base(name)
        {            
        }
    }
}

