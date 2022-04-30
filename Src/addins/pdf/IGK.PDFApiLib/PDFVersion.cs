

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFVersion.cs
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
file:PDFVersion.cs
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
using System.IO;
namespace IGK.PDFApi
{
    /// <summary>
    /// represent pdf file file versrion
    /// </summary>
    public sealed class PDFVersion : IPDFVersion 
    {
        const string VERSION_1_0 = "PDF-1.0";
        const string VERSION_1_1 = "PDF-1.1";
        const string VERSION_1_2 = "PDF-1.2";
        const string VERSION_1_3 = "PDF-1.3";
        const string VERSION_1_4 = "PDF-1.4";
        const string VERSION_1_5 = "PDF-1.5";
        const string VERSION_1_6 = "PDF-1.6";
        const string VERSION_1_7 = "PDF-1.7";
        private string m_version;
        public static readonly PDFVersion Version_1_0;
        public static readonly PDFVersion Version_1_1;
        public static readonly PDFVersion Version_1_2;
        public static readonly PDFVersion Version_1_3;
        public static readonly PDFVersion Version_1_4;
        public static readonly PDFVersion Version_1_5;
        public static readonly PDFVersion Version_1_6;
        public static readonly PDFVersion Version_1_7;
        public string Value { get { return this.m_version; } }
        private PDFVersion(string version)
        {
            this.m_version = version;
        }
        static PDFVersion() {
            Version_1_0 = new PDFVersion(VERSION_1_0);
            Version_1_1 = new PDFVersion(VERSION_1_1);
            Version_1_2 = new PDFVersion(VERSION_1_2);
            Version_1_3 = new PDFVersion(VERSION_1_3);
            Version_1_4 = new PDFVersion(VERSION_1_4);
            Version_1_5 = new PDFVersion(VERSION_1_5);
            Version_1_6 = new PDFVersion(VERSION_1_6);
            Version_1_7 = new PDFVersion(VERSION_1_7);
        }
        public void Render(Stream stream)
        {
            Utils.TextUtils.WriteString (stream , 
                string.Format("%" + this.m_version+"{0}", PDFConstant.EOF  ));
        }
        public override string ToString()
        {
            return this.m_version;
        }
    }
}

