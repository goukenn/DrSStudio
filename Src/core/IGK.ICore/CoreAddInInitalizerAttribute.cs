

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAddInInitalizerAttribute.cs
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
file:CoreAddInInitalizerAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    [AttributeUsage (AttributeTargets.Class, AllowMultiple=false , Inherited = false )]
    public class CoreAddInInitializerAttribute : Attribute
    {
        private bool m_Initializer;
        public bool Initializer
        {
            get { return m_Initializer; }
            set
            {
                if (m_Initializer != value)
                {
                    m_Initializer = value;
                    
                }
            }
        }
        public CoreAddInInitializerAttribute(bool initializer)
        {
            this.m_Initializer = initializer;
        }
    }
}

