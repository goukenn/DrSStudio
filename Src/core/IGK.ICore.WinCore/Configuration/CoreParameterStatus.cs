

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterStatus.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreParameterStatus.cs
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
using System.Text;
using IGK.ICore.WinUI.Configuration;
namespace IGK.ICore.WinCore.Configuration
{
    /// <summary>
    /// represent the core parameter status
    /// </summary>
    public class CoreParameterStatus : CoreParameterItemBase , ICoreParameterStatus 
    {
        private System.Reflection.PropertyInfo m_PropertyInfo;
        private object m_Target;
        public object Target
        {
            get { return m_Target; }
        }
        /// <summary>
        /// get or set the property information
        /// </summary>
        public System.Reflection.PropertyInfo PropertyInfo
        {
            get { return m_PropertyInfo; }
            set
            {
                if (m_PropertyInfo != value)
                {
                    m_PropertyInfo = value;
                }
            }
        }
        /// <summary>
        /// .ctr 
        /// </summary>
        /// <param name="name">name of this</param>
        /// <param name="captionkey">name of </param>
        public CoreParameterStatus(string name, string captionkey, object target):base(name,captionkey )
        {
            if (target == null)
                throw new ArgumentNullException("target");
            this.m_Target = target;
        }
    }
}

