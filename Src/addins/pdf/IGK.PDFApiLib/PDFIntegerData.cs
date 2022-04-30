

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFIntegerData.cs
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
file:PDFIntegerData.cs
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
    public class PDFIntegerData : PDFDataBase 
    {
        private int m_Value;
        public int Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }
        private PDFIntegerData(int value)
        {
            this.m_Value = value;
        }
        public static implicit operator PDFIntegerData (string value)
        {
            int i = 0;
            if (string.IsNullOrEmpty (value ) || ! int.TryParse (value , out i))
                return new PDFIntegerData (0);
            return new PDFIntegerData (i);
        }
        public static implicit operator PDFIntegerData(int value)
        {         
            return new PDFIntegerData(value);
        }
        public override void Render(System.IO.Stream stream)
        {
            Utils.TextUtils.WriteString(stream, this.Value.ToString());
        }
        public override string ToString()
        {
            return this.m_Value.ToString();
        }
        public override string ToPdf()
        {
            return this.m_Value.ToString();
        }
    }
}

