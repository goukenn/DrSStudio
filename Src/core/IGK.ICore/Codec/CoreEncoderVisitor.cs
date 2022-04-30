

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreEncoderVisitor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Codec
{
    /// <summary>
    /// Represent a visitor 
    /// </summary>
    public abstract  class CoreEncoderVisitor
    {

        public virtual string Out
        {
            get
            {
                return string.Empty;
            }
        }
        public CoreEncoderVisitor()
        {
        }

        public bool Accept(object obj)
        {
            if (obj == null)
                return false;
            MethodInfo minfo = GetType().GetMethod("Visit", new Type[] { obj.GetType() });
            return (minfo != null);
        }
        public void Visit(object obj)
        {
            if (obj == null)
                return;
            MethodInfo minfo = GetType().GetMethod("Visit", new Type[] { obj.GetType() });
            if ((minfo != null) && (!minfo.MethodHandle.Equals(MethodInfo.GetCurrentMethod().MethodHandle)))
            {
                minfo.Invoke(this, new object[] { obj });
            }
        }
        public void writeInfo(object obj)
        {
            if (obj == null)
                return;
            MethodInfo minfo = GetType().GetMethod("writeInfo", new Type[] { obj.GetType() });
            if ((minfo != null) && (!minfo.MethodHandle.Equals(MethodInfo.GetCurrentMethod().MethodHandle)))
            {
                minfo.Invoke(this, new object[] { obj });
            }
        }

        public abstract void Flush();
    }
    
    
}
