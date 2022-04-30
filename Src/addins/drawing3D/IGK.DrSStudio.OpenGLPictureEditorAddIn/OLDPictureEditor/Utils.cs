

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Utils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:Utils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    /// <summary>
    /// represent utility
    /// </summary>
    public class Utils
    {
        public static string getSaveFilter()
        {
            StringBuilder sub = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            IGK.DrSStudio.Codec.ICoreCodec[] d = IGK.DrSStudio.Codec.CoreEncoderBase.GetEncoders();
            sb.Append(CoreSystem.GetString("MSG.AllSupportedFiles") + "|");
            bool flag = false;
            string filter = string.Empty;
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] is IGK.DrSStudio.Drawing2D.Codec.ICoreBitmapEncoder)
                {
                    if (flag)
                    {
                        sub.Append("|");
                    }
                    filter = d[i].GetFilter();
                    sub.Append(filter);
                    sb.Append(filter.Split('|')[1]);
                    flag = true;
                }
            }
            sb.Append("|"+sub.ToString());
            return sb.ToString();
        }
    }
}

