

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreException.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreException.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace IGK.ICore
{
    /// <summary>
    /// represent the default exception
    /// </summary>
    public class CoreException : ApplicationException 
    {
        private enuExceptionType m_ExceptionType;
        private int m_Code;
        public int Code
        {
            get { return m_Code; }
        }
        public enuExceptionType ExceptionType
        {
            get { return m_ExceptionType; }
        }
        public CoreException():base()
        {
        }
        private string m_AssemblyName;
        public string AssemblyName
        {
            get { return m_AssemblyName; }
            set
            {
                if (m_AssemblyName != value)
                {
                    m_AssemblyName = value;
                }
            }
        }
        public CoreException(Exception ex):base(ex.Message, ex )
        {
        }
        public CoreException(string message):base(message)
        {
        }
        public CoreException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public CoreException(enuExceptionType type, string message)
            : base(message)
        {
            this.m_ExceptionType = type;
        }
        public CoreException(enuExceptionType type, string message, int Code)
            : base(message)
        {
            this.m_ExceptionType = type;
            this.m_Code = Code;
        }
    }
}

