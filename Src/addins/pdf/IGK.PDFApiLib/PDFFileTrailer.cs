

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFFileTrailer.cs
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
file:PDFFileTrailer.cs
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
    public class PDFFileTrailer : PDFDataBase , IPDFTrailer
    {
        private PDFDictionaryData m_Data;
        public PDFIntegerData Size{get{ return (PDFIntegerData)m_Data.Entries["Size"].Data;} set{}}
        public PDFDataBase Prev{get{return (PDFIntegerData)m_Data.Entries["Prev"].Data;}}
        public PDFDataBase Root { get { return (PDFDataBase)m_Data.Entries["Prev"].Data; } }
        public PDFDataBase Encrypt { get { return (PDFDataBase)m_Data.Entries["Prev"].Data; } }
        public PDFDataBase ID { get { return (PDFDataBase)m_Data.Entries["ID"].Data; } }
//Root
//Encrypt
//Info
//ID
        PDFDocument m_document;
        public PDFFileTrailer(PDFDocument document)
        {
            m_Data = new PDFDictionaryData();
            m_document = document;
            m_document.Body.ObjectAdded += new PDFObjectEventHandler(Body_ObjectAdded);
        }
        void Body_ObjectAdded(object sender, PDFObjectEventArgs e)
        {
            if (e.Object is PDFCatalog)
            {
                this.m_Data.Entries.Add("Root", new PDFIndirectObject(e.Object));
            }
        }
        public override void Render(System.IO.Stream stream)
        {
            Utils.TextUtils.WriteString(stream, PDFConstant.TRAILER+PDFConstant.EOF);
            if (this.m_Data.Entries.Contains("Size"))
                this.m_Data.Entries["Size"].Data = (PDFIntegerData)this.m_document.Body.Objects.Count;
            else
                this.m_Data.Entries.Add("Size", (PDFIntegerData)this.m_document.Body.Objects.Count);
            this.m_Data.Render(stream);
            Utils.TextUtils.WriteString(stream, PDFConstant.STARTXREF+PDFConstant.EOF );
        }
        #region IPDFTrailer Members
        public PDFDictionaryData Entries
        {
            get { return this.m_Data; }
        }
        #endregion
    }
}

