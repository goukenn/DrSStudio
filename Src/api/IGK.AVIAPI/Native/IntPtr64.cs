

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IntPtr64.cs
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
file:IntPtr64.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
namespace IGK.AVIApi
{
    [Serializable]
    [ComVisible(true)]
    [StructLayout ( System.Runtime.InteropServices.LayoutKind .Sequential )]
    public struct IntPtr64 : ISerializable
    {
        internal Int32 defHandle;
        internal Int32 exdefHandle;
        public static int Size { get { return 8; } }
        #region ISerializable Members
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

