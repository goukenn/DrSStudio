

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFIdentifier.cs
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
file:PDFIdentifier.cs
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
    public class PDFIdentifier : PDFDataValueBase 
    {
        public static readonly PDFIdentifier MediaBox = new PDFIdentifier(PDFConstant.MEDIABOX);
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(PDFIdentifier i, PDFIdentifier j)
        {
            if ((i is PDFIdentifier )&&(j is PDFIdentifier ))
                return i.Value == j.Value ;
            return false;
        }
        public static bool operator !=(PDFIdentifier i, PDFIdentifier j)
        {
            if ((i is PDFIdentifier) && (j is PDFIdentifier))
                return i.Value != j.Value;
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is PDFIdentifier)
            {
                return (obj as PDFIdentifier).Value == this.Value;
            }
            return base.Equals(obj);
        }
        public static implicit operator PDFIdentifier (string value)
        {
            return new PDFIdentifier (value );
        }
        public override void Render(System.IO.Stream stream)
        {
            byte[] t =Utils.TextUtils.GetUTF8Bytes ( string.Format("/" + this.Value));
            stream.Write(t, 0, t.Length);
        }
        public PDFIdentifier(string value)
        {
            this.Value = value;
        }
        public override string ToString()
        {
            return this.Value;
        }
        public override string ToPdf()
        {
            return string.Format("/" + this.Value);
        }
    }
}

