

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EmfEncoder.cs
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
file:EmfEncoder.cs
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
using System.Reflection;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    [BitmapEncoderAttribute("EMF", "pictures/emf", "emf")]
    class EmfEncoder : PictureEncoderBase 
    {
        protected override bool SaveDocument(string filename, ICore2DDrawingDocument[] documents)
        {
            string v_dir = Path.GetDirectoryName(filename);
            string v_file = Path.GetFileNameWithoutExtension(filename);
            string v_ext = Path.GetExtension(filename).ToUpper();
            if (documents.Length > 1)
            {
                for (int i = 0; i < documents.Length; i++)
                {
                    if (!SaveDocument(string.Format("{0}{1}{2}.{3}" +
                        v_dir,
                        Path.DirectorySeparatorChar,
                        v_file + "_" + i,
                        "emf"),
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
                        "emf");
                }
                //ImageFormat img = ImageFormat.Bmp;
                //ImageCodecInfo v_info = GetImageEncodersInfo("JPEG");                
               return  this.SavePicture(filename, CoreBitmapOperation.GetBitmap(documents[0], DpiX, DpiY));
            }
            return false;
        }
        public override bool SavePicture(string filename, Bitmap bitmap)
        {
            if ((bitmap == null) || (bitmap.PixelFormat == PixelFormat.Undefined))
                return false;
                //SetPropertyItem(bmp, filename);
                try
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        IntPtr hdc = g.GetHdc();
                        MemoryStream mem = new MemoryStream();
                        Metafile mfile = new Metafile(mem, hdc);
                        Graphics bf = Graphics.FromImage(mfile);
                        //documents[0].Draw(bf);
                        bf.DrawImage(bitmap, Point.Empty);
                        bf.Flush();
                        bf.Dispose();
                        g.ReleaseHdc(hdc);
                        SetPropertyItem(mfile, filename);
                        mem.Seek(0, SeekOrigin.Begin);
                        FileStream fs = File.Create(filename);
                        mem.WriteTo(fs);
                        fs.Flush();
                        fs.Close();
                        mem.Close();
                        mfile.Dispose();
                    }
                }
                catch
                {
                    throw new CoreException(enuExceptionType.ParameterNotValid, "Bitmap");
                }            
            return true;
        }
    }
}

