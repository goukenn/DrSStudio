

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DSystemIconEncoder.cs
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
file:IGKD2DSystemIconEncoder.cs
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
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.IO;

    /// <summary>
    /// uses to encode system icon
    /// </summary>
    [IGKD2DBitmapEncoderAttribute("SystemIconEncoder", "images/icon", "ico")]
    public sealed class IGKD2DSystemIconEncoder : IGKD2DPictureEncoderBase 
    {
        protected override bool SaveDocument(string filename, ICore2DDrawingDocument[] documents)
        {
            string v_dir = PathUtils.GetDirectoryName(filename);
            string v_file = Path.GetFileNameWithoutExtension(filename);
            string v_ext = Path.GetExtension(filename).ToUpper();
            if (documents.Length > 1)
            {
                for (int i = 0; i < documents.Length; i++)
                {
                    if (!SaveDocument(string.Format("{0}{1}{2}.{3}" ,
                        v_dir,
                        Path.DirectorySeparatorChar,
                        v_file + "_" + i,
                        "ico"),
                        new ICore2DDrawingDocument[] { documents[i] }))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (documents.Length == 1)
            {
                if (!this.Extensions.Contains(v_ext))
                {
                    filename = string.Format("{0}{1}{2}.{3}" +
                        v_dir,
                        Path.DirectorySeparatorChar,
                        v_file,
                        "ico");
                }
                return SavePicture(filename, documents[0].ToBitmapDpi (DpiX, DpiY));
            }
            return false;
        }
        public override bool SavePicture(string filename, ICoreBitmap bitmap)
        {
            if ((bitmap == null) || (bitmap.PixelFormat == enuPixelFormat.Undefined))
                return false;
            using (Bitmap bmp = bitmap.ToGdiBitmap())
            {
                if (bmp == null)
                    return false;
                Icon i = WinCoreBitmapOperation.GetSystemIcon(bmp);
                try
                {
                    StreamWriter sm = new StreamWriter(File.Create(filename));
                    i.Save(sm.BaseStream);
                    sm.Flush();
                    sm.Close();
                }
                catch
                {
                    throw new CoreException(enuExceptionType.ParameterNotValid, "Icon");
                }
                i.Dispose();
            }
            return true;
        }
    }
}

