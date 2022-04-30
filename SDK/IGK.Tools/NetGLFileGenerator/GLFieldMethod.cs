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
    class GLFieldMethod : GLMethod 
    {
        public GLFieldMethod(string name, string type)
            : base(name, type)
        {

        }
        public override string ToString()
        {
            return string.Format("internal static {0} __{1};", this.Type, this.Name);
        }
    }
}
