

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFDataBase.cs
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
file:PDFDataBase.cs
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
using System.Reflection;
namespace IGK.PDFApi
{
    public abstract class PDFDataBase
    {
        public virtual void Render(System.IO.Stream stream) {
            Utils.TextUtils.WriteString(stream, this.ToPdf());
        }
        public virtual string ToPdf() {
            return string.Empty;
        }
        public static PDFDataBase CreatePDFObject(string name)
        {
            string f = string.Format(System.Reflection.MethodInfo.GetCurrentMethod ().DeclaringType .Namespace +".PDF" + name);
            Type t = Type.GetType(f);
            if (t == null)
                return null;
            ConstructorInfo  m = t.GetConstructor(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                 null, new Type[0], null);
            object o = m.Invoke(null);
            return o as PDFDataBase ;
        }
        public static PDFDataBase CreatePDFObject(Type Type)
        {
            Type t = Type;
            if (t == null)
                return null;
            ConstructorInfo  m = t.GetConstructor(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                 null, new Type[0], null);
            object o = m.Invoke(null);
            return o as PDFDataBase ;
        }
    }
}

