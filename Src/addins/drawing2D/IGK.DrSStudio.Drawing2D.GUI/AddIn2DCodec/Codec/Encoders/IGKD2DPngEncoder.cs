

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DPngEncoder.cs
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
file:IGKD2DPngEncoder.cs
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
using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.IO;
    [IGKD2DBitmapEncoderAttribute("Png", "pictures/png", "png")]
    class PngEncoder : IGKD2DPictureEncoderBase 
    {
        private enuPngBitCount m_BitCount;
        public enuPngBitCount BitCount
        {
            get { return m_BitCount; }
            set
            {
                if (m_BitCount != value)
                {
                    m_BitCount = value;
                }
            }
        }
        public PngEncoder()
        {
            this.m_BitCount = enuPngBitCount.Bitcount24;
        }
        public override bool CanConfigure => true;

        public override enuParamConfigType GetConfigType()=> enuParamConfigType.ParameterConfig;
        
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            ICoreParameterGroup v_group = parameters.AddGroup("CodecDefinition", "lb.CodecDefinition.caption");
            v_group.AddItem(this.GetType().GetProperty("BitCount"));
            return parameters;
        }
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
                        "Png"),
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
                        "Png");
                }
                using (ICoreBitmap bmp = documents[0].ToBitmap(
                    documents[0].Width,
                    documents[0].Height,
                    false,
                    DpiX, DpiY))
                {
                    return SavePicture(filename, bmp);
                }
            }
            return false;
        }
        public override bool SavePicture(string filename, ICoreBitmap bitmap)
        {
            Image v_img = null;
            if (bitmap is WinCoreBitmap )
            {
                v_img = (bitmap as WinCoreBitmap).Bitmap;
            }
            else {
                v_img = bitmap.ToGdiBitmap(false);
            }
            if (v_img == null)
                return false;

            switch (this.BitCount)
            {
                case enuPngBitCount.Bitcount4:
                    using (var g = (v_img as Bitmap).Clone(new Rectangle(Point.Empty, v_img.Size),
                        PixelFormat.Format4bppIndexed)) {
                        g.Save(filename, ImageFormat.Png);
                    }
                    return true;
                case enuPngBitCount.Bitcount8:
                    using (var g = (v_img as Bitmap).Clone(new Rectangle(Point.Empty, v_img.Size),
                       PixelFormat.Format8bppIndexed))
                    {
                        g.Save(filename, ImageFormat.Png);
                    }
                    return true;
                case enuPngBitCount.Bitcount32:
                    break;
                case enuPngBitCount.Bitcount24:
                    using (var g = (v_img as Bitmap).Clone(new Rectangle(Point.Empty, v_img.Size),
                      PixelFormat.Format24bppRgb))
                    {
                        g.Save(filename, ImageFormat.Png);
                    }
                    return true;
                default:
                    break;
            }

            ImageFormat img = ImageFormat.Bmp;
            ImageCodecInfo v_info = GetImageEncodersInfo("PNG");
            EncoderParameters v_p = new EncoderParameters(1);
            v_p.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth,
                (long)this.BitCount);
            SetPropertyItem(v_img, filename);
            bool v = false;
            try
            {
                v_img.Save(filename, v_info, v_p);                
                v = true;
            }
            catch
            {
                throw new CoreException(enuExceptionType.ParameterNotValid, "Bitmap");
            }
            return v;
            //ImageFormat img = ImageFormat.Bmp;
            //ImageCodecInfo v_info = GetImageEncodersInfo("PNG");
            //EncoderParameters v_p = new EncoderParameters(1);
            //v_p.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth,
            //    (long)this.BitCount);
            //   SetPropertyItem(bitmap , filename);
            //    try
            //    {
            //        bitmap .Save(filename, v_info, v_p);
            //    }
            //    catch (Exception ex)
            //    {
            //        //throw new CoreException(enuExceptionType.ParameterNotValid, "Bitmap : "+ex.Message );
            //        CoreLog.WriteError (ex.Message);
            //        return false;
            //    }
            //return true;
        }
    }
}

