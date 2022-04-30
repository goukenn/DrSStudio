/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetGLFileGenerator
{
    class GLUtils
    {
        public static string TreatReturnType(string type)
        {
            if (type.Contains(" *"))
                type = type.Replace(" *", "*");
            type = type.Trim();

            type = type.Replace("GLintptr", "IntPtr");
            type = type.Replace("const GLubyte*", "byte[]");
            type = type.Replace("GLvoid*", "IntPtr");
            type = type.Replace("GLboolean", "bool");
            type = type.Replace("GLint", "int");
            type = type.Replace("GLuint", "uint");
            type = type.Replace("GLhandleARB", "IntPtr");
            type = type.Replace("GLvdpauSurfaceNV", "IntPtr");
            type = type.Replace("GLenum", "uint");
            type = type.Replace("GLsync", "IntPtr");
            
            if (type.Contains("*"))
            {
                type = "IntPtr";
            }
            return type;
        }
    }
}
