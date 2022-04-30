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
    class GLExtendedMethod : GLMethod 
    {
        public GLExtendedMethod(string name, string type)
            : base(name, type)
        {

        }
        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("public static {0} {1} ( {2} )", this.Type, this.Name, GetArsString()));
            sb.Append("{");
            sb.Append(string.Format ("if (__{0}!=null) ", this.Name ));
            if (this.Type == "void")
                sb.Append(string.Format("__{0}({1});", this.Name, this.GetParams()));
            else
                sb.Append(string.Format("return __{0}({1});", this.Name, this.GetParams()));
            sb.Append(string.Format("else throw new NotImplementedException(\"{0}\");", this.Name));
            sb.AppendLine("}");
            return sb.ToString();
            
        }
    }
}
