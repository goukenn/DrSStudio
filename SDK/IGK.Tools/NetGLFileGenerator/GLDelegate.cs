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
    class GLDelegate : GLMethod 
    {

        public GLDelegate(string name, string type)
            : base(name, type.Trim ())
        { 
 
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Type.Contains("bool"))
            { 
            }
            switch (Type.ToLower ())
            {
                case "bool":
                    sb.Append("[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]");
                    break;
            }
            sb.Append("public delegate " + Type + " " + this.Name + "( " + this.GetArsString() + ");");
            return sb.ToString();
        }
    }
}
