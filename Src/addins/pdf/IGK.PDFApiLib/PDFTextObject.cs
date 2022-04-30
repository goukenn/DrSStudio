

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFTextObject.cs
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
file:PDFTextObject.cs
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
    class PDFTextObject : PDFDataBase 
    {
        private PDFDictionaryData m_data;
        private PDFFont m_Font;
        public PDFFont Font
        {
            get { return m_Font; }
            set
            {
                if (m_Font != value)
                {
                    m_Font = value;
                }
            }
        }
        public PDFIntegerData FontSize { get { return m_data.Entries["Tf"].Data as PDFIntegerData; } }
        public PDFArrayData Location { get { return m_data.Entries["Tf"].Data as PDFArrayData; } }
        public PDFStringData Text { get { return m_data.Entries["Tj"].Data as PDFStringData; } }
        public PDFTextObject()
        {
            m_data = new PDFDictionaryData();
        }
        public override void Render(System.IO.Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BT"+PDFConstant.EOF );
            if (this.Font != null)
                sb.Append(this.Font.Name);
            foreach (PDFDataBase  item in this.m_data.Entries )
            {
            }
            sb.Append("LT"+PDFConstant.EOF );
            Utils.TextUtils .WriteString (stream , sb.ToString());
        }
        public override string ToPdf()
        {
            return string.Empty;
        }
    }
}

