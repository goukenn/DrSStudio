

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFCounter.cs
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
file:PDFCounter.cs
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
    public class PDFCounter : PDFDataBase 
    {
        private PDFArrayData  m_Target;
        public PDFArrayData  Target
        {
            get { return m_Target; }
            set
            {
                if (m_Target != value)
                {
                    m_Target = value;
                }
            }
        }
        public PDFCounter(PDFArrayData data)
        {
            this.m_Target = data;
        }
        public int Count {
            get {
                if (this.Target !=null)
                return this.Target.Count;
                return -1;
            }
        }
        public override void Render(System.IO.Stream stream)
        {
            Utils.TextUtils.WriteString(stream, this.Count.ToString());
        }
    }
}

