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
using System.Runtime.InteropServices;
namespace NetGLFileGenerator
{
    class GLStaticMethod : GLMethod 
    {
        public GLStaticMethod(string name, string type):base(name , type )
        {
            if (this.Name == "glGetString")
            {
                this.Type = "IntPtr";
                this.Name = "_glGetString";
            }
        }
        public override string ToString()
        {
            
            StringBuilder sb = new StringBuilder ();
            if (this.Name == "_glGetString")
            {
                sb.AppendLine(string.Format("[DllImport(\"opengl32.dll\",EntryPoint=\"glGetString\", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)] public static extern {0} {1} ({2});", this.Type, this.Name, GetArsString()));
            }
            else 
            sb.AppendLine (string.Format("[DllImport(\"opengl32.dll\")] public static extern {0} {1} ({2});", this.Type, this.Name , GetArsString ()));
            return sb.ToString();

        }
    }
}
