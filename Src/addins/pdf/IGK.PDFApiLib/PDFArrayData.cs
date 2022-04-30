

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFArrayData.cs
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
file:PDFArrayData.cs
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
    public class PDFArrayData : PDFDataBase
    {
        List<PDFDataBase> m_data;
        public PDFArrayData()
        {
            this.m_data = new List<PDFDataBase>();
        }
        public int Count {
            get {
                return this.m_data.Count;
            }
        }
        public override string ToString()
        {
            return string.Format ("PDFArrayData [Count: {0}]", Count );
        }
        public override void Render(System.IO.Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            Utils.TextUtils.WriteString(stream, "[");
            int i = 0;
            foreach (PDFDataBase item in m_data)
            {
                if (i!=0)
                    stream.WriteByte (PDFConstant.SPACE );
                item.Render(stream);
                i++;
            }
            Utils.TextUtils.WriteString(stream, "]");
        }
        public void Add(PDFDataBase data)
        { 
            if ((data!=null) && !this.m_data .Contains (data ))
            {
                if (data is PDFArrayData)
                {
                    PDFArrayData v_h = data as PDFArrayData;
                    foreach (PDFDataBase  c in v_h.m_data)
                    {
                        this.Add(c);
                    }
                    return;
                }
                this.m_data.Add (data );
            }
        }
    }
}

