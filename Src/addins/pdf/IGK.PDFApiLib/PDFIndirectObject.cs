

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFIndirectObject.cs
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
file:PDFIndirectObject.cs
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
    public class PDFIndirectObject : PDFDataBase
    {
        private IPDFObject m_Target;
        public IPDFObject Target
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
        public override void Render(System.IO.Stream stream)
        {
            if (Target != null)
            {
                Utils.TextUtils.WriteString(stream, m_Target.Id.ToString());
                stream.WriteByte(PDFConstant.SPACE);    
                Utils.TextUtils.WriteString(stream,  m_Target.Update.ToString() );
                stream.WriteByte(PDFConstant.SPACE);
                Utils.TextUtils.WriteString(stream,  "R");                    
            }
        }
        internal  PDFIndirectObject(IPDFObject Target)
        {
            this.m_Target = Target;
        }
    }
}

