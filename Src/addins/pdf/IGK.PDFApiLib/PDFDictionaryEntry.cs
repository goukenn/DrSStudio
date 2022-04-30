

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFDictionaryEntry.cs
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
file:PDFDictionaryEntry.cs
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
    public class PDFDictionaryEntry: IPDFDictionaryEntry 
    {
        private PDFIdentifier m_identifier;
        private PDFDataBase m_data;
        #region IPDFDictionaryEntry Members
        public PDFIdentifier Identifier
        {
            get
            {
                return this.m_identifier;
            }
            set
            {
                this.m_identifier = value;
            }
        }
        public PDFDataBase Data
        {
            get
            {
                return this.m_data;
            }
            set
            {
                this.m_data = value;
            }
        }
        #endregion
        public PDFDictionaryEntry(PDFIdentifier identifier, PDFDataBase data)
        {
            if (identifier == null)
                throw new ArgumentNullException("identifier");
            if (data == null)
                throw new ArgumentNullException("data");
            this.m_identifier = identifier;
            this.m_data = data;
        }
    }
}

