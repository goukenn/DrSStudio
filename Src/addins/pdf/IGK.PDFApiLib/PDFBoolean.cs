

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFBoolean.cs
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
file:PDFBoolean.cs
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
    public sealed class PDFBoolean : IPDFDataType 
    {
        public static readonly PDFBoolean True;
        public static readonly PDFBoolean False;
        public string m_value;
        static PDFBoolean()
        {
            True = new PDFBoolean();
            True.m_value = "true";
            False = new PDFBoolean();
            False.m_value = "false";
        }
        private PDFBoolean() { }
        public string Value { get { return this.m_value; } }
        #region IPDFDataType Members
        public void Render(System.IO.Stream stream)
        {
            byte[] t = Utils.TextUtils.GetUTF8Bytes(this.Value);            
            stream.Write(t, 0, t.Length);
        }
        #endregion
    }
}

