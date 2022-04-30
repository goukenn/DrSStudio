

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFPages.cs
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
file:PDFPages.cs
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
    public class PDFPages: PDFPageNode 
    {
        PageCollections m_pageCollections;
        public PDFArrayData Kids {
            get {
                return (PDFArrayData)this.Data.Entries["Kids"].Data ;
            }
        }
        internal PDFPages():base()
        {
            this.m_pageCollections = new PageCollections(this);
            this.Data.Entries.Add ("Type", new PDFIdentifier("Pages"));
            this.Data.Entries.Add("Kids", new PDFArrayData());
            this.Data.Entries.Add("Count",  new PDFCounter(this.Kids ));
        }
        public class PageCollections : IPDFPageCollections
        {
            List<PDFPage> m_pages;
            PDFPages m_owner;
            public PageCollections(PDFPages page)
            {
                this.m_owner = page;
                m_pages = new List<PDFPage>();
            }
            #region IPDFPageCollections Members
            public int Count
            {
                get { return this.m_pages.Count; }
            }
            public void Add(PDFPage page)
            {
                if ((page != null) && !this.m_pages.Contains(page))
                {
                    page.Parent = this.m_owner ;
                    this.m_pages.Add(page);
                    this.m_owner.Kids.Add(new PDFIndirectObject(page));
                    this.m_owner.Document.Body.Objects.Add(page);
                }
            }
            public void Remove(PDFPage page)
            {
                if (this.m_pages.Contains(page))
                {
                    this.m_pages.Remove(page);
                }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
            #endregion
        }
        public void Add(PDFPage page)
        {
            this.m_pageCollections.Add(page);
        }
        public PDFPage AddNewPage()
        {
            PDFPage page = PDFObject.CreatePDFObject("Page") as PDFPage;
            this.Add(page);
            return page;
        }
        public PDFPage AddNewPage(PDFRectangle rec)
        {
            PDFPage p = AddNewPage ();
            p.MediaBox = rec;
            return p;
        }
    }
}

