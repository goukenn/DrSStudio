

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFDocument.cs
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
file:PDFDocument.cs
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
using System.IO;
namespace IGK.PDFApi
{
    /// <summary>
    /// represent a pdf document builder
    /// </summary>
    public class PDFDocument : IPDFDocument
    {
        /// <summary>
        /// load a pdf document 
        /// </summary>
        /// <param name="filename">pdf file</param>
        /// <returns>PDFDocument</returns>
        public static PDFDocument Load(string filename) {
            return null;
        }
        #region IPDFDocument Members
        private PDFVersion m_Version;
        private PDFFileTrailer m_Trailer;
        private PDFCatalog m_Catalog;
        private PDFPages m_Pages;
        public PDFPages Pages
        {
            get { return m_Pages; }
            set
            {
                if (m_Pages != value)
                {
                    m_Pages = value;
                }
            }
        }
        public PDFVersion Version
        {
            get
            {
                return m_Version ;
            }
            set
            {
              this.m_Version = value ;
            }
        }
        public PDFFileTrailer Trailer { get { return this.m_Trailer; } }
        public PDFCatalog Catalog
        {
            get { return m_Catalog; }
            set
            {
                if (m_Catalog != value)
                {
                    m_Catalog = value;
                }
            }
        }
        public PDFDocument()
        {
            this.m_body = new PDFFileBody(this);
            this.m_Trailer = new PDFFileTrailer(this);
            this.m_Catalog = CreateNewCatalog();
            this.m_Version = PDFVersion.Version_1_7;
            this.m_Pages = CreateNewPagesCollection();
        }
        public void Save(string file)
        {
            Stream sm = File.Create(file);            
            if (this.Version != null) this.Version.Render(sm);         
            this.m_body.Render(sm);
            this.m_Trailer.Render(sm);
            //end of document
            StreamWriter sw = new StreamWriter(sm);
            sw.Write(PDFConstant .DOC_EOF);
            sw.Flush();
            //sw.Close();
            sm.Close();
        }
        #endregion
        #region IPDFDocument Members
        PDFFileBody m_body;
        public PDFFileBody Body
        {
            get {
                return this.m_body;
            }
        }
        #endregion
        /// <summary>
        /// create an add new catalog
        /// </summary>
        public virtual PDFCatalog CreateNewCatalog()
        {
            PDFCatalog v_cd = PDFFont.CreatePDFObject("Catalog") as PDFCatalog;
                this.m_body.Objects.Add (v_cd);
            return v_cd;
        }
        public virtual PDFFont CreateNewFont()
        {
            PDFFont v_cd = PDFObject.CreatePDFObject("Font") as PDFFont ;
            this.m_body.Objects.Add(v_cd);
            return v_cd;
        }
        public virtual PDFPages CreateNewPagesCollection()
        {
            PDFPages v_p = PDFObject.CreatePDFObject("Pages") as PDFPages;
            this.m_body.Objects.Add(v_p);
            return v_p;
        }
    }
}

