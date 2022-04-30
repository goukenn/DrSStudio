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
    class MethodArguments
    {
        private string m_Name;
        private string m_Type;
        private string m_Modifier;

        public string Modifier
        {
            get { return m_Modifier; }
            set
            {
                if (m_Modifier != value)
                {
                    m_Modifier = value;
                }
            }
        }
        public string Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type != value)
                {
                    m_Type = value;
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

        public string GetValue()
        {
            if (this.Type == "void")
                return string.Empty;
            StringBuilder sb = new StringBuilder();
        

            sb.Append(this.Type + " " + this.Name);
            return sb.ToString();
        }

        public MethodArguments(string name, string type)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Name is Null");
            }
            if (string.IsNullOrEmpty(type))
            {
                throw new Exception("Type is Null");
            }
            
            this.m_Name = name;
            this.m_Type = type;

            if (type.StartsWith("ref "))
            {
                this. m_Modifier = "ref";
            }
        }
        public override string ToString()
        {
            return this.GetValue();
        }
    }
}
