

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCodecBase.cs
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
file:CoreCodecBase.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// represent the base class of all Codec in the system
    /// </summary>
    public abstract class CoreCodecBase  :  IComparable 
    {
        public static string GetFilter(ICoreCodec [] codecs)
        {
            if (codecs == null)
                return null;
            StringBuilder sb = new StringBuilder();
            bool t = false;
            foreach (ICoreCodec codec in codecs )
            {
                if (t)
                    sb.Append("|");
                sb.Append(codec.MimeType);
                sb.Append("|");
                foreach (string ext in codec.Extensions)
                {
                    sb.Append("*.");
                    sb.Append(ext);
                    sb.Append(";");
                }
                t = true;
            }
            return sb.ToString();
        }

        public abstract int CompareTo(object obj);

    }
}

