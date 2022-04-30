

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFObject.cs
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
file:PDFObject.cs
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
    public class PDFObject : PDFDataBase , IPDFObject 
    {
        private PDFIntegerData m_ID;
        private PDFIntegerData m_Update;
        private PDFDictionaryData m_dictionary;
        private PDFDocument m_document;
        private PDFStreamData m_Stream;
        public PDFStreamData Stream
        {
            get { return m_Stream; }
            set
            {
                if (m_Stream != value)
                {
                    m_Stream = value;
                }
            }
        }
        public PDFDocument Document {
            get {
                return this.m_document;
            }
            internal set {
                if (this.m_document != value)
                {
                    if (this.m_document != null) UnRegisterDocumentEvent(m_document);
                    this.m_document = value;
                    if (this.m_document != null) RegisterDocumentEvent(m_document);
                    OnDocumentChanged(EventArgs.Empty);
                }
            }
        }
        public PDFDataBase GetEntry(string key)
        {
            if (this.Data.Entries.Contains(key))
                return this.Data.Entries[key].Data;
            return null;
        }
        public void SetEntry(string key, PDFDataBase data)
        {
            if (this.Data.Entries.Contains(key))
                this.Data.Entries[key].Data = data;
            else
            {
                this.Data.Entries.Add(key, data);
            }
        }
        protected virtual void UnRegisterDocumentEvent(PDFDocument document)
        {
        }
        protected virtual void RegisterDocumentEvent(PDFDocument document)
        {
        }
        public event EventHandler DocumentChanged;
        protected virtual  void OnDocumentChanged(EventArgs eventArgs)
        {
            if (DocumentChanged != null)
                this.DocumentChanged(this, eventArgs);
        }
        #region IPDFObject Members
        public PDFIntegerData Id
        {
            get { return m_ID; }
            internal set {
                m_ID = value;
            }
        }
        public PDFIntegerData Update
        {
            get { return m_Update; }
            set {
                this.m_Update = value;
            }
        }
        #endregion
        internal protected PDFDictionaryData Data {
            get {
                return this.m_dictionary;
            }
        }
        internal PDFObject(PDFIntegerData ID, PDFIntegerData Update)
            : this()
        {            
            this.m_ID = ID;
            this.m_Update = Update;
        }
        internal PDFObject ()
        {
            m_dictionary = new PDFDictionaryData(this);
        }
        public override void Render(System.IO.Stream stream)
        {
            Utils.TextUtils.WriteString(stream, this.Id.ToString());
            stream.WriteByte (PDFConstant.SPACE);
            Utils.TextUtils.WriteString(stream, this.Update.ToString());
            stream.WriteByte(PDFConstant.SPACE);
            Utils.TextUtils.WriteString(stream, PDFConstant.OBJ+PDFConstant .EOF );
            this.Data.Render(stream);
            if (this.Stream != null)
            {
                this.Stream.Render(stream);
            }
            Utils.TextUtils.WriteString(stream, PDFConstant.ENDOBJ + PDFConstant.EOF);
        }
    }
}

