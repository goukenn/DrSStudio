

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCodecTypeManager.cs
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
file:CoreCodecTypeManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// represent a codec type manager
    /// </summary>
    public class CoreCodecTypeManager
    {
        static Dictionary<string, object> m_renderers;
        public CoreCodecTypeManager()
        {
            m_renderers = new Dictionary<string, object>();
        }
        /// <summary>
        /// retrieve an encoder by name
        /// </summary>
        /// <param name="name"></param>
        public static void GetEncoder(string name) { 
        }
    }
}

