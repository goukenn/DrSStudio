

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreRendererBase.cs
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
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// represent the base abstract core rendering base
    /// </summary>
    public abstract class CoreRendererBase
    {
        /// <summary>
        /// used to initialize the rendering theme engine properties
        /// </summary>
        /// <param name="t"></param>
        public static void InitRenderer(Type t)
        {
            try
            {
                foreach (var item in t.GetProperties(System.Reflection.BindingFlags.Static| System.Reflection.BindingFlags.Public ))
                {
#if DEBUG
                    CoreLog.WriteLine("[IGK.ICore] - Load rendering setting properties <<" + item.Name+">>");
#endif
                    item.GetValue(null,null);
                }
            }
            catch
            {
            }
        }
    }
}
