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
    public class GLConstant
    {
        private string m_Name;
        private string m_Value;

        public string  Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }

        public GLConstant(string name, string value)
        {
            this.m_Name = name;
            this.m_Value = value;
        }

        public override string ToString()
        {
            if (this.Name == "GL_TIMEOUT_IGNORED")
            {
                return string.Format("public const ulong {0} = {1};", this.Name, this.Value);
            }
            return string.Format("public const uint {0} = {1};", this.Name, this.Value);
        }
    }
}
