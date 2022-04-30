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
    class GLMethod
    {
        Dictionary<string, MethodArguments> m_arguments;
        private string m_Name;
        private string m_Type;

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
        public GLMethod(string Name, string Type)
        {
            if (string.IsNullOrEmpty(Name))
                throw new Exception("Name is null");
            if (string.IsNullOrEmpty(Type))
                Type = "void";
            this.m_Name = Name;
            this.m_Type = Type;
            this.m_arguments = new Dictionary<string, MethodArguments>();
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public void AddArgs(MethodArguments arg)
        {
            if (this.m_arguments.ContainsValue(arg))
                return;
            if (this.m_arguments.ContainsKey(arg.Name))            
            {
                arg.Name = string.Format("{0}_{1}", arg.Name, this.m_arguments.Count);
            }
            this.m_arguments.Add(arg.Name, arg);
        }

        public string GetArsString()
        {
            StringBuilder sb = new StringBuilder();
            bool comma =false ;
            foreach (KeyValuePair<string, MethodArguments > item in this.m_arguments)
            {
                if (comma )
                    sb.Append (",");
                sb.Append(item.Value.ToString());
                if (!comma )
                    comma = true;
            }
            return sb.ToString();
        }
        public string GetParams()
        {
           
            StringBuilder sb = new StringBuilder();
            bool comma = false;
            foreach (KeyValuePair<string, MethodArguments> item in this.m_arguments)
            {
                if (!comma && (this.m_arguments.Count == 1))
                {
                    if (item.Value.Type == "void")
                        return string.Empty;
                }
                if (comma)
                    sb.Append(",");
                if (!string.IsNullOrEmpty(item.Value.Modifier))
                    sb.Append(item.Value.Modifier+" ");
                sb.Append(item.Value.Name );
                if (!comma)
                    comma = true;
            }
            return sb.ToString();
        }

        public MethodArguments[] MethodArgs()
        {
            List<MethodArguments> m = new List<MethodArguments>();
            foreach (KeyValuePair<string, MethodArguments> item in this.m_arguments)
            {

                m.Add(item.Value);

            }
            return m.ToArray();
        }
    }
}
