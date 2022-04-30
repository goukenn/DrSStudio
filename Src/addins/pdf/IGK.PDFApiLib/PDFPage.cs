


using IGK.ICore;
/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFPage.cs
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
file:PDFPage.cs
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
    public class PDFPage : PDFTypeObject
    {
        private PDFPageNode m_Parent;
        private Colorf m_fillColor;
        private Colorf m_strokeColor;
        private Colorf m_textColor;
        public PDFPageNode Parent {
            get {                
                return m_Parent ;
            }
            internal set {
                this.m_Parent = value;
                if (value !=null)
                    this.Data.Entries.Add(PDFConstant .PARENT, new PDFIndirectObject(value));
                else
                    this.Data.Entries.Remove (PDFConstant.PARENT );
            }
        }
        public void SetColor(float r, float g, float b)
        { 
        }
    
        public PDFRectangle MediaBox {
            get { return GetEntry(PDFConstant.MEDIABOX) as PDFRectangle; }
            set {
                this.Data.Entries[PDFConstant.MEDIABOX] = new PDFDictionaryEntry(PDFIdentifier.MediaBox, value);
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        internal PDFPage()
        {
            this.m_fillColor = Colorf.White;
            this.m_strokeColor = Colorf.Black;
            this.m_textColor = Colorf.Black;
            this.Data.Entries.Add(PDFConstant.TYPE, new PDFIdentifier(PDFConstant.PAGE));
            this.Data.Entries.Add(PDFConstant.MEDIABOX, new PDFRectangle(0, 0, 300, 300));
            this.Data.Entries.Add(PDFConstant.RESOURCES, new PDFResources());
        }
        public void DrawString(PDFFont font, string text,float size, int x, int y)
        {
            if (font == null)
                throw new ArgumentNullException("font");
            PDFObject obj =new PDFObject ();
            this.Document.Body.Objects.Add(obj);
            obj.Stream = new PDFStreamData ();
            StringBuilder sb = new StringBuilder();
            sb.Append("BT");
            sb.Append(PDFConstant.EOF);
            sb.Append(font.Name.ToPdf());
            sb.Append((char)PDFConstant.SPACE);            
            sb.Append(size+" Tf");
            sb.Append(PDFConstant.EOF);
            sb.Append(string.Format ("{0} {1} Td", x, y));
            sb.Append(PDFConstant.EOF);
            sb.Append(string.Format("{0} Tj", new PDFLitteralString(text).ToPdf ()));
            sb.Append(PDFConstant.EOF);
            sb.Append("ET");
            obj.Stream.Value = sb.ToString();
            PDFResources rs = this.Data.GetEntry (PDFConstant.RESOURCES ) as PDFResources;
            if (rs != null)
            {
                if (rs.ProcSet == null)
                {
                    rs.ProcSet = new PDFArrayData();
                }
                rs.ProcSet.Add(new PDFIdentifier("PDF"));
                rs.ProcSet.Add(new PDFIdentifier("Text"));
                if (rs.Font == null)
                {
                    rs.Font = new PDFDictionaryData();
                }
                rs.Font.Entries.Add(font.Name.ToString (), new PDFIndirectObject(font));
            }
            obj.Data.Entries.Add("Length", (PDFIntegerData )obj.Stream.Value.Length);
            PDFDataBase data = this.Data.GetEntry(PDFConstant.CONTENTS);
            if (data == null)
            {
                this.Data.Entries.Add(PDFConstant.CONTENTS, new PDFIndirectObject(obj));
            }
            else { 
                PDFArrayData d = new PDFArrayData ();
                d.Add (data );
                d.Add (new PDFIndirectObject(obj ));
                this.Data.Entries[PDFConstant.CONTENTS].Data = d;
            }
        }
    }
}

