

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFCatalog.cs
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
file:PDFCatalog.cs
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
    public class PDFCatalog : PDFTypeObject
    {        
        /// <summary>
        /// get or set the page mode
        /// </summary>
        public PDFPageMode PageMode
        {
            get { return this.GetEntry ("PageMode") as PDFPageMode ; }
            set
            {
                this.SetEntry("PageMode", value);
            }
        }
        internal PDFCatalog()
        {
            this.Data.Entries.Add ("Type", new PDFIdentifier("Catalog"));
        }
        protected override void RegisterDocumentEvent(PDFDocument document)
        {
            document.Body.ObjectAdded += new PDFObjectEventHandler(Body_ObjectAdded);
            document.Body.ObjectRemove += new PDFObjectEventHandler(Body_ObjectRemove);
        }
        protected override void UnRegisterDocumentEvent(PDFDocument document)
        {
            document.Body.ObjectAdded -= new PDFObjectEventHandler(Body_ObjectAdded);
            document.Body.ObjectRemove -= new PDFObjectEventHandler(Body_ObjectRemove);
        }
        void Body_ObjectRemove(object sender, PDFObjectEventArgs e)
        {
            if (e.Object is PDFPages)
            {
            ///    this.Data.Entries.Remove("Pages");
            }
        }
        void Body_ObjectAdded(object sender, PDFObjectEventArgs e)
        {
            if (e.Object is PDFPages)
            {
                this.Data.Entries.Add("Pages", new PDFIndirectObject(e.Object));
            }
        }
    }
}

