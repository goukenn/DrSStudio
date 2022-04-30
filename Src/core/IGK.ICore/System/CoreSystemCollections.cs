

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSystemCollections.cs
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
file:CoreSystemCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    [Serializable()]
    /// <summary>
    /// represent the base system collection
    /// </summary>
    public abstract class CoreSystemCollections : MarshalByRefObject 
    {
        protected CoreSystem m_core;
        protected  CoreSystemCollections(CoreSystem m_core)
        {
            this.m_core = m_core;
            m_core.RegisterTypeLoader (this.RegisterType);
        }
        //register type
        protected abstract void RegisterType(Type type);
        public override string ToString()
        {
            return string.Format("SystemCollections[#]");        
        }
    }
}

